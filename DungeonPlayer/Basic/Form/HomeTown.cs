using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;
//using Microsoft.DirectX.DirectSound;
using System.Threading;
using System.Runtime.InteropServices;

namespace DungeonPlayer
{
    public partial class HomeTown : MotherForm
    {
        private MainCharacter mc = null;
        public MainCharacter MC
        {
            get { return mc; }
            set { mc = value; }
        }
        private MainCharacter sc = null;
        public MainCharacter SC
        {
            get { return sc; }
            set { sc = value; }
        }
        private MainCharacter tc = null;
        public MainCharacter TC
        {
            get { return tc; }
            set { tc = value; }
        }
        private WorldEnvironment we;
        public WorldEnvironment WE
        {
            get { return we; }
            set { we = value; }
        }
        private bool[] knownTileInfo;
        private bool[] knownTileInfo2;
        private bool[] knownTileInfo3;
        private bool[] knownTileInfo4;
        private bool[] knownTileInfo5;
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

        private OKRequest ok;

        private int firstDay = 1;
        //private int day = 1;
        //public int Day
        //{
        //    get { return day; }
        //    set { day = value; }
        //}

        private int targetDungeon = 1;
        public int TargetDungeon
        {
            get { return targetDungeon; }
        }

        private bool noFirstMusic = false;
        public bool NoFirstMusic
        {
            get { return noFirstMusic; }
            set { noFirstMusic = value; }
        }

        private void UpdateMainMessage(string message)
        {
            UpdateMainMessage(message, false);
        }
        private void UpdateMainMessage(string message, bool ignoreOK)
        {
            mainMessage.Text = message;
            if (!ignoreOK)
            {
                ok.ShowDialog();
            }
        }

        System.Threading.Thread th;
        bool endSign;
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


        public HomeTown()
        {
            InitializeComponent();
            //th = new Thread(new System.Threading.ThreadStart(UpdateXAudio));
            //th.IsBackground = true;
            //th.Start();
        }

        private void HomeTown_FormClosing(object sender, FormClosingEventArgs e)
        {
            endSign = true;
            GroundOne.StopDungeonMusic();
            if (GroundOne.sound != null) // ��ҕҏW
            {
                GroundOne.sound.StopMusic(); // ��ҕҏW
                // this.sound.Disactive(); // ��ҍ폜
            }

            // �u�x���v���̏����͉��ɑ΂��ċً}��������C�����킩��Ȃ��܂܂ł��B
            if (this.firstDay >= 7)
            {
                we.AvailableEquipShop = true;
            }
        }

        private void HomeTown_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "hometown.jpg");
            this.dayLabel.Text = we.GameDay.ToString() + "����";
            if (we.AlreadyRest)
            {
                this.firstDay = we.GameDay - 1; // �x���������ǂ����̃t���O�Ɋւ�炸���ɖK�ꂽ�ŏ��̓����L�����܂��B
            }
            else
            {
                this.firstDay = we.GameDay; // �x���������ǂ����̃t���O�Ɋւ�炸���ɖK�ꂽ�ŏ��̓����L�����܂��B
            }
            this.we.SaveByDungeon = false; // ���ɖ߂��Ă��邱�Ƃ��������߂̂��̂ł��B

        }

        private void HomeTown_Shown(object sender, EventArgs e)
        {
            if (!noFirstMusic)
            {
                GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
            }

            ok = new OKRequest();
            ok.StartPosition = FormStartPosition.Manual;
            ok.Location = new Point(this.Location.X + 540, this.Location.Y + 440);

            // ���S���Ă�����͎̂����I�ɕ��������܂��B
            if (mc != null)
            {
                if (mc.Dead)
                {
                    mainMessage.Text = "�_���W�����Q�[�g����s�v�c�Ȍ���" + mc.Name + "�ւƗ��ꍞ�ށB";
                    ok.ShowDialog();
                    mainMessage.Text = mc.Name + "�͖��𐁂��Ԃ����B";
                    mc.Dead = false;
                    mc.CurrentLife = mc.MaxLife / 2;
                    ok.ShowDialog();
                }
            }
            if (sc != null)
            {
                if (sc.Dead)
                {
                    mainMessage.Text = "�_���W�����Q�[�g����s�v�c�Ȍ���" + sc.Name + "�ւƗ��ꍞ�ށB";
                    ok.ShowDialog();
                    mainMessage.Text = sc.Name + "�͖��𐁂��Ԃ����B";
                    sc.Dead = false;
                    sc.CurrentLife = sc.MaxLife / 2;
                    ok.ShowDialog();
                }
            }
            if (tc != null)
            {
                if (tc.Dead)
                {
                    mainMessage.Text = "�_���W�����Q�[�g����s�v�c�Ȍ���" + tc.Name + "�ւƗ��ꍞ�ށB";
                    ok.ShowDialog();
                    mainMessage.Text = tc.Name + "�͖��𐁂��Ԃ����B";
                    tc.Dead = false;
                    tc.CurrentLife = tc.MaxLife / 2;
                    ok.ShowDialog();
                }
            }

            // �e�_���W�����N���A���ɓ��ʃC�x���g�𔭐������܂��B�\������グ�Ă��������B
            #region "���߂ċA��"
            if (!this.we.CommunicationFirstHomeTown)
            {
                mainMessage.Text = "�A�C���F�ӂ��A�����O�̒��ɖ߂��Ă������B���āA�ǂ��������ȁB";
                ok.ShowDialog();
                mainMessage.Text = "���i�F�����A�o�J�������Ɩ߂��Ă����悤�ˁB";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F���i�A�l�̖��O�������ƌĂׁB�A�C���w�l�x�ƌĂׁB";
                ok.ShowDialog();
                mainMessage.Text = "���i�F�����Łw�l�x�t���Ă鎞�_�Ńo�J��B�J�M�J�b�R�܂ŕt��������āE�E�E���o���Ȃ����B";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F�S�z����ȁB���o�͂��Ă���肾�B";
                ok.ShowDialog();
                mainMessage.Text = "���i�F�ǂ񂾂��o�J��E�E�E���A��������B�R���n���Ƃ��ˁB";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F��H�Ȃ񂾂���́B�ۂ������ȁB�������H";
                ok.ShowDialog();
                mainMessage.Text = "���i�F������B�A�C���A������ɖ߂�܂łɃ����X�^�[�ɏo���킷�̂͐h���Ǝv��Ȃ��H";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F�܂��ȁA�߂낤���Ď��ɏo�Ă���Ɣ��ɃE�U�������̏�Ȃ��B�����I�܂����I�H";
                ok.ShowDialog();
                mainMessage.Text = "���i�F�y�����̐����z  �_���W���������C�ɂ��̃����O�̒��ɖ߂邱�Ƃ��ł���́B";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F����������˂����B����Ȃ��߂�C�ɂ����K���K���i�߂���Ď����ȁB";
                ok.ShowDialog();
                mainMessage.Text = "���i�F�A�C���ɂ������B�厖�Ɏg���Ă�ˁB"; 
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F�}�W����I���₠�����邺�B�T���L���[�B";
                ok.ShowDialog();
                mc.AddBackPack(new ItemBackPack("�����̐���"));
                using (MessageDisplay md = new MessageDisplay())
                {
                    md.Message = "�y�����̐����z����ɓ���܂����B";
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.ShowDialog();
                }
                mainMessage.Text = "���i�F�ǂ���悱�ꂮ�炢�B���A�n���i���΂���A�K���c��������ɉ���Ă�����H";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F�����A�������ȁB���[�āA���ሥ�A�����Ă��邩�I";
                ok.ShowDialog();
                we.CommunicationFirstHomeTown = true;
            }
            #endregion
            #region "�P�K���e"
            else if (this.we.CompleteArea1 && !this.we.CommunicationCompArea1)
            {
                mainMessage.Text = "�A�C���F���[���A���i�I���i�I";
                ok.ShowDialog();
                mainMessage.Text = "���i�F�Ȃɂ�A�E�b�T�C��ˁB";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F���ɂP�K���e�������B�����������ȁI";
                ok.ShowDialog();
                mainMessage.Text = "���i�F�����A�A�C���܂����Ђ���Ƃ��āB�B�B";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F��H������E�E�E��A����I";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F�C�f�f�f�f�I�I�I�C�b�e�F�F�F�G�G�G�F�F�F�I�I";
                ok.ShowDialog();
                mainMessage.Text = "���i�F�Ӂ[��E�E�E���I�`���Ă킯����Ȃ������ˁB";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F�������Ă񂾁A������O����B���`�C�b�e�F�Ȃ��A�����B";
                ok.ShowDialog();
                mainMessage.Text = "���i�F��邶��Ȃ��B������Ƃ���������������B";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F�����A�����Ƒ����ɋ����Ă���I�b�n�b�n�b�n�I�I";
                ok.ShowDialog();
                if (this.we.GameDay >= 7)
                {
                    mainMessage.Text = "���i�F�o�ߓ�����" + this.we.GameDay.ToString() + "���ˁB�܂��A��o���Ȃ񂶂�Ȃ��H";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F�����悻�́g�o�ߓ����h���Ă̂́B";
                    ok.ShowDialog();
                    mainMessage.Text = "���i�F�_���W�����P�K���e�܂Ŕ�₵�����ԂɌ��܂��Ă邶��Ȃ��B";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F�������������錾�������ȁB�����Ƒf���ɉ��𑸂ׁI";
                    ok.ShowDialog();
                    mainMessage.Text = "���i�F�z���b�g�A�o�J��ˁB���Ԃ��ĈӖ��͕������Ă�킯�H";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F�h���B";
                    ok.ShowDialog();
                    mainMessage.Text = "���i�F�h�����Ăǂ������Ӗ��H";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F���h����B";
                    ok.ShowDialog();
                    mainMessage.Text = "���i�F���h������Ăǂ������Ӗ��H";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F���ԁB";
                    ok.ShowDialog();
                    mainMessage.Text = "���i�F�n�A�@�`�`�`�@�@�`�`�E�E�E�܁A������B�Ƃ肠�������߂łƂ��B";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F�T���L���[�I�I���O���炨�߂łƂ����o��Ȃ�Ċ������ˁB";
                    ok.ShowDialog();
                    mainMessage.Text = "���i�F�������Ă�̂�B�C�x�߂�A���A��A���A�߁B";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F����A�C�x�߂ł����������B�T���L���[�B";
                    ok.ShowDialog();
                    mainMessage.Text = "���i�F�����A���q������ˁB���������A�K���c����A�n���i�f�ꂳ��ɂ��񍐂�����H";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F�����A���̂��肾���B���A�������B�����͈ꏏ�ɔтł��H�����B";
                    ok.ShowDialog();
                    mainMessage.Text = "���i�F�����ˁB���j���͂��j�������A�ꏏ�����Ⴈ�����ȁB";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F��������I��������Q�K�ڎw�����B�܂��͂�����ƃK���c����Ƃ��s���Ă��邺�B";
                    ok.ShowDialog();
                    mainMessage.Text = "���i�F�t�t�t�A�Ƃ��Ƃƍs���Ă����B";
                    ok.ShowDialog();
                    we.CommunicationCompArea1 = true;
                }
                else
                {
                    mainMessage.Text = "���i�F�o�ߓ����́E�E�E" + this.we.GameDay.ToString() + "���H�E�\�A��k�ł���B";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F��k���B";
                    ok.ShowDialog();
                    mainMessage.Text = "���i�F���o�J�����Ă�̂�B�{�P��������T�ɁE�E�E���A�˂��˂��B";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F������H";
                    ok.ShowDialog();
                    mainMessage.Text = "���i�F�o���Ă�H";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F�P�K���e�̂��Ƃ��H����፡����������炢�o���Ă邳�B";
                    ok.ShowDialog();
                    mainMessage.Text = "���i�F��������Ȃ��āA���̑O��B";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F���i�A�������Ă񂾂��O�H";
                    ok.ShowDialog();
                    // �P�x�Q�[���N���A���Ă邩�ǂ����ŕ��򂵂Ă��������B
                    mainMessage.Text = "���i�F���肢�A�^�ʖڂɓ����āB�@�@�@�@�y�o���Ă�H�z";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F�E�E�E";
                    ok.ShowDialog();
                    if (we.TrueEnding1)
                    {
                        using (YesNoRequest yesno = new YesNoRequest())
                        {
                            yesno.StartPosition = FormStartPosition.CenterParent;
                            yesno.ShowDialog();
                            if (yesno.DialogResult == DialogResult.Yes)
                            {
                                mainMessage.Text = "�A�C���F�E�E�E�����A�s�v�c�ƂȁB�y�_���W�����ɓ��������A�o���Ă��邼�z";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F�����A���Ⴀ�����^�ʖڂɓ����āB�@�@�@�@�y�����͕��������H�z";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F���i�E�E�E";
                                ok.ShowDialog();
                                // �^���̌��t�F�P�����Ă������ǂ����ŕ��򂵂Ă��������B
                                yesno.StartPosition = FormStartPosition.CenterParent;
                                yesno.ShowDialog();
                                if (yesno.DialogResult == DialogResult.Yes)
                                {
                                    mainMessage.Text = "�A�C���F�E�E�E�y���B�͂܂��_���W�����̒��ɂ���z";
                                    ok.ShowDialog();
                                    mainMessage.Text = "���i�F�����ˁB������B���Ⴀ����͂ǂ��H�@�@�@�@�y�o����@�́H�z";
                                    ok.ShowDialog();
                                    mainMessage.Text = "�A�C���F�����~�߂Ă���Ȃ����B���͂��̂܂܂��O��";
                                    ok.ShowDialog();
                                    mainMessage.Text = "���i�F�ʖڂ�B�A�C�����肢�A�����āB";
                                    ok.ShowDialog();
                                    mainMessage.Text = "�A�C���F�E�E�E";
                                    ok.ShowDialog();
                                    // �^���̌��t�F�Q�����Ă������ǂ����ŕ��򂵂Ă��������B
                                    yesno.StartPosition = FormStartPosition.CenterParent;
                                    yesno.ShowDialog();
                                    if (yesno.DialogResult == DialogResult.Yes)
                                    {
                                        mainMessage.Text = "�A�C���F�y�肢�����Ȃ��ꏊ�ցA�肢���I���ꏊ�ցz";
                                        ok.ShowDialog();
                                        mainMessage.Text = "���i�F�����ˁB���悢��{����ĂƂ�������B�@�@�@�@�y�肢�Ƃ́H�z";
                                        ok.ShowDialog();
                                        mainMessage.Text = "�A�C���F���������~�߂�����Ă񂾂낤���I�I";
                                        ok.ShowDialog();
                                        mainMessage.Text = "���i�F�A�C���A���߂�Ȃ����ˁB�ł��A�����m���Ă锤��B";
                                        ok.ShowDialog();
                                        mainMessage.Text = "�A�C���F�E�E�E";
                                        ok.ShowDialog();
                                        // �^���̌��t�F�R�����Ă������ǂ����ŕ��򂵂Ă��������B
                                        yesno.StartPosition = FormStartPosition.CenterParent;
                                        yesno.ShowDialog();
                                        if (yesno.DialogResult == DialogResult.Yes)
                                        {
                                            mainMessage.Text = "�A�C���F�y���̂܂܃_���W�����Q�K�֍s���鎖�z";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�����R�͗ǂ��́B���肪�Ƃ��ˁB";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F���O���ǂ����Ă���Ȏ������񂾁B�ǂ���������~�߂�I�I";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�z���g�̎������āA�ˁB";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�E�E�E�y���i���~���鎖�z";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�s���|�[����@�悤�₭�o�J�����Ƃ��ĂƂ��������";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�����A���P�킩��˂��b�͒u���Ƃ��āB�����Q�K�֍s�����B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�Ō�̎����B�@�@�@�@�y�^���Ƃ́H�z";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�����������Ȃ������I�I�I�ǂ��ł��ǂ��񂾂悻��Ȏ��́I�I�I";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�t�t�A�A�C�����Ă��A�D����������ˁB�˂��A�����Ă�B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�E�E�E";
                                            ok.ShowDialog();
                                            // �^���̌��t�F�S�����Ă������ǂ����ŕ��򂵂Ă��������B
                                            yesno.StartPosition = FormStartPosition.CenterParent;
                                            yesno.ShowDialog();
                                            if (yesno.DialogResult == DialogResult.Yes)
                                            {
                                                mainMessage.Text = "�A�C���F�E�E�E�E�E�E";
                                                ok.ShowDialog();
                                                mainMessage.Text = "���i�F�����ǂ��̂�B�z���g���߂�Ȃ����ˁB���肪�Ƃ��ˁB";
                                                ok.ShowDialog();
                                                mainMessage.Text = "�A�C���F�E�E�E�E���E�E���E�E�E��";
                                                ok.ShowDialog();
                                                mainMessage.Text = "���i�F��H�z���z���A�������Ȃ����[��";
                                                ok.ShowDialog();
                                                mainMessage.Text = "�A�C���F�E�E�E�����ƍD�������A���i�B";
                                                ok.ShowDialog();
                                                mainMessage.Text = "���i�F�E�E�E����B";
                                                ok.ShowDialog();
                                                mainMessage.Text = "�A�C���F���͎��ʂ܂ł����Ƃ��O���D�����A���i�B";
                                                ok.ShowDialog();
                                                mainMessage.Text = "���i�F�E�E�E�E�E�E����B";
                                                ok.ShowDialog();
                                                mainMessage.Text = "�A�C���F�������B";
                                                ok.ShowDialog();
                                                mainMessage.Text = "���i�F�E�E�E�E�E�E�E�E�E����B";
                                                ok.ShowDialog();
                                                mainMessage.Text = "�A�C���F�@�@�@�@�y���i�͎��񂾁z�@�@�@�@";
                                                ok.ShowDialog();
                                                mainMessage.Text = "���i�F�A�C���̎��A��D������B�����ƈ����Ă�B�Y��Ȃ��łˁB";
                                                ok.ShowDialog();
                                                mainMessage.Text = "�A�C���F�{�P���I�e���F�Ȃ񂼁A�����΂��Đ��X���邳�I�I�I";
                                                ok.ShowDialog();
                                                mainMessage.Text = "���i�F�����A���[�����������̂ˁB�����������������A�����΂��Ă����B";
                                                ok.ShowDialog();
                                                mainMessage.Text = "�A�C���F�����΂�^�}����B�E���Ă����Ȃ˂����c����˂����B�b�n�b�n�b�n�I";
                                                ok.ShowDialog();
                                                mainMessage.Text = "���i�F�A�C���A�������Ԃ������\�R�܂ŗ��Ă�́B";
                                                ok.ShowDialog();
                                                mainMessage.Text = "�A�C���F���A�����A�����̓Ő��ԃ|�[�V�����A����񂾂�H";
                                                ok.ShowDialog();
                                                mainMessage.Text = "���i�F��������������ăo�J�䎌�����Ă邩��΂����Ⴄ����Ȃ��B";
                                                ok.ShowDialog();
                                                mainMessage.Text = "�A�C���F���Ⴀ�f���ɏ΂������ẮB�Ȃ��B���ŋ����Ă�񂾂惉�i�B";
                                                ok.ShowDialog();
                                                mainMessage.Text = "���i�F�i���Ɉ����Ă��B���܂�ς������܂�����B";
                                                ok.ShowDialog();
                                                mainMessage.Text = "�A�C���F�o�J�B����ȋ����Ă邨�O�Ȃ񂩂ɉ�����Ȃ񂩂˂����ẮB";
                                                ok.ShowDialog();
                                                mainMessage.Text = "���i�F�t�t�E�E�E�E�t�t�B�Ō�܂ł���ȃo�J���Ă���āB�˂��A�C���B";
                                                ok.ShowDialog();
                                                mainMessage.Text = "�A�C���F���i�E�E�E���i�A�����B";
                                                ok.ShowDialog();
                                                mainMessage.Text = "���i�F�˂��A�A�C���E�E�E�A�C���B";
                                                ok.ShowDialog();
                                                mainMessage.Text = "�A�C���F���i�E�E�E�����A�����E�E�E";
                                                ok.ShowDialog();
                                                mainMessage.Text = "�A�C���F���i�A���i�I�O�A�@�@�@�@�@�I�I�I�I�I�I";
                                                ok.ShowDialog();
                                                we.CommunicationCompArea1 = true;
                                                // �s�q�t�d�G���h�𕪊�p�ɗp�ӂ��Ă��������B
                                                // �^���̌��t�F�T
                                                // �^���̌��ЁF���i�̃C�������O
                                                // �^���̋��F�U��̖��؋�
                                                // �^���̔����J���Ă������ǂ���
                                                // �^���̐��𕷂��Ă������ǂ���
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    mainMessage.Text = "�A�C���F�E�E�E����A�������Ă�񂾃��i�B������������̈Ӗ����S�R�킩��˂����B";
                    ok.ShowDialog();
                    mainMessage.Text = "���i�F�����A�����B�t�t�t�A���߂�ˁB�ςȎ�����������āB";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F�Ƃ���ŉ�����A���́g�o�ߓ����h���Ă̂́B";
                    ok.ShowDialog();
                    mainMessage.Text = "���i�F�_���W�����P�K���e�܂Ŕ�₵�����ԂɌ��܂��Ă邶��Ȃ��B";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F�������������錾�������ȁB�����Ƒf���ɉ��𑸂ׁI";
                    ok.ShowDialog();
                    mainMessage.Text = "���i�F�z���b�g�A�o�J��ˁB���Ԃ��ĈӖ��͕������Ă�킯�H";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F�h���B";
                    ok.ShowDialog();
                    mainMessage.Text = "���i�F�h�����Ăǂ������Ӗ��H";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F���h����B";
                    ok.ShowDialog();
                    mainMessage.Text = "���i�F���h������Ăǂ������Ӗ��H";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F���ԁB";
                    ok.ShowDialog();
                    mainMessage.Text = "���i�F�n�A�@�`�`�`�@�@�`�`�E�E�E�܁A������B�Ƃ肠�������߂łƂ��B";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F�T���L���[�I�I���O���炨�߂łƂ����o��Ȃ�Ċ������ˁB";
                    ok.ShowDialog();
                    mainMessage.Text = "���i�F�������Ă�̂�B�C�x�߂�A���A��A���A�߁B";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F����A�C�x�߂ł����������B�T���L���[�B";
                    ok.ShowDialog();
                    mainMessage.Text = "���i�F�����A���q������ˁB���������A�K���c����A�n���i�f�ꂳ��ɂ��񍐂�����H";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F�����A���̂��肾���B���A�������B�����͈ꏏ�ɔтł��H�����B";
                    ok.ShowDialog();
                    mainMessage.Text = "���i�F�����ˁB���j���͂��j�������A�ꏏ�����Ⴈ�����ȁB";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F��������I��������Q�K�ڎw�����B���Ⴀ�A������ƊF�̂Ƃ��ɉ���Ă��邺�B";
                    ok.ShowDialog();
                    mainMessage.Text = "���i�F�t�t�t�A�Ƃ��Ƃƍs���Ă����B";
                    ok.ShowDialog();
                }

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�A�C���͈�ʂ�A���̏Z�l�B�ɐ��������A���Ԃ����X�Ɖ߂��Ă������B";
                    md.ShowDialog();

                    md.Message = "���̓��̖�A�n���i�̏h�����ɂ�";
                    md.ShowDialog();
                }

                mainMessage.Text = "�A�C���F���ĂƁA���悢�斾������Q�K���B�o�b�N�p�b�N�̊m�F�ł����Ă������E�E�E";
                ok.ShowDialog();
                mainMessage.Text = "�@�@�@�w�h���I�h���I�h���I�@�E�E�E�@�o�R���I�I�I�x�@�@";
                ok.ShowDialog();
                mainMessage.Text = "�@�@�@�y�A�C���H���Ȃ��́H�����Ȃ�Ԏ����Ȃ�����B�z�@�@";
                ok.ShowDialog();
                mainMessage.Text = "�@�@�@�w�b�S�X���I�I�@�h�K���I�I�I�@�{�O�b�V���A�A�@�@�I�I�I�x�@�@";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F�҂āI������āI�J���邩��҂āA�󂷂ȁI";
                ok.ShowDialog();
                mainMessage.Text = "�@�@�@�w�E�E�E�b�K�`���x";
                ok.ShowDialog();
                mainMessage.Text = "���i�F�����A�ȂȁA�����A���邶��Ȃ��B�Ԏ����炢���Ȃ�����B";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F���O�A�������Ԃ��O�ɁA�P��R��΂��ĂȂ��������H";
                ok.ShowDialog();
                mainMessage.Text = "���i�F�Ԏ����������������Ȃ��Ǝv������Ł�";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F�������A�ǂ��ɕԎ�����Ԃ����������Ă񂾂�B�����ƁA���̗p���H";
                ok.ShowDialog();
                mainMessage.Text = "���i�F�_���W�����Q�K�ɍs���ɂ������āA�S�\���������Ă������Ǝv���ĂˁB";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F�����A�P�K�ƂQ�K���Ⴛ��Ȃɂ��Ⴄ�̂��H";
                ok.ShowDialog();
                mainMessage.Text = "���i�F������A�܂����Ƀ`�[�����[�N���؂ɂ��鎖�B";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F�������Ă񂾁B�y�P�l�ł��`�[���z�Ƃ����\���قǎ₵�����m�͂˂�����B";
                ok.ShowDialog();
                mainMessage.Text = "���i�F���A���ꂪ���\�̐S�Ȃ񂾂��ǁA����̏�Ԃ�X�e�[�^�X�ɂ��C��z�鎖�B";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F�܂������X�^�[�̏�Ԃ�c����������A�퓬��i�߂��ł͗L���ł͂���ȁB";
                ok.ShowDialog();
                mainMessage.Text = "���i�F�����đ�O�A�����i����Օi�ȂǁA�A�C�e���̊Ǘ��͂���w�d�v�ɂȂ��Ă����B";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F�܂��ȁA�����傤�ǃo�b�N�p�b�N�̐��������Ă������B";
                ok.ShowDialog();
                mainMessage.Text = "���i�F���Ⴀ��l�͂ˁB�A�g���[�h����肭���p���邽�߂ɕK�{�Ȃ̂��퓬���ԂɂȂ�񂾂���";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F�҂āA�҂đ҂āB�b�������Ȃ����A��̂�������񂾂悻��H";
                ok.ShowDialog();
                mainMessage.Text = "���i�F�S��B";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F�}�W����B�ǂ񂾂��S�\����񂾂�B�������A�g���[�h�Ƃ��������˂����B";
                ok.ShowDialog();
                mainMessage.Text = "���i�F�������Ă�̂�A�A�g���Ă����Ȃ��Ə��ĂȂ����B�����ƘA�g���Ȃ�����ˁB";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F�N�Ƃ��񂾂�H";
                ok.ShowDialog();
                mainMessage.Text = "���i�F���Ɍ��܂��Ă邶��Ȃ��B���񂽁A�o�J����Ȃ��́H";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F�b�n�A�@�@�I�H���O�ƘA�g�H�H";
                ok.ShowDialog();
                mainMessage.Text = "���i�F�܂��A�C���͘A�g�Ƃ����肻�������ˁB�����ƃT�|�[�g���Ă����邩��B";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F���i�A�Ђ���Ƃ��Ă��O�_���W�����ɗ���̂��H";
                ok.ShowDialog();
                mainMessage.Text = "���i�F�A�g���Č����̂͂ˁA�퓬���Ԃɉ����āA�A���q�b�g�������ꍇ�̑����v�l���グ��E�E�E";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F�_���W�����ɗ���̂��H���ĕ����Ă񂾂�A�����Ɠ����Ă���B";
                ok.ShowDialog();
                mainMessage.Text = "���i�F�E�E�E���A������B�ʖځH";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F�E�E�E�E�E�E";
                ok.ShowDialog();
                mainMessage.Text = "���i�F�ǂ��Ȃ̂�H";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F������A�叕���肾���A�T���L���[�B�����E�E�E�ȁB";
                ok.ShowDialog();
                mainMessage.Text = "���i�F�����A���H��������ł�����킯�H";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F����A����Ȃ񂶂�˂��񂾁B�����A���ƂȂ�����������ƌ������B";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F�厖�Ȃ��Ƃ�Y��Ă�悤�Ȋ�������������ŁA������ƂȁB";
                ok.ShowDialog();
                mainMessage.Text = "���i�F���i�l�̕S�̐S�\����Y��Ă邾���ł���B��������S���o���Ă�ˁB";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F����A����͍����n�߂ĕ������B���O���ˑR�@�֏e�̔@���E�E�E";
                ok.ShowDialog();
                mainMessage.Text = "���i�F�n�C�n�C�A���Ⴀ������@��������̓r�V�r�V�b���邩��o��I���Ⴀ�ト���V�N�ˁ�";
                ok.ShowDialog();
                mainMessage.Text = "�@�@�@�w�b�o�^���E�E�E�x";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F�����ǂ��g�ト���V�N�h�Ȃ񂾂�B�����ꒃ�������ȁA���i�̂�B";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F�������A���������񂾂낤�ȁB�������̂�����₵�����G�́E�E�E";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F�܂��A�C�ɂ��Ă����傤���˂����I";
                ok.ShowDialog();
                mainMessage.Text = "�A�C���F������A���i�̕����o�b�N�p�b�N�������Ă����Ƃ��邩�B";
                ok.ShowDialog();
                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "���i���p�[�e�B�ɉ����܂����B";
                    md.ShowDialog();
                }

                CallRestInn();
                we.AvailableSecondCharacter = true;
                we.CommunicationCompArea1 = true;
            }
            #endregion
            #region "�Q�K���P�C�x���g"
            else if (this.we.InfoArea21 && !this.we.SolveArea21)
            {
                UpdateMainMessage("�A�C���F�Ȃ��A���i�B���̂Q�K�A���łɍs���~�܂肾���B");

                UpdateMainMessage("���i�F����������ˁB����ȑ����ŉ��w�Ƃ͎v���Ȃ���B");

                UpdateMainMessage("�A�C���F���������Ă��ȁB�ǂ����悤���˂����B");

                UpdateMainMessage("���i�F������Ƒ҂��āA�������Ă��邩��A������x�ǂ�ł݂��B");

                UpdateMainMessage("�@�@�@�@�w���̐�A�s���~�܂�B�@��U�A�������邪�悢�B�x");

                UpdateMainMessage("�A�C���F�����I�H�E�E�E���������i�I�b�n�b�n�b�n�I�I");

                UpdateMainMessage("���i�F���񂽁A�o�J����Ȃ��́H���ꂮ�炢���ʂ��ł���B");

                UpdateMainMessage("�A�C���F���������ɉ��̕ϓN���Ȃ����͂��ȁB");

                UpdateMainMessage("���i�F�҂��āB�R�R���Ă�B�@�y��U�z���ď����Ă����B");

                UpdateMainMessage("�A�C���F���H�E�E�E�m���ɏ����Ă���ȁB���ꂪ�ǂ������B");

                UpdateMainMessage("���i�F��U�E�E�E��U�A��������E�E�E������A��U��������Ηǂ��̂�B");

                UpdateMainMessage("�A�C���F�Ȃ��A�ŉ��w�ɂ��ǂ蒅�������Ƃ����A�[�тł��H�ׂ˂����H");

                UpdateMainMessage("�@�@�@�w�b�V���S�I�H�H�I�H�H�I�I�I�x�i���i�̃��C�g�j���O�L�b�N���A�C�����y��j�@�@");

                UpdateMainMessage("�A�C���F�����I�I�I���A�������������E�E�E���O���̃c�b�R�~�A���A���߂��邼�B");

                UpdateMainMessage("���i�F���B�A��U�����O�̒��ɖ߂��Ă����̂�ˁB");

                UpdateMainMessage("�A�C���F�����A�m���ɍ��߂��Ă��Ă�ȁB");

                UpdateMainMessage("���i�F�_���W��������y��U�A�����������z���Ď��ɂȂ��B");

                UpdateMainMessage("�A�C���F�Ȃ�قǁA�������������B");

                UpdateMainMessage("���i�F���܂�ˁB�����܂��s���Ă݂܂��傤�B�����Ɖ����ς���Ă���");

                UpdateMainMessage("�A�C���F�Ȃ�قǁA�������������B");

                UpdateMainMessage("���i�F�A�C���E�E�E���񂽂Ђ���Ƃ��āE�E�E");

                UpdateMainMessage("�A�C���F�Ȃ�قǁA�������������B");

                UpdateMainMessage("���i�F�n�A�A�@�@�@�`�`�`�`�@�@�E�E�E�܂��ǂ���B�Ƃɂ����A�����ˁB");
                this.we.SolveArea21 = true;
            }
            #endregion
            #region "�Q�K���U�C�x���g"
            else if (this.we.InfoArea26 && !this.we.SolveArea26)
            {
                UpdateMainMessage("�A�C���F����[��ꂽ��ꂽ�I�I�����A�ǂ����тł��s�����I�H");

                UpdateMainMessage("���i�F�E�E�E");

                UpdateMainMessage("�A�C���F���������A���Â��炵�Ă񂾂�H");

                UpdateMainMessage("���i�F�����āE�E�E����Ȃ̉��������Ȃ��ł���I�H");

                UpdateMainMessage("�A�C���F�����A�����Ȃ肻��ȃc���c������Ȃ�B");

                UpdateMainMessage("���i�F�A�C���͉��������Č����́I�H�����Ă�I");

                UpdateMainMessage("�A�C���F����A�����܂����������ǂ��B");

                UpdateMainMessage("���i�F�������牽�ł���ȃe�L�g�[�Ȃ̂�B�x��ł����ĉ����₵�Ȃ��̂�H");

                UpdateMainMessage("�A�C���F����A�����܂����������m��Ȃ����B�ł��ȁE�E�E");

                UpdateMainMessage("���i�F���񂽂�����ȃe�L�g�[������A�Ō�ɂȂ��ĂQ�K�������Ȃ��񂶂�Ȃ��B");

                UpdateMainMessage("�A�C���F����A��肢�ȃz���g�B�����Ƃ܂��v���t���悤�ɂ��邩�炳�E�E�E");

                UpdateMainMessage("���i�F�z���b�g�I�ǂ�����̂�I�I�@�m�[�q���g�ŒN����������Č����̂�I�I�I");

                UpdateMainMessage("�A�C���F�����A�������i�B�����������āA�܂������Ȃǂ������E�E�E");
                
                UpdateMainMessage("�@�@�@�y����A�N���Ǝv���΁A�A�C���ƃ��i����񂶂�Ȃ����B����ȏ��ŁA�ǂ������񂾂��H�z");

                UpdateMainMessage("�A�C���F�����A���[�n���i�f�ꂳ�񂶂�Ȃ��ł����I�f�ꂳ��A�ǂ�������ł����H");

                UpdateMainMessage("�n���i�F�K���c�̒U�߂ɔтƎ��������Ă���������B�������̓_���W���������ɐi��ł邩���H");

                UpdateMainMessage("�A�C���F���₠�E�E�E���ꂪ�����Ƀs���`�ł��E�E�E");

                UpdateMainMessage("�n���i�F���i�������ꏏ�݂������ˁB���i�����A�ǂ������A�A�^�V�ƈꏏ�ɗ[�тł��H");

                UpdateMainMessage("���i�F�E�E�E���H���E�E�E");

                UpdateMainMessage("�n���i�F�������A�����d�ǂ��ˁB������Ƃ����҂��ĂȁB�U�߂ɓn���Ă����炷���߂��B");

                UpdateMainMessage("�@�@�@�y�n���i�́A�K���c�̕���ւƋ����Ă������E�E�E�z");

                UpdateMainMessage("�A�C���F�����A���i�B�n���i�f�ꂳ��Ƃ��ŔѐH��˂����H");

                UpdateMainMessage("���i�F�E�E�E�����ˁB");

                UpdateMainMessage("�A�C���F��������I���܂茈�܂���ƁI�I");

                UpdateMainMessage("�@�@�@�y�n���i���A�A�C���ƃ��i�̏��֖߂��Ă����E�E�E�z");

                UpdateMainMessage("�n���i�F�n�C�n�C�A���Ⴀ�E�`�֗��đ�R�H�ׂĂ����ȁB�����̓A�^�V���ꏏ�ɐH�ׂ��B");

                UpdateMainMessage("�A�C���F�{���ɂ��肪�Ƃ��������܂��I�悵�A���Ⴀ�s�������A���i�I");

                UpdateMainMessage("���i�F���A�����E�E�E");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�A�C���ƃ��i�̓n���i�f�ꂳ��Ƌ��Ƀn���i�̏h���֌��������B";
                    md.ShowDialog();
                }

                UpdateMainMessage("�A�C���F���O�E�E�E��肢�A��肢�ȁE�E�E");

                UpdateMainMessage("���i�F�A�C���A������Ƃ��������i�̂���H�ו����Ă�ˁB");

                UpdateMainMessage("�A�C���F���O�E�E�E�O�E�E�E��H�������H");

                UpdateMainMessage("�n���i�F�A�b�n�b�n�A�ǂ���ǂ���B�E�`����H�ו��̃��[���Ȃ�Ė�������ˁB");

                UpdateMainMessage("�n���i�F�Ƃ���ŁA�������_���W�����Ńs���`�Ƃ������Ă��ˁB�s���l�����̂����H");

                UpdateMainMessage("�A�C���F���A�����A���ꂪ�ȁB");

                UpdateMainMessage("���i�F�A�C���A�������������B");

                UpdateMainMessage("�n���i�F�܂��܂����i�����A�A�C���ɐ��������Ă������ǂ������H");

                UpdateMainMessage("���i�F���H�����A�f�ꂳ�񂪂��������̂Ȃ�A�A�C���ɔC���悤������B");

                UpdateMainMessage("�A�C���F���̃_���W�����A�Q�K�����ɋ�敪������ĂĂȁB");

                UpdateMainMessage("�A�C���F����łP��斈�̓�����ɊŔ������Ă�񂾂�B�����ǂނƁE�E�E");

                UpdateMainMessage("�A�C���F�w���̐�A�s���~�܂�B�@��U�A�������邪�悢�B�x");

                UpdateMainMessage("�A�C���F�w���̐�A�s���~�܂�B�@�e�X�̋����������B�x");

                UpdateMainMessage("�A�C���F�w���̐�A�s���~�܂�B�@�����g�ɖ₢������B�x");

                UpdateMainMessage("�A�C���F�w���̐�A�s���~�܂�B�@�����������A���������؂������B�x");

                UpdateMainMessage("�A�C���F�w���̐�A�s���~�܂�B�@�Ō�̋��֐i�ނׂ��B�x");

                UpdateMainMessage("�A�C���F�����܂ł͉��Ƃ������������킯���B����炵���q���g�����ɂ������Ă邩��ȁB");

                UpdateMainMessage("�A�C���F�Ƃ��낪�A�Ō�̋�悪���������Ŕ������񂾁B");

                UpdateMainMessage("�A�C���F�w���̐�A�s���~�܂�B�x");

                UpdateMainMessage("�A�C���F�����ɉ��������Ă˂��B����Ŏ����T�������񂾂��E�E�E");

                UpdateMainMessage("�A�C���F����q���g��������Ԃ����Ă킯���B�E�E�E�����ō~�Q���ă��P���B�Q�������z���g�B");

                UpdateMainMessage("�n���i�F�Ō�̓q���g�������Ă����H�����m���ɍ����������ˁA�A�b�n�n�n�n�B");

                UpdateMainMessage("���i�F�f�ꂳ��͂ǂ��H����������܂����H");

                UpdateMainMessage("�n���i�F������A�q���g���������͕̂�����Ȃ��ˁB");

                UpdateMainMessage("�A�C���F�ق�I�f�ꂳ�񂾂��ĕ�����Ȃ��񂾂��B���ɕ�����Ȃ��ē�����O����I�H�b�n�b�n�b�n�I");

                UpdateMainMessage("���i�F�ςȃg�R�Ŏ������Ȃ��ł����I�H���񂽂��ăz���b�g�o�J��ˁI�H");

                UpdateMainMessage("�A�C���F���A���₢��A�����������Č����Ă邾��E�E�E");

                UpdateMainMessage("�n���i�F�A�C���͂�������Ă��܂Ƀ��i�����ɋC�����Ă�񂾂�B�����Ă��ȁB");

                UpdateMainMessage("���i�F�f�ꂳ��I�H�A�C�c�͐��^�����ł���I�H");

                UpdateMainMessage("�n���i�F�A�C���A�U�̊Ŕ�S�ċL�����Ă����񂾂ˁH�����������m���B");

                UpdateMainMessage("���i�F���ȁI�H���A���������΁A�������X���X�������Ă���ˁB");

                UpdateMainMessage("�A�C���F�����A���̓��i�̃��������܂ɃR�b�\���E�E�E");

                UpdateMainMessage("�@�@�@�w�b�h�O�V�I�x�i���i�̃T�C�����g�u���[���A�C���̉������y��j�@�@");

                UpdateMainMessage("�A�C���F�i�b�O�A�����I�H�E�E�E���܁A�ѐH���Ă鎞�ɂ��Ȃ�E�E�E�j");

                UpdateMainMessage("�n���i�F�A�b�n�n�n�A�A�C���A���̎q�̎������`����������ʖڂ���B");

                UpdateMainMessage("�n���i�F�ǂ�����񂾂��H�������̕ӂŎ~�߂Ă��������H");

                UpdateMainMessage("���i�F�E�E�E�E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E�E�E�E");

                UpdateMainMessage("�n���i�F����[���Ȃ��q�B���ˁB����Ȃ񂶂�ŉ��w�s���Ȃ���H");

                UpdateMainMessage("�n���i�F���Ⴀ�A�f�ꂳ�񂩂�Ƃ��Ă����̃q���g������������悤�B");

                UpdateMainMessage("�A�C���ƃ��i�F�{���ł����I�I�H");

                UpdateMainMessage("�n���i�F�����A�悭�����񂾂ˁB");

                UpdateMainMessage("�n���i�F�y�q���g�͖����z�@���ꂪ���̂��q���g����B");

                UpdateMainMessage("���i�F������ƁA�ǂ������Ӗ��ł����\���H");

                UpdateMainMessage("�n���i�F���i�����A�������Ă͉̂����邩�����Ȃ����Ȃ�ĒN�ɂ�������Ȃ��̂��B");

                UpdateMainMessage("�n���i�F�q���g�ǂ��납�A�������Ă͖̂₢�������̂����Ă���Ȃ��B�ʂ�߂��Ă��Ԏ������Ȃ��B");

                UpdateMainMessage("�n���i�F�ł��ˁA���̃_���W�����ɂ͂���[��ƁA�Ŕ������Ă���ł��傤�H");

                UpdateMainMessage("���i�F�����A�m���ɊŔ��̂͂�������B");

                UpdateMainMessage("�n���i�F�܂��A�^�V��s���ĂȂ����番����Ȃ����ǁA�������肶�ᓯ���悤�ȊŔ��������񂾂ˁH");

                UpdateMainMessage("���i�F�����A�P��斈�̓�����ɓ\��t���Ă�������B");

                UpdateMainMessage("�n���i�F��������A���������͖ڂ̑O����B����́y�݂�z���Ď�����Ȃ�������ˁB");

                UpdateMainMessage("�A�C���F�E�E�E�I�[�P�[�I�[�P�[�B���ƂȂ��ǂ߂����I");

                UpdateMainMessage("���i�F���A�����ǂ߂����Č����́H");

                UpdateMainMessage("�A�C���F���H����A���₢��E�E�E�����͂킩��˂����ǂ��B���ƂȂ��E�E�E");

                UpdateMainMessage("�n���i�F�A�C���A�떂�����Ȃ��́B�����ƌ����ȁB");

                UpdateMainMessage("�A�C���F���A�����B����������f�ꂳ��B���i�A���t�ʂ肾�y�q���g�͖����z���Ă��Ƃ�");

                UpdateMainMessage("���i�F����B");

                UpdateMainMessage("�A�C���F�q���g�������Ă��A�������̂͑��݂���ƌ����Ă�悤�Ȃ��̂��B");

                UpdateMainMessage("���i�F�E�E�E����B");

                UpdateMainMessage("�A�C���F�܂肱�����������B�q���g�������ȏ�A�����B�̗͂ŒT�����Ă邵���Ȃ����ĂȁB");

                UpdateMainMessage("���i�F�E�E�E����B");

                UpdateMainMessage("�A�C���F�T���΍݂�B�@�u�Ŕ��݂�v ���@�u�������݂�v���Ď����I���h�ȃq���g����˂����R�����I");

                UpdateMainMessage("���i�F�E�E�E����B�������ˁB");

                UpdateMainMessage("�A�C���F������I�����ƌ��܂�΁A�����̃o�b�N�p�b�N�����ł����Ƃ����I�f�ꂿ���A������������I");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�A�C���͎����̕����ւƑ�}���ő����Ă������B";
                    md.ShowDialog();
                }

                UpdateMainMessage("���i�F�E�E�E�A�C���A�{���ɕ��������̂�����B");

                UpdateMainMessage("�n���i�F���������āA�؂��ǂ��̂悠�̎q�́B");
                
                UpdateMainMessage("�n���i�F����ƁA���܂Ƀ��i�����ɋC���g���Ă�̂�A�]�v�Ȃ����b����˂��H");

                UpdateMainMessage("���i�F���ӂ�A�A�C�c�͂���ς�o�J��B���P������Ȃ��g�R�ŕ��������悤�Ȋ炵�āE�E�E");

                UpdateMainMessage("�n���i�F�A�b�n�n�n�A�������������Ȃ��悤�Ȃ�e�͂Ȃ������ς����Ă��ȁB");

                UpdateMainMessage("���i�F�������Ȃ��Ɍ��܂��Ă��B���̎���100�r���^�ˁ�");

                UpdateMainMessage("�n���i�F�����A���̈ӋC����B�撣��ȁI���񂽒B�Ȃ�s���邩���m��Ȃ���B");

                UpdateMainMessage("���i�F�f�ꂳ��A���肪�Ƃ��������܂����B�_���W�����i�߂��铹���܂��T���Ă݂܂��B");

                UpdateMainMessage("�n���i�F�����A��R�H�ׂāA��R�Q��B����Ŏv�������芈�����Ă��ȁB");

                UpdateMainMessage("���i�F�����͔����������т��肪�Ƃ��������܂����B�܂��撣���Ă��܂��B");

                UpdateMainMessage("�n���i�F������B");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "���i�͎����̕����֖߂��Ă������B";
                }

                UpdateMainMessage("�n���i�F���ĂƁA�Еt���Ă��܂����ˁB");

                UpdateMainMessage("�@�@�@�y�����A�n���i�B����܂�Â₩���ȁB�z");

                UpdateMainMessage("�n���i�F�A�����A�A���^���̊Ԃɂ����ɋ����́H");

                UpdateMainMessage("�K���c�F���O�̔сA�͂��鎞�Ԃ��������Q�T�����������B�����������̂��Ǝv���ĂȁB");

                UpdateMainMessage("�n���i�F�Ȃ��ɁA������Ƃ����C�܂��ꂾ��A�C�ɂ��Ȃ���ȁB");

                UpdateMainMessage("�K���c�F���i�ƃA�C���B���̂܂ܕ����Ă����Ύ~�߂Ă����낤�B���̎�����ꂽ�H");

                UpdateMainMessage("�n���i�F�����ꂿ��ʖڂȂ̂����H���񂽂��ł��l���˂��B");

                UpdateMainMessage("�K���c�F�Q�K�̍Ō�ƌ����΁A�e�������ꂼ��Ⴆ�ǁA���h�Ȏ����B�q���g�Ȃǒ񎦂���ׂ��ł͂Ȃ��B");

                UpdateMainMessage("�n���i�F�ł��A�^�V�ኴ������B���̓�l�A���������B�A�^�V�ɂ����v�킹���́B");

                UpdateMainMessage("�K���c�F�Ȃ�قǂȁB�Ȃ�Ύd������܂��B�����A���܂�ߓx�Ȍ�����͂���ȁB");

                UpdateMainMessage("�n���i�F�����A��������肾��B");

                we.SolveArea26 = true;

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�����Ĉ�����߂����B";
                }
                CallRestInn();
            }
            #endregion
            #region "�Q�K���e"
            else if (this.we.CompleteArea2 && !this.we.CommunicationCompArea2)
            {
                UpdateMainMessage("�A�C���F�Q�K�E�E�E�˔j�����I�I");

                UpdateMainMessage("���i�F���낢�날�����ˁA�z���g�Q�K�͋�J������B");
                
                UpdateMainMessage("�A�C���F���Ƃ��ẮA�ŏ��̊Ŕ̎��_�Ń}�W�Ły�ŉ��w�z���Ǝv�������H");
                
                UpdateMainMessage("���i�F��ȃ��P�Ȃ��ł���B���ꂾ������P�O�O���l������X�O���l�͉����Ă���B");
                
                UpdateMainMessage("�A�C���F�Ȃ��A������E�E�E�R�K�����B");
                
                UpdateMainMessage("���i�F����H�}�ɂ������܂�������āB");
                
                UpdateMainMessage("�A�C���F���A��΂ɐ��e���Ă݂��邩��ȁB");
                
                UpdateMainMessage("���i�F�b�t�t�A���҂��Ă���B�A�C���Ȃ����Ǝv����B");
                
                UpdateMainMessage("�A�C���F����A���̃_���W�����R�K�B���̂܂܂��ᐧ�e�ł��˂��C������B");
                
                UpdateMainMessage("���i�F�ǂ���������H");
                
                UpdateMainMessage("�A�C���F�܂��b�͌ゾ�B�K���c�����n���i�f�ꂳ��ɘA�����Ă���B���ꂩ�炾�ȁI");
                
                UpdateMainMessage("���i�F�t�t�A�����ˁB�܂��͊F�ɘA�����Ă��܂����");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�A�C���͈�ʂ�A���̏Z�l�B�ɐ��������A���Ԃ����X�Ɖ߂��Ă������B";
                    md.ShowDialog();

                    md.Message = "���̓��̖�A�n���i�̏h�����ɂ�";
                    md.ShowDialog();
                }

                UpdateMainMessage("�A�C���F�J���p�[�C�I�J���p�[�C�I�J���p�[�C�I�b�n�b�n�b�n�b�n�I�I");
                
                UpdateMainMessage("�n���i�F�A�b�n�n�n�n�A�J���p�C���ˁI");
                
                UpdateMainMessage("���i�F�����A����J���p�C���Ă�̂�B����łT��ڂ���Ȃ��S���E�E�E");
                
                UpdateMainMessage("�A�C���F���₠�A���ɊŔR�ڂ����B���ꂪ���P�킩��˂����₪�����Ă�I�I");
                
                UpdateMainMessage("�n���i�F�ǂ�Ȃ̂������񂾂��H");
                
                UpdateMainMessage("�A�C���F���ӂȕ���͉����Ƃ��A�t���͒N���Ƃ��E�E�E���ɍŌ�̂P�O��ڂȂ񂩃A�������I�H");
                
                UpdateMainMessage("���i�F���A�A�C���I��������ƁI�I");
                
                UpdateMainMessage("�A�C���F�b���A���O�@�I�I�I�}�A�т������ɁE�E�E���K�K�K�K�I�I");
                
                UpdateMainMessage("�n���i�F�A�b�n�n�n�n�A�ǂ���ǂ���B�������ĕ����Ȃ�����B");

                UpdateMainMessage("�n���i�F�A�C���A�Q�K�̍Ō�͂ǂ�����ĉ������񂾂��H�����Ă����B");

                UpdateMainMessage("�A�C���F�b�Q�z�A�b�Q�z�E�E�E���A�����A�Ō�̑������ȁB");

                UpdateMainMessage("�A�C���F�́E�E�E����A����́y���z���Č����̂��H���������񂾂�y���z���B");

                UpdateMainMessage("���i�F���͑S�R�������Ȃ������̂ɂˁB�������A�C���A����̋���Ċ������������B");

                UpdateMainMessage("�A�C���F�y���z���������Ă��āA���ꂩ��c��ȁy�����̗���z�A��͂������ȁA�����ȁy�F�ʁz���������B");

                UpdateMainMessage("�A�C���F�ŁA����̑O�ɗ��������A�s�v�c�Ɓy���z�y�����̗���z�y�F�ʁz�������ɓ��Ă͂܂����B");

                UpdateMainMessage("�A�C���F���͂��̌��t������̂܂܌��ɏo���Ă����B����Ȋ����������ȁB");

                UpdateMainMessage("�n���i�F�E�E�E�E�E�E�Ȃ�قǂ˂��B�������������B");

                UpdateMainMessage("���i�F�R�C�c�A���P�킩��Ȃ��䎌�A�����������B");

                UpdateMainMessage("���i�F�˂��A�n���i�f�ꂳ��͉���������H");

                UpdateMainMessage("�n���i�F������A��������s��������������ˁB�ł����ꂾ���͌������B");

                UpdateMainMessage("�@�@�@�y�n���i�I�����I�@���I�z");
                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "����̃K���c���t���������ł�����֊���Ă����B";
                    md.ShowDialog();
                }
                UpdateMainMessage("�n���i�F����܁I�A���^�����͕�������̂����H");

                UpdateMainMessage("�K���c�F�����͓X���܂����B�A�C���A���i�A���߂łƂ��B");

                UpdateMainMessage("�A�C���E���i�F���肪�Ƃ��������܂��I");

                UpdateMainMessage("�K���c�F�A�C���A�悭������B���X������g���Ă�����Ă郏�V���ւ�Ɏv����B");

                UpdateMainMessage("�K���c�F���i�A�悭������B���O�����ŃA�C���͂����܂ŒH����Ȃ����낤�B");

                UpdateMainMessage("�K���c�F�ł́A�J���p�C�I");

                UpdateMainMessage("�A�C���F�J���p�[�C�I�J���p�[�C�I�J���p�[�C�I");

                UpdateMainMessage("���i�F�������E�E�E����łU��ڂˁB�J���p�C��");

                UpdateMainMessage("�K���c�F�Ƃ���ł��A�A�C���B�@���O�͏C�s�������B");

                UpdateMainMessage("�n���i�F�₾��A���̐l�B����ȓ��ɐ��������H");

                UpdateMainMessage("�K���c�F�n���i�A���O�͏����ق��Ă��Ȃ����B");

                UpdateMainMessage("�n���i�F�͂���B");

                UpdateMainMessage("�K���c�F�ǂ����A�A�C���B");

                UpdateMainMessage("�A�C���F�͂��A���ł����H");

                UpdateMainMessage("�K���c�F�A�C���B���̂܂܂ł͂R�K�͉����Ȃ��Ǝv���Ȃ����B");

                UpdateMainMessage("���i�F�����E�E�E���������΁A�������B");

                UpdateMainMessage("�K���c�F���ށH");

                UpdateMainMessage("�A�C���F�����A���̉�������������Ȃ��B���ƂȂ�����ȋC�����Ă�񂾁B");

                UpdateMainMessage("�K���c�F�ӂށA�����Ɛ������Ă�悤���ȁB���ꂪ������Ȃ�b�͑����B");

                UpdateMainMessage("���i�F���ň����s���������Ȃ��̂ɁA����Ȃ��Ƃ�������̂�H�A�C���B");

                UpdateMainMessage("�A�C���F�f�ꂳ��A�Q�K�̐��e���Ă�l�͂ǂ̂��炢����񂾁H");

                UpdateMainMessage("�n���i�F�������ˁB�����Ɛ����āA�P�T�O�l���炢����B");

                UpdateMainMessage("���i�F�E�\�E�E�E�قƂ�ǎc���ĂȂ�����Ȃ��B");

                UpdateMainMessage("�K���c�F�Q�K�̎��_�ŁA�قƂ�ǂ̃����o�[�͐U�藎�Ƃ����B");

                UpdateMainMessage("�K���c�F�R�K�ł͌���ꂽ�����o�[���������c�鎖�͋�����Ă��Ȃ��B");

                UpdateMainMessage("�K���c�F�A�C���A���̂R�K�ł́A����w�̒b�B�����Ȃ����B");

                UpdateMainMessage("�A�C���F�͂��B");

                UpdateMainMessage("�K���c�F���ꂩ��A���i�B");

                UpdateMainMessage("���i�F���A�͂��B");

                UpdateMainMessage("�K���c�F�R�K�̓��i�N�̔Ԃł��邪�A�����͂ł��Ă��邩�ˁH");

                UpdateMainMessage("���i�F�E�E�E���H");

                UpdateMainMessage("�K���c�F�ӂށE�E�E�ӂށB�ǂ��������̂��B");
                
                UpdateMainMessage("�K���c�F���i�N�̑����́y�Łz�y���z�y��z���ˁE�E�E�ӂށB");

                UpdateMainMessage("���i�F�����A���A���A���ŕ������ł����H�H�H");

                UpdateMainMessage("�K���c�F���i�N�A�����p�������g����Ă��ˁH");

                UpdateMainMessage("���i�F�����A�͂��B���C�g�j���O�L�b�N�Ȃǂ���X���K���ł��B");

                UpdateMainMessage("�A�C���F����E�E�E���K�Ȃ̂��H");

                UpdateMainMessage("���i�F���K��B�����Ǝ�������đł�����ł邶��Ȃ���");

                UpdateMainMessage("�A�C���F�E�E�E������E�E�E����ׂ��B");

                UpdateMainMessage("�K���c�F���i�N�B���V�����xStanceOfFlow�̋Ɉӂ������Ă����悤�B");

                UpdateMainMessage("�A�C���ƃ��i�F�����I�H�@�i�}�W����I�H�j�@�i�{���ł����I�H�j");

                UpdateMainMessage("�K���c�F���K���̋ɈӁB��͓�����A��������Δ���I�Ȑ����𐋂�����B");

                UpdateMainMessage("�A�C���F�E�E�E�΁A�o�J�ȁB�K���c�f������A������Č��ł���H�H");

                UpdateMainMessage("�K���c�F�A�C���A���̓��i�N�ɘb�������Ă���B");

                UpdateMainMessage("�A�C���F���A�͂��B");

                UpdateMainMessage("���i�F�z���A�z���z���z���I����ς肠�̑M���͊ԈႢ�Ȃ��̂��");

                UpdateMainMessage("���i�F�K���c�f������A��낵�����肢���܂��B��������݂܂�������");

                UpdateMainMessage("�A�C���F�����E�E�E�}�W����B");

                UpdateMainMessage("�K���c�F���i�N�A�R�K�ł͌N���悾�B�S���Ď��|����Ȃ����B");

                UpdateMainMessage("���i�F���A�n�C�I���肪�Ƃ��������܂��I�撣��܂��I");

                UpdateMainMessage("�n���i�F���������A���������Ă�����B�т��ǉ����Ƃ�������h���h���H�ׂȁI");

                UpdateMainMessage("�A�C���ƃ��i�F���肪�Ƃ��������܂��I�I���������܁[���I");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�A�C���ƃ��i�͂��ꂩ��c�R�̂ЂƂƂ����߂����A�����ւƖ߂��Ă������B";
                    md.ShowDialog();
                }

                UpdateMainMessage("�n���i�F���񂽂������Ƃ��l�D������Ȃ����B");

                UpdateMainMessage("�K���c�F���̂܂܂ł́A�R�K���e�m���͂O���̂܂܂��낤�B");

                UpdateMainMessage("�n���i�F���₨��A�m���v�Z�܂ł��Ă��̂����H���񂽂̌v�Z�A���m������˂��B");

                UpdateMainMessage("�K���c�F���Ƀ��i�N�̕��́A�����肪�o�Ă��Ă���B���܂萬�����ĂȂ��悤���B");

                UpdateMainMessage("�n���i�F���i�����A�������ł����B�b���Ăǂ��������Ȃ񂾂��H");

                UpdateMainMessage("�K���c�F�O���ł͂Ȃ��P�����B���V���o����̂͂����܂ł��B");

                UpdateMainMessage("�n���i�F�P�������H�A�b�n�n�n�n�B�R�K�Ȃ̂ɒ������傫�������ɂ��Ă邶��Ȃ����B");

                UpdateMainMessage("�K���c�F���V�������������Ȃ����B���O����Ȃ����A�m���Ɏ��݂������Ȃ����B");

                UpdateMainMessage("�n���i�F����ŁA�����������ˁB�����A�A���^���Еt����`���Ȃ����I");

                UpdateMainMessage("�K���c�F���ӂ�A���������ł͂Ȃ��B���V�͕��p��b���Ă�邾�����B");

                UpdateMainMessage("�n���i�F�A�b�n�n�n�A�m���ɂ������ˁB�A�^�V�͂��������̂ɓP�����B");

                UpdateMainMessage("�K���c�F���V��������肾�B�b�����������͌����Ƃ��悤�B");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�E�E�E�E�E�E���̍��A�A�C���̕����ɂāE�E�E�E�E�E";
                    md.ShowDialog();
                }

                UpdateMainMessage("�A�C���F���i�̂�ɃK���c�f�����t���Ƃ��ĕt���̂��B������ׂ��ȁE�E�E");

                UpdateMainMessage("�A�C���F�E�E�E�����R�K�Ɍ����ď����P�����Ă������E�E�E");

                UpdateMainMessage("�A�C���F�悵�I�O�ŗ��K���I");

                GroundOne.StopDungeonMusic();

                UpdateMainMessage("�@�@�@�y�h�A���J�����u�ԁA�ڂɌ����Ȃ������ŉ������ʂ�߂����B�z�@�@");

                UpdateMainMessage("�A�C���F��H�����˕��݂����Ȃ��̂��E�E�E����A���������Ă邼�B");

                UpdateMainMessage("�A�C���F�莆�E�E�E���H���i�A�Ђ���Ƃ��ĉ���");

                UpdateMainMessage("�@�@�@�y�_���W�����Q�[�g������̗��ő҂B�@�` �u�E�` �`�@�z�@�@");

                UpdateMainMessage("�A�C���F���������B�u�D�`�E�E�E�N���H");

                UpdateMainMessage("�A�C���F�܂����x���K���鏊���������ȁB�s���Ă݂�Ƃ��邩�I");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "���̖�A�A�C���̓_���W�����Q�[�g������̗��ɂ���Ă����B";
                    md.ShowDialog();
                }

                GroundOne.PlayDungeonMusic(Database.BGM10, Database.BGM10LoopBegin);
                this.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "hometown2.jpg");

                UpdateMainMessage("�A�C���F�ւ��A�_���W�����Q�[�g�̗������Ă���ȏꏊ�Ȃ񂾁E�E�E�����L�ꂾ�ȁB");

                UpdateMainMessage("�@�@�@�������@��̕����A�C���̑S�̂֐G�ꂽ�B����ȋC�������B�@�������@�@");

                UpdateMainMessage("�A�C���F�E�E�E��H");

                UpdateMainMessage("�@�@�@�w�A�C���N�B�@�@�n�߂܂��āB�@�@���ˁB�x�@�@");

                UpdateMainMessage("�A�C���F���ȁI�H�����A�S�R�����Ȃ��������I�H�������񂾁I�I�H");

                UpdateMainMessage("�A�C���F���O���E�E�E�@�`�u�D�`�`�@���I�H");

                UpdateMainMessage("�@�@�@�w��������B�{����Verze Artie�B��낵���ˁB�x�@�@");

                UpdateMainMessage("�A�C���F�E�E�E�����I�H���F�A���F���[�E�A�[�e�B�����āI�H");

                UpdateMainMessage("���F���[�F�A�C���N�A�\�ʂ�̐��������Ă�ˁB�e�Ȃ���{�N�����Ă�����B");

                UpdateMainMessage("�A�C���F����A���Ȃ񂩑S�R�܂��܂��ł��B�����Ȃ�Ă������t�͒������ł���B");

                UpdateMainMessage("���F���[�F�������Ȃ��ėǂ��B����Ε������B�P�K�˓����Ƃ͕ʐl�ɂȂ��Ă�ˁB");

                UpdateMainMessage("�A�C���F���A����Ȏ������̂�����ł����A���F���[�l�B");

                UpdateMainMessage("�A�C���F���̉��Ȃ񂩂��Ăяo������ł����H");

                UpdateMainMessage("���F���[�F���F���[�ŗǂ���B�{�N�͗l���ĕt������̂�����������B");

                UpdateMainMessage("���F���[�F���ƁA�����ƋC�y�ɘb���ėǂ��ł���B�{�N���C�y�ɒ��肽���ł�����B");

                UpdateMainMessage("���F���[�F���āA�{�N����܂�����Ă̗��݂������ł��B�����Ă��炦�܂����H");

                UpdateMainMessage("�A�C���F���F���[�l�E�E�E���F���[�̌������Ȃ牽�ł��A���ł��傤���H");

                UpdateMainMessage("���F���[�F�A�C���N�ɗ͂�݂��Ă��������B�{�N���p�[�e�B�ɓ���Ă��炦�܂��񂩁H");

                UpdateMainMessage("�A�C���F�{���ł����I�H���̓`����FiveSeeker���I�H");

                UpdateMainMessage("���F���[�F�`����FiveSeeker�Ȃ�Č�����Ƒ傰�����ȁB��������������Ȃ���B");

                UpdateMainMessage("�A�C���F�������ƁE�E�E����Ⴀ����");

                UpdateMainMessage("�@�@�@�w����H�Ђ���Ƃ��ăA�C������Ȃ��H�I�[�C�A�����̃o�J�[�I�I�x�@");

                UpdateMainMessage("�A�C���F��H�����A���i�[�I��̂ǂ������񂾁H����ȏ��ɗ��āH");

                UpdateMainMessage("���i�F��������K���c�f������ɒb���Ă��炤���߂ɁA������ƂˁB");

                UpdateMainMessage("���i�F���āA�A���H�A�C���E�E�E��l�H�H�H");

                UpdateMainMessage("�@�@�@�������@���i�ɂ͓�l�̃A�C��������悤�Ɍ������B����ȋC�������B�@�������@�@");

                UpdateMainMessage("���i�F���ƁA����Ȃ킯�������B�邾�����ԈႦ����������@������̕��͂ǂȂ��H");

                UpdateMainMessage("�A�C���F���i�I�����ċ����I�@���͂��̐l�́I�I");

                UpdateMainMessage("���F���[�F���F���[�E�A�[�e�B�ƌ����܂��B��낵���ˁB");

                UpdateMainMessage("���i�F�E�E�E�E�E�E�`����FiveSeeker�H");

                UpdateMainMessage("�A�C���F�����I���̓`����FiveSeeker���I�I�}�W�������I�I�I");

                UpdateMainMessage("���F���[�F�N�����i����ł��ˁB�n�߂܂��āA���͍��A�A�C���N�ɂ��肢�����Ă������ł��B");

                UpdateMainMessage("���i�F���A�ǂǂǁA�ǂ����n�߂܂��āB�E�E�E���̃o�J�ɗ��݂��Ƃł����H");

                UpdateMainMessage("���F���[�F�A�C���N�ƃ��i����A��l�̃p�[�e�B�Ƀ{�N�������Ă��炢�����̂ł��B");

                UpdateMainMessage("�A�C���F�����A���i�I�����Ă��炨�����I�I");

                UpdateMainMessage("���i�F���H���A�����B���A�����������A�����炩�炨�肢���������炢�ł��B");

                UpdateMainMessage("�A�C���F���ȁI��������ȁI�H���������Ⴀ�����������I�I�I");

                UpdateMainMessage("���F���[�F�ǂ����p�[�e�B�ɓ���Ă��炦��悤�ł��ˁB");

                UpdateMainMessage("�A�C���F���������ł���I�������Ă��ł����I�I�z���g�����炱����낵�����肢���܂��I");

                UpdateMainMessage("���i�F�E�E�E�E�E�E�@�E�E�E�E�E�E�@�E�E�E�E�E�E");

                UpdateMainMessage("�A�C���F�����A�ǂ������񂾂惉�i�I�|�J�[���Ƃ��Ă��A�����Ɗ�ׂ�I�H");

                UpdateMainMessage("���i�F���A���F���[�l�B��낵�����肢���܂���");

                UpdateMainMessage("���F���[�F��������������݂������ˁB���߂�ˁA���i����B");
                
                UpdateMainMessage("���F���[�F���Ⴀ�����������瓯�s�����Ă��炤���Ǘǂ����ȁH");

                UpdateMainMessage("�A�C���F���������������ł��I�S�l�͊��҂��Ă܂��I�ƒd��A���҂��Ă܂��I");

                UpdateMainMessage("���F���[�F�������A�A�C���N�B�\����Ȃ����{�N�͌N���v���Ă�قǋ����Ȃ��񂾂�B");

                UpdateMainMessage("�A�C���F�������Ă��ł����B�`����FiveSeeker�������w�����Ȃ��x�̈Ӗ����炢������܂�����");

                UpdateMainMessage("���F���[�F����A���������킯�ł͂Ȃ��񂾂�B�����A�܂����������B");

                UpdateMainMessage("�A�C���F�͂��A�͂��͂��B������܂����I���������Ⴀ�I�I�����͓O��ŌP�����I�I�I");

                UpdateMainMessage("���F���[�F���������낵���ˁB���Ⴀ�A�{�N�͂���ŁB");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "���F���[���p�[�e�B�ɉ����܂����B";
                    md.ShowDialog();
                }
                we.AvailableThirdCharacter = true;

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "���F���[�͂��̏ꂩ�狎���Ă�����";
                    md.ShowDialog();
                }

                GroundOne.StopDungeonMusic();

                UpdateMainMessage("���i�F�A�C���A������Ɨǂ��H");

                UpdateMainMessage("�A�C���F��H������H");

                UpdateMainMessage("���i�F���́E�E�E���������̂��Ȃ񂾂��ǁA���F���[������ăA�C���Ɏ��ĂȂ��H");

                UpdateMainMessage("�A�C���F�͂��H��ȃ��P�Ȃ�����B�ǂ�����ǂ����Ă��ʐl����B");

                UpdateMainMessage("���i�F����A�܂������Ȃ񂾂��ǂˁB�ł��ǂ������Ă�Ǝv��Ȃ������H");

                UpdateMainMessage("�A�C���F���͋C���A�ߑ����A���͂��A�������A������A�i�i���A�N����S�Ă��ʐl���Ǝv�����B");

                UpdateMainMessage("���i�F����E�E�E����A�܂������Ȃ񂾂��ǂˁB�����I�J�V�C�̂�����B");

                UpdateMainMessage("�A�C���F���i�A�����͎�����R����ł邵�A���̐^�钆���B�����Ă񂾂�H");

                UpdateMainMessage("���i�F������A������ˁB�S�����S�����A�����ςȎ��������˖Y��ā�");

                UpdateMainMessage("�A�C���F���i�A�������Duel�ł�����Ă������H�R�K�Ɍ����ē��P���悤���B");

                UpdateMainMessage("���i�F�ǂ����B�A�C���A�����Ƃ����ǎ��������������ˁ�");

                UpdateMainMessage("�A�C���F�b�n�b�n�b�n�I����������Ȃ񂩂���킯�˂�����B���i�A���O���蔲������Ȃ�H");

                UpdateMainMessage("���i�F�����A�ǂ����B����ł��m��Ȃ�����ˁ�");

                UpdateMainMessage("���i�F�R�E�E�E�Q�E�E�E�P�E�E�E");

                UpdateMainMessage("�A�C���F�f�n�I�I");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "���̌�A�A�C���ƃ��i�͏h���֖߂�A�����Ĉ�����߂����B";
                    md.ShowDialog();
                }

                we.CommunicationCompArea2 = true;
                GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
                this.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "hometown.jpg");
                CallRestInn();

            }
            #endregion
            #region "�A�C�e���\�[�g�擾�C�x���g"
            else if (this.we.CommunicationCompArea2 && this.we.InfoArea31 && !this.we.AvailableItemSort)
            {
                UpdateMainMessage("�A�C���F���ƁE�E�E�����������̃A�C�e���́E�E�E");

                UpdateMainMessage("�A�C���F�Ƃ肠�������̕��킶��˂��ȁB");

                UpdateMainMessage("�@�@�w�b�|�C�x");

                UpdateMainMessage("���i�F������Ă�̂�H�o�J�A�C���B");

                UpdateMainMessage("�A�C���F���Ă킩�邾��B�A�C�e���̐������B");

                UpdateMainMessage("�A�C���F����Ƃ��ȁB���͒f���ăo�J�ł͂Ȃ��B");

                UpdateMainMessage("�A�C���F���[��A��������̕��킶��˂��ȁB�B�B");

                UpdateMainMessage("�@�@�w�b�|�C�x");

                UpdateMainMessage("���i�F�A���^�̕���ȊO��������ӂɓ������Ă邾������Ȃ��B����̂ǂ��������Ȃ̂�B");

                UpdateMainMessage("�A�C���F�����ƌ�Ő���������āA�S�z����ȁI�b�n�b�n�b�n�I");

                UpdateMainMessage("���i�F�܂������B�\�[�g���Ă��̂��炢�����Ɗo���Ȃ�����ˁB");

                UpdateMainMessage("�A�C���F�����H���̃\�[�g���Ă̂́H");

                UpdateMainMessage("�@�@�w�b�|�C�x");

                UpdateMainMessage("���i�F��ނ��ĈӖ���B");

                UpdateMainMessage("�A�C���F��ނ��A��̉������Ă񂾁H");

                UpdateMainMessage("�@�@�w�b�|�C�x");

                UpdateMainMessage("���i�F�A�C�e���𐮗����鎞�̕��ёւ��̃��[���ɂ�����̂�B");

                UpdateMainMessage("�A�C���F����ȃ��[���A�ǂ����ɗ��񂾁H");

                UpdateMainMessage("�@�@�w�b�|�C�x");

                UpdateMainMessage("���i�F������ƁI���ꎄ�������n���Ă�ԃ|�[�V��������Ȃ��̂�I�I�I");

                UpdateMainMessage("�@�@�@�w�b�V���S�I�H�H�I�H�H�I�I�I�x�i���i�̃��C�g�j���O�L�b�N���A�C�����y��j�@�@");

                UpdateMainMessage("�A�C���F�Q�t�H�I�I�I�H�H�H�E�E�E���A���܂˂��E�E�E�r�����炠��܂�g���Ă˂��񂾁B");

                UpdateMainMessage("���i�F�܂�����������q�[������Ă�񂾂��A���傤���Ȃ��񂾂��ǁB");
                
                UpdateMainMessage("���i�F����͂���Ƃ��āA�����ƕ����Ȃ�����ˁB");

                UpdateMainMessage("�A�C���F�E�E�E�n�C�E�E�E");

                UpdateMainMessage("���i�F����̕��ёւ�����@�������B");

                UpdateMainMessage("���i�F�܂��ŏ��͎g�p�i�ˁB�����n���Ă�y�ԃ|�[�V�����z��y�����̐����z���Y�������B");

                UpdateMainMessage("���i�F���ꂪ��ԃo�b�N�p�b�N�̏�ɗ���悤�ɂ��邱�Ƃ��ł����B�J���^���ł����");

                UpdateMainMessage("���i�F���ꂩ�玟���A�N�Z�T���ˁB");

                UpdateMainMessage("���i�F���A�C�����������Ă�y" + mc.Accessory.Name + "�z�Ȃǂ��Y�������B");

                UpdateMainMessage("���i�F�������Ɠ��l�A��ԃo�b�N�p�b�N�̏�ɗ���悤�ɂ��邱�Ƃ��ł���̂��");

                UpdateMainMessage("���i�F��͏��Ԃɕ���E�h��E���O�A�����āA���A�B�v�͎�ނɂ���ĕ��ёւ����\�ɂȂ郏�P��B");

                UpdateMainMessage("���i�F���ꂩ��Ώۃ\�[�g�ȊO�̃A�C�e���Ɋւ��ẮA�ȉ��̏������D�悳����B");

                UpdateMainMessage("���i�F�g�p�i�@���@�A�N�Z�T���@���@����@���@�h��");

                UpdateMainMessage("���i�F������ނ������ꍇ�͖��O�����ŕ��ёւ�������B");

                UpdateMainMessage("���i�F�������A���A�\�[�g�̏ꍇ�͓������A���������ꍇ�͏�ɖ��O���ɂȂ��B���ӂ��ĂˁB");

                UpdateMainMessage("���i�F�������A�J���^���ł���B��������Ă݂Ȃ�����ˁ�");

                UpdateMainMessage("�A�C���F�E�E�E�Ȃ񂩁A�ʓ|�����������I�I");

                UpdateMainMessage("���i�F�n�@�@�`�`�`�`�A���ł���ȃJ���^���Ȃ̖ʓ|��������̂�E�E�E�z���b�g�o�J��ˁE�E�E");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�A�C���B�̓A�C�e���𐮗��ł���悤�ɂȂ���";
                    md.ShowDialog();
                }
                we.AvailableItemSort = true;
            }
            #endregion
            #region "�R�K��l�֖�˔j��"
            else if (this.we.SolveArea34 && !this.we.CompleteArea34)
            {
                UpdateMainMessage("���F���[�F�A�C���N�A�����܂���B�{�N�͂�����Ƌ~�}�p��ł�����Ă��܂�����E�E�E");

                UpdateMainMessage("�A�C���F�����A�C�������I");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�A�C���͋}���Ńn���i�̏h���փ��i���^�񂾁E�E�E";
                    md.ShowDialog();
                }

                GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                UpdateMainMessage("�A�C���F�E�E�E�E�E�E�E");

                UpdateMainMessage("�n���i�F�E�E�E���v�݂�������A���Ă邾�����ˁB����Q��΃X�b�J���ǂ��Ȃ��B");

                UpdateMainMessage("�A�C���F�E�E�E���̂������B");

                UpdateMainMessage("�n���i�F�������Ă񂾂��B�����ɋA���Ă����񂾗ǂ���������Ȃ����B");

                UpdateMainMessage("�A�C���F���̂��������Ă񂾁I�I");

                UpdateMainMessage("�n���i�F�A�C���A�~�߂Ȃ����B");

                UpdateMainMessage("�A�C���F�E�E�E�E�E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E�Ȃ��A�f�ꂳ�񋳂��Ă����B");

                UpdateMainMessage("�n���i�F�Ȃ񂾂��H");

                UpdateMainMessage("�A�C���F���̃_���W�����A��̉��Ȃ񂾁H");

                UpdateMainMessage("�n���i�F�E�E�E��������悭�m��Ȃ��B�������Ȃ��ˁB");

                UpdateMainMessage("�A�C���F�E�\���I�{���͉����m���Ă�񂾂�I�H���܂ł���R�̒T���҂����Ă񂾂�I�H");

                UpdateMainMessage("�n���i�F�{������B���g�Ɋւ��Ă͈�ؒm��Ȃ��񂾂�B");

                UpdateMainMessage("�A�C���F���i�̂�A�ςȋ��̂����Ŗ��Ƀe���V�����n�C�ɂȂ邵�B");

                UpdateMainMessage("�A�C���F�I�}�P�ɍŌ�̑���̃A���B���Ȃ񂾂�A���́I�����Ă����I�H");

                UpdateMainMessage("�K���c�F�A�C���A���������Ȃ����B�S�𗐂��ȁB");

                UpdateMainMessage("�A�C���F���̑���̎��A���i���E�E�E���i�������킩��˂��䎌��A�����Ă��B");

                UpdateMainMessage("�K���c�F�A�C���A���O�ƂĂQ�K�ň�x�̌����Ă���͂��B�Ȃ�Ƃ������낤�H");

                UpdateMainMessage("�A�C���F�E�E�E�����B");

                UpdateMainMessage("�K���c�F�������A���̑���͂ȁB");

                UpdateMainMessage("�n���i�F���傢�ƃA���^�B");

                UpdateMainMessage("�K���c�F�����񂾁B��̓��V�̕��ŉ��Ƃ�����B");

                UpdateMainMessage("�n���i�F�܂������A�P�񂾂�����Ȃ������̂����B");

                UpdateMainMessage("�K���c�F�y�_�̎���Y�z�@���������͂��邾�낤�H");

                UpdateMainMessage("�A�C���F�_�̎���Y���Ă��́E�E�E�����f�B�̃{�P���t���Ă�A�����H");

                UpdateMainMessage("�K���c�F�A�C���A�����ă��i�N�����̑���Ŏ󂯂��̂́y�_�̎����z�ƌĂ΂�Ă���B");

                UpdateMainMessage("�A�C���F�_�̎����H");

                UpdateMainMessage("�K���c�F�����A����͐l���{����������\�͂�S��������邽�߂̌Ăѐ����ݒ肳��Ă���B");

                UpdateMainMessage("�K���c�F�������E�`���Ȃǂ͓������҂ɂ���ĈႤ���A�{�l�ɂƂ��Ĉ�ԓ���ݐ[�����e�����e�����B");

                UpdateMainMessage("�K���c�F���ꂪ�{�l�֒��ڌĂѐ��Ƃ��ē`���A���ۂɑ���ɂ����ċ@�\���ʂ����B���ꂪ�_�̎������B");

                UpdateMainMessage("�K���c�F����Ő\����Ȃ����A����ŉ��l���̐l�Ԃ����𗎂Ƃ��Ă���B");

                UpdateMainMessage("�A�C���F�I�I�I���ӂ������I�I");

                UpdateMainMessage("�n���i�F�������炩�ɑf���������A�ʖڂ����Ȃ�A�^�V�����X�ɏR��΂��Ăł��A���悤�ɂ��Ă�̂��B");

                UpdateMainMessage("�A�C���F���i���E�E�E�������i������Ŏ���ł�����I�I�I");

                UpdateMainMessage("�K���c�F���v���B���V���b���Ă���B");

                UpdateMainMessage("�A�C���F��������Ă��邾�낤���I�H");

                UpdateMainMessage("�n���i�F�A�C���A���悵�I");

                UpdateMainMessage("        �w���i�F�i���E�E�E�A�C���E�E�E���񂽁E�E�E�o�J����Ȃ��H�x�i�Q���̂悤���j");

                UpdateMainMessage("�A�C���F�������E�E�E�l�̋C���m�炸�ɁE�E�E���񒆂܂ŉ����o�J���Ă��E�E�E�n�n");

                UpdateMainMessage("�K���c�F������͍݂蓾��B�����A�n���i�ƃ��V�����ɗ͂�݂����ƂȂ����B");

                UpdateMainMessage("�n���i�F��������B�߂����ɖ�����������ˁB");

                UpdateMainMessage("�K���c�F�䂦�Ƀ��V�̓��i�N��b���鎖�Ƃ����B����Ŏ��s�͖����B�������񂾂̂��B");

                UpdateMainMessage("�A�C���F�n���i�f�ꂳ��A�K���c�f������A�������E�E�E���܂˂��B");

                UpdateMainMessage("�n���i�F�₾�ˁA�������ች�����ĂȂ���B���̒U�߂��T�|�[�g���Ă��ꂽ�̂��B");

                UpdateMainMessage("�K���c�F�n���i�A���O�ƂĐ����Əh���ŉ���������񂾁B");

                UpdateMainMessage("�A�C���F���̐_�̎����Ƃ��A���i�����������Ď��ł���ˁH");

                UpdateMainMessage("�K���c�F�������B�����͂������Ƌx�܂���Ɨǂ��B�A�C���A���O���悭�撣�����B");

                UpdateMainMessage("�n���i�F���i�����̓A�^�V�����Ă�������B�A�C���A���񂽂��\���x��ł��ȁB��ɏo�Ă��B");

                UpdateMainMessage("�A�C���F�͂��B���Ⴀ�����ꑫ��ɋx�܂��Ă��炢�܂��B");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�A�C���͎���������Ă��镔���ւƖ߂����E�E�E";
                    md.ShowDialog();
                }

                UpdateMainMessage("�A�C���F�ӂ��A��������R�K�̎��҂��܂��������ȁE�E�E");

                UpdateMainMessage("�A�C���F������A�����̓_���W�����T���͋x�݂ɂ��Ă������ȁB");

                UpdateMainMessage("�A�C���F���i�̂�|��邮�炢�������񂾁B�����x�܂��Ȃ��ƂȁB");

                UpdateMainMessage("�A�C���F�E�E�E��H��������E�E�E���F���[�̂�B");

                UpdateMainMessage("�A�C���F�����~�}�p����ɍs�����āA�S�R�p�����Ȃ��ȁB");

                UpdateMainMessage("�A�C���F�܂��ǂ����B���������Ă݂鎖�ɂ��邩�B������ꂽ�������́E�E�E�B");

                UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E�E�E");

                UpdateMainMessage("�A�C���F�E�E");

                // [�x��]�F���z���E�i�^�����E�j�̕`�ʂ̓��A���ɕ`���Ă��������B����͐^���n�C�x���g���烉�i�C�x���g�ɂ��Ă��������B
                //UpdateMainMessage("        �w�E�E�E���[���E�E�E�����̃o�J�[�I�x");

                //UpdateMainMessage("�A�C���F�E�E�E��H�������i���H");

                //UpdateMainMessage("�A�C���F��H���A�N�����˂��ȁB�����������ȁB");

                //UpdateMainMessage("�A�C���F�E�E�E");
                we.CompleteArea34 = true;
                CallRestInn();

                UpdateMainMessage("�A�C���F�E�E�E�y����");

                UpdateMainMessage("        �w�A�C���o�ŉ��`�F�@�C���t�B�j�e�B�E�u���[�I�I�x");

                UpdateMainMessage("        �w�{�{�E�{�{�{�{�{�E�b�{�I�h�K�b�V���A�A�A�A�@�@�@���I�I�I�x�i�����̃h�A���j�󂳂ꂽ�j");

                UpdateMainMessage("�A�C���F��H�����I�H�������I�I�I�������������������I�҂đ҂đ҂đ҂đ҂āI�I�I");

                UpdateMainMessage("���i�F�^���`�F�@�C���t�B�j�e�B�E�L�b�N�I�I�I�@�n�A�A�A�@�@�@�I�I�I");

                UpdateMainMessage("�A�C���F�E�E�E���H�A�A�A�A�@�@�@�@�E�E�E�i�p���[���j");
                
                UpdateMainMessage("        �w�A�C���͕����̑����痎���܂����x");

                UpdateMainMessage("���i�F���[����A��D������I�I�@�A�C���A�������ǂ����ˁ�");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�_���W�����Q�[�g���̍L��ɂāB";
                    md.ShowDialog();
                }

                UpdateMainMessage("�A�C���F���܂����B������������Ă̊o������H�E�E�E�����E�E�E");

                UpdateMainMessage("���i�F�����f�B�X���t������͎�������Ȃ���ł���H");

                UpdateMainMessage("�A�C���F���x������B�n���i�f�ꂳ��̏h���A�󂷂��肩��B");

                UpdateMainMessage("���i�F���v�A�����Əf�ꂳ��̋��͎���Ă���́A���S�z�Ȃ���");

                UpdateMainMessage("�A�C���F�}�W����B���ŋ����~��Ă񂾂�B�������n���i�f�ꂳ������l���Ă񂾁B");

                UpdateMainMessage("���F���[�F���i����A���C�����ł��ˁB");

                UpdateMainMessage("���i�F���F���[����A���͂悤�B������������ǍD��B");

                UpdateMainMessage("�A�C���F�������A�Q�N���Ƃ͌����A�󂯎~�߂��Ȃ��������B");

                UpdateMainMessage("�A�C���F���i�A���F���[�ɂ������؂�d�|���Ă݂��H");

                UpdateMainMessage("���i�F���H���F���[����ɁH���[��E�E�E");

                UpdateMainMessage("���F���[�F���v�ł���B���i����A�������������Ă݂Ă��������B");

                UpdateMainMessage("���i�F�����Ƃ���A�s�����B�\���āB");

                UpdateMainMessage("���F���[�F�͂��A���ł��ǂ����B");

                UpdateMainMessage("���i�F�t�E�D�D�D�E�E�E�s�����B�C���t�B�j�e�B�E�u���[�I");

                UpdateMainMessage("        �w�b�{�{�I�x");

                UpdateMainMessage("���F���[�F�I�H�@�b�N�I");

                UpdateMainMessage("        �w�b�{�E�b�{�{�{�{�{�I�h�I�I�I�H�H���I�I�I�x�i�󒆂ō����������n�����j");

                UpdateMainMessage("���i�F���A������A��������ƁI�������ĂȂ�����Ȃ��I");

                UpdateMainMessage("���F���[�F���āA����͂Ȃ��Ȃ��ł��ˁB�����܂����B");

                UpdateMainMessage("���F���[�F�R�K�n�߂̍��Ƃ͂܂�ŕʐl�ł��ˁB�A�C���N�A����͒ɂ������ł��傤�H");

                UpdateMainMessage("�A�C���F����ς�������B���i�A���O�̌����p�A�������������Ă��邼�B");

                UpdateMainMessage("���i�F�҂��Ă�A���œ������ĂȂ��킯�H�H");

                UpdateMainMessage("���F���[�F���i����A�����܂���B������ƃ{�N�̕��Ŕڋ��Ȏ���g���܂����B");

                UpdateMainMessage("���i�F�ڋ��H�ǂ��������Ȃ́H");

                UpdateMainMessage("���F���[�FOneImmunity��Genesis�̑g�ݍ��킹�ŁA�Ǐ������_���[�W��A�����������܂����B");

                UpdateMainMessage("���i�F�E�E�E���H�������H");

                UpdateMainMessage("���F���[�F�ȒP�Ɍ����ƁA�����_���[�W�������̏�Ԃł��B");

                UpdateMainMessage("���i�F���A�����Ȃ񂾁B����ς胔�F���[���񐦂��ł��ˁB����ȃ��[�V�����S�R�����Ȃ��񂾂��́B");

                UpdateMainMessage("���F���[�F�����A���񃂁[�V�����̊ԂɁA�ꌂ�������͓����Ă܂���B�z���A���Ă��������B");

                UpdateMainMessage("        �w���F���[�̍��r�̌�둤�ɏ��������A�U���c���Ă���B�x");

                UpdateMainMessage("�A�C���FFiveSeeker���F���[�l�Ɉꌂ����Ă₪��E�E�E�ق�Ɗ�Ȃ����c���ȁE�E�E");

                UpdateMainMessage("���i�F���F���[���񂠂肪�Ƃ��B�܂��b�B���d�˂Ă�����B");

                UpdateMainMessage("���F���[�F�����A���̒��q�ŃA�C���N��ǂ������Ă��܂��č\���܂����B");

                UpdateMainMessage("�A�C���F��������A���������Ă��˂��B�����A�R�K�̎��ҁI�|���Ƃ��悤���I�I");

                UpdateMainMessage("���i�F�c��͎��҂ˁB����؂��Ă����܂��傤��");

                UpdateMainMessage("���F���[�F�s���܂��傤�B�S�K���B�́A���������ł��B");

            }
            #endregion
            #region "�R�K���e"
            else if (this.we.CompleteArea3 && !this.we.CommunicationCompArea3)
            {
                UpdateMainMessage("�A�C���F�E�E�E����łR�K�˔j���E�E�E");

                UpdateMainMessage("���i�F����A����݂肵������āB�����Ɣh��Ɋ�΂Ȃ��́H");

                UpdateMainMessage("�A�C���F���i�A������Ƙb������񂾁B���F���[�ɂ���Ō������Ǝv���B");

                UpdateMainMessage("���F���[�F�A�C���N�A�{�N�͂�����ƂS�K�T���p�̃A�C�e������ʂ萮�����Ă��܂��B");

                UpdateMainMessage("�A�C���F�������B���Ⴀ�܂���łȁB");

                UpdateMainMessage("���i�F�E�E�E�z���z���A�K���c�f�������n���i�f�ꂳ��ɕ񍐂��ɂ�������");

                UpdateMainMessage("�A�C���F�����I��������t����ŐH���Ƃ��邩�I�I�b�n�b�n�b�n�I�I");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�A�C���͈�ʂ�A���̏Z�l�B�ɐ��������A���Ԃ����X�Ɖ߂��Ă������B";
                    md.ShowDialog();

                    md.Message = "���̓��̖�A�n���i�̏h�����ɂ�";
                    md.ShowDialog();
                }

                UpdateMainMessage("�A�C���F����A�����Ń��i�̂�����B���������񂾂��ăR�����I�b�n�b�n�b�n�I");

                UpdateMainMessage("���i�F�Ȃɂ�A�����������������ŉ������񂶂�Ȃ��́A�����͔F�߂Ȃ�����ˁH");

                UpdateMainMessage("�A�C���F�������̉ʂẮw��������ǂ�����A�b�l��x���Ƃ����肦�˂�����ł��B�b�n�b�n�b�n�I");

                UpdateMainMessage("���i�F�����A�����I�H������ȕςȌ��������Ă��킯�H�A�C���֒����Č����ĂȂ��H�H");

                UpdateMainMessage("�A�C���F�����A�����H���O�A�o���ĂȂ��̂���H");

                UpdateMainMessage("���i�F�o���ĂȂ����B�A�C�����񂽉R�t���Ă�񂶂�Ȃ��́H");

                UpdateMainMessage("�A�C���F��H�����E�E�E");

                UpdateMainMessage("�A�C���F�R���I�I�I");

                UpdateMainMessage("�A�C���F�b�n�b�n�b�n�b�n�I");

                UpdateMainMessage("        �w�h�o�b�L���A�@�@�@�@�@�I�I�I�x�i���i�̍��E���C�g�j���O�L�b�N���y��j");

                UpdateMainMessage("�n���i�F�A�b�n�b�n�b�n�B���i�����A�����܂������Ƙr���グ���˂��B");

                UpdateMainMessage("�A�C���F�����A�{���ɈЗ͂��オ���Ă邵�A�}�W�Œɂ����́B");

                UpdateMainMessage("���i�F�A�C���A�b���ĉ���B�����Ă݂Ȃ����B");

                UpdateMainMessage("�A�C���F��H���A�����E�E�E���͂ȁB�����A�R�K���e�͂����Ɗ��������񂾂Ǝv���Ă��B");

                UpdateMainMessage("�A�C���F�����A���ۂ͈�����B���e�������ō��܂łɌo���������̂Ȃ��s�����L�������񂾁B");

                UpdateMainMessage("���i�F�|�C�Â����̂ˁB����������A�C���͂��ƂȂ������ĂāA�������e����Ƃ���");

                UpdateMainMessage("�A�C���F����A�b�̓\�R�Ȃ񂾁A���i�B");

                UpdateMainMessage("�A�C���F���i�A���̃_���W�������e�A������ӂŎ~�߂Ă����˂����H");

                UpdateMainMessage("���i�F�E�E�E���H");

                UpdateMainMessage("�A�C���F�f�ꂳ��A�R�K�w�̓��B�҂͂����Ɖ��l���H");

                UpdateMainMessage("�n���i�F�P�Q�l����B");

                UpdateMainMessage("���i�F�P�Q�l���āE�E�EFiveSeeker�l���T�l������");

                UpdateMainMessage("���i�F�c��V�l���Ď��́A��Q�`�R�p�[�e�B���炢�������B���ĂȂ�����Ȃ��B");

                UpdateMainMessage("�n���i�F��������B���i�����ƃA�C���͂����w�܂�ɐ������鏊�ɂ�����Ď�����B");

                UpdateMainMessage("�A�C���F���i�A���̃_���W�����ɂ͖�������p�[�e�B�[���[���ɒ���ł���񂾁B");

                UpdateMainMessage("�A�C���F����҂͏����A����҂͖��𗎂Ƃ��A����҂͐S��a�񂾁B");

                UpdateMainMessage("�A�C���F����搂�����B�W���[�N�ł��֒��ł����ł��˂��B���t�ʂ�}�W���B");

                UpdateMainMessage("���i�F�A�C���A�����Ȃ�����B");

                UpdateMainMessage("�A�C���F���H");

                UpdateMainMessage("���i�F�A���^������Ȏ������Ƃ͎v��Ȃ��������Ęb��B");

                UpdateMainMessage("�A�C���F���͂ȁA�T�d�ɍl������Ō����Ă�񂾁B");

                UpdateMainMessage("���i�F�A���^�̐T�d�ȍl���Ȃ�ăA�e�ɂȂ�Ȃ����A���̘b���Ǝv���΁B");

                UpdateMainMessage("���i�F���Ⴀ�����玿�₵�Ă������A�A�C���A�����_���W�����T���͎~�߂Ă����H");

                UpdateMainMessage("�A�C���F�E�E�E�E�E�E");

                UpdateMainMessage("���i�F�b�n�C�A���܂��");

                UpdateMainMessage("�A�C���F���������A�ǂ��ł���ȕ��@���o�����񂾂�A���O�B");

                UpdateMainMessage("���i�F�ǂ������ėǂ�����Ȃ��A����Ȏ��́B�b�T�T�H�ׂĈ���Ŗ����ɔ����܂����");

                UpdateMainMessage("�A�C���F�����A�������ȁI�l���ĂĂ����傤���˂��I�Ƃ��Ƃ񍡓��͐H�ׂĈ��ނ��I�I");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�A�C���ƃ��i�͂��̌�A�ЂƂƂ��̒c�R���y���݁A�����ւƖ߂��Ă������B";
                    md.ShowDialog();
                }

                UpdateMainMessage("�A�C���F�R�K�Ŗ����������Ƃ͌����A���i�̂�E�E�E");

                UpdateMainMessage("�A�C���F�S�K���E�E�E�ǂ�ȍ�����낤�Ƃ����͒H�蒅���Č����邺�E�E�E�I");

                UpdateMainMessage("�A�C���F���ƁA�o�b�N�p�b�N�������ȁE�E�E���i�̕��ƁA���F���[�̂�ƁE�E�E");

                UpdateMainMessage("�A�C���F��������A�܂����F���[�Ɉꔭ�����Đ؂�Ă��˂��E�E�E������FiveSeeker���ȁB");

                UpdateMainMessage("�A�C���F�E�E�E�������A�������˂��ȁB");

                UpdateMainMessage("�A�C���F����σ_���W�����Q�[�g���L��Řr�Ȃ炵�����Ă������B");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�_���W�����Q�[�g���̍L��ɂ�";
                    md.ShowDialog();
                }

                GroundOne.StopDungeonMusic();
                this.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "HomeTown2.jpg");

                UpdateMainMessage("�A�C���FGaleWind�I�E�E�E����������E�E�EFireBall�I");

                UpdateMainMessage("�A�C���F�����E�E�E�悭�l������Q�񓯂�������Ă邾�����E�E�E");

                GroundOne.PlayDungeonMusic(Database.BGM10, Database.BGM10LoopBegin);

                UpdateMainMessage("�@�@�@�w�A�C���N�B�@�@�ǂ����A�@�@�s���Ă܂���B�x�@�@");

                UpdateMainMessage("�A�C���F�I�I�@���F���[���I�H");

                UpdateMainMessage("�@�@�@�������@��̕����A�C���̑S�̂֐G�ꂽ�B����ȋC�������B�@�������@�@");

                UpdateMainMessage("���F���[�F�A�C���N�AGaleWind�͎g�����ɂ���Ĕ��ɋ��͂ł��B");

                UpdateMainMessage("�A�C���F�E�E�E���������������炻���ɋ����񂾁H");

                UpdateMainMessage("���F���[�F���������ł��B�{�N�����ƂȂ��Q�t���Ȃ��āB");

                UpdateMainMessage("�A�C���F�����Ă���B�S�K�͂ǂ�ȏꏊ�Ȃ񂾁H");

                UpdateMainMessage("���F���[�F�A�C���N�A�{�N�ƈ�x�������܂��񂩁H");

                UpdateMainMessage("�A�C���F����ɓ����Ă���BFiverSeeker�Ȃ��x�S�K�𐧔e���Ă�񂾂�H");

                UpdateMainMessage("���F���[�F����͓������܂���B�p�[�e�B�ɂ���ă_���W�����̍\���͈Ⴂ�܂��B");

                UpdateMainMessage("�A�C���F���Ⴀ���F���[�̎��͂ǂ�Ȃ̂������񂾁H�����Ă����H");

                UpdateMainMessage("���F���[�F�A�C���N�A�S�K�ɓ��ݍ��ޑO���琏���ƕs�������Ă܂��ˁB");

                UpdateMainMessage("�A�C���F�������牽����H");

                UpdateMainMessage("���F���[�F�A�C���N�A�Ђ���Ƃ��ĕ|���񂶂�Ȃ��ł����H");

                UpdateMainMessage("�A�C���F��������H");

                UpdateMainMessage("���F���[�F�^����m�鎖���E�E�E�ˁB");

                UpdateMainMessage("�A�C���F���F���[�A�������B");

                UpdateMainMessage("���F���[�F�����A�ǂ��ł���B���ł��������Ă��Ă��������B");

                GroundOne.StopDungeonMusic();

                mc.CurrentLife = mc.MaxLife;
                mc.CurrentMana = mc.MaxMana;
                mc.CurrentSkillPoint = mc.MaxSkillPoint;

                bool result = EncountBattle("���F���[�E�A�[�e�B");

                if (!result)
                {
                    UpdateMainMessage("���F���[�F�b�ɂȂ�Ȃ��ł��ˁA�A�C���N�B�������݂��Ă��܂���B");

                    UpdateMainMessage("�A�C���F�b�Q�n�@�I�I�E�E�E�b�K�E�E�E�񂾂�A�S�R���Ă˂��E�E�E�b�Q�E�Q�z�E�E�E�E�S�H");

                    UpdateMainMessage("        �w�A�C���͒v�����Ƃ��v�����ʂ̌���f�����I�x");

                    UpdateMainMessage("���F���[�FCelestialNova�ł��B�@���C�t�����ɖ߂��Ă����܂��傤�B");

                    mc.CurrentLife = mc.MaxLife;
                }
                else
                {
                    // [�x��]�F�b�𐷂�グ�邽�߁A�����ꏟ�������ꍇ�͉����t���O������Đ���グ�Ă��������B
                    we.DefeatVerze = true;
                    UpdateMainMessage("�A�C���F�ǂ����I���̕����������낤���I�I");

                    UpdateMainMessage("���F���[�F���āA�m���ɃA�C���N�B�����Ȃ�܂����B�ł����ʂ����Ăǂ��ł��傤���H");

                    UpdateMainMessage("�A�C���F�ǂ������Ӗ����H�@�������I�H");

                    UpdateMainMessage("�@�@�@�������@�؂�􂭕����A�C���̑S�̂֒��ڐG��n�߂��I�@�������@�@");

                    UpdateMainMessage("        �w�b�q���E�E�E�b�q���q�����E�E�E�Y�K�K�K�I�x");

                    UpdateMainMessage("���F���[�FGaleWind, CarnageRush, WordOfPower,������Genesis�{�Q��s���ɂ�鋆�ɃR���{");

                    UpdateMainMessage("        �w�b�K�K�I�@�K�K�b�K�K�K�I�I�@�b�q���q�����x");

                    UpdateMainMessage("���F���[�F�{�N�̂��C�ɓ���A�󂯎���Ă��������B");
                    
                    UpdateMainMessage("        �w�b�q���A�b�q���q�����A�b�K�A�b�K�K�A�b�K�K�K�K�K�I�I�x");

                    UpdateMainMessage("���F���[�FᏋZ�@�C�����B�W�u���E�n���h���b�h�E�J�b�^�[�I");

                    UpdateMainMessage("        �w�b�K�A�K�K�K�K�K�A�Y�K�K�K�K�K�K�K�@�@�I�I�I�Y�S�S�S�H�I�I�I�H���E�E�E�x");

                    UpdateMainMessage("�A�C���F�b�Q�n�@�I�I�E�E�E�b�K�E�E�E�񂾍��̑S�R�����˂��E�E�E�b�Q�E�Q�z�E�E�E�E�S�H");

                    UpdateMainMessage("        �w�A�C���͒v�����Ƃ��v�����ʂ̌���f�����I�x");

                    UpdateMainMessage("���F���[�FCelestialNova�ł��B�@���C�t�����ɖ߂��Ă����܂��傤�B");

                    mc.CurrentLife = mc.MaxLife;
                }

                UpdateMainMessage("�A�C���F�E�E�E�S�K�́E�E�E��������H");

                UpdateMainMessage("�@�@�@�w�@�@�@�@�@�@�@��]�ł��B�@�@�@�@�@�x�@�@");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�A�C���͂��̏�ŋC���������E�E�E";
                    md.ShowDialog();
                }

                GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                UpdateMainMessage("�E�E�E�E�E�E");

                UpdateMainMessage("�E�E�E�E");

                UpdateMainMessage("�E�E");

                UpdateMainMessage("�@�@�@�w�������A�A�C��������ςɎ㉹�Ȃ񂩓f��������āA�炵���Ȃ�������E�E�E����B�x�@");

                UpdateMainMessage("���i�F��������ƁA�����̃o�J�B�N���Ȃ�����H");

                UpdateMainMessage("�A�C���F�E�E�E�����B");

                UpdateMainMessage("���i�F�Ȃ񂾁A�N���Ă��̂ˁB������ȂƂ��ŋ����肵�Ă�̂�H");

                UpdateMainMessage("�A�C���F���F���[��Duel�ŕ������܂����B");

                UpdateMainMessage("���i�F���F���[����Ƃ���Ă��́H�������Ȃ��݂��������ǁB");

                UpdateMainMessage("�A�C���FFiveSeeker�̒��Ń��F���[�͊m���A�Z�̒B�l��������ȁH");

                UpdateMainMessage("���i�F�����ˁA�m���y�V��̗��z�̕ێ��҂ŁA�u�p�߂炦�����̑��݂����v�ł���B");

                UpdateMainMessage("�A�C���F���F���[�̂�A�P�ɐ_�̎���Y�ɗ�����������������˂��B�͂����ȏゾ�B");

                UpdateMainMessage("���i�F����Ⴀ���F���[����AFiveSeeker�����B���X�̃x�[�X�\�͂��Ⴄ�񂶂�Ȃ��H");

                UpdateMainMessage("�A�C���F����ŁA�S��������˂������Ă񂾁B�M�����˂��E�E�E�S���܂ꂿ�܂��������B");

                UpdateMainMessage("�A�C���F�����f�B�̃{�P�͂ǂ�����ă��F���[�Ɛ���Ă��񂾂낤�ȁE�E�E");

                UpdateMainMessage("���i�F���̃A�C���Ɍ����ƈ����񂾂��ǁB");

                UpdateMainMessage("�A�C���F��H�C�ɂ���ȁB�����Ă݂�B");

                UpdateMainMessage("���i�F���F���[������āA���B�Əo�������������A�����Ǝ�������Ă�悤�ȕ��͋C�Ȃ��H");

                UpdateMainMessage("�A�C���F�����A��������ȋC�����邺�B");

                UpdateMainMessage("���i�F���B���܂��K���ł��������Ȃ����e���y��������Ⴄ�����邵�B");

                UpdateMainMessage("���i�F����Ɋe���@�E�X�L���̃R���r�l�[�V��������E���Ă���Ǝv��Ȃ��H");

                UpdateMainMessage("�A�C���F�����āA�������[�V�������قƂ�ǌ����˂����ȁB");

                UpdateMainMessage("�A�C���F���ƂȂ������A���������̂����Ă�Ə��Ă�C�����Ȃ��Ȃ��Ă���񂾂�ȁB");

                if (result)
                {
                    UpdateMainMessage("�A�C���F���ɍŌ�͖̂h���悤���Ȃ��������B");

                    UpdateMainMessage("���i�F�ǂ�Ȃ̂�H");

                    UpdateMainMessage("�A�C���F���m�ɂ͊o���Ă˂����E�E�E�܂�GaleWind���B");

                    UpdateMainMessage("�A�C���F����𓯎��ɂQ�����Ă����B�܂�E�E�E�R��s���ɂȂ�B");

                    UpdateMainMessage("�A�C���F��������X�ɓ����ɂQ��ł���ɂ��ւ�炸�ACarnageRush��WordOfPower���J��o���Ă����B");

                    UpdateMainMessage("���i�F������E�E�E������E�E�E");

                    UpdateMainMessage("�A�C���FCarnageRush��h�䂵�Ă��AWordOfPower�������ɗ���񂾁B�������R�̂��Q�񂾁B�h���˂��B");

                    UpdateMainMessage("�A�C���F���̍��v�U��𓯎��ɂ����Q����悤�AGenesis�Ƃ������X�y�����Ԃ������Ă��B");

                    UpdateMainMessage("���i�F�U����Q����āE�E�E�ŁACarnageRush�Ƃ����͉̂���o�Ă��̂�H");

                    UpdateMainMessage("�A�C���F�T�񂮂炢���B");

                    UpdateMainMessage("���i�F�U�C�Q�C�T�E�E�E���č��v�U�O�񂶂�Ȃ��I�I�@�����[�[�[�[�[�I�H");

                    UpdateMainMessage("�A�C���FGenesis���̂��Q��r�����B���F���[�̓C���r�W�u���E�n���h���b�h�E�J�b�^�[�Ɩ��t���Ă��B");

                    UpdateMainMessage("���i�F���Ⴀ�P�Q�O��E�E�E�E�E�E�E�E�E�P�O�O�񒴂��Ă�E�E�E�E�E�\�E�E�E");

                    UpdateMainMessage("�A�C���F�����A����ł���Ǝ���������Ȃ񂾁B���߂Č������B");
                }

                UpdateMainMessage("���i�F���̃C���t�B�j�e�B�E�u���[�Ȃ�ĐԎq���R�Ȃ̂����m��Ȃ���ˁB");

                UpdateMainMessage("�A�C���F�Ƃɂ���FiveSeeker�͂������B�_���W�����ŉ��w���e�͂��ꂾ���������񂾂�A����ς�B");

                UpdateMainMessage("�A�C���F��������A�������F���[��Duel�������Ă���A���C�o�Ă������I�I");

                UpdateMainMessage("���i�F�b�t�t�A�A�C���̂��畉���������ˁBFiveSeeker�ڎw����Ȃ�撣��Ȃ��Ƃˁ�");

                UpdateMainMessage("�A�C���F���͂S�K���e���邺�B���i�A���O�����͂��Ă���I�I");

                UpdateMainMessage("���i�F�����A��������B�܂��A�C��������܂Ƃ��ɂȂ�悤�Ȃ�A�����擪�ɂȂ��B");

                UpdateMainMessage("�A�C���F�b�n�b�n�b�n�I�����Ă���邺�B�Ȃ����x���A���F���[�ɉ��B�Q�l�Œ���ł݂悤���H");

                UpdateMainMessage("���i�F�����ˁA�Q�l�Ȃ牽�Ƃ��|���邩������Ȃ���ˁBDuel���`�Ȃ烔�F���[����󂯂Ă��ꂻ�������B");

                UpdateMainMessage("�A�C���F�S�K���A��Γ˔j���悤�ȁI");

                UpdateMainMessage("���i�F�����A�撣��܂����");

                this.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "HomeTown.jpg");
                we.CommunicationCompArea3 = true;
                CallRestInn();
            }
            #endregion
            //#region "�S�K����˓���"
            //else if (!this.we.CommunicationEnterFourArea && this.we.CompleteArea3 && this.we.CommunicationCompArea3)
            //{
            //    UpdateMainMessage("�n���i�F�E�E�E���悢�悾�˂��B");

            //    UpdateMainMessage("�K���c�F�ӂށA�������炾�B���悢��h���Ȃ邼�B");

            //    UpdateMainMessage("�n���i�F�菕����������A���񂽁B");

            //    UpdateMainMessage("�K���c�F���܂������A���܂�m�b��݂��łȂ����B");

            //    UpdateMainMessage("�n���i�F�A�b�n�n�n�B�@��������A���̕ӂ���͂܂����������ɂȂ�Ȃ���B");

            //    UpdateMainMessage("�n���i�F�ł��ŏ��͔��M���^���������ǁA���i����񂪈ӊO�Ə����M�ɂȂ��Ă�˂��B");

            //    UpdateMainMessage("�K���c�F�������ȁA���̓�l�͂Ђ���Ƃ�����A��������̂����m���ȁB");

            //    UpdateMainMessage("�K���c�F�A�C���E�E�E���i�E�E�E");
                
            //    UpdateMainMessage("�K���c�F���V�̊��҂�傫�����؂��Ă݂���B���񂾂��B");

            //    this.we.CommunicationEnterFourArea = true;
            //}
            //#endregion
            #region "�S�K����߂�"
            else if (this.we.InfoArea46 && !this.we.InfoArea47)
            {
                UpdateMainMessage("�A�C���F��������A�߂��Ă������B�����؂�h���ɖ߂�Ƃ��邩�B");

                UpdateMainMessage("���F���[�F�A�C���N�A�����܂��񂪃{�N�͏����t�@�[�W���{�a�ɍs���Ă��܂��B");

                UpdateMainMessage("�A�C���F��������߂��͖����͂������A�����ɍs�����ł���̂��H");

                UpdateMainMessage("���F���[�F�����A�Ƃ��Ă����̓]���r���|�C���g������܂�����A���v�ł��B");

                UpdateMainMessage("�A�C���F�������B���Ⴀ�܂�������낵���ȁB");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "���F���[�̓_���W�����Q�[�g���̍L��̐�֋����Ă������B";
                    md.ShowDialog();
                }

                UpdateMainMessage("�A�C���F���ĂƁB���i�A���O���h���ňꏏ�ɔтł��H�ׂ邩�H");

                UpdateMainMessage("���i�F���H���A����B");

                UpdateMainMessage("�A�C���F���������A���v����B�₯�ɑf�����ȁE�E�E");

                UpdateMainMessage("���i�F�C�ɂ��Ȃ��ŁA�������s���܂����");

                UpdateMainMessage("�A�C���F���A�����B");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�A�C���ƃ��i�̓n���i�̏h���֌��������B";
                    md.ShowDialog();
                }

                UpdateMainMessage("�A�C���F�ӂ����A��肩�������B�n���i�f�ꂳ��A�����������܁I");

                UpdateMainMessage("�n���i�F�͂���A���e�����܁B");

                UpdateMainMessage("���i�F�f��l�A�����������܁B");

                UpdateMainMessage("�n���i�F�͂���A���i���������e�����܁B");

                UpdateMainMessage("�A�C���F�S�K�A���̉�L�B�������������񂶂Ȃ����H");

                UpdateMainMessage("���i�F�����ˁB");

                UpdateMainMessage("�A�C���F���̏��A���Ȏd�|����ςȓ䂩�����˂��B���̂܂ܓ˂��؂낤���I");

                UpdateMainMessage("���i�F�����A�撣��܂���B");

                UpdateMainMessage("�A�C���F����A���͕����ɖ߂��Ă邺�B���i�A�܂������ȁB");

                UpdateMainMessage("���i�F����A�I���X�~�B");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�A�C���ƃ��i�͊e���A�����̗\�񂵂������֍s�����B";
                    md.ShowDialog();
                }

                UpdateMainMessage("�A�C���F�E�E�E�b�`�B�ǂ��Ȃ��Ă񂾂����B");

                UpdateMainMessage("�A�C���F�S�K�́E�E�E�����̂͂����B�C���������炢�����˂����ȁB");

                UpdateMainMessage("�A�C���F������E�E�E�s����͂����B");

                UpdateMainMessage("�A�C���F�ςɍl����ȁB�����������B");

                UpdateMainMessage("�A�C���F�o�b�N�p�b�N�����ł����āA�������Q��Ηǂ����B");

                UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E");

                UpdateMainMessage("�A�C���F�E�E");

                // [�x��]�F�^�����E�̕`�ʂ͉��x�����x�����x�����x�����x�����Ȃ��Ă��������B
                UpdateMainMessage("�@�@�@�@�w�A�C���͂��̓��A���������x");

                UpdateMainMessage("�@�i�i�@�E�E�E�@����������@�E�E�E�@�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@�����H�@�E�E�E�@�Ƃƈ�ʂɑ����@�E�E�E�@�H�@�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@����ƃC�������O�@�E�E�E�@�N�̂��̂��@�E�E�E�@�H�@�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@����Ɓ@�E�E�E�@�󂪁@�E�E�E�@�����@�E�E�E�@�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@�i�b�n�n�n�j�@�E�E�E�@�N���@�E�E�E�@�΂��Ă₪��@�E�E�E�@�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@�����̒��@�E�E�E�@�����������@�E�E�E�@���H�@�E�E�E�@�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@�E�E�E�@�E�E�E�@�E�E�E�@�j�j");

                UpdateMainMessage("�A�C���F�������������I�H�I�H�@�n�@�b�n�@�b�E�E�E");

                UpdateMainMessage("�A�C���F�����A�����E�E�E�b�N�\�A�������Ă񂾈�́B");

                UpdateMainMessage("�A�C���F�E�E�E���i�̂�E�E�E�������ɕ������Ƃ��Ă��ȁB");

                UpdateMainMessage("        �������@���i�F���񂽁A�����B���Ă�ł���H�@������");

                UpdateMainMessage("�A�C���F�B���Ă���ĉ��̂��Ƃ��B");

                UpdateMainMessage("�A�C���F�ʖڂ��B�S�R�g�Ɋo�����˂��B");

                UpdateMainMessage("�A�C���F�E�E�E���������d��Ȏ���Y��Ă���̂��H���������A�v���o���E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E");

                UpdateMainMessage("�A�C���F�E�E");

                UpdateMainMessage("�A�C���F�E");

                UpdateMainMessage("�A�C���F�b�N�\�A�����v���o���˂��B���X�����˂����ẮB");

                UpdateMainMessage("�A�C���F�����_���W�����ł܂��A���i�̂�ɂ���ƂȂ��b���Ă݂邩�B");

                UpdateMainMessage("�A�C���F���������F���[�̂�A�t�@�[�W���{�a�ɖ߂�Ƃ������Ă��ȁB��̂Ȃ�̂��߂ɁH");

                UpdateMainMessage("�A�C���F�����A�ʖڂ��A�ʖڂ��I�~�߂��I�I�l���ĂĂ����傤���˂��A�Q�邼�I�I");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "���̓��A�A�C���͊���l�����������܂܍Ăі���ɂ����B";
                    md.ShowDialog();
                }

                this.we.InfoArea47 = true;
                CallRestInn();
            }
            #endregion
            #region "�S�K���Ԗ߂�"
            else if (this.we.InfoArea410 && !this.we.InfoArea411)
            {
                UpdateMainMessage("�A�C���F�����A���ɖ߂������B���i�I�h���֍s�����I");

                UpdateMainMessage("���F���[�F�A�C���N�A�{���ɂ����܂���B�������t�@�[�W���{�a�ֈ�U���܂��B");

                UpdateMainMessage("�A�C���F�����A�����B���Ⴀ�܂������ȁB");

                UpdateMainMessage("���F���[�F�͂��A����ł́B");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "���F���[�̓_���W�����Q�[�g���̍L��̐�֋����Ă������B";
                    md.ShowDialog();
                }

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�A�C���ƃ��i�̓n���i�̏h���֌��������B";
                    md.ShowDialog();
                }

                UpdateMainMessage("�A�C���F�E�E�E");

                UpdateMainMessage("���i�F�E�E�E");

                UpdateMainMessage("���i�F�E�E�E�E�E�E�˂��A�C���B");

                UpdateMainMessage("�A�C���F��H�����H");

                UpdateMainMessage("���i�F���ˁE�E�E�����E�E�E�����A�E�E�E���߂�A���߂�Ȃ����B");

                UpdateMainMessage("        �w���i�͊�ɗ�������Ăĕ����Ă��܂����B�x");

                UpdateMainMessage("�A�C���F�_���W�����E�E�E��߂Ƃ����H");

                UpdateMainMessage("���i�F�Ⴄ�́I�I�A�C���A�_���W�����͐i��ł��傤�����I");

                UpdateMainMessage("�A�C���F���A�����E�E�E�I�[�P�[�I�[�P�[�I");
                
                UpdateMainMessage("�A�C���F���O�������܂Ō����񂾁A������ł��i��ł�邺�I�b�n�b�n�b�n�I");

                UpdateMainMessage("���i�F�E�E�E�E�E�E");

                UpdateMainMessage("�A�C���F�Ȃ��A�������ɁE�E�E���߂�Ȃ����������ᕪ����Ȃ����B");

                UpdateMainMessage("�A�C���F��̉����ӂ��Ă�񂾁H�����ꂵ���Ȃ���Ό����Ă݂�B");

                UpdateMainMessage("���i�F�E�E�E�E�E�E");

                UpdateMainMessage("���i�F�E�E�E�E�E�E");

                UpdateMainMessage("���i�F�E�E�E�E�E�E");

                UpdateMainMessage("���i�F�E�E�E�E�E�E");

                UpdateMainMessage("���i�F�E�E�E�E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E�E�E�E");

                UpdateMainMessage("���i�F�E�E�E�E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E�E�E�E");

                UpdateMainMessage("���i�F�E�E�E�E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E�E�E�E");

                UpdateMainMessage("        �w���i�͊炩�痼��𗣂����B�����̃g�p�[�Y�F�̖ڂł͂Ȃ��A�����ԂɂȂ��Ă���B�x");

                UpdateMainMessage("���i�F�E�E�E�E�E�E����B");

                UpdateMainMessage("�A�C���F��H");

                UpdateMainMessage("���i�F���̉B������B");

                UpdateMainMessage("�A�C���F�B�����E�E�E���H");

                UpdateMainMessage("���i�F�E�E�E����B");

                UpdateMainMessage("�A�C���F���A�����B�B�����ȁB�N������1��100���炢���邳�B");

                UpdateMainMessage("���i�F�E�E�E����B���߂�Ȃ����E�E�E����B�S�����ˁE�E�E");

                UpdateMainMessage("�A�C���F��A�������������������炻��Ȏӂ�ȁB���ȁH");
                
                UpdateMainMessage("�A�C���F���i�A���O���ӂ�Ƃǂ����ėǂ����A�킩��˂��񂾂�B������΂��B");

                UpdateMainMessage("���i�F����E�E�E�b�t�t�A�S�����˖{���ɁB");

                UpdateMainMessage("�A�C���F�B�����A�b���邩�H");

                UpdateMainMessage("���i�F������A����͘b���Ȃ���B�ł��A���������ˁB���������b����Ǝv���́B");

                UpdateMainMessage("�A�C���F�������H�����፡�͖������Ęb���Ȃ��ėǂ����B����������������̎����A�܂��ȁB");

                UpdateMainMessage("���i�F����A����B�܂����ˁB�S�����˃z���g�A�_���W�������f�΂����肳��������āB");

                UpdateMainMessage("�A�C���F�C�ɂ���ȁI������A�܂��������痊�ނ��I�I");

                UpdateMainMessage("���i�F�����A�����������r���ō����グ�Ȃ��ł��");

                UpdateMainMessage("�A�C���F�悵�A�����ŋx�ނƂ��邩�B����ȁI");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�A�C���ƃ��i�͊e���A�����̗\�񂵂������֍s�����B";
                    md.ShowDialog();
                }

                UpdateMainMessage("�A�C���F���i�̂�A�����͋@�������ʂ�ɂȂ�����Ȃ�ǂ����B");

                UpdateMainMessage("�A�C���F���ĂƁA�o�b�N�p�b�N�������ƁE�E�E");

                UpdateMainMessage("�A�C���F�E�E�E�b�t���@�A�����ӊO�Ɣ��Ă�ȁB�Q��Ƃ��邩�B");

                UpdateMainMessage("�A�C���F�E�E�E�E�E�E");

                UpdateMainMessage("�@�@�@�@�w�A�C���͂��̓��A���������x");

                UpdateMainMessage("�@�i�i�@�E�E�E�@����@�E�E�E�@�Ȃ�Ă����̂��H�@�E�E�E�@�E�E�E�@�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@�C�Ɓ@�E�E�E�@��n�@�E�E�E�@�����ēV��@�E�E�E�@�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@�����˂����O���ȁ@�E�E�E�@�ǂ��Ł@�E�E�E�@�o�����@�E�E�E�@�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@�E�E�E�@�E�E�E�@�E�E�E�@�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@�i�U�U�A�@�@���E�E�E�j�@�E�E�E�@�C�@�E�E�E�@�ǂ������@�E�E�E�@�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@������R�@�E�E�E�@�E�E�E�@��H�����H�E�E�E�@�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@�E�E�E�@�i�i�i�����s���Ă݂����B�ǂ�������H�j�j�j�E�E�E�@�E�E�E�@�j�j");

                UpdateMainMessage("�A�C���F���A����I�I�I�킠�����������I�I�I�@�n�@�b�n�@�b�n�@�b�E�E�E");

                UpdateMainMessage("�A�C���F�Ђ���Ƃ��č���̖��̑������E�E�E�n�@�b�n�@�b�n�@�b�E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E�n�@�b�n�@�b�n�@�b�E�E�E���������A�����������B");

                UpdateMainMessage("�A�C���F�E�E�E�n�@�b�n�@�b�n�@�b�E�E�E�b�t�E�D�E�E�E�b�t�E�E�E�D�D�E�E�E");

                UpdateMainMessage("�A�C���F�������A�S�K���̂͂���ȓ���˂��B");

                UpdateMainMessage("�A�C���F����ŁA���ł���ȏ�ԂɂȂ�񂾁A�b�N�\�I�I");

                UpdateMainMessage("�A�C���F�E�E�E�ʖڂ��A���������Ă������l�����������܂��B");
                
                UpdateMainMessage("�A�C���F�����A���ݕ��ł��T���Ă��邩�B");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�A�C���͏h���̂P�K�J�E���^�[�܂ō~��Ă����B";
                    md.ShowDialog();
                }

                UpdateMainMessage("�A�C���F�������A�������B�y�u���I�A�N�A���t�@�C�A�z���ƁB");

                UpdateMainMessage("�A�C���F�����b�S�N�A�b�S�N�E�E�E����");

                UpdateMainMessage("�A�C���F�E�E�E���ӂ��E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E���i�̂�A��������Ȏӂ��Ă�񂾁B");

                UpdateMainMessage("�A�C���F�����A�B����������͈̂����̂�������Ȃ��B");

                UpdateMainMessage("�A�C���F�������A���̎ӂ���ُ͈킾�B�����������B");
                
                UpdateMainMessage("�A�C���F�ڂ����オ��܂ŋ����Ă��邶��˂����B���Ă��˂��B");

                UpdateMainMessage("�A�C���F�B�������̂��ӂ��Ă錴������˂������m��Ȃ��ȁB");

                UpdateMainMessage("�A�C���F�����A�܂��͂���𕷂��Ă��Ȃ����ɂ́E�E�E");

                UpdateMainMessage("        �������@���i�F�ł��A���������ˁB���������b����Ǝv���́B�@������");

                UpdateMainMessage("�A�C���F�{���ɉ���b�����肾�B���i�E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E����A�S�K���B");

                UpdateMainMessage("�A�C���F����ȏ��ŁA�v�l�΂��肵�Ă͑ʖڂ��B�S�K�ƌ�������S�K���B");

                UpdateMainMessage("�A�C���F�S�K�E�E�E�˔j���邼�I�I");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�A�C���͂��̌�A�����ɖ߂��čĂі���ɂ����B";
                    md.ShowDialog();
                }

                we.InfoArea411 = true;
                CallRestInn();
            }
            #endregion
            #region "�S�K�I�Ֆ߂�"
            else if (this.we.InfoArea416 && !this.we.InfoArea417)
            {
                UpdateMainMessage("���i�F�߂������A�A�C���B�����A��U�h���ɍs���܂���B");

                UpdateMainMessage("���F���[�F���i����A�{�N�̓t�@�[�W���{�a�ɖ߂��āA�C�t�����T���Ă��܂��B");

                UpdateMainMessage("���i�F���A�����B���肢�����B");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "���F���[�̓_���W�����Q�[�g���̍L��̐�֋����Ă������B";
                    md.ShowDialog();
                }

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�n���i�̏h���ɂāE�E�E";
                    md.ShowDialog();
                }

                UpdateMainMessage("�@�@�@�@�w�A�C���͖ڂ�����܂܉��ɂȂ��Ă���x");

                UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E�@�ǂ����@�E�E�E�@�����ꏊ�@�E�E�E�@����H");

                UpdateMainMessage("���i�F�E�E�E����E�E�E");
                
                UpdateMainMessage("�n���i�F���傢�ƁA���炷���B");

                UpdateMainMessage("���i�F���A�n�C�B");

                UpdateMainMessage("�n���i�F�E�E�E�A�C���A��������ǂ����낤�ˁB");

                UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E�@�ق�@�E�E�E�@�C��������ā@�E�E�E");

                UpdateMainMessage("�n���i�F���̂������������ˁB���ƃq�������T���[�������Ă�����B");

                UpdateMainMessage("���i�F�E�E�E�����E�E�E");

                UpdateMainMessage("�n���i�F���悵�B");

                UpdateMainMessage("���i�F�ǂ����āE�E�E�����Ȃ���������񂾂낤�ˁA�A�C���B");

                UpdateMainMessage("�n���i�F�ǂ̂��炢�i�񂾂̂��H");

                UpdateMainMessage("���i�F�}�b�v��������肾�ƁA���������}�b�v�オ���ߐs������鏊�B");

                UpdateMainMessage("�n���i�F�A�C���E�E�E�ق�A������B���݂ȁB");

                UpdateMainMessage("�A�C���F�E�E�E�b�O�A�S�N�E�E�E�b�K�n�@�I�I�@�b�K�n�I�@�O�E�D�D�E�E�E");

                UpdateMainMessage("�A�C���F�K�A�A�A�@�@�@�@�@�I�I�I�I�I�I");

                UpdateMainMessage("�n���i�F�A�C���I���������Ȃ����I�z�����v������I");

                UpdateMainMessage("�A�C���F�O�K�A�A�@�A�@�A�A�@�@�I�I�I�I�A�A�A�@�A�@�A�A�@�@�I�I�I�I");

                UpdateMainMessage("���i�F���߂�ˁA�A�C���B�S�����ˁB���߂�ˁB����ς莄��");

                UpdateMainMessage("�n���i�F���悵�I");

                UpdateMainMessage("�n���i�F�A�C���I�ق�A�q�������T���[����A��������݂ȁB");

                UpdateMainMessage("�A�C���F�K�A�b�K�A�A�@�A�A�I�I�O�A�A�A�A�@�@�@�I�I�I�E�E�E�E�E�b�n�@�b�n�@");

                UpdateMainMessage("�A�C���F�b�n�A�A�@�E�E�E�n�@�E�E�E�b�Q�z�I�@�b�Q�z�I�E�E�E�n�A�@�E�E�E");

                UpdateMainMessage("�n���i�F�ق�A�܂�������B�����ꑧ����A�ق�B");

                UpdateMainMessage("�A�C���F�E�E�E�n�@�E�E�E�n�@�E�E�E�t�E�E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E�@���F�@�E�E�E�@���F���[");

                UpdateMainMessage("�n���i�F�I�I�I�I�I");

                UpdateMainMessage("�@�@�@�@�w�n���i�̊�F�ُ͈�Ȃ܂łɐ��߂Ă���x");

                UpdateMainMessage("���i�F�A�C���A���F���[����͂����ɂ͋��Ȃ���B���C�t���򎝂��Ă��鏊��B");

                UpdateMainMessage("�n���i�F���i�����A�����l�ōs�����Ă�񂾂��H");

                UpdateMainMessage("���i�F���H�R�l�ł��B�A�C���ƁA�����ă��F���[����B");

                UpdateMainMessage("�K���c�F�E�E�E�n���ȁE�E�E");

                UpdateMainMessage("�@�@�@�@�w�삯���Ă����K���c�̊�F�͎��X�Ȃ܂łɐ[���Ȋ�����Ă���B�x");

                UpdateMainMessage("�K���c�F���i�N�A�b������B");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�A�C���𕔉��ŋx�܂��ĕ������o���B�@�H���ɂāE�E�E";
                    md.ShowDialog();
                }

                UpdateMainMessage("���i�F�R�K���o�����鎞�ɃA�C�����N���Ƙb���Ă���ł��B");

                UpdateMainMessage("���i�F����œ`����FiveSeeker���Q�����Ă������đ�͂��Ⴌ�ŁB");

                UpdateMainMessage("�K���c�F���i�N�A���݂����̐��E�̂��炭��͒m���Ă��邾�낤�B");

                UpdateMainMessage("���i�F�E�E�E�����B");

                UpdateMainMessage("�K���c�F�A�C���ɂ͓��R�����S�Ă��^�u�[���B�S�K���e����܂ŁA�܂������Ă͂����ȁH");

                UpdateMainMessage("���i�F�͂��B");

                UpdateMainMessage("�n���i�F���i�����B�A�C���͂��̃��F���[�ɏo��������͂���̂����H");

                UpdateMainMessage("���i�F������Ȃ���E�E�E�B");

                UpdateMainMessage("���i�F�ł��A�A�C���������͂��Ⴌ���邩��A�ǂ����Ȃ��Ďv�����́B");

                UpdateMainMessage("�K���c�F���~���Ȃ����B");

                UpdateMainMessage("�n���i�F�A���^�A���o���́B");

                UpdateMainMessage("�K���c�F�ق��Ă���B");

                UpdateMainMessage("���i�F���~���āE�E�E�_���W�������e���ł����H");

                UpdateMainMessage("�K���c�F���ށA���V�̌����݈Ⴂ���B");

                UpdateMainMessage("�n���i�F���悵�B�����璆�~���ĉ��ɂȂ���Č����̂��B�R���g���[���ł��₵�Ȃ���B");

                UpdateMainMessage("�K���c�F�ق��Ă���ƌ����Ă��邾�낤�I");

                UpdateMainMessage("���i�F�ǂ�����΁E�E�E�ǂ���ł��傤�ˁE�E�E�A�C���A���߂�Ȃ����E�E�E");

                UpdateMainMessage("�@�@�@�@�w���i�͗���Ŋ���ǂ����B�@���������k���Ă���悤���x");

                UpdateMainMessage("�n���i�F�A���^�������~�����������Ŗ��ʂ���B");

                UpdateMainMessage("�K���c�F�����E�E�E�܂����E�E�E���̃��F���[�Ƃ́B");

                UpdateMainMessage("�K���c�F����͂܂�����Ȃ��V�ˁB");

                UpdateMainMessage("�K���c�F�E�ɏo����̂͂����B�ŋ����B");

                UpdateMainMessage("�@�@�@�@�w���i���Ăї���������A�n�b�Ɗ���グ���x");

                UpdateMainMessage("���i�F�ŋ����āA�����f�B�X�̂��t�����񂶂�Ȃ���ł����H");

                UpdateMainMessage("�K���c�F���F���[�̓����f�B�X�Ƃ̂c�t�d�k�ł͕K���ꌂ�ł���Ă���B");

                UpdateMainMessage("���i�F���ꂶ�����ς�B");

                UpdateMainMessage("�K���c�F�I���͂悭�����Ă�������B");

                UpdateMainMessage("�����f�B�X�F�w�b�P�B�@���̕������B�x");

                UpdateMainMessage("���i�F�ꌂ�ŏ����Ă���̂ɁH");

                UpdateMainMessage("�K���c�F���F���[�B������{�C�Ő���Ă��鏊�͈�x���Č������͂Ȃ��B");

                UpdateMainMessage("�K���c�F�I���͂�����@������ŁA�������ӎ�������𓾂Ȃ��ƌ������Ƃ��B");

                UpdateMainMessage("�n���i�F���̎q�̓����A�s�C���Ȃ��炢�Y�킾��������ˁB");

                UpdateMainMessage("�K���c�F���i�N�A���F���[�ƑΛ��������́H");

                UpdateMainMessage("���i�F���K�Ȃ炠��܂���B�_���W�����Q�[�g���ŁB");

                UpdateMainMessage("�K���c�F�ǂ�Ȋ����ł������H");

                UpdateMainMessage("���i�F���̌����p���Ԃ��Ă݂��񂾂��ǁA�y���󂯎~�߂��Ă��܂��܂����B");

                UpdateMainMessage("�K���c�F�A�C���̕��͂ǂ����H");

                // �Q���ځA���F���[�c�t�d�k�ŏ��������ɉ����ĉ�b��ω������Ă��������B
                UpdateMainMessage("���i�F�_���W�����Q�[�g���łc�t�d�k���������ł��B������������݂����ł����ǁB");

                UpdateMainMessage("�K���c�F���F���[���E�E�E�������ƁI�H");

                UpdateMainMessage("���i�F�����A���̃o�J�A�C�������Ă�킯�����ł����B");

                UpdateMainMessage("�n���i�F����������ˁB");

                UpdateMainMessage("���i�F���H");

                UpdateMainMessage("�n���i�F���̎q�̂c�t�d�k����́E�E�E");
                
                UpdateMainMessage("�n���i�F�O���S�Q�R�s����B");

                UpdateMainMessage("���i�F�E�E�E��������ƁE�E�E�ǂ��������ł����H");

                UpdateMainMessage("�n���i�F�y�V��̗��z�̕ێ��҃��F���[�A���̎q�͕K��������B���������q�Ȃ̂��B");

                UpdateMainMessage("�n���i�F�ǂ�ȑ���ɂ��{�C���o���Ȃ��B�����ď��������߂��肵�Ȃ��q��������B");

                UpdateMainMessage("�K���c�F���V�̎��������������B�ŏ��̈�U���A�I����Ă��܂������̂悤�ȕ��͋C�B");

                UpdateMainMessage("�K���c�F�S���S���A���Ă�B���̎����V�́A�S�ꂩ���ɂ̊����o���B");

                UpdateMainMessage("�K���c�F�������̌�A�K���J�E���^�[��H����ă��F���[�͕�����B");

                UpdateMainMessage("���i�F�A�C���̎��́E�E�E�Ԉ���ĉ�����Y��āA����������Ď��H");

                UpdateMainMessage("�n���i�F����́E�E�E�ǂ��Ȃ��Ă񂾂��B������Ȃ��Ȃ��Ă����ˁB");

                UpdateMainMessage("�K���c�F���i�N�A�_���W�����T���𑱂��Ȃ����B");

                UpdateMainMessage("���i�F�E�E�E���H�������ƁE�E�E�͂��B");

                UpdateMainMessage("�n���i�F����͂�����ƕ��͋C���Ⴄ�ˁB���i�����B");

                UpdateMainMessage("���i�F�͂��B����Ȃ̂͏��߂Ăł��B");

                UpdateMainMessage("�K���c�F�����E�E�E�E�E�E�E�E�E�E�E�E�@�@�@�@����B�@�@�@�@���ށB");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�K���c�͗���������ԂŁA�N�ւƂ��Ȃ��A�󒆂֌��n�߂��B";
                    md.ShowDialog();
                }

                UpdateMainMessage("�K���c�F�������邩�ˁB");
                
                UpdateMainMessage("�K���c�F���i���Ȃ����B");
                
                UpdateMainMessage("�K���c�F���O�͂��̂��������m���߂Ă���B");

                UpdateMainMessage("�K���c�F���i���Ȃ����B");

                UpdateMainMessage("�K���c�F���O�͊ԈႢ�Ȃ��ł��̂߂����B");

                UpdateMainMessage("�K���c�F���i���Ȃ����B");

                UpdateMainMessage("�K���c�F�r���A�����Ă������Ă͂Ȃ��B");

                UpdateMainMessage("�K���c�F���i���Ȃ����B");

                UpdateMainMessage("�K���c�F�ǂ����Ă�����ǂ����͈�U�x�݂Ȃ����B");

                UpdateMainMessage("�K���c�F���i���Ȃ����B");

                UpdateMainMessage("�K���c�F���O�Ȃ炫���Ɗ�������͂����B");

                UpdateMainMessage("�K���c�F���i���Ȃ����B�A�C���B");

                UpdateMainMessage("�@�@�@�@�w�K���c�͗��ڂ��J���A�e�[�u���֊��߂����x");

                UpdateMainMessage("�K���c�F���i�N�A��͂�N���L�[�ɂȂ��Ă���B�ԈႢ�Ȃ�����낤�B");

                UpdateMainMessage("���i�F�͂��B");

                UpdateMainMessage("�n���i�F���i�����B�A�C���𓱂��Ă���Ă�����B");

                UpdateMainMessage("���i�F�͂��B�킩��܂����B");

                UpdateMainMessage("���i�F������U�����֖߂�܂��B");

                UpdateMainMessage("�n���i�F�����A�����͂������x�ނ񂾂ˁB");

                UpdateMainMessage("���i�F�͂��A���肪�Ƃ��������܂����B");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "����A�A�C���̕����ɂāE�E�E";
                    md.ShowDialog();
                }

                // [�x��]�F�^�����E�̕`�ʂ͉��x�����x�����x�����x�����x�����Ȃ��Ă��������B
                UpdateMainMessage("�@�@�@�@�w�A�C���͌ċz����������ԂŖ������Ă���x");

                UpdateMainMessage("�@�i�i�@�E�E�E�@���[�߂��B�@�E�E�E�@�ʖڂɌ��܂��Ă邾��@�E�E�E�@�E�E�E�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@�����@�E�E�E�@�Ə����Ƒ����΂�����@�E�E�E�@���邢�@�E�E�E�@�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@�E�E�E�@���[�߂��@�E�E�E�@��ł����Ă�@�E�E�E�@�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@�E�E�E�@�E�E�E�@�E�E�E�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@���@�E�E�E�@�E�E�E�@�C�����ǂ��ȁ@�E�E�E�@�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@�E�E�E�@�������@�E�E�E�@�̂��Ă���@�E�E�E�@���̎��E�E�E�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@�E�E�E�@�i�i�i�@���i�@�j�j�j�E�E�E�@�E�E�E�@�j�j");

                UpdateMainMessage("�A�C���F�E�E�E�E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E");

                UpdateMainMessage("�A�C���F�E�E�����E�E�E");

                UpdateMainMessage("�A�C���F���E�E�E�C�e�e�e�E�E�E");

                UpdateMainMessage("�A�C���F�����́E�E�E�f�ꂳ��́E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E�E�E�E�b�n�I�I�I���܂��I�I�I");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�A�C���͍Q�ĂĔ����J���ĒʘH�֏o�悤�Ƃ����E�E�E";
                    md.ShowDialog();
                }

                UpdateMainMessage("�@�@�@�@�y�o�`�C�C�B�B�B���I�I�I�z");

                UpdateMainMessage("���i�F�C�b�^�@�E�E�E����ˁI�I�I������̂�I���̃o�J�A�C���I�I");

                UpdateMainMessage("�A�C���F����E�E�E�܂��������ɋ���Ƃ́E�E�E�n�E�E�E�n�n�n�E�E�E");

                UpdateMainMessage("���i�F���������S�z���Ă���ė����̂ɁE�E�E�Œ�ˁI�I");

                UpdateMainMessage("�A�C���F�����Ⴊ�o�߂��܂��Ă��B�_���W�����ɂ��O��u���ė����܂����Ǝv���āB");

                UpdateMainMessage("���i�F�A�E�^�E�V�E���R�R�ɘA��ė����񂶂�Ȃ��B�������Ă�̂�H");

                UpdateMainMessage("�A�C���F�}�W����I�H");

                UpdateMainMessage("���i�F�S���A�ǂ̕ӂ���o���ĂȂ��̂�B�����t���t����������Ă����B");

                UpdateMainMessage("�A�C���F�}�W����I�H");

                UpdateMainMessage("���i�F�ςȎ��u�c�u�c�����Ă����A�N���Ă鎞�ɖ��V�a�Ȃ�ĕ����Ă�������B");

                UpdateMainMessage("�A�C���F�}�W����I�H");

                UpdateMainMessage("���i�F�����́g�}�W����I�H�h���Č�������A�t�@�C�i�����C�g�j���O�H��킹�邩��ˁ�");

                UpdateMainMessage("�A�C���F�E�E�E�����A����͊��ق��B");

                UpdateMainMessage("���i�F���v�Ȃ́H�̂̕��́B");

                UpdateMainMessage("�A�C���F�����A���v���B���̂����A�܂��������Ă����B");

                UpdateMainMessage("���i�F���A�����A�����E�E�E�ǂ�Ȃ̂�H");

                UpdateMainMessage("�A�C���F�����𒝂��Ă�B�ƂƑ����΂���B���������������B");

                UpdateMainMessage("���i�F�܂��ɖ��ˁB�����O����Ȃ��B");

                UpdateMainMessage("�A�C���F����A�����Ȃ񂾂��ǂȁE�E�E");

                UpdateMainMessage("���i�F�A�C���A�����͂ǂ�����́H");

                UpdateMainMessage("�A�C���F�s�����A�S�K���e�B");

                UpdateMainMessage("���i�F�E�E�E���悢��ˁB");

                UpdateMainMessage("�A�C���F���͂ȁB���܂ł̖��A�����O����Ȃ��C�����Ă�񂾁B");

                UpdateMainMessage("���i�F����B");

                UpdateMainMessage("�A�C���F���i�A���O�Ђ���Ƃ��āE�E�E����B");

                UpdateMainMessage("�A�C���F�s�����B�S�K���e�Ɍ����āB");

                UpdateMainMessage("���i�F����B�����撣��܂����");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�A�C���͍Ăю����̕����ւƖ߂����B�����Ė邪�X���Ă������B";
                    md.ShowDialog();
                }

                WE.InfoArea417 = true;
                CallRestInn();
            }
            #endregion
            #region "�S�K���e"
            else if (this.we.CompleteArea4 && !this.we.CommunicationCompArea4)
            {
                this.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "hometown2.jpg");
                GroundOne.PlayDungeonMusic(Database.BGM10, Database.BGM10LoopBegin);
                
                UpdateMainMessage("�A�C���F�S�K���e�E�E�E�{���͂����Ɗ�ԏ��Ȃ񂾂낤���ǂȁB");

                UpdateMainMessage("�A�C���F���ĂƁA�h���ɍs�������B�����Řb�𕷂����Ă���B");

                UpdateMainMessage("���F���[�F�����A�A�C���N�B���̃_���W�����̃Q�[�g���Řb���܂��傤�B");

                UpdateMainMessage("�A�C���F���F���[�A�������₨�O�A�h���═��ɂ͊���o���Ȃ��񂾂ȁB");

                UpdateMainMessage("���F���[�F�͂��A�����l�X�̓��킢�͋��ł��ĂˁB�����͍K���l�͏W�܂�ɂ����ł����B");

                UpdateMainMessage("�A�C���F���Ⴀ�����A�y�u���I�A�N�A���t�@�C�A�z�ł������Ă����B���������ő҂��ĂĂ���B");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�A�C���͏h���ֈ�U�W���[�X�𔃂��ɍs���A�����čĂі߂��Ă����B";
                    md.ShowDialog();
                }

                UpdateMainMessage("�A�C���F�����A�����Ă������B�ق�A���i�̕����I");

                UpdateMainMessage("���i�F����A���肪�ƁB");

                UpdateMainMessage("�A�C���F�������̓��F���[�̕����B�󂯎���Ă���B");

                UpdateMainMessage("���F���[�F�����A���C�������肪�Ƃ��������܂��B");

                UpdateMainMessage("�A�C���F�Ȃ��E�E�E�ǂ�����b�������H");

                UpdateMainMessage("���F���[�F�{�N����b���܂��傤�B�ǂ��ł����H���i����B");

                UpdateMainMessage("���i�F�����E�E�E�������B");

                UpdateMainMessage("�A�C���F�b���O�ɉ��������₳���Ă���B");

                UpdateMainMessage("���F���[�F�ǂ��ł���B���ł��傤�H");

                UpdateMainMessage("�A�C���F���ւ̎莆�A����͂ǂ�����Ēu�����񂾁H");

                UpdateMainMessage("���F���[�F�E�E�E�������A�C���N�ł��B�����ł��ˁA��������b���n�߂܂��傤�B");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "���F���[�͂��̏�ɍ������܂܁A�ڂ������ԂŒ���n�߂��B";
                    md.ShowDialog();
                }

                UpdateMainMessage("���F���[�F�����ɐ��m�ɕ\�����܂��傤�B");

                UpdateMainMessage("���F���[�F�{�N���g�͂��̎莆�Ƃ������̂��A�C���N�ɓ͂��Ă͂��܂���B");

                UpdateMainMessage("�A�C���F�E�E�E�ǂ������Ӗ����H");

                UpdateMainMessage("���F���[�F�A�C���N�A�R�K�ɍs�����Ƃ������A�y���K�ɗ�����z�Ǝv���܂���ł������H");

                UpdateMainMessage("�A�C���F�����A�������B���i�ɃK���c�̏f�����t��������ȁB�ǂ��z�����Ǝv�������B");

                UpdateMainMessage("�A�C���F����ŁA�ǂ����Ă��ғ��P�������������B����ȂƂ����ȁB");

                UpdateMainMessage("���F���[�F�A�C���N�A���ꂪ�y�����z�ł��B");

                UpdateMainMessage("�A�C���F�E�E�E�킩��˂��B�����ǂ������Ӗ��ł̓����Ȃ񂾁H");

                UpdateMainMessage("���F���[�F������x�����܂��傤�B�{�N���g�̓A�C���N�֎莆��͂��Ă��܂���B");

                UpdateMainMessage("�A�C���F���Ⴀ�N���͂������Č����񂾂�H");

                UpdateMainMessage("���F���[�F�E�E�E�A�C���N���g�ł��B");

                UpdateMainMessage("�A�C���F�E�E�E�����g�H�E�E�E�b�n�I���F���[���ʔ�����������ȁI�b�n�b�n�b�n�I");

                UpdateMainMessage("���F���[�F�A�C���N�A���̐��E�ł͂��ꂪ�����ł��B");

                UpdateMainMessage("�A�C���F���͂���Ȏ莆���������L���͂˂��B�����̊ԈႢ����B");

                UpdateMainMessage("���F���[�F�����A�����ł��B");

                UpdateMainMessage("���F���[�F�A�C���N�A�N�͊m���ɐ��ɕ�����Ȃ����̏u�ԁA��������ł��B");

                UpdateMainMessage("�A�C���F������A�S�R�킩��˂����B���F���[�A���������Ă�̂��������Ă���B");

                UpdateMainMessage("���F���[�F�����ł��ˁB����ł͕�����Ȃ���������܂���B");

                UpdateMainMessage("���F���[�F���Ⴀ�A����͂������ł��傤�H�@�y���̎O�ʋ��z�o���Ă��܂����H");

                UpdateMainMessage("�A�C���F��H�����A�R�K�ŏo�Ă������̃��[�v���u����ȁB");

                UpdateMainMessage("���F���[�F�A�C���N�A���̃_���W�����ɗ���O�A�t�@�[�W���{�a�ɖK�ꂽ���͂���܂��񂩁H");

                UpdateMainMessage("�A�C���F�����H���˂ɁE�E�E�������Ȃ��E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E�����A�������������B");

                UpdateMainMessage("�A�C���F���i�ƈꏏ�ɖK�ꂽ��������B���ȁI�H");

                UpdateMainMessage("���i�F�����ˁA���̎��̓t�@�����܁A�G���~�����Ɉ�x��������ăA�C���������o���āB");

                UpdateMainMessage("�A�C���F�������Ă�H���i���G���~���������������āA�������U�����񂾂�H");

                UpdateMainMessage("���i�F���A��������Ȃ��ł���I�H�A�C�����t�@�����܂������؂񌩂�񂾂��Č������炶��Ȃ��B");

                UpdateMainMessage("�A�C���F���₢��A�Ⴄ���B�m���ɂ��̎��̓��i�A���O��");

                UpdateMainMessage("���F���[�F�n�n�n�A�A�C���N���t�@�����܂������������ƌ������Ƃɂ��Ă����܂��傤�B");

                UpdateMainMessage("�A�C���F�b�P�A���`�����ł���B�@�������A�������Ă���Șb���Ă񂾁B");

                UpdateMainMessage("���F���[�F�ǂ��ł��B�t�@�[�W���{�a�Ły���̎O�ʋ��z���Ă܂���ł������H");

                UpdateMainMessage("�A�C���F���͒m��˂��B�������Ƃ͂˂��ȁB");

                UpdateMainMessage("���i�F���A�t�@�����܂֊獇�킹���Ă鎞�ɁA�t�@�[�W���{�a�����Љ�Ă��������B");

                UpdateMainMessage("���i�F���̎��ɂˁA���́y���̎O�ʋ��z�������Ă�������́B");

                UpdateMainMessage("�A�C���F���A�������≽������Ȏ������Ă��Ȃ��O�A�����������ꂢ�������Ƃ��B");

                UpdateMainMessage("���F���[�F�A�C���N�́A�ߋ��Ɉ�x�y���̎O�ʋ��z�̏��𓾂Ă��܂��B");

                UpdateMainMessage("�A�C���F�����B");

                UpdateMainMessage("���F���[�F�ł��A���́y���̎O�ʋ��z�����Ȃ̂��͂悭�m��Ȃ��B");

                UpdateMainMessage("�A�C���F�����A�������ȁB���ꂢ�ȋ����炢�����m��Ȃ��������B");

                UpdateMainMessage("���F���[�F����������ł��A�y���i����͋��Ɋւ���m���𓾂Ă����z�Ƃ��������͒m���Ă����B");

                UpdateMainMessage("�A�C���F�܂��A����ȏ����ȁB���ꂪ�ǂ��������Ă����񂾁H");

                UpdateMainMessage("���F���[�F�����ă_���W�����R�K�̍\�z�́A�܂��Ɂy���̎O�ʋ��z�������B");

                UpdateMainMessage("���F���[�F�ǂ����Ă��Ǝv���܂��H");

                UpdateMainMessage("�A�C���F�E�E�E�킩��˂��B�S�R�킩��˂����B");

                UpdateMainMessage("�A�C���F�܂�ŉ������̎�����m���Ă�������A�R�K�_���W�����̍\���������Ȃ����B");

                UpdateMainMessage("�A�C���F�Ƃł��A�����������ȁB");

                UpdateMainMessage("���F���[�F���ꂪ�y�����z�ł��B");

                UpdateMainMessage("�@�@�@�@�w�A�C���͂��̎��A�ُ�Ȃقǂ̗�⊾��w���Ɋ������B�x");

                UpdateMainMessage("�A�C���F�E�E�E�E�E�E�킩��˂��B");

                UpdateMainMessage("�A�C���F���F���[�A���O�������Ă�񂾁H���ɂ́A�S�R�킩��˂��B");

                UpdateMainMessage("���F���[�F�A�C���N�B�����Ȃ��ł��������B���ꂪ�y�����z�Ȃ�ł��B");

                UpdateMainMessage("�A�C���F���͓����ĂȂ񂩂��˂��I���͂����^���ʂ���Ԃ���ق����I");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�A�C���͋���ۂɂȂ����y�u���I�A�N�A���t�@�C�A�z�𓊂������B";
                    md.ShowDialog();
                }

                UpdateMainMessage("���F���[�F�A�C���N�A�����܂���B�ł͂S�K�֓��ݍ��ގ��A�Ђǂ��s�������Ă��܂����ˁB");

                UpdateMainMessage("���F���[�F����͉��̂ł��H");

                UpdateMainMessage("�A�C���F����Ⴀ�A���߂ē��ݍ��ޗ̈悾���A���i�����|�ꂿ�܂������ȁB�������炢�s���ɂ͂Ȃ邳�B");

                UpdateMainMessage("���F���[�F���ꂾ���ł����H");

                UpdateMainMessage("�A�C���F�����I�I���ꂾ������I�I");

                UpdateMainMessage("���F���[�F�ǂ����āA����ȑԓx�Ő��𒣂�グ�Ă��ł����H");

                UpdateMainMessage("�A�C���F����A���܂˂��E�E�E���ƂȂ��B");

                UpdateMainMessage("���F���[�F�����A��������₢�l�߉߂��ł����B�����܂���B");

                UpdateMainMessage("�A�C���F�E�E�E�E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E�����A���̊ŔB");

                UpdateMainMessage("�A�C���F�S�K�̍Ō�́A���̊Ŕ��������Ȃ������B");

                UpdateMainMessage("�A�C���F���͂��̊Ŕ����āA���Ȃ��Ƃ���]�����B���̐��̏I���݂����ȁB");

                UpdateMainMessage("�A�C���F�����Ă����B���̊Ŕ͉��Ȃ񂾁H");

                UpdateMainMessage("���F���[�F�^���ł��B");

                UpdateMainMessage("�A�C���F�^���H");

                UpdateMainMessage("���F���[�F�͂��A���̊Ŕɂ͐^�����`����Ă��܂��B");

                UpdateMainMessage("�A�C���F������A���̐^�����Ă̂́H");

                UpdateMainMessage("���F���[�F�E�E�E�E�E�E");

                UpdateMainMessage("�A�C���F�����E�E�E���������E�E�E�������Ă񂾂�B�����Ă����B");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "���F���[�͖ڂ��J���A�A�C���ɋC�t����Ȃ��悤�ɁA���i�̕��ւƖ����Ŗڂ��������B";
                    md.ShowDialog();
                }

                UpdateMainMessage("���i�F�A�C���A�����B���Ă��鎖�E�E�E�b����ˁB");

                UpdateMainMessage("���i�F�A�C���A�_���W�����ɍs�����Ƃ������������͊o���Ă�H");

                UpdateMainMessage("�A�C���F���B�̎��x�̓_���W�����Ő��藧���Ă邾��B�����҂��Ȃ��ƂȁB");

                UpdateMainMessage("���i�F�����ˁA�ł��v���o���Ă݂āB�Ȃ�Ń_���W�����ŋ��҂��ɂ����񂾂����H");

                UpdateMainMessage("�A�C���F���͐̂���A�͂ƌ��o�J������ȁB��Ԑ��ɍ����Ă������Ď����B");

                UpdateMainMessage("���i�F�����ˁB�o�J�A�C���͌���{�Ŋ撣���Ă�����B");

                UpdateMainMessage("���i�F�ł��A�����Ǝv���o���Ă݂āA���Ō��̌P��������悤�ɂȂ����́H");

                UpdateMainMessage("�A�C���F���i�A���O�̉B���Ă��鎖�Ƃǂ��֌W������񂾁H");

                UpdateMainMessage("���i�F�֌W��A���Ȃ́A���肢�A�v���o���Ă݂āB");

                UpdateMainMessage("�A�C���F�E�E�E�������Ȃ��E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E�E�E�E�����A�������B");

                UpdateMainMessage("�A�C���F�����������B���i�A���O�����ɗ��K�p�̌��B�@�����Ă����񂾂����ȁB");

                UpdateMainMessage("���i�F�r���S���@�Ȃ񂾈ӊO�Ɗo���Ă�̂ˁB�b�t�t�t");

                UpdateMainMessage("�A�C���F���O���v���o�����Č������炾��B�������A����Ȃ̂���̂Ȃ񂾂��Ă񂾁H");

                UpdateMainMessage("���i�F�A�C���A���m�ɂ͂���͗��K�p�̌�����Ȃ��́B");

                UpdateMainMessage("�A�C���F�����A����Ă��̂��H�܂��K�L�̍�������ȁB�ԈႦ����������B");

                UpdateMainMessage("���i�F����͂ˁA�y�_��  �t�F���g�D�[�V���z�Ȃ́B");

                UpdateMainMessage("�A�C���F�͂����I�H�b�n�n�A���i�B��k����T�ɂ���B����Ȃ��̂��_���ȃ��P�˂�����B");

                UpdateMainMessage("���i�F�A�C���A������Ȃ������Ȃ́B���܂ŉB���Ă��Ė{���ɂ��߂�ˁB");

                UpdateMainMessage("�A�C���F����A���₢��҂Ă�B���Ń��i������Ȃ��̎����Ă��񂾂�H");

                UpdateMainMessage("�A�C���F����A���₢��A���₢�₢��B���ꂪ�������Ă񂾂�H");

                UpdateMainMessage("���i�F�A�C���A�����嗎�������Ă�B�����炿���Ƙb����B");

                UpdateMainMessage("���i�F�܂��A���Ŏ��������Ă����A�����ǁB");

                UpdateMainMessage("���i�F���񂾂��ꂳ��̌`���Ȃ́B���ꂳ��A�̂͐������p�̎g����łˁB");

                UpdateMainMessage("���i�F���̂��ꂳ�񂪐́A���@�X�^�ꂳ��Ƃ����l����󂯌p�����������ꂾ�����́B");

                UpdateMainMessage("���i�F�A�C�����Ă��B���������A���������ォ��������Ȃ��B");

                UpdateMainMessage("�A�C���F�����A�v���o���������˂����炢�ォ�����ȁB");

                UpdateMainMessage("���i�F�A�C���A�o�J�݂����ɋ������Ⴍ�邩��A���������̂��Ȃ��Ďv�����́B");

                UpdateMainMessage("�A�C���F����œn���Ă��ꂽ�̂����̌��Ȃ̂��H");

                UpdateMainMessage("���i�F�����ˁA���ꂳ�񂪌����Ă��ꂽ�́B");

                UpdateMainMessage("���i�̕�F�w���i�A���͂ˁA�N���Ɏg���Ă��炤�̂���ԗǂ��́B�x");

                UpdateMainMessage("���i�̕�F�w���i�A���O�͌��p�͏��������Ȃ���������Ȃ���ˁB�x");

                UpdateMainMessage("���i�̕�F�w���̎��͂ˁB�����Ă邾������_���B�N���Ɏg���Ă��炢�Ȃ����B�x");

                UpdateMainMessage("���i�F�����̎��͂ˁA���p����������Ă��񂾂��ǁA���ꂪ�܂�Ń_���������킯�B");

                UpdateMainMessage("�A�C���F�܂��A���i�͂ǂ��l���Ă������p����ȁB");

                UpdateMainMessage("�A�C���F���Ⴀ�A���ꂩ�H�B�������Č����̂́A�_����n���Ă������Ď����H");

                UpdateMainMessage("���i�F�E�E�E����A�����������ɂȂ�ˁB");

                UpdateMainMessage("�A�C���F���A���₢��B�҂Ă�B�ʂɂ���Ȃ͉̂B�����������Ƃ��Ă����͂˂�����B");

                UpdateMainMessage("�A�C���F�_���W�����S�K�ł̂��O�A�Ђǂ��e���p���Ă�����˂����B");

                UpdateMainMessage("�A�C���F���������̌�A���x�����x���ӂ邵�A���炩�ɓ��h���Ă�����B");

                UpdateMainMessage("���i�F�A�C���A���̃_���W�����ɒ��ގ��A�y�_��  �t�F���g�D�[�V���z�����ĂȂ���ˁH");

                UpdateMainMessage("�A�C���F�����A��������K���c�f���̕���ł͔���؂ꂽ���āE�E�E");

                UpdateMainMessage("���i�F�����A�C���ɓn�������̌���B����ɔ����Ă��郏�P�������̂�B");

                UpdateMainMessage("�A�C���F�E�E�E�E�E�E");

                GroundOne.StopDungeonMusic();

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "���i�̕\��͏������΂񂾂悤�Ɍ������B�����Ĉ�ċz�u���đ������B";
                    md.ShowDialog();
                }

                UpdateMainMessage("���i�F�˂��A�C���B�@���ł��낤�ˁH");

                UpdateMainMessage("�A�C���F�E�E�E���ł���H�@���łȂ񂾂�H�����Ă����B���i�B");

                UpdateMainMessage("���i�F���ł��Ǝv���H�@�A�C���B");

                UpdateMainMessage("�A�C���F�E�E�E�킩��˂��E�E�E�S�R�킩��˂��B");

                UpdateMainMessage("�A�C���F���F���[�ɂ��Ă��A���i�ɂ��Ă��������B");

                UpdateMainMessage("�A�C���F���O�炳�A���Ń\�R�Řb���~�߂�񂾁H");

                UpdateMainMessage("���i�F�E�E�E�E�E�E�B");

                UpdateMainMessage("���F���[�F�E�E�E�E�E�E�B");

                UpdateMainMessage("�A�C���F���������E�E�E���Ȃ񂾂�I�b�N�\�I�I");

                UpdateMainMessage("���i�F�A�C���A�撣���āB�{�����܂ܓ����Ȃ��ŁB");

                UpdateMainMessage("�A�C���F�����瓦���Ă˂����āI�@���͒m��˂����Č����Ă邾��I�H");

                UpdateMainMessage("���i�F�E�E�E���߂�B�����������ǂ��l�߂�����āB");

                UpdateMainMessage("�A�C���F�E�E�E���������B���O��A���ǉ��ɐ^���Ƃ���b�����肪�����񂾂�H");

                UpdateMainMessage("���F���[�F�A�C���N�A����͈Ⴂ�܂��I");

                UpdateMainMessage("�A�C���F���i�A���O�����F���[�ƈꏏ�ɉB����������肾��H");

                UpdateMainMessage("���i�F�A�C���A����͈Ⴄ��I���肢�M���āI");

                UpdateMainMessage("�A�C���F�������牽�ł����Ɠ����Ȃ��񂾁I");

                UpdateMainMessage("���i�F�A�C���������Ŏv���o���Ȃ��ƑʖڂȂ́B");

                UpdateMainMessage("�A�C���F���������ŁH");

                UpdateMainMessage("���i�F������B���⃔�F���[���񂩂狳����񂶂�Ȃ��āA�A�C�����g���v���o���K�v������̂�B");

                UpdateMainMessage("�A�C���F�ǂ��������Ȃ񂾁B�{���ɉ����m��˂��B�v���o���������E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E�����B�������A�����B");

                UpdateMainMessage("�A�C���F��������A�_���W�����S�K���猩�n�߂��ςȖ��̘b�A���ĂȂ������ȁB");

                UpdateMainMessage("�A�C���F�����Ă���邩�H���i�A���F���[�B");

                UpdateMainMessage("���i�F��������B");

                UpdateMainMessage("���F���[�F�����A�ǂ����B");

                UpdateMainMessage("�A�C���F�ǂ��ɋ���̂����S�R�n�b�L�����Ȃ����B");

                UpdateMainMessage("�A�C���F�����˂��Ƃ��낾�ȁB�Ƃ��|�c���|�c���Ƃ����āA");

                UpdateMainMessage("�A�C���F���Ƃ́A�ʂĂ��Ȃ��L�����Ƒ������B");

                UpdateMainMessage("�A�C���F��ɂ͐����؂�Ȃ��قǂ̒������ł����B");

                UpdateMainMessage("�A�C���F���͂����ŁA���ɏE�����񂾁B");

                UpdateMainMessage("�A�C���F�m�������E�E�E����́E�E�E");

                UpdateMainMessage("�A�C���F�C�������O���B");

                UpdateMainMessage("�A�C���F�E�E�E�b�O�E�E�E�����E�E�E");

                UpdateMainMessage("���i�F�A�C���E�E�E������������E�E�E�������Ȃ��ŁE�E�E");

                UpdateMainMessage("�A�C���F����ƁE�E�E�������������Ƃ̂���E�E�E�����E�E�E");

                UpdateMainMessage("�A�C���F�E�b�E�E�E�O�A�A�A�@�@�A�A�@�@�A�@�@�@�I�I�I");

                UpdateMainMessage("�A�C���F�A�A�A�A�A�@�@�@�I�I�I");

                UpdateMainMessage("���i�F�A�C���I�ʖځI�������肵�āI�I");

                UpdateMainMessage("���F���[�F�A�C���N�I��������I���v�ł����I�H");

                UpdateMainMessage("���i�F�A�C���I�I�@�ʖځI�I�@��������I�I�I�@�A�C���I�I�I");

                UpdateMainMessage("���i�F�A�C���I�I�I�I");

                UpdateMainMessage("");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�A�C�����C�������Ă���A�T���Ԃ��߂���";
                    md.ShowDialog();
                }

                GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�n���i�̏h���ɂāE�E�E";
                    md.ShowDialog();
                }

                UpdateMainMessage("�n���i�F�A�C���͋x�܂��Ă�������B�������Q������Ƃ����B");

                UpdateMainMessage("���i�F�E�E�E�A�C���E�E�E");

                UpdateMainMessage("�n���i�F�A�C���͂ǂ��܂Œ������񂾂��H");

                UpdateMainMessage("���i�F���̘b�͍ŏ��̈�߂����B");

                UpdateMainMessage("���i�F�ł��r������A�Ђǂ��ꂵ�ݎn�߂āE�E�E");

                UpdateMainMessage("�K���c�F�E�E�E��͂�A��������낤�B");

                UpdateMainMessage("�n���i�F�������疲�̘b���n�߂��񂾂ˁH");

                UpdateMainMessage("���i�F�����A����R�����̃L�b�J�P�ɂȂ�悤�Șb��ŗU���͂������ǁB");

                UpdateMainMessage("���i�F��ċz�������ƁA�m���Ɏ������璝��n�߂Ă�����B");

                UpdateMainMessage("�K���c�F����ł��A�}���ȋ�ɂƎ��_�B");

                UpdateMainMessage("�K���c�F�����炭�A���N�������͂�������񂾂낤�B");

                UpdateMainMessage("���i�F�E�E�E���������ς�ʖڂˁE�E�E");

                UpdateMainMessage("�n���i�F���悵�B���i�����̂�������Ȃ�����ˁB");

                UpdateMainMessage("���i�F�ł��E�E�E�ł��f�ꂳ��A���ǂ͎�����");

                UpdateMainMessage("�n���i�F���v�A���v����B");

                UpdateMainMessage("�n���i�F���i�����́A�悭�������B�債�����̂��B");

                UpdateMainMessage("���i�F������E�E�E���߂�Ȃ����E�E�E");

                UpdateMainMessage("�K���c�F���i��A�A�C���̓��F���[�Ƃ͘b������̂��H");

                UpdateMainMessage("���i�F���F���[����ƃA�C���ł����H�����A�b���Ă��܂���");

                UpdateMainMessage("�K���c�F���F���[�́E�E�E���ƁH");

                UpdateMainMessage("���i�F�A�C�����ғ��P���n�߂����̘b�Ƃ��B");

                UpdateMainMessage("���i�F���ꂩ��A�y���̎O�ʋ��z�̘b���B");

                UpdateMainMessage("���i�F���ꂩ��E�E�E");

                UpdateMainMessage("�K���c�F�ӂށB");

                UpdateMainMessage("���i�F�u�^���ł��v�E�E�E�Ƃ������Ă���B");

                UpdateMainMessage("�K���c�F�Ȃ�ƁI�@����͖{�����ˁH");

                UpdateMainMessage("���i�F�����A�m���ɁB�ł����̂��ƁA�A�C�����������h��������āB");

                UpdateMainMessage("�K���c�F�E�E�E�E�E�E���́E�E�E�E�E�E���F���[���E�E�E");

                UpdateMainMessage("�n���i�F�A�C�����������������n�߂��񂾁B���Ə����̂͂�����B");

                UpdateMainMessage("�K���c�F�E�E�E�E�E�E���ށE�E�E�E�E�E����E�E�E�E�E�E");
                
                UpdateMainMessage("�K���c�F�E�E�E�E�E�E�E�E�E�ӂށE�E�E�E�E�E�E�E�E�m���ɐi��ł�B");
                
                UpdateMainMessage("�K���c�F�E�E�E�����E�E�E�E�E�E�䂦�Ɋ댯������B");

                UpdateMainMessage("�n���i�F�A���^�����悵�B���������܂ŗ��Ă���񂾁B�i�߂邵���Ȃ��񂾂�B");

                UpdateMainMessage("�n���i�F���i�����A�ŉ��w�֍s���Ă��ȁB");

                UpdateMainMessage("���i�F�����E�E�E�ł��A�A�C�������ƋN���Ă���邩����E�E�E");

                UpdateMainMessage("�n���i�F���v����A�Q���͂������萮���Ă���B�����ɂȂ�΂����ƋN�����B");

                UpdateMainMessage("���i�F���A�ŉ��w�ŃA�C���Ɖ���b���΂����̂�����B");

                UpdateMainMessage("�n���i�F�A�b�n�n�n�n�A�������Ă񂾂��B�����ʂ�o�V�[�����Ƃ���Ă��ȁB");

                UpdateMainMessage("���i�F�b�t�t�A�����ł��ˁB������܂����B");

                UpdateMainMessage("�K���c�F���i�N�B");

                UpdateMainMessage("���i�F�͂��B");

                UpdateMainMessage("�K���c�F�A�C���̖��̓��e�͂ǂ�Ȃ��̂ł������H");

                UpdateMainMessage("���i�F�����������ł��E�E�E�C�������O���B");

                UpdateMainMessage("�K���c�F�������A���ɂ����܂ŁB");

                UpdateMainMessage("�K���c�F�E�E�E�E�E�E�ӂށA�\�����B�@���ɂ܂�����B");

                UpdateMainMessage("�K���c�F��X�͌N�B���ŉ��w�Ły�^���̊Ŕz�����鍠���狏�Ȃ��Ȃ�B�������Ă���ˁH");

                UpdateMainMessage("���i�F�͂��B");

                UpdateMainMessage("�K���c�F�A�C���ɂ͐��i���Ȃ����A�ƁB");

                UpdateMainMessage("���i�F�͂��B");

                UpdateMainMessage("�K���c�F���i�A���O�ɂ͖{���ɐh���z���΂�����B���܂Ȃ��B");

                UpdateMainMessage("���i�F����Ȏ�����܂���B�������Ƃ���������S�ŃR�R�ɋ����ł��B");

                UpdateMainMessage("�K���c�F�ӂށA�\���x��ł��ꂩ��ŉ��w�ցB���̎��A���V��ւ̈��A�͕s�v���B");

                UpdateMainMessage("���i�F�͂��A�{���ɂ��肪�Ƃ��������܂��B");

                UpdateMainMessage("�n���i�F���i�����A�����x�ނ񂾁B�ŉ��w�֌����āB");

                UpdateMainMessage("���i�F�����A���肪�Ƃ��������܂��B����ł͂��₷�݂Ȃ����B");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "���i�͎����̕����ւƖ߂��Ă�����";
                    md.ShowDialog();
                }

                UpdateMainMessage("�K���c�F���āA���V�͕���֖߂�Ƃ��邩�B");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "���̎��A�K���c�̎肪�ق�̏��������ɂȂ�h��n�߂Ă����B";
                    md.ShowDialog();
                }

                UpdateMainMessage("�n���i�F�A���^�A�������͊����C������B����ȋC�����Ȃ������H");

                UpdateMainMessage("�K���c�F���O�̂悤�Ɋy�ώ�`�҂ł͂Ȃ��B");

                UpdateMainMessage("�n���i�F�A�b�n�n�n�A���񂽂��{���Ɋ�ł��ˁB");

                UpdateMainMessage("�K���c�F����ł����ʂ�̐���������܂ł�B�����āE�E�E");

                GroundOne.StopDungeonMusic();

                UpdateMainMessage("�@�@�@�@�w�K���c�F���̐��E�Łx");
                
                UpdateMainMessage("�@�@�@�@�w�K���c�F�񑩂��ꂽ�I����Â��Ɍ}���悤�B�x");
                
                UpdateMainMessage("�@�@�@�@�w�K���c�F���񂾂��A�A�C���A���i�B�x");


                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�A�C���̕����ɂāE�E�E";
                    md.ShowDialog();
                }

                // [�x��]�F�^�����E�̕`�ʂ͉��x�����x�����x�����x�����x�����Ȃ��Ă��������B
                UpdateMainMessage("�@�@�@�@�w�A�C���͌ċz����������ԂŖ������Ă���x");

                UpdateMainMessage("�@�i�i�@�E�E�E�@�i���Ƃ炵�j�@�E�E�E�@�i�n�͐V�΂��j�@�E�E�E�@�E�E�E�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@�i�̑�Ȃ�C�j�@�E�E�E�@�E�E�E�@�i�V�ւƊ҂�j�@�E�E�E�@�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@�ǂ������@�E�E�E�@�E�E�E�@���i���O���H�@�E�E�E�@�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@�E�E�E�@�̂���������@�E�E�E�@�A��Ă��Ă�ˁ@�E�E�E�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@�_���W�������H�@�E�E�E�@�E�E�E�@�������Ȃ��@�E�E�E�@�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@�E�E�E�@�E�E�E�@�������@�E�E�E�@�E�E�E�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@�E�E�E�@�t�t�t�@�E�E�E�@�E�E�E�@���肪�ƁA�A�C���@�E�E�E�@�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@�i�̑�Ȃ��j�@�E�E�E�@�E�E�E�@�i���i�Ȃ镃�j�@�E�E�E�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@�i�i�v�j�E�E�E�@�E�E�E�@�i���S���a�ւ̓����j�@�E�E�E�@�E�E�E�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@�i�I���Ȃ����ցj�@�E�E�E�@�E�E�E�@�i�I���Ǝn�܂�j�@�E�E�E�@�E�E�E�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@�E�E�E�@�����Ƃ��@�E�E�E�@�E�E�E�@����@�E�E�E�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@�i���Ȃ��j�@�E�E�E�@�E�E�E�@�i�킽���������ꏊ�ցj�@�E�E�E�@�E�E�E�j�j");

                UpdateMainMessage("�@�i�i�@�E�E�E�@�E�E�E�@�������@�����Ɓ@�����ȁ@�������@�E�E�E�@�E�E�E�@�j�j");

                UpdateMainMessage("�A�C���F�E�E�E�E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E");

                UpdateMainMessage("�A�C���F�E�E�����B");

                UpdateMainMessage("�A�C���F�E�E�E����E�E�E�����炭�E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E���ōŉ��w���B");

                UpdateMainMessage("�A�C���F�����v���o�������Ȃ��̂́E�E�E");

                UpdateMainMessage("�A�C���F���I�ɂ��E�E�E�e�e�e�E�E�E�ʖڂ��B�v���o���˂��B");

                UpdateMainMessage("�A�C���F�n�@�E�E�E�n�@�E�E�E�����E�E�E�����A�������傤�B");

                UpdateMainMessage("�A�C���F���i�̂�A�������Ă���ȗ��K�p�̌��̘b�Ȃ񂩂��B");

                UpdateMainMessage("�A�C���F���F���[�ɂ��Ă������Ɩ��Ȏ���΂��肵�₪���āB");

                UpdateMainMessage("�A�C���F�E�E�E���̓��̌��ɂ́E�E�E����ς�A���������Ă�̂��B");

                UpdateMainMessage("�A�C���F�������A�������ɂ����Ďv���o�����ɂ��v���o���˂��B");

                UpdateMainMessage("�A�C���F�ŉ��w�ŉ�����������̂��H");

                UpdateMainMessage("�A�C���F�E�E�E�ɂ��E�E�E�b�O�E�E�E�ʖڂ��B�Q�邵���˂��ȁB");

                UpdateMainMessage("�A�C���F�E�E�E�E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E���́E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E�E�E�E���i�E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E�E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "���̌�A�A�C���͐[������ɂ����B";
                    md.ShowDialog();
                }

                GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
                this.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "hometown.jpg");
                this.we.CommunicationCompArea4 = true;
                CallRestInn();
            }
            #endregion
            #region "�T�K���e�A�O�ҏI��"
            else if (WE.CompleteSlayBoss5 && WE.CompleteArea5 && WE.TruthEventForLana)
            {
                UpdateMainMessage("�E�E�E�@�E�E�E�@�E�E�E�@�E�E�E�@�E�E�E");

                button4.Visible = false;

                UpdateMainMessage("�E�E�E�@�E�E�E�@�E�E�E�@�E�E�E");

                button1.Visible = false;

                UpdateMainMessage("�E�E�E�@�E�E�E�@�E�E�E");

                button2.Visible = false;

                UpdateMainMessage("�E�E�E�@�E�E�E");

                dayLabel.Visible = false;

                UpdateMainMessage("�E�E�E");

                this.BackColor = Color.Black;
                this.BackgroundImage = null;
                this.Update();

                UpdateMainMessage("�v���f���[�T�F�@���ǁ@�o��");

                UpdateMainMessage("�V�i���I�F�@�ҒJ�@�F�I");

                UpdateMainMessage("��ȁF�@���ǁ@�W���Y");

                UpdateMainMessage("�ҋȁF�@���ǁ@�W���Y");

                UpdateMainMessage("�T�E���h�F�@���ǁ@�o��");

                UpdateMainMessage("�o�g���V�X�e���F�@���ǁ@�o��");

                UpdateMainMessage("�L�����N�^�[�ݒ�F�@�ҒJ�@�F�I / ���ǁ@�o��");

                UpdateMainMessage("�_���W�����f�U�C���F�@���ǁ@�o��");

                UpdateMainMessage("�A�C�e������F�@�΍��@�T��");

                UpdateMainMessage("�v���O�����F�@���ǁ@�o��");

                UpdateMainMessage("�G�O�[�O�e�B�u�E�v���f���[�T�F�@���ǁ@�o��");

                button3.Visible = false;

                UpdateMainMessage("         �`�`�`�@�c�������������@�r�����������@�O�ҁ@�i���j �`�`�`�@�@");

                mainMessage.Visible = false;
                this.Update();

                System.Threading.Thread.Sleep(3000);

                mainMessage.Visible = true;
                this.Update();

                Application.DoEvents();

                UpdateMainMessage("�A�C���F�@�E�E�E�@�b�O�@�E�E�E");

                UpdateMainMessage("�A�C���F�������A�m�����i�͎��񂾂񂾁B");

                UpdateMainMessage("�A�C���F�_���t�F���g�D�[�V�������i�Ɏh�����āE�E�E");

                UpdateMainMessage("�A�C���F�����E�E�E�ォ�������炾�B");

                UpdateMainMessage("�A�C���F�E�E�E�����Ȃ��Ă��B");

                UpdateMainMessage("�A�C���F���x�����E�E�E���́E�E�E����");

                using (YesNoRequest yesno = new YesNoRequest())
                {
                    yesno.StartPosition = FormStartPosition.CenterParent;
                    yesno.ShowDialog();
                    if (yesno.DialogResult == DialogResult.Yes)
                    {
                        UpdateMainMessage("�A�C���F���͊肢��������B");

                        UpdateMainMessage("�A�C���F���i�����Ȃ��͂��˂��B");

                        UpdateMainMessage("�A�C���F�K���H���~�߂Č�����B");

                        UpdateMainMessage("�@�@�@�������@�A�C���͐S�ɋ����������B���i�̎����~�߂�ƁB�@������");

                        this.BackColor = Color.White;
                        this.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "hometown.jpg");

                        GroundOne.PlayDungeonMusic(Database.BGM04, Database.BGM04LoopBegin);

                        UpdateMainMessage("�@�@�@�������@�c�������������@�r�����������i��җ\���j ������");
                        
                        UpdateMainMessage("      �������@�A�C���ƃ��i�ɋN�����o���������X�Ɩ��炩�ɂȂ�B�@������");

                        UpdateMainMessage("�@�@�@�w�A�C���F�������āH���O���Ȃ�����H�x");

                        UpdateMainMessage("�@�@�@�w���i�F�����炳�A�A�C���A�����F���[����ƒm�荇�����̂�H�x");

                        UpdateMainMessage("�@�@�@�w�A�C���F�_���W�����R�K�̂͂����E�E�E����A�����ƑO���Ď����H�x");

                        UpdateMainMessage("�@�@�@�w���i�F�n�߂ĉ�������̎��A�v���o���Ă�B�����ƁB�x");

                        UpdateMainMessage("�@�@�@�w�A�C���F�莆���m���Ɏ󂯂Ƃ����񂾂�A����Ȋ����̂��x");
                        
                        UpdateMainMessage("      �w�@�y�_���W�����Q�[�g������̗��ő҂B�@�` �u�E�` �`�@�z�@�@");

                        UpdateMainMessage("      �w���i�F������ăA�C�����h���ŏE�����莆�������H�H");

                        UpdateMainMessage("�@�@�@�w�A�C���F����E�E�Ⴄ�B�m�����̎莆��");

                        UpdateMainMessage("�@�@�@�������@�^����������n�߂��A�C���́A���ɂɑς��Ȃ��玟�X�Ǝv���o���@������");

                        UpdateMainMessage("�@�@�@�w�A�C���F�������A�����������͐_���t�F���g�D�[�V���������Ă��͂����B�x");

                        UpdateMainMessage("�@�@�@�w�A�C���F�����ǂ����ŁA���������܂��Ă�E�E�E�x");

                        UpdateMainMessage("�@�@�@�w���F���[�F�A�C���N�A�悭�v���o���Ă��������B�d�v�Ȏ肪����̂͂��ł��B�x");

                        UpdateMainMessage("�@�@�@�w�A�C���F�E�E�E����A�Ⴄ�B�ǂ����ŁE�E�E�x");

                        UpdateMainMessage("�@�@�@�w�A�C���F�E�E�E���܂ꂽ�񂾁B�x");

                        UpdateMainMessage("�@�@�@�w���F���[�F����́A�N�ɓ��܂ꂽ��ł����H�x");

                        UpdateMainMessage("�@�@�@�w�A�C���F�������Ă�΋�J�͂��˂��񂾂��E�E�E�Q�Ă��킯����Ȃ��񂾁B�x");

                        UpdateMainMessage("�@�@�@�w�A�C���F������̕����ʂ�߂����悤�ȁE�E�E����ȋC���������͊���");

                        UpdateMainMessage("�@�@�@�������@�e�|�C���g�ł̕������������X�Ɩ\����n�߂�I�@������");

                        UpdateMainMessage("      �w�A�C���F�S�R���������������ǂȁB�s�v�c�Ɛ̂���m���Ă�悤�Ȋ������������B�������Y�킾�����B");

                        UpdateMainMessage("�@�@�@�w���i�F���`��E�E�E�����������̎��A�m���Ă���B�x");

                        UpdateMainMessage("      �w�A�C���F�}�W����I�H�x");

                        UpdateMainMessage("�@�@�@�w���i�F���̕ꂳ�񂩂�悭��������Ă�����B�m���^�C�g�����E�E�E�x");

                        UpdateMainMessage("�@�@�@�������@�`����FiveSeeker���F���[�ƃA�C���̊֌W�����炩�ɂȂ�n�߂�I�@������");

                        UpdateMainMessage("      �w���i�F���F���[����A�܂��t�@���l�Ƃ��̂��b�A�h���ŕ������Ă��������ˁ�x");

                        UpdateMainMessage("      �w���F���[�F�E�E�E���i����A�������肢���܂��B�x");

                        UpdateMainMessage("      �w���i�F���H���A�����E�E�E���ꂶ��x");

                        UpdateMainMessage("      �w�A�C���F���F���[�A�҂Ă�B�h���E�E�E���Ă�����ȁH�x");

                        UpdateMainMessage("      �w���F���[�F�E�E�E����͏o���܂���ˁB�x");

                        UpdateMainMessage("      �w�A�C���F�ǂ��������Ƃ���H�x");

                        UpdateMainMessage("      �w���F���[�F����̓A�C���N�A���Ȃ��ƃ{�N�̊֌W�ɋN�����Ă��܂��B�x");

                        UpdateMainMessage("      �w�A�C���F���ƃ��F���[�H�x");

                        UpdateMainMessage("      �w���F���[�F�A�C���N�A�ǂ����Ă��m�肽����΁A�����̖�_���W�����Q�[�g���܂ŗ��Ă��������B�x");

                        UpdateMainMessage("      �w���F���[�F���i����͗��Ă͂����܂���B�A�C���N��l�ŗ��Ă��������B�x");

                        UpdateMainMessage("�@�@�@�������@���i�Ɣ��̎O�ʋ��Ɋւ��鎖������������ɂȂ�I�@������");

                        UpdateMainMessage("      �w���i�F�����A�A���̎��H����͂ˁB�x");

                        UpdateMainMessage("�@�@�@�w���F���[�F���̎O�ʋ��ł��ˁB�x");

                        UpdateMainMessage("�@�@�@�w���i�F�����A���̋��ɉ��x���G��Ă���E�`�ɕ������Ă��Ă��́B�x");

                        UpdateMainMessage("�@�@�@�w���i�F�N���ƒN���������Ă���̂�B���͂���������猩�Ă�́B�x");

                        UpdateMainMessage("�@�@�@�w�A�C���F�N���ƒN�����ĒN����H�x");

                        UpdateMainMessage("�@�@�@�w���i�F���`��A������Ƒ҂��Ă�ˁB�x");

                        UpdateMainMessage("�@�@�@�w���i�F���A�v���o������E�E�E���ǁA������āE�E�E�H�H�H�x");

                        UpdateMainMessage("�@�@�@�������@�`����FiveSeeker�ł���A�t���ł�����I���E�����f�B�X���Q�킷��I�@������");

                        UpdateMainMessage("      �w�A�C���F�����E�E�E����Ȃ񏟂Ă�킯�˂�����B�ǂ������E�E�E�x");

                        UpdateMainMessage("      �w�����f�B�X�F�I���I���A�シ���񂾂�A�U�R�A�C���I�I���I���A�A�@�@�A�I�I�I�I�x");

                        UpdateMainMessage("      �w���i�F�A�C���܂�Duel�ŕ����Ă��ˁE�E�E����Ꮯ�Ă����ɂȂ���B�b�t�t�A�K���o����x");

                        UpdateMainMessage("      �w�����f�B�X�F�����˂��I�I�[�����˂��I�e�C���˂��I�シ���񂾂�e���F�I�I�x");

                        UpdateMainMessage("      �w�A�C���F�������I�^�ʖڂɂ���Ă邾�낤���I�I�x");

                        UpdateMainMessage("      �w�@�K�L�B�B�B�I�I�I�i������������I�j�x");

                        UpdateMainMessage("      �w�����f�B�X�F�^�ʖڂ��Ă��I�b�n�b�n�b�n�b�n�@�I����Ȃ�ŏ��Ă�Ǝv���Ă�̂����I�I���A�@�@�I�I�I�x");

                        UpdateMainMessage("      �w�A�C���F�Q�z�E�E�E�I�b�O�A���������E�E�E�I�Ă߂����̃O���[�u�A�O����B�ڋ������B�x");

                        UpdateMainMessage("      �w�����f�B�X�F����̂�������I������e���F�͎ア���Ă񂾁I�I�����������V���Ă�����I���@�I�I�I�x");

                        UpdateMainMessage("      �w���i�F�ǂ����A�o�J�A�C���B�@�K���o����@�K���o����x");

                        UpdateMainMessage("      �w�A�C���F�����E�E�E�������������I�@������������I�I�I�x");

                        UpdateMainMessage("�@�@�@�������@�_���W�����̍\���ɕω��Ɗg�����N����I�@������");

                        UpdateMainMessage("�@�@�@�������@�V�퓬�V�X�e���w�X�^�b�N�E�C���E�U�E�R�}���h�x�𓱓��@�I�I�@������");

                        UpdateMainMessage("�@�@�@�������@���z���E�Ɛ^�����E�̃~�b�V���O�����N�֌W�����炩�ɁI�@������");

                        UpdateMainMessage("�@�@�@�������@�c�������������@�r�����������i��ҁj�@�֑���������");

                        we.EnterSecondGame = true;

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
                        //button1.Visible = true;
                        //button2.Visible = true;
                        //button3.Visible = true;
                        //button4.Visible = true;
                        //dayLabel.Visible = true;
                        Application.Exit();

                    }
                    else
                    {
                        Application.Exit();
                    }
                }

            }
            #endregion


        }

        /// <summary>
        /// if-else���ƃt���O�𕡎G�������ăn���i�f�ꂿ���Ƃ̉�b�𐷂�グ�Ă��������B
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            #region "�P����"
            if (this.firstDay >= 1 && !we.CommunicationHanna1 && mc.Level >= 1 && knownTileInfo[2])
            {
                if (!we.AlreadyRest)
                {
                    mainMessage.Text = "�A�C���F�ӂ��A��ꂽ�E�E�E�n���i���΂����A�����͋󂢂Ă�H";
                    ok.ShowDialog();
                    mainMessage.Text = "�n���i�F�����A�󂢂Ă��B���܂��Ă����Ȃ����B";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F�T���L���[�I���Ⴀ�オ�点�Ă��炤��B";
                    ok.ShowDialog();
                    mainMessage.Text = "�n���i�F���������A��������B�A���^���ꂩ��_���W�����T�����񂾂��āH";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F�Ő[���܂Ő�΂ɒH����Ă�邺�I�b�n�b�n�b�n�I";
                    ok.ShowDialog();
                    mainMessage.Text = "�n���i�F�Ⴂ���Ă̂͗ǂ��˂��B���̏��A�Ő[�����B�҂͐����邮�炢�ˁB";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F�������킷��B�����Ă��̍Ő[�����B�҂̒��Ɋ܂߂Ă��炤���B";
                    ok.ShowDialog();
                    mainMessage.Text = "�n���i�F�_���W��������肭�i�߂邽�߂ɂ́A�܂������̑̒��𐮂��邱�ƁB";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F�����B�����͎v��������x�ނƂ��邳�B";
                    ok.ShowDialog();
                    mainMessage.Text = "�n���i�F�����A�������Ƌx��ł����Ȃ����B";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F�T���L���[�B";
                    ok.ShowDialog();
                    CallRestInn();
                }
                else
                {
                    mainMessage.Text = "�n���i�F����������B�����A�n�߂��̐S������ˁB�����Ă�����Ⴂ�B";
                }
            }
            #endregion
            #region "�Q����"
            else if (this.firstDay == 2)
            {
                if (!we.AlreadyRest)
                {
                    mainMessage.Text = "�A�C���F���΂����B�󂢂Ă�H";
                    ok.ShowDialog();
                    mainMessage.Text = "�n���i�F�󂢂Ă��B���܂��Ă������H";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F�ǂ��������ȁE�E�E���������͔��܂邩�H";
                    using (YesNoRequestMini yesno = new YesNoRequestMini())
                    {
                        yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                        yesno.ShowDialog();
                        if (yesno.DialogResult == DialogResult.Yes)
                        {
                            mainMessage.Text = "�n���i�F�͂���A�ꖼ�l���ē����ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�T���L���[�A���΂����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���΂����A��������Ő[�����B�҂��Ăǂ̂��炢����񂾂��H";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�T�l���ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�b�Q�A�T�l������̂���I";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F���������邩���H�ł��������Ⴂ���Ȃ���B���܂ł̒��킵���҂̐���";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�ǂ̂��炢����񂾁H";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�����m�����ł��A�P�O�O���l���x���ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�}�W����E�E�E�قƂ�ǑS�ł���˂����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�T�l�ɂ��Ă͒m���Ă邩���H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F������A�m��˂��ȁB�X�Q�F�̂��H";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�����A���x�܂��������Ă������B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����A�y���݂ɂ��Ă邺�I";
                            ok.ShowDialog();
                            CallRestInn();
                        }
                        else
                        {
                            mainMessage.Text = "�A�C���F���߂�B�܂��p������񂾁A��ł����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F���ł�����Ă�����Ⴂ�B�����͋󂯂Ă�������ˁB";
                        }
                    }
                }
                else
                {
                    mainMessage.Text = "�n���i�F����������B�����A�n�߂��̐S������ˁB�����Ă�����Ⴂ�B";
                }
            }
            #endregion
            #region "�R����"
            else if (this.firstDay == 3)
            {
                if (!we.AlreadyRest)
                {
                    mainMessage.Text = "�A�C���F���΂����B�󂢂Ă�H";
                    ok.ShowDialog();
                    mainMessage.Text = "�n���i�F�󂢂Ă��B���܂��Ă������H";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F�ǂ��������ȁE�E�E���������͔��܂邩�H";
                    using (YesNoRequestMini yesno = new YesNoRequestMini())
                    {
                        yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                        yesno.ShowDialog();
                        if (yesno.DialogResult == DialogResult.Yes)
                        {
                            mainMessage.Text = "�n���i�F�͂���A�ꖼ�l���ē����ˁB";
                            ok.ShowDialog();
                            // [�x��]�F�Q���ډ�b���Ă邩�ǂ����ŕ��򂳂��Ă��������B
                            mainMessage.Text = "�A�C���F�T���L���[�A���΂����B���B�҂T�l�ɂ��Ă������������Ă����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F������ӂł́A�ނ�Ɍh�ӂ�\���āA�y-- FiveSeeker --�z�ƌĂ΂�Ă����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�܂���l�ڂ́y�G���~�E�W�����W���z";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���ȁE�E�E�}�W����I�I�E�E�E�}�W����I�I�I";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�A�b�n�n�n�A�����������H�����A��X�̍������߂Ă鍑���l������ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�V���b�N�B�������������ȁB���₠�A�������ւ�Ɏv����ȁI";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�W�����W���l�͏�ɑS�\�͂��オ��y�t�@�[�W�����Ƃ̍���z�𑕔����Ă����炵���ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�u�����E�t�F�A�E�Γ��v���d�񂶂Đ퓬����l�͒N�����Ă����ꍛ�ꂵ����������B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����ƃt�F�A�E�E�E���B���ɂ͏o���Ȃ��|�������ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�������Ă񂾂��B�A���^�����đf���͂����B�ق疾��������撣��ȁI";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����A���΂����ɗ�܂����Ɖ��ƂȂ����C�ɂȂ邺�B�T���L���[";
                            ok.ShowDialog();
                            CallRestInn();
                        }
                        else
                        {
                            mainMessage.Text = "�A�C���F���߂�B�܂��p������񂾁A��ł����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F���ł�����Ă�����Ⴂ�B�����͋󂯂Ă�������ˁB";
                        }
                    }
                }
                else
                {
                    mainMessage.Text = "�n���i�F����������B�������撣���Ă�����Ⴂ�B";
                }
            }
            #endregion
            #region "�S����"
            else if (this.firstDay == 4)
            {
                if (!we.AlreadyRest)
                {
                    mainMessage.Text = "�A�C���F���΂����B�󂢂Ă�H";
                    ok.ShowDialog();
                    mainMessage.Text = "�n���i�F�󂢂Ă��B���܂��Ă������H";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F�ǂ��������ȁE�E�E���������͔��܂邩�H";
                    using (YesNoRequestMini yesno = new YesNoRequestMini())
                    {
                        yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                        yesno.ShowDialog();
                        if (yesno.DialogResult == DialogResult.Yes)
                        {
                            mainMessage.Text = "�n���i�F�͂���A�ꖼ�l���ē����ˁB";
                            ok.ShowDialog();
                            // [�x��]�F�Q���ځA�R���ډ�b���Ă邩�ǂ����ŕ��򂳂��Ă��������B
                            mainMessage.Text = "�A�C���F�T���L���[�A���΂����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�܂����T�l���̂P�l�������Ƃ͂ȁB���Ⴀ���B�҂T�l�̂Q�l�ڂ͒N�Ȃ񂾁H";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�₾��A�܂��r�b�N���������ራ���񂾂��ǂˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���������A�܂����E�E�E";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F��l�ڂ́y�t�@���E�t���[���z�@�������̗ǂ��ȂˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�V���b�N��ʂ�z�����C������B";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�t�@���l�͂���͂����V�g�̗l�ȕ��ł˂��E�E�E�����̃A�^�V��ł������Ƃ肷�邮�炢��������B";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�����������ł���y�V�g�̃y���_���g�z���������ɒ����Ă����ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F����́y�_�̎���Y�z�ƌĂ΂�Ă���㕨�炵���ˁB�ʏ�̐l�ł͌��ʂ��������Ȃ��������B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�ʏ���āE�E�E�ǂ����������ʂ��������Ă��񂾁H";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�@�����������ȋF�����ɐS�ɔ�߂��҂ɂ̂݋������A�_�̎��߁�����";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�ǂ����r�͂���Ȃ��������ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F���������t�@���l�͏����A�A���^�͒j���A���{�I�ɈႤ���Ď����ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�����A�����͂����܂ł���B�������撣���Ă��ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���B�҂��Ă��肦�˂��ȃ}�W�E�E�E�����͋x�܂��Ă��炤��B�T���L���[�B";
                            ok.ShowDialog();
                            CallRestInn();
                        }
                        else
                        {
                            mainMessage.Text = "�A�C���F���߂�B�܂��p������񂾁A��ł����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F���ł�����Ă�����Ⴂ�B�����͋󂯂Ă�������ˁB";
                        }
                    }
                }
                else
                {
                    mainMessage.Text = "�n���i�F����������B�������撣���Ă�����Ⴂ�B";
                }
            }  
            #endregion
            #region "�T����"
            else if (this.firstDay == 5)
            {
                if (!we.AlreadyRest)
                {
                    mainMessage.Text = "�A�C���F���΂����B�󂢂Ă�H";
                    ok.ShowDialog();
                    mainMessage.Text = "�n���i�F�󂢂Ă��B���܂��Ă������H";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F�ǂ��������ȁE�E�E���������͔��܂邩�H";
                    using (YesNoRequestMini yesno = new YesNoRequestMini())
                    {
                        yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                        yesno.ShowDialog();
                        if (yesno.DialogResult == DialogResult.Yes)
                        {
                            mainMessage.Text = "�n���i�F�͂���A�ꖼ�l���ē����ˁB";
                            ok.ShowDialog();
                            // [�x��]�F�Q���ځA�R���ځA�S���ډ�b���Ă邩�ǂ����ŕ��򂳂��Ă��������B
                            mainMessage.Text = "�A�C���F�T���L���[�A���΂����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�Ȃ��R�l�ڂɂ��Ă����A������Ƒ҂��Ă���B";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F���₨��A�|�C�Â��������H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F��������˂��A��������������ςȂ�������ȁB�悵�A�����Ă���B�R�l�ڂ́H";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�O�l�ڂ́y�V�j�L�A�E�J�[���n���c�z";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����ō��N���X�A�`���̃J�[���݂���˂����B����Ȏ����낤�Ǝv�������B";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�J�[���݂͖����������i����E�ŁE�΁E���E���E����}�X�^�[�B";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�ዅ�������y�����f�r���A�C�z�Œ��_�E��_�E�C���E�͗ʁE�єz�E�ˊo�E�V�^�����ʂ��B";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�ނ܂�Ȃ�u���@�E�m�b�E���f�v�ŕS���S���B�ɂ܂ꂽ�҂͂Ђꕚ���Ă����񂾂�B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����\�����͕��������Ƃ���B�l�Ԃ̗̈�𒴂��Ă邺�S���B";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F���͉B��ƂŌ��������ǁA�����W�����W���l�̗ǂ����C�o�����������ď����ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�W�����W���l�ƃJ�[���݁A�ǂ��������������񂾂��H";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F��Ɍ݊p�������ƁA����Ă�˂��B";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�����W�����W���l�͐��X���X�Ƃ������Ă�B�w��_�͌�������Ă�����_�x�Ƃ����Ƃ��ŁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F����A�J�[���݂͓��@�������Łw��_���炯�B�ꂪ�m��ʁx�Ƃ����Ƃ��ŁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F���݂����荇�����ǂ��������t�����d�������Ă킯���B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�悭�킩��˂��ȁB�����Ă�Ӗ����s�����ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�A�C���A���񂽂ɂ���������������Ɨ����B�����A�����͂����x�ނ񂾂ˁB";
                            ok.ShowDialog();
                            CallRestInn();
                        }
                        else
                        {
                            mainMessage.Text = "�A�C���F���߂�B�܂��p������񂾁A��ł����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F���ł�����Ă�����Ⴂ�B�����͋󂯂Ă�������ˁB";
                        }
                    }
                }
                else
                {
                    mainMessage.Text = "�n���i�F����������B�������撣���Ă�����Ⴂ�B";
                }
            }
            #endregion
            #region "�U����"
            else if (this.firstDay == 6)
            {
                if (!we.AlreadyRest)
                {
                    mainMessage.Text = "�A�C���F���΂����B�󂢂Ă�H";
                    ok.ShowDialog();
                    mainMessage.Text = "�n���i�F�󂢂Ă��B���܂��Ă������H";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F�ǂ��������ȁE�E�E���������͔��܂邩�H";
                    using (YesNoRequestMini yesno = new YesNoRequestMini())
                    {
                        yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                        yesno.ShowDialog();
                        if (yesno.DialogResult == DialogResult.Yes)
                        {
                            mainMessage.Text = "�n���i�F�͂���A�ꖼ�l���ē����ˁB";
                            ok.ShowDialog();
                            // [�x��]�F�Q���ځA�R���ځA�S���ځA�T���ډ�b���Ă邩�ǂ����ŕ��򂳂��Ă��������B
                            mainMessage.Text = "�A�C���F�T���L���[�A���΂����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���悢��S�l�ڂ��B�������ł��������ė����B�b�n�b�n�b�n�I";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�������Ă��₵�Ȃ���A�����ƂS�l�ڂ������ˁB���Ⴂ����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���������I�b�n�b�n�b�n�b�n�I";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�l�l�ڂ́y�I���E�����f�B�X�z";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�n�[�[�[�[�b�n�b�n�b�n�b�n�I�I�I���̎t������˂����I�I�I�I";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�ʏ̃I�[�����E���h�f�X�B�S�Ă��Ă��E����҂Ƃ��Ė����ʂ����ŋ��l���B";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�S�Ă������ɏĂ��E�����y���_�O���[�u�z�B�����܁A�A�C���ɂƂ��ẮB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�j��Œ�̃O���[�u���ȁB�v���N���������ł��E�E�E";
                            ok.ShowDialog();
                            mainMessage.Text = "�����f�B�X�F�w�����؂񎀂�ł�����U�R�����������[�I�I�I���@�@�@�@�@�@�@�@�@�I�I�x";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�E�E�E���R�Ȃ��\���B����ŉ��x�D�ɂ�����ꂽ�����B����͊��S�Ɉ��̉��g����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�A�b�n�n�n�A���������˂��A�ł�����Ȍ��������Ȃ񂾂��ǁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F������y�_�̎���Y�z�̂����̈����B���Ƃ͂�����ƈႤ�Ǝv���˂��B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����ƁH���Ƃ�����ʏ킶���͂�P�Ȃ�O���[�u�Ȃ̂��H";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�����������ɂȂ�˂��B";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�@���������o�M�O��S�Ɍg�����҂ɂ̂݋������A�_�̐���������";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�ԈႢ�Ȃ��r�͂��ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�������˂��A�ł����̐��������ǁA�M�O���h���ĂȂ��Ƒʖڂ��낤�ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����A�������肽����������˂����B��Ȃ���A�ǂ����M�O�Ȃ񂾁B";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�A���^�ق�Ƃɗǂ��t���ɉ��������Ȃ����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������ςȂ�����A�����g�������Ȃ��ˁB�����A�C�c�͉�����̎�Ń��x���W���I";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�z���g�ǂ��M�O�����Ă邶��Ȃ��B�����A�����M�b�^�M�^�ɓ|���Ă��ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F��������A�����ƌ��܂�΂܂��͋x�����B���΂���񂠂肪�Ƃ��I";
                            ok.ShowDialog();
                            CallRestInn();
                        }
                        else
                        {
                            mainMessage.Text = "�A�C���F���߂�B�܂��p������񂾁A��ł����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F���ł�����Ă�����Ⴂ�B�����͋󂯂Ă�������ˁB";
                        }
                    }
                }
                else
                {
                    mainMessage.Text = "�n���i�F����������B�������撣���Ă�����Ⴂ�B";
                }
            }  
            #endregion
            #region "�V����"
            else if (this.firstDay == 7)
            {
                if (!we.AlreadyRest)
                {
                    mainMessage.Text = "�A�C���F���΂����B�󂢂Ă�H";
                    ok.ShowDialog();
                    mainMessage.Text = "�n���i�F�󂢂Ă��B���܂��Ă������H";
                    ok.ShowDialog();
                    mainMessage.Text = "�A�C���F�ǂ��������ȁE�E�E���������͔��܂邩�H";
                    using (YesNoRequestMini yesno = new YesNoRequestMini())
                    {
                        yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                        yesno.ShowDialog();
                        if (yesno.DialogResult == DialogResult.Yes)
                        {
                            mainMessage.Text = "�n���i�F�͂���A�ꖼ�l���ē����ˁB";
                            ok.ShowDialog();
                            // [�x��]�F�Q���ځA�R���ځA�S���ځA�T���ډ�b���Ă邩�ǂ����ŕ��򂳂��Ă��������B
                            mainMessage.Text = "�A�C���F�T���L���[�A���΂����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F����ōŌ�̂T�l�ڂ��Ď��ɂȂ�ȁB�����L���Ȑl���͋��Ȃ��Ǝv�����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�������낤�ˁB�����T�l�ڂɊւ��ẮB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�Ȃ񂾁A�ǂ������Ӗ����H�����";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�܂���ɖ��O����������ˁB�悭�o���Ă����񂾂�B";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�ܐl�ڂ́y���F���[�E�A�[�e�B�z";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�N�Ȃ񂾃\�C�c�́H�S�R�����������Ȃ����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�p�������̑��݂����A�p�߂炦�����̑��݂����A�p���������̑��݂����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�y�V��̗��z���܂Ƃ����҂̏؂Ƃ��āA�_�̑��x�A�c�����s�ɓV�n���삯����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�ǂ����������A�{���ɑ��݂���̂��H����ȃ��c�B";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F����炵���ˁB�������ǎ����g�������Ǝp�������킯����Ȃ��񂾂�B";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�������͍����l�̏d�b�Ƃ��Č��݂Ƃ̎��B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F��k����I�H���������˂����A����ȃ��c�B���āA�܂��W�����W���l�̉��̐l�Ȃ񂾂ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F���ۂɍ����l�������͍����̔���_�����҂�";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�m���E�E�E�w���̓��ɉ����̊O�Ŕ���ŋC��B�{�l���o�܂�S���o���ĂȂ��B�x�������ȁE�E�E";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�Ƃ���ł��́y�V��̗��z�B������y�_�̎���Y�z���ă��P���H";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�������ˁB���������`�����Ă����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�@�������v�l�E�_����遂���̂āA���R�S��ۂ҂ɂ̂݋������A�_�̖���������";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�v�l�Ƙ_�����̂Ă�E�E�E�r�͂���I";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F���i����񂩂畷���Ă邯�ǁE�E�E�A�C���A���񂽂͖{���Ƀo�J���˂��B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�b�n�b�n�b�n�b�n�I";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�܂�����łT�l�S������B�ǂ������H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����f�B�̃{�P�͒u���Ƃ��Ă��ȁB�A�C�c�ȊO�͑S�����������c��΂��肾�ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F�A�C���A�����A���^���Ő[�w�܂œ��B����悤�撣���Ă��ȁB�������Ă��B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����ȏ��A���͂���Ȃɓ`�����݂��\�͂��������˂��B��������Ă�邺�I";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F���̑����B���Ⴀ�����͂������x�݂Ȃ����B�����Ė����͂���񂶂�Ȃ���B";
                            ok.ShowDialog();
                            CallRestInn();
                        }
                        else
                        {
                            mainMessage.Text = "�A�C���F���߂�B�܂��p������񂾁A��ł����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�n���i�F���ł�����Ă�����Ⴂ�B�����͋󂯂Ă�������ˁB";
                        }
                    }
                }
                else
                {
                    mainMessage.Text = "�n���i�F����������B�������撣���Ă�����Ⴂ�B";
                }
            }  
            #endregion
            #region "�S�K���e��A�T�K�Ŕ��B��A�n���i�̂������h���X"
            else if (we.CommunicationCompArea4 & we.TruthWord5 && !we.CommunicationHanna100)
            {
                we.CommunicationHanna100 = true;
                if (!we.AlreadyRest)
                {
                    UpdateMainMessage("�A�C���F���΂����A�󂢂Ă�H");

                    UpdateMainMessage("�A�C���F����A�����K���[���Ƃ��Ă�ȁB���΂���񋏂Ȃ��̂���H");

                    UpdateMainMessage("���i�F�A�C���A�J�E���^�[�Ȃɉ������u����������B�ǂނ�ˁB");

                    UpdateMainMessage("�@�@�@�w�K���c�̕���X�ɂ��A�n���i�̂������h�����ꎞ�X�Ƃ���B�x");

                    UpdateMainMessage("�A�C���F���������A�ǂ��Ȃ��Ă񂾂�I�H�h�����X���Ď��͋x�߂˂�����Ȃ����I");

                    if (!we.CommunicationGanz100)
                    {
                        UpdateMainMessage("�A�C���F���₢��A�K���c�f������̕�����X���Ă�̂���I�H�ǂ��Ȃ��Ă񂾁I");
                    }

                    UpdateMainMessage("���i�F�҂��ăA�C���A���̉��ɑ����������Ă����B");

                    UpdateMainMessage("�@�@�@�w�Еt���A�������ځA��ꕨ�����āA�����Ǝ��̐l���g����悤�ɂ��Ƃ��񂾂�B�yHanna.Gimerga�z");

                    UpdateMainMessage("�A�C���F�Еt���Ƃ������ł��Ύg���Ă��ǂ����Ď����H");

                    UpdateMainMessage("���i�F�b�t�t�A���΂���炵���������ˁB");

                    UpdateMainMessage("�A�C���F�܂������Ƃ��΂����ɂ͐��b�ɂȂ���ςȂ�������ȁB�����B�ł��̂��炢�͂�邩�I");

                    UpdateMainMessage("���i�F�����ˁA������Ԃ͂����邯�ǁA���΂��񂪂�������Ă����A��������ɂ͒��x�ǂ���B");

                    UpdateMainMessage("�A�C���F�����A�������ȁB�K���c�f������A�n���i���΂���񂪋A���Ă���܂Œ��J�ɂ��Ƃ��Ȃ��ƂȁB");

                    UpdateMainMessage("���i�F�L�b�`���E�H���E�Q���E������胋�[���A�����ȏ��ɏ��u���������E�E�E");

                    UpdateMainMessage("�@�@�@�w�z�������A�H�ו����ڂ��񂶂�Ȃ���x");

                    UpdateMainMessage("�@�@�@�w�W���[�X�U�炩�����ςȂ��ŕԂ�����_������x");

                    UpdateMainMessage("�@�@�@�w�Q��O�́A�����ƖY�ꕨ���Ȃ��悤�ɉו��������Ƃ��ȁB�x");

                    UpdateMainMessage("�@�@�@�w�P���J������͈֎q�ƃe�[�u���̕��т��炢�����Ƃ��ȁB�x");

                    UpdateMainMessage("�A�C���F�����A�������ʂ��ȁB�A�`�R�`�ɓ\���Ă���E�E�E");

                    UpdateMainMessage("���i�F���΂��񂪋���Ǝv���Ďg���Ηǂ��̂�B");

                    UpdateMainMessage("�A�C���F������������ȁB�������΂���񂪋���݂����Ȋ���������ȁA�b�n�b�n�b�n�I");

                    UpdateMainMessage("���i�F�厖�Ɏg�킹�Ă��炢�܂���B����A�A�C�����₷�݂Ȃ����B");

                    UpdateMainMessage("�A�C���F�����A���₷�݂��B");

                    CallRestInn();
                }
                else
                {
                    mainMessage.Text = "�A�C���F���Ⴀ�A�s���Ă��邺�B�n���i���΂����B";
                } 
            }
            #endregion
            #region "�w����"
            else
            {
                if (!we.CommunicationHanna100)
                {
                    if (!we.AlreadyRest)
                    {
                        mainMessage.Text = "�A�C���F���΂����B�󂢂Ă�H";
                        ok.ShowDialog();
                        mainMessage.Text = "�n���i�F�󂢂Ă��B���܂��Ă������H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�ǂ��������ȁE�E�E���܂邩�H";
                        using (YesNoRequestMini yesno = new YesNoRequestMini())
                        {
                            yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                            yesno.ShowDialog();
                            if (yesno.DialogResult == DialogResult.Yes)
                            {
                                mainMessage.Text = "�n���i�F�͂���A�����͋󂢂Ă��B�������Ƌx�݂ȁB";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F�T���L���[�A���΂����B";
                                ok.ShowDialog();
                                CallRestInn();
                            }
                            else
                            {
                                mainMessage.Text = "�A�C���F���߂�B�܂��p������񂾁A��ł����B";
                                ok.ShowDialog();
                                mainMessage.Text = "�n���i�F���ł�����Ă�����Ⴂ�B�����͋󂯂Ă�������ˁB";
                            }
                        }
                    }
                    else
                    {
                        mainMessage.Text = "�n���i�F����������B�������撣���Ă�����Ⴂ�B";
                    }
                }
                else
                {
                    if (!we.AlreadyRest)
                    {
                        UpdateMainMessage("�A�C���F�h���A�g�킹�Ă��炤�Ƃ��邩�H", true);

                        using (YesNoRequestMini yesno = new YesNoRequestMini())
                        {
                            yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                            yesno.ShowDialog();
                            if (yesno.DialogResult == System.Windows.Forms.DialogResult.Yes)
                            {
                                UpdateMainMessage("���i�F���΂��񂪋A���Ă���܂ŁA�������Y��ɂ��Ă����܂���B");

                                UpdateMainMessage("�A�C���F�����A�������ȁB���Ⴀ���₷�݂��B���i�B");

                                UpdateMainMessage("���i�F�����A���₷�݂Ȃ����B");
                                CallRestInn();
                            }
                            else
                            {
                                UpdateMainMessage("", true);
                            }
                        }
                    }
                    else
                    {
                        mainMessage.Text = "�A�C���F���Ⴀ�A�s���Ă��邺�B�n���i���΂����B";
                    }
                }
            }
            #endregion
        }

        private void CallRestInn()
        {
            GroundOne.PlaySoundEffect("RestInn.mp3");
            using (MessageDisplay md = new MessageDisplay())
            {
                md.Message = "�x�����Ƃ�܂���";
                md.StartPosition = FormStartPosition.CenterParent;
                md.ShowDialog();
            }

            we.AlreadyRest = true;
            // [�x��]�F�I�u�W�F�N�g�̎Q�Ƃ��S�Ă̏ꍇ�A�N���X�Ƀ��\�b�h��p�ӂ��Ă�����R�[���������������B
            if (mc != null)
            {
                mc.CurrentLife = mc.MaxLife;
                mc.CurrentSkillPoint = mc.MaxSkillPoint;
                mc.CurrentMana = mc.MaxMana;
            }
            if (sc != null)
            {
                sc.CurrentLife = sc.MaxLife;
                sc.CurrentSkillPoint = sc.MaxSkillPoint;
                sc.CurrentMana = sc.MaxMana;
            }
            if (tc != null)
            {
                tc.CurrentLife = tc.MaxLife;
                tc.CurrentSkillPoint = tc.MaxSkillPoint;
                tc.CurrentMana = tc.MaxMana;
            }
            we.AlreadyUseSyperSaintWater = false;
            we.AlreadyUseRevivePotion = false;
            we.AlreadyUsePureWater = false; // ��Ғǉ�
            this.we.GameDay += 1;
            dayLabel.Text = we.GameDay.ToString() + "����";
        }

        /// <summary>
        /// �_���W��������o���΂���̏ꍇ�A��x�e�A�x�񂾌�̓_���W�����ֈړ����܂��B
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //bool result = EncountBattle("���F���[�E�A�[�e�B");
            //return;

            if (!we.AlreadyRest)
            {
                mainMessage.Text = "�A�C���F���o�Ă����΂��肾���H��x�e�����Ă���B";
            }
            // [�x��]�F���i�Ƃ̉�b�������Ȃ̂��C�ӂȂ̂������肵�Ă��������B
            //else if (!we.AlreadyCommunicate)
            //{
            //    mainMessage.Text = "�A�C���F���i�̂�A�����Ă邩�ȁB";
            //}
            else
            {
                // ���x���A�b�v���Ă���ꍇ�͐V�\�͕t�^�̉�b�𐷂�グ�Ă��������B
                #region "�A�C���̃��x���A�b�v"
                if (mc != null)
                {
                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.Manual;
                        md.Location = new Point(this.Location.X, this.Location.Y + this.Size.Height);
                        md.NeedAnimation = true;

                        if (mc.Level >= 3 && !mc.StraightSmash)
                        {
                            mainMessage.Text = "�A�C���F��H�������̊��G�́B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�ǂ������̂�H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F����E�E�E���傢�ƁA����Ă݂邩�B����ĂĂ���B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�I���@�I�X�g���[�g�E�X�}�b�V���I�I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�ւ��A��邶��Ȃ���";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����A��U�\���Ă���ˌ����銴�������B�Ȃ��Ȃ��C�P�Ă邾��B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�A�C���A����ɂ̓X�L���|�C���g�ƌ������̂��K�v�ɂȂ��Ă���̂�B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�Ȃ񂾂��̖ʓ|���������Ȃ̂́B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�ȒP��B�e�L�����ɂ͂P�O�O�̃X�L���|�C���g�����炩���ߗ^�����Ă����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���Ȃ݂ɃX�g���[�g�E�X�}�b�V���͂��������񂾁H";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�P�T�ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���Ƃ�����U�񂵂��ł��˂��ȁB���Ƃ̂P�O�̓��_���ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�퓬���͂P�^�[�����ɂP�񕜂���Ƃ�����H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�Ȃ�قǁA����Ƒ������Ԃ��o�ĂΉ��x���łĂ���Ă��Ƃ��B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�̐S�̍U���͂́E�E�E���΂₭���ݍ��߂�����З͂��傫�������ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�܂��A���ڍU���ł��Z�l�͏グ�Ă������ƂˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�I�[�P�[�A������g���Ă����Ƃ��邩�I";
                            ok.ShowDialog();
                            md.Message = "���A�C���̓X�g���[�g�E�X�}�b�V�����K�����܂�����";
                            md.ShowDialog();
                            using (MessageDisplay md2 = new MessageDisplay())
                            {
                                md2.StartPosition = FormStartPosition.Manual;
                                md2.Location = new Point(this.Location.X, this.Location.Y + this.Size.Height);
                                md2.NeedAnimation = true;
                                md2.Message = "���A�C���̃X�L���g�p���������܂�����";
                                md2.ShowDialog();
                            }
                            mc.AvailableSkill = true;
                            mc.StraightSmash = true;
                        }
                        if (mc.Level >= 4 && !mc.FreshHeal)
                        {
                            mainMessage.Text = "�A�C���F�{������A���̓p���f�B�����B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F���H�����剽�����āE�E�E";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���C�t�񕜂̎����B�t���b�V���E�q�[���I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�E�E�E�b�v�A�t�t�b�A�t�t�t�t�t�A���������ɂ��A�~�߂Ă�˃z���g�A�A�n�n�n�n";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������������H�p���f�B���Ȃ烉�C�t�񕜂��炢�ł��ē��R����I����I�Ȕ��z���Ȃ���́B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F����A�ʂɂ��̕ӂ̃o�J�͗ǂ����ǂˁB���͂ˁA�A�C���B���Ȃ��m�͂͂����Ȃ́H";
                            ok.ShowDialog();
                            if (this.mc.Intelligence < 6)
                            {
                                mainMessage.Text = "�A�C���F" + this.mc.Intelligence.ToString() + "���ȁE�E�E���Ď��͂܂������I�I";
                                ok.ShowDialog();
                                int effectValue = this.mc.Intelligence * 4 + 20;
                                mainMessage.Text = "���i�F������B���̒��x�̒m�͂���" + effectValue.ToString() + "���x�����񕜂��Ȃ����B�z���g�o�J�Ȃ񂾂���A�t�t�t";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F������̃��x���A�b�v���ɍl����悤�ɂ��邳�B�o���Ȃ����}�V����B";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F�܂������ˁB�m���ɂ�����ꗝ�����B�ł���������Ȃ��A�����Ȃ�񕜎����Ȃ�āB";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F�o���ē��R���B���X�̉��͂��ȁB";
                                ok.ShowDialog();
                            }
                            else
                            {
                                mainMessage.Text = "�A�C���F�b�t�E�E�E���i�A���̉��̒m�͂�" + this.mc.Intelligence.ToString() + "���B";
                                ok.ShowDialog();
                                int effectValue = this.mc.Intelligence * 4 + 20;
                                mainMessage.Text = "���i�F���A�E�\�I�H���Ⴀ" + effectValue.ToString() + "���炢�͉񕜏o����킯�ˁA���\���g���Ă邶��Ȃ��B";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F������̃��x���A�b�v������X�Ƀo�����X�悭�b������肳�B";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F�z���g�悭����Ă邶��Ȃ��B�����������̂ˁB�񕜎����̎��܂ōl���Ă����Ȃ�āB";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F�����l���Ă��ē��R���B���X�̉��͂��ȁB";
                                ok.ShowDialog();
                            }
                            mainMessage.Text = "���i�F���A����������������ˁB���炵����ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�Ƃ���ŁA������Ė��@�n����ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F������O����Ȃ��B�X�L������Ȃ�����ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�ǂ�����ď�����񂾁H";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F���A�Ђ���Ƃ��Ă����ǁA���������̕�����Ȃ��́H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�b�n�b�n�b�n�I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�ǂ�����Ă��̎�����낤�Ƃ��Ă��̂�B���񂽁A�o�J����Ȃ��H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�b�n�b�n�b�n�b�n�I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�}�i�|�C���g�Ƃ����̂������B������������Ĕ�������́A����͏���ʂQ�O���ď��ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�b�n�b�n�b�n�b�n�b�n�I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�����ʖڂ���E�E�E�n�A�@�@�`�`�`�`�E�E�E";
                            ok.ShowDialog();
                            md.Message = "���A�C���̓t���b�V���E�q�[�����K�����܂�����";
                            md.ShowDialog();
                            using (MessageDisplay md2 = new MessageDisplay())
                            {
                                md2.StartPosition = FormStartPosition.Manual;
                                md2.Location = new Point(this.Location.X, this.Location.Y + this.Size.Height);
                                md2.NeedAnimation = true;
                                md2.Message = "���A�C���̖��@�g�p���������܂�����";
                                md2.ShowDialog();
                            }
                            mc.FreshHeal = true;
                            mc.AvailableMana = true;
                        }
                        if (mc.Level >= 5 && !mc.FireBall)
                        {
                            mainMessage.Text = "�A�C���F�k�u�T�A�������������I�ǂ��ڊo�߂̏u�Ԃ��I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�k�u�T���Ⴙ������Fireball���ď��ł���H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���̉��̋ʂ�H�炢�₪��IFireBall�I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F���̋ʂ̓A�C���̃o�J�m�͂ł����������̃_���[�W�����҂ł����ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���Ă݂��B����ȃf�J�C�؂��ۏł��ɂ��Ă�������E�E�E";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�f�J�C�؂��炢�ł��A�m�͂��グ�Ă���΁A���̕�����";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���̐��΂�����͎~�߂Ă����ȁE�E�E";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�b�t�t�A��k���@�m�͂��グ��ΈЗ͂��������オ���Ă�����A�o���Ă������ƂˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����A���@�U�����̂Ă����񂶂�Ȃ�����ȁB";
                            ok.ShowDialog();
                            md.Message = "���A�C���̓t�@�C�A�E�{�[�����K�����܂�����";
                            md.ShowDialog();
                            mc.FireBall = true;
                        }
                        if (mc.Level >= 6 && !mc.Protection)
                        {
                            mainMessage.Text = "�A�C���F�k�u�R�F�X�L���A�S�F���@�A�T�F���@�A�����Ăk�u�U�ƌ����΁B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�ǂ����Ȃ̂�H�n�b�L�����Ȃ�����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�b�N�\�E�E�E���̗���͉��͔F�߂˂����B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�N�ɑ΂��Č����Ă�̂�A�����őM���Ă�񂶂�Ȃ��́H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�m�邩��B�����A���ƂȂ��v���o���������E�E�EProtection���ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�܂��A�v���o���ƑM�����Ă͎̂����悤�Ȃ��̂�B����n�̈��ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����h��͂��グ����悤���ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�������̖��@�V���v���Ȃ����ɋ��͂�ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����A��肭�g���Ă������Ƃɂ��邳�B";
                            ok.ShowDialog();
                            md.Message = "���A�C���̓v���e�N�V�������K�����܂�����";
                            md.ShowDialog();
                            mc.Protection = true;
                        }
                        if (mc.Level >= 7 && !mc.DoubleSlash)
                        {
                            mainMessage.Text = "�A�C���F�������A���x���V�ɂȂ������B���냉�i�A���̒b����ꂽ�̂��B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�N������ȏ��ꂵ���̂Ȃ�Č���̂�B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���A���������M�������E�E�E�_�u���X���b�V���I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�����ɂ��Q��U���������ȃl�[�~���O��ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�悭���Ă��ȁA�b�n�b�n�b�n�b�n�I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F���̃^�[���ŁA�ʏ�U�����Q��s���݂����ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�P�x�ɂQ��U������Ⴀ�G�����߂ɓ|����B�V���v���Ȃ��狭�����B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�Q��ڂ̍U���͋Z�p�|�C���g�����Z�ΏۂˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�}�W����I�H������A�K���K���g���Ă������I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�̐S�Ȏ��̂��߂ɃX�L���|�C���g���߂Ă����Ă�ˁH";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�I�[�P�[�A���𗹉��I";
                            ok.ShowDialog();
                            md.Message = "���A�C���̓_�u���E�X���b�V�����K�����܂�����";
                            md.ShowDialog();
                            mc.DoubleSlash = true;
                        }
                        if (mc.Level >= 8 && !mc.FlameAura)
                        {
                            mainMessage.Text = "�A�C���F�k�u�P�O�܂ŁA���ƂQ�Ɣ��������B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F��邶��Ȃ��́B���Ă��āA�����v������������H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���A�������ȁA�ȑO���i�������Ă����c�A�������Ă݂����I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F���@���̎��H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������A���Â��āE�E�EFlameAura���I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�z���g�����ȃl�[�~���O�ˁA�����Ȃ���B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������ɏh�����Ƃ���B����Ƃǂ��Ȃ�H";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�A���^�����߂Ȃ�����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F����A�_���[�W�ǉ����ʂŁI�I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�܂��A�_���[�W�Ȃ́H���œG���ݍ���ŁA���E���Ղ�Ƃ�����Ȃ����P�H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F��T���������̂́A�����Ȃ��G�Ƃ����邾��B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�����܂����Ȃ��킯����Ȃ����ǂˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���������͂�ǉ��_���[�W�Ŋm�肾�B�T���L���[�I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�ǂ��T���L���[�Ȃ̂�B�܂������E�E�E�����͈Ⴄ�̎v�����Ȃ��̂�����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�T���L���[�I�I";
                            ok.ShowDialog();
                            md.Message = "���A�C���̓t���C���E�I�[�����K�����܂�����";
                            md.ShowDialog();
                            mc.FlameAura = true;
                        }
                        if (mc.Level >= 9 && !mc.StanceOfStanding)
                        {
                            mainMessage.Text = "�A�C���F���悢��A�k�u�P�O���ȁB���̑O�ɂ�������炢�ǂ�����B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F��������炢���Ăǂ��������e�ɂ�������B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�܂��A�C���Ă������āE�E�E�������ȁAStanceOfStanding�Ȃ�Ăǂ����B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F������A�ǂ��������e�ɂ�������B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����낤�ȁE�E�E�������ȁA����ȕ��ɖh�q�̍\���������܂܂ł���̂��B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�ǂ��������e�Ȃ̂�A�\���B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���̎p����������A���̂܂܂̑̐��ōU���Ɉڂ�B��U��͂��˂��B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�Ӂ[��A�R�A���c�b�R�~�������ɁA�܂Ƃ��Ȃ̂ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�v�͖h��ƍU���̗��������˔����銴������";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F����A�o�����X���ǂ������Ȃ񂶂�Ȃ��H�C�ɓ������݂����ˁ�";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����A�_���[�W���[�X�̏ꍇ�͎g���Ă����Ƃ��邳�I";
                            ok.ShowDialog();
                            md.Message = "���A�C���̓X�^���X�E�I�u�E�X�^���f�B���O���K�����܂�����";
                            md.ShowDialog();
                            mc.StanceOfStanding = true;
                        }
                        if (mc.Level >= 10 && !mc.WordOfPower)
                        {
                            mainMessage.Text = "�A�C���F���Ƀ��x���P�O���B���B�L�O�Ɉ�M�����I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�L�O�ɑM�����Ăǂ��������Ⴂ�Ȃ̂�B�ŁA����M���̂�H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���ƂȂ������̂��炱�́w���x�A�m���Ă���C�����Ă񂾁B Word Of Power���B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F���[�h�E�I�u�E�E�E���H������A�S�R�m��Ȃ����A����Ȃ́B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������ȁA�܂����i�ɂƂ��ẮB";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F���H�ǂ������Ӗ���H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���A���≽�ł��˂��B�Ƃɂ���POWER = �͂������S�Ă��I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�����S�}�����ĂȂ��H�A�C���A�{�点�Ȃ��ł��";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�͂������S�Ă��I�b�n�b�n�b�n�I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�܂��ǂ���B�ŁA�ǂ�Ȗ��@�Ȃ킯��A���܂ɂ̓A�C����������Ă݂āB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���@�U�����Ă͖̂{���C�ӂ̑����ɉ������U�����B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F������ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����A����͈Ⴄ�B�����U���ɑ����閂�@�U�����ă��P���B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F������A�������ۂ���ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���@�Ƃ��Ă̓������g���ĕ����U�����s���ƌ������͂��ȁE�E�E";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�E�E�E���[�Ƃ��ȁA�͂������S�Ă��I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�E�E�E����ρA�o�J�ˁB";
                            ok.ShowDialog();
                            md.Message = "���A�C���̓��[�h�E�I�u�E�p���[���K�����܂�����";
                            md.ShowDialog();
                            mc.WordOfPower = true;
                        }
                        if (mc.Level >= 11 && !we.AlreadyLvUpEmpty11)
                        {
                            mainMessage.Text = "�A�C���F�b�t�A�Q���̂k�u�P�P�Ƃ��Ȃ�ƁA�ǂ��������ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�Â���˃A�C���B���񂠂�Ǝv�������ԈႢ��B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�ǂ������Ӗ�����A�������Ă݂��B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�A�C���A���񂠂Ȃ������K���ł��Ȃ�����";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�Ȃ񂾂ƁI�H�o�J�����A�҂��Ă덡�����v�����Ă��B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�E�E�E�@�E�E�E�@�E�E�E�b�\";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�E�E�E�@�Eelag�EA�E";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���FSaira Rol�E�E�E";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�ʂ����������������E�E�E";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�����A�����͖߂�����[�шꏏ�ɐH�ׂĂ����邩��A�s���s����";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�ȁA���̂����������������I�I";
                            ok.ShowDialog();
                            md.Message = "���A�C���͉����K���ł��܂���ł�����";
                            md.ShowDialog();
                            we.AlreadyLvUpEmpty11 = true;
                        }
                        if (mc.Level >= 12 && !mc.HolyShock)
                        {
                            mainMessage.Text = "�A�C���F�O��̃��x���A�b�v�͂������������A���̕����Ԃ����B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�܂��A�������ɂQ�񑱂��Ă��Ă����͖̂��������ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�C���[�W�͊��ɍ݂�B���Ȃ�S�ƁAHolyShock���I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�܁A�܂����E�E�E������ă_���[�W�n�H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F������O����B���ɉ�������H";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�z���g�_���[�W�n��������ˁB�������Ȃ񂾂��瑼�̃C���[�W�͕����΂Ȃ����P�H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���Ȃ�S�Ƃ��I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�₢��������������������E�E�E�ł��܂��΂Ɛ����ᑮ�����Ⴄ��ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������ȁA�Ƃɂ�������������ĂĂ��_���[�W�͕K�v���B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F����ɂ���Ďg���������d�v������ǂ��l���Ďg���Ă�B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�I�[�P�[�I";
                            ok.ShowDialog();
                            md.Message = "���A�C���̓z�[���[�E�V���b�N���K�����܂�����";
                            md.ShowDialog();
                            mc.HolyShock = true;
                        }
                        if (mc.Level >= 13 && !mc.TruthVision)
                        {
                            mainMessage.Text = "�A�C���F���x���A�b�v���邽�тɁA�P�K�����Ă̂͋C�����ǂ��ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F���܂ɁA�K���ł��Ȃ��ꍇ�����邯�ǂˁ�";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F����͊��ق��ė~�������S���E�E�E����A�}�Y�C�B���Ƃ��M���Č����邺�B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�E�E�E�@�E�E�E�@�E�E�E�����������B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F���������H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�h��͂t�o��U���͂t�o�Ƃ����邾��B�A�����C�ɂ���˂��B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�C�ɓ���Ȃ����Č����Ă��ˁB�ǂ�����̂�H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�S�̖{�������������΁A�F�t�������t���ɘf�킳��˂��n�Y���B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�S�̖{���H���܂Ƀw���Ȏ�������ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�S���畨��������X�L�����BTruthVision�Ɩ��Â��邺�B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�^���̎��o�A�v����ɑ���̖h��͂t�o��U���͂t�o��";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����A����������Ď����B�����킯����˂�����ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F���[�h�E�I�u�Ȃ�Ƃ������������ǁA���܂ɔ����ˁA�A�C���B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�ŏ����牽���t���ĂȂ����֌W�˂�����B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�܂������ˁA�g���ǂ��낪����������ǁA�撣���ĂˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����A�{�X��ȂǂŎg���������ȁB�C���Ƃ���B";
                            ok.ShowDialog();
                            md.Message = "���A�C���̓g�D���X�E���B�W�������K�����܂�����";
                            md.ShowDialog();
                            mc.TruthVision = true;
                        }
                        if (mc.Level >= 14 && !mc.HeatBoost)
                        {
                            mainMessage.Text = "�A�C���F���x���A�b�v�Ƃ����΁A�Α������B��������ABurn�I�I�I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F���낻��Α������_���[�W�̕��������瑲�Ƃ�����H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������H�܂���������Burn�I�������ᕨ����˂��C�����邵�ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�u�N�����鎖�A�΂̔@���v�݂����Ȃ̂͂ǂ���";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�m���ɉ��ƂȂ����������ȁA����ōs���Ă݂邩�BHeatBoost�łǂ����B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�Z�l�̃A�b�v�ˁB����ƃ_���[�W�o�J�������O�i�����̂ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������Ă�A�Z�l�����h�ȃ_���[�W�����B�債�ĕς��Ⴕ�˂���B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�t�t�A����������悤�ɂȂ��Ă������Ď�����Ȃ��B���̒��q�Ł�";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�܂��ǂ����ł��\��Ȃ����B�T�N�T�N���ƏK�����Ƃ����I";
                            ok.ShowDialog();
                            md.Message = "���A�C���̓q�[�g�E�u�[�X�g���K�����܂�����";
                            md.ShowDialog();
                            mc.HeatBoost = true;
                        }
                        if (mc.Level >= 15 && !mc.SaintPower)
                        {
                            mainMessage.Text = "�A�C���F���̃��x���A�b�v���[�e�[�V�����A�ǂ߂ė������B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F���Ⴀ����͉����Ǝv���킯�H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�X�L�����ȁI";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F����A�����v�����Č����Ă��";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���Ȃ�͂��A�����Ă�͂�A�͂����S�Ă��ISaintPower�I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�������̖��@�ˁB�c�O�ł�����";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���܂������������������I�ԈႦ������˂����I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�������̂�����̃p�����^�t�o���@�ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����A���R���������U�����t�o����B���ɂƂ��čŋ��X�y���̈���ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�����Ă������ǁA����̉ӏ����グ��̂̓X�L���ɂ͑��݂��Ȃ����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���i�A�O�ɂ����������A���O���܂ɉ��ł�����������m���Ă�񂾁H";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�m���Ă�킯����Ȃ��񂾂��ǂˁA���ƂȂ��v���o��������B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�v���o�����Č������ɐ����ƒm�������Ȍ�������Ȃ����B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�܂��܂��ǂ�����Ȃ�����Ȏ��́�";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�܂��ȁA�����A�K���K���g���Ă����Ƃ��邺�I";
                            ok.ShowDialog();
                            md.Message = "���A�C���̓Z�C���g�E�p���[�K�����܂�����";
                            md.ShowDialog();
                            mc.SaintPower = true;
                        }
                        if (mc.Level >= 16 && !mc.GaleWind)
                        {
                            UpdateMainMessage("�A�C���F�_�u���E�X���b�V���̎����炠�����M�������B");

                            UpdateMainMessage("���i�F��̂Q��U����ˁH");

                            UpdateMainMessage("�A�C���F�������A����ɂ��w���x�̗v�f������C�����Ă��񂾂�B");

                            UpdateMainMessage("���i�F�A�C���A���́w���x���Ăǂ����玝���Ă����̂�H");

                            UpdateMainMessage("�A�C���F�m�邩��B");

                            UpdateMainMessage("���i�F�����m��Ȃ������ɑM�����Ă��ƁH");

                            UpdateMainMessage("�A�C���F�m�邩���āA���ƂȂ�����B�Q��E�E�E�܂�Q�l����悤�Ȃ��̂��B");

                            UpdateMainMessage("���i�F�Q�l����킯�Ȃ�����Ȃ��B�������Ă�̂�A�C���B");

                            UpdateMainMessage("�A�C���F�����ɑ����d�Ȃ�C���[�W����ĂыN�����񂾂�BGaleWind�ƌĂԂ��Ƃɂ��邺�I");

                            UpdateMainMessage("���i�F�l�[�~���O�����Ȃ�I�J�V�C���B�A�C���E�E�E���v�H");

                            UpdateMainMessage("�@�@�@�y�A�C���̎p���d�����Ă���悤�Ɍ����n�߂��z");

                            UpdateMainMessage("���i�F��������ƁA�ςȗH�삪�t���Ă���B");

                            UpdateMainMessage("�A�C���F��������͉����B�C�ɂ���ȁB�܂������Ɉ��邺�I");

                            UpdateMainMessage("�y�A�C���F�H�炦�I�t�@�C�A�E�{�[���I�z�@�@�@�@�y�H�H�H�F�H�炦�I�t�@�C�A�E�{�[���I�z");

                            UpdateMainMessage("���i�F�E�E�E�E�E�E������ˁE�E�E�E�E�E��������B");

                            UpdateMainMessage("�A�C���F����͎g���邺�B�_�u���E�X���b�V���Ȃ�S��U�����B�ŋ�����I�b�n�b�n�b�n�I�I");

                            UpdateMainMessage("���i�F�A�C���A�M���ǂ��ł�����w�񂾂̂�H���A�m��Ȃ���悻�́w���x�Ƃ������́B");

                            UpdateMainMessage("�A�C���F�w�񂾂킯����˂��B�ŏ�������g�ɕt���Ă���悤�Ȋ������B");

                            UpdateMainMessage("���i�F�ŏ���������āA���Ⴀ�ŏ����������ĂȂ�����B");

                            UpdateMainMessage("�A�C���F����A�ŏ�������ł���󂶂�˂����āE�E�E");

                            UpdateMainMessage("���i�F���Ⴀ�A���ł��������̂��ł���̂�I�H�����}�X�^�[���邩��A�����Ă�I�I");

                            UpdateMainMessage("�A�C���F���i�A���������B�R���́A�����������񂶂�˂��񂾁B���x�܂������Ă�邳�B");

                            UpdateMainMessage("���i�F�z���g�H���Ⴀ��ŗǂ����狳���Ă�ˁB�z���g�ɂ���H");

                            UpdateMainMessage("�A�C���F�E�E�E�����A�z���g���B");

                            md.Message = "���A�C���̓Q�C���E�E�B���h���K�����܂�����";
                            md.ShowDialog();
                            mc.GaleWind = true;
                        }
                        if (mc.Level >= 17 && !we.AlreadyLvUpEmpty12)
                        {
                            UpdateMainMessage("�A�C���F�k�u�P�O�F���P�@�k�u�P�P�F�Ȃ��@�k�u�P�U�F���Q�@�k�u�P�V�F�E�E�E�y�݂�z�I�I");

                            UpdateMainMessage("���i�F�A�C���A�����͖߂��Ă�����[�шꏏ�ɂǂ���");

                            UpdateMainMessage("�A�C���F���̗[�т̗U�����͎~�߂Ă��ꂦ���������I�I�I");

                            UpdateMainMessage("���i�F�n���i�f�ꂳ��ɂ͂��������Ă���������A�S�z���Ȃ��ėǂ�����");

                            UpdateMainMessage("�A�C���F�����������������E�E�E���Ă�E�E�E");

                            UpdateMainMessage("�A�C���F�E�H�I�I�H�H�H�E�E�E�@�E�E�E�@�E�E�E");

                            UpdateMainMessage("�A�C���F�@�o�ł�I�@�@�o�ł�I�I�@�@�o�ł�I�I�I");

                            UpdateMainMessage("�A�C���F�@�n�C�p�[�E�W�F�l�e�B�b�N�E�G���N�g���E�{���o�[�I�I");

                            UpdateMainMessage("���i�F�n�C�n�C�A����Ȃ̖�������ˁB");

                            UpdateMainMessage("�A�C���F�R�R�Ńl�[�~���O������K���ł���񂶂�˂��̂���I");

                            UpdateMainMessage("���i�F����ȏ���ȃ��[������������ˁB���߂Ă��ѐH�ׂ��");

                            UpdateMainMessage("�A�C���F�E�E�E�E�E�E����u�����ɃC���Z���e�B�u�A�g���N�^�I�I�I");

                            UpdateMainMessage("�@�@�@�w�h�O�V���A�A�@�@�I�I�I�x�i���i�̃n�C�L�b�N����񂾁j");

                            UpdateMainMessage("�A�C���F�����A�_���W�����̌�́A�[�тł��s���Ƃ��邩�A���i�E�E�E");

                            md.Message = "���A�C���͉����K���ł��܂���ł�����";
                            md.ShowDialog();
                            we.AlreadyLvUpEmpty12 = true;
                        }
                        if (mc.Level >= 18 && !mc.InnerInspiration)
                        {
                            UpdateMainMessage("�A�C���F�O��̃��x���A�b�v���͊m���g�߂��Ă�����[�сh�������ȁB");

                            UpdateMainMessage("���i�F�������ɂQ��͖������Č����Ă邶��Ȃ��B�ǂ��M�������Ȃ́H");

                            UpdateMainMessage("�A�C���F�����A�퓬���ǂ����Ă��A���I�ȍs���̂����ŏW���ł��˂��ꍇ������B");

                            UpdateMainMessage("���i�F�m���ɁA�퓬���͂ǂ����Ă��C�������Ȃ���ˁB");

                            UpdateMainMessage("�A�C���F�����ł��A�C�𔲂��킯����˂����A�U����h������˂������ɁB");

                            UpdateMainMessage("���i�F�ǂ���������H");

                            UpdateMainMessage("�A�C���F�W�������Ȃ�Ƃł��Ȃ���Ď��ŁE�E�EInner Inspiration�łǂ����B");

                            UpdateMainMessage("���i�F�����ɏW���E�E�E�X�L���Ɋ֘A���Ă����ˁB");

                            UpdateMainMessage("�A�C���F�ċz�𐮂��āA�̐��𐮂���̂��B���������X�L���|�C���g�ɐ�O�ł���B");

                            UpdateMainMessage("���i�F���C�t����Ȃ��A�X�L���|�C���g�̉񕜂Ƃ͍l������ˁ�");

                            UpdateMainMessage("�A�C���F�X�L���͉����Ǝg����ʂ���������ȁB����ł��������X�L�����g������Ă킯���B");

                            UpdateMainMessage("���i�F�X�L���|�C���g�̉񕜂́A�S�l���e������݂���������グ�Ă������ˁB");

                            UpdateMainMessage("�A�C���F���𗹉��I");

                            md.Message = "���A�C���̓C���i�E�C���X�s���[�V�������K�����܂�����";
                            md.ShowDialog();
                            mc.InnerInspiration = true;
                        }
                        if (mc.Level >= 19 && !mc.WordOfLife)
                        {
                            UpdateMainMessage("�A�C���F�����������I�k�u�P�X�����I");

                            UpdateMainMessage("���i�F�k�u�Q�O�ł��Ȃ��̂ɉ����ł�̂�H");

                            UpdateMainMessage("�A�C���F���i�A�k�u�Q�O�Łg�߂��Ă�����[�сh�͖����͂�����ȁB");

                            UpdateMainMessage("���i�F�܂��A��؂�̗ǂ��|�C���g�ł���͖�����ˁB");

                            UpdateMainMessage("�A�C���F���̒��O�Łw���x���K�����Ă݂���I");

                            UpdateMainMessage("���i�F�ςȏ��ɃC�`�C�`���g���ĂȂ��ŁA�z���z���A��̂ǂ�Ȃ̂�H");

                            UpdateMainMessage("�A�C���F���R�̍L��ȑ����A����P��N�A�����̑�A�ΎR�̃}�O�}�Ȃǂɂ��w���x���݂�B");

                            UpdateMainMessage("�A�C���F�����͍݂�ׂ����č݂�A����ׂ����Đ����Ă��鑶�݂���B");

                            UpdateMainMessage("���i�F�E�E�E�܁A�܂������ˁB");

                            UpdateMainMessage("�A�C���F�����ɂ̓p���[���݂Ȃ����Ă�͂����B��������G�l���M�[���؂�邺�BWord Of Life�B");

                            UpdateMainMessage("���i�F����Ȃ��̊�������́H�{���ɁB");

                            UpdateMainMessage("�A�C���F�����A�������̑c��͌������炻�����Ă��Ă������B");

                            UpdateMainMessage("���i�F�܂������Ă݂�΂�����������Ȃ���ˁB");

                            UpdateMainMessage("�A�C���F�P�^�[���ɕt���A��΂������C�t���񕜂���A�����ōs�����ĉ񕜂���킯����˂��B");

                            UpdateMainMessage("���i�F�񕜂��Ȃ���s���o����킯�ˁB�Ȃ��Ȃ�����������Ȃ��B");

                            UpdateMainMessage("�A�C���F�܂��A���C�t������Ă�Ԃ͕s�v�����A�~�������ɃK�b�c���񕜂��Ă킯�ł��˂��B");

                            UpdateMainMessage("���i�F���������Ӗ����Ⴛ��قǋ����Ȃ������ˁB");

                            UpdateMainMessage("�A�C���F�_���[�W���[�X�ɂ́A�����Ă������B�C���Ƃ��ȁI");

                            md.Message = "���A�C���̓��[�h�E�I�u�E���C�t���K�����܂�����";
                            md.ShowDialog();
                            mc.WordOfLife = true;
                        }
                        if (mc.Level >= 20 && !mc.FlameStrike)
                        {
                            UpdateMainMessage("�A�C���F��������I�k�u�Q�O���B�I���������������܂ŁB");

                            UpdateMainMessage("���i�F�n���i�f�ꂳ��ɕ����Ă������͂����ƍ���Ă����炵����B�ǂ�������ˁ�");

                            UpdateMainMessage("�A�C���F�����A���ꂶ�Ⴂ�����E�E�E");

                            UpdateMainMessage("�A�C���F���R�����A�Α������ȁB");

                            UpdateMainMessage("�A�C���F�^�g�̉����C���[�W�B");

                            UpdateMainMessage("�A�C���F�Ă��ł������@���A���ڃ_���[�W���B");

                            UpdateMainMessage("�A�C���FFlameStrike�I�I�I");

                            UpdateMainMessage("�@�@�@�w�b�V���E�E�D�D�E�E�E�V���S�I�I�I�H�I�H�I�H�H�H�H�H�I�I�I�x�@�@");

                            UpdateMainMessage("���i�F�A���^�̍U�����@�̓z���g��Ȃ���������ˁB���Ă��ď����|�����炢��B");

                            UpdateMainMessage("�A�C���F�v���Ԃ�̃_���[�W�n���@���B�r���Ȃ������B");

                            UpdateMainMessage("���i�F�t�@�C�A�E�{�[�����F���Z�����A�Ïk���ꂽ��ɂȂ��Ă����");

                            UpdateMainMessage("�A�C���F�����A�L���������̂��ƕ��U������������ȁA�Ïk�����Ă���B�R�C�c�͋��͂����B");

                            UpdateMainMessage("���i�F�A�C���̓z���g�Α������s�b�^���ˁB�����܂������炢����B");

                            UpdateMainMessage("�A�C���F���i�A���O���������͑����s�b�^������B�ǂ����������̂𐶂ݏo�����A�������ȁI");

                            UpdateMainMessage("���i�F�t�t�A�����]�ނƂ����A�A�C���Ȃ񂩂ɕ����Ȃ��񂾂����");

                            md.Message = "���A�C���̓t���C���E�X�g���C�N���K�����܂�����";
                            md.ShowDialog();
                            mc.FlameStrike = true;
                        }
                        if (mc.Level >= 21 && !mc.HighEmotionality)
                        {
                            UpdateMainMessage("�A�C���F�k�u�Q�P���B�����I�����ŁA�������B�b�K�c���Ɨ��郂�m���~������ȁB");

                            UpdateMainMessage("���i�F�A�C���̎v�����̂́A�b�K�c���Ƃ������̂΂��肶��Ȃ��H");

                            UpdateMainMessage("�A�C���F����A���₢��Ⴄ���B�������Č����񂾁E�E�E");

                            UpdateMainMessage("�A�C���F�S�̂��N���オ�邩�̂悤�ȁE�E�E");

                            UpdateMainMessage("���i�F����ȏ�o�J�ɂȂ��Ăǂ���������B");

                            UpdateMainMessage("�A�C���F�o�J���Č����ȁE�E�E�ǂ����s�����I�@HighEmotionality�I");

                            UpdateMainMessage("      �w�A�C���̎���ɋ���ȃI�[�����\��n�߂��x");

                            UpdateMainMessage("���i�F������A���悻��H");

                            UpdateMainMessage("�A�C���F�b�n�n�A���q�ǂ����������B�@���i�I��������Ǝ󂯎~�߂Ă݂Ă���I");

                            UpdateMainMessage("�A�C���F�I���@�I�@�X�g���[�g�E�X�}�b�V���I�I");

                            UpdateMainMessage("      �w�K�L�C�B�B�I�I�x");

                            UpdateMainMessage("���i�F������A�������U�����E�E�E�I�H�@��Ȃ�����Ȃ��́I");

                            UpdateMainMessage("�A�C���F�����ȁA���̂ł�������������肾�B�ǂ����S�̓I�ɔ\�͂��t�o���Ă�悤���B");

                            UpdateMainMessage("���i�F�܂��������̃o�J�͂��X�ɏグ�Ă���Ȃ�āA�C�J�T�}��ˁB");

                            UpdateMainMessage("      �w�A�C�����Ƃ�܂��I�[��������n�߂Ă����x");

                            UpdateMainMessage("�A�C���F���ӂ��������E�E�E�ǂ���炱�ꂪ���E�݂������B");

                            UpdateMainMessage("���i�F������O��A����Ȋ�Ȃ���������ԑ����n�Y�Ȃ����B");

                            UpdateMainMessage("�A�C���F�ł��܂��A�̐S�ȋǖʂŎg���΂���قǃb�K�c���Ƃ������͖̂������B�C���Ă����ȁI");

                            md.Message = "���A�C���̓n�C�E�G���[�V���i���e�B���K�����܂�����";
                            md.ShowDialog();
                            mc.HighEmotionality = true;
                        }
                        if (mc.Level >= 22 && !mc.WordOfFortune)
                        {
                            UpdateMainMessage("�A�C���F�k�u�Q�Q�E�E�E�����邺�B");

                            UpdateMainMessage("���i�F������H");

                            UpdateMainMessage("�A�C���F�������B");

                            UpdateMainMessage("���i�F�A���^����l�{�P�΂����ɂ��鏊������H");

                            UpdateMainMessage("�A�C���F�΂����܂܎��ʂ킯�˂����낤���B���m�Ɍ����΁A���_�W�����̂��̂��B");

                            UpdateMainMessage("���i�F����ǂ͈�̂ǂ��������e��H");

                            UpdateMainMessage("�A�C���F���̂P�^�[���͉��������A���̐؂����_�ɑS�_�o���W�������閂�@���B");

                            UpdateMainMessage("���i�F�Ȃ�قǁA���ƂȂ��ǂ߂���B�o������ˁE�E�E�N���e�B�J��");

                            UpdateMainMessage("�A�C���F���������B������Word Of Fortune�Ƃ��邺�I");

                            UpdateMainMessage("�A�C���F�S�_�o�̏W�����L�}������A��͍U������݂̂��I�@�I���@�I");

                            UpdateMainMessage("���i�F������ˁA�{���ɃN���e�B�J�����o��Ȃ�āB");

                            UpdateMainMessage("�A�C���F�������A���̏W�����@�͖{���ɒZ���Ԃ����ێ��ł��˂��B�P�^�[�������x���ȁB");

                            UpdateMainMessage("���i�F���炩���߃N���e�B�J�����o�����߂ɂP�^�[���]���ɂ���̂�ˁB");

                            UpdateMainMessage("�A�C���F�������ȁA���\�g�����͌����邩������Ȃ��B�����A�g���؂��Ă݂��邺�B");

                            md.Message = "���A�C���̓��[�h�E�I�u�E�t�H�[�`�����K�����܂�����";
                            md.ShowDialog();
                            mc.WordOfFortune = true;
                        }
                        if (mc.Level >= 23 && !mc.Glory)
                        {
                            UpdateMainMessage("�A�C���F�k�u�Q�R���ƁI�@���i�A�����́g�߂��Ă�����[�сh�̗U���͖����񂾂�ȁH");

                            UpdateMainMessage("���i�F�����A�����͓��ɖ������B");

                            UpdateMainMessage("�A�C���F��������I���Ⴀ�A�������o���o���M���Ă݂���I");

                            UpdateMainMessage("���i�F������A����Ȋ�΂Ȃ������ėǂ�����Ȃ��B����ˁB");

                            UpdateMainMessage("�A�C���F�b�n�b�n�b�n�I���������I�@���Ă��A�悭�l�����炳�B");

                            UpdateMainMessage("�A�C���F�_���[�W���[�X���Ă��邾��H100�_���[�W�^���āA40�_���[�W�H�炤�݂����ȁB");

                            UpdateMainMessage("���i�F�퓬���̊�{���̊�{�ˁB���ꂪ�ǂ��������́H");

                            UpdateMainMessage("�A�C���F�ŋߋC�t�������AFreshHeal�͉r����̌��ʂƉr���O���[�V�����Ɏ኱���ԍ����������Ă�B");

                            UpdateMainMessage("�A�C���F�܂�A���̎��ԍ�����肭�q���΃q�[�����A�^�b�N�̊������B");

                            UpdateMainMessage("�A�C���F���̗���͕�����C�����˂����I�b�n�b�n�b�n�I�@Glory�Ɩ��t���邺�I");

                            UpdateMainMessage("���i�F�q�[�����A�^�b�N�E�E�E�V���v���Ȃ����ɁA�o�J�ˁB");

                            UpdateMainMessage("�A�C���F�o�J���Č����ȁA���\���͂����I�H");

                            UpdateMainMessage("�A�C���F�q�[�����A�^�b�N�I�@�q�[�����A�^�b�N�I�@�m�����ǂ�����I�@�b�n�b�n�b�n�I");

                            UpdateMainMessage("���i�F�n�A�A�@�@�E�E�E�E���^�����̃o�J�ˁB");

                            md.Message = "���A�C���̓O���[���[���K�����܂�����";
                            md.ShowDialog();
                            mc.Glory = true;
                        }
                        if (mc.Level >= 24 && !mc.VolcanicWave)
                        {
                            UpdateMainMessage("�A�C���F���������E�E�E�k�u�Q�S�I�I");

                            UpdateMainMessage("���i�F���r���[�Ȑ����Ȃ̂ɁA���ʂɐ���グ�Ă��ˁB");

                            UpdateMainMessage("�A�C���F�Α����Ɍ��肾�I�I�ǂ����I�H");

                            UpdateMainMessage("���i�F�m�����̑O�A�Α�����FlameStrike���K�����ĂȂ������H");

                            UpdateMainMessage("�A�C���F�ǂ�����˂����A���܂ɂ͘A���ł��ȁB");

                            UpdateMainMessage("���i�F�܂����E�E�E�_���[�W����Ȃ��ł��傤�ˁB");

                            UpdateMainMessage("�A�C���F�����̂܂������I�b�n�b�n�b�n�I");

                            UpdateMainMessage("���i�F��������ƁA�{���Ƀ_���[�W�o�J�ˁE�E�E�������̂��ǂ��񂶂�Ȃ��H");

                            UpdateMainMessage("�A�C���F�ǂ��񂾂��āA���̂��炢�ł����x�ǂ����炢����B�s�����I Volcanic Wave�I");

                            UpdateMainMessage("�@�@�@�w�V���S�I�I�H�H�H�H�E�E�E�u���A�A�@�@�A�I�I�I�x�@�@");

                            UpdateMainMessage("�A�C���F�b�n�b�n�b�n�I�R���オ��I�R������I�����āA�R���s�����܂��ȁI");

                            UpdateMainMessage("���i�F�Ō�����ɒP��ԈႦ�Ă���A�A�C���B");

                            UpdateMainMessage("�A�C���F�I�[�P�[�I�[�P�[�����̒P��~�X�͋C�ɂ��˂��I");

                            UpdateMainMessage("���i�F�z���b�g�����ꒃ����āE�E�E�ł��A�������Α�����ˁB�З͂͑������肻������B");

                            UpdateMainMessage("�A�C���F�����AFlameStrike�̎��_�ŃC���[�W���������B�Ïk���ꂽ���̂�A���Ŕg�̂悤�ɏo���������ȁB");

                            UpdateMainMessage("���i�F�}�i��������Ȃ�f�J�����ˁB�}�i�؂�ɋC�����Ďg���Ă����Ă�ˁB");

                            UpdateMainMessage("�A�C���F���𗹉��I");

                            md.Message = "���A�C���̓��H���J�j�b�N�E�E�F�C�u���K�����܂�����";
                            md.ShowDialog();

                            mc.VolcanicWave = true;
                        }
                        if (mc.Level >= 25 && !mc.CrushingBlow)
                        {
                            UpdateMainMessage("�A�C���F�k�u�Q�T���B������Ƃ������ԃ|�C���g�݂����Ȃ��񂾂ȁB");

                            UpdateMainMessage("���i�F�ǂ��A�����v���������H");

                            UpdateMainMessage("�A�C���F����A�ǂ����낤�ȁB�����Ƃ����M���͂���񂾂��E�E�E");

                            UpdateMainMessage("���i�F�����Ȃ́H�������n�b�L�����Ȃ��������ˁB");

                            UpdateMainMessage("�A�C���F���āE�E�E�ǂ�Ȃ̂��ǂ����E�E�E");

                            UpdateMainMessage("���i�F�����A�C���֔]�V�����ђʃu���[�����ΑM���Ƃ��H");

                            UpdateMainMessage("�A�C���F�����I�@���ꂾ�I");

                            UpdateMainMessage("���i�F�����A�{�C�Ȃ́H�A�C���E�E�E���񂽂Ђ���Ƃ��Đ^���o�J�H�@���Ⴀ�����Ȃ���");

                            UpdateMainMessage("�A�C���F���A���₢��I�@�҂đ҂đ҂āI�I���ɂ��̃u���[�͎~�߂Ă���I�M���������Ȃ����܂��I�I");

                            UpdateMainMessage("���i�F���`��B�Ȃ񂾁A�܂�Ȃ���ˁ�@�܂�������A����M�����̂�H");

                            UpdateMainMessage("�A�C���F�����X�^�[�̔]�V�֒��ڑŌ��������鎖�ň�u�C�₳������Ă̂͂ǂ����B");

                            UpdateMainMessage("���i�F�Ȃ�قǁA�������u�ł��C�₳����΁A�s�������~�߂鎖���o�����ˁB�ǂ��񂶂�Ȃ��H");

                            UpdateMainMessage("�A�C���F�l�[�~���O�͂������ȁE�E�E�������i�̋Ɉ��Z�̖��O���g���āE�E�E�B");

                            UpdateMainMessage("�A�C���FCrushingBlow���Ă̂͂ǂ����I�@���ȁI�@���i�I�H");

                            UpdateMainMessage("���i�F���E��E���E�Ɉ���I�H�H�炢�Ȃ����I�@�b�n�A�A�@�@�I�I");

                            UpdateMainMessage("�@�@�@�w�Y�h�I�I�H�H�H���I�I�I�x�i���i�̃N���[���N���e�B�J���N���b�V���O���A�C�����y�􂵂��j");

                            md.Message = "���A�C���̓N���b�V���O�E�u���[���K�����܂�����";
                            md.ShowDialog();

                            mc.CrushingBlow = true;
                        }
                        if (mc.Level >= 26 && !we.AlreadyLvUpEmpty13)
                        {
                            UpdateMainMessage("�A�C���F�܂��k�u�Q�U���ď����ȁB�����ĂƁI");

                            UpdateMainMessage("���i�F�����āA�����͖߂�����[�т��ꏏ�����Ă��炤���");

                            UpdateMainMessage("�A�C���F�E�E�E");

                            UpdateMainMessage("�A�C���F�E�E�E�����ĂƁI�ǂ��������ȁI");

                            UpdateMainMessage("���i�F���܂ɂ͐V�����O�H�n�̂��X�ł��T���Ă݂Ȃ��H");

                            UpdateMainMessage("�A�C���F�E�E�E���ĂƁI���ĂƁI���ĂƁI�I�I");

                            UpdateMainMessage("�A�C���F�E�I�I�I�H�H�H�I�I�@���肦�˂������남�����������I�I");

                            UpdateMainMessage("�A�C���F�K�E�I�@�I�����W�n�[�u�e�B�I");

                            UpdateMainMessage("�A�C���F���`�I�@�O�����`�L���I");

                            UpdateMainMessage("�A�C���F�K�E�R���{�I�@���񂪂�r�[�t�O���^���I");

                            UpdateMainMessage("�A�C���F�_�Z�I�@�G�ߕ��E�H�̏t�J�I");

                            UpdateMainMessage("�A�C���F�Ȃ񂾁A���̑M���͂����������I�I");

                            UpdateMainMessage("���i�F���������X�Ȃ�A�����ˁw�������K�΁x���I�X�X����B");

                            UpdateMainMessage("�A�C���F�����A���i�I���̐ݒ�͂ǂ��l���Ă�������������I�H");

                            UpdateMainMessage("���i�F���`��A�߂��Ă�����ꏏ�ɂ��ѐH�ׂ�񂾂�������Ɗ�񂾂�H");

                            UpdateMainMessage("�A�C���F�E�E�E�@�������Ȃ��A�{���͊�ԏ��Ȃ̂����ȁE�E�E�i�K�N�j");

                            UpdateMainMessage("���i�F�������A�C�ɂ����w�������K�΁x�֍s���܂���A�b�z���z����");

                            UpdateMainMessage("�A�C���F�_��A����͊�Ԃׂ��Ȃ̂��B�߂��ނׂ��Ȃ̂��E�E�E�E�E");

                            md.Message = "���A�C���͉����K���ł��܂���ł�����";
                            md.ShowDialog();
                            we.AlreadyLvUpEmpty13 = true;
                        }
                        if (mc.Level >= 27 && !mc.AetherDrive)
                        {
                            UpdateMainMessage("�A�C���F�k�u�Q�V�����I�A���x�݂͂˂��񂾂�����ȁI�H�悵�������I");

                            UpdateMainMessage("���i�F����A����ȂɎ��Ɨ[�ѐH�ׂ�̂����Ȃ̂�����B");

                            UpdateMainMessage("�A�C���F���A���₢�₢��A���������킯����˂��B");

                            UpdateMainMessage("���i�F�b�t�t�A�܂��ǂ����B�ŁA�ǂ��Ȃ̑M����́H");

                            UpdateMainMessage("�A�C���F�O���HighEmotionality�͐��_�I�ȍ��g����v�����Ă���񂾂��B");

                            UpdateMainMessage("�A�C���F����͂����Ǝ��R�@���ɉ��������̂𐶂ݏo���Ă݂����񂾂�B");

                            UpdateMainMessage("���i�F�������܂��w���e�R�Ȕ��z�������o���Ă�����ˁB");

                            UpdateMainMessage("�A�C���F�w���e�R���ĉ�����A����ł������ƍl���Ă�񂾂��B");

                            UpdateMainMessage("�A�C���F�ߋ��̈�Y�Ƃ��Ăׂ�G�[�e���̗́B����������Ė��@�Ƃ��ď悹�Ă͂ǂ����H");

                            UpdateMainMessage("���i�F�ǂ����H���Ď��Ɍ����Ă��𓚂̂��悤���Ȃ���B�ǂ��Ȃ́A�o�������Ȃ́H");

                            UpdateMainMessage("�A�C���F�ŏ���WordOfPower����������B�A�����U���ł͂Ȃ��A���͂Ɉ��ێ�������悤�Ȋ������B");

                            UpdateMainMessage("�A�C���F��������āE�E�E����ɏ������`�Ƃ��Ď��͂Ɏ�芪���󋵂𐶐����I�@AetherDrive�I");

                            UpdateMainMessage("      �w�����E�E�D�D�D���I�I�x�i�A�C���̎��͂ɐV������z�����@�����������ꂽ�I�j");

                            UpdateMainMessage("���i�F�b���I����A����I�HWordOfPower�̉�݂����ˁE�E�E");

                            UpdateMainMessage("�A�C���F�����A������Ƒf�U�肵�Ă݂邩�E�E�E�I���@�I");

                            UpdateMainMessage("���i�F�E�E�E��������Ȃ��I�H�@�������y���ɐU�肪������I�I");

                            UpdateMainMessage("�A�C���F���ꂾ������˂��E�E�E����A��������̑f�U�葤����Ȃ��āA�����́w�_�~�[�f�U��N�x�ɓ��ĂĂ����΁B");

                            UpdateMainMessage("���i�F�����I�w�_�~�[�f�U��N�x�̐U�肪�x���Ȃ��Ă��ˁB");

                            UpdateMainMessage("�A�C���F�������A����͋�z�����̉��p����ŁA�U���E�h��Ƃ��ɗD�ꂽ���ʂ𔭊��ł���B");

                            UpdateMainMessage("���i�FWordOfPower������Â��v���񂾂��ǁA�A�C���̂��������Ƃ��^���ł��Ȃ���ˁB");

                            UpdateMainMessage("�A�C���F�������H���i��������Ƃ��ΏK���ł����������ǂȁB");

                            UpdateMainMessage("���i�F���`��E�E�E��߂Ƃ���B���͎��̂����ŏK�����Ă�������B");

                            UpdateMainMessage("�A�C���F�������B�܂����ꂪ�ǂ������ȁB���ቓ���Ȃ��A���̖��@�̓K���K���g�킹�Ă��炤���I");

                            md.Message = "���A�C���̓G�[�e���E�h���C�u���K�����܂�����";
                            md.ShowDialog();
                            mc.AetherDrive = true;
                        }
                        if (mc.Level >= 28 && !mc.KineticSmash)
                        {
                            UpdateMainMessage("�A�C���F�悤�₭�k�u�Q�W���ȁA�k�u�R�O�܂ł��Ə��������B");

                            UpdateMainMessage("���i�F���\���͂Ȃ̂������Ă��Ă���ˁB");

                            UpdateMainMessage("�A�C���F�����A�����I���͂����ł����������z�����X�L�����K��������肾�B");

                            UpdateMainMessage("���i�F�܂������_���[�W�n�ȃ��P�H");

                            UpdateMainMessage("�A�C���F�����A�������I�I");

                            UpdateMainMessage("���i�F�������̗ނ͑M���Ȃ��́H");

                            UpdateMainMessage("�A�C���F�_���[�W�n���I�C���Ƃ����āI");

                            UpdateMainMessage("���i�F�܂��ǂ����ǂˁA��̂ǂ��������̂Ɏd�グ������B");

                            UpdateMainMessage("�A�C���F���Z����҂ݏo�����Ǝv���ĂȁI�܂��A��{�̓R�����B");

                            UpdateMainMessage("�@�@�@�w�b�u���A�b�u�u���I�x�i�A�C���͂Q�C�R��K�x�ɑf�U�肵���j");

                            UpdateMainMessage("�A�C���F�����Ă��ƁB�R�R���炾�E�E�E�s�����B");

                            UpdateMainMessage("���i�F�ւ��E�E�E�����������Ǝ��R�ȍ\���ˁB�����̓ːi�^����Ȃ���ˁB");

                            UpdateMainMessage("�A�C���F���͕���̈З͂̍������Ă̂͌��^�ǋ����Ǝv���Ă�B");

                            UpdateMainMessage("���i�F�����H");

                            UpdateMainMessage("�A�C���F�\�������Ă��ʖڂ��B�͂����U����̂��ʖڂ��B");

                            UpdateMainMessage("�A�C���F�ǂ̍\������ԗ͂��Ïk����邩�A�l�������ɂ��̃X�^�C���ɂ��ǂ蒅�����B");

                            UpdateMainMessage("���i�F�\�����Č������A�����l���������ɓ˂������Ă��邾���ɂ��������Ȃ����B");

                            UpdateMainMessage("�A�C���F�������A���̓˂������Ă����Ԃ��炪��ԗ͂��o����B����ȋC������񂾁B");

                            UpdateMainMessage("�A�C���F���̍ō����x�֓��B����̂Ɠ����ɗ͂���C�ɋÏk������A������KineticSmash�I�I");

                            UpdateMainMessage("      �w�b�K�I�b�V�C�C�B�B�E�E�E�x");

                            UpdateMainMessage("���i�F��������������ˁB�h�肳�͂Ȃ����ǁA�قƂ�ǉ��ł��Ȃ��|�������ˁB");

                            UpdateMainMessage("�A�C���F������I����͍s���邺�A�͂ƕ���U���A�����ĐS�̏W�����K�v�s�����ȁB");

                            UpdateMainMessage("�A�C���F���킪������΋����قǈЗ͂��������Ƃ��o���������B����͎g���邺�I");

                            md.Message = "���A�C���̓L�l�e�B�b�N�E�X�}�b�V�����K�����܂�����";
                            md.ShowDialog();
                            mc.KineticSmash = true;
                        }
                        if (mc.Level >= 29 && !mc.StanceOfEyes)
                        {
                            UpdateMainMessage("�A�C���F�k�u�Q�X�����I���ƈ�Ɣ������ȁI");

                            UpdateMainMessage("���i�F�k�u�R�O�L�O������񂾂���A����܂�h��Ȃ͔̂����Ă�������H");

                            UpdateMainMessage("�A�C���F���A�ǂ����ƌ����ȁA���i�B�������ȁA�����͈�n���ɍs���Ƃ��邩�B");

                            UpdateMainMessage("�A�C���F�m���Ƀ_���[�W�n�΂��肪��������������ȁA���ɂ��邩�E�E�E");

                            UpdateMainMessage("�@�@�@�w�A�C���͒������^�ʖڂȊ�����Đ^�������ɋ󒆂��Î����n�߂��x");

                            UpdateMainMessage("���i�F�����A����ǂ��񂶂�Ȃ��H�A�C���炵���������Ē��x�ǂ�������B");

                            UpdateMainMessage("�A�C���F��H�������H");

                            UpdateMainMessage("�@�@�@�w�A�C���̊�͌��̉�b���[�h�ɖ߂����B�x");

                            UpdateMainMessage("���i�F���A�����������������ɂݕt���Ă�����Ȃ��B���ł����؂ꂻ���Ȋ炵�Ă����B");

                            UpdateMainMessage("�A�C���F��H��A�����A������Ƃ��낢��l���Ă��̂��B");

                            UpdateMainMessage("�A�C���F�҂Ă�E�E�E�������B�������������I�@���߂��AStanceOfEyes�Ɩ��Â��邺�I");

                            UpdateMainMessage("�A�C���F���i�A���x�ǂ��B�������K���B");

                            UpdateMainMessage("���i�F�����v���������Č����̂�H�ςȃl�[�~���O��������āA����܂�W���W�����Ȃ��ł�B");

                            UpdateMainMessage("�A�C���F���A����A���������Ӗ�����˂��E�E�E�܂������A������Ƃ������ނ��B");

                            UpdateMainMessage("���i�F����������A�s�����E�E�E");

                            UpdateMainMessage("���i�F�n�A�@�@�I�A�C�X�j�[�h���I�I");

                            UpdateMainMessage("�A�C���F������A�\�R���I�I");

                            UpdateMainMessage("�@�@�@�w�A�C���͂Ƃ����Ƀ��i�̉r�������A�C�X�j�[�h���̐����u�ԂɌ���U�肩�������I�x");

                            UpdateMainMessage("���i�F�E�E�E�b�E�\�I�H���̖��@�����������ꂽ�I�H");

                            UpdateMainMessage("�A�C���F�b�n�b�n�b�n�I�ǂ������������I");

                            UpdateMainMessage("���i�F�n���Ȃ��ɕ������h��Ȍ��ʂˁE�E�E���ς�炸�ƍs������������B");

                            UpdateMainMessage("���i�F�ł��܂��A���������Ă�ԍU���͂������ɏo���Ȃ��킯�ˁB");

                            UpdateMainMessage("�A�C���F�܂��ȁA����̓�����W�����Č��ĂȂ��Əo���˂��B");

                            UpdateMainMessage("���i�F���肪�����_���Ă���ꍇ�͂��Ȃ�g����������Ȃ��H");

                            UpdateMainMessage("�A�C���F�����A����̑�Z�Ƃ��̓R���ŕ����Ă�邺�A�C���Ă����ȁI");

                            md.Message = "���A�C���̓X�^���X�E�I�u�E�A�C�Y���K�����܂�����";
                            md.ShowDialog();
                            mc.StanceOfEyes = true;
                        }
                        if (mc.Level >= 30 && !mc.Resurrection)
                        {
                            UpdateMainMessage("�A�C���F��������I���ɂk�u�R�O�B�������I");

                            UpdateMainMessage("���i�F�Ȃ��Ȃ�����ˁA�Ƃ肠�������߂łƂ��A�A�C���B");

                            UpdateMainMessage("�A�C���F�����A�T���L���[�B�k�u�R�O�Ƃ��Ȃ�Ɛ������������������ȁB");

                            UpdateMainMessage("���i�F�A�C�����ăz���g�o�J�Ȃ��ɁA���\�܂Ƃ��ɐ�������̂ˁB");

                            UpdateMainMessage("�A�C���F���O�ȁB�o�J�������琬�����˂����Ď��͂Ȃ�����B");

                            UpdateMainMessage("���i�F�o�J�����琬�����������̂����m��Ȃ���ˁB");

                            UpdateMainMessage("�A�C���F�������A�����J�߂�ꂽ�Ǝv���΂����R�����ȁB�܂��ǂ��A�M�������e�͂��ȁB");

                            UpdateMainMessage("�A�C���F�Ƃ肠�����_���[�W�n�ł́E�E�E�Ȃ��I");

                            UpdateMainMessage("�@�@�@�w�V���范������������\�ӏ��֍~�蒍�����I�I�I�x");

                            UpdateMainMessage("���i�F�b�E�E�E�E�E�\�E�E�E�b�o�E�E�E�o�J�A�C���Ɍ����āE�E�E");

                            UpdateMainMessage("�A�C���F�L�O�͂����_���[�W�n���Ǝv���Ă����̂��H�Â��ȃ��i�B");

                            UpdateMainMessage("���i�F���Ⴀ�������Č����̂�H");

                            UpdateMainMessage("�A�C���F�_���[�W�n����Ȃ��Ƃ��A���ʋ���Ȗ��@���B");

                            UpdateMainMessage("�A�C���F�������ȁA�����ō��A���i�l�ɓ��܂�Ă��܂����Q���S���[����Ώۂɂ��Ă݂邩�B");

                            UpdateMainMessage("���i�F�����I�E�\���񂶂���Ă��I�H");

                            UpdateMainMessage("�A�C���F�����A������Ƃǂ��ĂȁB����Ă݂邺�E�E�E");

                            UpdateMainMessage("�A�C���F���S���Ă�����Ԃ́A���̓I�ȕ���Ƃ���������������_�����ɂ��ꂽ��Ԃ��B");

                            UpdateMainMessage("�A�C���F������~����ɒ[�ɒ������Ԃ��󂢂Ă��܂�����ǂ����悤���˂���");

                            UpdateMainMessage("�A�C���F�����������܂�Ă��܂������z�ȃQ���S���[���N�Ȃ�E�E�E");

                            UpdateMainMessage("�A�C���F���_�𓝍�������ԂŁA�[�w���E�֐��荞�ރC���[�W�ŁE�E�EResurrection�I");

                            UpdateMainMessage("�@�@�@�w�Q���S���[���N�́A�W�����ɕ�܂ꂽ�Ɠ����ɍĂѓ����n�߂��I�x");

                            UpdateMainMessage("���i�F�����E�E�E��������Ȃ��I�H���ґh���ł���A�R���I�H");

                            UpdateMainMessage("�A�C���F�����Ƒ傰���ȕ\�����ȁB�����������m�ȕ\��������Ƃ���΁A�������ȁB");

                            UpdateMainMessage("���i�F�\���߂�����A����Ȃ̕��ʂ�낤�Ǝv���Ă����񂶂�Ȃ����B");

                            UpdateMainMessage("�A�C���F�������A�}�i�̏���ʂ����[����˂��B���x���A���ł���悤�ȑ㕨����˂��ȁB");

                            UpdateMainMessage("���i�F������ˁA���̖��@�̂��߂ɂ��}�i�͐ߖ񂷂���̂����A�ߓx�Ȋ��҂͏o���Ȃ���ˁB");

                            UpdateMainMessage("�A�C���F�܁A�Ƃ����킯���B�ǂ����ō��̖��@����H");

                            UpdateMainMessage("�A�C���F���i�A�����ꂨ�O�����ꂽ���́A���O�̐[�w���E�֐��荞�ރC���[�W�ŁE�E�E");

                            UpdateMainMessage("�@�@�@�w�h�o�L�O�b�V���A�A�A�@�@�I�I�I�x�i���i�̃f�X�g���N�V�����E�L�b�N���A�C�����y�􂵂��j");

                            md.Message = "���A�C���̓��U���N�V�������K�����܂�����";
                            md.ShowDialog();
                            mc.Resurrection = true;
                        }
                        if (mc.Level >= 31 && !mc.Catastrophe)
                        {
                            UpdateMainMessage("�A�C���F�悤�₭�k�u�R�O����R�P���ȁA�����܂ŗ���ƒ��X�オ��ɂ����ȁB");

                            UpdateMainMessage("���i�F�����ˁA������x�̐����������킯�����A�L�т����������ɂȂ肻���ˁB");

                            UpdateMainMessage("�A�C���F�܂����̕������M���̓��e�����͂ȃ��m�ɂȂ�͂����B");

                            UpdateMainMessage("�A�C���F��������A���U���N�V�����̑O���炢��KineticSmash������Ă���ȁB");

                            UpdateMainMessage("���i�FStanceOfEyes�̑O����Ȃ��H�܂��������ǁA�ǂ̕ӂ���M�����킯�H");

                            UpdateMainMessage("�A�C���F�񂻂��������������H�܂��ǂ����AKineticSmash�����_�����B");

                            UpdateMainMessage("�A�C���F�܂��A�ȒP�Ɍ����΁A������X�ɐ������������̂��B");

                            UpdateMainMessage("�A�C���F����ɑ΂��鎩�R�̍\������A�W���͂��ܘ_�K�v�Ȃ킯���B");

                            UpdateMainMessage("�A�C���F�����ɍX�ɉ����āA�������[�V�����A�ō����B�_�̓��o��������B");

                            UpdateMainMessage("���i�F�ւ��E�E�E���ƂȂ�StanceOfStanding�̍\���Ɏ��Ă��ˁB");

                            UpdateMainMessage("�A�C���F�������H����Ȃ���͖����񂾂��ȁA�m���Ɏ��Ă邩������˂��ȁB");

                            UpdateMainMessage("�A�C���F���ĂƁE�E�E�������I�y���ɉ��`�zCatastrophe�I�I");

                            UpdateMainMessage("�@�@�@�w�b�h�X�E�E�D�D�D���I�I�I�x�@�@");

                            UpdateMainMessage("���i�F�����������ˁE�E�E�j��I���Č������͑S�g�S��ƌ���������������H");

                            UpdateMainMessage("�A�C���F�����E�E�E�����E�E�E���ȁE�E�E���ӂ������A�}�W�Ŕ�ꂽ�I�I");

                            UpdateMainMessage("�A�C���F�ʖڂ��A�R�C�c�͑S�X�L���|�C���g���g�����܂��݂������B�P�񂵂��o���˂��B");

                            UpdateMainMessage("���i�F�S�X�L���|�C���g���Č����ƁA�P�Ƃ��Q�ł��ꉞ�\�Ȃ킯�H");

                            UpdateMainMessage("�A�C���F�����A����ďo���Ȃ����͂˂��B�����З͂͑�������Ȃɏo���˂��ȁB");

                            UpdateMainMessage("���i�F�Ȃ�ׂ��X�L���|�C���g�𗭂߂Ă����������ǂ����Ď��ˁB");

                            UpdateMainMessage("�A�C���F�܂��ȁA�Ō�̈ꌂ�K�E�ɋ߂������Ŏg���Ηǂ����ď����B");

                            md.Message = "���A�C���̓J�^�X�g���t�B�[�K�����܂�����";
                            md.ShowDialog();
                            mc.Catastrophe = true;
                        }
                        if (mc.Level >= 32 && !we.AlreadyLvUpEmpty14)
                        {
                            UpdateMainMessage("�A�C���F�����A�k�u�R�Q���BResurrection�ACatastrophe�Ɨ��Ď��͂��ȁE�E�E");

                            UpdateMainMessage("���i�F�A�C���A������Ƙb������񂾂��ǁ�");

                            UpdateMainMessage("�A�C���F�т̘b�Ȃ炨�f�肾�B");

                            UpdateMainMessage("���i�F������Ƙb���邾�����");

                            UpdateMainMessage("�A�C���F���O�Ȃ�Ł��t���Ă񂾂�H");

                            UpdateMainMessage("���i�F�ǂ�����C�C����A����ȍׂ������C�ɂ��Ȃ��ł��");

                            UpdateMainMessage("�A�C���F�����E�E�E�����������������I�I�I");

                            UpdateMainMessage("�A�C���F����I�ڊo�߂�I�@�n�A�A�@�@�@�@�@�A�I�I");

                            UpdateMainMessage("�A�C���F�E�E�E���A���������������������I�I");

                            UpdateMainMessage("�A�C���F�������������������������������������������I�I�I");

                            UpdateMainMessage("���i�F������E�E�E���\�撣���ˁE�E�E");

                            UpdateMainMessage("�A�C���F�E�I���A�A�A�A�A�A�A�A�A�A�A�A�A�@�@�@�I�I�I");

                            UpdateMainMessage("�A�C���F�@�@�@�@�@�@�y�p���`�I�I�I�z");

                            UpdateMainMessage("�A�C���F�E�E�E�E�E�E");

                            UpdateMainMessage("���i�F�b�v�E�E�E�b�v�v�A�A�C�����߂�Ȃ����A�΂�����ʖڂȂ񂾂��ǁE�E�b�t�t");

                            UpdateMainMessage("�A�C���F�E�E�E�������ȁE�E�E�������ɖ������ۂ��ȁB�߂�������ŐH�ׂ�Ƃ��邩�I");

                            UpdateMainMessage("���i�F���ɒ��߂����P�ˁB������A���܂ɂ͒��߂��̐S��B");

                            UpdateMainMessage("�A�C���F�����A�܂���肢�O�H���T���ĕ��������I�b�n�b�n�b�n�I");

                            UpdateMainMessage("���i�F�b�t�t�A���悻��B����A����_���W�����̌�A�����ĉ���Č��܂����");

                            md.Message = "���A�C���͉����K���ł��܂���ł�����";
                            md.ShowDialog();
                            we.AlreadyLvUpEmpty14 = true;
                        }
                        if (mc.Level >= 33 && !mc.Genesis)
                        {
                            UpdateMainMessage("�A�C���F�k�u�R�R���B�����I");

                            UpdateMainMessage("�A�C���F���i�A���̑O�͈ꏏ�ɔт���ăT���L���[�ȁB");

                            UpdateMainMessage("���i�F�}�Ɋ��ӂ���Ȃ�ċC����������ˁB");

                            UpdateMainMessage("�A�C���F����A�m���ɖ������ĉ���������悤�Ƃ���̂͑ʖڂȂ̂����ȁB");

                            UpdateMainMessage("���i�F�����ˁA�M����v�����Ȃ�Ă����̂͋C�܂��ꂪ��Ԃ�B");

                            UpdateMainMessage("�A�C���F����v�������̂́A�܂��ɋC�܂��ꂾ�B");

                            UpdateMainMessage("�A�C���F������x�C�܂���ł��o����s�ׂ́A����܂�ӎ����Ȃ��Ă��o���Ă邾��H");

                            UpdateMainMessage("���i�F�܂����������ˁB");

                            UpdateMainMessage("�A�C���F���ꂳ�A��肭�񂹂˂����ƍl�����񂾁B���Ƃ��ΑO�������s�ׂ����B");

                            UpdateMainMessage("�A�C���F��ԏ��߂ɂ�����s�ׂ��S�Č��_�ł���Ɖ��肵�Ă��ȁB");

                            UpdateMainMessage("�A�C���F������̍s�ׂ͑S�Ă����̌��_�ɑ΂���͕�E�ω��E���W�E��V�E���Ղ������肷��킯���B");

                            UpdateMainMessage("���i�F���H���A�����E�E�E");

                            UpdateMainMessage("�A�C���F����A�N�V�����̌��_�����K�b�`���͂�ł����΁A���S�������s�ׂ͈ӎ����Ȃ��ōςށB");

                            UpdateMainMessage("�A�C���F�܂�����A�N�V�����̈�Ⴞ�B�_�u���E�X���b�V���I�I���@�I");

                            UpdateMainMessage("�A�C���F�����đS�Ă͌��_�ɂ��č݂�B���_�ɗ����Ԃ鎖�ŕ҂ݏo����閂�@�AGenesis�I");

                            UpdateMainMessage("�A�C���F������������s�����A�_�u���E�X���b�V���I");

                            UpdateMainMessage("���i�F�����A�������ƑS�������Z�ˁB���̖��@�œ����s�ׂ��J��o����킯�H");

                            UpdateMainMessage("�A�C���F�������ȁB���������̖��@�͑S�R�ӎ����Ȃ��Ă��ǂ�����R�X�g�O���Ă킯���B");

                            UpdateMainMessage("�A�C���F�X�ɑO��s�ׂ͏����ς݂�����A�O��x���������@�R�X�g�E�X�L��������O���Ă킯���B");

                            UpdateMainMessage("���i�F������A�����ɔ������ۂ���ˁB");

                            UpdateMainMessage("�A�C���F�Q��ڈȍ~�œ����s�ׂ����������������A�C�y�Ȗ��@�����ӊO�Ǝg�����͓�������ȁB");

                            UpdateMainMessage("���i�F����ł��A�������@���ۂ���B���͂��������̋�肾�ȁB");

                            UpdateMainMessage("�A�C���F�܂��A���̖��@�͎������g�ɂ����������˂�����B���Ȃ�ɉ����R���{������Ă����B");

                            md.Message = "���A�C���̓W�F�l�V�X���K�����܂�����";
                            md.ShowDialog();
                            mc.Genesis = true;
                        }
                        if (mc.Level >= 34 && !mc.SoulInfinity)
                        {
                            UpdateMainMessage("�A�C���F�k�u�R�S�E�E�E���\����������ȁA�{���ɁB");

                            UpdateMainMessage("���i�F�����܂ŗ��ĉ������ɐZ���Ă�̂�B�z���z���A���������Ǝv���t���Ă�B");

                            UpdateMainMessage("�A�C���F�������̖������Ȓ����́H�������A���傤���˂��Ȃ��E�E�E");

                            UpdateMainMessage("�A�C���F�������Ǝv�����ƌ����΁A����σA�����ȁB�_���[�W�n����I�I");

                            UpdateMainMessage("���i�F������E�E�E���܂����B�o�J�A�C�����o�J�����Ď���������Y��Ă���B");

                            UpdateMainMessage("�A�C���F�����A���Ƃł������I�_���[�W�o�J���I�b�n�b�n�b�n�I");

                            UpdateMainMessage("�A�C���F��������A��������D�������I�����������A�y���ɉ��`�zSoulInfinity�I�I");

                            UpdateMainMessage("���i�F����I�������˕���������B����Ȃ̓��������琳���܂Ƃ��ɗ����Ă��Ȃ����B");

                            UpdateMainMessage("�A�C���F���̍��g�͂������E�E�E�́E�Z�E�m�E�S�̑S�p�����^���d�v�ɂȂ��Ă��������B");

                            UpdateMainMessage("�A�C���F�ǂ�����ɕ΂点����ʖڂ��B�S�Ă��܂�ׂ�Ȃ��グ�������l�͍����ȁB");

                            UpdateMainMessage("���i�F�����Ƃ��ƂƎv���������������āA�Ƃ肠�������ł��t���Ă݂��悤�Ȋ����ˁB");

                            UpdateMainMessage("�A�C���F�����A�m���ɂ������ȁB�����A�_���[�W�l�`�w���̂̓p�����^�̊���U�莟��ł͈�Ԃ����ȁB");

                            UpdateMainMessage("���i�F�ł��A������x�΂��Ă��Ă��\�R�\�R�̃_���[�W�͊��҂ł���񂶂�Ȃ��H");

                            UpdateMainMessage("�A�C���F�������ȁA�X�L�����������قǑ����킯����˂��B�C������������o���o���g���Ă������I");

                            md.Message = "���A�C���̓\�E���E�C���t�B�j�e�B���K�����܂�����";
                            md.ShowDialog();
                            mc.SoulInfinity = true;
                        }
                        if (mc.Level >= 35 && !mc.ImmortalRave)
                        {
                            UpdateMainMessage("�A�C���F�k�u�R�T�͊������񂾂��E�E�E");

                            UpdateMainMessage("���i�F���˘f���Ă�̂�A�n�b�L�������Ȃ�����B");

                            UpdateMainMessage("�A�C���F�Ζ��@�Ŏv�������񂾂��A���������̂̓A���Ȃ̂��ǂ����B");

                            UpdateMainMessage("���i�F�ǂ�Ȍ��ʂ�H���āA�o�J�A�C���̏ꍇ�̓_���[�W�n��ˁH");

                            UpdateMainMessage("�A�C���F�΂̖��@�ƌ����Γ��R�_���[�W�n���B");

                            UpdateMainMessage("���i�F�g���W��������ˁA�A�C���B");

                            UpdateMainMessage("�A�C���F�����A���ړI�ȃ_���[�W�΂��肶��ʔ����͂˂��B");

                            UpdateMainMessage("���i�F����ƒ��ڈȊO�̎����l����悤�ɂȂ����̂ˁB");

                            UpdateMainMessage("�A�C���F�����ł��A�΂̖��@�����̎��͂ł�����x�~������Ă̂͂ǂ����H");

                            UpdateMainMessage("���i�F�~����H�ǂ�ȕ��ɂ�B");

                            UpdateMainMessage("�A�C���F���͂����ImmortalRave�Ɩ������邺�A�I���@�I�t�@�C�A�{�[���I");

                            UpdateMainMessage("�@�@�@�w�A�C���̎��͂ɏ�����������Y���n�߂��x");

                            UpdateMainMessage("���i�F�ւ��Ȃ�قǁA�ʔ�����ˁB");

                            UpdateMainMessage("�A�C���F������A�t���C���X�g���C�N�I");

                            UpdateMainMessage("�@�@�@�w�A�C���̎��͂ɑ傫�߂̉�����Y���n�߂��x");

                            UpdateMainMessage("�A�C���F����Ń��X�g���I���H���J�j�b�N�E�F�C�u�I");

                            UpdateMainMessage("�@�@�@�w�A�C���̎��͂Ƀo�J�f�J�C������Y���n�߂��x");

                            UpdateMainMessage("���i�F�����ɂR���~����Ȃ�āE�E�E�A�C�����܂ɐ�����ˁB");

                            UpdateMainMessage("�A�C���F�������A�X�g���[�g�X�}�b�V���I���@�I");

                            UpdateMainMessage("�A�C���F�����ē����ɁA�t�@�C�A�{�[�����I");

                            UpdateMainMessage("�A�C���F���X�������A�_�u���E�X���b�V���I");

                            UpdateMainMessage("�A�C���F�����ɁA�t���C���X�g���C�N�I");

                            UpdateMainMessage("�A�C���F�I���I���I���@�I���X�g�A�L�l�e�b�B�N�X�}�b�V���I");

                            UpdateMainMessage("�A�C���F�����������ɐH�炦�A���H���J�j�b�N�E�F�C�u�I");

                            UpdateMainMessage("���i�F���A������E�E�E��������Ȃ��H");

                            UpdateMainMessage("�A�C���F�����A���������m��Ȃ��ȁB�������͂���Ă݂���I");

                            UpdateMainMessage("���i�F���������������@�Ȃ̂ɁA�Ō�̌��t�g�����Ԉ���Ă���B");

                            UpdateMainMessage("�A�C���F���I�H���܂����A���Ȃ�J�b�R�悭���߂����肪�E�E�E");

                            UpdateMainMessage("���i�F�܂��A���̂R�A���͂��Ȃ�_���[�W���ł������ˁB���҂��Ă���B");

                            UpdateMainMessage("�A�C���F���A�����A�C���Ă������āI�b�n�b�n�b�n�I");

                            md.Message = "���A�C���̓C���[�^���E���C�u���K�����܂�����";
                            md.ShowDialog();
                            mc.ImmortalRave = true;
                        }
                        if (mc.Level >= 36 && !mc.PainfulInsanity)
                        {
                            UpdateMainMessage("�A�C���F�k�u�R�U���ď����ȁB�l�`�w�S�O�܂ł��ƂS�Ɣ��������I");

                            UpdateMainMessage("���i�F�����܂ŗ���ƑM�����e�����Ȃ苭��Ȃ񂶂�Ȃ��H");

                            UpdateMainMessage("�A�C���F�����A���́u�S��v�n�̃X�L���łǂ����Ă������Ă݂����̂�����񂾁B");

                            UpdateMainMessage("���i�F��̂ǂ�Ȃ̂�H");

                            UpdateMainMessage("�A�C���F�ܘ_�A�_���[�W�n���I�I�I");

                            UpdateMainMessage("���i�F�E�E�E���A�E�\�A�{�C�Ȃ́H");

                            UpdateMainMessage("�A�C���F�{�C�Ɍ��܂��Ă邾��I�@�b�n�b�n�b�n�I�I");

                            UpdateMainMessage("���i�F�z���g�_���[�W�n�΂�����Ȃ̂ˁB�ł��u�S��v�n�Ȃ񂩂łǂ���������H");

                            UpdateMainMessage("�A�C���F����̐S�̒���T��񂶂�Ȃ��Ă��ȁB���ڈ������銴�����B");

                            UpdateMainMessage("�A�C���F������Ƃ����̃~���[�~���[�t�����[�Ŏ������Ă݂邺�B");

                            UpdateMainMessage("���i�F���A�~�߂Ă�B�~���[�~���[�t�����[�����z����Ȃ��̂�B");

                            UpdateMainMessage("�A�C���F���Ⴀ�A�ǂ�ł����Ă����񂾁B");

                            UpdateMainMessage("���i�F�����̃Q�o�Q�o�N�ŗǂ��񂶂�Ȃ��H");

                            UpdateMainMessage("�A�C���F�I�[�P�[�I�[�P�[�B���Ⴀ����Ă݂邺�B�������I�y���ɉ��`�zPainfulInsanity�I");

                            UpdateMainMessage("�@�@�@�w�A�C���͏W�������፷������x�Q�o�Q�o�N�ւƌ������I�x");

                            UpdateMainMessage("�A�C���F������A����Ŋ������B");

                            UpdateMainMessage("���i�F���H��̂ǂ���������B");

                            UpdateMainMessage("�A�C���F���A�����̃Q�o�Q�o�N�͐S�̒��Ŋi�������B");

                            UpdateMainMessage("���i�F�N�Ƃ�H");

                            UpdateMainMessage("�A�C���F���̊፷���Ɗi�������I");

                            UpdateMainMessage("���i�F���̊ԁA�A�C�����g�͎��R�ɓ�����́H");

                            UpdateMainMessage("�A�C���F�����A�����܂ł��Q�o�Q�o�N�̐S�̒��Ɋ፷�����h�点���B���͓����邺�B");

                            UpdateMainMessage("�@�@�@�w�Q�o�Q�o�N�̓������݂��Ă����B�_���[�W�������Ă���悤���B�x");

                            UpdateMainMessage("�A�C���F���Ȃ݂ɂ��̊፷���͉������S���邩�Q�o�Q�o�N�����ʂ܂ł͐�΂ɉ����ł��˂��B");

                            UpdateMainMessage("���i�F���Ȃ�l�`�������X�L���ˁB�ł����͂���B");

                            UpdateMainMessage("�A�C���F�����̂��߂̃X�L���ʂ͌��\�H�����A��x���܂�Ζ��^�[���_���[�W���B");

                            UpdateMainMessage("�@�@�@�w�Q�o�Q�o�N�̓������~�܂����B�ǂ����V�ɏ����ꂽ�悤���B�x");

                            UpdateMainMessage("�A�C���F�Q�o�Q�o�N�A���܂˂��ȁB���U���N�V�����I");

                            UpdateMainMessage("���i�F����I�����������Ă�̂�I�H�ǂ�����Ȃ��̂�A����Ȃ́B");

                            UpdateMainMessage("�A�C���F�������Ă�A�����ڂ͋C�����������R�C�c�͖��Q���B");

                            UpdateMainMessage("���i�F��܂����傤���Ȃ����E�E�E�A�C���A�S��n�X�L���Ń_���[�W�͒������Ǝv����B");

                            UpdateMainMessage("�A�C���F�����A�������i���_���[�W������ȁA�{�X��Ȃ񂩂ł͌��\�g���邺�I�C���Ă����ȁI");

                            md.Message = "���A�C���̓y�C���t���E�C���T�j�e�B���K�����܂�����";
                            md.ShowDialog();
                            mc.PainfulInsanity = true;
                        }
                        if (mc.Level >= 37 && !mc.CelestialNova)
                        {
                            UpdateMainMessage("�A�C���F����Ƃk�u�R�V���B�����܂ŗ���΂l�`�w�S�O�܂ł��Ƃ킸�����B");

                            UpdateMainMessage("���i�F�A�C�����Ă��A�z���b�g�_���[�W�n���������ˁB");

                            UpdateMainMessage("�A�C���F�����A�������ȁB");

                            UpdateMainMessage("���i�F���Ń_���[�W�n�ɂ������̂�H");

                            UpdateMainMessage("�A�C���F����Ⴀ�A�_���[�W�������������������B");

                            UpdateMainMessage("���i�F���Ń_���[�W����������������̂�H");

                            UpdateMainMessage("�A�C���F�_���[�W��������΃��C�t������B���R���̕�����������B");

                            UpdateMainMessage("���i�F�_���[�W�ȊO�ŋ����̂�����Ƃ͎v��Ȃ��H");

                            UpdateMainMessage("�A�C���F�ŏI�I�ɂ̓_���[�W�n����B�Ƃ����킯�ł��A�v�������̂��R�����B");

                            UpdateMainMessage("���i�F�������I����������ˁE�E�E�ǂ�Ȃ̂�H");

                            UpdateMainMessage("�A�C���F�_���[�W���Ă͍̂U�����Ǝv��ꂪ�������A���C�t�񕜂�����Ӗ��_���[�W���B");

                            UpdateMainMessage("�A�C���F���C�t�񕜂̓_���[�W�U���̔��΂݂����Ȃ��̂�����ȁB");

                            UpdateMainMessage("�A�C���F�틵�ɂ���ẮA�U���Œׂ��������ǂ��ꍇ������B");

                            UpdateMainMessage("�A�C���F�܂����̐틵�ł́A�_���[�W���[�X�̒��ŉ񕜂ɓO���������ǂ���������B");

                            UpdateMainMessage("���i�F�܂��ꍇ�ɂ���ĉ񕜂��������ƍU�����������͈Ⴄ��ˁB");

                            UpdateMainMessage("�A�C���F�ƁA�����킯���B���̖��@��CelestialNova�Ɩ��t���邺�I");

                            UpdateMainMessage("�A�C���F���i�A���ق��I");

                            UpdateMainMessage("���i�F���A���C�t�񕜂ˁB���肪�Ɓ�");

                            UpdateMainMessage("�A�C���F�������@�������́w�_�~�[�f�U��N�x�ɂԂ��Ă݂邺�I");

                            UpdateMainMessage("���i�F���A�_���[�W����������I�@������Ɖ��悻��I�H");

                            UpdateMainMessage("�A�C���F���ŋ󒆂�؂鎞�̌������̐S�Ȃ񂾁B���փX���C�h����������񕜁B");

                            UpdateMainMessage("�A�C���F�󒆂�؂�ہA���֓˂��グ������փX���C�h������̂��_���[�W���Ă킯���B");

                            UpdateMainMessage("�A�C���F���łɂ��ȁB���̖��@��łu�ԁA�̂̏d�S�����ɂ��Ă����΃_���[�W�̐��ɂ��₷���B");

                            UpdateMainMessage("�A�C���F�t�ɗ����̂����A�t�����̑���O�q�ɏo���Ă����΁A�񕜑��փV�t�g���₷���B");

                            UpdateMainMessage("�A�C���F���ĂȂ킯���B���������\�����炱�̖��@���J��o���΁A���ł��ǂ������������������邺�I");

                            UpdateMainMessage("�A�C���F�b�n�b�n�b�n�b�n�I");

                            UpdateMainMessage("���i�F�E�E�E�A�C���B");

                            UpdateMainMessage("�A�C���F�ǂ������I�r�r���Đ����o�˂����I�b�n�b�n�b�n�b�n�b�n�I");

                            UpdateMainMessage("���i�F�A�C���E�E�E���̃��F���[����ɂ��������B");

                            UpdateMainMessage("�A�C���F�b�n�b�n�b�n�b�n�I�������Ԃ肾�I�C�ɂ���Ȃ��āI");

                            UpdateMainMessage("���i�F���A�����H������ˁB����ȃo�J�A�C���Ɍ����ĂˁE�E�E");

                            md.Message = "���A�C���̓Z���X�e�B�A���E�m���@���K�����܂�����";
                            md.ShowDialog();
                            mc.CelestialNova = true;
                        }
                        if (mc.Level >= 38 && !mc.LavaAnnihilation)
                        {
                            UpdateMainMessage("�A�C���F�k�u�E�E�E�R�W���I�I�I");

                            UpdateMainMessage("���i�F�₯�ɋC�������Ă��ˁB���ɉ����M���Ă���킯�H");

                            UpdateMainMessage("�A�C���F�����A���R�M���Ă���B");

                            UpdateMainMessage("���i�F�ŏ�����M���Ă�����Ď��́E�E�E�_���[�W�n���ۂ���ˁB");

                            UpdateMainMessage("�A�C���F�����I����������̂͂����̃_���[�W�n����˂��񂾁I");

                            UpdateMainMessage("�A�C���F���������������I�I�I�������I�I�I�@LavaAnnihilation�I�I�I");

                            UpdateMainMessage("�@�@�@�w�S�S�b�S�H�H�I�H�I�I�V�������A�A�A�@�@�@���I�I�I�x�@�@");

                            UpdateMainMessage("�A�C���F�ǂ����A����I���i�I�Ă��쌴��Ԃ̊������I�I�I");

                            UpdateMainMessage("���i�F�_���W�����Q�[�g�̗��ɖ쌴�͖������B�����������ꒃ�ˁB");

                            UpdateMainMessage("�A�C���F�^�[�Q�b�g�Ƃ��ΏۂƂ�����Ƃ��֌W�˂��A�S���Ă��s�������I");

                            UpdateMainMessage("���i�F�_���W�������ł͈�̂Â���ɂ���̂ɁE�E�E�z���b�g�h��D���ˁB");

                            UpdateMainMessage("�A�C���F�E�E�E���܂����������I�����������I�I��̂Â����o�Ă��˂������I�H");

                            UpdateMainMessage("���i�F�����A�Y��Ă��̂ˁB����σo�J�A�C���̓o�J�ł����Ȃ���ˁ�");

                            UpdateMainMessage("�A�C���F����ϖ��@�R�X�g�팸�őΏۂƂ��ėǂ����HMiniAnnihilation�Ƃ��ʖڂ��H");

                            UpdateMainMessage("���i�F�ʖڂȂ񂶂�Ȃ��H��������������������P�����B");

                            UpdateMainMessage("�A�C���F���������������E�E�E�R�X�g��������߂��Ȃ񂾂��́B�������E�E�E");

                            UpdateMainMessage("�A�C���F�E�I�H���@�@�@�I�I�R����R����I�I�b�n�b�n�b�n�b�n�b�n�I�I�I");

                            UpdateMainMessage("���i�F�ǂ����̎O���{�X�݂����Ȏ����ĂȂ��ŁA�c��}�i�ɂ͒��ӂ��Ă�ˁB");

                            UpdateMainMessage("�A�C���F�����A���𗹉��E�E�E");

                            md.Message = "���A�C���̓����@�E�A�j�q���[�V�������K�����܂�����";
                            md.ShowDialog();
                            mc.LavaAnnihilation = true;
                        }
                        if (mc.Level >= 39 && !we.AlreadyLvUpEmpty15)
                        {
                            UpdateMainMessage("�A�C���F�k�u�R�X�E�E�E�l�`�w�܂ł��ƈ���B");

                            UpdateMainMessage("�A�C���F���i�A�������A�߂�����т��ꏏ�ɐH�ׂ悤���B");

                            UpdateMainMessage("���i�F���H�ׂ悤���H");

                            UpdateMainMessage("�A�C���F�y�ɖˁE�җ����z�͂ǂ����H�������̖˂͏�肢���B");

                            UpdateMainMessage("���i�F���ˌn�͂���܂�D�݂���Ȃ��̂�B���ɂȂ��H");

                            UpdateMainMessage("�A�C���F�y�G�X�e�E�������U�E�A�X�y�����e�z�͂ǂ����H");

                            UpdateMainMessage("���i�F�������̃j�R�j�R�p�t�F�͔���������� �s���܂����");

                            UpdateMainMessage("�A�C���F�E�E�E�n�n�B�@�����˂������ȁB���������̂�");

                            UpdateMainMessage("���i�F�₯�ɒ��߂�������������Ȃ��B");

                            UpdateMainMessage("�A�C���F�܂��ȁB�O��ł������ɖ��������Ă̂������������A�؂�ւ��Ȃ��ƂȁB");

                            UpdateMainMessage("���i�F�A�C���͕��i�͊O�ŐH�ׂȂ��́H");

                            UpdateMainMessage("�A�C���F����A���܂ɊO�ŐH�ׂ邯�ǂȁB���ꂪ�ǂ������H");

                            UpdateMainMessage("���i�F������A�ʂɉ��ł��B");

                            UpdateMainMessage("�A�C���F���ς�炸���������錾�������ȁB�܂��ǂ����ǂȁB");

                            UpdateMainMessage("���i�F�����A�����s���������B");

                            UpdateMainMessage("�A�C���F��H");

                            UpdateMainMessage("���i�F�����s���������A��������A�A�C��������H");

                            UpdateMainMessage("�A�C���F��H�����A�E�}�C���Ȃ�ǂ��ł��ǂ����I");

                            UpdateMainMessage("���i�F�b�t�t�A�ςȃA�C���ˁB");

                            UpdateMainMessage("�A�C���F���ȁI�ǂ����ςȂ񂾂�I�H");

                            UpdateMainMessage("���i�F�ǂ����炢������A�������A���Ⴀ�s���܂����");

                            md.Message = "���A�C���͉����K���ł��܂���ł�����";
                            md.ShowDialog();
                            we.AlreadyLvUpEmpty15 = true;
                        }
                        if (mc.Level >= 40 && !mc.EternalPresence)
                        {
                            UpdateMainMessage("�A�C���F����������������I�l�`�w�S�O�����I�I");

                            UpdateMainMessage("���i�F�A�C���A�l�`�w�k�u���e���߂łƂ��B");

                            UpdateMainMessage("�A�C���F�����A�����܂ŗ��ꂽ�̂͏����Ɋ������v�����B");

                            UpdateMainMessage("�A�C���F���Ȃ�ɁA���낢��M���Ă������A���X�g�͉��炵���s�����B");

                            UpdateMainMessage("���i�F�t�B�j�b�V���u���[�Ƃ�����Ȃ��ł��傤�ˁH");

                            UpdateMainMessage("�A�C���F������A�Ō�̓_���[�W����˂��B�p�����^�t�o���B");

                            UpdateMainMessage("���i�F�ǂ������グ������������H");

                            UpdateMainMessage("�A�C���F���i�A�������͕��i�A��������Đ���Ă���B�ƌ������痝���ł��邩�H");

                            UpdateMainMessage("���i�F���H����Ȏ��͖����ł���B���̃��C�g�j���O�L�b�N�͖{�C���");

                            UpdateMainMessage("�A�C���F�܂��E�E�E�\�R�͎�������Ă����B���ȁA�}�W�ŁB");

                            UpdateMainMessage("�A�C���F�Ώۂ����ł���A�l���s�����N�����ꍇ�A������x���͂ɑ΂���C�������Ă̂�����B");

                            UpdateMainMessage("�A�C���F�����A�������͂����F���͂��Ă˂��B�����܂ł�����ɂ������Ă邾�����B");

                            UpdateMainMessage("���i�F���������C�����𖳂������Ă����́H");

                            UpdateMainMessage("�A�C���F��������˂��B���������s�����Ă���@���̊�_��ς�����Ęb���B�C�����͓��R���邪�B");

                            UpdateMainMessage("�A�C���F�w���Ӂx�̂悤�ȍs���������܂߂�B�ƌ����Βʂ��邩�H");

                            UpdateMainMessage("���i�F��܂��A�ǂ����J���i�C���ǂ��B����Ă݂��Ă�B");

                            UpdateMainMessage("�A�C���F���Ⴀ�A������Ƃ���Ă݂邺�E�E�E�����͂������ȁB�@EternalPresence���I�I");

                            UpdateMainMessage("�@�@�@�w�A�C���̎���ɐV�����@���ƌ������������n�߂��I�I�I�x�@�@");

                            UpdateMainMessage("�A�C���F������A�R�C�c�͂��������B�����f�B�̃{�P��������g���Ă��񂾂낤�ȁB");

                            UpdateMainMessage("�A�C���F�U���́A�h��́A���@�U���́A���@�h��͂t�o���B");

                            UpdateMainMessage("���i�F������ˁE�E�E�������������������f�B�X���t������Ɏ��Ă��A�m���ɁB");

                            UpdateMainMessage("���i�F�����ɗ����Ă邾���ŁA�w�C�����悭�Ԃ��ׂ���x�����`����Ă����ˁB");

                            UpdateMainMessage("�A�C���F���A���₢��B�ʂɂ��������킯����˂����ǂȁB");

                            UpdateMainMessage("�A�C���F����Ȃ�A���߂Ă��̃{�P�t���Ɍܕ��œn�荇�������ȋC�����邺�B");

                            UpdateMainMessage("���i�F����A�C�P���悫���ƁB���̃_���W�������e�����炨�t������ɉ���Ă݂���H");

                            UpdateMainMessage("�A�C���F�����A�������ȁB�҂��Ă��A�N�\�{�P�����f�B�B�������Ă߂��ׂ͒����I");

                            md.Message = "���A�C���̓G�^�[�i���E�v���[���X���K�����܂�����";
                            md.ShowDialog();
                            mc.EternalPresence = true;
                        }
                    }
                }
                #endregion
                #region "���i�̃��x���A�b�v"
                if (sc != null)
                {
                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.Manual;
                        md.Location = new Point(this.Location.X, this.Location.Y + this.Size.Height);
                        md.NeedAnimation = true;

                        if (sc.Level >= 3 && !sc.IceNeedle)
                        {
                            UpdateMainMessage("���i�F��������A�A�C���B�k�u�R�ɂȂ�����B");

                            UpdateMainMessage("�A�C���F�����A��邶��˂����B�O���b�c�I");

                            UpdateMainMessage("���i�F�����Ƃ肠�����M���͖̂��@����ɂ����B");

                            UpdateMainMessage("�A�C���F�����A���҂��Ă邺�B�����Ă����B");

                            UpdateMainMessage("���i�F�{���͐����������ǁA�X���痈��C���[�W��B�g���h���h�ɂ��Ă������B");

                            UpdateMainMessage("�A�C���F���E�E�E�����E�E�E");

                            UpdateMainMessage("���i�F���̕X�̐n�Łg���Ɂh�𖡂키���ǂ���A Ice Needle�I�I");

                            UpdateMainMessage("�@�@�@�w�K�J�J�J�J�J�I�@�V���h�h�h�I�I�x�@�@");

                            UpdateMainMessage("�A�C���F���A���i�B����܂�ߌ��Ȕ����͍T�����ȁA�I�}�G�ꉞ���������E�E�E");

                            UpdateMainMessage("���i�F�ǂ�����Ȃ���@�A�C���Ȃ񂩂Ԃ��ׂ��Ă���I�I�I�@�����C���[�W�����́�");

                            UpdateMainMessage("�A�C���F�����������y�����Ɍ�����ȁB���A���ŕ|������Ȃ����B�b�n�b�n�b�n�E�E�E");

                            UpdateMainMessage("���i�F���v��A���A��������S�z���Ȃ��łˁ�");

                            md.Message = "�����i�̓A�C�X�E�j�[�h�����K�����܂�����";
                            md.ShowDialog();
                            using (MessageDisplay md2 = new MessageDisplay())
                            {
                                md2.StartPosition = FormStartPosition.Manual;
                                md2.Location = new Point(this.Location.X, this.Location.Y + this.Size.Height);
                                md2.NeedAnimation = true;
                                md2.Message = "�����i�̖��@�g�p���������܂�����";
                                md2.ShowDialog();
                            }
                            sc.AvailableMana = true;
                            sc.IceNeedle = true;
                        }
                        if (sc.Level >= 4 && !sc.CounterAttack)
                        {
                            UpdateMainMessage("���i�F���x���A�b�v��B���āA����͉��ɂ��悤���ȁB");

                            UpdateMainMessage("�A�C���F�h�䖂�@�Ȃ�Ă̂͂ǂ����H");

                            UpdateMainMessage("���i�F���ږ��@�U�����ǂ����ǁA�X�L���U�����K�v��ˁB");

                            UpdateMainMessage("�A�C���F�h��̍\��������X�L���Ȃ�Ă̂͂ǂ����H");

                            UpdateMainMessage("���i�F����I����������");

                            UpdateMainMessage("�A�C���F�b�t�D�E�E�E�ŁA�ǂ�Ȗh��Ȃ񂾁A���ۂ́H");

                            UpdateMainMessage("���i�F�����ɂ������Ă��Ȃ�����A�A�C���B");

                            UpdateMainMessage("�A�C���F�ǂ��̂��H���Ⴀ�������E�E�E�I���@�I");

                            UpdateMainMessage("���i�F�h��Ȃ�Ă���͂���������Ȃ��A�@Counter Attack��B�@�b�n�A�@�I�I");

                            UpdateMainMessage("�@�@�@�w�b�o�R���I�I�I�x�i���i�̃J�E���^�[���A�C���݂̂��������y�􂵂��j�@�@");

                            UpdateMainMessage("�A�C���F�b�O�I�b�Q�G�F�F�F�E�E�E�I�A�I�}�G����̂ǂ����h�q�Ȃ񂾂�E�E�E");

                            UpdateMainMessage("���i�F�������I�N���[���q�b�g�ˁ�@�������A�C���A�ア�ア���");

                            UpdateMainMessage("�A�C���F�E�E�E�����炢�͖h�q�I�Ȃ��̂ɂ����A���ȁE�E�E�b�n�b�n�b�n�E�E�E");

                            UpdateMainMessage("�@�@�@�y�E�E�E�p�^�b�E�E�E�z�i�A�C���͋C���������j�@�@");

                            md.Message = "�����i�̓J�E���^�[�E�A�^�b�N���K�����܂�����";
                            md.ShowDialog();
                            using (MessageDisplay md2 = new MessageDisplay())
                            {
                                md2.StartPosition = FormStartPosition.Manual;
                                md2.Location = new Point(this.Location.X, this.Location.Y + this.Size.Height);
                                md2.NeedAnimation = true;
                                md2.Message = "�����i�̃X�L���g�p���������܂�����";
                                md2.ShowDialog();
                            }
                            sc.AvailableSkill = true;
                            sc.CounterAttack = true;
                        }
                        if (sc.Level >= 5 && !sc.DarkBlast)
                        {
                            UpdateMainMessage("���i�F�X�̐j�A�݂������J�E���^�[�B���낢��l�������ˁE�E�E");

                            UpdateMainMessage("�A�C���F���i�A�悭�����B���������ĕ����񂾁B");

                            UpdateMainMessage("���i�F����A�������X�O�M�����Ȃ񂾂���A�ז����Ȃ��ł�ˁB");

                            UpdateMainMessage("�A�C���F�I�}�G����������ȁB���܂ɂ͖h��Ƃ��l�����A���ȁI�H");

                            UpdateMainMessage("���i�F�������Ɩh�䂵�Ȃ��Ⴂ���Ȃ����P�H�H");

                            UpdateMainMessage("�A�C���F���A����܂������������P����˂����ǂ��B���Č����񂾁B");

                            UpdateMainMessage("���i�F���A���܂�ˁB��������A�g�����̈Łh����̍U���Ȃ�Ăǂ��B");

                            UpdateMainMessage("�A�C���F�܁A�҂đ҂āI�~�߂�I���ɂ����������ȁI�I");

                            UpdateMainMessage("���i�F���̐^�����Ȕg���ɑς����邩����E�E�EDarkBlast�Ɩ��������B�H�炢�Ȃ����I");

                            UpdateMainMessage("�A�C���F���E�E�E�ʂ��������������������E�E�E");

                            UpdateMainMessage("���i�F�t�t�t�A�A�C���ꂵ�����˂��B���I����͉����������");

                            UpdateMainMessage("�A�C���F�_��A���Ɉ��炬��^�����܂��E�E�E");
                            md.Message = "�����i�̓_�[�N�E�u���X�g���K�����܂�����";
                            md.ShowDialog();
                            sc.DarkBlast = true;
                        }
                        if (sc.Level >= 6 && !sc.AbsorbWater)
                        {
                            UpdateMainMessage("���i�F�A�C���͂k�u�U�̎��͉���M�����́H");

                            UpdateMainMessage("�A�C���F�����H�����A�m���v���e�N�V�������ȁB�����h��t�o�������B");

                            UpdateMainMessage("���i�F�������A���Ⴀ�������܂ɂ͂���Ă݂�Ƃ����B");

                            UpdateMainMessage("�A�C���F���肪���i�ɑ΂��ăE�w�w�i�΁j�ƏP���Ă���̂��C���[�W����Ɨǂ����B");

                            UpdateMainMessage("�@�@�@�w�b�o�L�I�I�I�x�i���i���A�C���Ƀ~�h���L�b�N�����܂����j�@�@");

                            UpdateMainMessage("�A�C���F���Ă��ȁI���ŉ����R���Ă񂾂�B");

                            UpdateMainMessage("���i�F���邳���A�ق�A�o�J�A�C���B�@�C���[�W����ƕ|��������Ȃ��z���g�E�E�E");

                            UpdateMainMessage("�A�C���F�ǂ�ȃC���[�W�����񂾂�E�E�E�܂�����͒u���Ƃ��āA�ǂ����H");

                            UpdateMainMessage("���i�F���̏��_���傫�ȋ��̂ŗD������ݍ���ł����̂͂ǂ��H�@AbsorbWater��B");

                            UpdateMainMessage("�A�C���F�\�z�ȏ�ɐ����Ƃ��ꂢ�ȃC���[�W���ȁB�ǂ�Ȍ��ʂ��H");

                            UpdateMainMessage("���i�F���������痈��G�l���M�[���z�����Ă����B�܂薂�@�h��ˁB");

                            UpdateMainMessage("�A�C���F���Ώo���邶��Ȃ����B�債�����񂾂ȁB");

                            UpdateMainMessage("���i�F�ƁA���R����Ȃ��A���̂��炢�B�債�Đ������������B");

                            UpdateMainMessage("�A�C���F����A���̒��q�Ŋ撣���B���҂��Ă邺�I");

                            UpdateMainMessage("���i�F���A�����A���܂ɂ͖h�삵�Ă����邩��ˁB");

                            md.Message = "�����i�̓A�u�\�[�u�E�E�H�[�^���K�����܂�����";
                            md.ShowDialog();
                            sc.AbsorbWater = true;
                        }
                        if (sc.Level >= 7 && !sc.StanceOfFlow)
                        {
                            UpdateMainMessage("���i�F���x���A�b�v�A���x���A�b�v���Ɓ�");

                            UpdateMainMessage("�A�C���F�i�C�X���A���i�B���͉���M�����肾�H");

                            UpdateMainMessage("���i�F�����Ɛ�p�ɕ����~�����̂�ˁB�����w�_�x�̃C���[�W��B");

                            UpdateMainMessage("�A�C���F�_�炩����p���H�悭�킩��˂����ǂȁB");

                            UpdateMainMessage("���i�F�����邪�������Ă��邶��Ȃ��BStanceOfFlow�ƌĂԎ��ɂ����B");

                            UpdateMainMessage("�A�C���F��������A��������H");

                            UpdateMainMessage("���i�F�K����U�����悤�ɂ����B��������΁A����̏o������ɂȂ�B");

                            UpdateMainMessage("�A�C���F��U�Ȃ񂩎���Ă���A��������H");

                            UpdateMainMessage("���i�F�K��������������Ȃ����Ęb��A���K���Ƃ�����ł���H");

                            UpdateMainMessage("�A�C���F����E�E�E�܂��A��������H");

                            UpdateMainMessage("���i�F�E�E�E�A�C���Ȃ񂩁E�E�E����");

                            UpdateMainMessage("�A�C���F�����I�I�@���A����A���₢�₢�₢��I�@���������I�I�I");

                            UpdateMainMessage("���i�F���K���̍�A��΂ɍ\�z���Č����邩��A���ĂȂ�����z���g�B");

                            UpdateMainMessage("�A�C���F��A���������I�y���݂ɂ��Ă邺�I�b�n�b�n�b�n�b�n�I�I�I");

                            md.Message = "�����i�̓X�^���X�E�I�u�E�t���[���K�����܂�����";
                            md.ShowDialog();
                            sc.StanceOfFlow = true;
                        }
                        if (sc.Level >= 8 && !we.AlreadyLvUpEmpty21)
                        {
                            UpdateMainMessage("���i�F�E�E�E�����ˁA���̕����ǂ�������B�E�E�E���`�����σ_�����E�E�E");

                            UpdateMainMessage("�A�C���F������Ă񂾁A���i�H");

                            UpdateMainMessage("���i�F�����Č��K������B���A�����Ă������ǃA�C���ɂ͊֌W�Ȃ�����ˁB");

                            UpdateMainMessage("�A�C���F���A���₢��A���x���A�b�v���̑M���͂ǂ����񂾁H");

                            UpdateMainMessage("���i�F�p�X��B");

                            UpdateMainMessage("�A�C���F���A���₢��A�p�X���āE�E�E");

                            UpdateMainMessage("���i�F�p�X���Č����Ă邶��Ȃ��A�b�`�s���ĂĂ�I�I�o�J����Ȃ��́I�H");

                            UpdateMainMessage("�A�C���F���A�����E�E�E��股��");

                            UpdateMainMessage("�A�C���F���܂����A��U���̌��œ{�点���܂����E�E�E���Ƃ����˂��ƁB");

                            md.Message = "�����i�͉����K���ł��܂���ł�����";
                            md.ShowDialog();
                            sc.EmotionAngry = true;
                            we.AlreadyLvUpEmpty21 = true;
                        }
                        if (sc.Level >= 9 && !sc.ShadowPact)
                        {
                            UpdateMainMessage("���i�F���x���A�b�v�ˁB�k�u�X�ł͉��ɂ��悤������B");

                            UpdateMainMessage("�A�C���F��������A�ő������Ă̈ӊO����ȁB");

                            UpdateMainMessage("���i�F���炻���H�ӊO���Ď��������ł���B");

                            UpdateMainMessage("�A�C���F����A���Ƃ���A���Ȃ鏗�_�Ƃ����������C���[�W�����邵�ȁB");

                            UpdateMainMessage("���i�F������E�E�E�A�C�����ăw���^�C�Ȃ́H���̎q�͈ӊO�ƈł�����鐶���Ȃ̂��");

                            UpdateMainMessage("�A�C���F����Ȃ��̂Ȃ̂��H���܂˂��A���ɂ̓T�b�p�������B");

                            UpdateMainMessage("���i�F�܁A�A�C���ɂ͕�����Ȃ��Ă������B�����ˁA����͈ł̗͂��؂��Ƃ����B");

                            UpdateMainMessage("���i�F�Èł̒��ɖ��@�͂͏h����̂�BShadowPact�A���܂�ˁB");

                            UpdateMainMessage("�A�C���F�������̂����炳�܂ɉ������l�[���́E�E�E");

                            UpdateMainMessage("���i�F���@�U���͂��グ����̂ˁB�ő����Ƃ��Ă͂����炭��Ԏg�����肪�ǂ���B");

                            UpdateMainMessage("�A�C���F���܂���IceNeedle��DarkBlast�̈З͂��オ����Ď����H");

                            UpdateMainMessage("���i�F�������");

                            UpdateMainMessage("�A�C���F�E�E�E�����ƁA�p�����v���o�������B����A���Ⴀ�ȁI�b�n�b�n�b�n�I");

                            UpdateMainMessage("���i�F�A�C���A������A�����ł������Ȃ�����B�b�n�@�@�@�@�I�IIceNeedle�I�I�I");

                            md.Message = "�����i�̓V���h�E�E�p�N�g���K�����܂�����";
                            md.ShowDialog();
                            sc.ShadowPact = true;
                        }
                        if (sc.Level >= 10 && !sc.DispelMagic)
                        {
                            UpdateMainMessage("���i�F�悵�A���ɂk�u�P�O��B�����������");

                            UpdateMainMessage("�A�C���F���������˂����I�����A�L�O�Ɉ�M���Ă݂����B");

                            UpdateMainMessage("���i�F�����A�̂���l���Ă��y��i�����j�z�̊T�O��B����������������Ă݂����B");

                            UpdateMainMessage("�A�C���F�y��i�����j�z�H�H�H�J���b�|�E�E�E�����������ĈӖ����H");

                            UpdateMainMessage("���i�F������A���X�͉��������́B�S�Ė����̂���Ԃ̎n�܂肾�Ǝv��Ȃ��H");

                            UpdateMainMessage("�A�C���F�������Ă񂾃��i�A�����I�J�V�C���H�����̃Z���t�B");

                            UpdateMainMessage("���i�F�A�C���ɂ́E�E�E����������Ȃ����B���Ⴀ�A�s�����BDispel Magic�I");

                            UpdateMainMessage("�A�C���F���ɁA�����ω��͖������H");

                            UpdateMainMessage("���i�F���񂽃o�J����Ȃ��H�킩��킯�Ȃ��ł���B�����˂���v���e�N�V��������Ă����΁H");

                            UpdateMainMessage("�A�C���F�܁E�E�E�܂����I�I�I");

                            UpdateMainMessage("���i�F�n�C�n�C�A�ǂ��������Ă݂Ȃ������");

                            if (mc.Protection)
                            {
                                UpdateMainMessage("�A�C���F�v���e�N�V�����I");
                            }
                            if (mc.FlameAura)
                            {
                                UpdateMainMessage("�A�C���F�t���C���E�I�[���I");
                            }
                            if (mc.HeatBoost)
                            {
                                UpdateMainMessage("�A�C���F�q�[�g�E�u�[�X�g�I");
                            }
                            if (mc.SaintPower)
                            {
                                UpdateMainMessage("�A�C���F�Z�C���g�E�p���[�I");
                            }
                            if (mc.WordOfLife)
                            {
                                UpdateMainMessage("�A�C���F���[�h�E�I�u�E���C�t�I");
                            }
                            if (mc.HighEmotionality)
                            {
                                UpdateMainMessage("�A�C���F�n�C�E�G���[�V���i���e�B�I");
                            }
                            if (mc.WordOfFortune)
                            {
                                UpdateMainMessage("�A�C���F���[�h�E�I�u�E�t�H�[�`�����I");
                            }
                            if (mc.Glory)
                            {
                                UpdateMainMessage("�A�C���F�O���[���[�I");
                            }
                            if (mc.AetherDrive)
                            {
                                UpdateMainMessage("�A�C���F�G�[�e���E�h���C�u�I");
                            }
                            if (mc.EternalPresence)
                            {
                                UpdateMainMessage("�A�C���F�G�^�[�i���E�v���[���X�I");
                            }

                            UpdateMainMessage("���i�F���ɂ�����Ȃ����E�E�E����ADispel Magic��B");

                            UpdateMainMessage("�@�@�@�w�b�r�A�b�p�V���I�I�E�E�E�x�i�A�C�����ݍ���ł������ʂ��S�ď������j");

                            UpdateMainMessage("�A�C���F�����ɑS������������˂����I��������I�I");

                            UpdateMainMessage("���i�F�t�t�A�ǁ[���A�܂���������");

                            UpdateMainMessage("�A�C���F���i�I�����Ƃ����Ƃ��A���������͎̂~�߂Ă����I�I");

                            UpdateMainMessage("���i�F���ȁA����B�{��������Ă��B���������҂ݏo�����񂾂���ǂ�����Ȃ��B");

                            UpdateMainMessage("�A�C���F�܁A�܂��E�E�E���������ǂȁE�E�E�����낤�ȁB");

                            UpdateMainMessage("���i�F�܂����i�͂��܂���ɗ����Ȃ��̂͊m���ˁB�C�����Ďg���Ƃ����B");

                            UpdateMainMessage("�A�C���F�����A�ǂ����Ă����肪�������ʂ�������߂��Ă鎞�����ɂ��Ƃ���B");

                            UpdateMainMessage("���i�F�����A����������B");

                            md.Message = "�����i�̓f�B�X�y���E�}�W�b�N���K�����܂�����";
                            md.ShowDialog();
                            sc.DispelMagic = true;
                        }
                        if (sc.Level >= 11 && !sc.LifeTap)
                        {
                            UpdateMainMessage("���i�F�����悤�₭�k�u���Q����B�b�t�t�A�ǂ�����グ�悤�������");

                            UpdateMainMessage("�A�C���F�������i�B���O�͂��̃^�C�~���O�Łw�K�������x�͖����̂��H");

                            UpdateMainMessage("���i�F�����ł���B�A�C�����Ⴀ��܂����B");

                            UpdateMainMessage("�A�C���F�����E�E�E���Ƃ̓^�C�~���O���Ⴄ���Ď����B");

                            UpdateMainMessage("���i�F�܂��܂��C�ɂ��Ȃ��́B�E�E�E�����ˁA������ł̃C���[�W�������B");

                            UpdateMainMessage("�A�C���F�܂�����I�H�����ӂ̐���������˂��̂��H");

                            UpdateMainMessage("���i�F�����ˁA�ϊ��A�z���A������ł�����E�E�ELifeTap�Ȃ�Ăǂ��H");

                            UpdateMainMessage("�A�C���F������ƁH�܂��m���ɁA���i�͂悭���������̍D������ȁB");

                            UpdateMainMessage("���i�F�}�i�ƈ��������Ƀ��C�t�񕜂���́�");

                            UpdateMainMessage("�A�C���F�b�Q�A���O�܂Ń��C�t�񕜂ł���悤�ɂȂ�̂���B");

                            UpdateMainMessage("���i�F����A�łł����C�t�񕜂��炢�悭���鎖��B");

                            UpdateMainMessage("�A�C���F�܂����������ǂȁB");

                            UpdateMainMessage("���i�F����ŃA�C�������ɋC����Ȃ��Ă��v�������U���ł�����B");

                            UpdateMainMessage("�A�C���F����A�ł���Ȃ����͌�����ȁA�����ƃq�[�����Ă�����炳�B");

                            UpdateMainMessage("���i�F�����A����������B");

                            md.Message = "�����i�̓��C�t�E�^�b�v���K�����܂�����";
                            md.ShowDialog();
                            sc.LifeTap = true;
                        }
                        if (sc.Level >= 12 && !sc.PurePurification)
                        {
                            UpdateMainMessage("���i�F�k�u�P�Q���ĂƂ��ˁB�����āA���ɂ��悤������B");

                            UpdateMainMessage("�A�C���F�����̃��i����A���낻������n�ł́H");

                            UpdateMainMessage("���i�F����A���̖����n���āH");

                            UpdateMainMessage("�A�C���F����E�E�E�����n���Ă̂͂܂�E�E�E");

                            UpdateMainMessage("���i�F���P������Ȃ��P��g��Ȃ��ł�B���`��A�����ˁB");

                            UpdateMainMessage("���i�F�����Ȃ鐴��Ƃ��͂ǂ��H ���Ȃ鎩�Ȏ��������߂�́B");

                            UpdateMainMessage("      �w���i�̑̂��ق̂��ɔ������Y��Ɍ���n�߂��B�x");

                            UpdateMainMessage("�A�C���F���E�E�E�����������E�E�E");

                            UpdateMainMessage("���i�F�ӂ��A���悵����Ȋ����ˁ�@PurePurification�łǂ��H");

                            UpdateMainMessage("�A�C���F�ǂ�Ȍ��ʂȂ񂾁H");

                            UpdateMainMessage("���i�F���ƂĂ��C�����ǂ���B���̉e�����������Ă�̂��S����蕥���銴���ˁB");

                            UpdateMainMessage("�A�C���F�E�E�E�������A180�x�ς���āA�����������n�������ȁB");

                            UpdateMainMessage("���i�F�������̒P��~�߂Ă���Ȃ��H���������ǂ��C���Ȃ̂ɁA���ɃC���C�������ˁ�");

                            UpdateMainMessage("�A�C���F��A�킩�����킩�����B���Ⴀ�A���́E�E�E�Y�킾���A���i�B");

                            UpdateMainMessage("���i�F���ȁE�E�E���ŖJ�ߎE���Ȃ̂�I�@���ˁI�o�J�A�C���I�I");

                            UpdateMainMessage("�@�@�@�w�b�o�S�I�I�H�H���I�I�I�x�i���i�̃G���N�g���L�b�N���A�C�����y�􂵂��j�@�@");

                            md.Message = "�����i�̓s���A�E�s�����t�@�C�P�[�V�������K�����܂�����";
                            md.ShowDialog();

                            sc.PurePurification = true;
                        }
                        if (sc.Level >= 13 && !sc.EnigmaSence)
                        {
                            UpdateMainMessage("���i�F�悤�₭�k�u�P�R��B�����͉������Ȃ̂��l���Ă݂��B");

                            UpdateMainMessage("�A�C���F����Č����Ă��A����܂�ςȂ͎̂g�킸���܂��ɂȂ邺�B");

                            UpdateMainMessage("���i�F������ˁE�E�E�ł��A�O�X���犴���Ă������o�����ǁB");

                            UpdateMainMessage("�A�C���F�����H�����Ă݂��B");

                            UpdateMainMessage("���i�F�U�����ė͂��S�Ă���Ȃ��B�����v��Ȃ��H");

                            UpdateMainMessage("�A�C���F������A�͂����S�Ă���B");

                            UpdateMainMessage("���i�F���[��E�E�E���̎��̗́E�Z�E�m�̂����A�p�����^�ōő�̒l�͑����i���ʍ��킹��");

                            int maxValue = Math.Max(sc.StandardStrength, Math.Max(sc.StandardAgility, sc.StandardIntelligence));
                            if (maxValue == sc.StandardStrength)
                            {
                                UpdateMainMessage("���i�F�́A" + maxValue.ToString() + "����ԍ����킯�ˁB");
                            }
                            else if (maxValue == sc.StandardAgility)
                            {
                                UpdateMainMessage("���i�F�Z�A" + maxValue.ToString() + "����ԍ����킯�ˁB");
                            }
                            else
                            {
                                UpdateMainMessage("���i�F�m�A" + maxValue.ToString() + "����ԍ����킯�ˁB");
                            }

                            UpdateMainMessage("�A�C���F�Ȃ�قǂȁB���ꂪ�ǂ��������̂��H");

                            UpdateMainMessage("���i�F��ԍ����l�ōU���ł�����Đ����Ǝv��Ȃ��H");

                            UpdateMainMessage("�A�C���F����A����Ȃ̂̓_������B���܂�ɂ����z���䂷���邼�B");

                            UpdateMainMessage("���i�F�b�t�t�A�����o�������Ȃ̂�ˁA�ǂ�������Ȃ�����EnigmaSence�Ƃł����t�����B");

                            UpdateMainMessage("�A�C���F�}�W����I�H�A���Ȃ̂���A����Ȃ́I�I�͂������S�Ă���I�H");

                            UpdateMainMessage("���i�F�ǂ�����Ȃ��A���@�͂t�o�̒m���U���ɂ��o����B������čō��̃Z���X���");

                            UpdateMainMessage("�A�C���F���������A��k����E�E�E���ꂶ��͂��グ�闧�ꂪ�낤������˂����B");

                            UpdateMainMessage("���i�F�ł��X�L���|�C���g������邩��ˁB����ł��������ł���B");

                            UpdateMainMessage("�A�C���F������������A�����Ⴄ�C�����邯�ǂȁB�܂��ǂ��Ƃ��邩�B");

                            UpdateMainMessage("���i�F�b�t�t�A�������܂ɂ͂���ō����_���[�W���͂����o���Č�����");

                            md.Message = "�����i�̓G�j�O�}�E�Z���X���K�����܂�����";
                            md.ShowDialog();

                            sc.EnigmaSence = true;
                        }
                        if (sc.Level >= 14 && !sc.BlackContract)
                        {
                            UpdateMainMessage("���i�F�k�u�P�S�ˁB���[��A�������낻��l�������ȁB");

                            UpdateMainMessage("�A�C���F�N�����Ă���H�S������Ȃ�������ȁA�b�n�b�n�b�n�I");

                            UpdateMainMessage("�@�@�@�w���H�O�b�V���A�A�@�@�I�I�I�x�i���i�̃G�������^���u���[���A�C�����y�􂵂��j�@�@");

                            UpdateMainMessage("���i�F�C���[�W�͈łˁB���ꂩ�牽�����]���ɂ��Ȃ���E�E�E");

                            UpdateMainMessage("���i�F���������͂Ȍ��ʂ����̂�B�˂��A�ǂ��Ǝv��Ȃ��H�A�C���B");

                            UpdateMainMessage("�A�C���F�E�E�E�E�E�E");

                            UpdateMainMessage("���i�F�����ƌ����Ƃ��A���C�t���]���ɂ��Ăł��A�X�L���E���@��ł�����������Ă̂͂ǂ�������B");

                            UpdateMainMessage("���i�F�����I�ȗ͂��������B�b�t�t��@���܂�����ABlackContract��I");

                            UpdateMainMessage("���i�F�b�t�t�A�ł����ݕ�����čō����Ǝv��Ȃ��H�˂��A�A�C���I�@������Ԃ��ׂ����I");

                            UpdateMainMessage("���i�FDarkBlast�I�@IceNeedle�I�@EnigmaSence�U���I�@");

                            UpdateMainMessage("      �w�b�h�X�I�@�b�h�X�I�@�{�S�H�I�I�i�A�C����3��ނƂ��N���[���q�b�g�����I�j�x");

                            UpdateMainMessage("�A�C���F�E�E�E�E�E�E�E�E�E�@�O�H�E�E�E");

                            UpdateMainMessage("���i�F�b�t�t��@�ǂ���炱��ŏI���݂����B���[��A�X�b�L��������I");

                            UpdateMainMessage("���i�F�����A�A�C���B���v�H�S�����i�T�C�ˁA�t�������Ă����������āB");

                            UpdateMainMessage("�A�C���F�E�E�E�E�E�E");

                            UpdateMainMessage("���i�F���[��A�Q�����肩�E�E�E�������܂�Ȃ���ˁE�E�E���悵�A���Ⴀ");

                            UpdateMainMessage("���i�FLifeTap�ŉ񕜁B�@�������BlackContract�ˁB�@�s�����A�C����");

                            UpdateMainMessage("�A�C���F�҂đ҂đ҂đ҂đ҂āI�@�҂����҂����҂����҂����I�I");

                            UpdateMainMessage("���i�FDarkBlast�I�@IceNeedle�I�@EnigmaSence�U���I�@");

                            md.Message = "�����i�̓u���b�N�E�R���g���N�g���K�����܂�����";
                            md.ShowDialog();

                            sc.BlackContract = true;
                        }
                        if (sc.Level >= 15 && !sc.Cleansing)
                        {
                            UpdateMainMessage("���i�F�k�u�P�T���B������B����������M���Ɨǂ��񂾂��ǁB");

                            UpdateMainMessage("�A�C���F���O�A���̑O�������s���A�E�E�E���������B");

                            UpdateMainMessage("���i�FPurePurification��B�����Ɗo���Ă�ˁB");

                            UpdateMainMessage("�A�C���F�����A�\���\���B����͎������g�ɂ����Ώۂɂł��Ȃ�����B");

                            UpdateMainMessage("���i�F�����ˁA�������g�ɑ΂��鎩�Ȏ��������B");

                            UpdateMainMessage("�A�C���F���i���g������Ȏ��ɁA������X�ɑ��̂�ɕ����^�����Ȃ����H");

                            UpdateMainMessage("���i�F���`��A������ˁB�ł��A����Ă݂��B������Ɨ���Ă݂āB");

                            UpdateMainMessage("���i�F�E�E�E�X�E�E�D�D�E�E�E");

                            UpdateMainMessage("���i�F�E�E�E����ACleansing�Ɩ��Â����B�b�n�C�I");

                            UpdateMainMessage("�A�C���F�����I�E�E�E�������I�I�@�������C���u�������I");

                            UpdateMainMessage("���i�F����A���̑̒����ǂ��Ƃ��͏o��������B�O�b�h�A�C�f�A�ˁB");

                            UpdateMainMessage("�A�C���F����Ȃ瑽���̕��̉e�������Ă����Ƃ�����������ꂻ�����ȁB");

                            UpdateMainMessage("���i�F�����A�C�������q�������Ȃ�����A�܂���������Ȃ��Ƒʖڂ����璍�ӂ��Ă�ˁB");

                            md.Message = "�����i�̓N���[���W���O���K�����܂�����";
                            md.ShowDialog();

                            sc.Cleansing = true;
                        }
                        if (sc.Level >= 16 && !sc.Negate)
                        {
                            UpdateMainMessage("���i�F�����Ƀ��x���A�b�v��B");

                            UpdateMainMessage("�A�C���F��������A���̓X�L���F�u�S��v�n����K���������A���O�͂܂�����ȁH");

                            UpdateMainMessage("���i�F�C�C�Ƃ��ɋC�t������ˁB���͍��񂠂炩���߃C���[�W���Ă����̂�����́B");

                            UpdateMainMessage("�A�C���F�ǂ�Ȃ񂾁H");

                            UpdateMainMessage("���i�F���ɂ͉΁A���ɂ͈ŁA���ɂ͋�ƁA��̔��Α��Ɉʒu����v�f���Ă���ł���H");

                            UpdateMainMessage("�A�C���F�����E�E�E�܂������i�B�v�������̂���DispelMagic�݂����Ȃ���I�H");

                            UpdateMainMessage("���i�F���`�񏭂����Ă邩���ˁB�ł������Ė��Ƃ����\�݂����Ȃ̂Ɏ䂩���̂�ˁB");

                            UpdateMainMessage("�A�C���F�������A�����͈Ⴄ��ۂ̂��̂ɂ����ȁB����܂�ǂ��C���[�W�͂˂����ǂȁB");

                            UpdateMainMessage("���i�F�܂��ǂ�����Ȃ��B�D�݂Ȃ�Đl�ɂ���ĈႤ�񂾂���A�b�z���z�����K�����Ă��");

                            UpdateMainMessage("�A�C���F�ǂ����񂾂�H");

                            UpdateMainMessage("���i�F�K���ɖ��@��ł�����ł݂Ă�B");

                            UpdateMainMessage("�A�C���F������A���Ⴂ�����E�E�E�I���@�I�t���C���E�X�g���C�N�I�I");

                            UpdateMainMessage("      �w�V���S�I�I�H�H�I�I�I�x�@�@�@");

                            UpdateMainMessage("���i�F���̉r���X�y���֖��߂����BNegate�I");

                            UpdateMainMessage("      �w�b�o�V���E�E�E�D�D�D���E�E�E�x");

                            UpdateMainMessage("���i�F������A��������I�@�ǂ���琬���̂悤�ˁ�");

                            UpdateMainMessage("�A�C���F�}�W���E�E�E�܂����Ƃ͎v�������@�Ȃ�S�ď�������Ď����H");

                            UpdateMainMessage("���i�F���`��A�Ƃ肠�����S���������������ǁA����Ă݂Ȃ����ɂ͕�����Ȃ���B");

                            UpdateMainMessage("�A�C���FDispelMagic�͕t�^���ʂ������BNegate�͉r���^�C�~���O�X�y���������B�Ƃ�ł��˂��ȁB");

                            UpdateMainMessage("���i�F�ł����肪�g���Ă��Ȃ�����A�g���Ȃ���B��A���̃^�C�~���O����Ȃ��ƈӖ���������ˁB");

                            UpdateMainMessage("���i�F���\�g���ǂ��낪�������B����ȉߓx�Ȋ��҂͂ł��Ȃ���ˁB");

                            UpdateMainMessage("�A�C���F�������A���������͏�����񂾂�ȁB�f�J�C���@�͐���Ƃ��~�߂Ă����B");

                            UpdateMainMessage("���i�F�����A��肭���ĂĂ݂�����");

                            md.Message = "�����i�̓j�Q�C�g�K�����܂�����";
                            md.ShowDialog();
                            sc.Negate = true;
                        }
                        if (sc.Level >= 17 && !sc.FrozenLance)
                        {
                            mainMessage.Text = "���i�F���x���P�V���ď��ˁ�";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���i�A���O�̑́E�E�E";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�o�b�A�b�o�J�I����ȁI�I�A�b�`�s���Ă�I�I";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���A���₢��A���������Ӗ�����˂����B�ŁA�����M���������H";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�����ˁA�������ŉs���n�ŃA�C����S����˂��h���AFrozenLance���Ăǂ���";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���O�A�������ŉ��ł܂��U�����@�Ȃ񂾂�H�h�q�I�Ȃ��̂ɂ���B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�ǂ�����Ȃ��́A�Α����̃A�C���΂�����U������c�}���i�C���B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�܂��ǂ����ł��ǂ����ǂȁB���ŁA�ǂ�Ȕ\�͂Ȃ񂾁H";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�������A���ڍU���Ɍ��܂��Ă邶��Ȃ��B���[�āA�����Ă݂悤�������";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���������B�����������炻�̕ӂɂ��Ă������ȁB���ȁB���ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�@�@�@�w�b�s�V�A�s�L�L�L�L�B�I�s�V���A�A�[�[�[�[�[�[�I�I�h�h�h�h�h�h�I�I�I�x�@�@";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�b�O�A�b�O�A�A�A�A�@�@�@�@�E�E�E�E�E�E";
                            ok.ShowDialog();
                            md.Message = "�����i��FrozenLance���K�����܂�����";
                            md.ShowDialog();
                            sc.FrozenLance = true;
                        }
                        if (sc.Level >= 18 && !sc.RiseOfImage)
                        {
                            UpdateMainMessage("���i�F�����悤�₭�Q�O�܂Ō㏭�����ď��ˁB");

                            UpdateMainMessage("�A�C���F���i�A���O�����̂���̌����Č��\�_���[�W�n����Ă񂾂ȁB");

                            UpdateMainMessage("���i�F�����H�o�J�A�C���قǂ���Ȃ����ǂˁB�ł�����͈Ⴄ��B");

                            UpdateMainMessage("�A�C���F�ǂ������̂��v�������񂾂�B");

                            UpdateMainMessage("���i�F�S���ĕs�v�c����Ȃ��H");

                            UpdateMainMessage("�A�C���F�܂��ȁB�����Ȃ����������A�s�v�c�Ƃ�����蓖����O�ƌ����������ȁB");

                            UpdateMainMessage("���i�F�퓬���Ă鎞�A�����W���������񂾂��ǒ��X��_�Ɏ��܂�Ȃ��ꍇ������̂�B");

                            UpdateMainMessage("���i�F�ǂ����Ă��W�����Ȃ��Ƒʖڂ��Ď��̓R���ˁBRiseOfImage��B");

                            UpdateMainMessage("�A�C���F�ǂ��Ȃ�񂾁H�W���ł���悤�ɂȂ�̂��H");

                            UpdateMainMessage("���i�F�S�̃p�����^�t�o��B�ւ��E�E�E���\�u���ȋC���ˁA�R����");

                            UpdateMainMessage("�A�C���F�S�p�����^�t�o���B���ƂȂ����O�̊���������ǂ��Ȃ����ȁB");

                            UpdateMainMessage("���i�F������������ň���������ˁI");

                            UpdateMainMessage("�A�C���F�ׁA�ʂɂ���Ȃ񂶂�˂����āB���ł���ȗ��ǂ݂��Ă񂾂�B");

                            UpdateMainMessage("���i�F�֌W�Ȃ�����Ȃ��́B�܂����������͍l���Ȃ�����B");

                            UpdateMainMessage("�A�C���F��A�킩�����B�܂��A���̖��@�E�E�E�C���ǂ��Ȃ��Ă悩��������˂����B");

                            UpdateMainMessage("���i�F�m��Ȃ����B�����A�Ƃɂ����S�p�����^�t�o�͏d�v������ˁB");

                            UpdateMainMessage("�A�C���F��A�킩�����B�킩�������āE�E�E���J���}�V�^�B");

                            md.Message = "�����i�̓��C�Y�E�I�u�E�C���[�W���K�����܂�����";
                            md.ShowDialog();
                            sc.RiseOfImage = true;
                        }
                        if (sc.Level >= 19 && !we.AlreadyLvUpEmpty22)
                        {
                            UpdateMainMessage("���i�F�܂������A�l�̋C���m��Ȃ��ŁE�E�E�z���b�g�ɁE�E�E������x");

                            UpdateMainMessage("���i�F���C�Y�I�u�C���[�W�I�E�E�E���Ƃ��̌�ŃG�j�O�}�Z���X�I");

                            UpdateMainMessage("�A�C���F�����A����Ă�ȁB���i�A���C���H");

                            UpdateMainMessage("�@�@�@�w���H�Q�b�V���A�@�I�I�x�i���i�̃��C�Y�I�u���C�g�j���O���A�C�����y��j�@�@");

                            UpdateMainMessage("�A�C���F�����E�E�E�������������E�E�E���́E�E�E");

                            UpdateMainMessage("���i�F�m��Ȃ����I�A���^��������ł���I����₵�čl���Ȃ�����I�I");

                            UpdateMainMessage("�A�C���F���̗��R�����\���E�E�E�_��E�E�E�ޏ��ɐ��^�ȐS���E�E�E");

                            UpdateMainMessage("�A�C���F�@�@�w�b�p�^�x");

                            UpdateMainMessage("���i�F���Łw�b�p�^�x���Č����Ă��ʖڂ��@���炢�Ȃ����I");

                            UpdateMainMessage("�A�C���F�҂đ҂đ҂āI���Ȃ񂾂����́I�H");

                            UpdateMainMessage("���i�F���R�H�A�C�����Ԃ��ׂ�����������I�s�����I�I�n�A�A�@�@�@�E�E�E");

                            UpdateMainMessage("�A�C���F�҂āI���蓾�Ȃ�����I�H���߂ė��R���炢�t����I");

                            UpdateMainMessage("�@�@�@�w�b�h�E�E�E�I�b�O�V���A�@�I�I�x�i���i�̃G�j�O�}�u���[���A�C�����y��j�@�@");

                            UpdateMainMessage("�@�@�@�w�A�C���͂��̏�ŉʂĂ��E�E�E�x");

                            UpdateMainMessage("���i�F���Ӂ[�A�X�b�L�������I���[�C�����ǂ���ˁA���C�Y�I�u�C���[�W��");

                            UpdateMainMessage("���i�F�G�j�O�}�Z���X�����i�g���Ă�\�͂ōU��������]�v�C�����������");

                            UpdateMainMessage("���i�F���āA����H�A�C���E�E�E�{���ɋC�₵�������������B");

                            UpdateMainMessage("���i�F�܂�����A�������A�u���ɂȂ������Ń_���W�����s���܂����");

                            md.Message = "�����i�͉����K���ł��܂���ł�����";
                            md.ShowDialog();
                            we.AlreadyLvUpEmpty22 = true;
                        }
                        if (sc.Level >= 20 && !sc.Deflection)
                        {
                            UpdateMainMessage("���i�F�������I�k�u�Q�O�ɓ��B���");

                            UpdateMainMessage("�A�C���F��邶��˂����I���i�B�k�u�Q�O�͂������߂Ă���̂��H");

                            UpdateMainMessage("���i�F�����A�O�X�����낤�Ǝv���Ă��̂�����̂�B");

                            UpdateMainMessage("���i�F�A�C���A������Ƃ��������ɗ����Ă݂āB");

                            UpdateMainMessage("�A�C���F���������H�I�[�P�[�I�[�P�[�B");

                            UpdateMainMessage("���i�F�������A�������Ă��Ȃ�����");

                            UpdateMainMessage("�A�C���F���O�܂����̃p�^�[�����E�E�E�J�E���^�[�A�^�b�N�̎��Ƃ������肾�ȁB");

                            UpdateMainMessage("���i�F���������Ă�ԂɁE�E�E������ADeflection��I");

                            UpdateMainMessage("�@�@�@�w���i�̎��͂ɔ����h�ǋ�Ԃ����������I�x");

                            UpdateMainMessage("�A�C���F�b�Q�I��������I�H");

                            UpdateMainMessage("���i�F�b�t�t�A�閧���");

                            UpdateMainMessage("�A�C���F���������A���Ȃ񂾁E�E�E�������ȁB������I�t�@�C�A�{�[�����I");

                            UpdateMainMessage("���i�F�b�\���ˁA�j�Q�C�g�I");

                            UpdateMainMessage("�A�C���F�������I�������ʓ|���B�������A�X�g���[�g�X�}�b�V���H�炦�I");

                            UpdateMainMessage("�@�@�@�w�b�p�L�[���i�����h�ǂ��A�C���̃X�g���[�g�X�}�b�V����Ԃ����I�x");

                            UpdateMainMessage("�A�C���F�������I�ɂ��Ă����������I�I");

                            UpdateMainMessage("���i�F������A�_�[�N�u���X�g�I");

                            UpdateMainMessage("�A�C���F�����I�H�O�A�A�A�@�E�E�E�C�c�c�c");

                            UpdateMainMessage("�A�C���F��������A�����U�����˂���̂��H");

                            UpdateMainMessage("���i�F������B�o�J�A�C���ɂƂ��Ă͓V�G�݂����Ȃ��̂ˁB");

                            UpdateMainMessage("�A�C���F�����A���Ď��͖��@�����点��悤�ɗU�������Ă�悤�ȃ��m���B");

                            UpdateMainMessage("���i�F������B������j�Q�C�g���^�C�~���O�悭�����������Ď���B");

                            UpdateMainMessage("�A�C���F�E�E�E���[�[�[�[�ʓ|���������I�@�����A���͂���Ă݂���I");

                            UpdateMainMessage("���i�F�������Ă�̂�����B���˂͈�x����������ˁB�����������ӂ���Ηǂ����P��B");

                            UpdateMainMessage("�A�C���F���U�ƒ��ڍU������x�H�炦���Ă��H");

                            UpdateMainMessage("���i�F���Ȃ�A�����l���Ă����Ȃ�����B");

                            UpdateMainMessage("�A�C���F�E�E�E�ʓ|�����������I�I");

                            md.Message = "�����i�̓f�t���N�V�������K�����܂�����";
                            md.ShowDialog();
                            sc.Deflection = true;
                        }
                        if (sc.Level >= 21 && !sc.AntiStun)
                        {
                            UpdateMainMessage("���i�F�k�u�Q�P�ɂȂ������ŁA����͉��ɂ��悤���ȁB");

                            UpdateMainMessage("�A�C���F�ǂ������A�M���Ȃ��̂��H");

                            UpdateMainMessage("���i�F���`��A���������킯����Ȃ��񂾂��ǂˁB");

                            UpdateMainMessage("�A�C���F��������A�����Ă����B");

                            UpdateMainMessage("���i�F���`��A������ƃA�C���̂͒ɂ��������E�E�E���A��������B");

                            UpdateMainMessage("���i�F���ꂱ��B���́w�_�~�[�f�U��N�x���g�����ɂ�����");

                            UpdateMainMessage("�A�C���F�����I�H������B����Ȃ̂������̂��I�H");

                            UpdateMainMessage("���i�F�A�C���m��Ȃ��́HDUEL�����ŗǂ��g����g���[�j���O�}�V����B");

                            UpdateMainMessage("�A�C���F�}�V���H���̉����E�E�E�ꌂ�ŉ�ꂻ���Ȃ̂����H");

                            UpdateMainMessage("���i�F�ł��󂳂ꂽ���͖����݂�����B�������ƃX�^�����ʂŃ_���[�W�P���ƁE�E�E�B");

                            UpdateMainMessage("���i�F���ƁA�U�����@�E�E�E���ڂˁB���ꂩ��A�^�C�}�[�T�b����ƁB���͊�����");

                            UpdateMainMessage("���i�F�������Ƃ����ˁAAntiStun�Ƃł����t�����B�����ʂ�X�^�����ʂɑϐ����t����B");
                            
                            UpdateMainMessage("���i�F�T�C�S�C�R�C�Q�C�P�E�E�E");

                            UpdateMainMessage("�@�@�@�w�@�b�|�R�����@�x�@�i���i�̓��Ƀ_�~�[�f�U��N�̃X�^���U�����y�􂵂��j");

                            UpdateMainMessage("���i�F�����`��A���v��B�X�^�����ʂ̑ϐ��t�^�A��o���ˁ�");

                            UpdateMainMessage("�A�C���F�Ȃ��E�E�E������������������Ȃ��˂����H");

                            UpdateMainMessage("���i�F�X�^�����ʂ̓_���[�W�ʂƂ͒��ڊ֌W�͂Ȃ�����R���ŗǂ��̂�B");

                            UpdateMainMessage("�A�C���F�悭�킩��˂����ǂȁE�E�E���̃_�~�[�f�U��N�A���ł��ł���̂��H");

                            UpdateMainMessage("���i�F�����ȃ{�^������R����݂����B���ӂ��Ďg���Ă�ˁB");

                            UpdateMainMessage("�A�C���F������Ɖ�������Ă݂邩�E�E�E���X�H�E�E�E�_���[�W�x�[�X�R�T�O�O�B");

                            UpdateMainMessage("�A�C���F�w�^�{�X�yᏋZ�@�C�����B�W�u���E�n���h���b�h�E�J�b�^�[�z�ʔ��������ȁB�I���I");

                            UpdateMainMessage("�@�@�@�w�V���K�K�K�A�b�K�K�K�b�K�S�I�I�I�H�H�H���I�I�I�x�i�A�C���͗����֔�ї������j");
                            
                            md.Message = "�����i�̓A���`�E�X�^�����K�����܂�����";
                            md.ShowDialog();
                            sc.AntiStun = true;
                        }
                        if (sc.Level >= 22 && !sc.DevouringPlague)
                        {
                            UpdateMainMessage("���i�F�k�u�Q�Q��A�����ȐL�тˁB");

                            UpdateMainMessage("�A�C���F�O�̃A���`�X�^���ƌ����A���̑O�̃f�t���N�V�����ƌ����B");

                            UpdateMainMessage("���i�F�����ˁA���낻��O�q�I�Ȗ��@���~������ˁB");

                            UpdateMainMessage("�A�C���F���A���₢��A���������Ӗ��Ō������񂶂�˂��B���̂܂܂ŗǂ����ĈӖ����B");

                            UpdateMainMessage("���i�F�قߌ��t�A���肪�ƁB���̂܂܂̏�Ԃł��������Ŗ��@���v���������");

                            UpdateMainMessage("�A�C���F����A�ʂɂ���ȗ���ňŖ��@�Ȃ�Ă��Ȃ��ėǂ����A���ȁH");

                            UpdateMainMessage("���i�F�P�Ȃ�_���[�W���@��������C�}�C�`��ˁE�E�E�ł����C�t�񕜂͂�������킯�����B");

                            UpdateMainMessage("���i�F�����ˁA�R�R�͈Ŗ��@�炵���A�_���[�W�{�񕜂ł���Ă݂��ADevouringPlague��B");

                            UpdateMainMessage("�A�C���F���i�A�����Ɨp�ӂ��Ă����Ă�������B�w�_�~�[�f�U��N�x���B");

                            UpdateMainMessage("�A�C���F�Ȃ�ƁA���́w�_�~�[�f�U��N�x�A���@�h�䗦�Ƃ����ݒ�ł���݂������B");

                            UpdateMainMessage("�A�C���F�܂��A��^�[���̊Ԃ͕K�����ڍU���݂̂����{����Ɠ��͂��Ă��ȁE�E�E");

                            UpdateMainMessage("�A�C���F�X�Ƀ��C�t�Ⓖ�ڍU���ʂ��ݒ�\������A�_���[�W���[�X�Ȃ񂩂�肽�����");

                            UpdateMainMessage("���i�F�������炢������A�����A�s�������");

                            UpdateMainMessage("�A�C���F�b�O�E�E�E�E�O�I�I�I�H�H�H�E�E�E�E");

                            md.Message = "�����i�̓f���H�����O�E�v���O�[���K�����܂�����";
                            md.ShowDialog();
                            sc.DevouringPlague = true;
                        }
                        if (sc.Level >= 23 && !sc.Tranquility)
                        {
                            UpdateMainMessage("���i�F�悤�₭�k�u�Q�R�ˁB����͗ǂ��񂾂��ǁE�E�E");

                            UpdateMainMessage("�A�C���F�����B�M���ɂ����̂��H");

                            UpdateMainMessage("���i�F���A�����񂻂�����Ȃ����ǁB����ς�M�����e���ĕς����Ȃ��݂����ˁB");

                            UpdateMainMessage("�A�C���F�M���Ȃ�Ă��������ς������񂶂�Ȃ�����A�C�ɂ���ȁB");

                            UpdateMainMessage("���i�F����A�ŉ����v���������e�Ȃ񂾂��ǁB");

                            UpdateMainMessage("�A�C���F�����A�ǂ�Ȃ̂���H");

                            UpdateMainMessage("���i�F�������Č��̏�ԂƂ����̂��K������ł���B");

                            UpdateMainMessage("���i�F���̌��̏�Ԃɖ߂�������BTranquility�Ɩ��������B");

                            UpdateMainMessage("�@�@�@�w���i�̎茳�ɔ����邢�ΐF�̔����̂��������n�߂��x");

                            UpdateMainMessage("�A�C���F�E�E�E�ւ��E�E�E���������ƁE�E�E���ꂢ�ȐF���ȁB");

                            UpdateMainMessage("���i�F�s�����A�A�C���B�b�n�C�I");

                            UpdateMainMessage("�A�C���F�������������E�E�E�H�@���Ƃ��˂����B");

                            UpdateMainMessage("���i�F�L���Z�̎��ɁA�ꎞ�I�Ɍ��ʂ�t�^����̂�����ł���H");

                            UpdateMainMessage("�A�C���F�����A���܂ɏo�Ă����ȁB���������́B");

                            UpdateMainMessage("���i�F���������̂���U����������悤�ȓ��e�Ȃ́B������ƍ��̎��B����킩��Ȃ������B");

                            UpdateMainMessage("�A�C���F�܂��A�G�����B�o���o�������g���Ă����A�����ɑł��Ă݂��ǂ�����B");

                            UpdateMainMessage("���i�F����������ˁA���肪���͂Ȉꎞ�������g���Ă��������Ă݂��B");

                            md.Message = "�����i�̓g�����L�B���e�B���K�����܂�����";
                            md.ShowDialog();
                            sc.Tranquility = true;
                        }
                        if (sc.Level >= 24 && !sc.MirrorImage)
                        {
                            UpdateMainMessage("���i�F�k�u�Q�S��A�g���g�����q�ɂt�o�ł���̂��ėǂ���ˁB");

                            UpdateMainMessage("�A�C���F�Ȃ�ׂ��g���g���Ɛ��^�ȃC���[�W�ł��v���t���Ă���B");

                            UpdateMainMessage("���i�F���悻�̖������Ȍq�����E�E�E�܂�Ŏ������^����Ȃ��݂�������Ȃ��B");

                            UpdateMainMessage("�A�C���F���E�E�E����E�E�E");

                            UpdateMainMessage("�@�@�@�w�V���S�I�I�I�H�H���I�I�x�i���i�̃t�@�C�i���e�B�u���[���A�C�����y��j�@�@");

                            UpdateMainMessage("�A�C���F���܂��E�E�E���łɁE�E�E");

                            UpdateMainMessage("���i�F�ق�Ǝ����ˁA�������猩�ĂȂ�����A�����痧�؂��Ă݂��邩��B");

                            UpdateMainMessage("���i�F�����̏��_��A�������܂��B���̌ւ�A�[���̋����������܂��B");

                            UpdateMainMessage("�@�@�@�w���i�̑̂̎��͂ɔZ���F�̋�ԕǂ�����n�߂��B�x");

                            UpdateMainMessage("�A�C���F�����E�E�E�����������Z���F���ȁB");

                            UpdateMainMessage("���i�F�������AMirrorImage�I");

                            UpdateMainMessage("�A�C���F�ւ��A�����ƃn�b�L�������Z���F�̋�ԕǂ��ȁB���Ɏg����񂾁H");

                            UpdateMainMessage("���i�F�_���[�W�n�ɓ����閂�@�͔��˂����B");

                            UpdateMainMessage("���i�F�ł��p�����^�t�o�n�⃉�C�t�񕜂Ȃǂ͔��˂��Ȃ��݂�����B");

                            UpdateMainMessage("�A�C���F�Ȃ�قǂȁA�����͂��Ȃ�֗����ȁE�E�E���₠�������E�E�E");

                            UpdateMainMessage("���i�F�������E�E�E����H�����������Ă݂����킯�H");

                            UpdateMainMessage("�A�C���F���̒��ɋ��郉�i�E�E�E�����������B���^�����o�Ă��Y�킾�B");

                            UpdateMainMessage("���i�F�����I�H�E�E�E�����������������I�H");

                            UpdateMainMessage("���i�F���ˁI�o�J�A�C���I�I");

                            UpdateMainMessage("�@�@�@�w�V���S�I�I�I�H�H���I�I�x�i���i�̃C�[�O���L�b�N���A�C�����y��j�@�@");

                            UpdateMainMessage("�A�C���F�E�E�E�ȁE�E�E���́E�E�E");

                            md.Message = "�����i�̓~���[�E�C���[�W���K�����܂�����";
                            md.ShowDialog();
                            sc.MirrorImage = true;
                        }
                        if (sc.Level >= 25 && !sc.VoidExtraction)
                        {
                            UpdateMainMessage("���i�F�k�u�Q�T�A���x���ԃ|�C���g�ˁB");

                            UpdateMainMessage("�A�C���F���i�A���q�͂ǂ����B�������H");

                            UpdateMainMessage("���i�F����A�ǂ�������B����͂����ˁE�E�E�����A����Ȃ̂ǂ��H");

                            UpdateMainMessage("���i�F���ʂ͔\�͂����߂�ہA���炩�̃C���[�W�𔺂��B�����v��Ȃ��H");

                            UpdateMainMessage("�A�C���F�܂��A����Ă��Ɍ����΂������ȁB");

                            UpdateMainMessage("���i�F���̎����Ă����P�ɏW������̂Ƃ͈���ĂāA�����o���悤�Ȋ��o�ł��́B");

                            UpdateMainMessage("���i�F���������ł��o�������Ȏ��͉����A���ł������o�������Ȃ͉̂����B");

                            UpdateMainMessage("�A�C���F�Ȃ�قǂȁA���ƂȂ�������C�����邺�B");

                            UpdateMainMessage("���i�F�����Ă�ō��̃|�e���V�����E�E�E�X�Ɉ����o���Ă݂��E�E�EVoidExtraction�I");

                            UpdateMainMessage("�A�C���F�����I�����A�������ꂽ���͋C�ɂȂ����ȁB");

                            int maxValue = Math.Max(sc.StandardStrength, Math.Max(sc.StandardAgility, Math.Max(sc.StandardIntelligence, sc.StandardMind)));
                            if (maxValue == sc.StandardStrength)
                            {
                                UpdateMainMessage("���i�F���̎��̏ꍇ���Ɨ́A" + maxValue.ToString() + "�������o���ꂽ���ɂȂ��B");
                            }
                            else if (maxValue == sc.StandardAgility)
                            {
                                UpdateMainMessage("���i�F���̎��̏ꍇ���ƋZ�A" + maxValue.ToString() + "�������o���ꂽ���ɂȂ��B");
                            }
                            else if (maxValue == sc.StandardIntelligence)
                            {
                                UpdateMainMessage("���i�F���̎��̏ꍇ���ƒm�A" + maxValue.ToString() + "�������o���ꂽ���ɂȂ��B");
                            }
                            else
                            {
                                UpdateMainMessage("���i�F���̎��̏ꍇ���ƐS�A" + maxValue.ToString() + "�������o���ꂽ���ɂȂ��B");
                            }


                            UpdateMainMessage("�A�C���F�Ȃ�قǁA�ł������p�����^���X�ɏ㏸����̂��E�E�E");

                            UpdateMainMessage("�A�C���F���āA���������I����\������������˂����I�H");

                            UpdateMainMessage("���i�F����������H����Ȃ��񂾂Ǝv�����ǁB");

                            UpdateMainMessage("�A�C���F��ׂ��E�E�E�����E�E�E���̕����キ�Ȃ�˂����H");

                            UpdateMainMessage("���i�F����A�m���Ɏ��̌����p������ȏ㋭���Ȃ������Ȃ������ˁ�");

                            UpdateMainMessage("�A�C���F���������A���͂܂��܂�����I");

                            UpdateMainMessage("���i�F���ӂӁA�܂��������܂Ɏ�������Ă��������");

                            UpdateMainMessage("�A�C���F�{�C�ŗ��ȁA���i�B����ȃ{���h�E�G�L�X�g���V���b�g�|�������Ƃ��˂�����ȁB");

                            UpdateMainMessage("���i�F�A�C���E�E�E�������ɊԈႦ������E�E�E");

                            md.Message = "�����i�̓��H�C�h�E�G�N�X�g���N�V�������K�����܂�����";
                            md.ShowDialog();
                            sc.VoidExtraction = true;
                        }
                        if (sc.Level >= 26 && !sc.OneImmunity)
                        {
                            UpdateMainMessage("���i�F���x���A�b�v���x���A�b�v���Ɓ�");

                            UpdateMainMessage("�A�C���F���������P�Ɋy����������˂����B");

                            UpdateMainMessage("���i�F�����͂ˁA�Ƃ��Ă����̔��B�҂ݏo����C������̂��");

                            UpdateMainMessage("�A�C���F�ւ��A���M���肻���ȕ��͋C���ȁB���[���A����������ɂȂ��Ă���B");

                            UpdateMainMessage("���i�F�K���c�f������ɂˁA�q���g�������Ă�������̂�B");

                            UpdateMainMessage("�A�C���F���H�������E�E�E�K���c�f�����񂩁A�m���ȓ���m�b��������Ă������ȁB");

                            UpdateMainMessage("�A�C���F�܂������B�@������I���ł��������B");

                            UpdateMainMessage("���i�F���Ⴀ�A����Ă݂���E�E�EMirrorImage��Deflection�̃C���[�W����{�ŁE�E�E");

                            UpdateMainMessage("���i�F�b�n�I�@OneImmunity�I");

                            UpdateMainMessage("�A�C���F�I�H�����A�܂������̖h��n���H");

                            UpdateMainMessage("���i�F�b�t�E�D�E�E�E�悵�A����͐���������B�A�C���A�h��n�Ȃ�Ă��̂���Ȃ����B");

                            UpdateMainMessage("���i�F���ł��ǂ���A�D���ȍU�����@������Ă݂Ă��傤�����B");

                            UpdateMainMessage("�A�C���F�}�W����A�����Ȍ������Ղ肾�ȁB���ቓ���Ȃ���点�Ă��炤���B");

                            UpdateMainMessage("�A�C���F�I���@�I���H���J�j�b�N�E�F�C�u���I�I");

                            UpdateMainMessage("        �w�V���S�I�I�I�H�H�H�I�b�h�I�I�I�H�H���I�I�I�x");

                            UpdateMainMessage("�A�C���F�E�E�E�I�I�I�@�܂����I�I�I");

                            UpdateMainMessage("���i�F�ǂ���琬���̂悤�ˁB");

                            UpdateMainMessage("���i�F������A�m�[�_���[�W��B���ɂ͂܂�œ������ĂȂ���B");

                            UpdateMainMessage("�A�C���F���������A�{�C����B�����A�L���ł����Ď��ɃR�����ƁA���܂������񂶂�Ȃ����B");

                            UpdateMainMessage("���i�F�ł���_����R����݂����ˁB");

                            UpdateMainMessage("���i�F�܂����B�r���������͌��ʂ��Ȃ��́B");

                            UpdateMainMessage("�A�C���F�������A�܂������҂��\����`���������炩�B");

                            UpdateMainMessage("���i�F�����đ��B���̖h��̐����ێ����Ă����Ȃ��Ɣ������Ȃ���ˁB");

                            UpdateMainMessage("���i�F�����đ�O�B�ꎞ�I���@������A���^�[���o�Ə������Ⴄ��B");

                            UpdateMainMessage("�A�C���F�܂�����Ȗ��@���i�����@��������A�~�Q���x�������E�E�E");

                            UpdateMainMessage("���i�F�����E���@�Ƃ��Ɋ��S�h�䂾���炱�̂��炢�̐����͂����Ă����������Ȃ����Ď��ˁB");

                            UpdateMainMessage("�A�C���F���肪�����炳�܂ȍU���Ԑ�����������A���΂悳�������ȁB");

                            UpdateMainMessage("���i�F�{�X�킮�炢�����g�������Ȃ������m��Ȃ���ˁA�C�U�Ƃ������g���Ă����Ƃ����B");

                            md.Message = "�����i�̓����E�C���[�j�e�B���K�����܂�����";
                            md.ShowDialog();
                            sc.OneImmunity = true;
                        }
                        if (sc.Level >= 27 && !sc.WhiteOut)
                        {
                            UpdateMainMessage("���i�F�k�u�Q�V�����ǁE�E�E���������q���C�}�C�`��ˁB");

                            UpdateMainMessage("�A�C���F�����ǂ��������Ƃ��ł�����̂��H");

                            UpdateMainMessage("���i�F������A���ł�������B�ł��M�����o���C�}�C�`�Ȃ̂�B");

                            UpdateMainMessage("�A�C���F�ł��ᛂ͋N�����Ă˂����ȁB�M���̂͊ԈႢ�Ȃ����������B");

                            UpdateMainMessage("���i�F�ȁE��E�ŁE���E���E�āI�H");

                            UpdateMainMessage("�A�C���F�͂��A�͂��͂��A�C�̂����ł����B�b�n�n�n�E�E�E");

                            UpdateMainMessage("���i�F�ǂ��������̌܊����Ⴆ�Ȃ��̂�B�C�̂���������ˁE�E�E");

                            UpdateMainMessage("���i�F�E�E�E�����A�R���g�������ˁB���悵�A�o�J�A�C���Ŏ����Č����B");

                            UpdateMainMessage("�A�C���F�_���[�W�n�͑��̑Ώە��ɂ��悤�ȁH���i�搶�B");

                            UpdateMainMessage("���i�F���A�z���b�g���������̂Ɏ䂩���̂����m��Ȃ���B�������AWhiteOut�I");

                            UpdateMainMessage("�A�C���F�����I�E�E�E�E�I�I�H�H�H�A�̂������E�E�E�����Ă������������I�I");

                            UpdateMainMessage("���i�F�A�C���̑̂ɒʂ��Ă���S�܊��_�o�Ƀ_���[�W��^������A������ˁ�");

                            UpdateMainMessage("�A�C���F�������ĂĂĂĂĂ��Ă������I�I�~�ߎ~�߁I�^���}�I�I�A�_�_�_�_�I�I");

                            UpdateMainMessage("�A�C���F�������A�����瓪����ځA�葫�A�̒����q�f�F�ɂ݂��B���ق��Ă����B");

                            UpdateMainMessage("���i�F������E�E�E��������Ȃ���ˁB");
                            
                            UpdateMainMessage("���i�F�悵�A��������");

                            UpdateMainMessage("�A�C���F�C�f�f�f�f�f�f�f�f�f�f�f�I�I�I");

                            md.Message = "�����i�̓z���C�g�E�A�E�g���K�����܂�����";
                            md.ShowDialog();
                            sc.WhiteOut = true;
                        }
                        if (sc.Level >= 28 && !sc.BloodyVengeance)
                        {
                            UpdateMainMessage("���i�F�����ɂk�u�t�o���ď��ˁ�");

                            UpdateMainMessage("�A�C���F���i���A���܂��M�������Ăk�u�t�o�O���H�k�u�t�o�����H");

                            UpdateMainMessage("���i�F���`��A�ʂɂ��̎��̋C�������B");

                            UpdateMainMessage("�A�C���F�������A���Ⴀ��Ă��B���i�A����͐S���Â܂鉽���ɂ���B");

                            UpdateMainMessage("���i�F������A���悻��B�܂�Ŏ��������r���݂�������Ȃ��H");

                            UpdateMainMessage("�A�C���F���i�A���炵���Ȃ邽�߂ɁA���C�g�j���O�L�b�N�͓P�p����B");

                            UpdateMainMessage("���i�F�ց`�A�P���J������Ȃ���@�悵���߂���B");

                            UpdateMainMessage("���i�F�������X�ɂ������čō��̗͂łԂ��ׂ��ďグ����");

                            UpdateMainMessage("���i�F�����A�s�����A�o�債�Ȃ�����ˁE�E�EBloodyVengeance�I");

                            UpdateMainMessage("        �w���i�̌��ɍ��܂Ō������������͂̃I�[�����Ïk�����I�x");

                            UpdateMainMessage("�A�C���F�����܂��E�E�E�X�Q�G�̂������ȁE�E�E");

                            UpdateMainMessage("���i�FVoidExtraction��EnigmaSence�̃_�u���A�g��A�b�n�A�A�@�@�@�I");

                            UpdateMainMessage("�A�C���F���I���������������I�I�I");

                            UpdateMainMessage("���i�F�E�E�E�ӂ�A���ǎ~�߂��Ⴄ�̂ˁB");

                            UpdateMainMessage("�A�C���F���i�A���O�ǂ񂾂����ݐ����߂Ă񂾁B�}�W�ł�ׂ����āB");

                            UpdateMainMessage("���i�F�ł��A�A�C���͍��̂ł������~�߂�ꂽ�̂�ˁB");

                            UpdateMainMessage("���i�F�A�C���A���ɑ΂��Ă�����𔲂��ĂȂ��H");

                            UpdateMainMessage("�A�C���F�o�J�����A��͔����Ă˂����āB");

                            UpdateMainMessage("���i�F���Ƃ�����A���o���������P�ˁB���[���A������������������C���ˁB");

                            UpdateMainMessage("�A�C���F����A���₢��A�蔲�����Ă˂����Č����Ă邾��H���̂����ăX���X�����B");

                            UpdateMainMessage("���i�F�b�t�t�A�ǂ����B�܂��A���x�̋@��ᎄ���{�C��������ˁ�");

                            UpdateMainMessage("�A�C���F�����A���݂��蔲���������I�K���K���������āB�@�b�n�b�n�b�n�I�I");

                            md.Message = "�����i�̓u���b�f�B�E���F���W�F���X���K�����܂�����";
                            md.ShowDialog();
                            sc.BloodyVengeance = true;
                        }
                        if (sc.Level >= 29 && !we.AlreadyLvUpEmpty23)
                        {
                            UpdateMainMessage("���i�F�ǂ��������^��������ł�������̂�����E�E�E");

                            UpdateMainMessage("�A�C���F���i�A�k�u�t�o���߂łƂ��B");

                            UpdateMainMessage("���i�F���`��E�E�E�R�����ʖڂ����ˁE�E�E�����H����A�C���B");

                            UpdateMainMessage("�A�C���F����A�����炨�߂łƂ����ƁB�}�Y�C�܂������̓W�J��");

                            UpdateMainMessage("���i�F�������āE�E�E�����A�����I����Ȃ񂶂�J�E���^�[�őʖڂ���Ȃ��́I");

                            UpdateMainMessage("�A�C���F���āA���Ă��āE�E�E");

                            UpdateMainMessage("���i�F������ƁI�I�����̃A�C���I");

                            UpdateMainMessage("�A�C���F�͂��I�͂��͂��͂��A���������I�H");

                            UpdateMainMessage("���i�F�J�E���^�[�ɔ����Ă鑊��̃J�E���^�[�����j�������");

                            UpdateMainMessage("���i�F�J�E���^�[���m�ɂȂ����Ƃ��A��ɐ�s�Ō������ɂ�");

                            UpdateMainMessage("���i�F�ǂ�����Ηǂ��̂�I�H�����Ɠ����Ȃ�����ˁI�I");

                            UpdateMainMessage("�A�C���F�������Ƃ��ȁB����͂��ȁB�܂��A�҂đ҂đ҂ĂƂ肠�����\�͔��΁B");

                            UpdateMainMessage("�A�C���F�J�E���^�[���m�̂ɂ�ݍ�������H�悭���鎖���B�������Ƒ҂đ҂đ҂āB");

                            UpdateMainMessage("�A�C���F�������ȁE�E�E�w�����ɑł�����ł݂�x�B�ǂ����I�H�b�n�b�n�b�n�I");

                            UpdateMainMessage("�@�@�@�w�h�u�b�V�C�B�B�I�I�x�i���i�̃J�E���^�[���C�g�j���O���A�C�����y��j�@�@");

                            UpdateMainMessage("���i�F����A�Ȃ�قǂˁ�@���𗝉��B");

                            UpdateMainMessage("�A�C���F�����E�E�E���ݓI�Ɍ����Ă����ʂȂ̂���E�E�E�_��E�E�E");

                            UpdateMainMessage("���i�F�A�C���A�Ƃ���ň���������񂾂��ǁB");

                            UpdateMainMessage("�A�C���F�ʖځI���߂��߂��I�@�����͂��������܂ŁI�I");

                            UpdateMainMessage("���i�F����B�@�t������������ˁB");

                            UpdateMainMessage("�A�C���F���͂ȁA���O�ɏ\���t�������Ă�Ǝv�����H");

                            UpdateMainMessage("���i�F�z���g���ƈ������B");

                            UpdateMainMessage("���i�F�_���[�W���[�X�̍Ō�A���ߎ�ƂȂ�X�L���͉����ǂ�������H");

                            UpdateMainMessage("�A�C���F�n�[�C�n�C�n�C�n�C�I���^�C���I�@�܂����T�I�I");

                            UpdateMainMessage("���i�F���Ŗ������I��点�悤�Ƃ��Ă�̂�I�C���C�������ˁ��");

                            UpdateMainMessage("�@�@�@�w�Q���H�b�V���A�A�@�A�@�I�I�x�i���i�̃C�m�Z���g�u���[���A�C�����y��j�@�@");

                            md.Message = "�����i�͉����K���ł��܂���ł�����";
                            md.ShowDialog();
                            we.AlreadyLvUpEmpty23 = true;
                        }
                        if (sc.Level >= 30 && !sc.PromisedKnowledge)
                        {
                            UpdateMainMessage("���i�F�������I���ɂk�u�R�O���B���");

                            UpdateMainMessage("�A�C���F���������˂����I���i�A���߂łƂ��I");

                            UpdateMainMessage("���i�F�b�t�t�t�A���肪�ƃA�C����");

                            UpdateMainMessage("�A�C���F�i�܂��������i���炱�����Ă��E�E�E�j�@����͉���M�����肾�H");

                            UpdateMainMessage("���i�F���ɂ��邩�͑�̌��߂Ă���́B�����@��B");

                            UpdateMainMessage("�A�C���F�����@���A����E�h�q���@�͌��\�����ė��Ă��邵�ȁA�ǂ�Ȃ̂ɂ���񂾁H");

                            UpdateMainMessage("���i�F���͂̌���͒m�͂�ˁB");

                            UpdateMainMessage("���i�F�S�Ă̌��A����͐���ˁB");

                            UpdateMainMessage("���i�F�����V�g�Ƃ̌_��ōł����͂Ȃ��̂ɂȂ�C�������E�E�E�������B");

                            UpdateMainMessage("���i�F�����V�g��A��Ɍ×�����z���ꂽ�m�����������܂��APromisedKnowledge��B");

                            UpdateMainMessage("�A�C���F�܂����A�m�͂��̂��̂��グ�閂�@���H");

                            UpdateMainMessage("���i�F��������@����͐�����ˁB���@�͎��͓̂��R�オ�郏�P�����ǁB");

                            UpdateMainMessage("�A�C���F���@�֘A�S�ʂ��S�ʓI�ɋ�������銴�����ȁB���Ȃ苭���˂����H����B");

                            UpdateMainMessage("���i�F�����ˁA����ȊO�̂a�t�e�e�n�t�o�����������Ǝv���Ă����Ηǂ���B");

                            UpdateMainMessage("�A�C���F����A�z���g��邶��˂����B�������i�̕����M���Z���X������Ď����H");

                            UpdateMainMessage("���i�F�ł��o�J�A�C�������Ă��܂ɔ������ۂ��̎v��������Ȃ��B����̂͏�����ˁB");

                            UpdateMainMessage("���i�F�������A���@���g���҂ɂƂ��Ă͂����炭�p�ɂɋ�g���邩��ˁB���҂��ĂĂ��傤������");

                            md.Message = "�����i�̓v���~�X�h�E�i���b�W���K�����܂�����";
                            md.ShowDialog();
                            sc.PromisedKnowledge = true;
                        }
                        if (sc.Level >= 31 && !sc.SilentRush)
                        {
                            UpdateMainMessage("���i�F���Ɏ����k�u�R�P�ˁB�A�C���͂k�u�R�P�ł́A����M���Ă��́H");

                            UpdateMainMessage("�A�C���F�����H���͂��̎��́E�E�E�m�������X�L���n�������ȁB");

                            UpdateMainMessage("���i�F�X�L���n���ĉ��ƂȂ������ǁA���ڂ̊��G���厖���Ǝv��Ȃ��B");

                            UpdateMainMessage("�A�C���F�܂����������m��Ȃ��ȁB�����A�X�L���n��M�����̂��H");

                            UpdateMainMessage("���i�F���Ȃ�ɂ�����Ƃ����R���{���v�������̂�A�ŏ��̈ꔭ�̓L���Ă��������́B");

                            UpdateMainMessage("�A�C���F�������A�悵���Ⴀ������������ɂȂ��Ă��B�������ė����I");

                            UpdateMainMessage("���i�F�����C�C���Č����܂ŁA�^���ɍ\���ĂĂ�ˁB������s�����E�E�E");

                            UpdateMainMessage("�A�C���F������I���ł��ǂ����I");

                            UpdateMainMessage("���i�F�X�E�E�D�D�D�E�E�E");

                            UpdateMainMessage("�A�C���F�E�E�E��H");

                            UpdateMainMessage("�@�@�@�w�A�C���͂��̈�u�����A���i�̎p�����������I�x");

                            UpdateMainMessage("�A�C���F�E�E�E�I�H�����܂��I�I�I");

                            UpdateMainMessage("���i�F�H�炢�Ȃ����ASilentRush�I�@���A�@�I");

                            UpdateMainMessage("�@�@�@�w�b�h�I�b�Y�h�h���I�I�x�i�A�C���ɂR���̃_���[�W���������I�j");

                            UpdateMainMessage("�A�C���F�b�`�A���������I�I�@���̂ǂ�������񂾁H�ڂ̑O�ɂ����������B");

                            UpdateMainMessage("���i�F�������[�V�����̃X�e�b�v���p��B�����O�Ɍ��e���c��悤�ɃX�e�b�v�𓥂ނ́B");

                            UpdateMainMessage("���i�F����ŁA���������\�R�ɂ������̂悤�ȍ��o��^�����܂܁A���֔�э��߂��B");

                            UpdateMainMessage("�A�C���F�������\�e�N�j�b�N�g���Ă������ȁB���i�A�����Ɉ�t�H�킳�ꂽ���B");

                            UpdateMainMessage("���i�F�ł������i�K���܂߂ăX�L���ʂ͌��\�g����������B�T�d�Ɏg��Ȃ��ƑʖڂˁB");

                            UpdateMainMessage("�A�C���F���₵�����C�z�����c���Ď��̍s���Ɉڂ�Ȃ�ăz���g��邶��˂����B");

                            UpdateMainMessage("���i�F����A���������̂͂ǂ�ǂ񉞗p��������B���҂��Ă�������ˁ�");

                            md.Message = "�����i�̓T�C�����g�E���b�V�����K�����܂�����";
                            md.ShowDialog();
                            sc.SilentRush = true;
                        }
                        if (sc.Level >= 32 && !sc.CarnageRush)
                        {
                            UpdateMainMessage("���i�F�k�u�R�Q�ɂ��Ȃ��Ă���ƁA����w�̑M�����~�����Ƃ���ˁB");

                            UpdateMainMessage("�A�C���F���̑O�����Ă��ꂽ�A�T�C�����g���b�V���B���X�g�����肪�ǂ��������ȁB");

                            UpdateMainMessage("���i�F�����ˁA�ł�����͂����Ɖ��p�����������Ȃ񂾂��ǂˁB");

                            UpdateMainMessage("���i�F�����A��������B�A�C�����悭����Ă�_�u���E�X���b�V���������Ă�B");

                            UpdateMainMessage("�A�C���F��H�����A�ܘ_�I�[�P�[���B����A�s�����I");

                            UpdateMainMessage("���i�F�����ꂻ��A�ǂ�����Č��̐U��؂�r���ŕς��Ă���̂�H");

                            UpdateMainMessage("�A�C���F�ǂ��H�@�ǂ��ƌ����Ă��ȁE�E�E");

                            UpdateMainMessage("�A�C���F�ŏ�������r���ŕς������ł����Ċ������ȁB�C�܂���ŕς��Ă�킯����˂��B");

                            UpdateMainMessage("�A�C���F�v����ɂQ�񓖂Ă鎖��O��ɍŏ�������K���ς��������Ď����B");

                            UpdateMainMessage("���i�F�Ȃ�قǂˁB�킩������B������Ƃ���Ă݂邩��A��������āB");

                            UpdateMainMessage("�A�C���F�I�[�P�[�B�@���̂��炢�ŗǂ����H");

                            UpdateMainMessage("���i�F����A�\����B���Ⴀ�s�����B");

                            UpdateMainMessage("���i�F�E�E�E�@�E�E�E");

                            UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                            UpdateMainMessage("���i�F�E�E�E�@��ڂ�A�b�n�C�I");

                            UpdateMainMessage("�@�@�@�w�b�h�I�x�@");

                            UpdateMainMessage("���i�F��ڂ���̘A���A�b�Z�C�I");

                            UpdateMainMessage("���i�F���A�@�I");

                            UpdateMainMessage("���i�F�b�t�I");

                            UpdateMainMessage("�@�@�@�w�b�h�h�h�I�x�@");

                            UpdateMainMessage("���i�F�Ō�Ƀg�h���A�b�n�A�A�@�@�I");

                            UpdateMainMessage("�@�@�@�w�Y�b�h�I�I�I�H�H���I�I�x�@");

                            UpdateMainMessage("�@�@�@�w�b�o�^�E�E�E�x�@�i�A�C���͂��̏�œ|�ꂽ�j");

                            UpdateMainMessage("���i�F�������I�I�Y��ɃN���[���q�b�g�����ł���H�ǂ���������");

                            UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E�@�E�E�E");

                            UpdateMainMessage("�@�@�@�w�E�E�E�b�Q�V�x�@�i���i�̑��̍��m�F�L�b�N���A�C���Ƀq�b�g�j");

                            UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E�@�E�E�E");

                            UpdateMainMessage("���i�F������E�E�E����Ȃ�A�C���ɏ����ɏ��Ă����ˁ�@���K�ɗ�������Ɓ�");

                            UpdateMainMessage("���i�F�����P��A��ڂ���s�����A�b�n�C�I");

                            md.Message = "�����i�̓J���l�[�W�E���b�V�����K�����܂�����";
                            md.ShowDialog();
                            sc.CarnageRush = true;
                        }
                        if (sc.Level >= 33 && !we.AlreadyLvUpEmpty24)
                        {
                            UpdateMainMessage("���i�F�E�E�E���`��E�E�E�ǂ��Ȃ̂�����˃z���g�̂Ƃ��B");

                            UpdateMainMessage("�A�C���F�������H");

                            UpdateMainMessage("���i�F�A�C���A�����Ɠ����Ȃ�����B");

                            UpdateMainMessage("�A�C���F�������H");

                            UpdateMainMessage("���i�F�J���l�[�W���b�V���̃N���[���q�b�g��B");

                            UpdateMainMessage("�A�C���F����A���܂˂��B�}�W��K.O������Ċo���Ă˂��B");

                            UpdateMainMessage("���i�F������Ȃ�ł��Y��Ɍ��܂肷����B�����Ɍ����Ȃ����B");

                            UpdateMainMessage("���i�F�A�C���A���Ɏ�������Ă�ł���H");

                            UpdateMainMessage("�A�C���F��������Č����ƌ䕼������ȁE�E�E���Č����񂾁B");

                            UpdateMainMessage("�A�C���F���i�ɒv������^���鎖�͂ł��˂��B���Ƃ�DUEL�ł����Ă����B");

                            UpdateMainMessage("�A�C���F������E�E�E���̂Ȃ񂾁B������͂��Ă˂����āB");

                            UpdateMainMessage("���i�F���`��A���������������B");

                            UpdateMainMessage("���i�F�v����ɁA�A�C���͎����肾�Ɩ{�C���o�ĂȂ����Ď���B");

                            UpdateMainMessage("�A�C���F���܂˂��ȁB���������C�ɂ����Ă����B");

                            UpdateMainMessage("���i�F�ǂ��̂�A�����{�C�ɂ������郌�x������Ȃ������炢�������Ă�����B");

                            UpdateMainMessage("�A�C���F���₢��A���������Ӗ�����˂��B");

                            UpdateMainMessage("���i�F���́u���₢��v���Č����̎~�߂Ȃ�����B");

                            UpdateMainMessage("�A�C���F�܂��E�E�E���������B�������ȁA�m���ɖ{�C�ɓP������Ă˂��B");

                            UpdateMainMessage("�A�C���F�����f�B�t�����炷���A���͒N������ł��{�C�ɂȂ�Ă˂��݂������B");

                            UpdateMainMessage("�A�C���F���i�A������͂����Ɩ{�C�ő��肷�邺�B");

                            UpdateMainMessage("���i�F����A���̒��q�ŗ��Ȃ�����B�y���݂ɂ��Ă����");

                            md.Message = "�����i�͉����K���ł��܂���ł�����";
                            md.ShowDialog();
                            we.AlreadyLvUpEmpty24 = true;
                        }
                        if (sc.Level >= 34 && !sc.Damnation)
                        {
                            UpdateMainMessage("���i�F�k�u�R�S�Ƃ��Ȃ�ƈ��グ��̂���ςɂȂ��Ă�����B");

                            UpdateMainMessage("�A�C���F���̕ӂ���͂ǂ�ǂ񌵂����Ȃ邩��ȁB�ŁA�ǂ����H");

                            UpdateMainMessage("���i�F�h�R��Ԃ����܂ꂽ���̔��������Ă��������Ǝv��Ȃ��H");

                            UpdateMainMessage("�A�C���F�h�R��Ԃ��H�܂��A�ɂ�ݍ����ɂȂ��Ă���Ɗm���ɗ~�����ȁB");

                            UpdateMainMessage("���i�F�������Ȃ��܂܂Ȃ�A�ǂ�ǂ񌵂�����ԂɊׂꂽ���́B");

                            UpdateMainMessage("���i�F�ƌ������ƂŁA�R�R�͈Ŗ��@�Ō��܂�ˁ�");

                            UpdateMainMessage("���i�F��������A�Ȃ����i���I�Ȃ��̂��ǂ���ˁB�ǂ�ǂ񉟂��ׂ����悤��");

                            UpdateMainMessage("�A�C���F���܂��͂܂��A�ǂ����Ă���ȉߌ��ȁE�E�E");

                            UpdateMainMessage("���i�F�ǂ�����Ȃ��́A�ʂɁB�b�t�t�A�łт��}����Ɨǂ���o�J�A�C���ADamnation�I");

                            UpdateMainMessage("�@�@�@�w�A�C���̎��͂ɍ�������Ԃ��������n�߂��I�I�x");

                            UpdateMainMessage("�A�C���F�����I�H������R���E�E�E�b�O�A�O�A�A�@�I");

                            UpdateMainMessage("���i�F���̍�������Ԃ̓A�C���A���Ȃ��̗̑͂����X�ɐI��ł�����B�����A�������Ȃ����B");

                            UpdateMainMessage("�A�C���F�m���ɂ��̂܂Ƃ��߂��Ń��C�t���D����񂶂ᓮ������𓾂Ȃ��ȁB�����A�C�b�c�c�c�I");

                            UpdateMainMessage("���i�F�A�C���A���߂�Ȃ����B�\�������ł��Ȃ��݂����B");

                            UpdateMainMessage("�A�C���F�C�b�c�c�c�E�E�E�����I�H�����ƁI�H");

                            UpdateMainMessage("���i�F���̉i�����@��DispelMagic��Cleansing���Ꮬ���ł��Ȃ��悤�Ȃ́B���߂�ˁ�");

                            UpdateMainMessage("�A�C���F���Ⴀ�A�Ƃ��ƂƃP�������˂��ƂP�O�O�������������܂�����˂�����I");

                            UpdateMainMessage("���i�F�����Ă���ԂɁA���\�������݂����ˁ�@����A���̌��ߋZ�ł��H����Ă��傤�����B");

                            UpdateMainMessage("�A�C���F�Ă߂�����Ȃ̃A������I�H�o���Ă₪���I�@�C�b�c�c�c�I�O�A�A�@�I");

                            md.Message = "�����i�̓_���l�[�V�������K�����܂�����";
                            md.ShowDialog();
                            sc.Damnation = true;
                        }
                        if (sc.Level >= 35 && !sc.StanceOfDeath)
                        {
                            UpdateMainMessage("���i�F�k�u�R�T���B���Ɓ�@�������悢�悠�ƂT�Ɣ�������B");

                            UpdateMainMessage("�A�C���F���i�A���O�̌����w�Áx�̃X�L�����ĉ��ɂƂ�����ʓ|�Ȃ̂΂��肾�ȁB");

                            UpdateMainMessage("���i�F�A�C�����K�����Ă���w���x�Ǝ��́w�Áx���āw�o�Ɂx�ƌĂ΂��֌W�ɂ���炵�����B");

                            UpdateMainMessage("���i�F������A�C���ɂƂ��Ă͖��Ȃ̂΂���ɂȂ�̂�B");

                            UpdateMainMessage("�A�C���F�o�ɁH��������́A�����������˂��ȁB");

                            UpdateMainMessage("���i�F�܂������؂�K���c�f������ɂł������Ă݂�Ɨǂ����B�����A������炢�ˁB");

                            UpdateMainMessage("�A�C���F�������H");

                            UpdateMainMessage("���i�F�A�C�����ʓ|�������肻���ȃX�L�����ƁE�E�E����A���茈���");

                            UpdateMainMessage("�A�C���F�����A�҂đ҂āB����ȑM�����͂˂�����B");

                            UpdateMainMessage("���i�F�_���[�W�n�ɂƂ��Ĉ�Ԗ��Ȃ̂́A�ϐ��E�y���E�z���E�h�ǂ݂����Ȃ��̂�ˁH");

                            UpdateMainMessage("�A�C���F�܂��������ȁA���C�t�O�ɂȂ��Ă���˂��Ƃ���������z���g�ʓ|�������ȁB");

                            UpdateMainMessage("���i�F�A�C���A�����͐����Ⴆ�Ă��ˁB���̃A�C�f�A�͉���I��AStanceOfDeath�Ɩ��������B");

                            UpdateMainMessage("�A�C���F�܁A�܂����I�H���̃l�[�~���O�A���̂܂�܂���˂����낤�ȁI�H");

                            UpdateMainMessage("���i�F���̂܂�����B���C�t���O�ɂȂ�_���[�W�̍ہA�m����ԂŐ����c���悤�ɂȂ�́B");

                            UpdateMainMessage("�A�C���F�_�u���E�X���b�V���łP��ڂłO�A�Q��ڂŌ�������̂��H");

                            UpdateMainMessage("���i�F�������Ȃ�����@�A���_���[�W�n�̂̃X�L���͈�ƌ��Ȃ����́B");

                            UpdateMainMessage("�A�C���F�t���C���I�[���݂����Ȓǉ����ʌn�́H");

                            UpdateMainMessage("���i�F�����������c�����@��̍U���Ƃ��Č��Ȃ�����ˁB");

                            UpdateMainMessage("�A�C���F�Q�C���E�B���h�łQ��s���ł̘A���U���́H");

                            UpdateMainMessage("���i�F�����c�����@�Q��s�����Q��U�����P�^�[�����ł͓�������B");

                            UpdateMainMessage("�A�C���F�E�E�E�ʓ|�����������������������I�I�I�I�I");

                            md.Message = "�����i�̓X�^���X�E�I�u�E�f�X���K�����܂�����";
                            md.ShowDialog();
                            sc.StanceOfDeath = true;
                        }
                        if (sc.Level >= 36 && !sc.OboroImpact)
                        {
                            UpdateMainMessage("���i�F����Ƃk�u�R�U��B�k�u�R�T���琏���Əオ��h���Ȃ��Ă��ˁB");

                            UpdateMainMessage("�A�C���F�������ȁA��������͂���ǂ��Ȃ邺�B");

                            UpdateMainMessage("���i�F�ł��o���l���҂��ł�ԂɁA���낢��ƍl������B");

                            UpdateMainMessage("���i�F���A�K���c�f������ɂ��낢�닳���Ă�����Ė{���ɗǂ������Ǝv���Ă��B");

                            UpdateMainMessage("�A�C���F�����A�����b����ꂽ��ȁB�����ȑO�̂��O�Ƃ͔�ׂ��̂ɂȂ�˂����ȁB");

                            UpdateMainMessage("���i�F�����Ă�����Ă鎖�łǂ����Ă���肭������Ȃ���������̂�B");

                            UpdateMainMessage("���i�F�i���}�l�j�w�K���c�F���i�N�A�́A�Z�A�m�A�S���S�ē������K�v�Ɗo���Ă����Ȃ����B�x");

                            UpdateMainMessage("���i�F�i���}�l�j�w�K���c�F�̌n�ƃC���[�W�͓���Ɗo���Ă����Ȃ����B�x");

                            UpdateMainMessage("�A�C���F�b�n�b�n�b�n�I�@���i�����}�l����̂��Đ����ʔ����ȁA�b�n�b�n�b�n�I");

                            UpdateMainMessage("���i�F��������ƁA�����͂ǂ��ł��ǂ�����Ȃ��́A���g�𕷂��Ă�ˁB");

                            UpdateMainMessage("���i�F�����������e�A�悤�₭���������C������̂�B����Ă݂��B");

                            UpdateMainMessage("�@�@�@�w���i�͐퓬�̍\���ł͂Ȃ��A��̊�ȑ̌n��`���n�߂��B�x");

                            UpdateMainMessage("�@�@�@�w���i�̗���͗��̂悤�ȉ~��`���A�����͎��R�Ɨ���n�߂�悤�Ȉʒu�Ɏ�����B�x");

                            UpdateMainMessage("�@�@�@�w���i�̎p�����̏�łQ�d�R�d�ƌ��e���o�n�߂��I�x");

                            UpdateMainMessage("�A�C���F�I�I�H�@����́I�I");

                            UpdateMainMessage("���i�F�s�����E�E�E�t�E�E�D�D�E�E�E�������A�@�y���ɉ��`�zOboroImpact�I");

                            UpdateMainMessage("�@�@�@�w���i�̈�M���_�~�[�f�U��N�ɃN���[���q�b�g�����I�I�x");

                            UpdateMainMessage("�A�C���F���E�E�E�����������I�I�I�I�I");

                            UpdateMainMessage("���i�F�b�n�A�@�A�A�@�@�@�E�E�E�ʖڂˁB���������R���B");

                            UpdateMainMessage("�A�C���F���A���܁A���������������o���������B���Ȃ񂾍��̂́I�I�I");

                            UpdateMainMessage("���i�F�K���c�f�����񂪓`�����Ă��ꂽ���t�ʂ��B�́A�Z�A�m�A�S�̑S�Ă𒍂��ł݂��́B");

                            UpdateMainMessage("���i�F���Ƃ̓C���[�W��ˁA���̂悤�ȗ����^�ŗ���o��悤�ȃC���[�W�B");

                            UpdateMainMessage("�@�@�@�w�A�C���̊炩��͒����������̏Ί炪�����Ă���B�x");

                            UpdateMainMessage("�A�C���F���E�E�E���܂����B");

                            UpdateMainMessage("���i�F�ł��A���̂܂܂���s���S����B���������ƃG�b�Z���X���K�v�Ȃ̂�E�E�E�����H����H");

                            UpdateMainMessage("�A�C���F���܂��A�����{���Ɍ����p���ăs�b�^�����ȁB");

                            UpdateMainMessage("���i�F���H�E�E�E���������������H�H������A���}�W��ɂȂ��Ă�̂�I�H");

                            UpdateMainMessage("�A�C���F����A���̃X�L���B���ɂ͓���ł������ɂ˂��Ǝv�����̂��B");

                            UpdateMainMessage("���i�F�܁A�܂��A�A�A�A�C���ɂ͈Ⴄ�����̃X�L�������邶��Ȃ��H");

                            UpdateMainMessage("�A�C���F�����A���������ǂ��E�E�E");

                            UpdateMainMessage("���i�F�����Ɖ��Ńw�R��ł�̂�H�b�T�T�A���̓A�C���̔Ԃ��");

                            UpdateMainMessage("�A�C���F�E�E�E����");

                            UpdateMainMessage("���i�F�ςȏ��ŗ������ނ�˃z���g�B�V�������Ƃ��Ȃ�����I");

                            UpdateMainMessage("�A�C���F���A�����A�����A�I�[�P�[�I�[�P�[�B");

                            UpdateMainMessage("���i�F�D�����A�C������Ȃ��āA�����̃o�J�A�C���ɖ߂�Ȃ�����ˁB���q��������B");

                            UpdateMainMessage("�A�C���F���₢��A���Ȃ肷�����X�L������������ȁB�����r�r�������B");

                            UpdateMainMessage("�A�C���F�܂������������Ⴂ���˂��B���i����݂̂��ȁB");

                            UpdateMainMessage("���i�F�i���}�l�j�w�K���c�F�A�C����A���i���Ȃ����x");

                            UpdateMainMessage("�A�C���F�b�n�b�n�b�n�I���O�̂��ꂷ�������Ă�ȁB�����A���i���邺�A�C���Ă����ȁI");

                            UpdateMainMessage("���i�F�b�t�t�A�m�[�e���L�ˁB�������A�������i���Ă������A���ĂȂ�����ˁ�");

                            md.Message = "�����i�͞O�E�C���p�N�g���K�����܂�����";
                            md.ShowDialog();
                            sc.OboroImpact = true;
                        }
                        if (sc.Level >= 37 && !sc.AbsoluteZero)
                        {
                            UpdateMainMessage("���i�F��������@�k�u�R�V�ɓ��B�ˁ�");

                            UpdateMainMessage("�A�C���F���i���A���������l�`�w�S�O���ȁB");

                            UpdateMainMessage("���i�F���ˁA�����@�ōō��ɗǂ����͉̂������Ă����ƍl���Ă����́B");

                            UpdateMainMessage("�A�C���F�ǂ�Ȃ̂��H");

                            UpdateMainMessage("���i�F�u������ԁv���Ă����̂ɐ��������̂�ˁB�S�����������Ⴆ�΂����̂�B");

                            UpdateMainMessage("�A�C���F���O�͂��܂ɂ��������ߌ��Ȕ����������ȁE�E�E");

                            UpdateMainMessage("�A�C���F���āA�܂����I�H");

                            UpdateMainMessage("���i�F���������͖��S��A�H�炢�Ȃ����B�@AbsoluteZero�I");

                            UpdateMainMessage("�@�@�@�w�b�s�L�[�[�[�[�[���I�x");

                            UpdateMainMessage("�A�C���F�����I�E�E�E���A����́E�E�E�ʖڂ��A�����S�R�������E�E�E");

                            UpdateMainMessage("���i�F�������A�����ˁB�����������Ă������A��[�������Ȃ�����ˁ�");

                            UpdateMainMessage("�A�C���F���������A�����₪�����H�@�I���@�I");

                            UpdateMainMessage("���i�F�����A�U�������͂ł���̂�B���S�����H��");

                            UpdateMainMessage("�A�C���F�^��n�Ɂ􂭂����₪���āE�E�E���S�ł��邩��A�]�v�s�����L���������B");

                            UpdateMainMessage("���i�F�y���̂P�z�o�J�A�C���̓X�L�����g���Ȃ��Ȃ�����");

                            UpdateMainMessage("�A�C���F�����ƁI�H�E�E�E�L�E�E�E�L�l�e�B�b�E�E�E");

                            UpdateMainMessage("�A�C���F�ʖڂ��A�S�R�\�������˂��E�E�E");

                            UpdateMainMessage("���i�F�y���̂Q�z�o�J�A�C���͖��@���g���Ȃ��Ȃ�����");

                            UpdateMainMessage("�A�C���F�}�W����I�����������E�E�E�Q�C���E�E�E�E�B�E�E�E");

                            UpdateMainMessage("�A�C���F�b�N�\�I�S�R�A�r���`�Ԃ��E�E�E");

                            UpdateMainMessage("���i�F�y���̂R�z�o�J�A�C���̓��C�t�񕜂��o���Ȃ��Ȃ�����");

                            UpdateMainMessage("�A�C���F���������A����Ȃ̂܂ŃA������I�H�|�[�V�����͂������ɁE�E�E");

                            UpdateMainMessage("�A�C���F�i�b�S�N�b�S�N�j�E�E�E�I�܂�Ō����Ă˂��I�I");

                            UpdateMainMessage("���i�F�y���̂S�z�o�J�A�C���͖h��̍\�������Ȃ��Ȃ����B����A�s����ˁ�");

                            UpdateMainMessage("�A�C���F�b�o�I�R����I�H�������ɍ\�����炢�E�E�E���E�E�E�b�N�I�I�I�H�H�I�I");

                            UpdateMainMessage("�@�@�@�w�b�{�b�S�I�I�H�I�I�H�H���I�I �i���i�̃A�C���o�Ńu���[���y��I�j�x");

                            UpdateMainMessage("�A�C���F���E�E�E����Ȃ̃A�����E�E�E�@�@�@�i�b�p�^�j");

                            UpdateMainMessage("���i�F�悭�l������OboroImpact�Ƃ̋���ȃR���{�ɂȂ肻���ˁB");

                            UpdateMainMessage("���i�F�b�t�t�A�����̏��͂��̕ӂɂ��Ƃ��Ă�������");

                            UpdateMainMessage("�A�C���F���ނ��炻�̕ӂɂ��Ă����Ă���E�E�E");

                            md.Message = "�����i�̓A�u�\�����[�g�E�[�����K�����܂�����";
                            md.ShowDialog();
                            sc.AbsoluteZero = true;
                        }
                        if (sc.Level >= 38 && !we.AlreadyLvUpEmpty25)
                        {
                            UpdateMainMessage("���i�F�k�u�R�W�����ǁE�E�E�n�@�E�E�E�ǂ���������ˁE�E�E");

                            UpdateMainMessage("�A�C���F�����A���i�A�₯�Ɍ��C�������ȁB");

                            UpdateMainMessage("���i�F�A�C���͂����m�[�e���L��ˁB");

                            UpdateMainMessage("�A�C���F�������Ă�񂾁A�������Ă��܂ɂ��邼�B");

                            UpdateMainMessage("���i�F����A�����Ă݂Ȃ�����B");

                            UpdateMainMessage("�A�C���F�E�E�E���������I���ĂȁB");

                            UpdateMainMessage("���i�F���悻��B�S�R�Ⴄ����Ȃ��E�E�E�n�A�A�@�@�`�`�`�`�@�@�E�E�E");

                            UpdateMainMessage("�A�C���F���k�Ȃ炢�ł�����Ă��邺�H");

                            UpdateMainMessage("���i�F�o�J�A�C���ɕ����Ă��ʖڂ��낤���ǁA�ꉞ���Ⴀ�����Ă����H");

                            UpdateMainMessage("�A�C���F�����A�����I�C���Ă������āI");

                            UpdateMainMessage("���i�F���ˁA�򑐏p�͏o����Ƃ��Ă��A���@�ƌ����p�ǂ������Ǝv���H");

                            UpdateMainMessage("�A�C���F��������B");

                            UpdateMainMessage("���i�F�ǂ����H���ĕ����Ă�̂�A��̑I����A�� or �E��A�����Ȃ�đʖڂ�B");

                            UpdateMainMessage("�A�C���F���őʖڂȂ񂾁H�ǂ�����A��������΁B");

                            UpdateMainMessage("���i�F�b�N�E�E�E�����������o�J��������I");

                            UpdateMainMessage("�A�C���F�܁A�܂��҂đ҂ė����������āB���łǂ������I�ڂ��Ƃ���񂾂�H");

                            UpdateMainMessage("���i�F�����āE�E�E���̂܂܂��ƁA�ǂ�����������Ȃ��B");

                            UpdateMainMessage("�A�C���F���O���ςȏ��ł�������Č��肵������N�Z�������ȁB");

                            UpdateMainMessage("�A�C���F�������A���i�B����A�򑐏p�͉��ő��v�Ȃ񂾂�H");

                            UpdateMainMessage("���i�F�򑐏p�͐퓬�n����Ȃ��ł���H�������ʂ��Ă�Α��v��B");

                            UpdateMainMessage("�A�C���F����A����Ȃ̂͂ǂ����B���@�͐퓬�ȊO����g���Ȃ��̂��H");

                            UpdateMainMessage("�A�C���F�_���[�W�n���@�ɂ������Đ��䂪�o���Ă�����ł������Ǝg�����͂��邾��B");

                            UpdateMainMessage("���i�F���Ⴀ�A���@�͐퓬�ȊO���Ď��ɂ����Ƃ�����");

                            UpdateMainMessage("���i�F�����p�͎c�����������ǁA���x�͖򑐏p�Ɩ��@�̂ǂ�������I�����Ȃ���ʖڂ�ˁB");

                            UpdateMainMessage("�A�C���F���΁A�ʂɂ��������Ӗ��Ō������񂶂�˂����āB���ł����Ȃ�񂾁E�E�E");

                            UpdateMainMessage("�A�C���F�������A���i�B����A������񂾁B���O�A��ʂ��Ă�Α��v�Ȃ񂾂ȁH");

                            UpdateMainMessage("���i�F�܂������ˁA�����W�������Ƃ��Ă��Ԃ�Ȃ���Α��v���Ƃ͎v�����B");

                            UpdateMainMessage("�A�C���F���Ⴀ�A����������敪�����Ă��B�ǂ����A�悭������B");

                            UpdateMainMessage("�A�C���F�y�򑐏p�z�@���Y�n");

                            UpdateMainMessage("�A�C���F�y�����p�z�@�퓬�n");

                            UpdateMainMessage("�A�C���F�y���@�p�z�@�G�������^���n");

                            UpdateMainMessage("���i�F���悻�̃G�������^���n���āB���炩�ɍ�������ł���H");

                            UpdateMainMessage("�A�C���F�ʖڂȂ̂��H���Ⴀ�A�n�C�u���b�h�n�B");

                            UpdateMainMessage("���i�F����Ȃǂ��������ȒP��ʖڂɌ��܂��Ă邶��Ȃ��B�ʖڂ�B");

                            UpdateMainMessage("�A�C���F���������A���Ⴀ�Ƃ��Ă����́E�E�E�I�[�����E���_�[�n�I");

                            UpdateMainMessage("���i�F����ȕ�I�ȒP����ʖڂ�B����A�S�R�[���ł��Ȃ�����Ȃ��́I");

                            UpdateMainMessage("�A�C���F�N�b�L���O�n�I�@�T���_�[�n�I�@�r���n�I�@�C�����[�W�����n�I�@�X�y�V�����n�I");

                            UpdateMainMessage("���i�F�E�E�E�@�E�E�E");

                            UpdateMainMessage("�A�C���F���A���₷�܂˂��E�E�E�ʖڂ��B����܂葊�k�ɏ���Ă��ĂȂ����B");

                            UpdateMainMessage("���i�F�r���n�E�E�E�Ƃ����������ˁB");

                            UpdateMainMessage("���i�F�򑐂͎��ۂɍ��Ȃ��ƑʖځA�����p�͎����̌��Œ��ړI�Ȃ��̂����B");

                            UpdateMainMessage("���i�F���@�͉r�����邱�ƂŌ��ʂ𔭓����邩��m���ɋ敪���o�������ˁB�z���g����I");

                            UpdateMainMessage("���i�F����A���̂܂܂ōs���Ă݂��A���肪�Ɓ�");

                            UpdateMainMessage("�A�C���F�E�E�E�����I�悩��������˂����I�b�n�b�n�b�n�I");

                            UpdateMainMessage("���i�F�A�C���͂��������̗ǂ���������͂��Ȃ��́H");

                            UpdateMainMessage("�A�C���F�ʂɂȂ�Ƃ��v��˂��ȁA���Ȃ������͓K���ɂ���Ƃ���ǂ�����B");

                            UpdateMainMessage("�A�C���F�X���b�V���I�@�q�[���I�@�t�@�C�A�I�@�I���@�I�@�H�炦�I");

                            UpdateMainMessage("���i�F�E�E�E����̎���ꂮ�炢�̓L�`���Ƃ��Ȃ�����ˁB�׍H�p�Ƃ��������ǂ���H");

                            UpdateMainMessage("�A�C���F�����I�I");

                            UpdateMainMessage("���i�F����σo�J�ˁE�E�E����Ӗ���������������A���ʂȂ��o�J�ˁB");

                            UpdateMainMessage("�A�C���F���ʂȂ��o�J�Ƃ͉����B�܂��A�܂��������瑊�k���Ă����I���ȁI�H");

                            UpdateMainMessage("���i�F�����ˁA�܂����x�@�����΂ˁ�");

                            UpdateMainMessage("�A�C���F�Ƃ���ł��O���A����M���͖�������H");

                            UpdateMainMessage("���i�F�b���E�E�E�@�E�E�E�@�E�E�E�@�b�T�T�A���K���K��");

                            md.Message = "�����i�͉����K���ł��܂���ł�����";
                            md.ShowDialog();
                            we.AlreadyLvUpEmpty25 = true;
                        }
                        if (sc.Level >= 39 && !sc.TimeStop)
                        {
                            UpdateMainMessage("���i�F�k�u�R�X��A�ő�S�O�܂ł��ƈ���Ɣ��������");

                            UpdateMainMessage("�A�C���F���i�̏I�ՏK��������e�͂������̂΂��肾�ȁB");

                            UpdateMainMessage("���i�F�������Ă�̂�A�A�C�������đ債�����m�΂��肶��Ȃ��́B");

                            UpdateMainMessage("�A�C���F����A���̂͂Ȃ�Č����񂾁B������O�ȋ����΂��肾��");

                            UpdateMainMessage("�A�C���F���i���K�����Ă��Ă�����͈̂ٗ�ȋ����������B����ȋC�����邺�B");

                            UpdateMainMessage("���i�F���̏ꍇ�_���[�W�n�΂�����͍D������Ȃ�����A�����Ȃ�̂����ˁB");

                            UpdateMainMessage("���i�F���������A����̂����������Ǝv����B�ˁA�����Ă�B");

                            UpdateMainMessage("�A�C���F��H���x�͂ǂ�Ȃ̂��H");

                            UpdateMainMessage("���i�F�ȑO�̐����@�́u�����v���R���Z�v�g���������ǁA����͈Ⴄ�́B");

                            UpdateMainMessage("���i�F�󖂖@�ł��낢��l�����̂�A�����ő勉�Ȃ̂����āB");

                            UpdateMainMessage("���i�F�A�C���A���Ԃ��Ăǂ�������H");

                            UpdateMainMessage("�A�C���F���ԁH������ƌ������́A���ʂɌo�߂��Ă������Ċ������ȁB");

                            UpdateMainMessage("���i�F���ѐH�ׂĂ鎞��A���K�m�Âɗ��ł鎞�́H");

                            UpdateMainMessage("�A�C���F���Ɉӎ��͂��Ă˂��B");

                            UpdateMainMessage("���i�F�Q��O�́H");

                            UpdateMainMessage("�A�C���F�����ԐQ�邩���ď����͍l�����悬��ȁB");

                            UpdateMainMessage("���i�F�ł����Ԃ͓���������Ă����B�ӎ��ł��邩�o���Ȃ����̍��Ȃ̂�B");

                            UpdateMainMessage("���i�F���̖��@�́A�A�C���̎��Ԃɑ΂���F�������킹�閂�@��B");

                            UpdateMainMessage("�A�C���F��̂ǂ��Ȃ�񂾁H");

                            UpdateMainMessage("���i�F���������Ă݂����B�A�C�����g���̌����Ă݂Ă��傤�����B�������ATimeStop�I");

                            UpdateMainMessage("���i�F�ǂ��������H�A�C���B");

                            UpdateMainMessage("�A�C���F�́H�������瑁����������āB");

                            UpdateMainMessage("���i�F�����A�I����Ă���A�A�C���̎����̃��C�t�l�����Ă݂Ȃ����B");

                            UpdateMainMessage("�A�C���F�b�o�I�E�E�E�o�J�ȁI�I�I�H");

                            UpdateMainMessage("�@�@�@�w�A�C���̃��C�t�͊��ɔ����ȉ��ɂ܂Ō����Ă����I�x");

                            UpdateMainMessage("�A�C���F�E�E�E���E�E�E����́E�E�E");

                            UpdateMainMessage("�A�C���F�_���[�W�n���@���ȁI�I�I");

                            UpdateMainMessage("���i�F�Ⴄ���A�_���[�W�n�͎��ۂɓ��Ă��͎̂��������ǂˁB");

                            UpdateMainMessage("�A�C���F���ۓ��Ă��񂾂�A���Ⴀ�_���[�W�n����Ȃ����B");

                            UpdateMainMessage("���i�F�����d�|����OboroImpact���A�A�C�����F�����Ă��Ȃ��������Ď���B");

                            UpdateMainMessage("���i�F������A���ۂ̓_���[�W�q�b�g���O�ŔF���o���Ă�͂���B");

                            UpdateMainMessage("�A�C���F�܂��m���Ƀ��i��TimeStop�ƌ���������ɑ̂ɏՌ��������Ă邩��ȁB");

                            UpdateMainMessage("�A�C���F���̖��@���̂��_���[�W�n���@�Ȃ񂶂�Ȃ����Ǝv�����܂����B");

                            UpdateMainMessage("���i�F�A�C���A�����̖��@�͐����댯���Ǝv����B");

                            UpdateMainMessage("�A�C���F�ǂ������Ӗ����H");

                            UpdateMainMessage("���i�F���Ԃ̔F�����ł��Ȃ��̂�B���̂͒P�Ȃ�_���[�W�n������");

                            UpdateMainMessage("���i�F�����A���G�ŘA�����̍����R���{����u�̊ԂŊ������Ă��܂����Ƃ�����");

                            UpdateMainMessage("���i�F�����炭�A������p�͂ǂ�ǂ񖳂��Ȃ��Ă�����B");

                            UpdateMainMessage("�A�C���F�R���{�Ȃ񂴁A���ɂƂ��Ă͂���قǕ|���͂˂��B�U�����@�͕K������͂����B");

                            UpdateMainMessage("�A�C���F���i�A���̖��@�ǂ�ǂ�g���Ă��ėǂ����B��΂Ɏ�_�͂���͂�������ȁB");

                            UpdateMainMessage("���i�F�����ˁA�A�C���Ȃ牽�ł��ł��j�肻�������ADUEL�ł͂ǂ�ǂ�g������");

                            md.Message = "�����i�̓^�C���X�g�b�v���K�����܂�����";
                            md.ShowDialog();
                            sc.TimeStop = true;
                        }
                        if (sc.Level >= 40 && !sc.NothingOfNothingness)
                        {
                            UpdateMainMessage("���i�F������E�E�E���Ɏ����ō����x����B");

                            UpdateMainMessage("�A�C���F���������˂����A���i�I�@����ς��O�͂�������B");

                            UpdateMainMessage("���i�F�A�C���Ƃ����Ƃ���Ă��Ă郏�P�����A���̂��炢�͂��Ȃ��Ȃ��Ƃˁ�");

                            UpdateMainMessage("���i�F����ŁA���񎄂��K��������e�͎��͈�ԍŏ��̍����炠��\�z�Ȃ́B");

                            UpdateMainMessage("�A�C���F��ԍŏ�����H");

                            UpdateMainMessage("���i�F�����������Ȃ����̂��Đ����厖��ˁB");

                            UpdateMainMessage("�A�C���F�܂��A�������ȁB");

                            UpdateMainMessage("���i�F�ł��K�������Ȃ��Ă����́A�܂�ōŏ����疳���������̂悤�B");

                            UpdateMainMessage("�A�C���F�E�E�E�܂��E�E�E�����Ȃ̂����m��˂��ȁB");

                            UpdateMainMessage("���i�F���������Ƃ��Ă��������Ȃ��悤�ɂ���Ηǂ��Ǝv��Ȃ��H");

                            UpdateMainMessage("���i�F�����I�ɖ��������Ǝ��݂Ă��A��΂ɖ����Ȃ�͂��Ȃ��́B");

                            UpdateMainMessage("�A�C���F�ǂ�Ȃ̂ɁE�E�E�������Ȃ񂾂�E�E�E�H");

                            UpdateMainMessage("���i�F���W�����āA���������Ă݂��E�E�E");

                            UpdateMainMessage("���i�F�E�E�E�@�E�E�E�@�E�E�E�@");

                            UpdateMainMessage("���i�F�E�E�E�@�E�E�E");

                            UpdateMainMessage("���i�F�E�E�E�@������A������B�@������NothingOfNothingness��B");

                            UpdateMainMessage("�@�@�@�w���i�̎��͂ɖ��`�t�B�[���h���F�Ƃ�ǂ�Ɋg�������I�x");

                            UpdateMainMessage("�A�C���F�ȁE�E�E�@�E�E�E");

                            UpdateMainMessage("���i�F������B");

                            UpdateMainMessage("�A�C���F������Ƒ҂Ă�B���������Ȃ񂾁B");

                            UpdateMainMessage("���i�F���̃X�L���͑��肪���������Ă���s���E�X�L���E���@��S�Ė����ɂ���́B");

                            UpdateMainMessage("���i�F���ꂩ�玄������\�͂t�o�n�̂��̂͑S�ĉi���I�Ȍ��ʂ���ΓI�Ȃ��̂ƂȂ��B");

                            UpdateMainMessage("���i�F�����J��o���X�L���́A�S�Ĕ����̎ז��͂���Ȃ��Ȃ��B�������΂�B");

                            UpdateMainMessage("���i�F�����r�����閂�@�́A�S�Ĕ������荞�݁E�ł������͒ʗp���Ȃ��Ȃ��B��΂ɂˁB");

                            UpdateMainMessage("�A�C���F���S�ɁE�E�E�t���ȁB���܂łƁB");

                            UpdateMainMessage("�A�C���F�ǂ��������[�g�Ŏv�������񂾁H");

                            UpdateMainMessage("���i�F����Ȃ̔閧�Ɍ��܂��Ă邶��Ȃ��B");

                            UpdateMainMessage("�A�C���F�������A�����ɗ��Ĕ閧����B�܂����[�g�͂��̍ۗǂ��Ƃ��Ă����B");

                            UpdateMainMessage("�A�C���F���O�̂��̃X�L���A����ʂ͂ǂ̂��炢�Ȃ񂾁H");

                            UpdateMainMessage("���i�F�L�b�`���P�O�O��B��_�͂��̏���ʂ��炢�ˁB");

                            UpdateMainMessage("�A�C���F���i�A���O�{���ɁE�E�E��������A����ς�B");

                            UpdateMainMessage("���i�F������ȃ}�W��ɂȂ��Ă�̂�A�b�t�t�B�A�C���ɂ͂���܂�֌W�Ȃ������ˁ�");

                            UpdateMainMessage("���i�F���A���̃X�L������g���ĕK���c�t�d�k���ŗD�����Ă݂����B");

                            UpdateMainMessage("�A�C���F�b�n�n�A�����Ă���邶��˂����B�c�t�d�k���̓��C�o�����Ђ��߂������ꏊ���B");

                            UpdateMainMessage("�A�C���F���i�A���O����������ȁA�ł��邩������˂����B");

                            UpdateMainMessage("���i�F���肪�ƁA�����ĂƂ��̃X�L���łǂ̒��x�܂œK�p�����̂�������Ă݂�Ƃ����B");

                            UpdateMainMessage("���i�F�A�C���A������ƕt�������Ȃ�����");

                            UpdateMainMessage("�A�C���F�����A�ǂ����I");

                            md.Message = "�����i�̓i�b�V���O�E�I�u�E�i�b�V���O�l�X���K�����܂�����";
                            md.ShowDialog();
                            sc.NothingOfNothingness = true;
                        }
                    }
                }
                #endregion
                #region "���F���[�̃��x���A�b�v"
                if (tc != null)
                {
                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.Manual;
                        md.Location = new Point(this.Location.X, this.Location.Y + this.Size.Height);
                        md.NeedAnimation = true;

                        if (tc.Level >= 3 && !tc.FireBall)
                        {
                            UpdateMainMessage("���F���[�F���܂����A���x�����オ��܂�����B");

                            UpdateMainMessage("�A�C���F���F���[�A�ŏ��͂ǂ��������̂�M�����肾�H");

                            UpdateMainMessage("���i�F���F���[������āA�����͉��ɂȂ�́H");

                            UpdateMainMessage("���F���[�F�n�n�n�A�����؂�ɂ��܂����ˁB�ЂƂ������܂���B");

                            UpdateMainMessage("���F���[�F�܂��A�ŏ��͉��ƌ����Ă��_���[�W���ɂȂ�Α����ł��ˁB");

                            UpdateMainMessage("�A�C���F�����A���Ɠ�������Ȃ����B�C�������ȁB");

                            UpdateMainMessage("���F���[�F�����A�A�C������Ɠ����΂ł��B�ł������ɂ��Ăł����B");

                            UpdateMainMessage("���F���[�F�{�N�̏ꍇ�A���ʂȑ����͖�����ł���B");

                            UpdateMainMessage("���i�F�����E�E�E�H");

                            UpdateMainMessage("���F���[�F�����ł��B�S�����A�s�����ł��B�J�[���݂قǂł͂���܂��񂯂ǂˁB");

                            UpdateMainMessage("�A�C���F�}�W����E�E�E����ϓ`����FiveSeeker�̓_�e����˂��ȁE�E�E");

                            UpdateMainMessage("���F���[�F�n�n�A�A�C���N�B�������Ԃ�ł��A���r���[�Ȃ����ł���B");

                            UpdateMainMessage("���F���[�F����ɂ��Ă��v���Ԃ�ł��B������Ƃ���Ă݂܂��傤�E�E�E�n�C�B");

                            UpdateMainMessage("���i�F���A�y���o������BFireball��ˍ��́B�قƂ�Ǎ\���ĂȂ��Ȃ��H�H");

                            UpdateMainMessage("���F���[�F�o�����̓R�c������܂��B�\�������̃e�N�͍��x�����܂���B");

                            UpdateMainMessage("���i�F���A�z���g�ł����H���񐥔��");

                            md.Message = "�����F���[�̓t�@�C�A�E�{�[�����K�����܂�����";
                            md.ShowDialog();
                            using (MessageDisplay md2 = new MessageDisplay())
                            {
                                md2.StartPosition = FormStartPosition.Manual;
                                md2.Location = new Point(this.Location.X, this.Location.Y + this.Size.Height);
                                md2.NeedAnimation = true;
                                md2.Message = "�����F���[�̖��@�g�p���������܂�����";
                                md2.ShowDialog();
                            }
                            tc.AvailableMana = true;
                            tc.FireBall = true;
                        }
                        if (tc.Level >= 4 && !tc.DoubleSlash)
                        {
                            UpdateMainMessage("���F���[�F���Ă��āA���x���A�b�v�ł��ˁB");

                            UpdateMainMessage("���i�F�`����FiveSeeker����̃X�L���͂ǂ�Ȃ̂���������ł����H");

                            UpdateMainMessage("���F���[�FCarnageRush�Ƃ����̂�����܂�����B");

                            UpdateMainMessage("�A�C���F������Ăǂ�Ȃ̂��H");

                            UpdateMainMessage("���F���[�F�T��ł��B");

                            UpdateMainMessage("�A�C���F�E�E�E�́H�H");

                            UpdateMainMessage("���F���[�F���̃{�N�͂܂��v���o���܂��񂪁A�����ȂT��U���̎��ł��B");

                            UpdateMainMessage("���i�F�W���[�_���ł���E�E�E");

                            UpdateMainMessage("���F���[�F�����̍\���A�Ɠ��̃X�e�b�v�B�����ĕ���̐U���łT�񓖂Ă��ł��B");

                            UpdateMainMessage("���F���[�F������Ɛ̂̎��Ȃ�ł����܂���B���͂��̂��炢���ǂ����n�r���ł��B");

                            UpdateMainMessage("�@�@�@�w�b�b�u�u���I�I�I�x�@�@");

                            UpdateMainMessage("�A�C���F�����I�܂������̃_�u���E�X���b�V�����I�H�͂����I�I�I");

                            UpdateMainMessage("���F���[�F�͂��A������������ł��ˁB�̂��琏���P���������̂ł��B");

                            UpdateMainMessage("�A�C���F���A���x���B���ɍ��̃X�s�[�h�ł̂����������Ă���Ȃ����H");

                            UpdateMainMessage("���F���[�F�͂��A���ŁB");

                            md.Message = "�����F���[�̓_�u���E�X���b�V�����K�����܂�����";
                            md.ShowDialog();
                            using (MessageDisplay md2 = new MessageDisplay())
                            {
                                md2.StartPosition = FormStartPosition.Manual;
                                md2.Location = new Point(this.Location.X, this.Location.Y + this.Size.Height);
                                md2.NeedAnimation = true;
                                md2.Message = "�����F���[�̃X�L���g�p���������܂�����";
                                md2.ShowDialog();
                            }
                            tc.AvailableSkill = true;
                            tc.DoubleSlash = true;
                        }
                        if (tc.Level >= 5 && !tc.DarkBlast)
                        {
                            UpdateMainMessage("���F���[�F���x���E�E�E�A�b�v�ł��ˁB���ɂ��E�E�E");

                            UpdateMainMessage("���i�F�ǁA�ǂ�������ł����H");

                            UpdateMainMessage("���F���[�F����A���ł��Ȃ��ł��B������ƈő����̓N�Z������܂��ĂˁB");

                            UpdateMainMessage("�A�C���F�����A�ӊO�Ɠ���s���肪����񂾂ȁB");

                            UpdateMainMessage("���F���[�F���A�������E�E�E�ŏ��͊ȒP�ȃ��c�Ō����炵�E�E�E�ł��E�E�E");

                            UpdateMainMessage("���F���[�F�b�N�E�E�E�N�N�ADarkBlast�I�I�I");

                            UpdateMainMessage("�A�C���F���F�A���F���[���v�Ȃ̂��H���O�B");

                            UpdateMainMessage("���F���[�F�n�@�n�@�E�E�E���A���v�ł��B�S�z���Ȃ��ŁB");

                            UpdateMainMessage("���i�F����ǂ�������x��ł��������ˁB");

                            UpdateMainMessage("���F���[�F�����A���肪�Ƃ��������܂��B�������v�ł���B");

                            md.Message = "�����F���[�̓_�[�N�E�u���X�g���K�����܂�����";
                            md.ShowDialog();
                            tc.DarkBlast = true;
                        }
                        if (tc.Level >= 6 && !tc.CounterAttack)
                        {
                            UpdateMainMessage("���F���[�F���x���A�b�v���邽�тɁA�v���o���B�ǂ������\���Ȃ�ł��傤�ˁB");

                            UpdateMainMessage("�A�C���F���F���[�A�m���Ă邩�H���܂ɑM���Ȃ��񂾂��B");

                            UpdateMainMessage("���i�F����̓o�J�A�C�������̓����ł���B���F���[����Ɍ����Ė������B");

                            UpdateMainMessage("���F���[�F����A�{�N��������P��x�݂͂���Ǝv���܂��B�ł�����͍s���܂��B");

                            UpdateMainMessage("���F���[�F�̏p�͍D�ݎ���ł����A��͂�CounterAttack������ł��傤�B");

                            UpdateMainMessage("���i�F����ȋZ�Ȃ�ł����H");

                            UpdateMainMessage("���F���[�F�����A���肪�ǂ��������s�������Ă��邩������Ȃ��ꍇ�B");

                            UpdateMainMessage("���F���[�F�܂��́A���肪�����U�����������ǂ��������ɂ߂�ꍇ�B");

                            UpdateMainMessage("���F���[�F�X�ɁA�������������Ƀ}�C�i�X�ɂȂ鎖�͈������܂��񂵂ˁB");

                            UpdateMainMessage("�A�C���F���肪�\�͂t�o�n�������瑹����˂��̂��H");

                            UpdateMainMessage("���F���[�F�ŏ��̂P�񂮂炢�͍����グ�܂���B�ꌂ���̃��X�N������ŗD��ł��B");

                            UpdateMainMessage("�A�C���F�������E�E�E�����Ă݂�΂��������ȁB");

                            UpdateMainMessage("���i�F���������΁A�K���c�f�����������Ȏ������Ă���B");

                            UpdateMainMessage("�A�C���F���\�[���ȁA�Q�l�ɂȂ邺�B�T���L���[�B");

                            md.Message = "�����F���[�̓J�E���^�[�E�A�^�b�N���K�����܂�����";
                            md.ShowDialog();
                            tc.CounterAttack = true;
                        }
                        if (tc.Level >= 7 && !tc.FreshHeal)
                        {
                            UpdateMainMessage("���F���[�F���āA����̃��x���A�b�v�͊��ɃC���[�W������܂��B");

                            UpdateMainMessage("�A�C���F�ǂ�ɂ���񂾁H�܂���FreshHeal�Ƃ����H");

                            UpdateMainMessage("���F���[�F�͂��A���̂܂����ł���B�A�C���N�B");

                            UpdateMainMessage("�A�C���F�}�W����I�H�@��Γ�����Ǝv���ĂȂ����猾�����̂ɁB");

                            UpdateMainMessage("���i�F���ƈł̗����Ȃ�ĉ\�Ȃ�ł����H");

                            UpdateMainMessage("���F���[�F�J�[���݂Ɉ�x�����Ɨǂ��ł��B�{�����������Ƃ����������K�v�ł��B");

                            UpdateMainMessage("�A�C���F�{���������H�@�o�J�ȁB�܂������̋t����˂����B");

                            UpdateMainMessage("���F���[�F�����A�^�t�ł�����A�̂ɓ����{���������Ă����ł��B");

                            UpdateMainMessage("���i�F���ƂȂ������ǁA������C�������ˁB");

                            UpdateMainMessage("�A�C���F���A�������ƂȂ������A�����邺�B�b�n�b�n�b�n�I");

                            UpdateMainMessage("���i�F���Ⴀ�����炢�A�����Č��Ă�A�A�C���B");

                            UpdateMainMessage("�A�C���F���΂Ȃ񂾁B�����瓯���Ȃ񂾂�I�b�n�b�n�b�n�I");

                            UpdateMainMessage("���i�F�E�E�E���̔n���ɗ����͐�΂ɖ��������ˁB");

                            UpdateMainMessage("���F���[�F�A�C���N�͂��̂܂܂ŗǂ��Ǝv���܂����ǂˁA�C�������΂��ł��ǂ����B");

                            UpdateMainMessage("�A�C���F�b�n�b�n�b�n�I");

                            md.Message = "�����F���[�̓t���b�V���E�q�[���K�����܂�����";
                            md.ShowDialog();
                            tc.FreshHeal = true;
                        }
                        if (tc.Level >= 8 && !tc.StanceOfFlow)
                        {
                            UpdateMainMessage("���F���[�F���x���A�b�v�ł��B�ǂ�ǂ�s���܂��傤�B");

                            UpdateMainMessage("���i�F�ł����F���[����A�̏p���������T�}�ɂȂ��Ă܂���ˁB");

                            UpdateMainMessage("���F���[�F�n�n�A�A�C���N�ɂ������܂������A�������Ԃ�ł��B");

                            UpdateMainMessage("�A�C���F���F���[���Ė��Ƀx�[�X�̓�����������ȁB");

                            UpdateMainMessage("���F���[�F�\����N�Z��t���Ă����ƁA�g�����Ƃ������o�h�𑊎�Ɉ�ەt�����܂��B");

                            UpdateMainMessage("���F���[�F���A������StanceOfFlow�ō���͍s���܂��傤�B");

                            UpdateMainMessage("�A�C���F�o���I���̃��U�ƌ��ɂȂ��I�I���ɂ͗����ł��˂����B");

                            UpdateMainMessage("���i�F�������Ă�̂�B�K���c�f�����񒼓`�Ŏ������Ȃ�ǂ��Ȃ����񂾂���B");

                            UpdateMainMessage("���F���[�F���i����A������ƌ����Ă��������B�{�N���f�f���Ă݂܂��B");

                            UpdateMainMessage("���i�F�悵�A���Ⴀ�s�����E�E�E�E�E�E");

                            UpdateMainMessage("���F���[�F�E�E�E���āA�Ȃ��Ȃ����ł���B���������o�����X���ǂ��A�������ł��ˁB");

                            UpdateMainMessage("���i�F�z���z���A�ǂ����o�J�A�C���A���Ȃ��ɂ͈ꐶ������ˁ�");

                            UpdateMainMessage("�A�C���F�n�i�����狻�����˂��񂾂�B�m�邩�����́B");

                            UpdateMainMessage("���F���[�F���āA������Ɨ���ĂĂ��������B���̓{�N���\���Ă݂܂�����B");

                            UpdateMainMessage("���F���[�F�A�C���N�A�ł͍s���܂���E�E�E");

                            UpdateMainMessage("�@�@�@�w���F���[���ɂ܂ꂽ�u�ԁA�A�C���͈ٗl�ȗ�⊾���������x�@�@");

                            UpdateMainMessage("�A�C���F�E�E�E�������B�S���ǂ܂�Ă�݂����ŏ��Ă�C�����Ȃ��������A���B");

                            UpdateMainMessage("���i�F���̂Ƃ́A���ƂȂ����͋C���Ⴄ��ˁB");

                            UpdateMainMessage("���F���[�F���K���Ƃ͑���̎���𕕎E���鎖�ɂ���܂��B");

                            UpdateMainMessage("�A�C���F�����E�E�E�������K���Ă݂邩�ȁE�E�E���i�A���K���B���߂Ă������I�H");

                            UpdateMainMessage("���i�F�C������������A���ł���Ă��傤�����E�E�E");

                            md.Message = "�����F���[�̓X�^���X�E�I�u�E�t���[���K�����܂�����";
                            md.ShowDialog();
                            tc.StanceOfFlow = true;
                        }
                        if (tc.Level >= 9 && !tc.StanceOfStanding)
                        {
                            UpdateMainMessage("���F���[�F���x���A�b�v���Ɏv���o�������ł��B���������ł��ˁA�{���ɁB");

                            UpdateMainMessage("�A�C���F���F���[�͖��@�E�X�L���ǂ��������ӂȂ񂾁H");

                            UpdateMainMessage("���F���[�F���ӁE�E�E���ӂł����H�����Č����Η����ł��B");

                            UpdateMainMessage("�A�C���F���ƂȂ�����ȋC���������E�E�E");

                            UpdateMainMessage("���i�F���F���[����A����͂ǂ������v���o������ł����H");

                            UpdateMainMessage("�A�C���F�Q�A���̏p�������肵�ĂȁB�b�n�b�n�b�n�I");

                            UpdateMainMessage("���F���[�F�A�C���N�A���\�J�����ǂ��ł��ˁBStanceOfStanding�œ�����ł��B");

                            UpdateMainMessage("�A�C���FStanceOfFlow�Ɨ����g����̂��B�{���ɉ��ł��A���Ȃ񂾂ȁB");

                            UpdateMainMessage("���F���[�F���̍\���A�{�N�̂��C�ɓ���ł��A���ƌ����Ă��h�䌓�U���ł�����ˁB");

                            UpdateMainMessage("���F���[�F�ł́A����Ă݂܂��傤�B�A�C���N�A�\���āB");

                            UpdateMainMessage("�A�C���F�����A���ł������B��{�I�ɂ͕��ʍU��������ȁB");

                            UpdateMainMessage("���F���[�F�����ƁA�m������Ȋ����ł����ˁB�b�n�C�I�I�I");

                            UpdateMainMessage("�@�@�@�w�b�h�X�E�E�D�D�D���I�I�I�x�@�@");

                            UpdateMainMessage("���i�F���E�E�E�������A�����������Ȃ������H");

                            UpdateMainMessage("�A�C���F�b�Q�E�E�E�K�n�A�@�I�I�b�Q�z�b�Q�z�I�񂾍��́E�E�E�b�O�b�K�n�@�I�I");

                            UpdateMainMessage("���F���[�F�I�I�H�@���܂����I�I�I�@�����܂���A�{�N�Ƃ������Ƃ��I�I�I");

                            UpdateMainMessage("�A�C���F���́E�E�E���Ȃ񂾁H�̒��ɁE�E�E�Ռ���");

                            UpdateMainMessage("���F���[�F���ł�������ł��A�Y��Ă��������B�{���ɂ����܂���ł����B");

                            UpdateMainMessage("���i�F�A�C���A���v�H�炪�^���ˁA�������肵�Ȃ������");

                            UpdateMainMessage("���F���[�FCatastrophe�Ƃ������ɉ��`�ł��BStandOfStanding�Ɏ��Ă�̂ŁA���B");

                            UpdateMainMessage("�A�C���F�ӂ����v�݂������B�܂��AStandOfStanding�͒m���Ă邩��ȁB���܂������Ă���B");

                            UpdateMainMessage("���F���[�F�����A�{���Ɏ�����͋C�����܂��B");

                            md.Message = "�����F���[�̓X�^���X�E�I�u�E�X�^���f�B���O���K�����܂�����";
                            md.ShowDialog();
                            tc.StanceOfStanding = true;
                        }
                        if (tc.Level >= 10 && !tc.DispelMagic)
                        {
                            UpdateMainMessage("���F���[�F���āA���̂k�u�P�O�E�E�E�E�E�E����");

                            UpdateMainMessage("�A�C���F�����A���F���[��F���������B���v���A���O�H");

                            UpdateMainMessage("���F���[�F�����A�K�A�A�@�@�@�@�I�I�I�I�I");

                            UpdateMainMessage("���F���[�F�n�n�n�n�n�n�I�I�����Ė����Ȃ�I�I�I�@DispelMagic�I�I�I");

                            UpdateMainMessage("���i�F���ЁE�E�E�₾�E�E�E�ȁA�����ς���A�C���B");

                            UpdateMainMessage("�A�C���F�����I�I�C�I�I�I���F���[�������肵��I�I�I");

                            UpdateMainMessage("���F���[�F�A�A�@�@�@�@�E�E�E�n�@�n�@�E�E�E�A�A�A�C���N�ł����H");

                            UpdateMainMessage("�A�C���F�����A�����B���v���I�H");

                            UpdateMainMessage("���F���[�F�E�E�E�E�E�EDispelMagic�̓{�N�ɂƂ��ăg���E�}�݂����Ȗ��@�Ȃ�ł��B");

                            UpdateMainMessage("���i�F�₾�E�E�E����ǂ��Ȃ�v���o���Ȃ��ėǂ���B�������ďo���邩��B");

                            UpdateMainMessage("���F���[�F���i������A���܂藔�p���Ȃ������ǂ��ł��B���̖��@����p���傫���ł�����B");

                            UpdateMainMessage("�A�C���F�ǂ��������Ȃ񂾁H�����E�E�E���ƁA�~�߂Ă������B");

                            UpdateMainMessage("���F���[�F�����A���C�ɂ����ɁB�����������܂��傤���B");

                            UpdateMainMessage("���F���[�F���X����DispelMagic�Ƃ����̂͂��̐��ɂ͑��݂��Ȃ����@�ł��B");

                            UpdateMainMessage("�A�C���F���̐��ɁH���̐��Ȃ�݂���Ă̂��H");

                            UpdateMainMessage("���F���[�F�Ⴂ�܂��B���m�ɂ͂��̃_���W��������Ŏg���閂�@�Ƃ����Ӗ��ł��B");

                            UpdateMainMessage("���i�F���A�����������́H�S�R�m��Ȃ�������ˁB");

                            UpdateMainMessage("���F���[�FDispelMagic�Ƃ͏����ɂ��̃_���W�����֊肢��������悤�Ȃ��̂ł��B");

                            UpdateMainMessage("���F���[�F���̃_���W�����͒��킵�Ă���҂̐��_�֑傫�������Ă��܂��B");

                            UpdateMainMessage("�A�C���F�������E�E�E���ƂȂ������������B�m���Ɏ~�߂Ƃ��������ǂ������i�B");

                            UpdateMainMessage("���i�F���ł�H�_���W��������Ȃ�ł���H����������v�Ȃ񂶂�Ȃ��H");

                            UpdateMainMessage("�A�C���F������A�_���W�������肾�B�����炱�������B");

                            UpdateMainMessage("�A�C���F�������A�������_���W��������ŉ��ł��肢���������Ă݂�B");

                            UpdateMainMessage("�A�C���F���O�ꐶ���ʂ܂ŁA���̃_���W��������o�Ȃ��Ȃ����܂����B");

                            UpdateMainMessage("���i�F�������Ă�̂�B�ړI���B�������΂������ɃI�T���o�ł����");

                            UpdateMainMessage("���F���[�F�A�C���N�A�{�N���������܂��B���i����A�ǂ��ł����H");

                            UpdateMainMessage("���F���[�F�_���W�����͂���𗘗p���āA���i����̐l�i��傫�����킹��ꍇ������܂��B");

                            UpdateMainMessage("���F���[�F������DispelMagic�͋ɗ͎g��Ȃ��ł��������B�^���̕���厖�ɂ��Ă��������B");

                            UpdateMainMessage("���i�F�܂����F���[���񂪂���������Ȃ�A����������B����܂�g��Ȃ��悤�ɂ����ˁ�");

                            UpdateMainMessage("���F���[�F�����A���i����͓����ǂ��ł�����ˁB�����������ď�����܂��B");

                            UpdateMainMessage("���F���[�F���āA�ő��Ɏg��Ȃ��悤�Ƀ{�N�����ȋK�������������܂���B");

                            md.Message = "�����F���[�̓f�B�X�y���E�}�W�b�N���K�����܂�����";
                            md.ShowDialog();
                            tc.DispelMagic = true;
                        }
                        if (tc.Level >= 11 && !tc.WordOfPower)
                        {
                            UpdateMainMessage("���F���[�F�k�u�����ɂQ���ɏ���Ă��܂����B�{�N�Ƃ��Ă͊�����������ł��ˁB");

                            UpdateMainMessage("�A�C���F���F���[�A��ǂ����v���������B");

                            UpdateMainMessage("���F���[�F�͂��A�Ȃ�ł��傤�H");

                            UpdateMainMessage("�A�C���F�����������閂�@�Ɠ������̂����Ɍ����Ă݂Ă���B��ׂĂ݂����񂾁B");

                            UpdateMainMessage("���F���[�F���A�͂��A�ǂ��ł���B����Ă݂܂��傤���B");

                            UpdateMainMessage("���i�F�A�C��������A���̖��@�ɂ������Ȃ̂�H");

                            UpdateMainMessage("�A�C���F���@���́E�E�EWordOfPower���I�I���@�I�I");

                            UpdateMainMessage("���F���[�F���āA�A�C���N��WordOfPower�E�E�E�~�߂�܂����ˁBWordOfPower�ł��A�b�n�@�@�I");

                            UpdateMainMessage("�@�@�@�w�b�Y�Y�E�E���I�I�x�@�@");

                            UpdateMainMessage("���F���[�F���āE�E�E�قڌ݊p�ł��ˁB��������܂����B");

                            UpdateMainMessage("�A�C���F��k����B�U�����@�̒�����A���̈�ԓ��ӂȃ��c�����H");

                            UpdateMainMessage("���F���[�F�З͎��̂͂����炭�A�C���N�̕�����ł���B���S�z�Ȃ��B");

                            UpdateMainMessage("�A�C���F���ȁI�E�E�E����ǂ����Ă��I�H");

                            UpdateMainMessage("���F���[�F�������򐨂ȑ��Ɣ��f������A���̕t���˂Ǝ����������փX�i�b�v����������ɂ����ł��B");

                            UpdateMainMessage("���F���[�F�������鎖�ŁA����ւ̍U���͎キ�Ȃ�܂����A�Ռ��ɘa�v�f�������ł����ł���B");

                            UpdateMainMessage("���i�F���F���[������āA�悭���������̂���������������O�݂����ɂ��܂���ˁH");

                            UpdateMainMessage("���F���[�F�n�n�n�A�������ɔN�G�̍��Ƃ��������ł��傤�B�ł�������Ɣڋ��ł��ˁB");

                            UpdateMainMessage("���F���[�F�G���~��������A�����炭�������򐨂ł����ʂ���Ԃ��������I�т܂��B");

                            UpdateMainMessage("�A�C���F�������AWordOfPower�Ȃ�C�P��Ǝv�����񂾂��ȁB����˂����z���g�B");

                            UpdateMainMessage("���F���[�F���x�܂�����Ă݂܂��傤�B�{�N�����܂łɒb���Ă����܂���B");

                            md.Message = "�����F���[�̓��[�h�E�I�u�E�p���[���K�����܂�����";
                            md.ShowDialog();
                            tc.WordOfPower = true;
                        }
                        if (tc.Level >= 12 && !we.AlreadyLvUpEmpty31)
                        {
                            UpdateMainMessage("���F���[�F�������E�E�E���̃o�����X�B");

                            UpdateMainMessage("�A�C���F���F���[�A�ǂ������񂾁H�ςȈ�l���Ȃ񂩌����āB");

                            UpdateMainMessage("���F���[�F�A�C���N�A���Ă��������B���̃o�����X�B");

                            UpdateMainMessage("���F���[�F�l�t�̃N���[�o�[�B�K���̏ے��ł���Ȃ����A����ւ͋��ɂ̕s�K�̖K��B");

                            UpdateMainMessage("���F���[�F�Y��ɊJ�����t�̊Ԃ̋��ɓI�ȃo�����X�B�����K�����Ӗ��Â��������E�E�E�����߂��E�E�E");

                            UpdateMainMessage("���i�F�A�C���A�A�C���B������ĂЂ���Ƃ��āH");

                            UpdateMainMessage("�A�C���F�����A��̃A�����B�M���O���Ă���ȁE�E�E");

                            UpdateMainMessage("���F���[�F�A�C���N�����i����������v���܂��񂩁H���ꂱ�������A�����č߂��ƁB");

                            UpdateMainMessage("�A�C���ƃ��i�F���H���E�E�E�����A���A�����ł��ˁE�E�E");

                            UpdateMainMessage("���F���[�F���̋H�����E�E�E�Ȃ�č߂Ȃ񂾁E�E�E�����A�܂�Ń{�N�̂悤���B");

                            UpdateMainMessage("�A�C���F�����Ă������B�S�R��̋󂾂��ȁB");

                            UpdateMainMessage("���i�F���F���[����A���͑M���܂��悤�ɁB");

                            md.Message = "�����F���[�͉����K���ł��܂���ł�����";
                            md.ShowDialog();
                            we.AlreadyLvUpEmpty31 = true;
                        }
                        if (tc.Level >= 13 && !tc.EnigmaSence)
                        {
                            UpdateMainMessage("���F���[�F�k�u�P�R�E�E�E���������ł��ˁB");

                            UpdateMainMessage("���i�F���F���[������Ė{���͉��ł��o���Ă���񂶂�Ȃ���ł����H");

                            UpdateMainMessage("���F���[�F���ł��Ƃ͂ǂ������Ӗ��ł��傤�H");

                            UpdateMainMessage("���i�F�`����FiveSeeker�S��������̖��@�E�X�L���S���ł���B");

                            UpdateMainMessage("���F���[�F��������������������m��܂���ˁB�⑫���Ă����܂��傤�B");

                            UpdateMainMessage("�A�C���F����H");

                            UpdateMainMessage("���F���[�F���ۂɑ̂Ōo�����Ă������m�����X�Ǝv���o���Ă���銴���ł��ˁB");

                            UpdateMainMessage("���F���[�F���ł͖Y��Ă���B�ł��̂͊o���Ă���B����ȏ��ł��B");

                            UpdateMainMessage("���i�F����ς肻���Ȃ�ł��ˁB�������A�K�����鎞�̓��삪�ǂ������ŁB");

                            UpdateMainMessage("�A�C���F�m���Ƀ��F���[�̏K�����̓����̓n���p����˂����ȁB");

                            UpdateMainMessage("���F���[�F�����EnigmaSence�ł��ˁA���̃p���[���͂��܂��ɉ𖾂���Ă��܂��񂪁A");

                            int maxValue = Math.Max(tc.StandardStrength, Math.Max(tc.StandardAgility, tc.StandardIntelligence));
                            if (maxValue == tc.StandardStrength)
                            {
                                UpdateMainMessage("���F���[�F���̃{�N�ł͗́A" + maxValue.ToString() + "����ԍ����ł��ˁB");
                            }
                            else if (maxValue == tc.StandardAgility)
                            {
                                UpdateMainMessage("���F���[�F���̃{�N�ł͋Z�A" + maxValue.ToString() + "����ԍ����ł��ˁB");
                            }
                            else
                            {
                                UpdateMainMessage("���F���[�F���̃{�N�ł͒m�A" + maxValue.ToString() + "����ԍ����ł��ˁB");
                            }

                            UpdateMainMessage("���F���[�F�ł́A�s���܂���B�n�@�I");

                            UpdateMainMessage("�A�C���F���ς�炸������ȁE�E�E��u�o�b�N�X�e�b�v�������Ǝv������O�i���Ă�B");

                            UpdateMainMessage("���i�F���̃G�j�O�}�E�Z���X�Ƃ�����ƍ\�����Ⴄ��ˁB");

                            UpdateMainMessage("���F���[�F�{�N�ɂƂ��ẮA�o�b�N�X�e�b�v����ԗǂ������ł�����ˁB");

                            UpdateMainMessage("���F���[�F�����U���͂ɕs�������������͂�����g���čs���܂���B");

                            md.Message = "�����F���[�̓G�j�O�}�E�Z���X���K�����܂�����";
                            md.ShowDialog();
                            tc.EnigmaSence = true;
                        }
                        if (tc.Level >= 14 && !tc.BlackContract)
                        {
                            UpdateMainMessage("���F���[�F�k�u�P�S�E�E�E���낻�낱�̕ӂŁE�E�E�B");

                            UpdateMainMessage("�A�C���F���F���[�A���̕��͋C�E�E�E�Ŗ��@���H");

                            UpdateMainMessage("���F���[�F�S�z���Ȃ��ł��������B�����������m�Ȃ�ł���{�N�̏ꍇ�́E�E�E�B");

                            UpdateMainMessage("���F���[�F�����_��ł��A�{�N�ɂƂ��Ă͂���Ӗ������ł�����ˁB");

                            UpdateMainMessage("���F���[�F�����A�b�n�n�n�A�ǂ������ł��BBlackContract�I");

                            UpdateMainMessage("���i�F���F���[����E�E�E���͋C���ς��܂���ˁB");

                            UpdateMainMessage("���F���[�F��قǂ������܂������A�S�z���p�ł��B�����������͋C�̕����o���₷����ł��B");

                            UpdateMainMessage("���F���[�F�m���A���@�ƃX�L���̃R�X�g���O�ł����ˁB��������Ă݂܂����B");

                            UpdateMainMessage("�A�C���F�����ƁA����������ł�邺�B");

                            UpdateMainMessage("���F���[�F�����A�󂯂Ă��������B�_�u���E�X���b�V���I");

                            UpdateMainMessage("�A�C���F�����ƁA�b�O�I�@�C�c�c�E�E�E");

                            UpdateMainMessage("���F���[�F�@���ƁA�@�@�����ŁA�@�@�_�u���E�X���b�V���I");

                            UpdateMainMessage("�A�C���F�����I�H�@�܂�����A���Ă����������E�E�E");

                            UpdateMainMessage("���F���[�F�܂��s���܂��A�_�u���E�X���b�V���I");

                            UpdateMainMessage("�A�C���F�E�O�b�I�@�O�A�A�@�I");

                            UpdateMainMessage("���i�F�������́E�E�E�_�u���E�X���b�V�����R�A���Ȃ�茵������ˁB");

                            UpdateMainMessage("���F���[�F�R�X�g���C�ɂ��Ȃ��ėǂ��ł�����ˁB�����݂�����ɂ͍œK�ł��傤�B");

                            UpdateMainMessage("�A�C���F�������I����ɂ������āA�S�R�X�L���˂��B");

                            UpdateMainMessage("���F���[�F���i��������̖��@�������Ă܂��ˁA�����Ȃ�̃L���ł������Ă����Ɨǂ��ł���B");

                            UpdateMainMessage("���i�F�����A��_�W�����ǂ������ˁB�l���Ă�����B");

                            md.Message = "�����F���[�̓u���b�N�E�R���g���N�g���K�����܂�����";
                            md.ShowDialog();
                            tc.BlackContract = true;
                        }
                        if (tc.Level >= 15 && !tc.Cleansing)
                        {
                            UpdateMainMessage("���F���[�F�{�N���悤�₭�k�u�P�T�ł��ˁB");

                            UpdateMainMessage("�A�C���F���F���[�͍U���E�񕜁E����n�ƃo�����X���ǂ���ȁB");

                            UpdateMainMessage("���F���[�F�ǂ��g�R���Ɍ����܂����H����ł��A���\�s���R�Ȃ��̂ł���B");

                            UpdateMainMessage("���i�F�����n���Ђ���Ƃ��ďo����������肷���ł����H");

                            UpdateMainMessage("���F���[�F�n�n�n�A���i����Ɉ�{����Ă��܂��܂����ˁB");

                            UpdateMainMessage("���F���[�F���񂱂�Cleansing���v���o���Ă������ł���B");

                            UpdateMainMessage("���i�F�ǂ�������B����Ŏ������q�������ł����F���[���񂪋���΂n�j�ˁ�");

                            UpdateMainMessage("�A�C���F�����A���͐����@�Ȃ�ďo���˂�����ȁB�����܂����ȁA�����B");

                            UpdateMainMessage("���F���[�F�����A�A�C���N�͍U�ߑ���������ǂ��Ǝv���܂���B");

                            UpdateMainMessage("���F���[�F�R�l�Ƃ����h�q�I�Ȗ��@�΂���ł́A�퓬�̎����o���܂��񂩂�ˁB");

                            UpdateMainMessage("�A�C���F�����A�܂��������q�������́A�X�}��������ɂ��Ă邺�I");

                            UpdateMainMessage("���F���[�F�����A�C���Ă����Ă��������B");

                            md.Message = "�����F���[�̓N���[���W���O���K�����܂�����";
                            md.ShowDialog();
                            tc.Cleansing = true;
                        }
                        if (tc.Level >= 16 && !tc.GaleWind)
                        {
                            UpdateMainMessage("���F���[�F�P�U�Ƃ����������Ȃ��Ȃ��ǂ��Ǝv���܂��񂩁H");

                            UpdateMainMessage("�A�C���F�ǂ��ƌ����ƁA�ǂ������Ӗ����H");

                            UpdateMainMessage("���F���[�F�n�n�n�A�܂��l�I�ȉ��l�ςł��B�C�ɂ��Ȃ��ł��������B");

                            UpdateMainMessage("���F���[�F���āA�����͈�{�N�̊�{���̊�{���K������Ƃ��܂��傤�B");

                            UpdateMainMessage("���F���[�F�{�N���S��������́A�悭���������Ă��܂����ˁBGaleWind�ł��B");

                            UpdateMainMessage("���i�F�킠�E�E�E�قƂ�ǈ�u�ŏo����E�E�E������ˁB");

                            UpdateMainMessage("�y���F���[�F���[�h�E�I�u�E�p���[�I�z�@�@�@�@�y�H�H�H�F���[�h�E�I�u�E�p���[�I�z");

                            UpdateMainMessage("�A�C���F�Ȃ�قǁA�͉����łȂ����A�h��s�̃X�y�����Q�񂩁B");

                            UpdateMainMessage("���F���[�F���̂͂ق�̈�U�ɉ߂��܂��񂪁A���̃X�y���͍ŋ��̕��ނɓ���Ǝv���܂��B");

                            UpdateMainMessage("�A�C���F�������H�m���ɋ��͂��������A�����̂Q��s���݂����Ȃ��񂾂���ȁB");

                            UpdateMainMessage("���F���[�F������ƁA���̃A�C���N�ł͖�����������܂��񂪁B");

                            UpdateMainMessage("�A�C���F��H");

                            UpdateMainMessage("���F���[�F�܂��햾�����͏o���܂��񂪁A���ĂĂ��������B");

                            UpdateMainMessage("�@�@�@�w���F���[���ӂƊԂ�u�������Ǝv�킹���E�E�E���̏u�ԁI�x");

                            UpdateMainMessage("�y���F���[�F�_�u���E�X���b�V���I�z�@�@�@�@�y�H�H�H�F���[�h�E�I�u�E�p���[�I�z");

                            UpdateMainMessage("�A�C���F���ȁI�I�I�@�����ƁI�I�H");

                            UpdateMainMessage("���i�F�Ⴄ���[�V������������B�@�A�C�����ɂ߂�ꂽ�H");

                            UpdateMainMessage("�A�C���F���΁E�E�E�o�J�ȁI�I�@���肦�˂����덡�̂́I�H");

                            UpdateMainMessage("���F���[�F�r���̃^�C�~���O�Ə������[�V�����Ɋւ���e�N�j�b�N�A�g�ł��B");

                            UpdateMainMessage("�A�C���F�ǂ�������񂾂�B����Ȃ̂��\�Ȃ̂��I�H");

                            UpdateMainMessage("���F���[�F������ƍ��x�ȋZ�p��v���邩������܂��񂪁A�\�ł��B");

                            UpdateMainMessage("�A�C���F�����E�E�E����Ȃ̌�����ꂿ��ق��Ă��˂��B�Ӓn�ł��H�炢���Ă�邺�B");

                            UpdateMainMessage("���F���[�F�܂����������͌����Ă��������B���ł����������܂��B");

                            md.Message = "�����F���[�̓Q�C���E�E�B���h���K�����܂�����";
                            md.ShowDialog();
                            tc.GaleWind = true;
                        }
                        if (tc.Level >= 17 && !tc.FrozenLance)
                        {
                            UpdateMainMessage("���F���[�F�k�u�P�V�ł��ˁB���āA����͂ǂ�ł����܂��傤���B");

                            UpdateMainMessage("���i�F���F���[����A�����@���g�����ł���ˁH");

                            UpdateMainMessage("���F���[�F�����A�s���܂���B�����ł��ˁA����͐����@�̎�͂�����Ă݂܂��傤�B");

                            UpdateMainMessage("���F���[�F�m�����̖�ł����ˁB�b�n�C�B");

                            UpdateMainMessage("�@�@�@�w�b�s�L�L�I�V���A�A�@�@�[�[�[�I�I�x�@�@");

                            UpdateMainMessage("���i�F���킠���E�E�E�����Y��ȗ�����ł����ˁB");

                            UpdateMainMessage("���F���[�F�����܂���A�ǂ����^�������ɔ�΂��ĂȂ��悤�ł��ˁB");

                            UpdateMainMessage("�A�C���F��������Ăނ���ǂ�����Ĕ�΂��񂾁H");

                            UpdateMainMessage("���F���[�F������ɔ�΂��ۂ́A���˂��o�����ɁA�I�̕�����������]�������ł���B");

                            UpdateMainMessage("�A�C���F�Ȃ�قǁA�������Ɨ�����ɂȂ�̂��E�E�E��������A��������Ă݂����I");

                            UpdateMainMessage("�A�C���F�������Ƃ��ȁE�E�E������I�t�@�C�A�E�{�[���I");

                            UpdateMainMessage("���i�F���킠���E�E�E�����Y��Ȓ�����������ˁE�E�E�A�C���B");

                            UpdateMainMessage("�A�C���F�b�n�b�n�b�n�I�E�E�E���������I");

                            UpdateMainMessage("���F���[�F������͓��ɈЗ͂��オ������͂��܂���A���������؂Ɍ����邭�炢�ł��B");

                            UpdateMainMessage("�A�C���F�����������ړI�Ȉ�ۂ��厖������ȁB");

                            UpdateMainMessage("���i�F�����ƁE�E�E����H");

                            UpdateMainMessage("���F���[�F�@�@�H�@�@�@���ł��傤�H");

                            UpdateMainMessage("���i�F�����ƁA���F���[����A�^��������΂����ł����H");

                            UpdateMainMessage("���F���[�F�����A�^�������͂����ł��ˁB�b�n�C�B");

                            UpdateMainMessage("���i�F�E�E�E���`��A�����낤�B");

                            UpdateMainMessage("���F���[�F�ǂ������܂������H");

                            UpdateMainMessage("���i�F�A�C���A�������t�@�C�A�E�{�[�������Ă݂āB");

                            UpdateMainMessage("�A�C���F��H�����C���Ă����E�E�E�t�@�C�A�E�{�[���I");

                            UpdateMainMessage("���i�F�E�E�E����ς�E�E�E");

                            UpdateMainMessage("�A�C���F������H���������Ԃ�ȁB");

                            UpdateMainMessage("���i�F�������������ǁA�\�����A�������[�V�����A��ѕ��A���x�A�����������S�R�Ⴄ��ˁB");

                            UpdateMainMessage("�A�C���F����Ⴀ�A��������B�������X�H");

                            UpdateMainMessage("���i�F���A������A���ł��Ȃ���B");

                            UpdateMainMessage("���F���[�F�l�ɂ���Čl���͂���܂��B��������ɋC�t������ł��傤�B");

                            UpdateMainMessage("���F���[�F���i����ƃ{�N�̑ł������Ⴂ�܂�����ˁA���݂����K�ɗ�݂܂��傤�B");

                            UpdateMainMessage("���i�F���A���񂤂�B������B�l���ˁA�����Ă���Ă��肪�Ɓ�");

                            md.Message = "�����F���[�̓t���[�Y���E�����X���K�����܂�����";
                            md.ShowDialog();
                            tc.FrozenLance = true;
                        }
                        if (tc.Level >= 18 && !tc.InnerInspiration)
                        {
                            UpdateMainMessage("���F���[�F�k�u�P�W�ɂȂ�܂����ˁB");

                            UpdateMainMessage("�A�C���F���F���[�̑S����������Ă��B�����f�B�̃{�P���ꏏ�ɋ����񂾂�H");

                            UpdateMainMessage("���F���[�F�{�P�Ƃ͉ߌ��ȕ\���ł��ˁA�n�n�n�B�m���Ƀ{�P���Ƃ��������܂������B");

                            UpdateMainMessage("���F���[�F�ł��X�^�C����퓬�Z�p�͂ǂ���Ƃ��Ă��X�g���[�g�Ȃ��̂΂���ł�����B");

                            UpdateMainMessage("���F���[�F�v�������܂����B�퓬���Ƀ{�N����������Ɣނ͂悭�{���Ă܂����B");

                            UpdateMainMessage("�w�����f�B�X�F�A�[�e�B�I�I�Ă߂��A����o�b�J���ȁI�I�I�x");

                            UpdateMainMessage("���i�F�A�[�e�B�H");

                            UpdateMainMessage("���F���[�F�{�N�̉��̌Ăі��ł��B���F���[�͔��������h���āA���������悤�ł��B");

                            UpdateMainMessage("�A�C���F���ǂǂ�Ȃ̂�����Ă����񂾁H");

                            UpdateMainMessage("���F���[�F�̓��ɓ��݂��Ă��鐸�_�͂������o�����ŃX�L�����񕜂���InnerInspiration�ł��B");

                            UpdateMainMessage("���F���[�F�{�N�̏ꍇ�A���\�X�L���𑽒i���p����̂ŁA�悭������g����ł���B");

                            UpdateMainMessage("�A�C���F�m���Ƀ��F���[�̏ꍇ�A�X�L���U�������\�L�x����ȁB");

                            UpdateMainMessage("���i�F���F���[������āA����ł��X�L�����͊����鎖���Ă��܂�Ȃ��ł���ˁH");

                            UpdateMainMessage("���F���[�F�퓬���̓C�U�Ƃ������̂��߂ɂ�����x���߂Ă������ł�����ˁB");

                            UpdateMainMessage("���F���[�F���������̃X�L��������΂��̐S�z���Ȃ��Ȃ�܂��B��p�̕��͂���Ɗg����܂��B");

                            UpdateMainMessage("���i�F�A�C���������͌��K���Ȃ�����H�o�J�݂����Ɏg���Ă����O�ɂ��Ȃ��悤�Ɂ�");

                            UpdateMainMessage("�A�C���F�͂��͂��A��[������܂�����B���������A�o���Ă��B");

                            md.Message = "�����F���[�̓C���i�[�E�C���X�s���[�V�������K�����܂�����";
                            md.ShowDialog();
                            tc.InnerInspiration = true;
                        }
                        if (tc.Level >= 19 && !we.AlreadyLvUpEmpty32)
                        {
                            UpdateMainMessage("���F���[�F�k�u�Q�O�܂ł��ƈ���Ɣ���܂�����B");

                            UpdateMainMessage("���i�F���F���[����́A���ԂƂ��͍D���ł����H");

                            UpdateMainMessage("���F���[�F�Ԃ́E�E�E�����ȏ��A�D���ł͂���܂���B");

                            UpdateMainMessage("���i�F���A�����Ȃ�ł����B�܂��D�݂̖�肾���A���傤���Ȃ���ˁB");

                            UpdateMainMessage("���F���[�F�i�����Ă���Ǝv���܂����H");

                            UpdateMainMessage("���i�F���H���A�����E�E�E���ƁA�������ȁ�");

                            UpdateMainMessage("���F���[�F���̐��ɐ��܂�Ă��������͑S�ĉi���ł͂���܂���B");

                            UpdateMainMessage("���F���[�F���i����A�Ԃ���͉i����A�z���܂��񂩁H");

                            UpdateMainMessage("���F���[�F���̔������A�ƂĂ��l�̎�őn�����̂ł͂���܂���B");

                            UpdateMainMessage("���F���[�F�������A���̔��������K���I��肪�K��܂��B");

                            UpdateMainMessage("���F���[�F�i��������������B�����i���ł͂Ȃ��B");

                            UpdateMainMessage("���F���[�F�����E�E�E�Ԃ��܂��A�{�N�̂悤���E�E�E");
                            
                            UpdateMainMessage("���F���[�F�߂��E�E�E�߂��B���Ȃ��ł���B");

                            UpdateMainMessage("���i�F�E�E�E�@�E�E�E");

                            UpdateMainMessage("�A�C���F�i�����E�E�E�����A���i�j");

                            UpdateMainMessage("���i�F�i����H�j");

                            UpdateMainMessage("�A�C���F�i���O�A���̘b�������񂾁H�j");

                            UpdateMainMessage("���i�F�i���Ԃ�E�E�E�ʂɕςȘb����������͂Ȃ��񂾂��ǁj");

                            UpdateMainMessage("���F���[�F�����ƍ炫�����Ă���ꂽ��E�E�E������ʖڂ��B���ꂶ��ʖڂȂ񂾁B");

                            UpdateMainMessage("���F���[�F�{�N�́E�E�E�{�N�͍ŒႾ�I�I�I�������������I�I�I");

                            UpdateMainMessage("���i�F�i�E�E�E�s���܂���A����͎������������Ǝv����B�j");

                            UpdateMainMessage("�A�C���F�i���܂����A���F���[�̂��̏�Ԓm���Ă邾�낤���A�C�������ȁj");

                            UpdateMainMessage("���i�F�i����E�E�E���Ԃ͋֋�ˁE�E�E�j");

                            UpdateMainMessage("���F���[�F�炢�Ă������̂��Ȃ��A�ƂĂ��Y��ł����B");

                            UpdateMainMessage("���F���[�F�{�N�͂����Ƃ��Ȃ���Y��܂���B�u�z���`���Ήi���v�ł�����ˁE�E�E");

                            md.Message = "�����F���[�͉����K���ł��܂���ł�����";
                            md.ShowDialog();
                            we.AlreadyLvUpEmpty32 = true;
                        }
                        if (tc.Level >= 20 && !tc.FlameStrike)
                        {
                            UpdateMainMessage("���F���[�F���܂����B�k�u�Q�O���B�ł��B");

                            UpdateMainMessage("�A�C���F��邶��˂����A���F���[�I");

                            UpdateMainMessage("���F���[�F�n�n�n�A���������ĂȂ��Ȃ����������̂Ȃ�ł���B");

                            UpdateMainMessage("�A�C���F�k�u�Q�O���B�L�O�ł́A�����K������񂾁H");

                            UpdateMainMessage("���F���[�F�K������̂́A�A�C���N�Ɠ���FlameStrike�ɂ��悤�ƑO���猈�߂Ă��܂����B");

                            UpdateMainMessage("�A�C���F�b�Q�I�}�W����I�H");

                            UpdateMainMessage("���i�F�A�C�����K�������k�u�͂Q�O��������ˁB�܂��ɓ����^�C�~���O�ˁ�");

                            UpdateMainMessage("���F���[�F���i������A�C���N������Ă��Ă��������B");

                            UpdateMainMessage("���F���[�F�ł́A�s���܂��B��u�ŏĂ��s�����AFlameStrike�ł��B");

                            UpdateMainMessage("      �w�b�V���E�E�E�D�D�E�E�E�b�S�S�I�I�I�H�H�I�I�I�I�x");

                            UpdateMainMessage("�A�C���F�I�I�I�I�I");

                            UpdateMainMessage("���F���[�F����Ȃ��̂ł��傤�B�v���Ԃ�ł����A��͂�u���ł��ˁB");

                            UpdateMainMessage("�A�C���F���A���̊m���ɁE�E�E");

                            UpdateMainMessage("���F���[�F�ǂ������܂������H");

                            UpdateMainMessage("�A�C���F���̂Ƃ͑S���Ⴄ�ȁB");

                            UpdateMainMessage("���F���[�F�������@�ł������Ƃ��Ă��A�r���҂��Ⴆ�΍��ق͏o�Ă��܂��ˁB");

                            UpdateMainMessage("���F���[�F�{�N�̂͏����Ђ˂����O����`���đΏە��Ɍ������܂���");

                            UpdateMainMessage("���F���[�F�A�C���N�̏ꍇ�́A�Ώە��܂Œ����O���Ői��ł��܂��B");

                            UpdateMainMessage("���F���[�F�J�[���݂͖̂ʔ����ł���H��������`���Y��ȉ~��`���܂��B");

                            UpdateMainMessage("���i�F����ɂ��Ă������v���񂾂��ǁA�z���b�g������E�E�E");

                            UpdateMainMessage("���i�F�o�J�A�C���̂Q�{���炢�̃X�s�[�h����Ȃ��H");

                            UpdateMainMessage("�A�C���F�m���ɉ����͗y���ɑ����E�E�E");

                            UpdateMainMessage("���F���[�F�O�ɂ����x�������Ă��܂����A�������[�V�����Ƃ����̂��d�v�ł��B");

                            UpdateMainMessage("���F���[�F�{�N�̏ꍇ�́A�r���J�n���鎞�Ǝ葫�̖�̋L�q���قړ����Ɏn�߂鎖�ɂ��Ă��܂��B");

                            UpdateMainMessage("���F���[�F�r�����Ɏ�𓮂������肷��̂͋C�������l�����邻���ł���");

                            UpdateMainMessage("���F���[�F�J�[���݂̃��x���ɂł��Ȃ�Ȃ�����A���������_���[�W�ʂ��ς�鎖�͂���܂���B");

                            UpdateMainMessage("���F���[�F�A�C���N����x����Ă݂܂��񂩁H");

                            UpdateMainMessage("���F���[�F�����ł��B�������āE�E�E���̎���������r���ł��ˁE�E�E���̒��O��");

                            UpdateMainMessage("�A�C���F�E�E�E����A��ŋ����Ă���E�E�E");

                            md.Message = "�����F���[�̓t���C���E�X�g���C�N���K�����܂�����";
                            md.ShowDialog();
                            tc.FlameStrike = true;
                        }
                        if (tc.Level >= 21 && !tc.AntiStun)
                        {
                            UpdateMainMessage("���F���[�F�k�u�Q�P�ɂȂ�܂����B");

                            UpdateMainMessage("���i�F���F���[����A�O��̓A�C���Ɠ������@���K�����Ă���ˁB");

                            UpdateMainMessage("���i�F���x�͂�����Ǝ��Ɠ������m���K���͏o���Ȃ�������H");

                            UpdateMainMessage("�A�C���F���O���A�l�̑M����K��������̂ɑ΂��āA���肢�Ȃ񂩂���Ȃ�ȁB");

                            UpdateMainMessage("���i�F�ǂ�����Ȃ��A�ʂɁB");

                            UpdateMainMessage("���F���[�F�n�n�n�A���i����ʔ������������܂��ˁB�����A�ǂ��ł���B");

                            UpdateMainMessage("���F���[�F�����ł��ˁA���ႠAntiStun�ɂ��Ă݂܂��傤�B");

                            UpdateMainMessage("���F���[�FAntiStun�̓X�^�����ʂɑ΂��Ă̑ϐ����t���̂͒m���Ă܂��ˁH");

                            UpdateMainMessage("���i�F����A�_�~�[�f�U��N�ň�x���؍ς݂��");

                            UpdateMainMessage("���F���[�F����������AntiStun�͌���I�Ȏ�_������܂��B�A�C���N������܂����H");

                            UpdateMainMessage("�A�C���F�U���_���[�W�ł͂Ȃ��I");

                            UpdateMainMessage("���F���[�F�n�Y���ł��B���i����́H");

                            UpdateMainMessage("���i�F���`��A�����낤�E�E�E�^�C�~���O������ď�������H");

                            UpdateMainMessage("���F���[�F���������ł��ˁB���Ⴀ�{�N���班��������܂��傤�B");

                            UpdateMainMessage("���F���[�F���̃X�L���A�搧�����Ȃ��Ɛ�p�Ƃ��Ă͏\���Ȍ��ʂ���������܂���B");

                            UpdateMainMessage("���F���[�F�悭�l���Ă݂Ă��������B�ŏ��̍U���ŃX�^���U�����o����Ƃ�����");

                            UpdateMainMessage("���F���[�F��������s���āA���肪�X�^���U���ւ̑ϐ��������Ă邩�m�낤�Ǝv���܂��񂩁H");

                            UpdateMainMessage("�A�C���F�������A�����������B");

                            UpdateMainMessage("�A�C���F�搧������ĂȂ���AntiStun���o���Ă��Ȃ���Ԃő��肩��X�^���U��������B");

                            UpdateMainMessage("�A�C���F�X�^���ϐ���t���悤�Ƃ��Ă�̂ɁA�ŏ��H�������t����˂�����ȁB");

                            UpdateMainMessage("�A�C���F���Ď��ŁA�ŏ��̈ꔭ���~�߂�Ȃ��悤����Ӗ����˂����Ęb����B�Ⴄ���H");

                            UpdateMainMessage("���i�F�E�E�E�����H�����Ȃ́H");

                            UpdateMainMessage("���F���[�F���̂Ƃ���ł��B�A�C���N�A��͂�Z���X���ǂ��ł��ˁB");

                            UpdateMainMessage("���F���[�F��ԏ��߂ɃX�^���ϐ����t������悤�ɂȂ�ΐ�p�̑g�ݍ��킹�͊g����܂���B");

                            UpdateMainMessage("���i�F���F���[������čs������������A���͐�s��ˁB");

                            UpdateMainMessage("���F���[�F���i�����������g�����͂Ȃ�ׂ���s������悤�ɂ��Ƃ��Ă��������B");

                            UpdateMainMessage("���i�F����A����������B�z���b�g���߂ɂȂ�����A���肪�Ɓ�");

                            UpdateMainMessage("���F���[�F�A�C���N�������c�t�d�k�����鎞�͓��ɒ@������ł����Ă��������B");

                            UpdateMainMessage("�A�C���F�I�[�P�[�I�[�P�[�I");

                            md.Message = "�����F���[�̓A���`�E�X�^�����K�����܂�����";
                            md.ShowDialog();
                            tc.AntiStun = true;
                        }
                        if (tc.Level >= 22 && !tc.WordOfFortune)
                        {
                            UpdateMainMessage("���F���[�F�k�u�Q�Q�Ə����ɏオ���Ă��܂��ˁB");

                            UpdateMainMessage("�A�C���F���F���[���Ă��܂Ɉ�l�q�σZ���t�������������ȁB");

                            UpdateMainMessage("���F���[�F�ςȌ��Ȃł�����A�C�ɂ��Ȃ��ł��������B");

                            UpdateMainMessage("���F���[�F���āA�ǂ���獡��̎v���o�������e�͖��@�̂悤�ł��ˁB");

                            UpdateMainMessage("���F���[�F����́E�E�EWordOfFortune�ł��ˁI���܂�����I");

                            UpdateMainMessage("�A�C���F�����A�₯�Ɋ����������ȁH");

                            UpdateMainMessage("���F���[�F����͂����ł���A�����{�N�̑S��������̏�p�X�L���ł�����B");

                            UpdateMainMessage("���F���[�F����100%�N���e�B�J���Ƃ����̂̓{�N�ɂƂ��Ă͌������Ȃ����݂ł��B");

                            UpdateMainMessage("���i�F��������ˁA���F���[���񂪂���ȂɊ�ԂȂ�āB");

                            UpdateMainMessage("���F���[�F�N���e�B�J���ƌ����̂́A��{�_���[�W�̒ꂪ�R�{�ɂȂ���̂ł��B");

                            UpdateMainMessage("���F���[�F�����̃{�N�͂��̖��@�ɂƂĂ��䂩��܂����ˁB");

                            UpdateMainMessage("���F���[�F���̖��@���g��������A�{�N�̐�p���m�����n�߂��Ƃ����Ă��ߌ��ł͂���܂���B");

                            UpdateMainMessage("�A�C���F�N���e�B�J���ɂ���p�Ȃ�Ă��̂�����̂��H");

                            UpdateMainMessage("���F���[�F�����ł��ˁA�Ⴆ�΃A�C���N�̃��C�t���c��T�O�O���Ƃ��܂��傤�B");

                            UpdateMainMessage("���F���[�F�{�N�̍U������{�l�͂T�O���Ƃ����ꍇ�A�A�C���N�͂P�O��܂Ŏ󂯎~�߂��܂��B");

                            UpdateMainMessage("�A�C���F����A���₢��A���������͕̂����邯�ǂȁB��p���Ă̂��s���Ƃ��˂��񂾁B");

                            UpdateMainMessage("���F���[�F���ۂ���Ă݂�Ε�����܂����ˁB���i���񂿂���Ɨ���ĂĂ��������B");

                            UpdateMainMessage("���F���[�F�A�C���N�A�ł͍s���܂���B���ł��g���Č��\�ł��B");

                            UpdateMainMessage("�A�C���F�̂ŕ����点����Ă��A�㓙���I�s�����I�I");

                            UpdateMainMessage("      �w�b�K�L�C�I�@�b�K�b�K�K�K�K�I�@�p�b�V�C�C�B�B���I�i�퓬���J��L������j�x");

                            UpdateMainMessage("�A�C���F�b�`�E�E�E�t���b�V���q�[���I");

                            UpdateMainMessage("���F���[�F�����Ď����Ō�ł��傤�A���[�h�E�I�u�E�t�H�[�`��������");

                            UpdateMainMessage("�A�C���F�I�I�I�����܂��I");

                            UpdateMainMessage("���F���[�F�_�u���E�X���b�V���ł��B�b�n�@�I");

                            UpdateMainMessage("      �w�b�u���D���A�b�K�V�C�C�B���I�I�I�x");

                            UpdateMainMessage("�A�C���F�b�O�A�O�n�@�I�I�I�E�E�E�C�e�e");

                            UpdateMainMessage("���i�F�E�\�A���捡�́E�E�E�q�[�����Ă�^�C�~���O�Ƃقړ�������B");

                            UpdateMainMessage("���F���[�F�A�C���N�A�q�[������^�C�~���O�������܂����ˁB");

                            UpdateMainMessage("�A�C���F�������A���Ȃ�˂����B����ȃ^�C�~���O�ŏo���ꂽ��B");

                            UpdateMainMessage("���F���[�F���̖��@�͐퓬�̔g�E�o�����X��ˑR�������@�ł��B");

                            UpdateMainMessage("���i�F�A�C���E�E�E�������ɃL�c�C��ˁB�������������ڂɉ���Ă�Ǝv����B");

                            UpdateMainMessage("���F���[�F���i�ǂ���̐퓬������Ă���Ƒ������d����̂ŋC�����Ă��������B");

                            md.Message = "�����F���[�̓��[�h�E�I�u�E�t�H�[�`�������K�����܂�����";
                            md.ShowDialog();
                            tc.WordOfFortune = true;
                        }
                        if (tc.Level >= 23 && !tc.Glory)
                        {
                            UpdateMainMessage("���F���[�F�k�u�Q�R�ł����A�����łЂƂA�A�C���N�Ɏ���ł��B");

                            UpdateMainMessage("�A�C���F�����I�H������H���������H");

                            UpdateMainMessage("���F���[�FFlameStrike�AWordOfFortune�A�����č���v���o����Glory�B");

                            UpdateMainMessage("�A�C���F�O���[���[���v���o�������Ă��B�z���g���̊o���鏇���̂܂܂��ȁB");

                            UpdateMainMessage("���F���[�F�n�n�n�A�m���ɂ����ł����B���R�ł��ˁB");

                            UpdateMainMessage("���F���[�F���āA����Glory�ł����A�C�Â�������S�Č����Ă݂Ă��������B");

                            UpdateMainMessage("�A�C���F�S������B�������Ȃ��E�E�E");

                            UpdateMainMessage("�A�C���F�񕜁@�{�@�A�^�b�N�I�I");

                            UpdateMainMessage("�A�C���F�_���[�W���[�X�ŗL�����B");

                            UpdateMainMessage("�A�C���F�ꎞ�I���ʂȂ̂łR�^�[�����炢�ŏ������܂��B");

                            UpdateMainMessage("�A�C���F��͂������Ȃ��E�E�E�@�E�E�E");

                            UpdateMainMessage("���F���[�F�A�C���N�A����͋C�Â������ł͂Ȃ��A���@�̓������q�ׂĂ��邾���ł��B");

                            UpdateMainMessage("���F���[�F���@�E�X�L�����ǂ�������̂��œK���𔭌����邱�Ƃ��C�Â����ł��B");

                            UpdateMainMessage("���i�F���F���[����A�����̃o�J�A�C���ɂ͕s�\����B");

                            UpdateMainMessage("���F���[�F�������A�����Ƃ�����܂���B�{�N���猩��ƃA�C���N�A�Z���X�͊m���ł��B");

                            UpdateMainMessage("���i�F���A�����H���`��A����Ȏ������Ǝv�����ǁE�E�E�A�C���A�ǂ��H");

                            UpdateMainMessage("�A�C���F�E�E�E�@�������ȁB");

                            UpdateMainMessage("�A�C���F���ڍU����������ꂽ�肵���ꍇ�ł��A���C�t�Q�C���ɂ͂Ȃ�B");

                            UpdateMainMessage("�A�C���F�����牴�̕��͒��ڍU����������Ȃ������Ƃ��Ă����͂��˂��B");

                            UpdateMainMessage("�A�C���F�ނ���A��{�U�����Ⴍ�`�}�`�}��낤�Ƃ��郄�c�ɂƂ�����Г�낤�ȁB");

                            UpdateMainMessage("���F���[�F���΂炵���ł��ˁA�������A�C���N�ł��B");

                            UpdateMainMessage("���i�F�A�C�����Ă��܂ɕςȎ������Ă邯�ǁA�I�𓾂Ă��鎞������̂ˁB");

                            UpdateMainMessage("���F���[�F������������������ςݏd�˂Ă݂Ă��������B");

                            UpdateMainMessage("�A�C���F�����A����A�h�o�C�X���Ă���ď����邺�A�T���L���[�B");

                            md.Message = "�����F���[�̓O���[���[���K�����܂�����";
                            md.ShowDialog();
                            tc.Glory = true;
                        }
                        if (tc.Level >= 24 && !tc.Tranquility)
                        {
                            UpdateMainMessage("���F���[�F�{�N�����������オ���Ă��܂����B�k�u�Q�S�ɂȂ�܂����B");

                            UpdateMainMessage("���i�F���F���[������Ďv���o�����Ԃɖ@�����͂����ł����H");

                            UpdateMainMessage("���F���[�F�ǂ��ł��傤�ˁA���������v���o���̂ɏ��Ԃ͌��߂��Ȃ��Ǝv���܂��B");

                            UpdateMainMessage("���i�F�b�t�t�A�����������ˁ�@����͂ǂ�Ȃ̂��v���o����́H");

                            UpdateMainMessage("���F���[�FTranquility�̂悤�ł��B��������X�D���ł��ˁB");

                            UpdateMainMessage("���F���[�FDispelMagic�Ƃ͈Ⴄ������ł������Ă����̂͂��肪�����ł��B");

                            UpdateMainMessage("���i�F�ł�Tranquility�ŏ����镔�����g���Ă��鑊����Ē��X���Ȃ���ˁB");

                            UpdateMainMessage("���i�F�������ꎞ�I���ʂ����疳�����đł��Ȃ��Ă��ǂ����ċC�������B");

                            UpdateMainMessage("���F���[�F�����X�^�[�����̎�̐�p���g���Ă���P�[�X�͋ɋH�ł��B");

                            UpdateMainMessage("���F���[�FTranquility�ŏ����閂�@�Ƃ����̂͑����̐�p�p�^�[��������܂��B");

                            UpdateMainMessage("���F���[�F�ǂ����������͂ȃp�^�[���ł�����A�������Ă��Ă��h���Ȃ��ꍇ");

                            UpdateMainMessage("���i�F�����ˁA���̖��@�őł������Ă��܂��΁A���Y�������킹��ꂻ���ˁB");

                            UpdateMainMessage("���F���[�F���Y�������킹��E�E�E�ǂ������ł��B");

                            UpdateMainMessage("���F���[�F�퓬�Ɋ���ė������͈̂��̐�p�p�^�[���^�ɂ͂܂肪���ł�����");

                            UpdateMainMessage("���F���[�F�ނ炪�҂ݏo����p���A���̖��@�ŕ��ꋎ��u�Ԃ́E�E�E���Ƃ��߂ł��B");

                            UpdateMainMessage("���i�F���́E�E�E���F���[����");

                            UpdateMainMessage("���F���[�F�߂��Ǝv���܂��񂩁H���̖��@���̂��̂��E�E�E�É��Ƃ͖��΂���B");

                            UpdateMainMessage("���F���[�F����̐S����Ԃ��������h���Ԃ�܂��B���A�����������킹�Ă��܂����E�E�E");

                            UpdateMainMessage("���i�F�E�E�E���܂�����A�u���Y���v�u�����v���֋�݂����E�E�E");

                            md.Message = "�����F���[�̓g�����L�B���e�B���K�����܂�����";
                            md.ShowDialog();
                            tc.Tranquility = true;
                        }
                        if (tc.Level >= 25 && !tc.MirrorImage)
                        {
                            UpdateMainMessage("���F���[�F�k�u�Q�T�ł��B���āA����͂ǂ�ōs���܂��傤�B");

                            UpdateMainMessage("�A�C���F���F���[�����ʓ��ӂȖ��@�������ĉ��ɂȂ�񂾁H");

                            UpdateMainMessage("���F���[�F��p�n�R�ł�����ˁA���ʓ��ӂƂ����͓̂���ł����E�E�E");

                            UpdateMainMessage("�A�C���ƃ��i�F�i��p�n�R�͂˂���ȁE�E�E�j�i�Ȃ���ˁE�E�E�j");

                            UpdateMainMessage("���F���[�F�{�N�̏ꍇ�A�����@�ł��ˁB");

                            UpdateMainMessage("���i�F�����H�����Ȃ́I�H");

                            UpdateMainMessage("���F���[�F�n�C�A�����炭�B���̓��Ă��X�̃C���[�W����Ԉ����₷���ł��B");

                            UpdateMainMessage("���F���[�F�����ł��ˁAMirrorImage�Ȃ�Ăǂ��ł��傤�B����Ă݂܂��傤���B");

                            UpdateMainMessage("�A�C���F���ւ��E�E�E���F���[��MirrorImage�A�������������X���ȁH");

                            UpdateMainMessage("���F���[�F���̖��@�͉����[���ł��B���������C���[�W��������Ă���񂾂Ǝv���܂��B");

                            UpdateMainMessage("���F���[�F�J�E���^�[�Ƃ͈Ⴂ�A���̖��@�̓_���[�W�n���@�𒵂˕Ԃ��܂��B");

                            UpdateMainMessage("���F���[�F�܂����O�ɐ錾���閂�@�ł�����A�ʏ�̓_���[�W�n���@�͂����ł���Ȃ��ł��傤�B");

                            UpdateMainMessage("���F���[�F�������o���Ă��܂������ɂȂ�܂��񂩁H���i����B");

                            UpdateMainMessage("���i�F���������v�����̂�ˍŏ��E�E�E");

                            UpdateMainMessage("�A�C���F�����A�Ⴄ�̂���H");

                            UpdateMainMessage("���F���[�F�ŏ��͕K���͂˕Ԃ��܂��B�܂葊��͍ŏ��育��Ȗ��@��ł��Ă���");

                            UpdateMainMessage("���F���[�F���̌�ő傫���_���[�W�̖��@��ł����߂΂����킯�ł��B");

                            UpdateMainMessage("���F���[�FMirrorImage�������Ă��鑤�͈��S���Ă���ł��傤����A");

                            UpdateMainMessage("���F���[�F�����������U���ɑ΂��Ă͈ӊO�Ɩ��h���ɂȂ肪���Ȃ�ł���B");

                            UpdateMainMessage("���i�F�����Ȃ̂�B�����玄������܂�ߓx�ɂ��̖��@�ɂ͊��҂ł��Ȃ���");

                            UpdateMainMessage("���F���[�F����ŗǂ���ł��惉�i����B");

                            UpdateMainMessage("���F���[�F�ߓx�Ȋ��҂͏o���Ȃ��B������������K���ꔭ�͓��ĂȂ��Ƃ����Ȃ��B");

                            UpdateMainMessage("���F���[�F�v����ɑ��肪��������_���[�W�n���@��ł��Ă����Ƃ�����A");

                            UpdateMainMessage("���F���[�F���͑傫���_���[�W���@���\���Ă���B�����������ɓ`���Ă���悤�Ȃ��̂ł��B");

                            UpdateMainMessage("���F���[�F�����Ńu���t���d�|���Ă���ꍇ���z�肳��܂����A���ʂ����܂ł͂��܂���ˁB");

                            UpdateMainMessage("���F���[�FMirrorImage�͒[�I�Ɍ����΁A��p�̕����L����̂ł͂Ȃ��A����̐�p�������߂�B");

                            UpdateMainMessage("���F���[�F�����������̂��Ɖ��߂��Ďg���΁A���Ȃ�g������͏オ��܂���B");

                            UpdateMainMessage("���i�F�E�E�E���H�������ƁE�E�E���肪�Ɓ�");

                            UpdateMainMessage("�A�C���F�ʖڂ��B���ɂ͑S�R������Ȃ��������E�E�E");

                            UpdateMainMessage("���F���[�F�����܂���A�܂�Ȃ����e�ł����B�C�y�Ɏg���čs���܂��傤�B");

                            md.Message = "�����F���[�̓~���[�E�C���[�W���K�����܂�����";
                            md.ShowDialog();
                            tc.MirrorImage = true;
                        }
                        if (tc.Level >= 26 && !tc.CrushingBlow)
                        {
                            UpdateMainMessage("���F���[�F�k�u�Q�U�ł��ˁB���낻��K���������������Ă������ł��B");

                            UpdateMainMessage("���i�F���F���[����A����Ȏ������̂��ςł����ǁE�E�E");

                            UpdateMainMessage("���F���[�F���ł��傤���H");

                            UpdateMainMessage("���i�F�ǂ����ă��F���[����͑S��������̏K���������̂�Y��Ă��܂����́H");

                            UpdateMainMessage("���F���[�F�Y�ꂽ�킯�ł͂���܂����B");

                            UpdateMainMessage("���i�F�ł��A���͎v���o���Ă���Œ��Ȃ̂�ˁH");

                            UpdateMainMessage("���F���[�F�����A�����ł��ˁB");

                            UpdateMainMessage("���i�F���`��E�E�E������ƕ�����Ȃ��񂾂��ǁE�E�E");

                            UpdateMainMessage("���F���[�F�n�n�n�A�����܂��񏭂��Ӓn�������Ă��܂��܂����B");

                            UpdateMainMessage("���F���[�F���傤�Ǘǂ��Ⴆ�b������܂��B����{�N���v���o�����̂́B");

                            UpdateMainMessage("���F���[�F�����܂��ACrushingBlow�ł��B");

                            UpdateMainMessage("      �w�b�K�R�I�I�H�H���I�@�i�A�C���̓��F���[��CrushingBlow��H������j�x");

                            UpdateMainMessage("�A�C���F�E�E�E�i�p�^�j");

                            UpdateMainMessage("�A�C���F�E�E�E");

                            UpdateMainMessage("�A�C���F�E�E�E�b�e�e�e�E�E�E�����Ȃ艽���񂾃��F���[�I�H");

                            UpdateMainMessage("���F���[�F�A�C���N�A���������i����ƃ{�N�͉���b���Ă��܂����H");

                            UpdateMainMessage("�A�C���F��H�E�E�E���ƁA������Ƒ҂��Ă���E�E�E");

                            UpdateMainMessage("�A�C���F�E�E�E�������������E�E�E�E���ƁE�E�E");

                            UpdateMainMessage("�A�C���F���F���[�͑S��������̃X�L����Y�ꂽ�̂��ǂ����H�������ȁB");

                            UpdateMainMessage("���F���[�F���i����A���傤�ǂ���Ȋ����ł��B");

                            UpdateMainMessage("���i�F�Ȃ�قǁA�Ȃ�قǁ�@�������");

                            UpdateMainMessage("�A�C���F���O��A����Ⴆ�b�̓I�ɂ���Ȃ�ȁE�E�E");

                            md.Message = "�����F���[�̓N���b�V���O�E�u���[���K�����܂�����";
                            md.ShowDialog();
                            tc.CrushingBlow = true;
                        }
                        if (tc.Level >= 27 && !tc.OneImmunity)
                        {
                            UpdateMainMessage("���F���[�F�k�u�Q�V�ɂ��Ȃ�Ə����k�u�R�O�����҂��Ă��܂��܂��ˁB");

                            UpdateMainMessage("�A�C���F���F���[�A���܂��̊o����X�L���͌��\�l�I�Ȃ̂������ȁB");

                            UpdateMainMessage("���F���[�F���������m��܂���ˁB�����Ƀp�[�e�B�v���C�ȂǏ��X���ł��B");

                            UpdateMainMessage("�A�C���F����A���������Ӗ�����˂����ǂ��B����v���o���̂͂ǂ�Ȃ̂��H");

                            UpdateMainMessage("���F���[�FOneImmunity�ł��B����͂Ȃ��Ȃ��g���h�����̂ł��B");

                            UpdateMainMessage("���F���[�F�m���Ɋ��S�h��͗L�������킳�����͂ł����A");

                            UpdateMainMessage("���F���[�F�g�������ԈႦ��΁A����ɃA�h�o���e�[�W��^���邾���ł�����ˁB");

                            UpdateMainMessage("���F���[�F�Ƃ���ŃA�C���N�͖h��n�ɂ͂��܂苻���͂���܂��񂩁H");

                            UpdateMainMessage("�A�C���F���̂Ƃ���͂ȁA���܂苻���͂˂��B����ł��N�\�����f�B�̂������ȁB");

                            UpdateMainMessage("���F���[�F���������΁A�ނ��t���ł����ˁB�n�n�n�A�����܂��񂻂ꂶ�ዻ���͂���܂���ˁB");

                            UpdateMainMessage("�A�C���F�ł����̐�Ζh����Ă͖̂{���Ƀ��J�����@���ȁB��������˂��̂��H");

                            UpdateMainMessage("���F���[�F���̖��@�͖h��̍\�������Ȃ��ƁA���ʂ𔭊����Ȃ���ł���B");

                            UpdateMainMessage("���F���[�F�h������Ă���ƁA�U���͓��R�ł��܂���A�\���t�F�A�ȓ��e�ł��B");

                            UpdateMainMessage("���F���[�F�h�䂵�Ă���Ԃɑ��肪���C�t�񕜂�p�����^�t�o�n�����Ă����疳�ʂɂȂ�܂��B");

                            UpdateMainMessage("���F���[�F���������Ӗ��ł́A���̖��@�͂���قǑ���Ɉ��͂͗^���Ă�Ƃ͌����܂���B");

                            UpdateMainMessage("���F���[�F�ł���");

                            UpdateMainMessage("�A�C���F��H");

                            UpdateMainMessage("���F���[�F�E�E�E�����ł��ˁB");

                            UpdateMainMessage("�A�C���F�����悻��I�H������惔�F���[�I");

                            UpdateMainMessage("���F���[�F���x�c�t�d�k�̋@��ł�����΁A���������܂��B�y���݂ɂ��ĂĂ��������B");

                            UpdateMainMessage("�A�C���F�}�W����I������A���̎��͐�΂���Ă��炤����ȁI");

                            md.Message = "�����F���[�̓����E�C���[�j�e�B���K�����܂�����";
                            md.ShowDialog();
                            tc.OneImmunity = true;
                        }
                        if (tc.Level >= 28 && !tc.AetherDrive)
                        {
                            UpdateMainMessage("���F���[�F�k�u�Q�W�ł��ˁB");

                            UpdateMainMessage("�A�C���F�����A���F���[�Ɍ��ɂ߂Ăق�����������񂾁B�����Ă���邩�H");

                            UpdateMainMessage("���F���[�F�{�N�œ������鎖�Ȃ�A���ł��ǂ����B");

                            UpdateMainMessage("�A�C���F�p�����^�t�o�n�͋����Ǝv�����H");

                            UpdateMainMessage("���F���[�F�����A�����ł���B�����AetherDrive���K���ł������ł��B");

                            UpdateMainMessage("�A�C���F���F���[���Ă��̂܂܂ŏ\����������H");

                            UpdateMainMessage("���F���[�F����͔������Ԃ�ł��B���x�������Ă܂����A����ւ̍��o�v�f�����������ł��B");

                            UpdateMainMessage("���F���[�F�ł����A�C���N�̎w�E�ǂ���A����{�N�̏K������̂�");

                            UpdateMainMessage("���F���[�F�����U���Q�{�A�����h�䔼���ŊԈႢ�Ȃ��������@�ł��B");

                            UpdateMainMessage("�A�C���F���F���[�̂Q�{�U�����R�^�[���E�E�E�l���������˂����e���ȁB");

                            UpdateMainMessage("���i�F���F���[������ă^�C�~���O���▭�ɗǂ��̂�ˁB");

                            UpdateMainMessage("�A�C���F�Ђ���Ƃ��ă��F���[�����T�^�[�����炢�p�������񂶂�˂����낤�ȁH");

                            UpdateMainMessage("���F���[�F�n�n�n�A����͂����ɖ����ł���B�ԈႢ�Ȃ��p�����Ԃ͂R�^�[���܂łł��B");

                            UpdateMainMessage("���F���[�F�A�C���N�A�{�N�͂���Ȃɐ�������܂���B�����֒�����߂����������ł��B");

                            UpdateMainMessage("�A�C���F�������Ɨǂ����ǂȁA���͂ǂ������F���[�͂܂��܂���������Ă銴�����Ă�B");

                            UpdateMainMessage("���F���[�F������Ƃ͏����Ⴂ�܂��ˁA���䂵�Ă���Ƒ����Ă��������B");

                            UpdateMainMessage("���F���[�F�ǂ�ȗ͂�Z����݂����ȐU�肩�������ł͑ʖڂł��B");

                            UpdateMainMessage("���F���[�F����AetherDrive�ɂ��Ă��A�����P�ɂR�^�[���U�������Ƃ�");

                            UpdateMainMessage("���F���[�F�������Ă��邾���ő����h�q�I�Ȏv�l�Ɋׂ�鎖���\�ł�����ˁB");

                            UpdateMainMessage("�A�C���F�����l�����鏊����������ȁB���Ȃ񂩂܂��܂����Ċ��������B");

                            UpdateMainMessage("���i�F�A�C�������܂ɕςȎ��������Ȃ��玗���悤�Ȏv�l���Ă���H");

                            UpdateMainMessage("�A�C���F�����I�H��k���냉�i�A�b�n�b�n�b�n�I");

                            UpdateMainMessage("���i�F���`��A�C�̂���������ˁB");

                            UpdateMainMessage("���F���[�F�E�E�E���āA���x�̋@��ɂ܂��ꏏ�ɍl���Ă݂܂��傤�B");

                            md.Message = "�����F���[�̓G�[�e���E�h���C�u���K�����܂�����";
                            md.ShowDialog();
                            tc.AetherDrive = true;
                        }
                        if (tc.Level >= 29 && !we.AlreadyLvUpEmpty33)
                        {
                            UpdateMainMessage("���F���[�F�k�u�Q�X�Ƃ������Ƃ́A�R�O�܂Ō��Ɣ���܂����ˁB");

                            UpdateMainMessage("���i�F���F���[����A�ق猩�Č��ā�");

                            UpdateMainMessage("���F���[�F���ł��傤�H");

                            UpdateMainMessage("�A�C���F�҂āA���i�B");

                            UpdateMainMessage("���i�F�����H����H");

                            UpdateMainMessage("�A�C���F���́A�����B���ꌩ���ă��F���[�̒��q���܂�����Ȃ����낤�ȁH");

                            UpdateMainMessage("���i�F���v�悱�̂��炢�B�������A���Ă��傤�������F���[�����");

                            UpdateMainMessage("���F���[�F����́A�����ł��ˁB���i���񂪍�����̂ł����H");

                            UpdateMainMessage("���i�F���`��A������Ȃ����ǂˁB�ǂ��H");

                            UpdateMainMessage("���F���[�F�ƂĂ��Y�킾�Ǝv���܂��B���i����Ɏ����������ł���B");

                            UpdateMainMessage("���i�F�n�[�g�^�̎���肾���A������ƍD�݂���Ȃ���ˁA���́B");

                            UpdateMainMessage("���F���[�F���������΁A�ǂ�����ƃn�[�g�^�ł��ˁE�E�E");

                            UpdateMainMessage("���F���[�F�n�[�g�^�͐l�X�̊����傫���h���Ԃ�܂��B���̂ł��傤�ˁH");

                            UpdateMainMessage("���i�F�S���̌`�݂��������炶��Ȃ�������H���ƂȂ�������ۂ��`���Ă邵�B");

                            UpdateMainMessage("���F���[�F���̌`�A���݂��̂��̂��E�E�E�@�E�E�E�@����A�z�����邩��ł��B");

                            UpdateMainMessage("�A�C���F�i�ق�݂�A����ς�ʖڂ���˂����A���i�I�ǂ����񂾂�H�j");

                            UpdateMainMessage("���i�F�i�܂����ĂȂ������āA�����͗\�����Ă��񂾂���B���ɖ߂��Ă݂����j");

                            UpdateMainMessage("�A�C���F�i�}�W����I�H���O���ł���ȂƂ���˂�����ł񂾂�B�j");

                            UpdateMainMessage("���i�F���F���[����A�����Ă���Ǝv���܂��H");

                            UpdateMainMessage("���F���[�F������ł���E�E�E���́A���݂��܂���B");

                            UpdateMainMessage("���i�F�����H");

                            UpdateMainMessage("���F���[�F���݂��Ă����炨�������ł��傤�H����𗧏؂���Έ��́A���ł͂Ȃ��Ȃ�B");

                            UpdateMainMessage("���i�F���A�ł����̃n�[�g�^�͈���A�z�������ł��傤�H");

                            UpdateMainMessage("���F���[�F�n�[�g�^�͈����v���N�������܂��B�������A���̓n�[�g�ł͂���܂���B");

                            UpdateMainMessage("���i�F���ȁE�E�E�ł��A���F���[����͈������݂��Ȃ��Ă��ǂ��́H");

                            UpdateMainMessage("���F���[�F���͌`�ł��C���[�W�ł��A�p�ł��A���҂ɂ��u���������Ȃ���ł��B");

                            UpdateMainMessage("���F���[�F�u��������ꂽ��E�E�E���́E�E�E�����ŏ��ł��܂�");

                            UpdateMainMessage("���i�F�i�E�E�E�b�N�E�E�E�Ȃ��Ȃ��苭����ˁB�j");

                            UpdateMainMessage("�A�C���F�i�����A�ǂ�ǂ�G�X�J���[�g���Ă邶��˂����B�������̕ӂɂ��Ƃ����āj");

                            UpdateMainMessage("���i�F���́E�E�E�݂���I");

                            UpdateMainMessage("���F���[�F����A���݂��܂���E�E�E������ł���E�E�E�����炱�����ɂȂ��ł��B");

                            UpdateMainMessage("���i�F����I�@�������F���[����̂��΂ɋ��āA���������Ă������I");

                            UpdateMainMessage("�A�C���F�b�u�o�@�I�I�i�A�C���̓W���[�X��f�����j");

                            UpdateMainMessage("���F���[�F�E�E�E�{���ɋ��Ă���܂����H�Z�t�B�[�l�B");

                            UpdateMainMessage("���i�F�����A�����Ƃ��΂ɋ��Ă������I");

                            UpdateMainMessage("���i�F���āA����I�I�H�H�@�Z�t�B�[�l���ĒN��I�H");

                            UpdateMainMessage("���F���[�F���̓�����E�E�E�܂�ōŏ����瑶�݂��Ă����̂悤�ɋ����A�i�^��");

                            UpdateMainMessage("���F���[�F���̓�����E�E�E�����n�[�u�e�B�ł��ĂȂ��Ă��ꂽ�A�i�^��");

                            UpdateMainMessage("���F���[�F���̓�����E�E�E�����Y��ȃn�[�g�̎�����t���Ă����A�i�^��");

                            UpdateMainMessage("���F���[�F���̓�����E�E�E�����Ƒ������X�����Ŗ��߂Ă��ꂽ�A�i�^��");

                            UpdateMainMessage("���i�F�i���܂�����E�E�E�n�[�g�̎����A���S�Ƀr���S�ˁE�E�E�j");

                            UpdateMainMessage("�A�C���F�i������~�߂Ƃ����Č���������˂����j");

                            UpdateMainMessage("���F���[�F�����A���Ȃ��͂��Ȃ��E�E�E�S�Ă������A�����Č��z�B�䂦�Ɉ������܂��B");

                            UpdateMainMessage("���F���[�F�Z�t�B�[�l�E�E�E�{�N�͂����ŉi����");

                            UpdateMainMessage("���i�F���E�E�E�ʖڂ݂����ˁB");

                            UpdateMainMessage("�A�C���F���F���[�̃��c�A�󒆂ɉ������̂�������z�V�[����`���Ă����B");

                            UpdateMainMessage("�A�C���F�r������́A���i�̕��ɑS���ڂ������Ė�����������˂����B");

                            UpdateMainMessage("�A�C���F���i�A�Ƃɂ������B���F���[�ɑ΂��Ă͍ő���ɒ��肩�����ɋC��z��B");

                            UpdateMainMessage("�A�C���F�w���̎�Ɋ֘A����x���t�A���A���͋C�A�C���[�W��A�z��������͍̂T����A�ǂ��ȁH");

                            UpdateMainMessage("���i�F���[�����������B�B�B�ł��A�����������̂�����ˁB");

                            UpdateMainMessage("�A�C���F�y�T�����z�@���������ȁH");

                            UpdateMainMessage("���i�F�����������E�E�E");

                            md.Message = "�����F���[�͉����K���ł��܂���ł�����";
                            md.ShowDialog();
                            we.AlreadyLvUpEmpty33 = true;
                        }
                        if (tc.Level >= 30 && !tc.Resurrection)
                        {
                            UpdateMainMessage("���F���[�F���܂�����B���ɂk�u�R�O�ɓ��B�ł��B");

                            UpdateMainMessage("�A�C���F������ȁA���F���[�B�k�u�R�O�͋L�O�Ɉꔭ���ʂȂ̂��v���o���������H");

                            UpdateMainMessage("���F���[�F�����ł��ˁA�R���Ƃ����Ďv����������̂͂���܂��񂪁B");

                            UpdateMainMessage("���F���[�F�����A�Ƃ��Ă����̗ǂ��̂�����܂��ˁA����ɂ��܂��傤�B");

                            UpdateMainMessage("�A�C���F������ĉ����H");

                            UpdateMainMessage("���F���[�F�A�C���N���k�u�R�O�ŏK������Resurrection�ł��B");

                            UpdateMainMessage("�A�C���F�}�W����I�H���F���[�{���ɉ��ł����Ȃ���񂾂ȁB����ς��������I");

                            UpdateMainMessage("���i�F�o�ɂɈʒu���鑮���͂ǂ�����ė����Ƃ��K�����Ă��ł����H");

                            UpdateMainMessage("���F���[�F�{�����������Ƃ�������͊o���Ă��܂����H");

                            UpdateMainMessage("���i�F���`��A���ƂȂ������ǁB���߂�Ȃ����A�ǂ��̂Ɋo���Ă��Ȃ���B");

                            UpdateMainMessage("���F���[�F�����ł��ˁAFrozenLance��FlameStrike���ɂƂ��Ă݂܂��傤�B");

                            UpdateMainMessage("���F���[�FFlameStrike������͉΂̃C���[�W�����܂���ˁH");

                            UpdateMainMessage("�A�C���F�����A�������ȁB");

                            UpdateMainMessage("���F���[�F���̃C���[�W�̂܂ܕ��ĂΓ��R�����FlameStrike�ƂȂ�܂��B");

                            UpdateMainMessage("���F���[�F���i����AFrozenLance������͐��������͕X�̃C���[�W�ł���ˁH");

                            UpdateMainMessage("���i�F�����A�����ˁB");

                            UpdateMainMessage("���F���[�F����������菇�ł��B���āA�v�͂��̎菇���ň����������^���܂��B");

                            UpdateMainMessage("�A�C���F���������H");

                            UpdateMainMessage("���F���[�F�͂��A�A�C���N�̏ꍇ�͉΁A���i����̏ꍇ�͐��B�r���܂ł͑S�������ŗǂ��ł��B");

                            UpdateMainMessage("���F���[�F�r�����ł́A���΂̑�����ł���������悤�����C���[�W���܂��B");

                            UpdateMainMessage("���F���[�F�����Č��ʔ������ɂ����āA�ł��������Ǝv�������̂ق��������C���[�W���Ă��������B");

                            UpdateMainMessage("���F���[�F���̏u�ԁA��������閂�@���o�ɂɈʒu���閂�@��������܂��B");

                            UpdateMainMessage("�A�C���F��E�E�E�E����E�E�E�E�����������E�E�E�E");

                            UpdateMainMessage("�A�C���F�ʖڂ��I��Ȃ̖�������I�H");

                            UpdateMainMessage("���i�F�����E�E�E����ȊȒP�ɂ͏o���Ȃ���ˁB");

                            UpdateMainMessage("���F���[�F���̂����̓J�[���݂��狳���Ă���������̂ł��B�ނȂ�����Ə�肭�����Ă���܂���B");

                            UpdateMainMessage("���F���[�F���U���N�V�������ǂ��炩�Ƃ����΁A�Ŗ��@�̔��΂��C���[�W���ă{�N�͕����Ă��܂��B");

                            UpdateMainMessage("���F���[�F���ʂ͓����Ȃ̂ŁA�A�C���N�Ə����r�����@���Ⴂ�܂����C�ɂ��Ȃ��ł��������B");

                            UpdateMainMessage("�A�C���F���₠�E�E�E�������͏o���˂���������o����悤�ɂȂ��Ă݂��邺�B");

                            UpdateMainMessage("���F���[�F�����A���̈ӋC�ł��B����΂��ĉ��x���{�s���Ă݂Ă��������B");

                            md.Message = "�����F���[�̓��U���N�V�������K�����܂�����";
                            md.ShowDialog();
                            tc.Resurrection = true;
                        }
                        if (tc.Level >= 31 && !tc.CarnageRush)
                        {
                            UpdateMainMessage("���F���[�F�k�u�R�P�܂ŗ��܂����ˁB");

                            UpdateMainMessage("���i�F���F���[������k�u�R�O���炢���猋�\���蒲�q�ɂȂ肻���ł����H");

                            UpdateMainMessage("���F���[�F�����ł��ˁA���낢��Ǝv��������߂��o�Ă��܂�����B");

                            UpdateMainMessage("���F���[�F�A�C���N�ƁA�ŏ��̍��b�����Ă������̂�����Ă݂܂��傤�B");

                            UpdateMainMessage("�A�C���F�܂����Ƃ͎v�����E�E�E");

                            UpdateMainMessage("���F���[�F���i����AOneImmunity�𒣂��Ă����Ă��������B");

                            UpdateMainMessage("���i�F���H�����A�ǂ����B");

                            UpdateMainMessage("�@�@�@�w���i��OneImmunity�𔭓������A�h��̍\����������B�x");

                            UpdateMainMessage("���i�F�n�C�A�����������");

                            UpdateMainMessage("���F���[�FCarnageRush�ł��A�@�P��");

                            UpdateMainMessage("���F���[�F�Q�A�R�C�S�E�E�E�����ĂT�I�n�A�A�@�@�@�I�I�I");

                            UpdateMainMessage("�@�@�@�w�K�b�A�K�K�K�K�A�A�@�@�I�I�I�x");

                            UpdateMainMessage("���i�F�E�E�E���A������E�E�E");

                            UpdateMainMessage("���F���[�F���̃X�L�����{�N�̂��C�ɓ���̈�ł��ˁB");

                            UpdateMainMessage("���F���[�F�X�L���ʂ̓o�J�ɂȂ�܂��񂪁A�T��A���U����CarnageRush�B");

                            UpdateMainMessage("���F���[�FInnerInspiration�����܂����p���Ă����΁A�ō��̃o�����X�ł��B");

                            UpdateMainMessage("���i�F�Ȃ񂾂��������S�R�����Ȃ�������BOneImmunity����Ȃ��Ɩh���悤���Ȃ���ˁB");

                            UpdateMainMessage("�A�C���F���̃_�u���X���b�V���̉��p�݂����Ȃ��̂��H");

                            UpdateMainMessage("���F���[�F�����ł��ˁA�ł������Ⴂ�܂��B");

                            UpdateMainMessage("���F���[�F�A���U���̗v�f�Ƃ��Ă͑傫�������ĂR����܂��B");

                            UpdateMainMessage("���F���[�F���̒��ł��ł��傫���v�f�Ƃ��Ă�");

                            UpdateMainMessage("�A�C���F����E�E�E�����B���Ȃ�̂��̂�҂ݏo���Č�����B");

                            UpdateMainMessage("���i�F�������������̂���Ă݂����̂�ˁA���F���[���񍡓x�����Ă���������");

                            UpdateMainMessage("���F���[�F�����A�ǂ��ł���B���ł������Ă��������B");

                            md.Message = "�����F���[�̓J���l�[�W�E���b�V�����K�����܂�����";
                            md.ShowDialog();
                            tc.CarnageRush = true;
                        }
                        if (tc.Level >= 32 && !tc.Catastrophe)
                        {
                            UpdateMainMessage("���F���[�F�k�u�R�Q�ɂȂ�܂����B���悢��k�u�S�O�������Ă��܂����ˁB");

                            UpdateMainMessage("�A�C���F���F���[�͂����k�u�R�O���炢���l�`�w���Ď��ɂ��˂����H");

                            UpdateMainMessage("���F���[�F�n�n�n�A�ʔ�����k�ł��ˁB�������ɂ���͏o���܂���B");

                            UpdateMainMessage("�A�C���F����͂ǂ�Ȃ̂������Ă����񂾁H");

                            UpdateMainMessage("���F���[�F�A�C���N�ɈȑO�ԈႦ�Ă���Ă��܂�����������܂��B");

                            UpdateMainMessage("�A�C���F�Ԉ���Ă���Ă��܂����E�E�E�H");

                            UpdateMainMessage("���F���[�F�����ACatastrophe�ł��B�A�C���N�A�����܂���A�󂯂Ă݂Ă��������B");

                            UpdateMainMessage("�A�C���F���ȁI�H�����Ȃ肩��I�I");

                            UpdateMainMessage("�@�@�@�w�b�h�X�E�E�D�D�D���I�I�I�x�@�@");

                            UpdateMainMessage("�A�C���F�b�t�E�D�`�`�`�E�E�E�Ԉꔯ���������B");

                            UpdateMainMessage("���i�F���A�A�C���Ђ���Ƃ��č��̎󂯎~�߂�ꂽ�́H");

                            UpdateMainMessage("�A�C���F����A�_���[�W���̂͐H����Ă�A�����������x���̃_���[�W�͉���ł����B");

                            UpdateMainMessage("���F���[�F���āA��͂�A�C���N�̓Z���X���ǂ��ł��ˁB");

                            UpdateMainMessage("���F���[�F�v���o�����n�߂Ƃ͌����A���ꂾ���󂯎~�߂�ꂽ�͈̂ӊO�ł��B");

                            UpdateMainMessage("�A�C���F���F���[�̓����͂���������������ȁB����ɍ��킹��悤�ɂ��Ă݂��������B");

                            UpdateMainMessage("���F���[�F�E�E�E�n�n�A�n�n�n�n�n�I�������A�C���N�ł��B�������Ă����ƁA�ƂĂ��������ł��B");

                            UpdateMainMessage("���F���[�F�A�C���N�A���͂����ƈႤ�J�^�X�g���t�B��҂ݏo����悤���K���Ă����܂��B");

                            UpdateMainMessage("���F���[�F���̎��܂ŃA�C���N���b�B��ӂ�Ȃ��悤���肢���܂��B");

                            UpdateMainMessage("�A�C���F���A�����I�C���Ă����I�H");

                            md.Message = "�����F���[�̓J�^�X�g���t�B���K�����܂�����";
                            md.ShowDialog();
                            tc.Catastrophe = true;
                        }
                        if (tc.Level >= 33 && !tc.Genesis)
                        {
                            UpdateMainMessage("���F���[�F�k�u�R�R�ł��ˁB���̕ӂ���͒��X�オ��h�����������Ă��܂��B");

                            UpdateMainMessage("�A�C���F���F���[������Ɨǂ����I�@�@�@�@�@���i�F�˂��A���F���[����H");

                            UpdateMainMessage("���F���[�F�͂��A���ł��傤�H�Q�l�Ƃ��B");

                            UpdateMainMessage("�A�C���F���i�A���O��ŗǂ����B");

                            UpdateMainMessage("���i�F�����H���Ⴀ�����Ȃ���@���F���[����ɖ��@�r�����@���K�������́B");

                            UpdateMainMessage("���i�F���F���[����A���񂾂��܂��������Ɖr�����Ă��炦�Ȃ�������H");

                            UpdateMainMessage("���F���[�F�����A�������ǂ��ł���B����̂�Genesis�ł�����������ł��\���܂���ˁB");

                            UpdateMainMessage("���F���[�F�ł́A����t���ōs���܂��B�܂��r�����̏������[�V�����B");

                            UpdateMainMessage("���F���[�F�����ł͉r���Ɠ����ɗ���̖��`���܂��B");

                            UpdateMainMessage("���i�F�҂��āA��������������̂�B���߂�Ȃ����A������񂨊肢�B");

                            UpdateMainMessage("���F���[�F�ǂ��ł���B��������āE�E�E�����E�E�E�ŁE�E�E�����ł��ˁB");

                            UpdateMainMessage("�A�C���F�������A����ȕ��ɂ���Ă��̂��B�S�R������Ȃ��������B");

                            UpdateMainMessage("���F���[�F�ŁA�����^�[�Q�b�g�I��Ɣ������[�V�����ł����A");

                            UpdateMainMessage("���F���[�F�������^�[�Q�b�g�I��ő̂�������ۂɁA�������[�V�����𓯎��ɌJ��o���܂��B");

                            UpdateMainMessage("���i�F�������҂��āB���߂�Ȃ����ˁA����~�߂�����āB");

                            UpdateMainMessage("���F���[�F�\���܂����B���̒��x�̓���Ȃ�x��͂���܂���B");

                            UpdateMainMessage("���i�F�������[�V�������Đ�Η������Œ肳������ˁH");

                            UpdateMainMessage("���F���[�F����Ȏ��͂���܂����B");

                            UpdateMainMessage("���i�F���H�H");

                            UpdateMainMessage("���F���[�F����͎v�����݂ł��B�m���ɔ������[�V�������͗������Œ肳�������ł���");

                            UpdateMainMessage("���F���[�F�������J�n����钼�O�܂ł͓������Ă����̉e��������܂���B");

                            UpdateMainMessage("���F���[�F�Ȃ̂ŁA�^�[�Q�b�g�I�莞�ɔ������[�V�������J��L����Ηǂ���ł���B");

                            UpdateMainMessage("���i�F�ւ��E�E�E�����������񂾁B���S�R�m��Ȃ������킻��Ȃ́B");

                            UpdateMainMessage("���F���[�F�Ō�̔������ł����A�R�R���|�C���g������܂��B");

                            UpdateMainMessage("���F���[�F���ۂ̔����܂ł̃^�C�����O���k�߂邽�߂ɂ͎�������ł��O�ɏo���Ă��������B");

                            UpdateMainMessage("���F���[�F�l�ɂ��܂����A�{�N�̏ꍇ�A���̔������[�V�����̍Ōギ�炢�ŏo���Ă����܂��B");

                            UpdateMainMessage("���F���[�F�����āE�E�E�����E�E�E�ł����ˁB���悢�攭���ł��ˁAGenesis�ł��B");

                            UpdateMainMessage("���F���[�F������FireBall�ł��B");

                            UpdateMainMessage("���F���[�F����ŉ���ł�FireBall��A���r���ɂȂ�܂��B�y�ŗǂ��ł���ˁA���̖��@�B");

                            UpdateMainMessage("�A�C���F�E�E�E�������ȁB");

                            UpdateMainMessage("���i�F���`��A�����Ă�������̂͗ǂ��񂾂��ǁA�C�}�C�`������Ȃ����ˁB");

                            UpdateMainMessage("���i�F���̂�S��������Ƃ��Ă�����ȃX�s�[�h�ɂ͂Ȃ�Ȃ��Ǝv���񂾂��ǁB");

                            UpdateMainMessage("�A�C���F�m���ɂ������B���̂�S������Ă����F���[�̒ʂ�ɂ͂Ȃ�˂��B");

                            UpdateMainMessage("���F���[�F�l�ɂ��܂��B���낢�뎎���Ă݂āA��ԃ^�C�~���O�������̂�T���Ă݂Ă��������B");

                            UpdateMainMessage("���i�F���A���肪�ƃ��F���[�����@�܂����x�����Ă��������B");

                            UpdateMainMessage("���F���[�F�����A���ł��ǂ����B�@���āA�A�C���N�̕��͉���������ł��傤�H");

                            UpdateMainMessage("�A�C���F����A���͂����B�܂��̋@��ɂł������Ă݂邺�B");

                            UpdateMainMessage("���F���[�F�����ł����B�ł͂܂����̋@��ŁB");

                            md.Message = "�����F���[�̓W�F�l�V�X���K�����܂�����";
                            md.ShowDialog();
                            tc.Genesis = true;
                        }
                        if (tc.Level >= 34 && !we.AlreadyLvUpEmpty34)
                        {
                            UpdateMainMessage("���F���[�F�k�u�R�S�ł��ˁB�R�S�Ƃ����΃A�C���N�A����Ȃ̂�m���Ă܂����H");

                            UpdateMainMessage("�A�C���F��H�������H");

                            UpdateMainMessage("�@�@�@�w���F���[�͂����ނ�ɖ_�̂���͂��Œn�ʂɉ����`���n�߂��x");

                            UpdateMainMessage("�@�@�@�w�l�p�`��`������A�����l�������Ă���悤���x");

                            UpdateMainMessage("���F���[�F�����w�Ƃ������̂ł��B�^�e�ƃ��R�̘a���K����v���Ă���Ί����ł��B");

                            UpdateMainMessage("�A�C���F���R�ƃ^�e�������a�H");

                            UpdateMainMessage("���F���[�F�͂��A����Ŋ����ł��B");

                            UpdateMainMessage("���i�F���A�A�����Y��˂���B�����w�Č����́H");

                            UpdateMainMessage("���F���[�F���݂��̐��l�����݂��̘a��m���Ă��邩�炱�̐��l�ɂȂ�܂��B");

                            UpdateMainMessage("���F���[�F�P����P�U�܂ł��g���^�C�v�ł́A�S���S�̎l�p�`�̏ꍇ�A���v�̘a�͂R�S�ł��B");

                            UpdateMainMessage("�A�C���F���ꂪ��̉��ɂȂ���Ă񂾁H");

                            UpdateMainMessage("���F���[�F��������܂���B�ړI�����������A���������ɑ��݂����ł��B");

                            UpdateMainMessage("���F���[�F�u�����ȑ��݁v�E�E�E�����ɂ͑��݂��������葱�����ł��E�E�E�Y��ł����");

                            UpdateMainMessage("���F���[�F���i������������u�Y�킳�v���������ɂ���܂��B");

                            UpdateMainMessage("���F���[�F�A�i�^�́A����𖳎׋C�ɕ`�����Ƃ��ĉ��x�����s���Ă��܂����ˁB");

                            UpdateMainMessage("���F���[�F�A�i�^�́A���s�ɋC�Â��̂��ƂĂ��x���A���s���Ă���΂��Ă܂����ˁB");

                            UpdateMainMessage("���F���[�F�A�i�^�́A�S�����������Ɉ������Ă����B�ƂĂ����ʂȎ��Ԃł����B");

                            UpdateMainMessage("���F���[�F�A�i�^�́A����𖳑ʂȎ��ԂƂ͌Ăт܂���ł����ˁA�����i���Ɋy�������Ԃ��ƁE�E�E");

                            UpdateMainMessage("�A�C���F�i�}�Y�C�B�@�A�i�^�A�ł��n�܂������E�E�E�ǂ����郉�i�H�j");

                            UpdateMainMessage("���i�F�i�ǂ����������������B�@�����W�E�G���h����Ȃ��̂悱��E�E�E�j");

                            UpdateMainMessage("���F���[�F�Z�t�B�[�l�A�{�N���A�i�^�ƈꏏ�ɂ��閳�ʂȎ��Ԃ��i���̊y�����ɕς��܂����B");

                            UpdateMainMessage("���F���[�F�Z�t�B�[�l�A�ǂ����ď΂��Ă���̂��A�{�N�ɂ͑S�R�������Ă��܂���ł����ˁB");

                            UpdateMainMessage("���F���[�F�Z�t�B�[�l�A�����ȑ��݂��̂��̂��߂�������ł��E�E�E�����E�E�E");

                            UpdateMainMessage("���i�F���F���[����A���́`�����v�����܂��H");

                            UpdateMainMessage("�A�C���F�i���i�A���͂△����肾�E�E�E�j");

                            UpdateMainMessage("���F���[�F�����A�v�����܂�����B");

                            UpdateMainMessage("���i�F��������ǂ�Ȃ̂��v�����܂����H");

                            UpdateMainMessage("���F���[�F�����̐������Q�����Ă݂āA���������Ă݂Ă͂ǂ��ł��傤���H");

                            UpdateMainMessage("���F���[�F��������ƃz���A������Ƃ����炪�R�S�ɂȂ�܂���ˁB");

                            UpdateMainMessage("���F���[�F�y�����ł��ˁE�E�E���̊ԈႦ���͖{���ɍ߂ł��E�E�E");

                            UpdateMainMessage("���i�F�E�E�E�@�E�E�E�@�ǁ[����̂�R���I�I�I�H�H�H");

                            UpdateMainMessage("�A�C���F�u�����w�v�Ƃ��u�Y��v���^�u�[���ȁB");

                            UpdateMainMessage("���i�F���F���[����I���̂܂�܂���ʖڂ�I�H");

                            UpdateMainMessage("���F���[�F�n�n�n�A�����܂���B�{�点�Ă��܂��܂����ˁB");

                            UpdateMainMessage("���F���[�F�{�����Z�t�B�[�l���Y��ł��B");

                            UpdateMainMessage("���i�F���`�����������������@���`���󂩂Ȃ���I�I");

                            UpdateMainMessage("�A�C���F���A�������i�B�B�B�ʂɗǂ�����˂����B");

                            UpdateMainMessage("���i�F�C�C���P�����ł���I�H�����K�����Ȃ��̂ɉ��Ȃ̂�R���́I�H");

                            UpdateMainMessage("���i�F���ɂ��́w���S���ɃA�b�`�����[�h�x���������ꂽ�玄���~�߂Ă݂����I�I�I");

                            UpdateMainMessage("�A�C���F�E�E�E�I�[�P�[�I�[�P�[�E�E�E���ቴ�̓R���ŁE�E�E");

                            UpdateMainMessage("���i�F�A�C�������͂��Ă�ˁI�H�[�b�^�C������I�I�I");

                            UpdateMainMessage("�A�C���F�}�W����E�E�E���𗹉��E�E�E");

                            md.Message = "�����F���[�͂Ȃɂ��K���ł��܂���ł�����";
                            md.ShowDialog();
                            we.AlreadyLvUpEmpty34 = true;
                        }
                        if (tc.Level >= 35 && !tc.ImmortalRave)
                        {
                            UpdateMainMessage("���F���[�F�k�u�R�T�ɂȂ�܂����B���悢��k�u�S�O���������������Ă��܂����B");

                            UpdateMainMessage("�A�C���F���F���[������Ɨǂ����I�H");

                            UpdateMainMessage("���F���[�F���������ΑO�񉽂��������Ƃ��Ă܂����ˁB���ł��傤�H");

                            UpdateMainMessage("�A�C���F���̓��i�Ɠ������₾�����񂾁B�r�����������ƌ����Ăق����ĂȁB");

                            UpdateMainMessage("���F���[�F����ň�U��߂Ēu������ł��ˁB�ǂ��ł��扽�x�ł����������܂��B");

                            UpdateMainMessage("�A�C���F�����A��낵�����ނ��B");

                            UpdateMainMessage("���F���[�F����v�����Ă���̂́AImmortalRave�ł��ˁB�ł͂���Ă݂܂��傤�B");

                            UpdateMainMessage("���F���[�F�܂��������[�V�����ł���");

                            UpdateMainMessage("�A�C���F�҂��Ă���B���̖��@�A���������^�C�~���O����ꏏ�ɂ���Ă݂�B");

                            UpdateMainMessage("�A�C���F���i�A�������猩�ĂĂ���B");

                            UpdateMainMessage("���i�F�����A�ǂ�����");

                            UpdateMainMessage("���F���[�F�܂��������[�V�������痈�āE�E�E");

                            UpdateMainMessage("�@�@�@�w���F���[���������Ɖ������ԁA�A�C���͂���ɒǂ����`�ŉr������x");

                            UpdateMainMessage("���F���[�F�b�n�C�AImmortailRave�ł��B�@�@�@�@�A�C���F�b���@�I�C���[�^�����C�u�I�I");

                            UpdateMainMessage("���i�F���ւ��A�A�C���ӊO�ƒǂ����Ă��邶��Ȃ��B");

                            UpdateMainMessage("�A�C���F���F���[������t���ŃK�C�h���Ă���Ă邩��ȁB����ɒǂ����΂����킯���B");

                            UpdateMainMessage("���i�F�ł����A������ƋC�Â����񂾂��ǁB");

                            UpdateMainMessage("���i�F�o�J�A�C���̉r�����Ė{���Ƀ��F���[����Ɠ��������S�Ɉ���Ă���H");

                            UpdateMainMessage("���i�F�m���ɍŌ�̃^�C�~���O�̓o�b�`�����������ǂˁE�E�E�ǂ��������Ȃ̂�����H");

                            UpdateMainMessage("���F���[�F�l�ɂ���čו��͈���Ă��܂��A�������̉e���ł��傤�B");

                            UpdateMainMessage("���i�F����ɂ��Ă������ٗl�Ȃ��炢�^�t�ňႤ�̂�ˁB");

                            UpdateMainMessage("�A�C���F�C�ɂ���������˂��̂��H���F���[������������ƃ^�C�~���O�͍���Ȃ����ȁB");

                            UpdateMainMessage("���i�F�܂������Ȃ񂾂��ǁE�E�E���F���[����͉����v���Ƃ��날��܂��H");

                            UpdateMainMessage("���F���[�F�E�E�E���ɋC�ɂ͂Ȃ�܂���ˁB�l���͒N�ɂł�����܂�����B");

                            UpdateMainMessage("���i�F�Ӂ`��A���̊��Ⴂ�Ȃ̂��ȁB");

                            UpdateMainMessage("���F���[�F����ImmortalRave�͂R�^�[���̓��������܂��g���؂�̂��R�c�ł��B");

                            UpdateMainMessage("���F���[�F�A�C���N�A���x���݂��ɗ��K���Ă݂܂��傤�B");

                            UpdateMainMessage("�A�C���F�����A���������B�����������Ă���ăT���L���[�I");

                            UpdateMainMessage("���F���[�F���i������A�ʂ̖��@�ł��낢��Ǝ����Ă݂܂��傤�B");

                            UpdateMainMessage("���i�F���`��A���񂤂�B�킩�������");

                            md.Message = "�����F���[�̓C���[�^���E���C�u���K�����܂�����";
                            md.ShowDialog();
                            tc.ImmortalRave = true;
                        }
                        if (tc.Level >= 36 && !we.AlreadyLvUpEmpty35)
                        {
                            UpdateMainMessage("���F���[�F�k�u�R�U���B�ł��B��������͂������ɏオ��h���ł��ˁB");

                            UpdateMainMessage("�A�C���F���F���[�ł��h���Ǝv�����肷�鎖�͂���̂��H");

                            UpdateMainMessage("���F���[�F�����A����Ȃ�ɐh�����͂���܂���B");

                            UpdateMainMessage("���i�F���ł������ɂ��Ȃ����Ⴄ����A�����͌����Ȃ��̂�ˁB");

                            UpdateMainMessage("���F���[�F�����ɂ��Ȃ��Ă���킯�ł͂���܂����B");

                            UpdateMainMessage("���i�F���H�����Ȃ́H");

                            UpdateMainMessage("���F���[�F�͂��A�{�N�Ȃ񂩂܂��܂��ł���B");

                            UpdateMainMessage("���i�F��Ԑh���Ǝv�����̂͂ǂ�Ȏ��������H");

                            UpdateMainMessage("�A�C���F�i�����A���i�I�j");

                            UpdateMainMessage("���i�F���H����A�C���B");

                            UpdateMainMessage("���F���[�F�ǂ���ł���A�����͈��������܂���B");

                            UpdateMainMessage("���F���[�F�Z�t�B�[�l���������������ł��B");

                            UpdateMainMessage("���i�F���E�E�E���߂�Ȃ����E�E�E�E�E�E");

                            UpdateMainMessage("���F���[�F�C�ɂ��Ȃ��ł��������B�ߋ��̏o�����ł��B");

                            UpdateMainMessage("���F���[�F�Z�t�B�[�l�B�{�N�̍߂́E�E�E");

                            UpdateMainMessage("���i�F�i�E�E�E�悵�A������ˁE�E�E�w���S���ɃA�b�`�����[�h�x�j");

                            UpdateMainMessage("�A�C���F�i�܂����A���O���U�Ƃ���H�j");

                            UpdateMainMessage("���i�F�i������O����Ȃ��H�A�C���A���񂽂����͂��Ȃ�����j");

                            UpdateMainMessage("�A�C���F�i�������A���O���Ƃ񂾕��D�����ȁE�E�E�j");

                            UpdateMainMessage("���i�F���F���[����A�����������Ă��������B");

                            UpdateMainMessage("���F���[�F�{�N�̍߂́E�E�E�{�N�������߂������ł����ˁB");

                            UpdateMainMessage("�A�C���F�������F���[�B�Z�t�B�[�l���Ă�ł邼�B");

                            UpdateMainMessage("���F���[�F�H");

                            UpdateMainMessage("�Z�t�B�[�l�i���i�j�F���F���[����A�����������Ă��������B");

                            UpdateMainMessage("���F���[�F�Z�t�B�E�E�E�Z�t�B�ł����I�H");

                            UpdateMainMessage("�Z�t�B�[�l�i���i�j�F�����A�v���Ԃ�ˁA���F���[����B");

                            UpdateMainMessage("���F���[�F�Z�t�B�E�E�E�v���Ԃ�ł��B���C�ł������H");

                            UpdateMainMessage("�A�C���F�i���O���A�Z�t�B�[�l���ď����̎��m���Ă�̂���H�j");

                            UpdateMainMessage("���i�F�i���A�m���Ă�킯�Ȃ�����Ȃ��B�K���悱��Ȃ̂́j");

                            UpdateMainMessage("�Z�t�B�[�l�i���i�j�F���F���[����A���^�V�̎��o���Ă��Ă��ꂽ��ł��ˁB");

                            UpdateMainMessage("���F���[�F���R����Ȃ��ł����B������Y�ꂽ���Ȃǂ���܂���B");

                            UpdateMainMessage("�Z�t�B�[�l�i���i�j�F���^�V�����F���[����̂��ƁA�����Ɗo���Ă����B");

                            UpdateMainMessage("���i�F�i���܂�����E�E�E�����Ɍ�����E�E�E���c�J�V�C��ˁj");

                            UpdateMainMessage("�A�C���F�i����Ȃ�ŗǂ�����ȁE�E�E�j");

                            UpdateMainMessage("���F���[�F�Z�t�B�����Ƃ��{�N��Y��Ă��A�{�N�̓Z�t�B��Y��܂���B");

                            UpdateMainMessage("�Z�t�B�[�l�i���i�j�F���F���[����A�܂����̎��݂����ɖ����w�`���Ă��炦��H");

                            UpdateMainMessage("���F���[�F�����A�������ł���B�傫���͂S���S�ɂ��܂��傤�B");

                            UpdateMainMessage("�@�@�@�w���F���[�͂����ނ�Ƀ��i�ׂ̗ɍ���A�Y��Ȗ����w��`���n�߂��x");

                            UpdateMainMessage("�Z�t�B�[�l�i���i�j�F���[�A������������Ȃ犮������Ǝv�����̂ɁE�E�E");

                            UpdateMainMessage("���F���[�F�n�n�n�A�Z�t�B�B����ł͊������܂����B");

                            UpdateMainMessage("���F���[�F�p�̑΋ɂɈʒu����ꏊ�ɏo�������o�����X�̗ǂ����l��z�u�����ł��B");

                            UpdateMainMessage("���F���[�F�ق�A��������āE�E�E");

                            UpdateMainMessage("�Z�t�B�[�l�i���i�j�F���I�_���I���^�V������������́I�I");

                            UpdateMainMessage("���F���[�F�A�b�n�n�n�A�ǂ��ł���B�D���Ȃ��������Ă݂Ă��������B");

                            UpdateMainMessage("�@�@�@�w�����āA���F���[�ƃ��i�i�Z�t�B�[�l���j�̊y�����ꎞ���߂����x");

                            UpdateMainMessage("�A�C���F���F���[�A�Z�t�B�[�l�ɓ`���Y�ꂽ���A�����������񂶂�˂��̂��H");

                            UpdateMainMessage("���F���[�F�E�E�E�Z�t�B�A�����Ă���܂����H");

                            UpdateMainMessage("�Z�t�B�[�l�i���i�j�F���H");

                            UpdateMainMessage("���F���[�F�Z�t�B�[�l�B�{�N�̓����O���̃_���W�����ɒ������Ǝv���܂��B");

                            UpdateMainMessage("�A�C���F���ȁI�H");

                            UpdateMainMessage("���F���[�F�Z�t�B�[�l�B���̎��A�{�N�̓A�i�^�Ƃ��΂炭��Ȃ��Ȃ�܂��B");

                            UpdateMainMessage("���F���[�F�_���W��������A���ė���܂ł̊ԁA�҂��Ă��Ă��炦�܂����H");

                            UpdateMainMessage("�Z�t�B�[�l�i���i�j�F�E�E�E�@�E�E�E�@�E�E�E");

                            UpdateMainMessage("�A�C���F�i�������i�I�I�|�J�[���Ƃ��Ă񂶂�˂���I�H�j");

                            UpdateMainMessage("�Z�t�B�[�l�i���i�j�F����A���肪�Ƃ��B�҂��Ă����B�񑩂ˁ�");

                            UpdateMainMessage("���F���[�F�Z�t�B�A����̓{�N����̑��蕨�ł��B�󂯎���Ă��������B");

                            UpdateMainMessage("�@�@�@�w���i���ȑO�n�����n�[�g�^�̃y���_���g����n���ꂽ�B�x");

                            UpdateMainMessage("�Z�t�B�[�l�i���i�j�F���肪�Ƃ��A�n�[�g�^�͑�D����B�{���ɂ��肪�Ƃ��B");

                            UpdateMainMessage("���F���[�F�Z�t�B�A���Ⴀ�{�N�͍s���Ă��܂��B�K���A���Ă��܂����炻�̎��܂ŁE�E�E");

                            UpdateMainMessage("�Z�t�B�[�l�i���i�j�F����A�K���E�E�E");

                            UpdateMainMessage("�@�@�@�w���F���[�͂��̏�ňӎ����������B�x");

                            UpdateMainMessage("���i�F�E�E�E�@�E�E�E");

                            UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                            UpdateMainMessage("���i�F�E�E�E");

                            UpdateMainMessage("�A�C���F�E�E�E");

                            md.Message = "�����F���[�͉����K���ł��܂���ł�����";
                            md.ShowDialog();
                            we.AlreadyLvUpEmpty35 = true;
                        }
                        if (tc.Level >= 37 && !tc.AbsoluteZero)
                        {
                            UpdateMainMessage("���F���[�F�k�u�R�V�ł��ˁB�����܂ł���Ƃk�u�S�O���҂��������ł��B");

                            UpdateMainMessage("���i�F�b�t�t�A���F���[����ł��w�҂��������x�Ƃ����Ă����ł��ˁB");

                            UpdateMainMessage("���F���[�F�n�n�n�A����͂����ł���B���i����A�{�N�ɂ͖����Ƃł��v���Ă���ł����H");

                            UpdateMainMessage("���i�F�������ƁA�Ⴄ�́B�ق烔�F���[������Ă�����Â����B");

                            UpdateMainMessage("���F���[�F��Âł��肽���Ƃ͏�X�S�����Ă��܂��B���̂��������m��܂���B");

                            UpdateMainMessage("���F���[�F���������΁A��ÂŎv���o���܂����B���̖��@���{�N�̂��C�ɓ���ł��B");

                            UpdateMainMessage("���F���[�F�⌵�Ȃ�f�ߓV�g�AAbsoluteZero�ł��B");

                            UpdateMainMessage("�@�@�@�w�b�p�L�L�L�B�B�B�A�b�V���E�E�E�E�D�D�D�E�E�E�x");

                            UpdateMainMessage("�A�C���F�b�Q�I�����Ώۂ���I�H");

                            UpdateMainMessage("���F���[�F���̖��@�̓����͕�������܂����A�����̊T�O���p�����Ă��܂��A�����Ǝv���܂��H");

                            UpdateMainMessage("���i�F�o�J�A�C���𓀂�t���ɂ��鎖��");

                            UpdateMainMessage("���F���[�F�n�n�n�A���i����͖{���ɖʔ������������܂��ˁB");

                            UpdateMainMessage("���F���[�F���̖��@�́w�΍R�x�̊T�O���ɗ͏Ȃ����ɏW�񂳂�Ă��܂��B");

                            UpdateMainMessage("���i�F�w�΍R�x�H");

                            UpdateMainMessage("���F���[�F�����ł��B�Ⴆ�΂ł����A�A�C���N�A������Ƃ����܂���B");

                            UpdateMainMessage("�A�C���F�����҂āA�Ⴆ�Ȃ񂩗v��˂�����B");

                            UpdateMainMessage("���F���[�F�A�C���N�̏ꍇ�A�w�΍R�x�ɊY������̂͗B��StanceOfEyes�݂̂ł��B");

                            UpdateMainMessage("���F���[�F�������A���̔ނ͂��̍\������鎖���o���܂���B");

                            UpdateMainMessage("���F���[�F�܂�A���̔ނ͕K���C�ӂ̍s�����󂯂���𓾂܂���B");

                            UpdateMainMessage("���F���[�F�������h�䂷�鎖���܂܂Ȃ�܂��񂩂�A�΍R�̂��悤���Ȃ��킯�ł��B���āE�E�E");

                            UpdateMainMessage("�A�C���F�҂đ҂đ҂āE�E�E");

                            UpdateMainMessage("���F���[�F�n�n�n�A��k�ł��B�������ɍ���͕s�ӑł��������̂ŁA����ȏ�͉������܂���B");

                            UpdateMainMessage("���F���[�F�΍R���ӎ����邪�̂ɁA�������Ȃ���΁A�A�C���N���g�ɉ����N����܂���ˁB");

                            UpdateMainMessage("�A�C���F�ӂ��E�E�E�r�r�������A�z���g�B���̖��@�����͊��ق��Ăق������B");

                            UpdateMainMessage("�A�C���F���������Ă��炦�Ȃ�����ȁB�������h�͉B���؂�˂����ĂƂ����ȁB");

                            UpdateMainMessage("���i�F��x���܂��Ă��܂��΁A�m���ɂقƂ�ǖ���R���ĂƂ���ˁB");

                            UpdateMainMessage("���F���[�F���̖��@�͏o����A�h�R��Ԓ��A�g�h���ȂǑ����̋ǖʂŎg�p�ł��܂��B");

                            UpdateMainMessage("���F���[�F�G���h�q�I�ȏꍇ�Ƀ{�N�̕�����ϋɓI�Ɏg���Ă����Ƃ��܂��傤�B");

                            md.Message = "�����F���[�̓A�u�\�����[�g�E�[�����K�����܂�����";
                            md.ShowDialog();
                            tc.AbsoluteZero = true;
                        }
                        if (tc.Level >= 38 && !tc.CelestialNova)
                        {
                            UpdateMainMessage("���F���[�F�k�u�R�W�ɂȂ�܂����ˁB���悢���Q�Ɣ���܂����B");

                            UpdateMainMessage("�A�C���F���F���[�̂k�u�S�O���Ă̂͐����킢�����͂˂��ȁE�E�E");

                            UpdateMainMessage("���F���[�F�������A�c�t�d�k��ł͑��l�`�w���x���܂ŊF�グ�Ă��܂��ˁB");

                            UpdateMainMessage("�A�C���F�܁A�m���ɂ��������ǂȁB�ŁA�ǂ����H");

                            UpdateMainMessage("���F���[�F����́E�E�E�ǂ����CelestialNova�̂悤�ł��B����͗ǂ����@�ł��ˁB");

                            UpdateMainMessage("�A�C���F���́A�񕜂ƍU�������ł������ȁB�m���Ƀ\�C�c�͕֗����B");

                            UpdateMainMessage("���F���[�F���̖��@�͊m���������ł����ˁH");

                            UpdateMainMessage("�A�C���F�����A�������ȁB���ꂪ�����H");

                            UpdateMainMessage("���F���[�F���̖��@�͍U�����ɂ��g����̂ł�����ƈő����ƊԈႦ�₷���Ƃ͎v���܂���ł������H");

                            UpdateMainMessage("�A�C���F�������H���ɋC�ɂ͂Ȃ�Ȃ��������B");

                            UpdateMainMessage("���F���[�F�܂�����͂��Ă����A���i����B");

                            UpdateMainMessage("���i�F���H���H");

                            UpdateMainMessage("���F���[�F���i����͘A���I�ȉ񕜂����Ă��鑊��ɂ͂ǂ�����ׂ����Ǝv���܂����H");

                            UpdateMainMessage("���i�F���R�A����܂������");

                            UpdateMainMessage("�A�C���F�}�W����E�E�E�|����");

                            UpdateMainMessage("���i�F�����āA����ȊO��������Ȃ��B�A�C�������������ł���H");

                            UpdateMainMessage("�A�C���F�܂��������ȁB");

                            UpdateMainMessage("���F���[�F�t�ɍl���Ă݂Ă��������B���肪�U���΂��肵�Ă���ꍇ�͂ǂ����܂����H");

                            UpdateMainMessage("�A�C���F�܂����ȂȂ����x�ɍU�������C�t�񕜂��ȁB�_���[�W�ʂɂ���邪�B");

                            UpdateMainMessage("���F���[�F���̃��i����A�A�C���N�ł͖��������m��܂��񂪁A���������̂�����܂��B");

                            UpdateMainMessage("�@�@�@�w���F���[��CelestialNova�̖��@�̍\���������B�x");

                            UpdateMainMessage("���F���[�F�A�C���N�A������ƃ{�N�ɍU�����悤�Ƃ��Ă݂Ă��������B");

                            UpdateMainMessage("�A�C���F�ǂ��̂���H�����Ȃ��s�����B");

                            UpdateMainMessage("���F���[�F�����A�ǂ������\���Ȃ��B");

                            UpdateMainMessage("�A�C���F�I���@�I");

                            UpdateMainMessage("���F���[�FCelestialNova�ł��A�񕜂��܂�����B");

                            UpdateMainMessage("�A�C���F�I���@�I");

                            UpdateMainMessage("���F���[�FCelestialNova�ł��A�񕜂��܂�����B");

                            UpdateMainMessage("�A�C���F�ł�����͓��R����ȁB");

                            UpdateMainMessage("���F���[�F���āA�ǂ��ł����ˁB�����Ăǂ����B");

                            UpdateMainMessage("�A�C���F�I���@�I");

                            UpdateMainMessage("���F���[�FCelestialNova�A�A�C���N�֍U���ł��B");

                            UpdateMainMessage("�A�C���F�I���@�I");

                            UpdateMainMessage("���F���[�FCelestialNova�A�A�C���N�֍U���ł��B");

                            UpdateMainMessage("�A�C���F�I���@�I");

                            UpdateMainMessage("���F���[�FCelestialNova�ł��A�񕜂��܂�����B");

                            UpdateMainMessage("�A�C���F�I���@�I");

                            UpdateMainMessage("���F���[�FCelestialNova�A�A�C���N�֍U���ł��B");

                            UpdateMainMessage("�A�C���F�����ACelestialNova�A���C�t�񕜂������B");

                            UpdateMainMessage("���F���[�FCelestialNova�A�A�C���N�֍U���ł��B");

                            UpdateMainMessage("�A�C���F�����ACelestialNova�A���C�t�񕜂������B");

                            UpdateMainMessage("���F���[�FCelestialNova�ł��A�񕜂��܂�����B");

                            UpdateMainMessage("�A�C���F�I���@�I");

                            UpdateMainMessage("���F���[�FCelestialNova�A�A�C���N�֍U���ł��B");

                            UpdateMainMessage("�A�C���F���^�C���I");

                            UpdateMainMessage("�A�C���F���������E�E�E�����S�R�������ǂݐh���E�E�E�B");

                            UpdateMainMessage("���i�F���[��A�m���ɉ������A�C����������C����������ˁB���ł�H");

                            UpdateMainMessage("�A�C���F���������񂾂�A�r���܂œ���������������̂������A�킩��˂����ǂ�B");

                            UpdateMainMessage("�A�C���F�����܂ł̃��[�V�����������������B�e���|�����킳��銴�����B");

                            UpdateMainMessage("���F���[�F���̎�̐퓬���@�͗ǂ��g������̂ł��B�o���Ă����Ă��������B");

                            UpdateMainMessage("�A�C���F�����A���Ȃ�˂��ȁA�}�W�ŁB");

                            md.Message = "�����F���[�̓Z���X�e�B�A���E�m���@���K�����܂�����";
                            md.ShowDialog();
                            tc.CelestialNova = true;
                        }
                        if (tc.Level >= 39 && !tc.LavaAnnihilation)
                        {
                            UpdateMainMessage("���F���[�F�悵�A�k�u�R�X�ł��B���ƈ�ł��ˁB");

                            UpdateMainMessage("�A�C���F���F���[���g�悵�h�Ƃ������ƁA�����΂����܂��ȁB�b�n�b�n�n�n�I");

                            UpdateMainMessage("���F���[�F���āE�E�E�A�C���N�ɂ͋v���Ԃ�ɍ��؈�ࣂȖ��@�𗁂т���Ƃ��܂��傤�B");

                            UpdateMainMessage("�A�C���F�҂āA���F���[���Ă���ȒZ�C����˂���ȁH");

                            UpdateMainMessage("���F���[�F�����ł��ˁA����͂Ƃ��Ă����̉Ζ��@�ALavaAnnihilation�ł��B");

                            UpdateMainMessage("�@�@�@�w�S�S�S�H�H�I�H�I�I�I�I�I�I�I�I�I�I�x�@�@");

                            UpdateMainMessage("�A�C���F�b�Q�G�F�F�I�I���̎��͈�̑S������I�I");

                            UpdateMainMessage("���F���[�F���̖��@�͖��m�ȑΏۂ��Ƃ�܂���B���͂ɋ�����̂͑S�Ă��ΏۂƂȂ肦�܂��B");

                            UpdateMainMessage("�@�@�@�w�b�S�E�E�E�S�p�A�A�@�@�@���I�I�I�I�I�x�@�@");

                            UpdateMainMessage("�A�C���F�A�I�A�b�c�c�c�A�O�A�A�@�@�@�A�`�`�`�`�I�I�I�I");

                            UpdateMainMessage("���F���[�F�����Ă��̗n��͕s�K���ȓ����őΏۂ���炸�Ƃ��Ώۂ��Ă��s�����܂��B");

                            UpdateMainMessage("�A�C���F�킩�����I������������~�߂Ă�����āI�I�A�`�`�`�`�I�I�I�I");

                            UpdateMainMessage("���F���[�F���݂܂��񂪁A���͂��̖��@�A��U�n�܂�Ǝ~�߂悤������܂���B");

                            UpdateMainMessage("���i�F���[��A�A�C���A����͂ǂ���瓦�������Ȃ������ˁB���߂���H");

                            UpdateMainMessage("�A�C���F�����������I������́E�E�E");

                            UpdateMainMessage("�A�C���F�b�A�I�I�@�A�b�`�C�C�B�B�B�I�I�I�I");

                            UpdateMainMessage("���F���[�F���i����A��ł��܂��܂���̂Ő����@�ŗ�₵�Ă����Ă��������B");

                            UpdateMainMessage("���i�F���A�͂����Ł�");

                            UpdateMainMessage("�A�C���F�҂āI���i�̐����@���U����p�̂����ɑł��肾��I�H");

                            UpdateMainMessage("���i�F���v��A�Ώۂ͂����Ǝ���Ă����邩���");

                            UpdateMainMessage("�A�C���F�A�b�c�I�A�c�c�c�c�I�I�I�������A�������������������I�I�I");

                            UpdateMainMessage("���i�F�A�C���o�ŉ��`�B�@�n�C�p�[�t���[�Y�������X����I");

                            UpdateMainMessage("���F���[�F�ʔ������ł��ˁA�{�N���������ˌ��ł��B�t���[�Y�������X�E�G�N�X�g���[���ł��B");

                            UpdateMainMessage("�@�@�@�w�r�L�L�L�C�C�C�B�B�I�I�I�I�x�@�@�w�s�b�L�C�C�B�B���I�I�I�x");

                            UpdateMainMessage("�A�C���F�K�A�A�A�@�@�I�����������΂��炩���܂���I�I�I");

                            md.Message = "�����F���[�̓����@�E�A�j�q���[�V�����K�����܂�����";
                            md.ShowDialog();
                            tc.LavaAnnihilation = true;
                        }
                        if (tc.Level >= 40 && !tc.TimeStop)
                        {
                            UpdateMainMessage("���F���[�F�k�u�S�O�E�E�E�l�`�w���B�ł��B");

                            UpdateMainMessage("���i�F���F���[����A���߂łƂ��������܂���");

                            UpdateMainMessage("�A�C���F������ȃ��F���[�A�������Ƃ����ǂ��悤���Ȃ����B");

                            UpdateMainMessage("���F���[�F���������A������A�C���N�⃉�i���񂪈ꏏ�ɋ��Ă��ꂽ�������ł��B");

                            UpdateMainMessage("�A�C���F�Ō�͉����K������񂾁H");

                            UpdateMainMessage("���F���[�F���肫����ł����A���i����Ɠ��l�ATimeStop�ł��B");

                            UpdateMainMessage("�A�C���F�E�E�E��ׂ��E�E�E�}�W����B���F���[��������g�����炢�悢�攽������B");

                            UpdateMainMessage("���F���[�F�����ł�����܂����B�g���ǂ��낪�̐S�Ȃ����ł��B");

                            UpdateMainMessage("���i�F���A������Ɨǂ�������H");

                            UpdateMainMessage("���F���[�F�͂��A���ł��傤�H");

                            UpdateMainMessage("���i�F���F���[�������GaleWind�g������ˁH");

                            UpdateMainMessage("���F���[�F���i������{���ɃZ���X���ǂ��ł��ˁB���i����̐����͓������Ă��܂��B");

                            UpdateMainMessage("���i�F�Q�^�[�������Ԓ�~�ł����Ⴄ�킯�H�H");

                            UpdateMainMessage("���F���[�F�����Ȃ�܂����AGaleWind���̂ɂP�^�[���g���Ă�̂������ł��B");

                            UpdateMainMessage("���i�F���[��A�������ɖ����^�[���ɂ͂Ȃ�Ȃ��킯�ˁB");

                            UpdateMainMessage("���F���[�F���ꂪ�\�ɂȂ��Ă��܂�����A�c�t�d�k���͏��ł���ł��傤�B");

                            UpdateMainMessage("�A�C���F�ł��ȁE�E�E���������g�ݍ��킹���o�Ă��Ă��鎞�_�œV�˂̗̈悾���A�}�W�ŁB");

                            UpdateMainMessage("���F���[�F�n�n�n�A���x�������Ă܂����A�������Ԃ�ł��B���@�����̓J�[���݂ɂ͊����܂��񂩂�ˁB");

                            UpdateMainMessage("���F���[�F��́A�؂�D�≜�̎�ƌ��������̂��܂�Œʗp���Ȃ��G���~�ɂ����������ɂ͂���܂���B");

                            UpdateMainMessage("�A�C���F�����f�B�̃{�P�͂ǂ����H");

                            UpdateMainMessage("���F���[�F�ނƂ͉��x�������Ă��܂����A���Ă������͂���܂���ˁB");

                            UpdateMainMessage("���i�F���A�����Ȃ�ł����H");

                            UpdateMainMessage("���F���[�F�����A�s���ł����H");

                            UpdateMainMessage("���i�F�������ƁA���������킯����Ȃ��񂾂��ǁE�E�ETimeStop���g���Ă��ł����H");

                            UpdateMainMessage("���F���[�F�����B�ނ͂��������^�C�~���O��S�Ēm��s�����Ă��܂��B");

                            UpdateMainMessage("���F���[�F�Ȃ̂ŁATimeStop���������Ă��A�Ή�����d�|������ԂŎ��Ԓ�~�ɓ����Ă��܂��B");

                            UpdateMainMessage("���i�F����Ȏ����\�Ȃ񂾁E�E�E����ς萦����ˁAFiveSeeker�B");

                            UpdateMainMessage("���F���[�F���������Ӗ��ł́A����TimeStop�́A�_���Ă��̂͂��܂���ʓI�ł͂���܂���B");

                            UpdateMainMessage("���F���[�F�ӊO���������A���Ɉӎ���K�v�Ƃ��Ȃ��Ԃ�n��グ�ĕ��̂����ʓI�ł��ˁB");

                            UpdateMainMessage("�A�C���F��Ȃ́A�ǂ�����Č��ɂ߂�񂾂�B�_�����A�b�̎�������������Ă��Ă邺�B");

                            UpdateMainMessage("���F���[�F�A�C���N�Ȃ���v�ł���A�����Ɩ������ł����炭�t���ė����͂��ł��B");

                            UpdateMainMessage("���i�F���͂ǂ��H");

                            UpdateMainMessage("���F���[�F�������A���i��������h�ɂ��ė���܂��B���S���Ă��������B");

                            UpdateMainMessage("���F���[�F���āA����Ń{�N���悤�₭�S��������Ɉ���߂������������܂��B");

                            UpdateMainMessage("�A�C���F����߂����Ƃ́H");

                            UpdateMainMessage("���F���[�F���̐�́A�܂����x�̋@��ɏڂ����b���܂��B");

                            UpdateMainMessage("���F���[�F���͂��̏�Ԃ��L�[�v���āA�S�͂Ń_���W�����֒��݂܂��傤�B");

                            md.Message = "�����F���[�̓^�C���X�g�b�v���K�����܂�����";
                            md.ShowDialog();
                            tc.TimeStop = true;
                        }
                    }
                }
                #endregion

                #region "StandOfFlow���o�J�ɂ���ē{�郉�i"
                if (sc != null && sc.EmotionAngry)
                {
                    UpdateMainMessage("�A�C���F����E�E�E���̂܂ܓ{�点���܂܂���ʖڂ��B������ƃ��i�̏��Ɋ�邺�B");

                    UpdateMainMessage("�A�C���F�E�E�E�����A���������B���i�I");

                    UpdateMainMessage("���i�F�E�E�E����B");

                    UpdateMainMessage("�A�C���F�_�E�E�E�_���W�����s�����I");

                    UpdateMainMessage("���i�F�s���Ă���΁B");

                    UpdateMainMessage("�A�C���F���A�������������B�ǂ����Ǝv���������B");

                    UpdateMainMessage("���i�F�E�E�E���H");

                    UpdateMainMessage("�A�C���F���A���₠�\���Ȃ񂾂��ǂȁE�E�E���Č����񂾁B");

                    UpdateMainMessage("���i�F�E�E�E");

                    UpdateMainMessage("�A�C���F�������ƁE�E�E�܁A�҂��Ă��E�E�E");

                    UpdateMainMessage("���i�F�E�E�E�E�E�E�b�v�@");

                    UpdateMainMessage("���i�F�A�b�n�n�n�n�n�B�b�t�t�t�t�A���[���������A�A�b�n�n�n�n�A������Ă�̂�A�C��");

                    UpdateMainMessage("�A�C���F�Ȃ��A�������������I�H");

                    UpdateMainMessage("���i�F�����Ă��A�{�����߂邽�߂̍s���͉������߂ĂȂ��݂��������B");

                    UpdateMainMessage("�A�C���F����͍��l���Ă�񂾂����́B");

                    UpdateMainMessage("���i�F�v�����ĂȂ����E�E�E����ρA�o�J�ˁB");

                    UpdateMainMessage("�A�C���F�b�O�E�E�E");

                    UpdateMainMessage("���i�F�؋�����˃z���g�B���ʂ͑���ɉ�O�ɍl���Ƃ����̂�A�o���Ƃ��Ȃ����B");

                    UpdateMainMessage("�A�C���F�I�[�P�[�B���𗹉��I");

                    UpdateMainMessage("���i�F��́A�A���^���ŏ��Ɏӂ�ɗ��Ă�̂ɁA���ŃA���^���I�[�P�[�Z���t�Ȃ̂�B");

                    UpdateMainMessage("�A�C���F����A���Ƃ����ɖ߂��Ă��ꂽ�݂����ł��A�ق�ƂɈ��S������B");

                    UpdateMainMessage("���i�F�C�`�C�`�{������A���������ĂĂ������Ȃ��ł���B���X�{���ĂȂ��킯�����B");

                    UpdateMainMessage("�A�C���F�������炾�H");

                    UpdateMainMessage("���i�F�ŏ��́y�E�E�E����z����Ɍ��܂��Ă邶��Ȃ��B");

                    UpdateMainMessage("�A�C���F�}�W����I�ŏ������[�ڂ��ނ�オ���Ă�����˂����I");

                    UpdateMainMessage("���i�F����Ȃ��̉��Z�ł��傤���A���[�A����ς�A���Ńr�r�����킯�ˁ�");

                    UpdateMainMessage("�A�C���F��Ȏ��˂��I�����r�r��킯�˂�����I�����A������ƂȁE�E�E�B");

                    UpdateMainMessage("���i�F�A�b�n�n�n�n�A�S�����S������@���A���������A���͂ˁB�B�B");

                    UpdateMainMessage("�@�@�@�y�E�E�E�b�K�T�E�E�E�b�S�\�z�@�@���i�F��������B�R���ˁB");

                    UpdateMainMessage("���i�F�n�C�A����ł��t���Ă݂�B");

                    UpdateMainMessage("�A�C���F��H�����A���̖�͂������Ă�ȁI�ǂ�����˂������R���I�I�I");

                    UpdateMainMessage("���i�F�܁A�܂��A�o�J�A�C���͌��o�J�����A�o�J�ɂ̓o�J��͂���Ԃ��������o�J���ď���B");

                    UpdateMainMessage("�A�C���F���O�A�ǂꂾ���o�J���ē���Ă񂾂�B�Ō�̃o�J�̓I�J�V�C����B");

                    UpdateMainMessage("���i�F�I�J�V���Ȃ���B���񂽃o�J������ˁB���̂��炢�����傤�Ǘǂ��́�");

                    UpdateMainMessage("�A�C���F���肪�����������B���₢��A�T���L���[�I��������I�I�������������I�I�I");
                    using (MessageDisplay md = new MessageDisplay())
                    {
                        mc.AddBackPack(new ItemBackPack("����̓y���_���g"));
                        md.Message = "�y����̓y���_���g�z����ɓ���܂����B";
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.ShowDialog();
                    }

                    UpdateMainMessage("���i�F�ǂ񂾂����ł�̂�B�z���z���A�Ƃ��Ƃƃ_���W�����s������");

                    UpdateMainMessage("�A�C���F���A���������������ȁB����A�s���Ƃ��邩�I�I");
                   
                    sc.EmotionAngry = false;
                }
                #endregion
                #region "���F���[���n�߂ĎQ�������"
                if (WE.AvailableThirdCharacter && !WE.CommunicationThirdChara1)
                {
                    UpdateMainMessage("�A�C���F�悵�E�E�E���ɂR�K�X�^�[�g���B����Ă�邺�I");

                    UpdateMainMessage("���F���[�F���҂����Ă܂�����A�A�C���N�B");

                    UpdateMainMessage("���i�F�x����ˁA�A�C���B");

                    UpdateMainMessage("�A�C���F�����A�����Ă邶��˂����B���i�A�K���c����Ɋ��҂ɉ����悤���I");

                    UpdateMainMessage("���i�F�����A�����������P���Ă��炦�鎖�ɂȂ����񂾂��A�撣����B");

                    UpdateMainMessage("�A�C���F���F���[�l�E�E�E����A���F���[�B���������낵�����ނ��I");

                    UpdateMainMessage("���F���[�F�����A���������܂���҂��Ȃ��ł��������ˁH�{�N�̓z���g�Ɏア�ł���B");

                    UpdateMainMessage("�A�C���F����̋���ۂɂ������Ă����A�ǂ������Ӗ��Ȃ񂾁H");

                    UpdateMainMessage("���F���[�F���͎��̑����i���N���ɓ��܂�Ă��܂����̂ł���B");

                    UpdateMainMessage("���F���[�F����̌��A���^��̊Z�A�����ēV��̗��E�E�E�R�Ƃ��S�ĂˁB");

                    UpdateMainMessage("�A�C���F���I�H�E�E�E��̒N���E�E�E");

                    UpdateMainMessage("���F���[�F�{�N���I舂ł����B���x�t�@�[�W���{�a���łӂ��ƎU�����Ă���Ԃł����B");

                    UpdateMainMessage("�A�C���F�̗͓I�Ȑg�̔\�͂́H");

                    UpdateMainMessage("���F���[�F���āA�_���W�������e�̎������猩�Đ����o���܂�����ˁB���Ȃ萊���Ă��܂���B");

                    UpdateMainMessage("�A�C���F����A���������B���O�ɋ����Ă���ď����邺�B");

                    UpdateMainMessage("�A�C���F�`����FiveSeeker�ƌ����ǁA�����l�Ԃ��B�Γ��ɂ���Ă������肾�B��낵���ȃ��F���[�I");

                    UpdateMainMessage("���F���[�F�b�n�n�A�A�C���N�͏����G���~�Ɏ��Ă��܂��ˁB�����炱����낵���B");

                    UpdateMainMessage("���i�F���F���[����́A�����G���~�l�Ɠ����N�Ȃ�ł���ˁH");

                    UpdateMainMessage("���F���[�F�{�N�ƃG���~�ł����H�����ł���A���ꂪ�����H");

                    UpdateMainMessage("���i�F�����A�ǂ���ł��B���ꂶ���낵�����肢���܂���");

                    UpdateMainMessage("���F���[�F�����A�����炱���B");

                    UpdateMainMessage("�A�C���F��������A�������I");

                    we.CommunicationThirdChara1 = true;
                }
                #endregion
                if (we.CompleteArea1)
                {
                    mainMessage.Text = "�A�C���F���āA���K����n�߂邩�ȁB";
                    mainMessage.Update();
                    using (SelectDungeon sd = new SelectDungeon())
                    {
                        sd.StartPosition = FormStartPosition.Manual;
                        sd.Location = new Point(this.Location.X + 50, this.Location.Y + 50);
                        //if (we.CompleteArea5) sd.MaxSelectable = 5;
                        if (we.CompleteArea4) sd.MaxSelectable = 5;
                        else if (we.CompleteArea3) sd.MaxSelectable = 4;
                        else if (we.CompleteArea2) sd.MaxSelectable = 3;
                        else if (we.CompleteArea1) sd.MaxSelectable = 2;
                        sd.ShowDialog();
                        this.targetDungeon = sd.TargetDungeon;
                    }
                }
                if (this.targetDungeon == 1)
                {
                    if (!we.CompleteArea1)
                    {
                        mainMessage.Text = "�A�C���F���������P�K��˔j���邺�I";
                    }
                    else
                    {
                        mainMessage.Text = "�A�C���F�����P�x�A�P�K�ł��T�����邩�B";
                    }
                }
                else if (this.targetDungeon == 2)
                {
                    if (!we.CompleteArea2)
                    {
                        mainMessage.Text = "�A�C���F�ڎw���͂Q�K�𐧔e���ȁI";
                    }
                    else
                    {
                        mainMessage.Text = "�A�C���F�����P�x�A�Q�K�ł��T�����邩�B";
                    }
                }
                else if (this.targetDungeon == 3)
                {
                    if (!we.CompleteArea3)
                    {
                        mainMessage.Text = "�A�C���F���悢��R�K�A�C���������߂Ă������I";
                    }
                    else
                    {
                        mainMessage.Text = "�A�C���F�����P�x�A�R�K�ł��T�����邩�B";
                    }
                }
                else if (this.targetDungeon == 4)
                {
                    if (!we.CompleteArea4)
                    {
                        mainMessage.Text = "�A�C���F�S�K���e����Ă݂��邺�I";
                    }
                    else
                    {
                        mainMessage.Text = "�A�C���F�����P�x�A�S�K�ł��T�����邩�B";
                    }
                }
                else if (this.targetDungeon == 5)
                {
                    if (!we.CompleteArea5)
                    {
                        mainMessage.Text = "�A�C���F�ŉ��w���e�A����Ă݂���I";
                    }
                    else
                    {
                        mainMessage.Text = "�A�C���F�����P�x�A�T�K�ł��T�����邩�B";
                    }
                }
                else if (this.targetDungeon == -1)
                {
                    this.targetDungeon = 1;
                    return;
                }
                mainMessage.Update();
                System.Threading.Thread.Sleep(1000);

                // ���i�A�K���c�A�n���i�̈�ʉ�b�����͂����Ŕ��f���܂��B
                if (this.firstDay >= 1 && !we.CommunicationLana1 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana1 = true;
                else if (this.firstDay >= 2 && !we.CommunicationLana2 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana2 = true;
                else if (this.firstDay >= 3 && !we.CommunicationLana3 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana3 = true;
                else if (this.firstDay >= 4 && !we.CommunicationLana4 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana4 = true;
                else if (this.firstDay >= 5 && !we.CommunicationLana5 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana5 = true;
                else if (this.firstDay >= 6 && !we.CommunicationLana6 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana6 = true;
                else if (this.firstDay >= 7 && !we.CommunicationLana7 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana7 = true;
                else if (this.firstDay >= 8 && !we.CommunicationLana8 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana8 = true;
                else if (this.firstDay >= 9 && !we.CommunicationLana9 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana9 = true;
                else if (this.firstDay >= 10 && !we.CommunicationLana10 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana10 = true;
                else if (this.firstDay >= 11 && !we.CommunicationLana11 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana11 = true;
                else if (this.firstDay >= 12 && !we.CommunicationLana12 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana12 = true;
                else if (this.firstDay >= 13 && !we.CommunicationLana13 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana13 = true;
                else if (this.firstDay >= 14 && !we.CommunicationLana14 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana14 = true;
                else if (this.firstDay >= 15 && !we.CommunicationLana15 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana15 = true;
                else if (this.firstDay >= 16 && !we.CommunicationLana16 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana16 = true;
                else if (this.firstDay >= 17 && !we.CommunicationLana17 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana17 = true;
                else if (this.firstDay >= 18 && !we.CommunicationLana18 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana18 = true;
                else if (this.firstDay >= 19 && !we.CommunicationLana19 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana19 = true;
                else if (this.firstDay >= 20 && !we.CommunicationLana20 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana20 = true;

                if (this.firstDay >= 1 && !we.CommunicationHanna1 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna1 = true;
                else if (this.firstDay >= 2 && !we.CommunicationHanna2 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna2 = true;
                else if (this.firstDay >= 3 && !we.CommunicationHanna3 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna3 = true;
                else if (this.firstDay >= 4 && !we.CommunicationHanna4 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna4 = true;
                else if (this.firstDay >= 5 && !we.CommunicationHanna5 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna5 = true;
                else if (this.firstDay >= 6 && !we.CommunicationHanna6 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna6 = true;
                else if (this.firstDay >= 7 && !we.CommunicationHanna7 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna7 = true;
                else if (this.firstDay >= 8 && !we.CommunicationHanna8 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna8 = true;
                else if (this.firstDay >= 9 && !we.CommunicationHanna9 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna9 = true;
                else if (this.firstDay >= 10 && !we.CommunicationHanna10 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna10 = true;
                else if (this.firstDay >= 11 && !we.CommunicationHanna11 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna11 = true;
                else if (this.firstDay >= 12 && !we.CommunicationHanna12 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna12 = true;
                else if (this.firstDay >= 13 && !we.CommunicationHanna13 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna13 = true;
                else if (this.firstDay >= 14 && !we.CommunicationHanna14 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna14 = true;
                else if (this.firstDay >= 15 && !we.CommunicationHanna15 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna15 = true;
                else if (this.firstDay >= 16 && !we.CommunicationHanna16 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna16 = true;
                else if (this.firstDay >= 17 && !we.CommunicationHanna17 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna17 = true;
                else if (this.firstDay >= 18 && !we.CommunicationHanna18 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna18 = true;
                else if (this.firstDay >= 19 && !we.CommunicationHanna19 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna19 = true;
                else if (this.firstDay >= 20 && !we.CommunicationHanna20 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna20 = true;

                if (this.firstDay >= 1 && this.firstDay <= 4 && (!we.CommunicationGanz1 || !we.CommunicationGanz2 || !we.CommunicationGanz3 || !we.CommunicationGanz4) &&
                    mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop)
                {
                    switch (this.firstDay)
                    {
                        case 1:
                            we.CommunicationGanz1 = true;
                            break;
                        case 2:
                            we.CommunicationGanz1 = true;
                            we.CommunicationGanz2 = true;
                            break;
                        case 3:
                            we.CommunicationGanz1 = true;
                            we.CommunicationGanz2 = true;
                            we.CommunicationGanz3 = true;
                            break;
                        case 4:
                            we.CommunicationGanz1 = true;
                            we.CommunicationGanz2 = true;
                            we.CommunicationGanz3 = true;
                            we.CommunicationGanz4 = true;
                            break;
                        default:
                            we.CommunicationGanz1 = true;
                            we.CommunicationGanz2 = true;
                            we.CommunicationGanz3 = true;
                            we.CommunicationGanz4 = true;
                            break;
                    }
                }
                else if (this.firstDay >= 7 && !we.CommunicationGanz7 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop) we.CommunicationGanz7 = true;
                else if (this.firstDay >= 8 && !we.CommunicationGanz8 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop) we.CommunicationGanz8 = true;
                else if (this.firstDay >= 9 && !we.CommunicationGanz9 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop) we.CommunicationGanz9 = true;
                else if (this.firstDay >= 10 && !we.CommunicationGanz10 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop) we.CommunicationGanz10 = true;
                else if (this.firstDay >= 11 && !we.CommunicationGanz11 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop) we.CommunicationGanz11 = true;
                else if (this.firstDay >= 12 && !we.CommunicationGanz12 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop) we.CommunicationGanz12 = true;
                else if (this.firstDay >= 13 && !we.CommunicationGanz13 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop) we.CommunicationGanz13 = true;
                else if (this.firstDay >= 14 && !we.CommunicationGanz14 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop) we.CommunicationGanz14 = true;
                else if (this.firstDay >= 15 && !we.CommunicationGanz15 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop) we.CommunicationGanz15 = true;
                else if (this.firstDay >= 16 && !we.CommunicationGanz16 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop) we.CommunicationGanz16 = true;
                else if (this.firstDay >= 17 && !we.CommunicationGanz17 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop) we.CommunicationGanz17 = true;
                else if (this.firstDay >= 18 && !we.CommunicationGanz18 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop) we.CommunicationGanz18 = true;
                else if (this.firstDay >= 19 && !we.CommunicationGanz19 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop) we.CommunicationGanz19 = true;
                else if (this.firstDay >= 20 && !we.CommunicationGanz20 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop) we.CommunicationGanz20 = true;

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }




        /// <summary>
        /// if-else���ƃt���O�𕡎G�������ėc����݃��i�Ƃ̉�b�𐷂�グ�Ă��������B
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            #region "�P����"
            if (this.firstDay >= 1 && !we.CommunicationLana1 && mc.Level >= 1 && knownTileInfo[2])
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "���i�F�A�C���A�y�����̐����z�͐퓬���ɂ͎g���Ȃ����璍�ӂ��ĂˁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�����B�����̒����W�����Č������Ȃ��Ƒʖڂ�����ȁB";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�E�E�E�K���c��������̏��Ŕ���Ȃ��ł�H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���̂ȁB����킯�Ȃ�����A����ȕ֗��ȃ��m�B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�E�E�E�Q�{�P����ԂŁA�������Ȃ��ł�H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�w�������܂����x�Ȃ�ăC�x���g�ł��Ȃ����肻��͖����ȁB";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�E�E�E�H�ׂ��肵�Ȃ��ł�H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�������ɂ����܂Ńo�J����˂��B";
                        ok.ShowDialog();
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                }
                else
                {
                    if (!WE.AlreadyCommunicate)
                    {
                        mainMessage.Text = "���i�F�A�C���A�y�����̐����z�͐퓬���ɂ͎g���Ȃ����璍�ӂ��ĂˁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�����B�����̒����W�����Č������Ȃ��Ƒʖڂ�����ȁB";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�E�E�E�K���c��������̏��Ŕ���Ȃ��ł�H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���̂ȁB����킯�Ȃ�����A����ȕ֗��ȃ��m�B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�E�E�E�Q�{�P����ԂŁA�������Ȃ��ł�H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�w�������܂����x�Ȃ�ăC�x���g�ł��Ȃ����肻��͖����ȁB";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�E�E�E�H�ׂ��肵�Ȃ��ł�H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�������ɂ����܂Ńo�J����˂��B";
                        ok.ShowDialog();
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "�Q����"
            else if (this.firstDay >= 2 && !WE.CommunicationLana2 && mc.Level >= 1 && knownTileInfo[2])
            {
                we.CommunicationFirstContact2 = true;
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        if (!we.OneDeny)
                        {
                            mainMessage.Text = "�A�C���F����A���i�B���߂������I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F������ƃA�C���B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�Ȃ񂾂�H";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�A���^�A�����Ɛԃ|�[�V�����g���Ă�H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����Ă�킯�Ȃ�����B�Ȃ񂾁A�����̂��H";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�^�_�œn���킯�Ȃ�����Ȃ��B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�M�l�A�򉮂ł������̂ɋ���v������̂��I�H";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�r���S��";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�̂̂悵�݁E�E�E";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�����݂̘b�A�T�f���";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���������E�E�E��k����B�������Ă̂���H";
                            using (YesNoRequestMini yesno = new YesNoRequestMini())
                            {
                                yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                yesno.ShowDialog();
                                if (yesno.DialogResult == DialogResult.Yes)
                                {
                                    mc.Gold -= 5;
                                    GetPotionForLana();
                                    mainMessage.Text = "�A�C���F�b�N�E�E�E�ق��B";
                                    ok.ShowDialog();
                                    mainMessage.Text = "���i�F�����A�i�C�X�g���[�h��";
                                    ok.ShowDialog();
                                    mainMessage.Text = "�A�C���F���Ɍ��Ă�B�߂������A�����ȓ`������R���������n�C�p�[�򉮂��o����͂����B";
                                    ok.ShowDialog();
                                    mainMessage.Text = "���i�F���̐_���݂����Ă�̂���@���x�L��I";
                                    ok.ShowDialog();
                                    mainMessage.Text = "�A�C���F�_��E�E�E�܂��Ƃ��Ȗ򉮂��E�E�E";
                                    we.AlreadyCommunicate = true;
                                    we.CommunicationSuccess2 = true;
                                }
                                else
                                {
                                    mainMessage.Text = "�A�C���F�s�v��";
                                    ok.ShowDialog();
                                    mainMessage.Text = "���i�F�����[�H�o�J����Ȃ��́H";
                                    ok.ShowDialog();
                                    mainMessage.Text = "�A�C���F���͒��߂�Ɍ���B����ȏ��Ŏg���͓̂��̈������c�����鎖���B";
                                    ok.ShowDialog();
                                    mainMessage.Text = "���i�F�o�J����Ȃ��́H";
                                    ok.ShowDialog();
                                    mainMessage.Text = "�A�C���F�E�E�E�b�n�b�n�b�n�b�n�b�n";
                                    ok.ShowDialog();
                                    mainMessage.Text = "���i�F������������Ă��B������ł��ʖڂ�����ˁB";
                                    ok.ShowDialog();
                                    mainMessage.Text = "�A�C���F�b�n�b�n�b�n�b�n�b�n�E�E�E";
                                    ok.ShowDialog();
                                    we.OneDeny = true;
                                }
                            }
                        }
                        else
                        {
                            mainMessage.Text = "�A�C���F�����A���i�B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F����H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���ł��˂���B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�ԃ|�[�V�����~������ł���H�T�f���";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���������E�E�E�B�����������͔��������B";
                            using (YesNoRequestMini yesno = new YesNoRequestMini())
                            {
                                yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                yesno.ShowDialog();
                                if (yesno.DialogResult == DialogResult.Yes)
                                {
                                    mc.Gold -= 5;
                                    GetPotionForLana();
                                    mainMessage.Text = "�A�C���F�b�N�E�E�E�ق��B";
                                    ok.ShowDialog();
                                    mainMessage.Text = "���i�F�����A�i�C�X�g���[�h��";
                                    ok.ShowDialog();
                                    mainMessage.Text = "�A�C���F���Ɍ��Ă�B�߂������A�����ȓ`������R���������n�C�p�[�򉮂��o����͂����B";
                                    ok.ShowDialog();
                                    mainMessage.Text = "���i�F���̐_���݂����Ă�̂���@���x�L��I";
                                    ok.ShowDialog();
                                    mainMessage.Text = "�A�C���F�_��E�E�E�܂��Ƃ��Ȗ򉮂��E�E�E";
                                    we.AlreadyCommunicate = true;
                                    we.CommunicationSuccess2 = true;
                                }
                                else
                                {
                                    mainMessage.Text = "�A�C���F���ł��˂���I�I";
                                    ok.ShowDialog();
                                    mainMessage.Text = "���i�F��������B�ʔ�����悾�Ǝv�����̂ɁE�E�E";
                                }
                            }
                        }
                    }
                    else
                    {
                        mainMessage.Text = "���i�F�ԃ|�[�V������������������A��������͂����Ɛi�߂�͂���ˁB";
                    }
                }
                else
                {
                    if (!we.AlreadyCommunicate)
                    {
                        if (!we.OneDeny)
                        {
                            mainMessage.Text = "�A�C���F��[���A����s���Ă��邺�I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F������ƃA�C���B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�Ȃ񂾂�H";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�A���^�A�ԃ|�[�V�������Ă�������m���Ă�H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�m���Ă邪�����ĂȂ��ȁB�Ȃ񂾁A�����̂��H";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�^�_�œn���킯�Ȃ�����Ȃ��B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�M�l�A�򉮂ł������̂ɋ���v������̂��I�H";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�r���S��";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�̂̂悵�݁E�E�E";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�����݂̘b�A�T�f���";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���������E�E�E��k����B�������Ă̂���H";
                            using (YesNoRequestMini yesno = new YesNoRequestMini())
                            {
                                yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                yesno.ShowDialog();
                                if (yesno.DialogResult == DialogResult.Yes)
                                {
                                    mc.Gold -= 5;
                                    GetPotionForLana();
                                    mainMessage.Text = "�A�C���F�b�N�E�E�E�ق��B";
                                    ok.ShowDialog();
                                    mainMessage.Text = "���i�F�����A�i�C�X�g���[�h��";
                                    ok.ShowDialog();
                                    mainMessage.Text = "�A�C���F���Ɍ��Ă�B�߂������A�����ȓ`������R���������n�C�p�[�򉮂��o����͂����B";
                                    ok.ShowDialog();
                                    mainMessage.Text = "���i�F���̐_���݂����Ă�̂���@���x�L��I";
                                    ok.ShowDialog();
                                    mainMessage.Text = "�A�C���F�_��E�E�E�܂��Ƃ��Ȗ򉮂��E�E�E";
                                    we.AlreadyCommunicate = true;
                                    we.CommunicationSuccess2 = true;
                                }
                                else
                                {
                                    mainMessage.Text = "�A�C���F�s�v��";
                                    ok.ShowDialog();
                                    mainMessage.Text = "���i�F�����[�H�o�J����Ȃ��́H";
                                    ok.ShowDialog();
                                    mainMessage.Text = "�A�C���F���͒��߂�Ɍ���B����ȏ��Ŏg���͓̂��̈������c�����鎖���B";
                                    ok.ShowDialog();
                                    mainMessage.Text = "���i�F�o�J����Ȃ��́H";
                                    ok.ShowDialog();
                                    mainMessage.Text = "�A�C���F�E�E�E�b�n�b�n�b�n�b�n�b�n";
                                    ok.ShowDialog();
                                    mainMessage.Text = "���i�F������������Ă��B������ł��ʖڂ�����ˁB";
                                    ok.ShowDialog();
                                    mainMessage.Text = "�A�C���F�b�n�b�n�b�n�b�n�b�n�E�E�E";
                                    ok.ShowDialog();
                                    we.OneDeny = true;
                                }
                            }
                        }
                        else
                        {
                            mainMessage.Text = "�A�C���F�����A���i�B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F����H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���ł��˂���B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�ԃ|�[�V�����~������ł���H�T�f���";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���������E�E�E�B�����������͔��������B";
                            using (YesNoRequestMini yesno = new YesNoRequestMini())
                            {
                                yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                yesno.ShowDialog();
                                if (yesno.DialogResult == DialogResult.Yes)
                                {
                                    mc.Gold -= 5;
                                    GetPotionForLana();
                                    mainMessage.Text = "�A�C���F�b�N�E�E�E�ق��B";
                                    ok.ShowDialog();
                                    mainMessage.Text = "���i�F�����A�i�C�X�g���[�h��";
                                    ok.ShowDialog();
                                    mainMessage.Text = "�A�C���F���Ɍ��Ă�B�߂������A�����ȓ`������R���������n�C�p�[�򉮂��o����͂����B";
                                    ok.ShowDialog();
                                    mainMessage.Text = "���i�F���̐_���݂����Ă�̂���@���x�L��I";
                                    ok.ShowDialog();
                                    mainMessage.Text = "�A�C���F�_��E�E�E�܂��Ƃ��Ȗ򉮂��E�E�E";
                                    we.AlreadyCommunicate = true;
                                    we.CommunicationSuccess2 = true;
                                }
                                else
                                {
                                    mainMessage.Text = "�A�C���F���ł��˂���I�I";
                                    ok.ShowDialog();
                                    mainMessage.Text = "���i�F��������B�ʔ�����悾�Ǝv�����̂ɁE�E�E";
                                }
                            }
                        }
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "�R����"
            else if (this.firstDay >= 3 && !we.CommunicationLana3 && mc.Level >= 1 && knownTileInfo[2])
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        // �Q���ڂ̉�b�����{���Ă����ꍇ
                        if (we.CommunicationFirstContact2)
                        {
                            // �Q���ځF�ԃ|�[�V�������󂯎���Ă����ꍇ�B
                            if (we.CommunicationSuccess2)
                            {
                                mainMessage.Text = "�A�C���F�悧�A�������ň�����ƎҁE�E�E";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F�W���[�_�����ʂ��Ȃ��̂˃z���g�B�z������ԋp��B";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F�b�o�I�o�J�ȁI�I�I";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F�ԃ|�[�V�����ŏ����͐i�߂�񂶂�Ȃ����Ǝv���������B�W���[�_���ł��傤���B�z���z��";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F�N���M�l��";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F���H";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F�M�l�A���i�̖������U�҂��ȁI";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F�{�������������ԋp�͂��肦�Ȃ��́H";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F�������B�ǂ��������Ă邶��Ȃ����B";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F�ց[�A���፡�U�҂����ˁB�{���ɂȂ낤���ȁ[��";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F������ƃ\�R�A�^�[�[�[�[�C���I�I�I";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F�ӂ��Ă�ˁ[�z���g";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F�X�C�}�Z���ł����E�E�E";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F���Ⴀ����A����̕���";
                                ok.ShowDialog();
                                mc.Gold += 5;
                                mainMessage.Text = "�A�C���F�z���g�X�C�}�Z���ł����E�E�E�i������󂯎��܂����j";
                                ok.ShowDialog();
                                UpdateMainMessage("���i�F���Ă��āA�Ƃɂ����ԃ|�[�V�����͈���P�n�����炳�B�厖�Ɏg���Ă�ˁB");
                                GetPotionForLana();

                                mainMessage.Text = "�A�C���F�����I";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F����P�łǂ��܂ōs������A�z���g��";
                                we.AlreadyCommunicate = true;
                            }
                            else
                            {
                                mainMessage.Text = "�A�C���F����I���̂͑��k�Ȃ񂾂�";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F���k���ĉ���H";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F���k�̓A���ɂ��Ă��B";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F�A�����ĉ���H";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F�Ȃ��Ȃ��ǂ����āA�A�����K�v�Ȃ񂾂�B";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F���Ȃ�n�C�p�[�����\�[�h�Ƃ��H";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F���������̂��~�����񂾂��A����A����";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F�����}���g�݂����Ȃ�H";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F�Ⴄ�A�����ʂ�}���g�ł͂Ȃ��A�r���ɉt�̂������Ă���A�����B";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F���|�r�^���y�̂��ƁH";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F�E�E�E����A�ق�ƈ��������E�E�E";
                                ok.ShowDialog();
                                UpdateMainMessage("���i�F�܂��A�ӂ����Ƃ������ŋ����Ƃ��Ă������B���Ⴀ�n�C��");
                                GetPotionForLana();

                                mainMessage.Text = "�A�C���F���ƁA�T�f�������ȁB������Ƒ҂��Ă�";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F�E�\��A�E�\�E�\�B����Ȃ̎���Ȃ��ł���";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F���������������I�I";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F���Ƃ��Ƃ�����Ƃ�����悾�����̂�B�^�Ɏ󂯂邩�炱�������r�b�N����������Ȃ��́B";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F����A�z���g���܂˂��ȁB����P�Ŋ撣���Ă��邺�I";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F�ԃ|�[�V�����͈���P�n�����炳�B���s�����珳�m���Ȃ�����ˁB";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F�C�G�b�T�[�I";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F�܂��������������n�����ۂ�������������΂˂��E�E�E";
                                we.AlreadyCommunicate = true;
                            }
                        }
                        else
                        {
                            mainMessage.Text = "�A�C���F�Ȃ��Ȃ��i�߂˂��ȁB�r���܂ł����ƃ��C�t�������Ă��Ĉ����Ԃ��n���ɂȂ�B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F����͉񕜖򂪖�������ł���B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�ł��n���i�f�ꂿ���͏h���A�K���c�̂��������͕�������ȁB";
                            ok.ShowDialog();
                            UpdateMainMessage("���i�F�����A���ꂶ��E�E�E�͂��R����");
                            GetPotionForLana();

                            mainMessage.Text = "�A�C���F�����I�I�I�����ʂ�r���ɐԐF�̉t�́E�E�E�܂����I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�񕜖��B�厖�Ɏg���Ă��傤�����ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F����A�z���g���܂˂��ȁB����P�Ŋ撣���Ă��邺�I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�ԃ|�[�V�����͈���P�n�����炳�B���s�����珳�m���Ȃ�����ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�C�G�b�T�[�I";
                            we.AlreadyCommunicate = true;
                        }
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                }
                else
                {
                    if (!we.AlreadyCommunicate)
                    {
                        // �Q���ڂ̉�b�����{���Ă����ꍇ
                        if (we.CommunicationFirstContact2)
                        {
                            // �Q���ځF�ԃ|�[�V�������󂯎���Ă����ꍇ�B
                            if (we.CommunicationSuccess2)
                            {
                                mainMessage.Text = "�A�C���F�悧�A�������ň�����ƎҁE�E�E";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F�W���[�_�����ʂ��Ȃ��̂˃z���g�B�z������ԋp��B";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F�b�o�I�o�J�ȁI�I�I";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F�ԃ|�[�V�����ŏ����͐i�߂�񂶂�Ȃ����Ǝv���������B�W���[�_���ł��傤���B�z���z��";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F�N���M�l��";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F���H";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F�M�l�A���i�̖������U�҂��ȁI";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F�{�������������ԋp�͂��肦�Ȃ��́H";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F�������B�ǂ��������Ă邶��Ȃ����B";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F�ց[�A���፡�U�҂����ˁB�{���ɂȂ낤���ȁ[��";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F������ƃ\�R�A�^�[�[�[�[�C���I�I�I";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F�ӂ��Ă�ˁ[�z���g";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F�X�C�}�Z���ł����E�E�E";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F���Ⴀ����A�O�񒥎���������";
                                ok.ShowDialog();
                                mc.Gold += 5;
                                mainMessage.Text = "�A�C���F�z���g�X�C�}�Z���ł����E�E�E�i������󂯎��܂����j";
                                ok.ShowDialog();
                                UpdateMainMessage("���i�F���Ă��āA�Ƃɂ����ԃ|�[�V�����͈���P�n�����炳�B�厖�Ɏg���Ă�ˁB");
                                GetPotionForLana();

                                mainMessage.Text = "�A�C���F�����I";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F����P�łǂ��܂ōs������A�z���g��";
                                we.AlreadyCommunicate = true;
                            }
                            else
                            {
                                mainMessage.Text = "�A�C���F����I���̂͑��k�Ȃ񂾂�";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F���k���ĉ���H";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F���k�̓A���ɂ��Ă��B";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F�A�����ĉ���H";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F�Ȃ��Ȃ��ǂ����āA�A�����K�v�Ȃ񂾂�B";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F���Ȃ�n�C�p�[�����\�[�h�Ƃ��H";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F���������̂��~�����񂾂��A����A����";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F�����}���g�݂����Ȃ�H";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F�Ⴄ�A�����ʂ�}���g�ł͂Ȃ��A�r���ɉt�̂������Ă���A�����B";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F���|�r�^���y�̂��ƁH";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F�E�E�E����A�ق�ƈ��������E�E�E";
                                ok.ShowDialog();
                                UpdateMainMessage("���i�F�܂��A�ӂ����Ƃ������ŋ����Ƃ��Ă������B���Ⴀ�n�C��");
                                GetPotionForLana();

                                mainMessage.Text = "�A�C���F���ƁA�T�f�������ȁB������Ƒ҂��Ă�";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F�E�\��A�E�\�E�\�B����Ȃ̎���Ȃ��ł���";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F���������������I�I";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F���Ƃ��Ƃ�����Ƃ�����悾�����̂�B�^�Ɏ󂯂邩�炱�������r�b�N����������Ȃ��́B";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F����A�z���g���܂˂��ȁB����P�Ŋ撣���Ă��邺�I";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F�ԃ|�[�V�����͈���P�n�����炳�B���s�����珳�m���Ȃ�����ˁB";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F�C�G�b�T�[�I";
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F�܂��������������n�����ۂ�������������΂˂��E�E�E";
                                we.AlreadyCommunicate = true;
                            }
                        }
                        else
                        {
                            mainMessage.Text = "�A�C���F�Ȃ��Ȃ��i�߂˂��ȁB�r���܂ł����ƃ��C�t�������Ă��Ĉ����Ԃ��n���ɂȂ�B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F����͉񕜖򂪖�������ł���B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�ł��n���i�f�ꂿ���͏h���A�K���c�̂��������͕�������ȁB";
                            ok.ShowDialog();
                            UpdateMainMessage("���i�F�����A���ꂶ��E�E�E�͂��R����");
                            GetPotionForLana();

                            mainMessage.Text = "�A�C���F�����I�I�I�����ʂ�r���ɐԐF�̉t�́E�E�E�܂����I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�񕜖��B�厖�Ɏg���Ă��傤�����ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F����A�z���g���܂˂��ȁB����P�Ŋ撣���Ă��邺�I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�ԃ|�[�V�����͈���P�n�����炳�B���s�����珳�m���Ȃ�����ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�C�G�b�T�[�I";
                            we.AlreadyCommunicate = true;
                        }
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "�S����"
            else if (this.firstDay >= 4 && !we.CommunicationLana4 && mc.Level >= 1 && knownTileInfo[2])
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "�A�C���F�������������ȁB����̕i���𔃐�߂��邩�H���ʁB";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�K���c����̏��A���\�i�����ǂ���������ˁB�����r�b�N���ˁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���́A����߂����񂾁H�܂����S�����������킯�Ȃ�����B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�����ˁB�_���W������i�߂��ŁA����򉻂�z�肵�A���x���ɉ���������̎g���������ĂƂ��ł���B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�������̓��g���Ă����ȍ��́B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F���ہA�p�ӎ����ȍ�킶��Ȃ��B�|�[�V�������疳�������A���^���͂邩�Ƀ}�V�ˁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�b�P�A�ӊO�ƗL��]��������_�Ɏg���������m��˂�����B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�܂��܂��A���΂炭�҂��Ă�΁A�܂����ׂ�����Ęb����Ȃ��B�C���ɑ҂��܂����";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�������ȃO�_�O�_�l���Ă����傤���˂��I";
                        ok.ShowDialog();
                        UpdateMainMessage("���i�F���A�ł����͎g���Ȃ�����ˁB�ق疾���̃|�[�V�����B");
                        GetPotionForLana();

                        mainMessage.Text = "�A�C���F�T���L���[�I";
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                }
                else
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "�A�C���F�������������ȁB����̕i���𔃐�߂��邩�H���ʁB";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�K���c����̏��A���\�i�����ǂ���������ˁB�����r�b�N���ˁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���́A����߂����񂾁H�܂����S�����������킯�Ȃ�����B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�����ˁB�_���W������i�߂��ŁA����򉻂�z�肵�A���x���ɉ���������̎g���������ĂƂ��ł���B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�������̓��g���Ă����ȍ��́B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F���ہA�p�ӎ����ȍ�킶��Ȃ��B�|�[�V�������疳�������A���^���͂邩�Ƀ}�V�ˁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�b�P�A�ӊO�ƗL��]��������_�Ɏg���������m��˂�����B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�܂��܂��A���΂炭�҂��Ă�΁A�܂����ׂ�����Ęb����Ȃ��B�C���ɑ҂��܂����";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�������ȃO�_�O�_�l���Ă����傤���˂��I";
                        ok.ShowDialog();
                        UpdateMainMessage("���i�F���A�ł����͎g���Ȃ�����ˁB�ق獡���̃|�[�V�����B");
                        GetPotionForLana();

                        mainMessage.Text = "�A�C���F�T���L���[�I";
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "�T����"
            else if (this.firstDay >= 5 && !we.CommunicationLana5 && mc.Level >= 1 && knownTileInfo[2])
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "�A�C���F���i�A�ǂ����Ǝv���������I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�������ŉ����v�������̂�H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�_���W�����ɍs�����A�_���W�����ŉ��w�֓���������@���B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F���t�������Ďg���Ă�̂�����z���g�E�E�E";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���������ړI�n�̓_���W�����̍ŉ��Ȃ񂾂낤�H�Ȃ�΁A�S�Ă��@��Ԃ��Ă��܂��Ηǂ��B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F����撣���Č@��Ԃ��΁H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�C���Ƃ����āI�b�n�b�n�b�n�b�n�I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�E�E�E�i�����ځj";
                        ok.ShowDialog();
                        mainMessage.Text = "�E�E�E�E�E";
                        ok.ShowDialog();
                        mainMessage.Text = "�E�E�E�E";
                        ok.ShowDialog();
                        mainMessage.Text = "�E�E�E";
                        ok.ShowDialog();
                        mainMessage.Text = "�E�E";
                        ok.ShowDialog();
                        mainMessage.Text = "�E";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�E�E�E�E�E�E�b�n�b�n�b�n�b�n�b�n�I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�����������l�A�ǂ������������H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�b�n�b�n�b�n�I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F��̓�����̃h�A���炵�ĉ󂹂Ȃ��̂ɉ��l���Ă��̂�B�_���W������O��B���̃o�J�B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F��������E�E�E�����B";
                        ok.ShowDialog();
                        UpdateMainMessage("���i�F�͂��A�����̃|�[�V�����ˁB�������A���������A�����ɒ����ă`���[�_�C��B");
                        GetPotionForLana();

                        mainMessage.Text = "�A�C���F�������邺�I�C���Ƃ��I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�u�������邺�v���Ď������Ă邵�E�E�E�悪�v��������B";
                        WE.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                }
                else
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "�A�C���F���i�A�Q�Ă�Ԃɗǂ����Ǝv���������I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�������ŉ����v�������̂�H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�_���W�����ɍs�����A�_���W�����ŉ��w�֓���������@���B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F���t�������Ďg���Ă�̂�����z���g�E�E�E";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���������ړI�n�̓_���W�����̍ŉ��Ȃ񂾂낤�H�Ȃ�΁A�S�Ă��@��Ԃ��Ă��܂��Ηǂ��B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F����撣���Č@��Ԃ��΁H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�}�g�b�N�܂ō���p�ӂ����񂾂��B�C���Ƃ����āI�b�n�b�n�b�n�b�n�I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�E�E�E�i�����ځj";
                        ok.ShowDialog();
                        mainMessage.Text = "�E�E�E�E�E";
                        ok.ShowDialog();
                        mainMessage.Text = "�E�E�E�E";
                        ok.ShowDialog();
                        mainMessage.Text = "�E�E�E";
                        ok.ShowDialog();
                        mainMessage.Text = "�E�E";
                        ok.ShowDialog();
                        mainMessage.Text = "�b�o�L�E�E�E�i�}�g�b�N�����܂����j";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�E�E�E�E�E�E�b�n�b�n�b�n�b�n�b�n�I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�����������l�A�ǂ������������H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�A�[�[�[�b�n�b�n�b�n�I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�������߂ă_���W�����ɐ�O���Ȃ�����B���̃o�J�B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F��������E�E�E�����B";
                        ok.ShowDialog();
                        UpdateMainMessage("���i�F�͂��A�����̃|�[�V�����ˁB�������A���������A�����ɒ����ă`���[�_�C��B");
                        GetPotionForLana();

                        mainMessage.Text = "�A�C���F�������邺�I�C���Ƃ��I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�u�������邺�v���Ċ��S�Ɏ������Ă邵�E�E�E�������ǂ����܂ł�����̂�����B";
                        WE.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "�U����"
            else if (this.firstDay >= 6 && !we.CommunicationLana6 && mc.Level >= 1 && knownTileInfo[2])
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "���i�F���������΁A����������T�Ԍo�߂ˁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���̃_���W�����A�s���ǂ��s���ǂ����`�������˂��ȁB";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�ǂ����Ă�H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�����ȁB";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�w�����ȁx���ĂЂ���Ƃ��ăo�J�H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���܂����́w�o�J�x���Ă悭�g���ȁB���͌����ăo�J����Ȃ��B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�ց[�E�E�E�����Ȃ񂾁B�Ⴆ�΁H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�X�g���[�g�E�X�}�b�V���I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�ց[�E�E�E���ɂ́H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�틵�����āA������I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�Ō�̓|�[�V�����ł��܂ɉ񕜁H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�s���`�̎������A�|�[�V���E�E�E���̕�����I�H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�����p�^�[����";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���ȁA����ǂ�����ƌ����񂾁B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�m���ɁA���͂܂��������邵���Ȃ���ˁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���͂܂��H";
                        ok.ShowDialog();
                        UpdateMainMessage("���i�F�����B�n�C�A�|�[�V������");
                        GetPotionForLana();

                        mainMessage.Text = "�A�C���F�Ȃ񂩓˂������錾�������ȁB�܂��|�[�V�����͂��肪�������炤���I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�C�ɂ��Ȃ��A�C�ɂ��Ȃ��B���������撣���Ă�ˁB";
                        WE.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                }
                else
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "���i�F���Ɉ�T�Ԃ��o�߂�����ˁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���̃_���W�����A�s���ǂ��s���ǂ����`�������˂��ȁB";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�ǂ����Ă�H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�����ȁB";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�w�����ȁx���ĂЂ���Ƃ��ăo�J�H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���܂����́w�o�J�x���Ă悭�g���ȁB���͌����ăo�J����Ȃ��B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�ց[�E�E�E�����Ȃ񂾁B�Ⴆ�΁H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�X�g���[�g�E�X�}�b�V���I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�ց[�E�E�E���ɂ́H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�틵�����āA������I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�Ō�̓|�[�V�����ł��܂ɉ񕜁H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�s���`�̎������A�|�[�V���E�E�E���̕�����I�H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�����p�^�[����";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���ȁA����ǂ�����ƌ����񂾁B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�m���ɁA���͂܂��������邵���Ȃ���ˁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���͂܂��H";
                        ok.ShowDialog();
                        UpdateMainMessage("���i�F�����B�n�C�A�����̃|�[�V������");
                        GetPotionForLana();

                        mainMessage.Text = "�A�C���F�Ȃ񂩓˂������錾�������ȁB�܂��|�[�V�����͂��肪�������炤���I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�C�ɂ��Ȃ��A�C�ɂ��Ȃ��B���፡�����撣���Ă��Ă�ˁB";
                        WE.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "�V����"
            else if (this.firstDay >= 7 && !we.CommunicationLana7 && mc.Level >= 1 && knownTileInfo[2])
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "���i�F���[�I������Ƃ�����ƁI";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�Ȃ񂾁H���X�����ȁB";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�A�C���A�Ђ���Ƃ��ĂƂ�ł��Ȃ����Ⴂ���ĂȂ��H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�������̉����ǂ��������Ă񂾁B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�����ˁE�E�E�������Ȃ���B�����A���܂ŋ����ĂȂ��������P�����B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�V�c�R�C�B���������{�邼�B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�Z�ƌ����΁H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�N���e�B�J���q�b�g�I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F���́H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�X�[�p�[�N���e�B�J���q�b�g�I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F����E�E�E���͂ˁA�Z�p�ɂ͂܂��B���ꂽ�p�����^������̂�B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�n�C�p�[�N���e�B�J���q�b�g�I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F������A�ǂ����畷���Ȃ�����ˁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���܂�B�ŁA�B���ꂽ�p�����^���Ă͉̂����H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F����͔閧���";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���G���N�g���E�W�F�l�e�B�b�N�E�N���e�B�J���q�b�g�I�I�I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�����������������A�n�C�n�C�B�����ƂˁA���͐搧�U���Ɋ֌W����̂�B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�����A�������₽�܂ɑ��肪��Ɏd�|���Ă���ȁB";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F������郉�C�t�ɂȂ��Ă���񕜂��悤�Ƃ��Ă�";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�|�[�V������x��̏ꍇ������ȁB";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�Ȃ̂ŁA���������O��";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�|�[�V�������ޕK�v������ȁB";
                        ok.ShowDialog();
                        UpdateMainMessage("���i�F�����Ȃ�Ȃ����߂ɂ��Z�p���グ��Ό��ʃe�L������B�n�C�A�|�[�V�����B");
                        GetPotionForLana();

                        if (mc.Agility > 10)
                        {
                            mainMessage.Text = "�A�C���F�m���ɍŋ߉��ƂȂ������搧�U�����o�₷���Ǝv���Ă������B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�ց[�A�����ƒb���Ă�񂾋Z�̕����B" + mc.Agility.ToString() + "������Ώ㓙�ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����A�C���Ƃ����āI�I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�Ȃ񂾌��\��邶��Ȃ��́B�z���g�ɔC�����Ⴈ��������B";
                        }
                        else
                        {
                            mainMessage.Text = "�A�C���F�E�E�E�ʓ|�������I�I�n�C�p�[�N���e�B�J���q�b�g�I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�n�@�`�`�`�`�`�`�@�`�`�`�`�`�@�`�`�`�`�`�`�`�E�E�E";
                        }
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                }
                else
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "���i�F�I�n���[�B���A������Ƃ�����ƁI";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�Ȃ񂾁H�����瑛�X�����ȁB";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�A�C���A�Ђ���Ƃ��ĂƂ�ł��Ȃ����Ⴂ���ĂȂ��H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�������̉����ǂ��������Ă񂾁B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�����ˁE�E�E�������Ȃ���B�����A���܂ŋ����ĂȂ��������P�����B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�V�c�R�C�B���������{�邼�B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�Z�ƌ����΁H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�N���e�B�J���q�b�g�I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F���́H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�X�[�p�[�N���e�B�J���q�b�g�I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F����E�E�E���͂ˁA�Z�p�ɂ͂܂��B���ꂽ�p�����^������̂�B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�n�C�p�[�N���e�B�J���q�b�g�I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F������A�ǂ����畷���Ȃ�����ˁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���܂�B�ŁA�B���ꂽ�p�����^���Ă͉̂����H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F����͔閧���";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���G���N�g���E�W�F�l�e�B�b�N�E�N���e�B�J���q�b�g�I�I�I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�����������������A�n�C�n�C�B�����ƂˁA���͐搧�U���Ɋ֌W����̂�B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�����A�������₽�܂ɑ��肪��Ɏd�|���Ă���ȁB";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F������郉�C�t�ɂȂ��Ă���񕜂��悤�Ƃ��Ă�";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�|�[�V������x��̏ꍇ������ȁB";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�Ȃ̂ŁA���������O��";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�|�[�V�������ޕK�v������ȁB";
                        ok.ShowDialog();
                        UpdateMainMessage("���i�F�����Ȃ�Ȃ����߂ɂ��Z�p���グ��Ό��ʃe�L������B�n�C�A�|�[�V�����B");
                        GetPotionForLana();

                        if (mc.Agility > 10)
                        {
                            mainMessage.Text = "�A�C���F�m���ɍŋ߉��ƂȂ������搧�U�����o�₷���Ǝv���Ă������B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�ց[�A�����ƒb���Ă�񂾋Z�̕����B" + mc.Agility.ToString() + "������Ώ㓙�ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����A�C���Ƃ����āI�I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�Ȃ񂾌��\��邶��Ȃ��́B�z���g�ɔC�����Ⴈ��������B";
                        }
                        else
                        {
                            mainMessage.Text = "�A�C���F�E�E�E�ʓ|�������I�I�n�C�p�[�N���e�B�J���q�b�g�I";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�n�@�`�`�`�`�`�`�@�`�`�`�`�`�@�`�`�`�`�`�`�`�E�E�E";
                        } 
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "�W����"
            else if (this.firstDay >= 8 && !we.CommunicationLana8 && mc.Level >= 1 && knownTileInfo[2])
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "�A�C���F�̗͂��グ�Ă����ƁA�ő僉�C�t��������񂾂�ȁH";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F������O����Ȃ��B����Ȏ��B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���i�A���͒m���Ă�񂾂�H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F���̎���H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�̗͂Ɋւ���B���p�����^���I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�������A����Ȃ́B�̗͂͏����ɍő僉�C�t�̑��������ˁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�w�閧���x�Ƃ�������B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�������Č����Ă邶��Ȃ��́B�C�����������������Ȃ��ł�B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�����킯�Ȃ�����I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F���������̗͂��Č�������A����ȊO����������Č����̂�H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�������ȁB�̗͂ƌ����΁A�́I�I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F����͘r�́B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���Ⴀ���ɂ������ȁB���v�́I�I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�����ɍő僉�C�t�̎��ˁB���́H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�b�n�b�n�b�n�b�n�I";
                        ok.ShowDialog();
                        UpdateMainMessage("���i�F�z���A�ԃ|�[�V������B�����Ƃ����ǁA�̗̓o�J�ɂ͂Ȃ�Ȃ��ł�ˁB");
                        GetPotionForLana();

                        mainMessage.Text = "�A�C���F�S�z����ȁA�̗͂��S�Ă���Ȃ������炢�������Ă邳�B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�z���g������E�E�E�p�����^�t�o�ɂ͍őP��s�����ĂˁB";
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                }
                else
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "�A�C���F����ӂƋ^��Ɏv�������A�̗͂��グ�Ă����ƁA�ő僉�C�t��������񂾂�ȁH";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F������O����Ȃ��B����Ȏ��B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���i�A���͒m���Ă�񂾂�H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F���̎���H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�̗͂Ɋւ���B���p�����^���I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�������A����Ȃ́B�̗͂͏����ɍő僉�C�t�̑��������ˁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�w�閧���x�Ƃ�������B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�������Č����Ă邶��Ȃ��́B�C�����������������Ȃ��ł�B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�����킯�Ȃ�����I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F���������̗͂��Č�������A����ȊO����������Č����̂�H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�������ȁB�̗͂ƌ����΁A�́I�I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F����͘r�́B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���Ⴀ���ɂ������ȁB���v�́I�I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�����ɍő僉�C�t�̎��ˁB���́H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�b�n�b�n�b�n�b�n�I";
                        ok.ShowDialog();
                        UpdateMainMessage("���i�F�z���A�ԃ|�[�V������B�����Ƃ����ǁA�̗̓o�J�ɂ͂Ȃ�Ȃ��ł�ˁB");
                        GetPotionForLana();

                        mainMessage.Text = "�A�C���F�S�z����ȁA�̗͂��S�Ă���Ȃ������炢�������Ă邳�B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�z���g������E�E�E�p�����^�t�o�ɂ͍őP��s�����ĂˁB";
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "�X����"
            else if (this.firstDay >= 9 && !we.CommunicationLana9 && mc.Level >= 1 && knownTileInfo[2])
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "�A�C���F�͂������S�Ă��B���i�������v������H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F���[�񂺂��";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�ʏ�U�����t�o�B���ꂳ������΁A�ǂ�ȓG���ꌂ���I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F���[�񂺂���";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���܂��ɂ�������N���e�B�J���q�b�g���o��΍X�Ɋm���I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F���[�񂺂����";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�������O�̂��̃m���m���Ȏp�����P�ɕ����ȁB���Ȃ񂾁H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�����A�{�点����������B�͂��ĕ����U���ł���H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�������B���̐��ň�Ԑ�ΓI�Ȃ��̂��B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�����U���������Ȃ������������B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F����ł����͓��Ă�B���ĂČ�����B";
                        ok.ShowDialog();
                        if (WE.CompleteArea1)
                        {
                            mainMessage.Text = "���i�F�Q�K�ɍs���Ă�Ȃ猾���Ă������ǁE�E�E�o����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����o����Ă񂾁H���[���C�Ƃ����������H";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F������A�ǂꂾ�������u���u������Ă����Ӗ��ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F����ł����͓��Ă�B���ĂČ�����B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F����Ȃ��䎌�����ĂȂ��ŁA�����͈͗ȊO���グ�Ă����Ȃ����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F����ł����͓��Ă�B���ĂČ�����B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�b�N�E�E�E�����䎌�����܂ł��E�E�E���P�ɃC���C���������ˁB���낤�������";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���������I�I�͈ȊO���グ��B�}�W���قȁB";
                            ok.ShowDialog();
                            UpdateMainMessage("���i�F�܂��A�Ƃ������B�����U���������Ȃ����͖��@��X�L���őΏ����邱�ƂˁB�͂��A�|�[�V�����B");
                            GetPotionForLana();

                            mainMessage.Text = "�A�C���F�Ƃ���ł��O���A���ł���Ȏ������Ă�킯�H";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�����H���A�������A���[�Ă���ł͎��񂨊y���݂ɁE�E�E�A�n�n��";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������A�����΂����Ă񂾂��B�܂���񂭂�ăT���L���[�B";
                        }
                        else
                        {
                            mainMessage.Text = "���i�F�܂����͑��v�����ǁA�͈ȊO�Ń_���[�W���オ����@�����鎖���炢�o���ĂˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F����ł����͓��Ă�B���ĂČ�����B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�͈ȊO�́A���Ƃ��Βm�͂��オ��ΈЗ͂��オ�閂�@���������݂���̂�B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F����ł����͓��Ă�B���ĂČ�����B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�X�L�����Ă���ł���H�������{�͗͂���Ȃ��ċZ�̕����������ʂ������o���̂�B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F����ł����͓��Ă�B���ĂČ�����B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�b�N�E�E�E������œ����䎌�����܂ł��E�E�E���P�ɃC���C���������ˁB���낤�������";
                            ok.ShowDialog();
                            UpdateMainMessage("�A�C���F���������I�I�͈ȊO���グ��B�}�W���قȁB");
                            GetPotionForLana();

                            mainMessage.Text = "�A�C���F�Ƃ���ł��O���A���ł���Ȏ���m���Ă�킯�H";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�����H���A�������A���[�Ă���ł͎��񂨊y���݂ɁE�E�E�A�n�n��";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������A�����΂����Ă񂾂��B�܂���񂭂�ăT���L���[�B";
                        }
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                }
                else
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "�A�C���F�͂������S�Ă��B���i�������v������H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F���[�񂺂��";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�ʏ�U�����t�o�B���ꂳ������΁A�ǂ�ȓG���ꌂ���I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F���[�񂺂���";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���܂��ɂ�������N���e�B�J���q�b�g���o��΍X�Ɋm���I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F���[�񂺂����";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�������O�̂��̃m���m���Ȏp�����P�ɕ����ȁB���Ȃ񂾁H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�����A�{�点����������B�͂��ĕ����U���ł���H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�������B���̐��ň�Ԑ�ΓI�Ȃ��̂��B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�����U���������Ȃ������������B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F����ł����͓��Ă�B���ĂČ�����B";
                        ok.ShowDialog();
                        if (WE.CompleteArea1)
                        {
                            mainMessage.Text = "���i�F�Q�K�ɍs���Ă�Ȃ猾���Ă������ǁE�E�E�o����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����o����Ă񂾁H���[���C�Ƃ����������H";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F������A�ǂꂾ�������u���u������Ă����Ӗ��ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F����ł����͓��Ă�B���ĂČ�����B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F����Ȃ��䎌�����ĂȂ��ŁA�����͈͗ȊO���グ�Ă����Ȃ����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F����ł����͓��Ă�B���ĂČ�����B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�b�N�E�E�E�����䎌�����܂ł��E�E�E���P�ɃC���C���������ˁB���낤�������";
                            ok.ShowDialog();
                            UpdateMainMessage("�A�C���F���������I�I�͈ȊO���グ��B�}�W���قȁB");
                            GetPotionForLana();

                            mainMessage.Text = "�A�C���F�Ƃ���ł��O���A���ł���Ȏ���m���Ă�킯�H";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�����H���A�������A���[�Ă���ł͎��񂨊y���݂ɁE�E�E�A�n�n��";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������A�����΂����Ă񂾂��B�܂���񂭂�ăT���L���[�B";
                        }
                        else
                        {
                            mainMessage.Text = "���i�F�܂����͑��v�����ǁA�͈ȊO�Ń_���[�W���オ����@�����鎖���炢�o���ĂˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F����ł����͓��Ă�B���ĂČ�����B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�͈ȊO�́A���Ƃ��Βm�͂��オ��ΈЗ͂��オ�閂�@���������݂���̂�B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F����ł����͓��Ă�B���ĂČ�����B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�X�L�����Ă���ł���H�������{�͗͂���Ȃ��ċZ�̕����������ʂ������o���̂�B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F����ł����͓��Ă�B���ĂČ�����B";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�b�N�E�E�E������œ����䎌�����܂ł��E�E�E���P�ɃC���C���������ˁB���낤�������";
                            ok.ShowDialog();
                            UpdateMainMessage("�A�C���F���������I�I�͈ȊO���グ��B�}�W���قȁB");
                            GetPotionForLana();

                            mainMessage.Text = "�A�C���F�Ƃ���ł��O���A���ł���Ȏ������Ă�킯�H";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�����H���A�������A���[�Ă���ł͎��񂨊y���݂ɁE�E�E�A�n�n��";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������A�����΂����Ă񂾂��B�܂���񂭂�ăT���L���[�B";
                        }
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "�P�O����"
            else if (this.firstDay >= 10 && !we.CommunicationLana10 && mc.Level >= 1 && knownTileInfo[2])
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "���i�F�����A�����̃o�J�B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�悤�A�����悻���́E�E�E���i";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�A���^�A�z���g�Ƀo�J��ˁE�E�E�����v�����Ȃ�����B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�����牽�����Ă񂾂�B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�A�C���A�����̓��i�搶���m�͂Ƃ������̂�e�ؒ��J�ɉ�����Ă������A�o��Ȃ����B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���`�A�n���i���΂���Ƃ��s���ċx��ŗǂ����H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�܂��A�m�͂t�o�����@�͂t�o�B����͊�{���̊�{�ˁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���`�͂��͂��B�K���c��������Ƃ��ŕ��팩�Ă��ėǂ����H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F���ꂩ��t�����ʂ������閂�@������񂾂��ǁA���̕t���|�C���g�ɉ��Z�l���t���́B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���`��A�ŋ߂��B�ǂ����Q�t���������ĂȁB������Ƃ��������H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F��͑��ɖh��n�̖��@�ɑ΂��Ă��ǂꂾ���h�䂷�邩�̃|�C���g�����Z������ˁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�܂����̉�����̂���������ď����B�t���@�@�`�`�A�@�B���Ꮽ���Q���B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�ɂ߂��͖��@���ˁB�ʏ�U�����̂ɖ��@�t�^�����A�U���͂𑝑傳����́B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�ȂɁI�H���̍������Ă镐����オ��̂��I�H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F������B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�ǂ̂��炢�オ��񂾁I�H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�m�͂̕������B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�E�E�E�O�n�@�@�I�I�I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�A���^�̕�����A�C���B����ȂR���̃V�J�g�X�^���X�Ȃ�Ď�낤���Ă̂��ԈႢ�Ȃ̂�B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�ǁA�ǂ�����΂����H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�m�͂��グ�Ȃ�����";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���Ɏ�͂Ȃ��̂��I�H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�m�͂ɂ��ĉ�������Ă�񂶂�Ȃ��B���ɂ���킯�Ȃ��ł���B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�\�R�����Ƃ��������܂��B���i�w�l�x�I�I";
                        ok.ShowDialog();
                        UpdateMainMessage("���i�F�͂��A�|�[�V������");
                        GetPotionForLana();

                        mainMessage.Text = "�A�C���F�������A�|�[�V�����ŉ��͂��܂�����˂��B�͂��B�͂����グ��΁B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F���@���͒m�́~�R�̈З͂��o��̂��";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���i�B����f�킷�񂶂�Ȃ��B����Ȃ��̂ɗ��鉴�ł͂Ȃ��B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�ӊO�Ɗ�łˁB�܂��A�m�͂��̂Ă邱�Ƃ����͔����Ă�ˁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�I�[�P�[�B���@��������Ă݂������ȁB";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F����A�撣���ĂˁB�m�͂��オ��΃A�C���A���Ȃ�ǂ����s�����B";
                        this.we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                }
                else
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "���i�F�����A�����̃o�J�B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�悤�A�����悻���́E�E�E���i";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�A���^�A�z���g�Ƀo�J��ˁE�E�E�����v�����Ȃ�����B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�����牽�����Ă񂾂�B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�A�C���A�����̓��i�搶���m�͂Ƃ������̂�e�ؒ��J�ɉ�����Ă������A�o��Ȃ����B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���`�A�����_���W�����������Ƃ��Ȃ񂾁B�s���ėǂ����H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�܂��A�m�͂t�o�����@�͂t�o�B����͊�{���̊�{�ˁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�����Ƃ������B�K���c��������A�����ǂ����̎d����Ă邩�ȁB";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F���ꂩ��t�����ʂ������閂�@������񂾂��ǁA���̕t���|�C���g�ɉ��Z�l���t���́B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���`��B�ǂ�������͐Q�t�������������݂������B�ڊo�܂���Ƃ��͖������H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F��͑��ɖh��n�̖��@�ɑ΂��Ă��ǂꂾ���h�䂷�邩�̃|�C���g�����Z������ˁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�܂����̉����������ڊo�܂��ɂ��Ȃ�˂��ȁB�t���@�@�`�`�A�@�B���ĂƁA����s���Ă��邩�B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�ɂ߂��͖��@���ˁB�ʏ�U�����̂ɖ��@�t�^�����A�U���͂𑝑傳����́B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�ȂɁI�H���̍������Ă镐����オ��̂��I�H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F������B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�ǂ̂��炢�オ��񂾁I�H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�m�͂̕������B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�E�E�E�O�n�@�@�I�I�I";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�A���^�̕�����A�C���B����ȂR���̃V�J�g�X�^���X�Ȃ�Ď�낤���Ă̂��ԈႢ�Ȃ̂�B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�ǁA�ǂ�����΂����H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�m�͂��グ�Ȃ�����";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���Ɏ�͂Ȃ��̂��I�H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�m�͂ɂ��ĉ�������Ă�񂶂�Ȃ��B���ɂ���킯�Ȃ��ł���B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�\�R�����Ƃ��������܂��B���i�w�l�x�I�I";
                        ok.ShowDialog();
                        UpdateMainMessage("���i�F�͂��A�|�[�V������");
                        GetPotionForLana();

                        mainMessage.Text = "�A�C���F�������A�|�[�V�����ŉ��͂��܂�����˂��B�͂��B�͂����グ��΁B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F���@���͒m�́~�R�̈З͂��o��̂��";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���i�B����f�킷�񂶂�Ȃ��B����Ȃ��̂ɗ��鉴�ł͂Ȃ��B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�ӊO�Ɗ�łˁB�܂��A�m�͂��̂Ă邱�Ƃ����͔����Ă�ˁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�I�[�P�[�B���@��������Ă݂������ȁB";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F����A�撣���ĂˁB�m�͂��オ��΃A�C���A���Ȃ�ǂ����s�����B";
                        this.we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "�P�P����"
            else if (this.firstDay >= 11 && !we.CommunicationLana11 && mc.Level >= 1 && knownTileInfo[2])
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "�A�C���F�ӂ��`�A�ǂ������q���o�˂��ȁB�N���e�B�J�����C�}�C�`�o�˂��B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�́E�Z�E�m�E�̂ƈ�ʂ葵�����Ƃ��Ă��A��ԏd�v�ȗv�f�B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�Ō�Ɏc�����S���Ă��B���ǂǂ��e�����Ă���񂾁H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�S���̂��̂ˁB���ʃp�����^�t�o�ɂ͊֗^���Ȃ��́B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�S�ЂƂŐ��E���ς����Ă��B�܂��グ�Ăǂ��Ȃ�킯�ł�����܂��B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�����ˁB�R���Ƃ����ďグ��K�v�͂Ȃ���ˁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F����������ۖ��A�グ��K�v�͖�����ȁH";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�����v���񂾂�����A�グ�Ȃ��Ă��ǂ��񂶂�Ȃ��H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�ӂ��`�A�ǂ����[���������˂��ȁB";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�z���g����[���Ȃ���ˁA���Ⴀ�q���g��B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�����A���ނ��B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�͂P�A�Z�P�A�m�P�A�̂P�A�S�P�O�O�B�ǂ��H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�����O�E�S�u��������|�������ɂ˂��ȁE�E�E";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�悤����ɐS�����グ�Ă��Ӗ����Ȃ����Ă킯��B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���Ⴀ����ϕs�v���Ă��Ƃ��H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�͂S�O�A�Z�Q�T�A�m�P�O�A�̂Q�T�A�S�P�B�ǂ��H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���@���኱��͂����A��̂�ꂻ�����ȁB�����E�E�E";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F������A�R���Ƃ����ďグ��K�v�͂Ȃ����ǁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���ƂȂ����q���o�Ȃ������ȁB�����G��������}�Y�������ȁB";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�����@�B�S�͒��q�̗ǂ��������ď��ˁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�ǂ����������H�A�o�E�g�����Ă悭������˂��B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F������Ƙb�������Ȃ肻���ˁB�����͖����̃_���W�����̌���Ď��Ł�";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�n�@�I�H����������҂Ă��āB��������B";
                        ok.ShowDialog();
                        UpdateMainMessage("���i�F�����͂��ƂȂ������q�����܂܂ŋ��Ă�B�|�[�V�����ŉ䖝�䖝��");
                        GetPotionForLana();

                        mainMessage.Text = "�A�C���F�|�[�V�����̓��C�t�񕜂������˂����A�������������������̂킾���܂�͂������I";
                        ok.ShowDialog();
                        mainMessage.Text = MessageFormatForLana(1001) + "�t�t�t��";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�������E�E�E�����܂�̑䎌���Ɍ����₪���āE�E�E�o���Ă��A���i�B";
                        this.we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1001) + "�t�t�t��";
                    }
                }
                else
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "�A�C���F�ӂ��`�A��������������񂾂��A�ǂ����N���e�B�J�����C�}�C�`�o�˂��B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�́E�Z�E�m�E�̂ƈ�ʂ葵�����Ƃ��Ă��A��ԏd�v�ȗv�f�B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�Ō�Ɏc�����S���Ă��B���ǂǂ��e�����Ă���񂾁H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�S���̂��̂ˁB���ʃp�����^�t�o�ɂ͊֗^���Ȃ��́B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�S�ЂƂŐ��E���ς����Ă��B�܂��グ�Ăǂ��Ȃ�킯�ł�����܂��B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�����ˁB�R���Ƃ����ďグ��K�v�͂Ȃ���ˁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F����������ۖ��A�グ��K�v�͖�����ȁH";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�����v���񂾂�����A�グ�Ȃ��Ă��ǂ��񂶂�Ȃ��H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�ӂ��`�A�ǂ����[���������˂��ȁB";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�z���g����[���Ȃ���ˁA���Ⴀ�q���g��B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�����A���ނ��B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�͂P�A�Z�P�A�m�P�A�̂P�A�S�P�O�O�B�ǂ��H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�����O�E�S�u��������|�������ɂ˂��ȁE�E�E";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�悤����ɐS�����グ�Ă��Ӗ����Ȃ����Ă킯��B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���Ⴀ����ϕs�v���Ă��Ƃ��H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�͂S�O�A�Z�Q�T�A�m�P�O�A�̂Q�T�A�S�P�B�ǂ��H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���@���኱��͂����A��̂�ꂻ�����ȁB�����E�E�E";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F������A�R���Ƃ����ďグ��K�v�͂Ȃ����ǁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���ƂȂ����q���o�Ȃ������ȁB�����G��������}�Y�������ȁB";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�����@�B�S�͒��q�̗ǂ��������ď��ˁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�ǂ����������H�A�o�E�g�����Ă悭������˂��B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F������Ƙb�������Ȃ肻���ˁB�����̓_���W��������A���Ă��Ă�����Ď��Ł�";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�n�@�I�H����������҂Ă��āB��������B";
                        ok.ShowDialog();
                        UpdateMainMessage("���i�F���q�����܂܃_���W�����S�[�S�[��@�܂��̓|�[�V������ˁB");
                        GetPotionForLana();

                        mainMessage.Text = "�A�C���F�|�[�V�����̓��C�t�񕜂������˂����A�������������������̂킾���܂�͂������I";
                        ok.ShowDialog();
                        mainMessage.Text = MessageFormatForLana(1002) + "�t�t�t��";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�������E�E�E�����܂�̑䎌���Ɍ����₪���āE�E�E�o���Ă��A���i�B";
                        this.we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002) + "�t�t�t��";
                    }
                }
            }
            #endregion
            #region "�P�Q����"
            else if (this.firstDay >= 12 && !we.CommunicationLana12 && mc.Level >= 1 && knownTileInfo[2])
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "�A�C���F�悤�A�������_���ƎҁB";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�ǂ��������Ǝ҂�B�ւ�Ȍ���������t���Ȃ��ł�B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�X�}���A�ŁA���q�̗ǂ������Ƃ͂ǂ������Ӗ����H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�A�C���A���U���͂͂����Ȃ́H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F��H���`�ƂȁB" + Convert.ToString(this.mc.Strength + mc.MainWeapon.MinValue) + " - " + Convert.ToString(this.mc.Strength + mc.MainWeapon.MaxValue) + "���ȁB";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F���̍����ǂ������ɏk�܂�Ǝv���Ηǂ���B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�ő�U���͂̒l�ɂȂ�₷�����Ď����H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F���������ˁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���������͂ǂ������Ӗ����H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�A�C���̋Z�l�͂����H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F" + this.mc.Agility.ToString() + "���ȁB���ꂪ�ǂ��e�����Ă���H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F���Ƃ��΃����X�^�[�̋Z�l��20�������Ƃ����ˁB";
                        ok.ShowDialog();
                        int difference = this.mc.Agility - 20;
                        mainMessage.Text = "���i�F��������ƍ����������E�E�E" + difference.ToString();
                        ok.ShowDialog();
                        if (difference > 0)
                        {
                            mainMessage.Text = "���i�F��邶��Ȃ��A�A�C���搧�U������50%����ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������≽������Șb�������ȁB����ŐS���ǂ��e������񂾁H";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F���Ƃ���50%���ゾ���ǁA���̒l���̂��̂����������́B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�悭�킩��˂��ȁB�ǂ����ɂ���g50%����h�Ȃ񂾂�H";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F50%��艺�ł��搧�U���͂��܂ɏo����ł���H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F50%����ł��搧�U������鎞������ł���H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F���̃x�[�X�l���̂����m���Ȍ��ʂɌq������Ď���B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����璲�q�̗ǂ��������Ă��E�E�E�����悭�킩��˂����ǂȁB";
                            ok.ShowDialog();
                        }
                        else if (difference == 0)
                        {
                            mainMessage.Text = "���i�F���傤�ǃC�[�u���B�A�C���搧�U������50%���̂��̂ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������≽������Șb�������ȁB����ŐS���ǂ��e������񂾁H";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F50%�W���X�g�̏ꍇ�A3��A���Ő搧�U������邱�Ƃ������Ȃ��ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�m���ɕ����Ȃ̂ɁA�A�����Đ搧�����ƃC���C������ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�ł�50%��艺�ł��搧�U���͂��܂ɏo����ł���H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�ł�50%���ゾ�����Ƃ��Ă��搧�U������鎞������ł���H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F50%�W���X�g�ȊO�ł��A���̃x�[�X�l���̂����m���Ȍ��ʂɌq������Ď���B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����璲�q�̗ǂ��������Ă��E�E�E�����悭�킩��˂����ǂȁB";
                            ok.ShowDialog();
                        }
                        else
                        {
                            mainMessage.Text = "���i�F�A�C���搧�U������50%��艺���Ď��ɂȂ��ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������≽������Șb�������ȁB����ŐS���ǂ��e������񂾁H";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F���Ƃ���50%��艺�̏ꍇ�A���̒l���̂��̂��z�������́B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�悭�킩��˂��ȁB�ǂ����ɂ���g50%��艺�h�Ȃ񂾂�H";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F50%����ł��搧�U������鎞������ł���H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F50%��艺�ł��搧�U���͂��܂ɏo����ł���H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F���̃x�[�X�l���̂����m���Ȍ��ʂɌq������Ď���B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����璲�q�̗ǂ��������Ă��E�E�E�����悭�킩��˂����ǂȁB";
                            ok.ShowDialog();
                        }
                        mainMessage.Text = "���i�F�v����ɂ��������悤�ȏ��ɉe�����o�Ă�����Ď���B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���@��N���e�B�J���A�A�C�e���g�p�A�G����̃_���[�W�A�ȂǂȂǑS�Ă��H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�����A�S�Ăɒʂ��Ă���B���ǁA�S���Ⴂ�Ԃ͗��s�s�Ȑ퓬�P�[�X�ɉՂ܂�����ˁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���q�̗ǂ��������Ă��B���ƂȂ������A�����������B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F���������Ă��炦��Ə������B����Ɲh�R����ۂɔ��ɏd�v������B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�����A�S���b���Ă����Ƃ��邩�B";
                        ok.ShowDialog();
                        UpdateMainMessage("���i�F���āA�|�[�V�����n�����疾������܂��撣���Ă��Ă�ˁB");
                        GetPotionForLana();

                        mainMessage.Text = "�A�C���F�����A�C���Ƃ��I";
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                }
                else
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "�A�C���F�悤�A�������_���ƎҁB";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�ǂ��������Ǝ҂�B�ւ�Ȍ���������t���Ȃ��ł�B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�X�}���A�ŁA���q�̗ǂ������Ƃ͂ǂ������Ӗ����H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�A�C���A���U���͂͂����Ȃ́H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F��H���`�ƂȁB" + Convert.ToString(this.mc.Strength + mc.MainWeapon.MinValue) + " - " + Convert.ToString(this.mc.Strength + mc.MainWeapon.MaxValue) + "���ȁB";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F���̍����ǂ������ɏk�܂�Ǝv���Ηǂ���B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�ő�U���͂̒l�ɂȂ�₷�����Ď����H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F���������ˁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���������͂ǂ������Ӗ����H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�A�C���̋Z�l�͂����H";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F" + this.mc.Agility.ToString() + "���ȁB���ꂪ�ǂ��e�����Ă���H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F���Ƃ��΃����X�^�[�̋Z�l��20�������Ƃ����ˁB";
                        ok.ShowDialog();
                        int difference = this.mc.Agility - 20;
                        mainMessage.Text = "���i�F��������ƍ����������E�E�E" + difference.ToString();
                        ok.ShowDialog();
                        if (difference > 0)
                        {
                            mainMessage.Text = "���i�F��邶��Ȃ��A�A�C���搧�U������50%����ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������≽������Șb�������ȁB����ŐS���ǂ��e������񂾁H";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F���Ƃ���50%���ゾ���ǁA���̒l���̂��̂����������́B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�悭�킩��˂��ȁB�ǂ����ɂ���g50%����h�Ȃ񂾂�H";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F50%��艺�ł��搧�U���͂��܂ɏo����ł���H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F50%����ł��搧�U������鎞������ł���H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F���̃x�[�X�l���̂����m���Ȍ��ʂɌq������Ď���B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����璲�q�̗ǂ��������Ă��E�E�E�����悭�킩��˂����ǂȁB";
                            ok.ShowDialog();
                        }
                        else if (difference == 0)
                        {
                            mainMessage.Text = "���i�F���傤�ǃC�[�u���B�A�C���搧�U������50%���̂��̂ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������≽������Șb�������ȁB����ŐS���ǂ��e������񂾁H";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F50%�W���X�g�̏ꍇ�A3��A���Ő搧�U������邱�Ƃ������Ȃ��ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�m���ɕ����Ȃ̂ɁA�A�����Đ搧�����ƃC���C������ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�ł�50%��艺�ł��搧�U���͂��܂ɏo����ł���H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F�ł�50%���ゾ�����Ƃ��Ă��搧�U������鎞������ł���H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F50%�W���X�g�ȊO�ł��A���̃x�[�X�l���̂����m���Ȍ��ʂɌq������Ď���B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����璲�q�̗ǂ��������Ă��E�E�E�����悭�킩��˂����ǂȁB";
                            ok.ShowDialog();
                        }
                        else
                        {
                            mainMessage.Text = "���i�F�A�C���搧�U������50%��艺���Ď��ɂȂ��ˁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������≽������Șb�������ȁB����ŐS���ǂ��e������񂾁H";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F���Ƃ���50%��艺�̏ꍇ�A���̒l���̂��̂��z�������́B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�悭�킩��˂��ȁB�ǂ����ɂ���g50%��艺�h�Ȃ񂾂�H";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F50%����ł��搧�U������鎞������ł���H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F50%��艺�ł��搧�U���͂��܂ɏo����ł���H";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�������ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "���i�F���̃x�[�X�l���̂����m���Ȍ��ʂɌq������Ď���B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����璲�q�̗ǂ��������Ă��E�E�E�����悭�킩��˂����ǂȁB";
                            ok.ShowDialog();
                        }
                        mainMessage.Text = "���i�F�v����ɂ��������悤�ȏ��ɉe�����o�Ă�����Ď���B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���@��N���e�B�J���A�A�C�e���g�p�A�G����̃_���[�W�A�ȂǂȂǑS�Ă��H";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F�����A�S�Ăɒʂ��Ă���B���ǁA�S���Ⴂ�Ԃ͗��s�s�Ȑ퓬�P�[�X�ɉՂ܂�����ˁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���q�̗ǂ��������Ă��B���ƂȂ������A�����������B";
                        ok.ShowDialog();
                        mainMessage.Text = "���i�F���������Ă��炦��Ə������B����Ɲh�R����ۂɔ��ɏd�v������B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�����A�S���b���Ă����Ƃ��邩�B";
                        ok.ShowDialog();
                        UpdateMainMessage("���i�F���āA�|�[�V�����n������_���W�����撣���Ă��Ă�ˁB");
                        GetPotionForLana();

                        mainMessage.Text = "�A�C���F�����A�C���Ƃ��I";
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "�P�R����"
            else if (this.firstDay >= 13 && !we.CommunicationLana13 && mc.Level >= 5 && knownTileInfo[2])
            {
                if (!we.AlreadyCommunicate)
                {
                    UpdateMainMessage("�A�C���F�́A�Z�A�m�A�́A�S�A����łT�S�đ������ȁB");

                    UpdateMainMessage("���i�F�A�C���A���ǂǂ���d�_�ŏグ�Ă����̂�H");

                    UpdateMainMessage("�A�C���F���\�Y�܂�����ȁB���������炢���Ă݂邩�B");

                    UpdateMainMessage("�A�C���F�́B�@�����U���͂t�o�B");

                    UpdateMainMessage("�A�C���F�Z�B�@�N���e�B�J���q�b�g���A�搧�U�����B");

                    UpdateMainMessage("���i�F�Z�p�����^�ňЗ͂��オ��X�L���������B�Y��Ȃ��悤�Ɂ�");

                    UpdateMainMessage("�A�C���F�m�B�@���@�U���͂t�o�B");

                    UpdateMainMessage("�A�C���F�S�B�@�����B");

                    UpdateMainMessage("���i�F�������āA�����炢�ɂȂ��ĂȂ�����Ȃ��B");

                    UpdateMainMessage("�A�C���F�S�B�@�e��p�����^�ɂ���Ĉ����o�����l�̃u���𖳂����B");

                    UpdateMainMessage("���i�F����Ă��Ɍ����Ă��܂��΂����ˁB");

                    UpdateMainMessage("�A�C���F���ǂǂꂪ�v�邩���Ď������E�E�E");

                    using (SelectTarget st = new SelectTarget())
                    {
                        st.FirstName = "��";
                        st.SecondName = "�Z";
                        st.ThirdName = "�m";
                        st.FourthName = "��";
                        st.FifthName = "�S";
                        st.MaxSelectable = 5;
                        st.StartPosition = FormStartPosition.CenterParent;
                        st.ShowDialog();
                        switch (st.TargetNum)
                        {
                            case 1:
                                UpdateMainMessage("�A�C���F����ς�ǂ��l���Ă��͂���B���̍l���͕ς��Ȃ����B");

                                UpdateMainMessage("���i�F�����ˁA��O�͂��邯�Ǖ����U���ŉ����Ă����̂͊�{���̊�{��B");

                                UpdateMainMessage("�A�C���F�Ȃ񂾁A���P�ɑf�����ȁH");

                                UpdateMainMessage("���i�F����̂܂܂̎����������Ă邾����B�͖����ł��̋ƊE�͂���Ă����Ȃ���B");

                                UpdateMainMessage("�A�C���F�ƊE���Ă��B�܂��A�͔͂C���Ă������ĂƂ��낾�I�b�n�b�n�b�n�I");
                                break;

                            case 2:
                                UpdateMainMessage("�A�C���F�ӊO�ƋZ���ŋ��Ȃ񂶂�˂��̂��H");

                                UpdateMainMessage("���i�F���ŋ^��n�ɂ��Ă�̂�B�Z��������͂ɂ͂Ȃ�Ȃ����H");

                                UpdateMainMessage("�A�C���F�܂����������ǂȁB�X�g���[�g�E�X�}�b�V���g���ꍇ�Ȃǂ͂�����x�X�s�[�h���v��B");

                                UpdateMainMessage("�A�C���F���ɂ��N���e�B�J���R�{��_�����́A�͂�������_������B");

                                UpdateMainMessage("���i�F���\�׋����Ă�̂ˁB�Z���オ��΂��ꂾ���퓬��D�ʂɐi�߂��鎖�͊m����B");

                                UpdateMainMessage("�A�C���F���K���A�A�h�o���e�[�W�A��t���v���b�V���[�ȂǑ����Ă݂�����ȁB");

                                UpdateMainMessage("���i�F�b�t�t�A�ǂ��񂶂�Ȃ��H���������̂���̂�肩�����Ǝv����B");
                                break;

                            case 3:
                                UpdateMainMessage("�A�C���F�m�̓o���o���グ�Ă��A�q�[�����܂���A�Ƃ��ǂ����H");

                                UpdateMainMessage("���i�F�����ˁA�������Ă����΂����ȒP�ɂ��ꂽ��͂��Ȃ��Ȃ��ˁB");

                                UpdateMainMessage("�A�C���F�t�@�C�A�E�{�[�����З͂��オ�邵�ȁB�����A����͈�Γ񒹂���˂����I�H");

                                UpdateMainMessage("���i�F�}�i��������ɂ���΁A�_���[�W���[�X�̓R���g���[�����₷���A�ꗝ�����B");

                                UpdateMainMessage("�A�C���F���i�A���O���@�p�ȊO�Ɍ����p�������o������ȁB�ǂ������ƃ`�F���W���Ȃ����H");

                                UpdateMainMessage("���i�F�͂������S�Ă���Ȃ������́H�C����������ĂˁE�E�E�{�C������B");

                                UpdateMainMessage("�A�C���F�_���[�W��������Ȃ�A������͂��B�ǂ����H");

                                UpdateMainMessage("���i�F�܂��A�l���Ƃ��Ă������");

                                UpdateMainMessage("�A�C���F�����A�����q�[�����ɓO���āA���i���u���u�������̂������˂������ȁB");
                                break;

                            case 4:
                                UpdateMainMessage("�A�C���F�E�E�E����A�̗͂͂���Ӗ��ŋ������A����͖����ȁB");

                                UpdateMainMessage("���i�F����Ӗ����Ăǂ���������H");

                                UpdateMainMessage("�A�C���F�퓬���Ă����̂́A�����Еt���Ȃ��ƑʖڂȂ񂾁B");

                                UpdateMainMessage("���i�F�Еt���Ȃ��ƑʖڂȂ́H�t�@���l�̏ꍇ�A��퓬��ԂɎ������ނ��Ă悭�����b��B");

                                UpdateMainMessage("�A�C���F����́E�E�E�Ȃ�����ʎ������B�t�@�����ܓ��L�̂��̂ŁA���ɏo����㕨����Ȃ��ȁB");

                                UpdateMainMessage("�A�C���F�����A�_���W�����̃����X�^�[�͓|���Ȃ��Ƒʖڂ��B");

                                UpdateMainMessage("�A�C���F�̗͂�����΁A���C�t���オ�镪�A�����ȒP�ɂ���Ȃ��Ȃ�B");

                                UpdateMainMessage("�A�C���F�����A���ꂾ�����B���ꂾ������G�͓|���˂��B");

                                UpdateMainMessage("���i�F�����͂��Ȃ����ǁA���������Ȃ����ď��H");

                                UpdateMainMessage("���i�F��������邪�AFiveSeeker�B�̂��ꂼ��̓����A�o���Ă邩�H");

                                UpdateMainMessage("���i�F�����G���~�l�͑S�\�́B�t�@���l�͑����S�B");

                                UpdateMainMessage("���i�F�J�[���݂͑�z�����m�́A���F���[����͐_�Ƃ̋Z�p�B");

                                UpdateMainMessage("���i�F�����f�B�X���t������́A���킸���Ȃ���́E�E�E�����B");

                                UpdateMainMessage("�A�C���F�������A�̗͂��Ă̂͐�ΕK�{�����A����܂ł��Ď����B");

                                UpdateMainMessage("���i�F�ł��A�̗͂������Ƃ������ꂿ�Ⴄ����ˁB�グ�Ă����ɉz�������͂Ȃ���B");

                                UpdateMainMessage("�A�C���F�������ȁB�グ�Ȃ��킯�ɂ������˂�����A�K�x�ɏグ���ď��ɂ��Ă������B");
                                break;

                            case 5:
                                UpdateMainMessage("�A�C���F�S���グ��ƁA�ǂ������킯���ŋ��ɂȂ����肵�ĂȁB");

                                UpdateMainMessage("���i�F���ő��l���䎌�݂����ȕ\���Ȃ̂�B�����Ƃ�����");

                                UpdateMainMessage("�A�C���F�S�����グ�Ă����ʂ͂˂��E�E�E����H�킩���Ă���āB");

                                UpdateMainMessage("�A�C���F�����A�����f�B�̃{�P����K�������Ƃ����邪");

                                UpdateMainMessage("�����f�B�X�F�w�Ă߂��A���̐U����A�X�b�J�X�J���ȁI�I�@�h���Ă˂������S�R�|���˂��񂾂�I�I�x");

                                UpdateMainMessage("�����f�B�X�F�w�Ƃ����킯�ł��B�@�����P�O�؂񎀂�ł����₠�������������I�I�I�I�x");

                                UpdateMainMessage("���i�F�X�b�J�X�J���ĐS�̂��ƁH");

                                UpdateMainMessage("�A�C���F�����A�����������B���̂��Ƃ͍����������B�}�W�łP�O�A�s���B");

                                UpdateMainMessage("�A�C���F�����f�B�̂�A����{�C�Ȃ񂾂�B�蔲���E���K�E����{���Ă����̂��S�R�˂��B");

                                UpdateMainMessage("���i�F�S���グ�Ă����ƁA��ɖ{�C�E�S�́E�^�C�S�J���ď�ԂɂȂ�̂����ˁB");

                                UpdateMainMessage("�A�C���F�������������B���͂���ɓq���Ă݂邺�B���i�A�y���݂ɂ��ĂĂ���B");
                                break;
                        }
                    }

                    UpdateMainMessage("���i�F���Ⴀ�A�͂��|�[�V�����B");
                    GetPotionForLana();

                    mainMessage.Text = "�A�C���F�����T���L���[�B";
                    we.AlreadyCommunicate = true;
                }
                else
                {
                    if (!we.AlreadyRest)
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "�P�S����"
            else if (this.firstDay >= 14 && !we.CommunicationLana14 && mc.Level >= 5 && knownTileInfo[2])
            {
                if (!we.AlreadyCommunicate)
                {
                    UpdateMainMessage("�A�C���F�v�X�ɑM�������I�_���W�����U���@�I�I");

                    UpdateMainMessage("���i�F���H�@���̃}�g�b�N�Ō@��Ԃ��̂Œ��肽�񂶂�Ȃ������́H");

                    UpdateMainMessage("�A�C���F�����������B�U���Ȃ�đ��ɂ�����͂����B��������H");

                    UpdateMainMessage("�A�C���F���������I�p�b�p���p�[���I�I�I");

                    UpdateMainMessage("���i�F���񂽂̓����p�b�p���p���ł���E�E�E������H");

                    UpdateMainMessage("�A�C���F�_�E�W���O����B�������i�m��Ȃ��̂��H���傤���˂��A�C���搶���ڂ���������Ă���B");

                    UpdateMainMessage("�A�C���F�n������M�����Ȃǂ̍z���A�B�ꂽ�u�c�����̖_��U��q�Ȃǂ̑��u�̓����ɂ���Č�����̂��I");

                    UpdateMainMessage("�A�C���F�b�t�A�ǂ������B�˂����݂͖������B�ǂ����Ԃ����t���˂��悤���ȁI�b�n�b�n�b�n�I");

                    UpdateMainMessage("���i�F�u�Ԃ����t�������v���āA���������Ӗ�����Ȃ����E�E�E");

                    UpdateMainMessage("�A�C���F���������I�����āA�����������Ă݂邩�I���Ă�惉�i�B���ŉ��w�̏ꏊ�������邩��ȁI");

                    UpdateMainMessage("���i�F�E�E�E�i�����ځj");

                    UpdateMainMessage("�E�E�E�E�E");

                    UpdateMainMessage("�E�E�E�E");

                    UpdateMainMessage("�E�E�E");

                    UpdateMainMessage("�E�E");

                    UpdateMainMessage("�A�C���F���������E�E�E�ق�E�E�E");

                    UpdateMainMessage("        �w�A�C���̓u���b�N�}�e���A���𔭌@�����B�x");

                    UpdateMainMessage("���i�F�u���b�N�}�e���A���A�F�t���}�e���A�������H������Ŕp�������E�E�E");

                    UpdateMainMessage("�A�C���F�E�E�E�E�E�E�����A����ȏ�͌����ȁI�I");

                    UpdateMainMessage("���i�F�E�E�E�͂��A�|�[�V�����B");
                    GetPotionForLana();

                    we.AlreadyCommunicate = true;
                }
                else
                {
                    if (!we.AlreadyRest)
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "�P�T����"
            else if (this.firstDay >= 15 && !we.CommunicationLana15 && mc.Level >= 5 && knownTileInfo[2])
            {
                if (!we.AlreadyCommunicate)
                {
                    UpdateMainMessage("���i�F���v���̂�ˁB���̂܂܂��Ⴂ���Ȃ����āB");

                    UpdateMainMessage("�A�C���F�������B�̏d���H");

                    UpdateMainMessage("        �w�h�O�b�V���A�A�A�@�@�I�I�I�i�G�^�[�i���u���[���A�C�����y��j�x");

                    UpdateMainMessage("�A�C���F�E�E�E�E�E�E�@�@�@�i�b�p�^�j");

                    UpdateMainMessage("���i�F��w�̒m����B�ƁA���p�E���@�p�̗������ĂȂ��Ȃ�����̂�B");

                    UpdateMainMessage("���i�F�򑐎��ƒ����΂��������Ă�ƁA�퓬�̃J������������Ă����̂�����B");

                    UpdateMainMessage("���i�F���i�������Ă��Ȃ��ƁA���i����Ă���΂���ɕ΂�������ĂˁB");

                    UpdateMainMessage("���i�F�������Ă��܂ɃA�C���Ɍ����p�ł�����ĂȂ��ƂȂ܂����Ⴄ���P��B�ˁA�����Ă�H");

                    UpdateMainMessage("�A�C���F�E�E�E�E�E�E");

                    UpdateMainMessage("���i�F���[���A�����̓|�[�V�����v��Ȃ��̂�����H");

                    UpdateMainMessage("�A�C���F�v��B");

                    UpdateMainMessage("���i�F�N���Ă�񂾂�����A�����Ǝ󂯓������Ȃ�����B");

                    UpdateMainMessage("�A�C���F���O�̂��̕��p�A�\���r�͗����Ă˂��Ǝv���񂾂��E�E�E");

                    if (!we.CompleteArea1)
                    {
                        UpdateMainMessage("���i�F�@�i�˂��A�����_���W�����ɍs�����Č�������E�E�E�A�C���ǂ��H�j");
                    }
                    else
                    {
                        UpdateMainMessage("���i�F�@�i�˂��A�����_���W�����Ō��퓬�p���E�E�E��{�ɂ��ėǂ��H�j");
                    }

                    UpdateMainMessage("�A�C���F�́H�@�Ȃ񂩁A�������Ȃ������B�Ȃ񂾂��āH");

                    UpdateMainMessage("        �w�{�M���A�A�A�@�@�@�I�I�I�i�t�@���g���L�b�N���A�C�����y��j�x");

                    UpdateMainMessage("�A�C���F�Q�z�I�H�H�H�E�E�E����A�ˑR�������������āE�E�E�ǁA�ǂ�����ƁE�E�E");

                    UpdateMainMessage("�A�C���F�E�E�E�E�E�E�@�@�@�i�b�p�^�j");
                    GetPotionForLana();
                    we.AlreadyCommunicate = true;
                }
                else
                {
                    if (!we.AlreadyRest)
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "�P�U����"
            else if (this.firstDay >= 16 && !we.CommunicationLana16 && !we.CompleteSlayBoss1 && mc.Level >= 8 && knownTileInfo[386])
            {
                if (!we.AlreadyCommunicate)
                {
                    UpdateMainMessage("�A�C���F�b�Z�C�I�b�n�I");

                    UpdateMainMessage("���i�F�����A�������o�邶��Ȃ��B�A�C���B���q�͂ǂ��Ȃ́H");

                    UpdateMainMessage("�A�C���F�_���W�����P�K�����\�i��ł�Ǝv���B���ƈ�������I");

                    UpdateMainMessage("���i�F�A�C���A�y���ҁz���ĕ�����������H");

                    UpdateMainMessage("�A�C���F�����A���荇�킹�����������邺�B�A�C�c�Ƃ�ł��Ȃ��������B");

                    UpdateMainMessage("���i�F�y���ҁz�͂������̐�p�p�^�[����p���Ă��邻��������A�C�����ĂˁB");

                    UpdateMainMessage("�A�C���F�������̐�p�p�^�[���H���������H");

                    UpdateMainMessage("���i�F���C�t�������Ă�����H");

                    UpdateMainMessage("�A�C���F�t���b�V���E�q�[���B");

                    UpdateMainMessage("���i�F���C�t�������ĂȂ��āA�X�L���|�C���g������悤�Ȃ�H");

                    UpdateMainMessage("�A�C���F�X�g���[�g�E�X�}�b�V���B");

                    UpdateMainMessage("���i�F��̂���Ȋ�����B");

                    UpdateMainMessage("�A�C���F�Ȃ�قǂȁA���낢�닳���Ă���ăT���L���[�I");

                    UpdateMainMessage("���i�F�y���ҁz��|�����ŁA���̊K�w�֐i�߂�ƕ��������Ƃ������B���������ˁB");

                    UpdateMainMessage("�A�C���F�����A���x�����A�C�c���Ԃ��|���Ă�邺�I");

                    UpdateMainMessage("���i�F�b�t�t�A���̂�����B�͂��A�|�[�V������");
                    GetPotionForLana();
                    we.AlreadyCommunicate = true;
                }
                else
                {
                    if (!we.AlreadyRest)
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "�Ō�̓�"
            else if (we.CommunicationCompArea4 && we.TruthWord5 && !we.CommunicationLana100)
            {
                we.CommunicationLana100 = true;
                if (!we.AlreadyCommunicate)
                {
                    UpdateMainMessage("���i�F�ŉ��w�̊ŔA�y���z���E�z���ĈӖ��A�A�C���ɂ͕�����H");

                    UpdateMainMessage("�A�C���F�����A���ƂȂ������ȁB");

                    UpdateMainMessage("�A�C���F���ƂȂ������������B����̎�����H");

                    UpdateMainMessage("���i�F����A�A�C�������������֌W���Ă�̂͊ԈႢ�Ȃ�������B");

                    UpdateMainMessage("�A�C���F���ƁA���������T�낤�Ƃ������ɗ���Ђǂ����ɂ��������B");

                    UpdateMainMessage("�A�C���F���ꂩ��A���i�B���O���������w���߂�Ȃ����q�t�r�g�x���֌W���肻�����ȁB");

                    UpdateMainMessage("���i�F�A�C���A���ˁB");

                    UpdateMainMessage("�A�C���F�ǂ��񂾁A���i�B�ŉ��w�A���e���悤���B���̌�A�t�@�[�W���{�a�s�����ȁB");

                    UpdateMainMessage("���i�F�����A�����ˁB�t�@�[�W���{�a�A�܂��s���܂����@�͂��A�|�[�V�����B");
                    GetPotionForLana();

                    UpdateMainMessage("�A�C���F�T���L���[�I");

                    we.AlreadyCommunicate = true;
                }
                else
                {
                    if (!we.AlreadyRest)
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "X����"
            else
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        if (sc != null && sc.EmotionAngry)
                        {
                            UpdateMainMessage("���i�F�E�E�E�ԃ|�[�V������B");
                            GetPotionForLana();

                            UpdateMainMessage("�A�C���F���A�����A�s���Ă��邺�B");

                            UpdateMainMessage("���i�F�E�E�E�E�E�E");

                            UpdateMainMessage("�A�C���F�E�E�E", true);
                        }
                        else
                        {
                            UpdateMainMessage("���i�F�ǂ��H���q�̂ق��́H");

                            UpdateMainMessage("�A�C���F���R���R�B�C���Ƃ����āI�b�n�b�n�b�n�I");

                            UpdateMainMessage("���i�F�����ǂ��C����̂��E�E�E�z���A�����̐ԃ|�[�V������B");
                            GetPotionForLana();

                            UpdateMainMessage("�A�C���F�I�b�A�T���L���I����ł܂��������撣���Ă��邺�B", true);
                        }
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        if (sc != null && sc.EmotionAngry)
                        {
                            mainMessage.Text = "���i�F����E�E�E�x�߂΁H";
                        }
                        else
                        {
                            mainMessage.Text = MessageFormatForLana(1001);
                        }
                    }
                }
                else
                {
                    if (!we.AlreadyCommunicate)
                    {
                        if (sc != null && sc.EmotionAngry)
                        {
                            UpdateMainMessage("���i�F�E�E�E�ԃ|�[�V������B");
                            GetPotionForLana();

                            UpdateMainMessage("�A�C���F���A�����A�s���Ă��邺�B");

                            UpdateMainMessage("���i�F�E�E�E�E�E�E");

                            UpdateMainMessage("�A�C���F�E�E�E", true);
                        }
                        else
                        {
                            UpdateMainMessage("���i�F�͂��A�����̐ԃ|�[�V�����B");
                            GetPotionForLana();

                            UpdateMainMessage("�A�C���F�T���L���I���Ⴂ���Ă���I");

                            UpdateMainMessage("���i�F���ȂȂ��悤�ɂ���΂鎖�ˁB", true);
                        }
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        if (sc != null && sc.EmotionAngry)
                        {
                            mainMessage.Text = "���i�F�E�E�E����ɍs���Ă����";
                        }
                        else
                        {
                            mainMessage.Text = MessageFormatForLana(1002);
                        }
                    }
                }
            }
            #endregion

        }

        /// <summary>
        /// if-else���ƃt���O�𕡎G�������ăK���c�f������Ƃ̉�b�𐷂�グ�Ă��������B
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            int firstOpenDay = 3;
            #region "�P���� - �Q����"
            if (this.firstDay >= 1 && this.firstDay <= (firstOpenDay-1) && mc.Level >= 1 && knownTileInfo[2] &&
                 (!we.CommunicationGanz1 ||
                  !we.CommunicationGanz2/* ||
                  !we.CommunicationGanz3 ||
                  !we.CommunicationGanz4*/))
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyEquipShop)
                    {
                        if (!we.CommunicationGanz1 && !we.CommunicationGanz2) // && !we.CommunicationGanz3 && !we.CommunicationGanz4)
                        {
                            mainMessage.Text = "�A�C���F�K���c�f������A���܂����H";
                            ok.ShowDialog();
                            mainMessage.Text = "�K���c�F���ށA���������ő҂��Ă���B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�E�E�E�E�E";
                            ok.ShowDialog();
                            mainMessage.Text = "�E�E�E�E";
                            ok.ShowDialog();
                            mainMessage.Text = "�E�E�E";
                            ok.ShowDialog();
                            mainMessage.Text = "�K���c�F�҂������ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�ǂ��i�͑����Ă邩�H";
                            ok.ShowDialog();
                            mainMessage.Text = "�K���c�F���͂������V���������`���҂��S�Ĕ���߂Ă��܂��������B���܂�ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����I�H�I�H����̔���߂��Ăǂ�����������I�H";
                            ok.ShowDialog();
                            mainMessage.Text = "�K���c�F��ʓI�ȕ�����A���ꋉ�i�̕��܂őS�Ď����Ă����������B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�}�W����E�E�E���̓��ׂ͉����ɂȂ�񂾁H";
                            ok.ShowDialog();
                            if (firstOpenDay - we.GameDay <= 0)
                            {
                                UpdateMainMessage("�K���c�F�S�z����ł��A���łɓ��׍ς݁B���������Ă��鏊���A�_���W�����ł��s���ė��邪�ǂ��B");

                                UpdateMainMessage("�A�C���F�{�����I�H��������I���Ⴀ�_���W�����s���ė��邩��A�A���Ă����猩���Ă����I");

                                UpdateMainMessage("�K���c�F�񌾂͂Ȃ��B�͂₭�s���ė��Ȃ����B");

                                UpdateMainMessage("�A�C���F�����I�I");
                            }
                            else
                            {
                                mainMessage.Text = "�K���c�F����" + Convert.ToString(firstOpenDay - we.GameDay) + "���͑҂��Ă���B";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F�������A����߂Ȃ�Ă��郄�c�̋C���m��˂��ȁB";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F�܂����傤���˂��A�܂����邺�I";
                                ok.ShowDialog();
                                mainMessage.Text = "�K���c�F���׎���A������B���̎��ɂ܂����邪�����B";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F�����I�I";
                            }
                            we.AlreadyEquipShop = true;
                        }
                        else
                        {
                            UpdateMainMessage("�A�C���F�K���c�f������A�����܂��H");

                            if (firstOpenDay - we.GameDay <= 0)
                            {
                                UpdateMainMessage("�K���c�F���׍ς݂ŁA���������Ă��鏊���A�_���W�����ł��s���ė��邪�ǂ��B");

                                UpdateMainMessage("�A�C���F�{�����I�H��������I���Ⴀ�_���W�����s���ė��邩��A�A���Ă����猩���Ă����I");

                                UpdateMainMessage("�K���c�F�񌾂͂Ȃ��B�͂₭�s���ė��Ȃ����B");

                                UpdateMainMessage("�A�C���F�����I�I");
                            }
                            else
                            {
                                UpdateMainMessage("�K���c�F����" + Convert.ToString(firstOpenDay - we.GameDay) + "���͑҂��Ă���B");

                                UpdateMainMessage("�A�C���F�I�[�P�[�A�����B");
                            }

                            we.AlreadyEquipShop = true;
                        }
                    }
                    else
                    {
                        if (firstOpenDay - we.GameDay <= 0)
                        {
                            UpdateMainMessage("�K���c�F���׍ς݂ŁA���������Ă��鏊���A�_���W�����ł��s���ė��邪�ǂ��B");
                        }
                        else
                        {
                            mainMessage.Text = "�K���c�F����" + Convert.ToString(firstOpenDay - we.GameDay) + "���҂��Ă���B";
                        }
                    }
                }
                else
                {
                    if (!we.AlreadyEquipShop)
                    {
                        if (!we.CommunicationGanz1 && !we.CommunicationGanz2) // && !we.CommunicationGanz3 && !we.CommunicationGanz4)
                        {
                            mainMessage.Text = "�A�C���F�K���c�f������A���܂����H";
                            ok.ShowDialog();
                            mainMessage.Text = "�K���c�F���ށA���������ő҂��Ă���B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����B";
                            ok.ShowDialog();
                            mainMessage.Text = "�E�E�E�E�E";
                            ok.ShowDialog();
                            mainMessage.Text = "�E�E�E�E";
                            ok.ShowDialog();
                            mainMessage.Text = "�E�E�E";
                            ok.ShowDialog();
                            mainMessage.Text = "�K���c�F�҂������ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�ǂ��i�͑����Ă邩�H";
                            ok.ShowDialog();
                            mainMessage.Text = "�K���c�F���͂������V���������`���҂��S�Ĕ���߂Ă��܂��������B���܂�ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�����I�H�I�H����̔���߂��Ăǂ�����������I�H";
                            ok.ShowDialog();
                            mainMessage.Text = "�K���c�F��ʓI�ȕ�����A���ꋉ�i�̕��܂őS�Ď����Ă����������B";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�}�W����E�E�E���̓��ׂ͉����ɂȂ�񂾁H";
                            ok.ShowDialog();
                            if (firstOpenDay - we.GameDay <= 0)
                            {
                                UpdateMainMessage("�K���c�F�S�z����ł��A���łɓ��׍ς݁B���������Ă��鏊���A�_���W�����ł��s���ė��邪�ǂ��B");

                                UpdateMainMessage("�A�C���F�{�����I�H��������I���Ⴀ�_���W�����s���ė��邩��A�A���Ă����猩���Ă����I");

                                UpdateMainMessage("�K���c�F�񌾂͂Ȃ��B�͂₭�s���ė��Ȃ����B");

                                UpdateMainMessage("�A�C���F�����I�I");
                            }
                            else
                            {
                                mainMessage.Text = "�K���c�F����" + Convert.ToString(firstOpenDay - we.GameDay) + "���͑҂��Ă���B";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F�������A����߂Ȃ�Ă��郄�c�̋C���m��˂��ȁB";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F�܂����傤���˂��A�܂����邺�I";
                                ok.ShowDialog();
                                mainMessage.Text = "�K���c�F���׎���A������B���̎��ɂ܂����邪�����B";
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F�����I�I";
                            }
                            we.AlreadyEquipShop = true;
                        }
                        else
                        {
                            UpdateMainMessage("�A�C���F�K���c�f������A�����܂��H");

                            if (firstOpenDay - we.GameDay <= 0)
                            {
                                UpdateMainMessage("�K���c�F���׍ς݂ŁA���������Ă��鏊���A�_���W�����ł��s���ė��邪�ǂ��B");

                                UpdateMainMessage("�A�C���F�{�����I�H��������I���Ⴀ�_���W�����s���ė��邩��A�A���Ă����猩���Ă����I");

                                UpdateMainMessage("�K���c�F�񌾂͂Ȃ��B�͂₭�s���ė��Ȃ����B");

                                UpdateMainMessage("�A�C���F�����I�I");
                            }
                            else
                            {
                                UpdateMainMessage("�K���c�F����" + Convert.ToString(firstOpenDay - we.GameDay) + "���͑҂��Ă���B");

                                UpdateMainMessage("�A�C���F�I�[�P�[�A�����B");
                            }

                            we.AlreadyEquipShop = true;
                        }
                    }
                    else
                    {
                        if (firstOpenDay - we.GameDay <= 0)
                        {
                            UpdateMainMessage("�K���c�F���׍ς݂ŁA���������Ă��鏊���A�_���W�����ł��s���ė��邪�ǂ��B");
                        }
                        else
                        {
                            mainMessage.Text = "�K���c�F����" + Convert.ToString(firstOpenDay - we.GameDay) + "���҂��Ă���B";
                        }
                    }
                }
            }
            #endregion
            #region "�R����"
            else if (this.firstDay >= firstOpenDay && !we.CommunicationGanz3/*5*/ && mc.Level >= 1 && knownTileInfo[2])
            {
                we.CommunicationGanz3 = true;
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyEquipShop)
                    {
                        mainMessage.Text = "�A�C���F�K���c�f������A���X�A�󂢂Ă܂����H";
                        ok.ShowDialog();
                        mainMessage.Text = "�K���c�F�����A�悭�����ȃA�C���B�X�̕��͋󂢂Ă��邼�B�D���Ȃ������Ă������ǂ��B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�������I�����q�������Ă��炤���I";
                        ok.ShowDialog();
                        mainMessage.Text = "�K���c�F�������A���������Ă͂Ȃ񂾂��B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F��H";
                        ok.ShowDialog();
                        if (!we.CommunicationGanz1 && !we.CommunicationGanz2 && !we.CommunicationGanz3 && !we.CommunicationGanz4)
                        {
                            UpdateMainMessage("�K���c�F�ȑO�A����̕i�����S�Ĕ���؂�Ă��܂��ĂȁB");

                            UpdateMainMessage("�K���c�F�V�������B�����i�������܂��X�ɂ͏o����B");
                        }
                        else
                        {
                            mainMessage.Text = "�K���c�F�����̓s�ł�����N���F���^�X�B�����̒b�艮���@�X�^��H���E�E�E";
                            ok.ShowDialog();
                            mainMessage.Text = "���@�X�^�F�w�N����I����̔���߂Ȃ񂼂��郄�c�́B���̃��_�g������ȁB�x";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F���Ɠ����������Ă�ȁB�b�n�b�n�b�n�I";
                            ok.ShowDialog();
                            mainMessage.Text = "���@�X�^�F�w�{���R���Ԃł͓��ꖳ�������A�K���c�B���O�Ƃ͌Â��t���������Ⴉ��́B�x";
                            ok.ShowDialog();
                            mainMessage.Text = "�K���c�F����������ŁA�{���Ɋ�{�I�ȕ�����𑵂��Ă�������B";
                            ok.ShowDialog();
                        }

                        UpdateMainMessage("�A�C���F������A���ꂾ���ł��\�������B�{���ɏ������B");

                        UpdateMainMessage("�K���c�F�i���͏��Ȃ����A�g����㕨�΂��肾�B���Ă����Ă���B");

                        we.AvailableEquipShop = true;
                        CallEquipmentShop();

                        mainMessage.Text = "";
                        we.AlreadyEquipShop = true;
                    }
                    else
                    {
                        we.AvailableEquipShop = true;
                        CallEquipmentShop();
                        mainMessage.Text = "";
                    }
                }
                else
                {
                    if (!we.AlreadyEquipShop)
                    {
                        mainMessage.Text = "�A�C���F�񑩂̂R���Ԃ����B";
                        ok.ShowDialog();
                        mainMessage.Text = "�K���c�F�����A�悭�����ȃA�C���B���҂��������ȁB";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�������I�����q�������Ă��炤���I";
                        ok.ShowDialog();
                        mainMessage.Text = "�K���c�F�������A���������Ă͂Ȃ񂾂��B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F��H�����H";
                        ok.ShowDialog();
                        mainMessage.Text = "�K���c�F�����̓s�ł�����N���F���^�X�B�����̒b�艮���@�X�^��H���E�E�E";
                        ok.ShowDialog();
                        mainMessage.Text = "���@�X�^�F�w�N����I����̔���߂Ȃ񂼂��郄�c�́B���̃��_�g������ȁB�x";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F���Ɠ����������Ă�ȁB�b�n�b�n�b�n�I";
                        ok.ShowDialog();
                        mainMessage.Text = "���@�X�^�F�w�{���R���Ԃł͓��ꖳ�������A�K���c�B���O�Ƃ͌Â��t���������Ⴉ��́B�x";
                        ok.ShowDialog();
                        mainMessage.Text = "�K���c�F����������ŁA�{���Ɋ�{�I�ȕ�����𑵂��Ă�������B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F������A���ꂾ���ł��\�������B�{���ɏ������B";
                        ok.ShowDialog();
                        mainMessage.Text = "�K���c�F�i���͏��Ȃ����A�g����㕨�΂��肾�B���Ă����Ă���B";
                        ok.ShowDialog();
                        mainMessage.Text = "�A�C���F�����A�����Ȃ��������Ă��炤���B";
                        ok.ShowDialog();

                        we.AvailableEquipShop = true;
                        CallEquipmentShop();

                        mainMessage.Text = "";
                        we.AlreadyEquipShop = true;
                    }
                    else
                    {
                        we.AvailableEquipShop = true;
                        CallEquipmentShop();
                        mainMessage.Text = "";
                    }
                }
            }
            #endregion
            #region "�P�K���e��A�k�u�Q����̔�"
            else if (we.CommunicationCompArea1 && !we.AvailableEquipShop2)
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyEquipShop)
                    {
                        UpdateMainMessage("�A�C���F���񂿂�[�B");

                        UpdateMainMessage("�K���c�F���ށA�A�C�����B���܂񂪁A���͏��������B");
                        
                        UpdateMainMessage("�A�C���F��H�����Ȃ̂��H");
                        
                        UpdateMainMessage("�K���c�F�����ɂȂ�΁A�����ɂ܂��J�X�o����B���̎��ɗ��Ă���B");
                        
                        UpdateMainMessage("�A�C���F�I�[�P�[�A�����B");

                        we.AlreadyEquipShop = true;
                    }
                    else
                    {
                        UpdateMainMessage("�K���c�F�����ɂȂ�΁A�����ɂ܂��J�X�o����B���̎��ɗ��Ă���B");
                    }
                }
                else
                {
                    if (!we.AlreadyEquipShop || !we.AvailableEquipShop2)
                    {
                        we.AvailableEquipShop2 = true;
                        UpdateMainMessage("�A�C���F���񂿂�[�B���ƁA�����I�H");

                        UpdateMainMessage("�K���c�F���ށA�A�C�����B�҂��Ă��������B");

                        UpdateMainMessage("�A�C���F����̎�ނ��������Ă���I�������ȁI�I");

                        UpdateMainMessage("�K���c�F����ƁA���i�N�ɂ������������p�ӂ��Ă������B���Ă����Ă���B");

                        UpdateMainMessage("���i�F�������H���ɂ��ł����I�H  ���肪�Ƃ��������܂��A�K���c�f������B");

                        UpdateMainMessage("�A�C���F���i�A���O�_���W�����ɍs�����A�K���c����ɒ����Ă��̂��H");

                        UpdateMainMessage("���i�F����Ȃ킯�Ȃ��ł���B����A�C���Ɍ������̂��n�߂Ă����B");

                        UpdateMainMessage("�K���c�F�n���i�͋��ݎ��̂������łȁB���i�N���s�����Ƃ��Ă���̂����V�ɓ`���Ă��������B");

                        UpdateMainMessage("���i�F���A�n���i�f�ꂳ�񂪁E�E�E�H");

                        UpdateMainMessage("�K���c�F����́A�v��񎖂������ɗ��邩��ȁA�܂�����������B");

                        UpdateMainMessage("���i�F�����܂���A�����킴�킴�E�E�E");

                        UpdateMainMessage("�K���c�F���������Ă���A���V�Ƃ��Ă͑劽�}����B�����A���Ă��Ȃ����B");

                        we.AlreadyEquipShop = true;
                        CallEquipmentShop();
                        mainMessage.Text = "";
                    }
                    else
                    {
                        we.AvailableEquipShop2 = true;
                        CallEquipmentShop();
                        mainMessage.Text = "";
                    }
                }
            }
            #endregion
            #region "�Q�K���e��A�k�u�R����̔�"
            else if (we.CommunicationCompArea2 && !we.AvailableEquipShop3)
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyEquipShop)
                    {
                        UpdateMainMessage("�A�C���F���񂿂�[�B");

                        UpdateMainMessage("�K���c�F���ށA�A�C�����B��قǃ��@�X�^�ꂪ���V�̏��ɗ��Ă����ĂȁB���͏��������B");

                        UpdateMainMessage("�A�C���F��H���@�X�^�ꂳ����Ă��傭���傭�R�R�ɖK���̂��H");

                        UpdateMainMessage("�K���c�F�N��苤�̋Y�ꂾ�A�C�ɂ���ȁB�܂��J�X����B���̎��ɗ���Ƃ悢�B");

                        UpdateMainMessage("�A�C���F�I�[�P�[�A�����B");

                        we.AlreadyEquipShop = true;
                    }
                    else
                    {
                        UpdateMainMessage("�K���c�F�����ɂȂ�΁A�����ɂ܂��J�X����B���̎��ɗ���Ƃ悢�B");
                    }
                }
                else
                {
                    if (!we.AlreadyEquipShop || !we.AvailableEquipShop3)
                    {
                        we.AvailableEquipShop3 = true;
                        UpdateMainMessage("�K���c�F�A�C���ƃ��i�N���B���҂��������ȁB");

                        UpdateMainMessage("�A�C���F�����I�i��������������Ă���I�I���ĉ���Ă����ł����I�H");

                        UpdateMainMessage("���i�F���킟�A���\�i�����������Ă��B�ǂ�ɂ��悤���������Ⴄ��ˁ�");

                        UpdateMainMessage("�K���c�F�R�K�͍��܂ňȏ�ɋ���ȃ����X�^�[���o�Ă��邾�낤�B�����͑ӂ�ȁB");
                        
                        UpdateMainMessage("�A�C���ƃ��i�F�b�n�C�I");

                        we.AlreadyEquipShop = true;
                        CallEquipmentShop();
                        mainMessage.Text = "";
                    }
                    else
                    {
                        we.AvailableEquipShop3 = true;
                        CallEquipmentShop();
                        mainMessage.Text = "";
                    }
                }
            }
            #endregion
            #region "�R�K���e��A�k�u�S����̔�"
            else if (we.CommunicationCompArea3 && !we.AvailableEquipShop4)
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyEquipShop)
                    {
                        UpdateMainMessage("�A�C���F���񂿂�[�B");

                        UpdateMainMessage("�K���c�F���ށA�A�C�����B�����΂炭�҂��Ă���B���M����������Ă��鏊���B");

                        UpdateMainMessage("�A�C���F�����I�}�W�ł����I�H�����A������ł��҂��܂���I");

                        UpdateMainMessage("�K���c�F�A�C�����_���W�����ɍs���Ă���Ԃɐ����Ă������B�܂�����Ƃ悢�B");

                        UpdateMainMessage("�A�C���F�I�[�P�[�I");

                        we.AlreadyEquipShop = true;
                    }
                    else
                    {
                        UpdateMainMessage("�K���c�F�����ɂȂ�΁A�����ɂ܂��J�X����B���̎��ɗ���Ƃ悢�B");
                    }
                }
                else
                {
                    if (!we.AlreadyEquipShop || !we.AvailableEquipShop4)
                    {
                        we.AvailableEquipShop4 = true;
                        UpdateMainMessage("�K���c�F���҂��������ȁB�S�K�p�̃A�C�e���͂�苭���������̂Ɏd�グ�Ă���B");

                        UpdateMainMessage("�A�C���F�������E�E�E���܂Ō������Ƃ��������炢�̎��̍������E�E�E");

                        UpdateMainMessage("���i�F�A�N�Z�T���֘A�������Ƒ�������ˁB�S���~�������炢�ˁB");

                        UpdateMainMessage("�K���c�F�c�O�����A�l�i�͂��ꑊ���ɂ��Ă���B");
                        
                        UpdateMainMessage("�A�C���F�S�R�C�ɂ��܂����I");

                        UpdateMainMessage("�K���c�F���ށA�ł͌��čs�����ǂ��B");

                        we.AlreadyEquipShop = true;
                        CallEquipmentShop();
                        mainMessage.Text = "";
                    }
                    else
                    {
                        we.AvailableEquipShop4 = true;
                        CallEquipmentShop();
                        mainMessage.Text = "";
                    }
                }
            }
            #endregion
            #region "�S�K���e��A�T�K�Ŕ��B��A�K���c����X"
            else if (we.CommunicationCompArea4 && we.TruthWord5 && !we.AvailableEquipShop5 && !we.CommunicationGanz100)
            {
                if (!we.AlreadyRest)
                {
                    we.AvailableEquipShop5 = true;
                    we.CommunicationGanz100 = true;
                    if (!we.AlreadyEquipShop)
                    {
                        UpdateMainMessage("�A�C���F���񂿂�[�B");

                        UpdateMainMessage("�A�C���F����H�N�����Ȃ��ȁB");

                        UpdateMainMessage("���i�F�A�C���A�˂����Ă�B�R�R�ɏ��u����������B");

                        UpdateMainMessage("�A�C���F���ď����Ă���񂾁H");

                        UpdateMainMessage("���i�F�������ƂˁA���������Ă����E�E�E");

                        if (we.SpecialTreasure1)
                        {
                            UpdateMainMessage("�@�@�@�w����̃��[�c����肵�����߁A�V����i�@�K���c����X�͕X�Ƃ���B�x");

                            UpdateMainMessage("�A�C���F����̃��[�c�H���̃^�C���E�I�u�E���[�Z�̂��Ƃ��H");

                            UpdateMainMessage("���i�F���������ˁB�ł��A�ˑR�X����Ȃ�đ����ȕi���������݂����ˁB");
                        }
                        else
                        {
                            UpdateMainMessage("�@�@�@�w����̃��[�c��T���ׂ��A�V����i�@�K���c����X�͕X�Ƃ���B�x");

                            UpdateMainMessage("�A�C���F����̃��[�c�H���������B�����������˂��ȁB");

                            UpdateMainMessage("���i�F�������߂ĕ�����B�ł��A���܂ɕX����̂��K���c�f������炵����ˁB");
                        }

                        UpdateMainMessage("�A�C���F�������q�����Ȃ񂾂���A�N���㗝�̌ق��l�ł��t���Ă��Ηǂ��̂ɂȁB");

                        UpdateMainMessage("�A�C���F���Ă��A���������˂����Ď�����I�H�����A�}�W����I");

                        UpdateMainMessage("���i�F�����ƒ��ӏ����܂ŏ����Ă�����B�������ƂˁE�E�E");

                        UpdateMainMessage("�@�@�@�w�Ȃ��A���ݔ����Ă��镐��́A�w�����ɕK��Gold���x�����悤�Ɂ@�yGanz.Gimerga�z");

                        UpdateMainMessage("�A�C���F�N�����Ȃ��񂾂���A�����肾��B����Ȃ̂ŏ��������藧�̂��H");

                        UpdateMainMessage("���i�F�ł��K���c�f������{�点��ƕ|�����A�N�����݂͂��Ȃ��񂶂�Ȃ��́H");

                        UpdateMainMessage("�A�C���F�E�E�E������������ȁB");

                        UpdateMainMessage("���i�F�܂��A�����Ă݂܂���B�����~������������΍w�����̂͂��ėǂ��݂��������B");

                        UpdateMainMessage("�A�C���F�I�[�P�[�A���Ⴀ�X��s�݂̕���X�ɂ��ז����Ă݂邩�I");

                        we.AlreadyEquipShop = true;
                        CallEquipmentShop();
                        mainMessage.Text = "";
                    }
                    else
                    {
                        CallEquipmentShop();
                        mainMessage.Text = "";
                    }
                }
                else
                {
                    we.AvailableEquipShop5 = true;
                    we.CommunicationGanz100 = true;
                    if (!we.AlreadyEquipShop)
                    {
                        UpdateMainMessage("�A�C���F���񂿂�[�B");

                        UpdateMainMessage("�A�C���F����H�N�����Ȃ��ȁB");

                        UpdateMainMessage("���i�F�A�C���A�˂����Ă�B�R�R�ɏ��u����������B");

                        UpdateMainMessage("�A�C���F���ď����Ă���񂾁H");

                        UpdateMainMessage("���i�F�������ƂˁA���������Ă����E�E�E");

                        if (we.SpecialTreasure1)
                        {
                            UpdateMainMessage("�@�@�@�w����̃��[�c����肵�����߁A�V����i�@�K���c����X�͕X�Ƃ���B�x");

                            UpdateMainMessage("�A�C���F����̃��[�c�H���̃^�C���E�I�u�E���[�Z�̂��Ƃ��H");

                            UpdateMainMessage("���i�F���������ˁB�ł��A�ˑR�X����Ȃ�đ����ȕi���������݂����ˁB");
                        }
                        else
                        {
                            UpdateMainMessage("�@�@�@�w����̃��[�c��T���ׂ��A�V����i�@�K���c����X�͕X�Ƃ���B�x");

                            UpdateMainMessage("�A�C���F����̃��[�c�H���������B�����������˂��ȁB");

                            UpdateMainMessage("���i�F�������߂ĕ�����B�ł��A���܂ɕX����̂��K���c�f������炵����ˁB");
                        }

                        UpdateMainMessage("�A�C���F�������q�����Ȃ񂾂���A�N���㗝�̌ق��l�ł��t���Ă��Ηǂ��̂ɂȁB");

                        UpdateMainMessage("�A�C���F���Ă��A���������˂����Ď�����I�H�����A�}�W����I");

                        UpdateMainMessage("���i�F�����ƒ��ӏ����܂ŏ����Ă�����B�������ƂˁE�E�E");

                        UpdateMainMessage("�@�@�@�w�Ȃ��A���ݔ����Ă��镐��́A�w�����ɕK��Gold���x�����悤�Ɂ@�yGanz.Gimerga�z");

                        UpdateMainMessage("�A�C���F�N�����Ȃ��񂾂���A�����肾��B����Ȃ̂ŏ��������藧�̂��H");

                        UpdateMainMessage("���i�F�ł��K���c�f������{�点��ƕ|�����A�N�����݂͂��Ȃ��񂶂�Ȃ��́H");

                        UpdateMainMessage("�A�C���F�E�E�E������������ȁB");

                        UpdateMainMessage("���i�F�܂��A�����Ă݂܂���B�����~������������΍w�����̂͂��ėǂ��݂��������B");

                        UpdateMainMessage("�A�C���F�I�[�P�[�A���Ⴀ�X��s�݂̕���X�ɂ��ז����Ă݂邩�I");

                        we.AlreadyEquipShop = true;
                        CallEquipmentShop();
                        mainMessage.Text = "";
                    }
                    else
                    {
                        CallEquipmentShop();
                        mainMessage.Text = "";
                    }

                }
                
            }
            #endregion
            #region "�w����"
            else
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AvailableEquipShop)
                    {
                        if (!we.AlreadyEquipShop)
                        {
                            int target = firstOpenDay - we.GameDay;// day;
                            mainMessage.Text = "�K���c�F����" + target + "���҂��Ă���B";
                            we.AlreadyEquipShop = true;
                        }
                        else
                        {
                            int target = firstOpenDay - we.GameDay;// day;
                            mainMessage.Text = "�K���c�F����" + target + "���҂��Ă���B";
                        }
                    }
                    else
                    {
                        CallEquipmentShop();
                        mainMessage.Text = "";
                    }
                }
                else
                {
                    if (!we.AvailableEquipShop)
                    {
                        if (!we.AlreadyCommunicate)
                        {
                            int target = firstOpenDay - we.GameDay;// day;
                            mainMessage.Text = "�K���c�F����" + target + "�����B���̂܂܂Ń_���W�����֍s���ė����B";
                            we.AlreadyEquipShop = true;
                        }
                        else
                        {
                            int target = firstOpenDay - we.GameDay;// day;
                            mainMessage.Text = "�K���c�F����" + target + "�����B���̂܂܂Ń_���W�����֍s���ė����B";
                        }
                    }
                    else
                    {
                        CallEquipmentShop();
                        mainMessage.Text = "";
                    }
                }
            }
            #endregion
        }

        private void CallEquipmentShop()
        {
            using (EquipmentShop ES = new EquipmentShop())
            {
                ES.StartPosition = FormStartPosition.CenterParent;
                ES.MC = this.mc;
                ES.SC = this.sc;
                ES.TC = this.tc;
                ES.WE = this.we;
                ES.ShowDialog();
            }
        }

        private void HomeTown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                using (ESCMenu esc = new ESCMenu())
                {
                    esc.MC = this.MC;
                    esc.SC = this.SC;
                    esc.TC = this.TC;
                    esc.WE = this.we;
                    esc.KnownTileInfo = this.knownTileInfo;
                    esc.KnownTileInfo2 = this.knownTileInfo2;
                    esc.KnownTileInfo3 = this.knownTileInfo3;
                    esc.KnownTileInfo4 = this.knownTileInfo4;
                    esc.KnownTileInfo5 = this.knownTileInfo5;
                    esc.StartPosition = FormStartPosition.CenterParent;
                    esc.ShowDialog();
                    if (esc.DialogResult == DialogResult.Retry)
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
                        this.DialogResult = DialogResult.Retry;
                    }
                    else if (esc.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    }
                }
            }
        }


        // [�x��]�FForm1.cs����R�s�y���܂����B���Ŏ���Form1.cs�����Y��Ȃ��B
        private bool EncountBattle(string enemyName)
        {
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
                    tempBackPack = MC.GetBackPackInfo();
                    be.MC = tempMC;
                    for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
                    {
                        if (tempBackPack[ii] != null)
                        {
                            be.MC.AddBackPack(tempBackPack[ii]);
                        }
                    }

                    //if (WE.AvailableSecondCharacter)
                    //{
                    //    ItemBackPack[] tempBackPack2 = new ItemBackPack[this.SC.GetBackPackInfo().Length];
                    //    tempBackPack2 = SC.GetBackPackInfo();
                    //    be.SC = tempSC;
                    //    for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
                    //    {
                    //        if (tempBackPack2[ii] != null)
                    //        {
                    //            be.SC.AddBackPack(tempBackPack2[ii]);
                    //        }
                    //    }
                    //}
                    //else
                    //{
                        be.SC = null;
                    //}

                    //if (WE.AvailableThirdCharacter)
                    //{
                    //    ItemBackPack[] tempBackPack3 = new ItemBackPack[this.TC.GetBackPackInfo().Length];
                    //    tempBackPack3 = TC.GetBackPackInfo();
                    //    be.TC = tempTC;
                    //    for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
                    //    {
                    //        if (tempBackPack3[ii] != null)
                    //        {
                    //            be.TC.AddBackPack(tempBackPack3[ii]);
                    //        }
                    //    }
                    //}
                    //else
                    //{
                        be.TC = null;
                    //}

                    EnemyCharacter1 ec1 = new EnemyCharacter1(enemyName, this.difficulty);
                    if (enemyName == "���F���[�E�A�[�e�B")
                    {
                        ec1.MainWeapon = new ItemBackPack("����̌��i���v���J�j");
                        ec1.MainArmor = new ItemBackPack("���^��̊Z�i���v���J�j");
                        ec1.Accessory = new ItemBackPack("�V��̗��i���v���J�j");
                    }
                    be.EC1 = ec1;

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

                    be.WE = tempWE;
                    be.StartPosition = FormStartPosition.CenterParent;
                    be.BattleSpeed = this.battleSpeed;
                    be.Difficulty = this.difficulty;
                    be.ShowDialog();
                    if (be.DialogResult == DialogResult.Retry)
                    {
                        // ���S���A�Ē��킷��ꍇ�A�͂��߂���ĂтȂ����B
                        if (ec1.Name == "���F���[�E�A�[�e�B")
                        {
                            this.mainMessage.Text = "���F���[�F���x�ł��������Ă��Ă��������B";
                        }
                        else
                        {
                            this.mainMessage.Text = "";
                        }
                        this.Update();
                        continue;
                    }
                    if (be.DialogResult == DialogResult.Abort)
                    {
                        // ���������A�o���l�ƃS�[���h�͓���Ȃ��B
                        this.MC = tempMC;
                        this.MC.ReplaceBackPack(tempMC.GetBackPackInfo());
                        //if (WE.AvailableSecondCharacter)
                        //{
                        //    this.SC = tempSC;
                        //    this.SC.ReplaceBackPack(tempSC.GetBackPackInfo());
                        //}
                        //if (WE.AvailableThirdCharacter)

                        //    this.TC = tempTC;
                        //    this.TC.ReplaceBackPack(tempTC.GetBackPackInfo());
                        //}
                        //this.WE = tempWE; // WE��HomeTown�ōX�V���܂��B
                        return false;
                    }
                    else if (be.DialogResult == DialogResult.Ignore)
                    {
                        endFlag = true;
                        //this.WE = tempWE; // WE��HomeTown�ōX�V���܂��B
                    }
                    else
                    {
                        //if (WE.AvailableFirstCharacter)
                        //{
                        //this.MC = tempMC;
                        this.MC.ReplaceBackPack(tempMC.GetBackPackInfo());
                        //    MC.Exp += be.EC1.Exp;
                        //    MC.Gold += be.EC1.Gold;
                        //    if (MC.Exp >= MC.NextLevelBorder)
                        //    {
                        //        mainMessage.Text = "�A�C���F���x���A�b�v�����I�I";
                        //        using (StatusPlayer sp = new StatusPlayer())
                        //        {
                        //            // [�x��]�F���x���A�b�v��MAX���C�t����ɂQ�O�A�}�i���R�O�ŗǂ����ǂ����������Ă��������B
                        //            MC.BaseLife += 20;
                        //            MC.BaseMana += 30;
                        //            //MC.CurrentLife = MC.BaseLife;
                        //            MC.Exp = MC.Exp - MC.NextLevelBorder;
                        //            MC.Level += 1;
                        //            sp.WE = WE;
                        //            sp.MC = MC;
                        //            sp.SC = SC;
                        //            sp.TC = TC;
                        //            sp.CurrentStatusView = Color.LightSkyBlue;
                        //            sp.LevelUp = true;
                        //            sp.UpPoint = MC.LevelUpPoint;
                        //            sp.StartPosition = FormStartPosition.CenterParent;
                        //            sp.ShowDialog();
                        //        }
                        //    }
                        //}
                        //if (WE.AvailableSecondCharacter)
                        //{
                        //    this.SC = tempSC;
                        //    this.SC.ReplaceBackPack(tempSC.GetBackPackInfo());
                        //    SC.Exp += be.EC1.Exp;
                        //    //SC.Gold += be.EC1.Gold; // [�x��]�F�S�[���h�̏����͕ʃN���X�ɂ���ׂ��ł��B
                        //    if (SC.Exp >= SC.NextLevelBorder)
                        //    {
                        //        mainMessage.Text = "���i�F������A���x���A�b�v��";
                        //        using (StatusPlayer sp = new StatusPlayer())
                        //        {
                        //            // [�x��]�F���x���A�b�v��MAX���C�t����ɂQ�O�A�}�i���R�O�ŗǂ����ǂ����������Ă��������B
                        //            SC.BaseLife += 20;
                        //            SC.BaseMana += 30;
                        //            //MC.CurrentLife = MC.BaseLife;
                        //            SC.Exp = SC.Exp - SC.NextLevelBorder;
                        //            SC.Level += 1;
                        //            sp.WE = WE;
                        //            sp.MC = MC;
                        //            sp.SC = SC;
                        //            sp.TC = TC;
                        //            sp.CurrentStatusView = Color.Pink;
                        //            sp.LevelUp = true;
                        //            sp.UpPoint = SC.LevelUpPoint;
                        //            sp.StartPosition = FormStartPosition.CenterParent;
                        //            sp.ShowDialog();
                        //        }
                        //    }
                        //}
                        //if (WE.AvailableThirdCharacter)
                        //{
                        //    this.TC = tempTC;
                        //    this.TC.ReplaceBackPack(tempTC.GetBackPackInfo());
                        //    TC.Exp += be.EC1.Exp;
                        //    //TC.Gold += be.EC1.Gold; // [�x��]�F�S�[���h�̏����͕ʃN���X�ɂ���ׂ��ł��B
                        //    if (TC.Exp >= TC.NextLevelBorder)
                        //    {
                        //        mainMessage.Text = "���F���[�F���x���A�b�v�ł��ˁB";
                        //        using (StatusPlayer sp = new StatusPlayer())
                        //        {
                        //            // [�x��]�F���x���A�b�v��MAX���C�t����ɂQ�O�A�}�i���R�O�ŗǂ����ǂ����������Ă��������B
                        //            TC.BaseLife += 20;
                        //            TC.BaseMana += 30;
                        //            //TC.CurrentLife = MC.BaseLife;
                        //            TC.Exp = TC.Exp - TC.NextLevelBorder;
                        //            TC.Level += 1;
                        //            sp.WE = WE;
                        //            sp.MC = MC;
                        //            sp.SC = SC;
                        //            sp.TC = TC;
                        //            sp.CurrentStatusView = Color.Silver;
                        //            sp.LevelUp = true;
                        //            sp.UpPoint = TC.LevelUpPoint;
                        //            sp.StartPosition = FormStartPosition.CenterParent;
                        //            sp.ShowDialog();
                        //        }
                        //    }
                        //}
                        //this.WE = tempWE; // WE��HomeTown�ōX�V���܂��B
                        return true;
                    }
                }
            }

            return false;
        }

        private void GetPotionForLana()
        {
            string potionName = "�������ԃ|�[�V����";

            if (!we.CompleteArea1 && !we.CompleteArea2 && !we.CompleteArea3 && !we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = "�������ԃ|�[�V����";
            }
            else if (we.CompleteArea1 && !we.CompleteArea2 && !we.CompleteArea3 && !we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = "���ʂ̐ԃ|�[�V����"; 
            }
            else if (we.CompleteArea1 && we.CompleteArea2 && !we.CompleteArea3 && !we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = "�傫�Ȑԃ|�[�V����";
            }
            else if (we.CompleteArea1 && we.CompleteArea2 && we.CompleteArea3 && !we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = "����ԃ|�[�V����";
            }
            else if (we.CompleteArea1 && we.CompleteArea2 && we.CompleteArea3 && we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = "���؂Ȑԃ|�[�V����";
            }
            else
            {
                potionName = "�������ԃ|�[�V����";
            }
            bool result = mc.AddBackPack(new ItemBackPack(potionName));
            if (!result)
            {
                UpdateMainMessage("�A�C���F���܂����A�o�b�N�p�b�N�������ς����B�����̂ĂȂ��ƂȁE�E�E");
                using (StatusPlayer sp = new StatusPlayer())
                {
                    sp.MC = mc;
                    if (we.AvailableSecondCharacter)
                    {
                        sp.SC = sc;
                    }
                    if (we.AvailableThirdCharacter)
                    {
                        sp.TC = tc;
                    }
                    sp.WE = we;
                    sp.OnlySelectTrash = true;
                    sp.StartPosition = FormStartPosition.CenterParent;
                    sp.ShowDialog();
                    if (this.DialogResult == DialogResult.Cancel)
                    {
                    }
                    else
                    {
                        mc = sp.MC;
                        mc.AddBackPack(new ItemBackPack(potionName));
                    }
                }
                UpdateMainMessage("", true);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GroundOne.StopDungeonMusic();

            UpdateMainMessage("�A�C���F�悵�A���K�ł����邩", true);
            mainMessage.Update();
            using (RequestInput ri = new RequestInput())
            {
                ri.StartPosition = FormStartPosition.CenterParent;
                ri.InputData = "�_�~�[�f�U��N";
                ri.ShowDialog();

                string entryName = ri.InputData; // �_�~�[�f�U��N
                bool result = EncountBattle(entryName);
                if (result)
                {
                    UpdateMainMessage("�A�C���F���̏������ȁB", true);
                }
                else
                {
                    UpdateMainMessage("�A�C���F�����E�E�E�������܂������B", true);
                }

            }
            GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
        }

    }
}