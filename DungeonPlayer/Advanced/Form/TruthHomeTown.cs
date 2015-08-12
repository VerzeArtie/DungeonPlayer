using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace DungeonPlayer
{
    public partial class TruthHomeTown : MotherForm
    {
        #region "�v���p�e�B"
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
        public bool[] Truth_KnownTileInfo { get; set; } // ��Ғǉ�
        public bool[] Truth_KnownTileInfo2 { get; set; } // ��Ғǉ�
        public bool[] Truth_KnownTileInfo3 { get; set; } // ��Ғǉ�
        public bool[] Truth_KnownTileInfo4 { get; set; } // ��Ғǉ�
        public bool[] Truth_KnownTileInfo5 { get; set; } // ��Ғǉ�
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
        #endregion

        public TruthHomeTown()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
        }

        private void TruthHomeTown_Load(object sender, EventArgs e)
        {
            //ReConstractWorldEnvironment(); // �o�b�h�G���h����̍ăX�^�[�g�`�F�b�N

            if (we.AvailablePotionshop && !GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEvent511)
            {
                this.buttonPotion.Visible = true;
            }

            if (we.AvailableBackGate && !GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEvent511)
            {
                this.buttonShinikia.Visible = true;
            }

            if (we.AvailableDuelColosseum && !GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEvent511)
            {
                this.buttonDuel.Visible = true;
            }

            //if (we.Truth_CommunicationFirstHomeTown)
            {
                if (!we.AlreadyRest)
                {
                    ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_EVENING);
                }
                else
                {
                    ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                }
            }
            this.dayLabel.Text = we.GameDay.ToString() + "����";
            if (we.AlreadyRest)
            {
                this.firstDay = we.GameDay - 1; // �x���������ǂ����̃t���O�Ɋւ�炸���ɖK�ꂽ�ŏ��̓����L�����܂��B
                if (this.firstDay <= 0) this.firstDay = 1; // [�x��] ��ҏ����̃��W�b�N����ɂ������i�B���܂�ǂ��������ł͂���܂���B
            }
            else
            {
                this.firstDay = we.GameDay; // �x���������ǂ����̃t���O�Ɋւ�炸���ɖK�ꂽ�ŏ��̓����L�����܂��B
            }
            this.we.SaveByDungeon = false; // ���ɖ߂��Ă��邱�Ƃ��������߂̂��̂ł��B
        }
        private void TruthHomeTown_Shown(object sender, EventArgs e)
        {
            GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

            ok = new OKRequest();
            ok.StartPosition = FormStartPosition.Manual;
            ok.Location = new Point(this.Location.X + 904, this.Location.Y + 708);

            #region "���S���Ă�����͎̂����I�ɕ��������܂��B"
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
            #endregion

            if (!we.AlreadyShownEvent)
            {
                we.AlreadyShownEvent = true;
                // �_���W��������A�Ҍ�A�K�{�C�x���g�Ƃ��ėD��
                #region "�G���f�B���O"
                if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd && GroundOne.WE2.SeekerEvent1103)
                {
                    ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);

                    buttonDungeon.Visible = false;
                    buttonGanz.Visible = false;
                    buttonShinikia.Visible = false;
                    buttonHanna.Visible = false;
                    buttonRana.Visible = false;
                    buttonPotion.Visible = false;
                    buttonDuel.Visible = false;
                    dayLabel.Visible = false;

                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                    UpdateMainMessage("�@�@�@�������@���O�I�I�I�@�������@");

                    UpdateMainMessage("�@�@�@�y��؂ȃI���E�����f�B�X�ADUEL���Z��ɂāA���J�̔s�k�I�I�z�@");

                    UpdateMainMessage("�@�@�@�yDUEL�^�C���͎j��ō��z�@�j�@");

                    UpdateMainMessage("�@�@�@�y�P�U���S�Q�b�z�@");

                    UpdateMainMessage("�@�@�@�y���ߎ�́A�[�[�^�E�G�N�X�v���[�W�����z");

                    UpdateMainMessage("�@�@�@�y�I���E�����f�B�X�A�K���Ƀ��[�v�Q�[�g����g���Ĕ������J��L���Ă������z");

                    UpdateMainMessage("�@�@�@�y����̃j�Q�C�g�A�X�^���X�E�I�u�E�T�b�h�l�b�X�Ɋ��x�ƂȂ��j�~����z");

                    UpdateMainMessage("�@�@�@�y�I���E�����f�B�X���A�^�C���X�g�b�v�𔭓�����u�ԁz");

                    UpdateMainMessage("�@�@�@�y����ɐ��̃^�C���X�g�b�v�𔭓����ꂽ�̂��v���I�ƂȂ����z");

                    UpdateMainMessage("�A�C���F�}�W���E�E�E�t���������܂����̂���B");

                    UpdateMainMessage("���i�F���������ˁA���̃����f�B�X���񂪕�����Ȃ�āB");

                    UpdateMainMessage("�A�C���F�����A�܂�����������ȁB�ǂ�ł݂邩�B");

                    UpdateMainMessage("�@�@�@�y�Ȃ��A�{DUEL�Ɋւ��āA�L�҂͎����̐�����q���āA�I���E�����f�B�X�I��֓ˌ���ނ��s�����B�z");

                    UpdateMainMessage("�@�@�@�L�ҁF�u�����f�B�X�I��I����͔s�k��������ł��傤���I�H�v");

                    UpdateMainMessage("�@�@�@�����f�B�X�F�u���������ȁI�v");

                    UpdateMainMessage("�@�@�@�L�ҁF�u�헪�I�ɂ͏����Ă������ǂ��������ł��A�����������������I�v");

                    UpdateMainMessage("�@�@�@�����f�B�X�F�u���������A�m�邩�{�P�I�v");

                    UpdateMainMessage("�@�@�@�L�ҁF�u�̒��͖��S��������ł��傤���H�ǂ����ɂ߂Ă����Ȃǂ́I�H�v");

                    UpdateMainMessage("�@�@�@�����f�B�X�F�u���������A��Ȃ񂠂邩�A�{�P���I�v");

                    UpdateMainMessage("�@�@�@�L�ҁF�u�Ō�ɁA�c�t�d�k���Z��̔e�҂Ƃ��āA����̕����Ɋւ��Ĉꌾ���肢���܂��I�I�I�v");

                    UpdateMainMessage("�@�@�@�����f�B�X�F�u�E�E�E�@�E�E�E�@�E�E�E�v");

                    UpdateMainMessage("�@�@�@�����f�B�X�F�u���׍H�͈�ؖ����A�̒����݂��ɖ��S�A�����O�̕s�������ؖ����v");

                    UpdateMainMessage("�@�@�@�����f�B�X�F�u���̕������v");

                    UpdateMainMessage("�@�@�@�����f�B�X�F�u�E�E�E�@�E�E�E�@�E�E�E�v");

                    UpdateMainMessage("�@�@�@�L�ҁF�u�E�E�E�@�E�E�E�@�E�E�E�v");

                    UpdateMainMessage("�@�@�@�����f�B�X�F�u�h�P��N�\�{�P�L�҂����I�I�H���E�����b���@�I�I�v");

                    UpdateMainMessage("�@�@�@�L�ҁF�u�b�q�A�q�G�G�F�F�I�I�I�킽�����A���肪�Ƃ��������܂����I�I�I�v");

                    UpdateMainMessage("�A�C���F�E�E�E�����E�E�E�悭�����ɂ������Ȃ��̋L�ҁE�E�E");

                    UpdateMainMessage("���i�F���̉f���A���̂����������œ����Ă��ˁA�L�҂���E�E�E");

                    UpdateMainMessage("�A�C���F�����E�E�E�����߂܂�����}�W�ŕa�@���肾�����낤�ȁE�E�E");

                    UpdateMainMessage("���i�F���A���Ă݂čŌ�̃R�����g����������A�z����");

                    UpdateMainMessage("�A�C���F�ق�Ƃ��A�ǂ�ǂ�E�E�E");

                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("�@�@�@�y�Ȃ��A�L�҂͖҃_�b�V���œ�����ہA�I���E�����f�B�X�I��̍Ō�ɐU��Ԃ鎞�̊��ڌ������z");

                    UpdateMainMessage("�@�@�@�y���̎��́A�I���E�����f�B�X�I��̊�́z");

                    UpdateMainMessage("�@�@�@�y�ǂ��ƂȂ��A�D�����΂��Ă���悤�Ɍ������z");

                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                    UpdateMainMessage("���i�F�΂��Ă���悤�ɁE�E�E");

                    UpdateMainMessage("�A�C���F�E���ꂩ�������ゾ��H�@���ӂ���Ί�Ɗ��Ⴂ�����񂶂�Ȃ��̂��H");

                    UpdateMainMessage("���i�F�b�t�t�A�����Ȃ̂����ˁB");

                    UpdateMainMessage("�A�C���F�����ĂƁA���N���t�@�[�W���{�a���a�Ղւ̏��ҏ󂪓͂��Ă������B");

                    UpdateMainMessage("���i�F�E�\�A�����Ɗo���Ă��킯�H");

                    UpdateMainMessage("�A�C���F�����A�������o���Ă邳�B�����Ə��ҏ���R�R�ɁE�E�E");

                    UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E�@�E�E�E");

                    UpdateMainMessage("�A�C���F�b�O�E�E�E");

                    UpdateMainMessage("���i�F�i�W�B�`�j");

                    UpdateMainMessage("�A�C���F���₢�₢��A�h���̃o�b�N�p�b�N�Ǘ��q�ɂɓ��ꂽ�񂾂����A�b�n�b�n�b�n�I");

                    UpdateMainMessage("���i�F�ց`�A���Ⴀ��ł����ƌ��ɍs���܂����");

                    UpdateMainMessage("�A�C���F�����E�E�E�b�n�b�n�n�E�E�E");

                    UpdateMainMessage("���i�F�˂��A�Ƃ���ŃA�C���B");

                    UpdateMainMessage("�A�C���F��H�@�Ȃ񂾁H");

                    UpdateMainMessage("���i�F���B���ă_���W�����̍Ō�łǂ��Ȃ����̂��A�o���Ă�H");

                    UpdateMainMessage("�A�C���F�_���W�����̍Ō�H");

                    UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E�@�E�E�E");

                    UpdateMainMessage("�A�C���F�������Ă񂾂�B");

                    UpdateMainMessage("�A�C���F���e�����Ɍ��܂��Ă邶��Ȃ����I�b�n�b�n�b�n�I");

                    UpdateMainMessage("���i�F������ƁA�܂Ƃ��ɓ����Ȃ�����ˁB");

                    UpdateMainMessage("���i�F���Ȃ��Ƃ����͊o���ĂȂ����A�Ō�̕��́B");

                    UpdateMainMessage("���i�F�Ō�͂ǂ�Ȋ����������́H�����Ă�B");

                    UpdateMainMessage("�A�C���F����Ⴈ�O�A�ŉ��w�ƌ����΁E�E�E");

                    UpdateMainMessage("�A�C���F�f�J�C�����h�J�[���ƍ\���ĂĂ��B");

                    UpdateMainMessage("�A�C���F����������Y�o�Y�o���ƌ��j�����̂��I");

                    UpdateMainMessage("�A�C���F�b�n�b�n�b�n�I");

                    UpdateMainMessage("���i�F�E�E�E�Ӂ[��E�E�E");

                    UpdateMainMessage("���i�F�S�R�����ɂȂ��ĂȂ���ˁB");

                    UpdateMainMessage("�A�C���F�i�b�M�N�j");

                    UpdateMainMessage("���i�F���̎������Ă��ꂽ�̂͂ǂ̕ӂ肾�����̂�H");

                    UpdateMainMessage("���i�F����������₵�����Ɉ͂܂�āA�^���ÂɂȂ��ċC�₵���u�Ԃ͊o���Ă�񂾂��ǁE�E�E");

                    UpdateMainMessage("���i�F���A���̌�ǂ��Ȃ����̂����S�R�v���o���Ȃ��̂�B�X�b�|�����������ŁB");

                    UpdateMainMessage("���i�F�C���t������A�_���W�����̊O�ŉ������Ă��̂�ˁB");

                    UpdateMainMessage("���i�F����Ȃ̔[�����s���Ȃ���B�����Ƌ����Ă��傤�����B");

                    UpdateMainMessage("�A�C���F���A����͂��Ȃ��E�E�E");

                    UpdateMainMessage("�A�C���F�S�K�ł��O���|��Ă��񂾂�B");

                    UpdateMainMessage("�A�C���F�ł��A����𔭌��������͍Q�ĂĂ��O������N�����āE�E�E");

                    UpdateMainMessage("�w�b�h�X�E�E�D�D���I�I�I�x�i���i�̃t�@�C�i���e�B�E�L�b�N���A�C�����y��j�@�@");

                    UpdateMainMessage("���i�F�u�����N�����āv�Ƃ������\���͎~�߂Ă��傤�����B");

                    UpdateMainMessage("�A�C���F�O�t�H�I�H�E�E�E�ǁA�ǂ������Ηǂ��񂾂�E�E�E�������E�E�E");

                    UpdateMainMessage("�A�C���F�Q�ĂĂ��O�������ƋN�����Ă��ȁE�E�E");

                    UpdateMainMessage("�A�C���F�i�n���n���E�E�E�j");

                    UpdateMainMessage("���i�F���̎��́H");

                    UpdateMainMessage("�A�C���F�ŁA��͂��̂܂܃_���W��������A�҂������ď����B");

                    UpdateMainMessage("���i�F�E�E�E�@�E�E�E�@�E�E�E");

                    UpdateMainMessage("���i�F����ȕ��ʂ̓��e��������A�������Ċo���Ă�Ǝv���񂾂���");

                    UpdateMainMessage("���i�F�˂��A���̎��ɉ����������񂶂�Ȃ��́H�ǂ��������̂�H");

                    UpdateMainMessage("�A�C���F�܁E�E�E");

                    UpdateMainMessage("�A�C���F�܂܂܁A�ǂ�����˂�������ȏ��́I");

                    UpdateMainMessage("�A�C���F�_���W�������e�I�����A�����I�I�I");

                    UpdateMainMessage("�A�C���F�n�[�b�n�b�n�b�n�I�I");

                    UpdateMainMessage("���i�F��Ή����B���Ă���ˁE�E�E");

                    UpdateMainMessage("�A�C���F�܂��A���i�A���ꂾ�B");

                    UpdateMainMessage("�A�C���F�ǂ�������A���O�ƈꏏ�ɖ����ɂ����܂Ŗ߂�āE�E�E�{���ɁB");

                    UpdateMainMessage("���i�F�����E�E�E�Ƃ��ƂƁE�E");

                    UpdateMainMessage("���i�F������ƁA������݂肵������Ă�̂�A����ȏ��ł����B");

                    UpdateMainMessage("�A�C���F�E�E�E���������A�Ȃ�ƂȂ��ȁE�E�E");

                    UpdateMainMessage("�A�C���F�����A���������΃��i�A���ݎ�������񂾂�");

                    UpdateMainMessage("���i�F�ȂɁH");

                    UpdateMainMessage("�A�C���F�������B�t�@�[�W���{�a�̐��a�ՂQ�P�N�����邾��H");

                    UpdateMainMessage("���i�F�����B");

                    UpdateMainMessage("�A�C���F���̎��ɁE�E�E�K���A����Ă��������ꏊ������񂾁B");

                    UpdateMainMessage("�A�C���F�ꏏ�ɁA���Ă���邩�H");

                    UpdateMainMessage("���i�F�ׂɗǂ����ǁA�ǂ��ɍs������Ȃ̂�H");

                    UpdateMainMessage("�A�C���F���v�A����ȉ������֍s���킯����˂��񂾁B");

                    UpdateMainMessage("�A�C���F���ށB");

                    UpdateMainMessage("���i�F����A�ǂ����B");

                    UpdateMainMessage("�A�C���F�T���L���[�B");

                    UpdateMainMessage("���i�F���Ⴀ�A�����܂��ˁ�");

                    UpdateMainMessage("�A�C���F�����A�܂������ȁB");

                    UpdateMainMessage("���i�F�E�E�E�@�E�E�E�@�E�E�E");

                    UpdateMainMessage("���i�F�Q�V���Ă���A�@���E������ˁB");

                    UpdateMainMessage("�A�C���F�E�E�E���₢��A�E���Ȃ��悤�ɂ��Ă���B");

                    UpdateMainMessage("���i�F���Ⴀ�ˁB");

                    UpdateMainMessage("�A�C���F�����B");

                    ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "���̓��̒��E�E�E";
                        md.ShowDialog();
                    }

                    UpdateMainMessage("�A�C���F�S�t�H�I�H�H�H�E�E�E�b�S�z�b�S�z�E�E�E");

                    UpdateMainMessage("���i�F�P���x��Ă����B���A�N���Ȃ����B");

                    UpdateMainMessage("�A�C���F�C�b�c�c�c�E�E�E�e�͂˂��ȁE�E�E");

                    UpdateMainMessage("���i�F�t�@�[�W���{�a�O�͒���ōs���Ă����G����񂾂���A���߂ɍs���܂��傤���");

                    UpdateMainMessage("�A�C���F�����A�������Ă���āB");

                    UpdateMainMessage("�A�C���F���Ⴀ�A�o�����I�I");

                    GroundOne.StopDungeonMusic();
                    GroundOne.PlayDungeonMusic(Database.BGM13, Database.BGM13LoopBegin);
                    ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_FAZIL_CASTLE);
                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "---- �t�@�[�W���{�a�ɂ� ----";
                        md.ShowDialog();
                    }

                    UpdateMainMessage("�A�C���F�����������I�@�������l�̐����ȁI�I");

                    UpdateMainMessage("���i�F���a�Ղ����́A������O����Ȃ���");

                    UpdateMainMessage("�A�C���F�G���~�������悭���ꂾ���̐l�𖈔N���N�A�{�a�ɏ���������ȁE�E�E");

                    UpdateMainMessage("�A�C���F�{�a�����낻��p���N����񂶂�˂��̂��H");

                    UpdateMainMessage("���i�F�{�a�O�̃K�[�f���L�������񂾂��A���v�炵�����B");

                    UpdateMainMessage("�A�C���F�b�n�n�E�E�E���̕ӂ͂������ƍs���������ȁB");

                    UpdateMainMessage("�A�C���F���ĂƁA���Ⴀ��ɕ���ŃG���~�����ƃt�@�����܂ɂ��Ζʂƍs���܂����I");

                    UpdateMainMessage("���i�F�����");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "---- �Q���Ԕ��o�ߌ� ----";
                        md.ShowDialog();
                    }
                    UpdateMainMessage("�A�C���F�E�E�E�@�����������@�E�E�E");

                    UpdateMainMessage("���i�F����A���������̂͂���B");

                    UpdateMainMessage("���i�F�����A�z���������B���");

                    UpdateMainMessage("�A�C���F�{�����A�����������ȁE�E�E");

                    UpdateMainMessage("�A�C���F�Ȃ��A���Ń��i�B");

                    UpdateMainMessage("���i�F����H");

                    UpdateMainMessage("�A�C���F�{�a���A�ǂ���ӂ����Ă݂����񂾁H");

                    UpdateMainMessage("�A�C���F�s���������������Ă���B�D�悳���邩�炳�B");

                    UpdateMainMessage("���i�F�E�E�E���E�E�E");

                    UpdateMainMessage("���i�F�������������������[�[�[�[�I�I�H�H");

                    UpdateMainMessage("�A�C���F��[�[�A�����Ȃ肤����������o���Ȃ��āA�������B");

                    UpdateMainMessage("���i�F���Ђ���Ƃ��āA�C�������Ƃł������킯�H���̂́H�H");

                    UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E�@�E�E�E");

                    UpdateMainMessage("�A�C���F�����A��������B��������A�܂������E�E�E");

                    UpdateMainMessage("���i�F�E�E�E�@�E�E�E�@�E�E�E");

                    UpdateMainMessage("���i�F�M�����Ȃ���ˁA�o�J�A�C�������������������́B");

                    UpdateMainMessage("�A�C���F�ǂ�����ق�A�ǂ��ɍs���Ă݂�񂾁H");

                    UpdateMainMessage("���i�F��[���Ⴀ�E�E�E");

                    UpdateMainMessage("���i�F�t�@�����ܗl�̉y�����I����Ă��猾����ˁ�");

                    UpdateMainMessage("�A�C���F�����A�����B");

                    UpdateMainMessage("�A�C���F�����A�悤�₭�O�̃��c���I������݂��������I");

                    UpdateMainMessage("���i�F���Ⴀ�A�i�߂܂����");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "---- �����^���܁@�y���̊Ԃɂ� ----";
                        md.ShowDialog();
                    }

                    UpdateMainMessage("�A�C���F���ƂƁE�E�E�����Ő��񂾂�ȁE�E�E");

                    UpdateMainMessage("�G���~�F�悭�����ˁB�A�C���N�ƃ��i����B");

                    UpdateMainMessage("���i�F�G���~�l�A�t�@���l�A���a�Ղ��߂łƂ��������܂��B");

                    UpdateMainMessage("�t�@���F�A�C����������i������A�����b�N�X���Ă��������ˁi�O�O");

                    UpdateMainMessage("�A�C���F�����I���肪�Ƃ��������܂��I�I");

                    UpdateMainMessage("�t�@���F�i������ƁA����܂�`��������Ȃ��ł�ˁj");

                    UpdateMainMessage("�A�C���F�i��������˂����E�E�E���������Ă�񂾂����j");

                    UpdateMainMessage("�G���~�F�����ł���A�����̃A�C���N�炵�������Ă��������B");

                    UpdateMainMessage("�A�C���F�n�C�A�ǂ��������I�I");

                    UpdateMainMessage("���i�F�����ƁA�����̃o�J�͂�����ƒu���Ƃ��āE�E�E");

                    UpdateMainMessage("���i�F�G���~�l�A�{���܂�����Ă̗��݂������Ă��̉y���̊ԂɎQ��܂����B");

                    UpdateMainMessage("�G���~�F�ǂ������񂾂��H");

                    UpdateMainMessage("���i�F�ŋ߃t�@�[�W���̈�֕��͂ɂ��ڐG���v��" + Database.VINSGALDE + "�����ł���");

                    UpdateMainMessage("���i�F" + Database.VINSGALDE + "�͌��X���̕ꂪ�������");

                    UpdateMainMessage("���i�F�ꂩ��͍������炿�ɂ����G���A�����������ƕ�������Ă���܂����B");

                    UpdateMainMessage("���i�F�����ł��肢������܂��B");

                    UpdateMainMessage("���i�F������Q�N�̊ԁA���Ƃ����̃o�J�A�C���ɑ΂���");

                    UpdateMainMessage("���i�F���O�������؂𔭍s���Ă��������Ȃ��ł��傤���H");

                    UpdateMainMessage("�A�C���F�����A�����I�H");

                    UpdateMainMessage("�G���~�F�Ȃ�قǁA" + Database.VINSGALDE + "�ɍs���č����ȊO�̐��Y�������Ă������Ȃ񂾂ˁB");

                    UpdateMainMessage("�G���~�F���������O�������؂�v�����Ă���Ƃ́E�E�E");

                    UpdateMainMessage("�G���~�F�ǂ����悤���H�t�@���B");

                    UpdateMainMessage("�t�@���F�G���~������A�Ђǂ��j�i�O�O");

                    UpdateMainMessage("�G���~�F���E�E�E�ǂ����Ă��O�́E�E�E");

                    UpdateMainMessage("�t�@���F���扰�a�ҁA�����ƋM�����l���Ă������������Ȃ�����ˁi�O�O");

                    UpdateMainMessage("�G���~�F�킩���Ă���āA������͂肨�O�͏�����������łȂ����B");

                    UpdateMainMessage("�t�@���F�E�E�E�i�O�O��");

                    UpdateMainMessage("�G���~�F�b�S�z���A�ł́B���[�E�E�E");

                    UpdateMainMessage("�t�@���F���i����A���O�������؂͊��ɗp�ӂ��Ă���܂��i�O�O");

                    UpdateMainMessage("���i�F�����I�H�@�{���ł����I�H");

                    UpdateMainMessage("�t�@���F�A�C������̕�������܂���i�O�O");

                    UpdateMainMessage("�A�C���F�}�W����I�I���̂܂ɂ���ȃ��m���I�H");

                    UpdateMainMessage("�t�@���F�͂��A�G���~����i�O�O");

                    UpdateMainMessage("�G���~�F�b�E�E�E�b�N�E�E�E");

                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("�G���~�F���͂ˁA�Q�����O�����肩��l���Ă��񂾂�A" + Database.VINSGALDE + "�̌��́B");

                    UpdateMainMessage("�G���~�F�K�C���͒N���Ȃƍl������");

                    UpdateMainMessage("�G���~�F�N�B�Q�l�ɂ��̐��a�Ղ��I�������A�����Ɉ˗����ɍs�����Ǝv���Ă��񂾂�B");

                    UpdateMainMessage("�i���i�F�����I�H�j�@�@�i�A�C���F�����I�I�j");

                    UpdateMainMessage("�G���~�F���̃_���W������˔j���Ă����N�B���B");

                    UpdateMainMessage("�G���~�F���ݒB�Ȃ�A�����Ƃ��̌����������Ă����B�����M�����񂾁B");

                    UpdateMainMessage("�G���~�F�Ƃ����킯�ŁE�E�E�t�@��");

                    UpdateMainMessage("�t�@���F�͂��A���ł��傤�H�i�O�O");

                    UpdateMainMessage("�G���~�F���Ԃ��t�ɂȂ��Ă��܂������E�E�E��̃��m��������");

                    UpdateMainMessage("�t�@���F�t�t�t�A�������̎�ɉB�������Ă܂�����A�W���[���i�O�O");

                    UpdateMainMessage("�G���~�F�������A���O�͂����Ɖ��܂炵�����Ȃ����B�{���ɁE�E�E");

                    UpdateMainMessage("�t�@���F���̂��炢�ǂ�����Ȃ��A�b�l�A�A�C������i�O�O");

                    UpdateMainMessage("�A�C���F�^�E�E�E�^�n�n�E�E�E");

                    UpdateMainMessage("�A�C���F���ς�炸�A���肳��Ă��܂��܂��ˁB�����ł���A���������܂��B");

                    UpdateMainMessage("���i�F�{���A����������B������Ȃ�ł������܂Ŏ��̍s�����ǂݐ؂���Ȃ�āE�E�E");

                    UpdateMainMessage("�A�C���F���A������Ǝ���B���i�̐��^�ʖڂ��Ɛ��`�s���͉��ƂȂ��ǂ߂�Ƃ��Ă�");

                    UpdateMainMessage("�A�C���F���ŉ��̕��܂ŁH�H");

                    UpdateMainMessage("�t�@���F�E�E�E�b�L���A��邾��i�O�O");

                    UpdateMainMessage("�A�C���F�E�E�E�n�C�H�H");

                    UpdateMainMessage("�G���~�F����A�t�@���B�b����₱�������Ȃ��悤�ɁB");

                    UpdateMainMessage("�G���~�F�A�C������A�N�ɉ������Ă��炤�̂́A�ʂ̈Ӑ}������񂾁B");

                    UpdateMainMessage("�A�C���F�́A�͂��B");

                    UpdateMainMessage("�G���~�F�A�C������E�E�E���͂ˁE�E�E");

                    GroundOne.PlayDungeonMusic(Database.BGM15, Database.BGM15LoopBegin);

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "---- �t�@�[�W���{�a�A���|�̍L��ɂ� ----";
                        md.ShowDialog();
                    }

                    UpdateMainMessage("���i�F�����A�z���z�����ăA�C���B����A�J�V�W�A�̖؂��");

                    UpdateMainMessage("�A�C���F�ւ��E�E�E����Ȍ`���Ă񂾂ȁB");

                    UpdateMainMessage("�A�C���F�����A�������̂���Ȍ`�����Ă邼�B���i�킩�邩�H");

                    UpdateMainMessage("���i�F����̓q�����M�c���N�T��B���n����܂ł́A�Q������ɗt���L����́B");

                    UpdateMainMessage("�A�C���F�ւ��E�E�E�ւ��������E�E�E");

                    UpdateMainMessage("���i�F�A�C�����Ė{���ɒm��Ȃ��̂ˁA�����������́B");

                    UpdateMainMessage("�A�C���F�����A�܂������m��Ȃ��B");

                    UpdateMainMessage("���i�F�������́A�S�����E���N�����́H");

                    UpdateMainMessage("�A�C���F�������Ƃ������������Ȃ��ȁB");

                    UpdateMainMessage("���i�F���́A�}���E�n�N�W���́H");

                    UpdateMainMessage("�A�C���F�ǂ����̖{�ł��낤���āE�E�E");

                    UpdateMainMessage("���i�F�����ƁA�t�@�[�W���{�a�ō��N�V��̖򑐂�E�E�E�{�ɂ܂��ڂ��ĂȂ����E�E�E");

                    UpdateMainMessage("�A�C���F�b�Q�A�܂�����H");

                    UpdateMainMessage("���i�F�t�E�E�E�b�t�t�t��@���[�I�J�V�C�[��");

                    UpdateMainMessage("�A�C���F�������A���Ɍ��Ă�E�E�E�S���ËL���Ă�邩��ȁB");

                    UpdateMainMessage("���i�F�o�J�A�C���ɂ͈ꐶ�����ȉۑ肶��Ȃ��������");

                    UpdateMainMessage("�A�C���F�Ȃ����i�B���O�A����ς艀�|�Ƃ��D���Ȃ񂾂ȁH");

                    UpdateMainMessage("���i�F����A�����ˁB");

                    UpdateMainMessage("�A�C���F��������āA�򑐂Ƃ����ď΂��Ă邨�O���āE�E�E");

                    UpdateMainMessage("���i�F����H");

                    UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E�@�E�E�E");

                    UpdateMainMessage("�A�C���F�����I�@�������ɂ��ʔ����`�̖؂����邼�A�s���Ă݂悤���I");

                    UpdateMainMessage("���i�F���[�[�[�A���捡�́B");

                    UpdateMainMessage("�A�C���F�s���Ă݂悤���I�@���ȁI");

                    UpdateMainMessage("���i�F�E�E�E�n�C�n�C�A������܂�����");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "---- �t�@�[�W���{�a�A�H���̊Ԃɂ� ----";
                        md.ShowDialog();
                    }

                    UpdateMainMessage("�A�C���F�}�W����E�E�E���̔������E�E�E");

                    UpdateMainMessage("�A�C���F���͍K���҂��I�@�܂��܂��H�킹�Ă��炤���I�I");

                    UpdateMainMessage("���i�F���݂܂���A�J�[���n���c�݁B���̃o�J���E�E�E");

                    UpdateMainMessage("�J�[���F�悢�A�C�̍ςނ܂ŐH�����B");

                    UpdateMainMessage("�J�[���F��������A�M���͐�����A" + Database.VINSGALDE + "�֌������Ƃ̎��B");

                    UpdateMainMessage("�J�[���F������ӂ�łȂ����A�悢�ȁB");

                    UpdateMainMessage("���i�F�n�C�A���肪�Ƃ��������܂��B");

                    UpdateMainMessage("�J�[���F����ƋM�N�B");

                    UpdateMainMessage("�A�C���F�b���S�E�E�E�n�C�I");

                    UpdateMainMessage("�J�[���F�����f�B�X�Ƃ͂����ŉ�������ˁB");

                    UpdateMainMessage("�A�C���F�����A�܂��ł����E�E�E");

                    UpdateMainMessage("�A�C���F�����A����ς�ǂ����ɐ���ł��ł����H���a�Ղ����E�E�E");

                    UpdateMainMessage("�J�[���F���R�̎��B");

                    UpdateMainMessage("�A�C���F�g�z�z�E�E�E����ꂽ���Ȃ��񂾂��E�E�E");

                    UpdateMainMessage("�J�[���F�b�t�A�������ɐ��a�Ղ̂ǐ^�񒆂Ńo�g�����d�|���鎖�͂Ȃ��낤�B");

                    UpdateMainMessage("�J�[���F�����f�B�X�͋M�N�ɉ����b������悤���������B");

                    UpdateMainMessage("�J�[���F���̐H�����I�������A����Ă݂邪�悢�B");

                    UpdateMainMessage("�A�C���F�����ł��B�������A���̋{�a�L�����ȁE�E�E�ǂ��ɍs���΁E�E�E");

                    UpdateMainMessage("�@�@�y�y�y�@���̏u�ԁB�@�A�C���͗y����������`����Ă���E�C������������B�@�z�z�z");

                    UpdateMainMessage("�A�C���F�n�b�I�@�܂������̈����Ƌ��|�I�I");

                    UpdateMainMessage("�J�[���F���̌����̒��Ō݂��Ɋ��m�o����Ƃ́A�M�N�������r���オ�����悤���ȁB");

                    UpdateMainMessage("�A�C���F����E�E�E����͒P�Ƀ{�P�t���̎E�C���Ј��I�����邾���ł��E�E�E");

                    UpdateMainMessage("���i�F�ł��A�s�v�c��ˁB�A�C������������@�m���悤�Ƃ�����A���m�ł�����ł���H");

                    UpdateMainMessage("�A�C���F�����A�܂��E�E�E�E");

                    UpdateMainMessage("���i�F�b�����؂肷��܂Ń����f�B�X����͑҂��ĂĂ��ꂽ�B���Ď�����Ȃ��������");

                    UpdateMainMessage("�A�C���F����A��΂ɂ��肦�Ȃ�����B���̎t���Ɍ����ĂȂ��Ȃ��B");

                    UpdateMainMessage("���i�F�b�t�t�A����Ȏ������Ă�ƁE�E�E�m��Ȃ����[��");

                    UpdateMainMessage("�A�C���F���H�H�H");

                    UpdateMainMessage("�@�@�y�y�y�@���I�I�H�H�H���I�I�I�@�z�z�z");

                    UpdateMainMessage("�A�C���F�����킠�������I�I�I");

                    UpdateMainMessage("�A�C���F���傿�傿��A�~�߂Ă����{�P�t���E�E�E���̊ԂɌ���ɔE�ъ�����񂾁A�������E�E�E");

                    UpdateMainMessage("�����f�B�X�F���a�Ղ̂ǐ^�񒆂Ńo�g�����d�|���鎖���˂��Ǝv���Ă񂶂�˂����A�{�P���B");

                    UpdateMainMessage("�A�C���F���₢�₢��E�E�E");

                    UpdateMainMessage("�����f�B�X�F�_���W�����A�ǂ��������B");

                    UpdateMainMessage("�A�C���F�E�E�E�܂��E�E�E");

                    UpdateMainMessage("�A�C���F�\�z�𒴂��Ă��B");

                    UpdateMainMessage("�A�C���F�����̐g�̒������������A����ȋC�������B");

                    UpdateMainMessage("�A�C���F���A����������Ƃ��낢�����Ă݂悤�Ǝv���񂾁B");

                    UpdateMainMessage("�����f�B�X�F�ق�");

                    UpdateMainMessage("�A�C���F�m��Ȃ�������������B");

                    UpdateMainMessage("�A�C���F�J�[���搶�ɂ������Ă��炢���������R�قǂ���񂾂��B");

                    UpdateMainMessage("�A�C���F�����Ă��炤�ȑO�ɁA�����̎葫���g���āA�܂����낢��E���Ă݂����񂾁B");

                    UpdateMainMessage("�A�C���F����Ȃ��Ƃ��܂ł����Ă��A�t����J�[���搶�ɂ͏��Ă�C�����Ȃ��B");

                    UpdateMainMessage("�����f�B�X�F���������A�����������ď����B");

                    UpdateMainMessage("�����f�B�X�F" + Database.VINSGALDE + "�����A�撣���Ă�����B");

                    UpdateMainMessage("�A�C���F�����A�Ȃ�ł����m���Ă�񂾁E�E�E");

                    UpdateMainMessage("�����f�B�X�F���l�͍������������A�Ă߂��ɘb���s���O���畷������Ă񂾂�B");

                    UpdateMainMessage("�A�C���F�����E�E�E�������Ⓖ�������������E�E�E");

                    UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E�@�E�E�E");

                    UpdateMainMessage("�A�C���F�Ȃ��t��");

                    UpdateMainMessage("�A�C���F���A" + Database.VINSGALDE + "�ɍs���āA���x�����B");

                    UpdateMainMessage("�A�C���F�r�𖁂��Ė����āA�t���ɏ����Ă݂���I");

                    UpdateMainMessage("�����f�B�X�F�E�E�E�@�P�b�E�E�E");

                    UpdateMainMessage("�����f�B�X�F���������撣���A�b�J�b�J�b�J�B");

                    UpdateMainMessage("�A�C���F�΂��Ă���̂����̂������A��΂�����ȁB");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "---- �t�@�[�W���{�a�A�x���̊Ԃɂ� ----";
                        md.ShowDialog();
                    }

                    UpdateMainMessage("�A�C���F���₠�A�������{�a�������������ȁB");

                    UpdateMainMessage("���i�F�܂�����o���ĂȂ����ˁB�{��������ˁA�G���~�l�ƃt�@���l�́B");

                    UpdateMainMessage("�A�C���F�����A���񂾂��̍L���B�Q�l����Ő݌v�����炵������ȁB");

                    UpdateMainMessage("���i�F�E�E�E�˂��A�C��");

                    UpdateMainMessage("�A�C���F��H");

                    UpdateMainMessage("���i�F�A���^�ǂ����ꏏ�ɗ��ė~������������Ƃ������ĂȂ����������H");

                    UpdateMainMessage("�A�C���F���A�����A�������������B�Y��鏊�������B");

                    UpdateMainMessage("���i�F������ƁE�E�E����ȖY���悤�ȓ��e�Ȃ́H");

                    UpdateMainMessage("�A�C���F���A���₢�∫���E�E�E");

                    UpdateMainMessage("�A�C���F�Y��Ă͂��Ȃ����B");

                    UpdateMainMessage("�A�C���F�����A������ƂȁB");

                    UpdateMainMessage("���i�F��`�A�n�b�L�����Ȃ���ˁB���ǂ����ɂ͍s���́H�s���Ȃ��́H");

                    UpdateMainMessage("�A�C���F�E�E�E�@�܂��@�E�E�E");

                    UpdateMainMessage("�����f�B�X�F�s���Ă��");

                    UpdateMainMessage("�A�C���F�t���A���̂܂ɁE�E�E");

                    UpdateMainMessage("�����f�B�X�F�Ă߂�������{�a�ɗ�����Ԃ̖ړI�͂��ꂾ��B");

                    UpdateMainMessage("���i�F�����A�����Ȃ́H�A�C��");

                    UpdateMainMessage("�A�C���F�E�E�E����");

                    UpdateMainMessage("�A�C���F����A���ɕςȓ��e���Ă킯����Ȃ��񂾁B");

                    UpdateMainMessage("�A�C���F�������������ɍs�����Ă͕̂ς��Ȃ��Ďv���Ă�ʂ��E�E�E");

                    UpdateMainMessage("�����f�B�X�F�ςł��Ȃ�ł��˂��A�s���Ă��B");

                    UpdateMainMessage("�����f�B�X�F���������牴���ꏏ�ɍs���Ă��B���A���܂������ȁB");

                    UpdateMainMessage("�A�C���F���A�����B�n�n�n�E�E�E������B");

                    UpdateMainMessage("���i�F�ǂ��ɍs���́H");

                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E�@�E�E�E");

                    UpdateMainMessage("�A�C���F�G���}�E�Z�t�B�[�l����̕�ꂾ�B");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "---- �t�@�[�W���{�a�A��n�ɂ� ----";
                        md.ShowDialog();
                    }

                    UpdateMainMessage("���i�F����ȏ����������̂ˁE�E�E�m��Ȃ�������B");

                    UpdateMainMessage("�����f�B�X�F�B�����悤�݌v����Ă񂾁B�����ɂ͌������Ȃ��ē��R���B");

                    UpdateMainMessage("���i�F�����Ȃ�ł����E�E�E");

                    UpdateMainMessage("�A�C���F�m���G���}����̕�W�́E�E�E���̕ӂ肾�������ȁE�E�E");

                    UpdateMainMessage("�A�C���F�E�E�E�����I");

                    UpdateMainMessage("�A�C���F�N���E�E�E����E�E�E");
                    mainMessage.Visible = false;

                    for (int ii = 0; ii < 10; ii++)
                    {
                        ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_FAZIL_CASTLE, (ii + 1) * 0.1f);
                        System.Threading.Thread.Sleep(400);
                        Application.DoEvents();
                    }

                    labelEnding.Visible = false;
                    labelEnding2.Visible = false;
                    this.BackColor = Color.White;
                    UpdateEndingMessage2("");
                    GroundOne.WE2.SeekerEndingRoll = true;

                    GroundOne.PlayDungeonMusic(Database.BGM09, Database.BGM09LoopBegin);
                    
                    UpdateEndingMessage2("Dungeon Player\r\n �` The Liberty Seeker �`");

                    UpdateEndingMessage2("�X�g�[���[�@�@�y�@���ǁ@�o�́@�z\r\n�@�@�@�@�@�@�@�y�@�ҒJ�@�F�I�@�z");

                    UpdateEndingMessage2("���y�@�@�y�@���ǁ@�W���Y�@�z");

                    UpdateEndingMessage2("�o�g���V�X�e���@�@�y�@���ǁ@�o�́@�z");

                    UpdateEndingMessage2("�}�b�v����@�y�@���ǁ@�o�́@�z");

                    UpdateEndingMessage2("�����X�^�[����@�@�y�@�ҒJ�@�F�I�@�z\r\n�@�@�@�@�@�@�@�@�@�y�@���ǁ@�o�́@�z");

                    UpdateEndingMessage2("���@�^�X�L���@�@�@�y�@���ǁ@�o�́@�z");

                    UpdateEndingMessage2("�T�E���h�G�t�F�N�g�@�y�@���ǁ@�o�́@�z");

                    UpdateEndingMessage2("�A�C�e������@�@�y�@�΍��@�T��@�z\r\n�@�@�@�@�@�@�@�@�y�@���ǁ@�o�́@�z");

                    UpdateEndingMessage2("�O���t�B�b�N�@�@�@�y�@�ҒJ�@�F�I�@�z");

                    UpdateEndingMessage2("�v���O���}�[�@�y�@���ǁ@�o�́@�z");

                    UpdateEndingMessage2("�X�y�V�����T���N�X�@�@�y�@KANAKO�@�z");

                    UpdateEndingMessage2("�v���f���[�T�[�@�@�y�@���ǁ@�o�́@�z");


                    UpdateEndingMessage("�@���i�F����E�E�E���F���[���񂶂�Ȃ��H");

                    UpdateEndingMessage("�@�A�C���F�����E�E�E");

                    UpdateEndingMessage("�@�A�C���F�Ȃ��A����ȏ��ɁE�E�E");

                    UpdateEndingMessage("�@�����f�B�X�F�E�E�E");

                    UpdateEndingMessage("�@�A�C���F�i�@�_���W��������o��Ō�@�j");

                    UpdateEndingMessage("�@�A�C���F�i�@���̐^�����ȋ�Ԃ̒��ŁA�x�z�����Ō�A���ɍ��������Ɓ@�j");

                    UpdateEndingMessage("�@�A�C���F�i�@�B��̗�O�Ƃ��ā@�j");

                    UpdateEndingMessage("�@�A�C���F�i�@���F���[�E�A�[�e�B�������ɖ߂��Ă����Ɠ`���Ă��ꂽ�@�j");

                    UpdateEndingMessage("�@�A�C���F�i�@���i�����������ƕ�����������@�j");

                    UpdateEndingMessage("�@�A�C���F�i�@���̏���Ȃ킪�܂܂�������������Ȃ����@�j");

                    UpdateEndingMessage("�@�A�C���F�i�@���͂��̐^�����ȋ�ԂɈ������܂�钆�ŕK���Ɋ�����@�j");

                    UpdateEndingMessage("�@�A�C���F�i�@������x�z���͗�O�I�ɕ�������Ă��ꂽ�悤���@�j");

                    UpdateEndingMessage("�@�A�C���F�i�@�������A��΂̏������������@�j");

                    UpdateEndingMessage("�@�A�C���F�i�@�����Ƃ��Ă̊����𕜊������邾���ł��������I�ɂ͍s���Ȃ��@�j");

                    UpdateEndingMessage("�@�A�C���F�i�@���������Ƃ��Ă��A�{�l�̍��܂ł̋L���͈�ؕێ�����Ȃ��@�j");

                    UpdateEndingMessage("�@�A�C���F�i�@�L���͊��S�ɏ��������@�j");

                    UpdateEndingMessage("�@�A�C���F�i�@�Œ���̐����������s���R�A�����݂̂����Ƃ��Đ������܂��j");

                    UpdateEndingMessage("�@�A�C���F�i�@���̎��ۂ͎x�z����ʂ��ĉ��ɂ����`�����Ă���@�j");

                    UpdateEndingMessage("�@�A�C���F�E�E�E�@�E�E�E�@�E�E�E");

                    UpdateEndingMessage("�@�A�C���F�t������ς�E�E�E���F���[�͋L���r���Ȃ̂��H");

                    UpdateEndingMessage("�@�����f�B�X�F�E�E�E");

                    UpdateEndingMessage("�@�����f�B�X�F�ʏ�̉�b�ł���̕�����񂾂�");

                    UpdateEndingMessage("�@�����f�B�X�F�G���~�̎���t�@���A�J�[���̎��A������񉴂��܂߂�");

                    UpdateEndingMessage("�@�����f�B�X�F�A�[�e�B�̖�Y�́A���S�Ɋo���Ă˂��B");

                    UpdateEndingMessage("�@�����f�B�X�F�B���ȎG�k�̒��ł͓ǂݐ؂�Ȃ��ʂ����邪");

                    UpdateEndingMessage("�@�����f�B�X�F����DUEL���Z��ŁA�������������̎���");

                    UpdateEndingMessage("�@�����f�B�X�F���̎��̐퓬�̓����ŁA����������������Ă�������B");

                    UpdateEndingMessage("�@�����f�B�X�F���̃A�[�e�B�͊��S�ɋL���r�����B");

                    UpdateEndingMessage("�@���i�F�G���}����Ƃ̋L�����E�E�E�S���c���ĂȂ��̂�ˁE�E�E");

                    UpdateEndingMessage("�@�����f�B�X�F���낤�ȁB");

                    UpdateEndingMessage("�@���i�F����ȁE�E�E���Ⴀ�A�ǂ����āE�E�E");

                    UpdateEndingMessage("�@�A�C���F�E�E�E�@�E�E�E�@�E�E�E");

                    UpdateEndingMessage("�@�A�C���F�i�@�L������؏������ꂽ��ԂŁ@�j");

                    UpdateEndingMessage("�@�A�C���F�i�@���F���[�́E�E�E�����ŘȂ�ł���E�E�E�@�j");

                    UpdateEndingMessage("�@�A�C���F�i�@�ň��̐l������揊�̑O�ŁE�E�E�@�j");

                    UpdateEndingMessage("�@�A�C���F�i�@�������E�E�E�����ƁE�E�E�j");

                    UpdateEndingMessage("�@���i�F�˂��A���Ă���B");

                    UpdateEndingMessage("�@�A�C���F��H");

                    UpdateEndingMessage("�@���i�F���F���[����̑����A�Ԃ��炢�Ă��B");

                    UpdateEndingMessage("�@�A�C���F�E�E�E�z���g���E�E�E���Ă����ԂȂ񂾁H");

                    UpdateEndingMessage("�@���i�F�A�����B�A�i�̉�");

                    UpdateEndingMessage("�@�A�C���F�A�����B�A�i�́E�E�E�ԁH");

                    UpdateEndingMessage("�@���i�F���̎����ɂ́A�ő��ɍ炩�Ȃ��Ԃ�B");

                    UpdateEndingMessage("�@�A�C���F�����Ȃ񂾁E�E�E");

                    UpdateEndingMessage("�@�A�C���F�Ԍ��t�Ƃ��A�������肷��̂��H");

                    UpdateEndingMessage("�@���i�F����A�Ԍ��t�͂ˁE�E�E");

                    UpdateEndingMessage("");

                    UpdateEndingMessage("");

                    for (int ii = 0; ii < 3000; ii++)
                    {
                        point = new PointF(point.X, point.Y - 1);
                        this.Invalidate();

                        int sleep = 70;
                        if (ii > 2000) { sleep = 50; }
                        if (ii > 2500) { sleep = 30; }
                        if (ii > 2700) { sleep = 15; }
                        System.Threading.Thread.Sleep(sleep);
                        Application.DoEvents();
                    }

                    this.endingText3.Add("���@��Ղ̍ĉ�@���@���Č����̂�B"); 
                    for (int ii = 0; ii < 800; ii++)
                    {
                        this.Invalidate();
                        System.Threading.Thread.Sleep(20);
                        Application.DoEvents();
                    }

                    GroundOne.StopDungeonMusic();
                    GroundOne.WE2.SeekerEnd = true;
                    this.we.TruthCompleteArea5 = true;
                    this.we.TruthCompleteArea5Day = this.we.GameDay;
                    using (SaveLoad sl = new SaveLoad())
                    {
                        sl.MC = this.MC;
                        sl.SC = this.SC;
                        sl.TC = this.TC;
                        sl.WE = this.WE;
                        sl.Truth_KnownTileInfo = this.Truth_KnownTileInfo; // ��Ғǉ�
                        sl.Truth_KnownTileInfo2 = this.Truth_KnownTileInfo2; // ��Ғǉ�
                        sl.Truth_KnownTileInfo3 = this.Truth_KnownTileInfo3; // ��Ғǉ�
                        sl.Truth_KnownTileInfo4 = this.Truth_KnownTileInfo4; // ��Ғǉ�
                        sl.Truth_KnownTileInfo5 = this.Truth_KnownTileInfo5; // ��Ғǉ�
                        sl.SaveMode = true;
                        sl.StartPosition = FormStartPosition.CenterParent;
                        sl.ShowDialog();
                        sl.RealWorldSave();
                    }

                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    return;
                }
                #endregion
                #region "�������E"
                else if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd && GroundOne.WE2.SeekerEvent511 && !GroundOne.WE2.SeekerEvent601)
                {
                    GroundOne.StopDungeonMusic();

                    dayLabel.Visible = false;
                    buttonHanna.Visible = false;
                    buttonDungeon.Visible = false;
                    buttonRana.Visible = false;
                    buttonGanz.Visible = false;
                    this.backgroundData = null;
                    this.BackColor = Color.Black;

                    UpdateMainMessage("�A�C���F���E�E�E���E�E�E");

                    UpdateMainMessage("�A�C���F���E�E�E�������H");

                    UpdateMainMessage("        �w�A�C���͏h���̐Q������N���オ�����B�x");

                    UpdateMainMessage("�A�C���F���̂U�����E�E�E�N����ɂ͏����������炢���ȁB");

                    UpdateMainMessage("�A�C���F�E�E�E��H�������ɗ����Ă�ȁB");
                    
                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.Message = "�y���i�̃C�������O�z����ɓ���܂����B";
                        GroundOne.playbackMessage.Insert(0, md.Message);
                        GroundOne.playbackInfoStyle.Insert(0, TruthPlaybackMessage.infoStyle.notify);
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.ShowDialog();
                    }

                    GetItemFullCheck(mc, Database.RARE_EARRING_OF_LANA);

                    UpdateMainMessage("�A�C���F���i�̃C�������O����˂����E�E�E���ł���ȕ����E�E�E");
                    
                    UpdateMainMessage("�A�C���F�E�E�E�@���ł���񂾂����@�E�E�E�@���i�����Ƃ����̂��H");
                    
                    UpdateMainMessage("�A�C���F������A����ȃ��P������ȁE�E�E���Ⴀ���ł��E�E�E");
                    
                    UpdateMainMessage("�A�C���F�܂��������B�Ƃ肠�����A�ڊo�߂��킯�����A���ɂł��o�Ă݂�Ƃ��邩�B");
                    
                    ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                    this.BackColor = Color.WhiteSmoke;
                    buttonHanna.Visible = true;
                    buttonDungeon.Visible = true;
                    buttonRana.Visible = true;
                    buttonGanz.Visible = true;
                    dayLabel.Visible = true;
                    
                    UpdateMainMessage("�A�C���F���āA���������ȁB", true);

                    we.AlreadyRest = true; // ���N�����Ƃ�����X�^�[�g�Ƃ���B

                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                    GroundOne.WE2.SeekerEvent601 = true;
                    Method.AutoSaveTruthWorldEnvironment();
                    Method.AutoSaveRealWorld(this.MC, this.SC, this.TC, this.WE, null, null, null, null, null, this.Truth_KnownTileInfo, this.Truth_KnownTileInfo2, this.Truth_KnownTileInfo3, this.Truth_KnownTileInfo4, this.Truth_KnownTileInfo5);
                }
                #endregion
                #region "�R�K���e"
                else if (this.we.TruthCompleteArea3 && !this.we.TruthCommunicationCompArea3)
                {
                    UpdateMainMessage("�A�C���F�悵�A�R�K�������N���A�������I�I");

                    UpdateMainMessage("���i�F�b�t�t�A���Ⴀ�F�ɒm�点�ɍs���܂��傤����");

                    UpdateMainMessage("�A�C���F�����A�������ȁI");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "�A�C���͈�ʂ�A���̏Z�l�B�ɐ��������A���Ԃ����X�Ɖ߂��Ă������B";
                        md.ShowDialog();

                        md.Message = "���̓��̖�A�n���i�̏h�����ɂ�";
                        md.ShowDialog();
                    }

                    GroundOne.StopDungeonMusic();
                    GroundOne.PlayDungeonMusic(Database.BGM15, Database.BGM15LoopBegin);
                     
                    UpdateMainMessage("�A�C���F���₠�E�E�E������������������ȁA���̋��̐�");

                    UpdateMainMessage("���i�F�{����ˁA�����ǂꂮ�炢�ʂ������o���Ă��Ȃ����炢����B");

                    UpdateMainMessage("�A�C���F���i�̃}�b�v�͖{���ɏ����������B�T���L���[�T���L���[�ȁB");

                    UpdateMainMessage("���i�F����̓��[�v�����������炠�܂���ɂ����ĂȂ��������ǂˁE�E�E");

                    UpdateMainMessage("�A�C���F����Ȏ��˂����āI�@�s�������������Ɣc���ł��鎞�_�ő叕���肳�I�b�n�b�n�b�n�I");

                    UpdateMainMessage("���i�F�b�t�t�A����Ȃ�ǂ��񂾂��ǁ�");

                    UpdateMainMessage("�A�C���F�����A���ꂩ������ނ��I�b�n�b�n�b�n�I  ���`���A�f�ꂿ���A������t�ǉ��ł��邩�H");

                    UpdateMainMessage("�n���i�F�n�C��A������ł����݂ȁB");

                    UpdateMainMessage("�A�C���F�����A�R���R���I�T���L���[�I");

                    // ���_���������Ă��Ȃ��i328)
                    // �o�b�h�G���h����
                    if (!we.dungeonEvent328)
                    {
                        UpdateMainMessage("���i�F�ł��A�A�C���͂���ŗǂ������́H");

                        UpdateMainMessage("�A�C���F�H�@���̘b���H");

                        UpdateMainMessage("���i�F�Ō�̋��̏���B�������ɋC�ɂ���������Ă��݂��������ǁB");

                        UpdateMainMessage("�A�C���F�����A�A�����B");

                        UpdateMainMessage("�A�C���F����ʂ�߂��Ă����������邵�ȁB�܂��A�C�ɂ���Ȃ��āB");

                        UpdateMainMessage("�A�C���F���A�q���g�炵���q���g���Ȃ��񂾁B�ǂ����悤���Ȃ���������B");

                        UpdateMainMessage("�A�C���F���������ꍇ�́A�f���ɖڂ̑O��i�߂�Ɍ���B�@���ꂪ�P�Ԃ��B");

                        UpdateMainMessage("���i�F��`�A����Ȃ�ǂ��񂾂��ǁE�E�E");

                        UpdateMainMessage("�n���i�F�����Ō�ɂ������̂����H");

                        UpdateMainMessage("���i�F���`��A�{�X��|������ɂˁB�@�~��K�i�̑O�ɊŔ��������̂�B");

                        UpdateMainMessage("���i�F�������ˁE�E�E���`�ƁE�E�E");

                        UpdateMainMessage("�@�@�@�@�w�@�����𓱂����ҁA�������̒T���ɂĉi���ɜf�r���A���_��m�邱�Ɩ����A��葱���邪�悢�@�x");

                        UpdateMainMessage("�n���i�F�����A����͂�₱���������񂵕����˂��B");

                        UpdateMainMessage("���i�F�����Ȃ�ł���A���΂���͂��ꉽ��������܂��H");

                        UpdateMainMessage("�n���i�F����A�A�^�V���ᖳ�����ˁB�_���W�����ɍs���Ă�{�l�ɂ���������Ȃ���A���������̂́B");

                        UpdateMainMessage("�A�C���F����Ȃ��E�E�E������f�ꂳ��ł����������̂́E�E�E");

                        UpdateMainMessage("�n���i�F���_���Ă����̂́A�T�����̂����H");

                        UpdateMainMessage("�A�C���F���`��A����Ȃ�ł����ǂˁA���͍ŏ����玟�̒i�K�̃G���A�ɓ���O�ɕʂ̊Ŕ������Ăł��ˁE�E�E");
                        
                        GroundOne.StopDungeonMusic();

                        ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_NIGHT);

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "�A�C�����\�񂵂Ă��������ɂ�";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("�A�C���F�ӂ��E�E�E�o�b�N�p�b�N�������ƁE�E�E");

                        UpdateMainMessage("�A�C���F���悢��R�K�����e�A������S�K���E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E");

                        UpdateMainMessage("�A�C���F���i�́E�E�E���v�Ȃ񂾂낤���B");

                        UpdateMainMessage("�A�C���F���������鎞�ɁA����ȕ��Ɋ�̒��F���ς���āE�E�E");

                        UpdateMainMessage("�A�C���F�ł��܂��A���i�ɂ����������Ȃ������Ăѐ��ŁA�������킩�����킯���B");

                        UpdateMainMessage("�A�C���F���ۍ������i�̑̒��������킯����˂��B");

                        UpdateMainMessage("�A�C���F���v�ȃn�Y�E�E�E�s����͂����B");

                        this.backgroundData = null;
                        this.BackColor = Color.Black;

                        UpdateMainMessage("�A�C���F�E�E�E�@�������A���Ȃ񂾂낤���̕s���́@�E�E�E");

                        UpdateMainMessage("�A�C���F���̐������[�g�̋����ŔB");

                        UpdateMainMessage("�A�C���F����͖{���ɒP�Ȃ鋺���Ȃ񂾂낤���B");

                        UpdateMainMessage("�A�C���F����Ƃ��E�E�E�B");

                        UpdateMainMessage("�A�C���F�Ƃ͂����A���i�ƈꏏ�Ƀ_���W�����ŉ��w�ɂ͋߂Â��Ă��邱�Ƃ͊m�����E�E�E");

                        UpdateMainMessage("�A�C���FFiveSeeker�̃��F���[�����������ꏏ���B");

                        UpdateMainMessage("�A�C���F���̃����o�[�Ȃ�ŉ��w�֕K���s����");
                        
                        UpdateMainMessage("�A�C���F���͐M���Ă�B");

                        UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E");

                        UpdateMainMessage("�A�C���F�Ȃɂ��d��Ȏ��������Ƃ��Ă���C������E�E�E");

                        UpdateMainMessage("�A�C���F��������Ԃ��̕t���Ȃ������A���邢�́A���������߂��Ȃ������B");

                        UpdateMainMessage("�A�C���F����A����ȑO�ɂ��B");

                        UpdateMainMessage("�A�C���F���͂������x���E�E�E");

                        UpdateMainMessage("�A�C���F�L���͂˂����A���̊��o");

                        UpdateMainMessage("�A�C���F���͈ȑO�A�ǂ����Ły�����Ȃ낤�z�Ɛ�����");

                        UpdateMainMessage("�A�C���F�������ꂪ�ǂ�����N���Ă����L���Ȃ̂�");
                        
                        UpdateMainMessage("�A�C���F�c���ł��Ȃ��ł���");

                        UpdateMainMessage("�A�C���F�S�K�֌��������̑���");

                        UpdateMainMessage("�A�C���F�ǂ�ǂ�[���D�^�Ƀn�}���Ă����悤��");

                        UpdateMainMessage("�A�C���F��̒m��Ȃ��łւƁE�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E�@�E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E");

                        GroundOne.WE2.TruthBadEnd3 = true;
                    }
                    else if (!we.dungeonEvent332)
                    {
                        UpdateMainMessage("���i�F�ł��A�A�C���͂���ŗǂ������́H");

                        UpdateMainMessage("�A�C���F�H�@���̘b���H");

                        UpdateMainMessage("���i�F�Ō�̌��_����B�A�����������̂ɂT���ɒ��킵�Ȃ��Ȃ�āB");

                        UpdateMainMessage("�A�C���F�����E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E");

                        UpdateMainMessage("�A�C���F����A�ǂ��񂾁B");

                        UpdateMainMessage("�A�C���F���ɂƂ��āA�ǂ������̂T���͐i�񂶂�s���˂��C�������B");

                        UpdateMainMessage("�A�C���F���ꂾ���̎����B");

                        UpdateMainMessage("���i�F��`�A����Ȃ�ǂ��񂾂��ǁE�E�E");

                        UpdateMainMessage("�n���i�F�����Ō�ɂ������̂����H");

                        UpdateMainMessage("���i�F���`��A�{�X��|������ɂˁB�@�~��K�i�̑O�ɊŔ��������̂�B");

                        UpdateMainMessage("���i�F�������ˁE�E�E���`�ƁE�E�E");

                        UpdateMainMessage("�@�@�@�@�w�@�����𓱂����ҁA�������̒T���ɂĉi���ɜf�r���A���_��m�邱�Ɩ����A��葱���邪�悢�@�x");

                        UpdateMainMessage("�n���i�F�����A����͂�₱���������񂵕����˂��B");

                        UpdateMainMessage("���i�F�����Ȃ�ł���A���΂���B�ł��A���͂ł��ˁI�@�����Ă���������I�H");

                        UpdateMainMessage("���i�F���Ƃ����̒��o�J�A�C�����y���_��m�邱�Ɩ����z�̈Ӗ��ɑ������錴�_�����������������ł���I�H");

                        UpdateMainMessage("�n���i�F�����Ȃ̂����H���܂ɂ͂�邶��Ȃ����A�A�b�n�n�n�B");

                        UpdateMainMessage("�A�C���F�܂��A�����ȏ����M���^���������ǂȁA�n�n�n�E�E�E");

                        UpdateMainMessage("���i�F�ŁA�����܂Ō����Ƃ��ĉ��łT���ɂ͌�����Ȃ������̂��A���ɂ͂�����Ɨ���s�\�����ǁB");

                        UpdateMainMessage("�A�C���F���`��A�܂��܂��A����͗ǂ�����˂����I");

                        UpdateMainMessage("�A�C���F���΂����A�A�J�V�W�A�̃X�p�Q�b�e�B��ǉ��I");

                        UpdateMainMessage("�n���i�F�͂���A�҂��ĂȁB");

                        GroundOne.StopDungeonMusic();

                        ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_NIGHT);

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "�A�C�����\�񂵂Ă��������ɂ�";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("�A�C���F�ӂ��E�E�E�o�b�N�p�b�N�������ƁE�E�E");

                        UpdateMainMessage("�A�C���F���悢��R�K�����e�A������S�K���E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E");

                        UpdateMainMessage("�A�C���F���i�́E�E�E���v�Ȃ񂾂낤���B");

                        UpdateMainMessage("�A�C���F���������鎞�ɁA����ȕ��Ɋ�̒��F���ς���āE�E�E");

                        UpdateMainMessage("�A�C���F�ł��܂��A���i�ɂ����������Ȃ������Ăѐ��ŁA�������킩�����킯���B");

                        UpdateMainMessage("�A�C���F���ۍ������i�̑̒��������킯����˂��B");

                        UpdateMainMessage("�A�C���F���v�ȃn�Y�E�E�E�s����͂����B");

                        this.backgroundData = null;
                        this.BackColor = Color.Black;

                        UpdateMainMessage("�A�C���F�E�E�E�@�������A���Ȃ񂾂낤���̕s���́@�E�E�E");

                        UpdateMainMessage("�A�C���F���_�����������̂́AOK�Ȃ񂾂낤");

                        UpdateMainMessage("�A�C���F�������A���ɂƂ��Ă͂��̂T�����ǂ����Ă�");

                        UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                        UpdateMainMessage("�A�C���F�|���Đ�ɐi�߂Ȃ������B");

                        UpdateMainMessage("�A�C���F���̋��|�̍����������g������Ȃ��ł���B");

                        UpdateMainMessage("�A�C���F�L���̉�z������͌��Ă��邪");

                        UpdateMainMessage("�A�C���F���ꂪ�����v���N�������Ă���Ă�̂��A���̉�����قƂ�ǒ͂߂Ȃ��ł���B");

                        UpdateMainMessage("�A�C���F���̃����o�[�Ȃ�ŉ��w�֕K���s����A���̊m�M�͂���B");

                        UpdateMainMessage("�A�C���F�E�E�E");
                        
                        UpdateMainMessage("�A�C���F�����A���ꂾ����");

                        UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E");

                        UpdateMainMessage("�A�C���F�Ȃɂ��d��Ȏ��������Ƃ��Ă���C������E�E�E");

                        UpdateMainMessage("�A�C���F��������Ԃ��̕t���Ȃ������A���邢�́A���������߂��Ȃ������B");

                        UpdateMainMessage("�A�C���F����A����ȑO�ɂ��B");

                        UpdateMainMessage("�A�C���F���͂������x���E�E�E");

                        UpdateMainMessage("�A�C���F�L���͂˂����A���̊��o");

                        UpdateMainMessage("�A�C���F���͈ȑO�A�ǂ����Ły�����Ȃ낤�z�Ɛ�����");

                        UpdateMainMessage("�A�C���F�������ꂪ�ǂ�����N���Ă����L���Ȃ̂�");

                        UpdateMainMessage("�A�C���F�c���ł��Ȃ��ł���");

                        UpdateMainMessage("�A�C���F�S�K�֌��������̑���");

                        UpdateMainMessage("�A�C���F�ǂ�ǂ�[���D�^�Ƀn�}���Ă����悤��");

                        UpdateMainMessage("�A�C���F��̒m��Ȃ��łւƁE�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E�@�E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E");

                        GroundOne.WE2.TruthBadEnd3 = true;
                    }
                    else
                    {
                        UpdateMainMessage("�A�C���F�ӂ����ƁE�E�E");

                        UpdateMainMessage("�A�C���F�������A�Ō�̊ŔE�E�E�C�ɂȂ�񂾂�ȁE�E�E");

                        UpdateMainMessage("�A�C���F�y���z�y���z���Č����Ă��Ȃ��E�E�E");

                        UpdateMainMessage("���i�F���̊K�w�̉����q���g�ɂȂ��Ă�񂶂�Ȃ��́H");

                        UpdateMainMessage("�A�C���F�܂��A����Ȃ炻��ŁA������₷�����ǂȁB");

                        UpdateMainMessage("���i�F�ʂ̉����B���ꂽ�Ӗ��Ƃ��H���_���݂����ɁB");

                        UpdateMainMessage("�A�C���F����A�����������ނ���Ȃ��������B");

                        UpdateMainMessage("�A�C���F���������̂́A�𓚂����߂�₢��������˂����ȁB");

                        UpdateMainMessage("�n���i�F�Ō�ɉ��ď����Ă������̂��A���m�Ɏv���o���邩���H");

                        UpdateMainMessage("�A�C���F���A�����B�����ƁE�E�E");

                        UpdateMainMessage("�A�C���F�@�@�@�@�w�@���_��m�肵�ҁA�@�@�������́@�y���z�y���z�@�x");

                        UpdateMainMessage("�n���i�F�ӂ���A�����������e�������񂾂ˁB");

                        UpdateMainMessage("�A�C���F���΂����͉��������邩�H");

                        UpdateMainMessage("�n���i�F����A�F�ڌ������t���Ȃ��ˁB");

                        UpdateMainMessage("�A�C���F�������E�E�E");

                        UpdateMainMessage("�n���i�F����܂�C�ɂ�����_������B");

                        UpdateMainMessage("�n���i�F�ǂݐ؂�Ȃ����́A�f���ɂ��̂܂ܐS�ɗ��߂Ă����񂾂ˁB");

                        UpdateMainMessage("�A�C���F�����A���肪�Ƃ��ȁA���΂����B");

                        UpdateMainMessage("���i�F�ꉞ�������߂Ă����Ă��邩��A�C�ɂȂ������͌����Ă��傤�����ˁB");

                        UpdateMainMessage("�A�C���F�����A�������������邺�A�T���L���[�ȁB");

                        UpdateMainMessage("���i�F�˂��A�A�C���B");

                        UpdateMainMessage("�A�C���F��H�����H");

                        UpdateMainMessage("���i�F���`��A���ł��Ȃ���");

                        UpdateMainMessage("�A�C���F�����A�܂����O���ꂩ��B���������̂��A�����Ă݂Ă����H");

                        UpdateMainMessage("���i�F�����H���Ⴀ�E�E�E");

                        UpdateMainMessage("���i�F�A�C���A���F���[����̎��ǂ��v���H");

                        UpdateMainMessage("�A�C���F�E�E�E���H");

                        UpdateMainMessage("���i�F���F���[����̎����ǂ��v���̂����ĕ����Ă�̂�B");

                        UpdateMainMessage("�A�C���F���F���[�́E�E�E");

                        UpdateMainMessage("�A�C���F�D�����g�R�������āA����̂���l�ŁA�����X�L���R���r�l�[�V�����������B");

                        UpdateMainMessage("���i�F���`��A��������Ȃ��āB");

                        UpdateMainMessage("���i�F�ǂ��v���̂����ď��𕷂��Ă�񂾂��ǁB");

                        UpdateMainMessage("�A�C���F�ǁA�ǂ����Č����Ă��ȁE�E�E");

                        UpdateMainMessage("�A�C���F�����悭�͂߂˂����Ċ����ł͂���B");

                        UpdateMainMessage("���i�F�͂߂Ȃ����Ăǂ������Ӗ��H");

                        UpdateMainMessage("�A�C���FDUEL��ł������������񂾂�");

                        UpdateMainMessage("�A�C���F��{�I�ɁA���삻�̂��̎��͓̂ǂ߂Ȃ��B");

                        UpdateMainMessage("�A�C���F�v�f�A�v���A�������E�E�E�Ȃ�Č�������ǂ��񂾂낤�ȁB");

                        UpdateMainMessage("�A�C���F���������̂��ǂ߂Ȃ��B�悤�͍l���Ă鎖���̂��ǂ߂Ȃ����Ęb���B");

                        UpdateMainMessage("���i�F����́A�t���������Z����������Ȃ�������B");

                        UpdateMainMessage("�A�C���F�܂��A�����Ƃ��l�����邯�ǂȁB");

                        UpdateMainMessage("���i�F�ł��E�E�E�m���ɃA�C���̌����Ƃ���E�E�E");

                        UpdateMainMessage("�A�C���F��H");

                        UpdateMainMessage("�n���i�F���̎q�́A�̂��炻������B");

                        UpdateMainMessage("���i�F�����Ȃ�ł����H");

                        UpdateMainMessage("�A�C���F�ȁA���̘b����H");

                        UpdateMainMessage("���i�F�A�C�����������F���[����̎v�l��ǂ߂Ȃ��̂́A���ԓI�Ȃ��̂������邯��");

                        UpdateMainMessage("���i�F���F���[����A�����̎�����ؒ���Ȃ��̂�B");

                        UpdateMainMessage("�A�C���F�����������E�E�E");

                        UpdateMainMessage("�n���i�F�̂��玩���Ɋւ����b�͂��Ȃ��q����������ˁB");

                        UpdateMainMessage("�n���i�F���ł����̕ӂ�͕ς���ĂȂ���B");

                        UpdateMainMessage("���i�F������A�C���⎄�ɂƂ��āA���F���[���񂪂ǂ������l�Ȃ̂���ەt���Ȃ��킯��B");

                        UpdateMainMessage("�A�C���F���`��A�����Ă݂�Ⴛ���Ȃ̂����E�E�E");

                        UpdateMainMessage("�A�C���F���A�������B���A���F���[��DUEL�Ό������񂾂���");

                        UpdateMainMessage("�A�C���F���F���[��DUEL����A�m��Ȃ����H���΂����B");

                        UpdateMainMessage("�n���i�F�m�肽�������H");

                        UpdateMainMessage("�A�C���F���A�����E�E�E");

                        UpdateMainMessage("�n���i�F����͂�");

                        UpdateMainMessage("�A�C���F�E�E�E�i�S�N���j");

                        UpdateMainMessage("�n���i�F�O���S�Q�R�s����B");

                        UpdateMainMessage("�y�y�y�@�A�C���́A��ɂ��o�����@�z�z�z");

                        UpdateMainMessage("�A�C���F�΁E�E�E�o�J�ȁI�I�I");

                        UpdateMainMessage("�A�C���F���̓��e�őS�s���ăE�\����I�H");

                        UpdateMainMessage("�n���i�F����͐�΂ɍ��̏o���Ȃ�����ˁA�ԈႢ�͂Ȃ��Ǝv�����B");

                        UpdateMainMessage("�A�C���F�E�E�E�ǂ����������E�E�E�M�����˂��E�E�E");

                        UpdateMainMessage("���i�F�A�C���ɂƂ��āA���F���[�����DUEL��p�͂ǂ�������ꂽ�́H");

                        UpdateMainMessage("�A�C���F���Ă������E�E�E����Ŏ��ۏ��������Ă����񂾂낤�ȁA���Ĉ�ۂ��������B");

                        UpdateMainMessage("�n���i�F���̎q�́A�K��������B");

                        UpdateMainMessage("�n���i�F�����ď��������߂��肵�Ȃ��q��������B");

                        UpdateMainMessage("�A�C���F�����������񂩁E�E�E�ł��E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E�@���`��@�E�E�E");

                        UpdateMainMessage("���i�F�ǂ��������́H");

                        UpdateMainMessage("�A�C���F����E�E�E");

                        UpdateMainMessage("�A�C���F�ǂ����낤�ȁA�킩��˂��B");

                        UpdateMainMessage("�A�C���F�܂��E�E�E�������I");

                        UpdateMainMessage("�A�C���F���΂����A�A�J�V�W�A�̃X�p�Q�b�e�B������I");

                        UpdateMainMessage("�n���i�F�͂���A�����҂��ĂȁB");

                        UpdateMainMessage("�@  �������@�n���i�͐~�[�ւƖ߂��Ă������@�������@");

                        UpdateMainMessage("�A�C���i�����j�F���i�A����܃��F���[�Ɏ�͓˂����ނȁB");

                        UpdateMainMessage("���i�F���I�H");

                        UpdateMainMessage("�A�C���i�����j�F���̘b�͂�����ƒ����Ȃ肻�������A�����Č�����");

                        UpdateMainMessage("�A�C���i�����j�F���F���[�͒ꂪ�[���B");

                        UpdateMainMessage("�A�C���i�����j�F�[����`�����ޗl�Ȋ��o���B");

                        UpdateMainMessage("�A�C���i�����j�F�������߂Ă����A�ǂ��ȁH");

                        UpdateMainMessage("���i�F�����A�ʂɂ����������P����Ȃ��񂾂��ǁB");

                        UpdateMainMessage("�A�C���i�����j�F�C�C����A���ʂ̉�b�U�肾���ɂ��Ă�����A�ǂ��ȁH");

                        UpdateMainMessage("���i�F��[�������A�����������B");

                        UpdateMainMessage("�n���i�F�A�J�V�W�A�X�p�Q�b�e�B�A���҂��ǂ����܁B");

                        UpdateMainMessage("�A�C���F��������A�҂��Ă܂����I");

                        UpdateMainMessage("���i�F�������A�Q���L����˃z���b�g�E�E�E�n�A�A�@�@�@");

                        UpdateMainMessage("�A�C���F���i�A�S�K�ɍs�����炳�A�����烔�F���[�Ɋ�������Ă݂邺�B");

                        UpdateMainMessage("���i�F�����E�E�E");

                        UpdateMainMessage("���i�F�������������������I�I�H�I�H�I�H");

                        UpdateMainMessage("�A�C���F������A���O�����f�P�F�����́B");

                        UpdateMainMessage("���i�i�����j�F��������������ƁA�������ƑS�R�Ⴄ����Ȃ��́B");

                        UpdateMainMessage("�A�C���F�ǂ��񂾂��āA���������ꍇ�͂��ꂪ�P�Ԃ��B");

                        UpdateMainMessage("���i�F�����P�ԂȂ̂�A�܂����������E�E�E");

                        UpdateMainMessage("�A�C���F��������A�҂��Ă�惔�F���[�A�b�n�b�n�b�n�I");

                        UpdateMainMessage("�A�C���F����A�����������܁I");

                        UpdateMainMessage("�n���i�F�͂���A�Q��O�́A�����Ƃ������x�߂�񂾂�B");

                        UpdateMainMessage("�A�C���F�����A���������B���肪�Ƃ��Ȃ��΂����B");

                        UpdateMainMessage("�A�C���F�悵�A���Ⴀ�Q���ɖ߂��ĉו������ł��������ȁB");

                        UpdateMainMessage("���i�F���Ⴀ�A�܂������ˁB");

                        UpdateMainMessage("�A�C���F�����B");

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "�A�C�����\�񂵂Ă��������ɂ�";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("�A�C���F�ӂ��E�E�E�o�b�N�p�b�N�������ƁE�E�E");

                        UpdateMainMessage("�A�C���F���悢�掟����S�K���E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E");

                        UpdateMainMessage("�A�C���F���i�������o���������A�����Č��_���ւ̓��B�B");

                        UpdateMainMessage("�A�C���F���̂������ŁA���������ʉ߂����B");

                        UpdateMainMessage("�A�C���F���v�ȃn�Y�E�E�E�s����͂����B");

                        this.backgroundData = null;
                        this.BackColor = Color.Black;

                        UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                        this.backgroundData = null;
                        this.BackColor = Color.Black;

                        if (GroundOne.WE2.TruthRecollection3_4)
                        {
                            UpdateMainMessage("�A�C���F�L�������ǂ�ƁA���X�v���o���邱�Ƃ�����B");

                            UpdateMainMessage("�A�C���F�����A�������������Ă��鎖�ۂƐH���Ⴂ��������݂��Ă���B");

                            UpdateMainMessage("�A�C���F�������čl�������B");

                            UpdateMainMessage("�A�C���F�����A����ȋC�����Ƃ͗�����");

                            UpdateMainMessage("�A�C���F�����������ʂ͌������������Ƃ����C���������݂��Ă���B");

                            UpdateMainMessage("�A�C���F�ǂ����邩�E�E�E");

                            UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                            UpdateMainMessage("�A�C���F����A�l����ׂ���");

                            UpdateMainMessage("�A�C���F���͍��A�������Ȃ�����Ȃ�Ȃ�");

                            UpdateMainMessage("�A�C���F�E�E�E");

                            UpdateMainMessage("�A�C���F�Ō�̊Ŕɂ́y���z�y���z��������Ă����B");

                            UpdateMainMessage("�A�C���F�����A���̋L�����Ă������A�������̊Ŕ́E�E�E");

                            UpdateMainMessage("�A�C���F�y���z");

                            UpdateMainMessage("�A�C���F���ꂾ���̂͂�");

                            UpdateMainMessage("�A�C���F���́A����́y���z�y���z�Ȃ̂��B");

                            UpdateMainMessage("�A�C���F����͐l�Ԃ̐����Ɋ֘A���Ă���B");

                            UpdateMainMessage("�A�C���F�L�����y���z�����ŁA���݂��Ŕ́y���z�y���z�ł���Ƃ���΁A");

                            UpdateMainMessage("�A�C���F����������˂��B�ǂ����ɍl���Ă����͂����B");

                            UpdateMainMessage("�A�C���F�܂��A����ȍl�����͂܂��t���ɂǂ₳���̂��I�`�����ǂȁA�����ł͗ǂ��Ƃ��悤�B");

                            UpdateMainMessage("�A�C���F���ɁA���F���[�E�A�[�e�B�Ƃ������݁B");

                            UpdateMainMessage("�A�C���F���̋L���ł́A���F���[�E�A�[�e�B�͉ߋ��o�������������B");

                            UpdateMainMessage("�A�C���F�����č����݂̉��Ƃ��ẮA���F���[�E�A�[�e�B������̂͏��߂Ă��B");

                            UpdateMainMessage("�A�C���F�펯�I�ɍl���Ă��̊��o�͖��炩�ɂ��������B");

                            UpdateMainMessage("�A�C���F�����L����ł͉���Ă���̂�����");

                            UpdateMainMessage("�A�C���F�����݁A���͏��߂ă��F���[�̊�������Ƃ����̂�");

                            UpdateMainMessage("�A�C���F�L�������ł��邩�A��������");

                            UpdateMainMessage("�A�C���F������Ă��邩");

                            UpdateMainMessage("�A�C���F�E�E�E");

                            UpdateMainMessage("�A�C���F�_�����E�E�E������ӂ͒ǂ������Ă����ʂ��B");

                            UpdateMainMessage("�A�C���F�E�E�E");

                            UpdateMainMessage("�A�C���F���F���[�E�A�[�e�B�Ɋւ���ߋ��͒f�ГI�ȏ��̒~�ς����c����Ă��Ȃ��B");

                            UpdateMainMessage("�A�C���F�Z�̒B�l�B");

                            UpdateMainMessage("�A�C���F�`����FiveSeeker�̈�l");

                            UpdateMainMessage("�A�C���F�t�@�[�W���{�a�Ɏd������");

                            UpdateMainMessage("�A�C���F�����č��́A���Ƌ��Ƀp�[�e�B��g��ł���Ă���");

                            UpdateMainMessage("�A�C���F�����A���F���[�E�A�[�e�B�̎������͉����m��Ȃ��܂܂�");

                            UpdateMainMessage("�A�C���F����Ȓ��ł��������ȃq���g�͂�����");

                            UpdateMainMessage("�A�C���F�L���ł́A���i�������\���B�����鏗�̃J�����ă��c����");

                            UpdateMainMessage("�A�C���F���F���[�E�A�[�e�B�ɂ͗��l�����݂��Ă�������������");

                            UpdateMainMessage("�A�C���F�����Ă��̐l�͕s���̎��̂ŖS���Ȃ��Ă��܂����B");

                            UpdateMainMessage("�A�C���F���F���[�E�A�[�e�B�ɂƂ��Ă͗��l�����������E�B");

                            UpdateMainMessage("�A�C���F���z��]�񂾂Ƃ��Ă����������͂Ȃ��B�ǎ��͈͓̔����B");

                            UpdateMainMessage("�A�C���F���̃��i�̐��@�͂����炭�������B�Ȃ��Ȃ�");

                            UpdateMainMessage("�A�C���F�L����H��A�t�@���l���狳���Ă���������e�ƏƂ炵���킹���");

                            UpdateMainMessage("�A�C���F�s�^���Əƍ����邩�炾�B");

                            UpdateMainMessage("�A�C���F�������G���~��t�@�����܁A�J�[���A�{�P�t���B");

                            UpdateMainMessage("�A�C���F�����ă��F���[�E�A�[�e�B");

                            UpdateMainMessage("�A�C���F6�l�ځA�G���}�E�Z�t�B�[�l");

                            UpdateMainMessage("�A�C���F�G���}����̓��F���[�E�A�[�e�B�̓�����m��s�����Ă����B");

                            UpdateMainMessage("�A�C���F���ꂪ�ǂ��������Ȃ̂��A������Ȃ�ł����ɂ����ĕ�����B");

                            UpdateMainMessage("�A�C���F�����A�����݃G���}����͉��̑O�ɂ͑��݂��Ȃ��B");

                            UpdateMainMessage("�A�C���F���݂��Ă���̂́A���F���[�E�A�[�e�B���B");

                            UpdateMainMessage("�A�C���F���������A���̂��܉��̖ڂ̑O�ɂ���̂��B");

                            UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                            UpdateMainMessage("�A�C���F�E�E�E");

                            UpdateMainMessage("�A�C���F�_�����E�E�E���������Ǖ�����˂��E�E�E");

                            UpdateMainMessage("�A�C���F�E�E�E");

                            UpdateMainMessage("�A�C���F�E�E�E���������E�E�E");

                            UpdateMainMessage("�A�C���F�E�E�E");

                            UpdateMainMessage("�A�C���F�_���W����");

                            UpdateMainMessage("�A�C���F�Ō�Ƀt�@���l�Ɍ���ꂽ��");

                            UpdateMainMessage("�A�C���F�I���ւƑ����^�Ԃ�");

                            UpdateMainMessage("�A�C���F�n�܂�ւƑ���i�߂�");

                            UpdateMainMessage("�A�C���F���͂Ȃ�ƂȂ������A���̌��t�ɍ��A���|���o���Ă���");

                            UpdateMainMessage("�A�C���F���̂Ȃ�A������");

                            UpdateMainMessage("�A�C���F�����S�K�Ɍ����đ����^��ł��܂��Ă���");

                            UpdateMainMessage("�A�C���F�A��ׂ��Ȃ̂��H");

                            UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                            UpdateMainMessage("�A�C���F�E�E�E");

                            UpdateMainMessage("�A�C���F����A�_�����B");

                            UpdateMainMessage("�A�C���F���̓_���W�����֍s���B�����S�ɐ�����");

                            UpdateMainMessage("�A�C���F���x�����x�������S�ɐ����Ă���C�����邪�E�E�E");

                            UpdateMainMessage("�A�C���F���̗��R�̓V���v����");

                            UpdateMainMessage("�A�C���F�������A�퓬�X�L����B�A�x�������A�l�ɂ���ė��R�͗l�X���Ǝv����");

                            UpdateMainMessage("�A�C���F�����_���W�����֍s�����Ƃ������R�͂������");

                            UpdateMainMessage("�A�C���F���i����邽��");

                            UpdateMainMessage("�A�C���F���̏ؖ��̈�Ƃ���");

                            UpdateMainMessage("�A�C���F�_���W�����͉���l�Ŋ��S�ɐ��e������肾");

                            UpdateMainMessage("�A�C���F���́A���i�⃔�F���[�Ƌ��ɍs�����Ă��邪");

                            UpdateMainMessage("�A�C���F����͂���Ő���s�����A�����ɒf������͂��Ȃ�");

                            UpdateMainMessage("�A�C���F�ǂ�����A�_���W�������e�͕K������Ă݂���");

                            UpdateMainMessage("�A�C���F��U�s���̃_���W����");

                            UpdateMainMessage("�A�C���F�_�X���Z�܂��_���W����");

                            UpdateMainMessage("�A�C���F�_�̈�Y��������_���W����");

                            UpdateMainMessage("�A�C���F�l�̐S����炤�_���W����");

                            UpdateMainMessage("�A�C���F���낢��Ɖ\�͂���");

                            UpdateMainMessage("�A�C���F���킵���҂͐��m��Ȃ����A���e�����҂͋ɁX�킸��");

                            UpdateMainMessage("�A�C���F�\��������FiveSeeker��������˂��̂����Ęb�����邮�炢��");

                            UpdateMainMessage("�A�C���F�܂�A���̃_���W���������ɂ����e�ł����");

                            UpdateMainMessage("�A�C���F�������������ɕ������肷�鎖���Ȃ�");

                            UpdateMainMessage("�A�C���F�ǂ�Ȏ������N���悤��");

                            UpdateMainMessage("�A�C���F���i��K������悤�ɂȂ�");

                            UpdateMainMessage("�A�C���F���߂����Ȃ�");

                            UpdateMainMessage("�A�C���F�E�E�E");

                            UpdateMainMessage("�A�C���F�ނ��킯�ɂ͍s���Ȃ��B");

                            UpdateMainMessage("�A�C���F�K���E�E�E�����񂾁B");

                            UpdateMainMessage("�A�C���F�E�E�E");

                            GroundOne.WE2.TruthKey3 = true; // �����^�����E�ւ̃L�[���̂R�Ƃ���B
                        }

                        UpdateMainMessage("�@�@�@�y�L���Ǝ����̖����z");

                        UpdateMainMessage("�@�@�@�y���F���[�ƃG���}�z");

                        UpdateMainMessage("�@�@�@�y�ɂ������Ă��鋰�|���z");

                        if (we.dungeonEvent328)
                        {
                            UpdateMainMessage("�@�@�@�y���Ǝ����������Ŕz");
                        }
                        else
                        {
                            UpdateMainMessage("�@�@�@�y���������������Ŕz");
                        }

                        UpdateMainMessage("�@�@�@�y������Ȃ������炯�̂܂܁z");

                        UpdateMainMessage("�@�@�@�y���͂S�K�ւƑ���i�߂�z");

                        UpdateMainMessage("�@�@�@�y�����͑����z");

                        UpdateMainMessage("�@�@�@�y���̐�ɂ���z");
                    }

                    we.TruthCommunicationCompArea3 = true;

                    CallRestInn(true);

                    using (ESCMenu esc = new ESCMenu())
                    {
                        esc.MC = this.MC;
                        esc.SC = this.SC;
                        esc.TC = this.TC;
                        esc.WE = this.we;
                        esc.KnownTileInfo = null;
                        esc.KnownTileInfo2 = null;
                        esc.KnownTileInfo3 = null;
                        esc.KnownTileInfo4 = null;
                        esc.KnownTileInfo5 = null;
                        esc.Truth_KnownTileInfo = this.Truth_KnownTileInfo;
                        esc.Truth_KnownTileInfo2 = this.Truth_KnownTileInfo2;
                        esc.Truth_KnownTileInfo3 = this.Truth_KnownTileInfo3;
                        esc.Truth_KnownTileInfo4 = this.Truth_KnownTileInfo4;
                        esc.Truth_KnownTileInfo5 = this.Truth_KnownTileInfo5;
                        esc.StartPosition = FormStartPosition.CenterParent;
                        esc.TruthStory = true;
                        esc.OnlySave = true;
                        esc.ShowDialog();
                    }

                    //this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                    this.BackColor = Color.WhiteSmoke;
                    this.Update();
                    UpdateMainMessage("", true);
                    FourthCommunicationStart();
                }
                #endregion
                #region "�R�K�A���G���A�Q�|�T���N���A������"
                 else if (we.dungeonEvent312 && !we.dungeonEvent312_2)
                 {
                     we.dungeonEvent312_2 = true;

                     UpdateMainMessage("�A�C���F������A�߂��Ă������B");

                     UpdateMainMessage("���F���[�F�A�C���N�A�{�N�͏����G���~���A�t�@�����܂֌���̕񍐂��s���Ă��܂��B");

                     UpdateMainMessage("�A�C���F���A�����A�������B���Č���񍐁H");

                     UpdateMainMessage("���F���[�F�͂��B�ꉞ�t�@�[�W���{�a�ŋ΂߂Ă���ȏ�A�񍐂͕K�{�����ł�����B");

                     UpdateMainMessage("�A�C���F�����Ȃ̂��B���Ⴀ�A���������܂������ȁB");

                     UpdateMainMessage("���F���[�F�����A�ł́B");

                     UpdateMainMessage("�@�@�@�������@���F���[�͂��̏ꂩ�狎���Ă����� ������");

                     UpdateMainMessage("�A�C���F���ĂƁB������ȑ�������������ė����������A�����͂��悢��{�X���ď����ȁI");

                     UpdateMainMessage("���i�F�悤�₭���Ċ�����ˁB");

                     UpdateMainMessage("�A�C���F�������\�O���O���Ɖ񂳂ꂽ������������ȁA�o������˂����B");

                     UpdateMainMessage("���i�F������A������ƍ���̂͂ǂ����ǂ��q�����Ă�̂��A�S�R�c���ł��Ȃ���B");

                     UpdateMainMessage("�A�C���F�͂����E�E�E�����܂��n���ɒT���̂��E�E�E�L�b�`���o���Ă�����ǂ��������A�ƂقفB");

                     UpdateMainMessage("���i�F�܂��A�����܂��s���Ă݂܂���A���ꂵ���Ȃ��񂾂����");

                     UpdateMainMessage("�A�C���F�����A���Ⴀ�܂������ȁI");

                     UpdateMainMessage("���i�F����A�܂��ˁ�");
                 }
                 #endregion
                #region "�R�K�A���G���A�Q�|�S���N���A������"
                 else if (we.dungeonEvent317 && !we.dungeonEvent317_2)
                 {
                     we.dungeonEvent317_2 = true;

                     UpdateMainMessage("���F���[�F�A�C���N�A���݂܂��񂪃t�@�[�W���{�a�֍s���Ă܂��B");

                     UpdateMainMessage("�A�C���F�����A���񂾁B");

                     UpdateMainMessage("�@�@�@�������@���F���[�͂��̏ꂩ�狎���Ă����� ������");

                     UpdateMainMessage("�A�C���F���i�A�����B�N����邩�H");

                     UpdateMainMessage("���i�F�����H�����ǂ���H");

                     UpdateMainMessage("�A�C���F�_���W�����Q�[�g�̓�������B�ق�A���}�Z�b�g���B");

                     UpdateMainMessage("���i�F�����A���肪�Ɓ�");

                     UpdateMainMessage("���i�F�ŁA���ł����ɖ߂��Ă��Ă�̂�A�܂������C�₵������Ă����P�H");

                     UpdateMainMessage("�A�C���F����A�����C��Ƃ������́A�X���X���ƐQ�Ă銴�����������B");

                     UpdateMainMessage("���i�F���`��A�x�X�S�����ˁB");

                     UpdateMainMessage("�A�C���F�������āA�����C�����悳�����ːQ�Ă����B");

                     UpdateMainMessage("���i�F�E�E�E");

                     UpdateMainMessage("���i�F������ƁE�E�E���Ȃ��ł�ˁA�l�̐Q��B");

                     UpdateMainMessage("�A�C���F�b�O�A�E�E�E�����������������E�E�E���񂾂����C�������I�[�P�[���B");

                     UpdateMainMessage("�A�C���F�h���܂Ŗ߂ꂻ�����H");

                     UpdateMainMessage("���i�F�����A���v��B");

                     UpdateMainMessage("�A�C���F�������x�ނ񂾂��B");

                     UpdateMainMessage("���i�F�����A�܂�������낵���ˁ�");

                     UpdateMainMessage("�A�C���F�����A�܂��ȁB");
                 }
                 #endregion
                #region "�R�K�A���G���A�Q�|�R���N���A������"
                 else if (we.dungeonEvent316 && !we.dungeonEvent316_2)
                 {
                     we.dungeonEvent316_2 = true;

                     UpdateMainMessage("�A�C���F���i�E�E�E�ق牞�}�Z�b�g���B");

                     UpdateMainMessage("���i�F���ӂ��E�E�E���肪�ƁB");

                     UpdateMainMessage("�A�C���F���v���H");

                     UpdateMainMessage("���i�F�����A�������Ƃ������݂����B");

                     UpdateMainMessage("���F���[�F���}�Z�b�g�̓t�@�[�W���{�a�q�ɂɂ͎R�̂悤�ɂ���܂�����A�S�z�����g���Ă��������B");

                     UpdateMainMessage("���i�F����A���߂�Ȃ����A���肪�Ƃ��ˁB");

                     UpdateMainMessage("���F���[�F����ł́A���݂܂��񂪃t�@�[�W���{�a�֎��̕������ɍs���܂��B");

                     UpdateMainMessage("�@�@�@�������@���F���[�͂��̏ꂩ�狎���Ă����� ������");

                     UpdateMainMessage("�A�C���F���i�A�h���܂ő����Ă��������H");

                     UpdateMainMessage("���i�F�C�C���ā�@�z���b�g�ɑ��v�������");

                     UpdateMainMessage("�A�C���F�������A��łȓz���Ȗ{���ɁA�b�n�n�n�E�E�E");

                     UpdateMainMessage("�A�C���F�������A���F���[�������Ă����h�����͂����ƌ����񂾂��A�����ȁH");

                     UpdateMainMessage("���i�F�����A����������B�����ƌ����悤�ɂ��邩��S�z���Ȃ��Ł�");

                     UpdateMainMessage("�A�C���F���𗹉��A���Ⴀ�A�܂������ȁB");

                     UpdateMainMessage("���i�F����A�A�C�����������x��łˁB");

                     UpdateMainMessage("�A�C���F�����B");
                 }
                 #endregion
                #region "�R�K�A���G���A�Q�|�Q���N���A������"
                 else if (we.dungeonEvent315 && !we.dungeonEvent315_2)
                 {
                     we.dungeonEvent315_2 = true;

                     UpdateMainMessage("�A�C���F���i�A�h���܂ŕ����Ă����邩�H");

                     UpdateMainMessage("���i�F�����A���̂��炢�͑��v��B");

                     UpdateMainMessage("���F���[�F���i����A���}�Z�b�g���ǂ����B");

                     UpdateMainMessage("���i�F���肪�Ƃ���");

                     UpdateMainMessage("���F���[�F���̗\���̕����K�v�ł��ˁA���}�Z�b�g������Ă��Ă����܂��傤�B");

                     UpdateMainMessage("�A�C���F�����A���܂˂��B");

                     UpdateMainMessage("�@�@�@�������@���F���[�͂��̏ꂩ�狎���Ă����� ������");

                     UpdateMainMessage("���i�F�E�E�E���̉��}�Z�b�g������ˁB���������_�I�Ȕ�ꂪ��u�Ŕ��ł�����������B");

                     UpdateMainMessage("�A�C���F�����Ȃ̂��H");

                     UpdateMainMessage("���i�F�����A�������t�@�[�W���{�a��p�B���ď��Ȃ̂�����B");

                     UpdateMainMessage("�A�C���F�ł������͈�U�x�������B����͈ꎞ�̂��񂾂낤���ȁB");

                     UpdateMainMessage("���i�F�����A����������B���Ⴀ�A�܂��ˁB");

                     UpdateMainMessage("�A�C���F�����B");
                 }
                 #endregion
                #region "�R�K�A���G���A�Q�|�P���N���A������"
                 else if (we.dungeonEvent314 && !we.dungeonEvent314_2)
                 {
                     we.dungeonEvent314_2 = true;

                     UpdateMainMessage("�A�C���F���i�A���v���H");

                     UpdateMainMessage("���i�F���`��A���v���Ƃ͎v���B");

                     UpdateMainMessage("���F���[�F�A�C���N�A�{�N�̓t�@�[�W���{�a���牞�}�Z�b�g�������Ă��܂��B");

                     UpdateMainMessage("�A�C���F�����A���܂˂��ȗ��ނ��B");

                     UpdateMainMessage("�@�@�@�������@���F���[�͂��̏ꂩ�狎���Ă����� ������");

                     UpdateMainMessage("�A�C���F���i�A�����͏h���֖߂��ċx�ނ񂾁B");

                     UpdateMainMessage("���i�F�����S�����ˁA�ςȃg�R�ɂȂ�������āE�E�E");

                     UpdateMainMessage("�A�C���F�������āA�i�߂��񂾂��S�ROK����B���͂Ƃɂ����x�ނ񂾁B�����ȁH");

                     UpdateMainMessage("���i�F�����A�킩������B���Ⴀ�A�܂������ˁ�");

                     UpdateMainMessage("�A�C���F�����A�܂������ȁB");
                 }
                 #endregion
                #region "�R�K�A�G���A�P�̋����N���A��"
                 else if (we.TruthCompleteArea1 && we.TruthCompleteArea2 && !we.TruthCompleteArea3 && we.dungeonEvent305 && !we.dungeonEvent306)
                 {
                     we.dungeonEvent306 = true;

                     UpdateMainMessage("�A�C���F�ӂ��A�߂����ȁB");

                     UpdateMainMessage("���i�F�ł��A�{���ɒ�������ˁA�o�J�A�C���������I��������Ȃ�āB");

                     UpdateMainMessage("�A�C���F�E�E�E����A�����ȏ��E�E�E");

                     UpdateMainMessage("���i�F���H");

                     UpdateMainMessage("�A�C���F����A���ł��˂��B���������B");

                     UpdateMainMessage("���i�F���A������ƁI�H�@�����̌��������Ď~�߂�̃i�V�ɂ��Ă�ˁI�H");

                     UpdateMainMessage("�A�C���F���₢��E�E�E�܂��E�E�E");

                     UpdateMainMessage("���F���[�F�A�C���N�A���݂܂��񂪃{�N�͏����t�@�[�W���{�a�ɗp��������̂ŁA�{�a�֖߂��Ă��܂��ˁB");

                     UpdateMainMessage("�A�C���F��H���A�����B�@���𗹉��I");

                     UpdateMainMessage("�@�@�@�������@���F���[�͂��̏ꂩ�狎���Ă����� ������");

                     UpdateMainMessage("�A�C���F�����Ƃ��ȁE�E�E���Ⴀ���i�B");

                     UpdateMainMessage("�A�C���F���i�͋�������Ă݂Ă��́E�E�E");

                     UpdateMainMessage("�A�C���F�ǂ����B�@�̒��ɕω��͂Ȃ����H");

                     UpdateMainMessage("���i�F�E�E�E���H");

                     UpdateMainMessage("���i�F�������������������I�I�H�H");

                     UpdateMainMessage("�A�C���F������A�����Ȃ�吺�o���Ȃ����́B");

                     UpdateMainMessage("���i�F���A������Ɖ��A�Ђ���Ƃ��āE�E�E");

                     UpdateMainMessage("���i�F���̑̒��ł��C���������Č����Ă郏�P�I�H");

                     UpdateMainMessage("�A�C���F���₢��A���ƂȂ��C�ɂȂ����������I�@����Ȍ��w�ȐS�z����˂����āI");

                     UpdateMainMessage("���i�F�E�E�E�b�v");

                     UpdateMainMessage("���i�F�b�v�n�n�n�n�A�������Ă�̂��̃o�J�A�C���A�A���^�ق�ƃI�J�V�C�񂶂�Ȃ��́�");

                     UpdateMainMessage("���i�F�̒��H�H�@���[�����傤�ԂɌ��܂��Ă邶��Ȃ��́�@�ςȏ��ŃJ���`�K�C�C������������z���g��");

                     UpdateMainMessage("�A�C���F�ȁA�Ȃ�ǂ��񂾂��ȁB�b�n�n�n�E�E�E");

                     UpdateMainMessage("�A�C���F�܂��A���܂ɂ͂���ȕ��ɐ؂�グ��̂������͂Ȃ�����B");

                     UpdateMainMessage("���i�F�b�t�t�A�����ˁB���܂ɂ͋����Ă�����Ƃ�����");

                     UpdateMainMessage("�A�C���F���Ⴀ�A�����͂����܂ł��B");

                     UpdateMainMessage("�A�C���F�����A�ǂ����B�@�тł��ꏏ�ɐH�ׂĂ������H");

                     UpdateMainMessage("���i�F������A����͌��\��A��l�ŐH�ׂ邩���");

                     UpdateMainMessage("�A�C���F�����E�E�E�܂��A���ꂾ�����C�Ȃ�ǂ��񂾁B");

                     UpdateMainMessage("�A�C���F�i�E�E�E�C�̂������ȁA�����Ɓj");

                     UpdateMainMessage("�A�C���F���Ⴀ�܂���������낵�����ނ��I");

                     UpdateMainMessage("���i�F�G�G�A�����ꂳ�܁�");
                 }
                 #endregion
                #region "��ҏ���"
                 else if (this.firstDay >= 1 && !we.Truth_CommunicationFirstHomeTown)
                 {
                     GroundOne.StopDungeonMusic();

                     dayLabel.Visible = false;
                     buttonHanna.Visible = false;
                     buttonDungeon.Visible = false;
                     buttonRana.Visible = false;
                     buttonGanz.Visible = false;
                     this.backgroundData = null;
                     this.BackColor = Color.Black;

                     if (GroundOne.WE2.TruthBadEnd1) UpdateMainMessage("�A�C���F�E�E�E�����́E�E�E���E�E�E���E�E�E");
                     else UpdateMainMessage("�A�C���F���E�E�E���E�E�E");

                     if (GroundOne.WE2.TruthBadEnd1) UpdateMainMessage("�A�C���F���A�������炢���H");
                     else UpdateMainMessage("�A�C���F���E�E�E�������H");

                     UpdateMainMessage("        �w�A�C���͏h���̐Q������N���オ�����B�x");

                     if (GroundOne.WE2.TruthBadEnd1) UpdateMainMessage("�A�C���F���V�����炢���B�܂��A���傤�Ǘǂ����炢�̎��Ԃ��ȁB");
                     else UpdateMainMessage("�A�C���F���̂U�����E�E�E�N����ɂ͏����������炢���ȁB");

                     UpdateMainMessage("�A�C���F�E�E�E��H�������ɗ����Ă�ȁB");

                     using (MessageDisplay md = new MessageDisplay())
                     {
                         md.Message = "�y���i�̃C�������O�z����ɓ���܂����B";
                         GroundOne.playbackMessage.Insert(0, md.Message);
                         GroundOne.playbackInfoStyle.Insert(0, TruthPlaybackMessage.infoStyle.notify);
                         md.StartPosition = FormStartPosition.CenterParent;
                         md.ShowDialog();
                     }

                     GetItemFullCheck(mc, Database.RARE_EARRING_OF_LANA);

                     if (GroundOne.WE2.TruthBadEnd1) UpdateMainMessage("�A�C���F���i�̃C�������O����˂����E�E�E���ł���ȕ����E�E�E");
                     else UpdateMainMessage("�A�C���F�����A���i�̂�B���ł܂�����ȏ��ɗ��Ƃ��Ă�񂾁B");

                     if (GroundOne.WE2.TruthBadEnd1) UpdateMainMessage("�A�C���F�E�E�E�@���ł���񂾂����@�E�E�E�@���i�����Ƃ����̂��H");
                     else UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                     if (GroundOne.WE2.TruthBadEnd1) UpdateMainMessage("�A�C���F������A����ȃ��P������ȁE�E�E���Ⴀ���ł��E�E�E");
                     else UpdateMainMessage("�A�C���F���傤���˂��B��œn���ɍs���Ă�邩�B");

                     if (GroundOne.WE2.TruthBadEnd1) UpdateMainMessage("�A�C���F�܂��������B�Ƃ肠�����A�ڊo�߂��킯�����A���ɂł��o�Ă݂邩�I");
                     else UpdateMainMessage("�A�C���F��������A���������ڊo�߂��킯�����A���ɂł��o�Ă݂邩�B");

                     ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                     this.BackColor = Color.WhiteSmoke;
                     buttonHanna.Visible = true;
                     buttonDungeon.Visible = true;
                     buttonRana.Visible = true;
                     buttonGanz.Visible = true;
                     dayLabel.Visible = true;


                     UpdateMainMessage("�A�C���F���āA���������ȁB", true);

                     we.Truth_CommunicationFirstHomeTown = true;
                     we.AlreadyRest = true; // ���N�����Ƃ�����X�^�[�g�Ƃ���B

                     GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
                 }
                 #endregion
                #region "�P�K�ŔŌ�̏�����肵���Ƃ�"
                 else if (this.firstDay >= 1 && !we.Truth_Communication_Dungeon11 && we.dungeonEvent27)
                 {
                     UpdateMainMessage("�A�C���F�����E�E�E���܂˂��ȁB");

                     UpdateMainMessage("���i�F�ʂɗǂ����B�ł��A�{���ɑ��v�H");

                     UpdateMainMessage("�A�C���F�����A���Ƃ��ȁB���v���A�����Ђ��Ă����B");

                     UpdateMainMessage("���i�F���������Ђ����݂����ˁB");

                     UpdateMainMessage("�A�C���F��̉��Ȃ񂾁A���̊Ŕ́B");

                     UpdateMainMessage("���i�F���W�n�_���w�������Ă����݂��������ǁH");

                     UpdateMainMessage("�A�C���F���n�܂�̒n�ɂā��@�E�E�E���B");

                     UpdateMainMessage("�A�C���F�������B�w�n�܂�̒n�A�����Ƃ��ׂ��炸�B�x���ĊŔ���������ȁB");

                     UpdateMainMessage("���i�F�����ƃR���̎��������Ă����̂ˁB");

                     UpdateMainMessage("�A�C���F���i�A�w�S�V�@�Q�X�x�ƌ�������ǂ̕ӂɂȂ�񂾁H");

                     UpdateMainMessage("���i�F�����炭�����ǂ��̐����͍��W�|�C���g�w�Ƃx�������Ă�̂�B");

                     UpdateMainMessage("���i�F�w�͍��E�A�x���㉺�Ƃ���ƁA�w�����ւS�V�A�x�����ւQ�V�B");

                     UpdateMainMessage("���i�F�܂�A�E���̂��̕ӂ���w�������Ă鎖�ɂȂ��B");

                     UpdateMainMessage("���i�F�����ƈ�t���Ă���������B�Y��邱�Ƃ͖����Ǝv����B");

                     UpdateMainMessage("�A�C���F�����ɂȂ�����A�s���Ă݂�Ƃ��邩�B");

                     UpdateMainMessage("���i�F����A�����ˁB�����͂����x�݂܂��傤�B");

                     UpdateMainMessage("�A�C���F�����B���ꂶ��A�n���i�f�ꂳ��̏h���֍s�������B");

                     UpdateMainMessage("���i�F������B");

                     we.Truth_Communication_Dungeon11 = true;
                 }
                 #endregion
                #region "�P�K���e"
                 else if (this.we.TruthCompleteArea1 && !this.we.TruthCommunicationCompArea1)
                 {
                     if (we.AvailableSecondCharacter)
                     {
                         UpdateMainMessage("�A�C���F������I��������I���i�I");

                         UpdateMainMessage("���i�F��o���Ȃ񂶂�Ȃ���");

                         UpdateMainMessage("�A�C���F���i�I���O�͂���ύō��̃p�[�g�i�[�����I�b�n�b�n�b�n�I");

                         UpdateMainMessage("���i�F�܂��A����ȕ�����Ă�����āB�z���z���A���̏Z�l�B�ɕ񍐂��ɂ����܂���B");

                         UpdateMainMessage("�A�C���F���A�����A�������ȁI�b�n�b�n�b�n�I");

                         using (MessageDisplay md = new MessageDisplay())
                         {
                             md.StartPosition = FormStartPosition.CenterParent;
                             md.Message = "�A�C���͈�ʂ�A���̏Z�l�B�ɐ��������A���Ԃ����X�Ɖ߂��Ă������B";
                             md.ShowDialog();

                             md.Message = "���̓��̖�A�n���i�̏h�����ɂ�";
                             md.ShowDialog();
                         }

                         GroundOne.StopDungeonMusic();
                         GroundOne.PlayDungeonMusic(Database.BGM15, Database.BGM15LoopBegin);

                         UpdateMainMessage("�A�C���F�����ŁA�{�X����̕K�E�Z���o���Ă���u�Ԃɂ��ȁA�Y�o�Y�o���ƁI");

                         UpdateMainMessage("���i�F�������Ă�̂�B�N����������Ă������Ǝv���Ă�̂�H");

                         UpdateMainMessage("�A�C���F���₢��A�����������ȁB�T���L���[�T���L���[�I");

                         // ���i�̃C�������O��n���Ă��܂��Ă����ꍇ�A����
                         // �^�����̕����֓��B���Ă��Ȃ��ꍇ�ABADEND
                         if ((we.Truth_GiveLanaEarring) &&
                             (!GroundOne.WE2.TruthRecollection1))
                         {
                             UpdateMainMessage("�n���i�F���₨��A�A�C���B�o�J�ɑ����ł�悤���ˁB");

                             UpdateMainMessage("�A�C���F�����Ă����A���΂����B�����񂾂�ȁA�R�R�ŁE�E�E");

                             UpdateMainMessage("�A�C���F�T�^�̃q�����L���ȁI�I");

                             UpdateMainMessage("�A�C���F�b�n�b�n�b�n�b�n�b�n�I�I");

                             UpdateMainMessage("���i�F�܂��T�^�I�ȃo�J��ˁE�E�E�n�A�A�@�@�@�E�E�E");

                             UpdateMainMessage("���i�F�A�C���A���낻�뎄�͕����ɖ߂��Ĉ�U�x�������ˁB");

                             UpdateMainMessage("�A�C���F��H�����I���𗹉��I");

                             UpdateMainMessage("���i�F�n���i�f�ꂳ��B�ǂ������������l�ł�����");

                             UpdateMainMessage("�n���i�F������A�܂��������撣��񂾂ˁB");

                             UpdateMainMessage("���i�F�n�C�A����ł͎��炵�܂��B");

                             using (MessageDisplay md = new MessageDisplay())
                             {
                                 md.StartPosition = FormStartPosition.CenterParent;
                                 md.Message = "���i�͎������\�񂵂Ă��������֕����Ă������B";
                                 md.ShowDialog();
                             }

                             UpdateMainMessage("�A�C���F�͂��`�H�����H�����B�������B���΂����A���肪�ƁI");

                             UpdateMainMessage("�n���i�F�悭�H�ׂ��ˁB�����ɍ����x���Ȃ��悤�ɂ���񂾂�B");

                             UpdateMainMessage("�A�C���F�͂��I�I���肪�Ƃ������I�I");

                             UpdateMainMessage("�n���i�F�A�C���B���������΁A�����ɉ��������Ă��Ȃ����������H");

                             UpdateMainMessage("�A�C���F�E�E�E���͂��H");

                             UpdateMainMessage("�n���i�F�C�������O�A�����ɉ��������Ă��Ȃ����������H");

                             UpdateMainMessage("�A�C���F�E�E�E�����A�����B����Ȃ�E�E�E");

                             UpdateMainMessage("�A�C���F���i�ɕԂ��Ă����܂�����B");

                             UpdateMainMessage("�A�C���F�m���A�C�c�������t���Ă��C�������O�ł�����B");

                             UpdateMainMessage("�n���i�F���������B�܂��A�������Ȃ��悤�ɓ`���Ă����񂾂�B");

                             UpdateMainMessage("�A�C���F���`���ƁA������܂����I����A���������l�ł����I");

                             UpdateMainMessage("�n���i�F�͂���A���������邾�낤�B�������Ƌx�݂ȁB");

                             UpdateMainMessage("�A�C���F���肪�Ƃ��������܂��A���炵�܂��I");

                             GroundOne.StopDungeonMusic();

                             ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_NIGHT);

                             using (MessageDisplay md = new MessageDisplay())
                             {
                                 md.StartPosition = FormStartPosition.CenterParent;
                                 md.Message = "�A�C�����\�񂵂Ă��������ɂ�";
                                 md.ShowDialog();
                             }

                             UpdateMainMessage("�A�C���F�ӂ��E�E�E�o�b�N�p�b�N�������ƁE�E�E");

                             UpdateMainMessage("�A�C���F��������Q�K���E�E�E������I�C������邺�I�I");

                             UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                             UpdateMainMessage("�A�C���F�E�E�E");

                             UpdateMainMessage("�A�C���F�E�E�E�@�Ȃ񂾂낤�@�E�E�E");

                             UpdateMainMessage("�A�C���F���i�ƈꏏ�Ƀ_���W�����֐i��ŁE�E�E���ꂩ��E�E�E");

                             this.backgroundData = null;
                             this.BackColor = Color.Black;

                             UpdateMainMessage("�A�C���F�E�E�E�@�����Y��Ă�C������@�E�E�E");

                             UpdateMainMessage("�A�C���F���A������Ă��񂾂����E�E�E");

                             UpdateMainMessage("�A�C���F�܂�������A�ЂƂ܂��_���W�����̍ŉ��w�ցE�E�E");

                             UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                             UpdateMainMessage("�A�C���F�E�E�E");

                             UpdateMainMessage(" �`�@THE�@END�@�`�@�i���\�ցj");

                             GroundOne.WE2.TruthBadEnd1 = true;
                         }
                         // ����ȊO��GOOD
                         else
                         {
                             UpdateMainMessage("�n���i�F���₨��A�A�C���B�o�J�ɑ����ł�悤���ˁB");

                             UpdateMainMessage("�A�C���F�����Ă����A���΂����B�����񂾂�ȁA�R�R�ŁE�E�E");

                             UpdateMainMessage("�A�C���F�V�[�̃q�����L���ȁI�I");

                             UpdateMainMessage("�A�C���F�b�n�b�n�b�n�b�n�b�n�I�I");

                             UpdateMainMessage("���i�F���܂��ܓ˂��h�����������{�X�̋}�������������ł���H");

                             UpdateMainMessage("�A�C���F�_���Ă�����Ɍ��܂��Ă邾��H");

                             UpdateMainMessage("���i�F�z���b�g�Ă��Ɓ[�Ȃ񂾂���E�E�E");

                             UpdateMainMessage("�n���i�F�܂��܂��A�ǂ�����Ȃ������i�����B��肭�i�߂��悤�����ˁB");

                             UpdateMainMessage("���i�F�����A�����ł��ˁB���܂ɂ̓o�J���{���̃o�J�ɖ߂��Ċ������ł��傤����");

                             UpdateMainMessage("�A�C���F�����A�o�J�Ō��\�I");

                             UpdateMainMessage("�A�C���F�b�n�b�n�b�n�b�n�b�n�I�I");

                             UpdateMainMessage("���i�F�n�A�A�@�@�@�E�E�E�E���v�Ȃ̂�����A����Ȋ����ŁE�E�E");

                             if (!we.Truth_GiveLanaEarring)
                             {
                                 UpdateMainMessage("�A�C���F���ƁA�������I�Y��Ă����I");

                                 UpdateMainMessage("���i�F���ȁA����ˑR�ǂ������̂�H");

                                 UpdateMainMessage("�A�C���F���i�A�����B��ӂ点�Ă���B");

                                 UpdateMainMessage("���i�F������H");

                                 UpdateMainMessage("�A�C���F���́A�R���Ȃ񂾂��E�E�E");

                                 using (MessageDisplay md = new MessageDisplay())
                                 {
                                     md.StartPosition = FormStartPosition.CenterParent;
                                     md.Message = "�A�C���́w���i�̃C�������O�x���|�P�b�g������o�����B";
                                     md.ShowDialog();
                                 }
                                 mc.DeleteBackPack(new ItemBackPack("���i�̃C�������O"));

                                 UpdateMainMessage("���i�F�����I�\�����āI�I");

                                 UpdateMainMessage("�n���i�F���₨��E�E�E�Ђ���Ƃ��ă��i�����̃A�N�Z�T�������H");

                                 UpdateMainMessage("�A�C���F����A���₢�₢�₢��I");

                                 UpdateMainMessage("�A�C���F�����A���܂��܉��̕����ɉ��̂��]�����Ă��񂾂��āI");

                                 UpdateMainMessage("�A�C���F�������������������������I���ȁI�H");

                                 using (MessageDisplay md = new MessageDisplay())
                                 {
                                     md.StartPosition = FormStartPosition.CenterParent;
                                     md.Message = "���i�͋����ƌ˘f���̕\����B���Ȃ��ł���E�E�E";
                                     md.ShowDialog();

                                     md.Message = "�E�E�E�@���b��@�E�E�E";
                                     md.ShowDialog();
                                 }

                                 UpdateMainMessage("���i�F�E�E�E�@�E�E�E�@�E�E�E���ǁE�E�E");

                                 UpdateMainMessage("�A�C���F�{���V�A���e�B���b�g�u���[�Ƃ����قȁI�H���ȁI�H");

                                 UpdateMainMessage("���i�F�ǂ��E�E�E�ǂ����A���肪�ƁB");

                                 UpdateMainMessage("�A�C���F�E�E�E�b�n�H");

                                 UpdateMainMessage("�n���i�F�A�b�n�n�n�n�n�B�悩��������Ȃ����B");

                                 UpdateMainMessage("�n���i�F�z���I�����Ȃ�����A�A�C���������Ɏӂ�񂾂ˁB");

                                 UpdateMainMessage("�A�C���F�����A�����A���������ȁI����A�z���g���������I");

                                 UpdateMainMessage("���i�F�ʂɗǂ����B�C�ɂ��ĂȂ������");

                                 UpdateMainMessage("�A�C���F�����A�������B�Ȃ�ǂ��񂾂��E�E�E�Ƃɂ������������ȁB");

                                 UpdateMainMessage("���i�F�ǂ����Č����Ă邶��Ȃ���@�ς񂾎������B");

                                 UpdateMainMessage("���i�F�Ƃ���ŁA�R���ǂ��ɗ����Ă��̂�H");

                                 UpdateMainMessage("�A�C���F���������������Ǝv�����A���̕������B");

                                 UpdateMainMessage("�A�C���F" + we.GameDay.ToString() + "�����炢�O���������ȁB");

                                 UpdateMainMessage("�A�C���F���ӂƋN����Ƃ��A�x�b�h�̉��ɓ]�����Ă��񂾂�B");

                                 UpdateMainMessage("���i�F�ւ��A����ȏ��ɗ����Ă��񂾁B");

                                 UpdateMainMessage("�A�C���F����ȏ����āA���Ⴀ�ǂ��ɗ����Ă��Ǝv�����񂾂�H");

                                 UpdateMainMessage("���i�F���ׁׂׂA�ʂɒm��Ȃ����I�I����Ȃ́I�I");

                                 UpdateMainMessage("�A�C���F�����A�������������āB����ȃr�r��Ȃ��ėǂ����낤���B");

                                 UpdateMainMessage("���i�F���܂������E�E�E�����A�������B����������ėǂ��H");

                                 UpdateMainMessage("�A�C���F��H�����A���ł������Ă���B");

                                 UpdateMainMessage("���i�F���ōŏ��������Ƃ��A�n���Ă���Ȃ������́H");

                                 UpdateMainMessage("�A�C���F���Č�������ǂ��񂾂낤�ȁB");

                                 UpdateMainMessage("�A�C���F�悭�l���Ă݂��������񂾁B");

                                 UpdateMainMessage("�n���i�F�E�E�E");

                                 UpdateMainMessage("�A�C���F���i�̃C�������O�A�ŏ����������B");

                                 UpdateMainMessage("�A�C���F�ǂ��ƂȂ������A�悭�����ł��Ȃ������񂾂�B");

                                 UpdateMainMessage("�A�C���F�����ɗ����Ă��Ƃ��A���������\�ʓI�Ȏ�����Ȃ��B");

                                 UpdateMainMessage("�A�C���F���ŃR��������񂾂����E�E�E");

                                 UpdateMainMessage("�A�C���F�ǂ����ė����Ă�񂾂����E�E�E�Ƃ�");

                                 UpdateMainMessage("�A�C���F���������������炠�����񂾁E�E�E�H�Ƃ���");

                                 UpdateMainMessage("�A�C���F���ƂȂ��������������A�ǂ����Ă��v���o���˂��񂾂�B");

                                 UpdateMainMessage("�A�C���F���i�ɓn���ƁA�������̂�����₵�����������������Ă��܂������ł��B");

                                 UpdateMainMessage("�A�C���F����ł����A�n���̂��x��Ă��܂����A���ă��P���B");

                                 UpdateMainMessage("���i�F�E�E�E�����E�E�E�v���o�����́H");

                                 UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E�@");

                                 UpdateMainMessage("�A�C���F�����A�v���o�������B");
                             }

                             UpdateMainMessage("�n���i�F�A�C���A���i�����B���낻��X���܂�����B");

                             UpdateMainMessage("���i�F���I���A�����ˁB��������Ȏ��Ԃ���Ȃ��B");

                             UpdateMainMessage("���i�F�n���i���΂���A�����������܂ł�����");

                             UpdateMainMessage("�n���i�F�A�C�����������Ƌx�ނ񂾂ˁB");

                             UpdateMainMessage("�A�C���F���H���A�����A���肪�Ƃ��������܂��B�����������܂ł����I");

                             UpdateMainMessage("�A�C���F�񂶂Ⴀ�A�܂������ȁB���i�B");

                             UpdateMainMessage("���i�F�����A��������Q�K�ˁB���̒��q�Ői�݂܂��傤�B");

                             UpdateMainMessage("�A�C���F�������ȁI���Ⴀ�A�܂��ȁI");

                             GroundOne.StopDungeonMusic();

                             ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_NIGHT);
                             using (MessageDisplay md = new MessageDisplay())
                             {
                                 md.StartPosition = FormStartPosition.CenterParent;
                                 md.Message = "�A�C�����\�񂵂Ă��������ɂ�";
                                 md.ShowDialog();
                             }

                             UpdateMainMessage("�A�C���F�ӂ��E�E�E�o�b�N�p�b�N�������ƁE�E�E");

                             UpdateMainMessage("�A�C���F��������Q�K���E�E�E������I�C������邺�I�I");

                             UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                             UpdateMainMessage("�A�C���F�E�E�E");

                             this.backgroundData = null;
                             this.BackColor = Color.Black;

                             if (GroundOne.WE2.TruthRecollection1)
                             {
                                 UpdateMainMessage("�A�C���F���̓_���W�����֍s�����Ƃ��Ă����B");

                                 UpdateMainMessage("�A�C���F�t���͖��������Č����Ă����A���ɂ͏o����C�����Ă����B");

                                 UpdateMainMessage("�A�C���F���������͊y�X���Ȃ��Ă�����");

                                 UpdateMainMessage("�A�C���FDUEL�Ɋւ��Ă��A���Ȃ��ʂɃ����N�C���o���Ă����B");

                                 UpdateMainMessage("�A�C���F�����A�������g���悤�₭�����Ɗ�������悤�ɂȂ��Ă����B");

                                 UpdateMainMessage("�A�C���F�����O�X�̃_���W�����B");

                                 UpdateMainMessage("�A�C���F������u�_�̎����v���Ă̂��҂��\���Ă���炵��");

                                 UpdateMainMessage("�A�C���F��k����Ȃ��B���́u�_�v�Ƃ������ނ��匙�����B");

                                 UpdateMainMessage("�A�C���F�_�Ƃ����̕t�����ɂ́A���܂��ăE��������B");

                                 UpdateMainMessage("�A�C���F��΂ɐ��̂�\���Ă��B�����āA");

                                 UpdateMainMessage("�A�C���F���̃_���W�����̍ŉ��w�܂Ő�΂ɒH����Ă݂���B");

                                 UpdateMainMessage("�A�C���F�ŏ��͂����l���Ă����B");

                                 UpdateMainMessage("�A�C���F�P�K�܂ł͉��̋�J�������N���A���邱�Ƃ��o���Ă����B");

                                 UpdateMainMessage("�A�C���F�s���v�f�Ȃ�Ă͈̂�����������B");

                                 UpdateMainMessage("�A�C���F�����Č����΁A���i�ƈꏏ�Ƀ_���W�����֌��������ɂȂ������炢���B");

                                 UpdateMainMessage("�A�C���F���i�͕��i�̓������̂͗ǂ��B");

                                 UpdateMainMessage("�A�C���F�����A���i�͂����ƌ������ɍd�����Ă��܂��ꍇ������B");

                                 UpdateMainMessage("�A�C���F�܂����񎞂́A�����Ƃ����ɃJ�o�[�ɓ���Ηǂ������̘b�B���v���B");

                                 UpdateMainMessage("�A�C���F�s���v�f�ƌĂԂɂ͂��܂�ɂ�����������s�����B");

                                 UpdateMainMessage("�A�C���F���i�Ɖ��́A���܂ł��ǂ��A�g��g��ł���Ă��Ă���B");

                                 UpdateMainMessage("�A�C���F���݂��̎��͒m��s�����Ă���B");

                                 UpdateMainMessage("�A�C���F�P�K�{�X�ɂ͎኱��Ԏ�������̂�");

                                 UpdateMainMessage("�A�C���F�{�X�̈А��̗ǂ��������Ȃ�܂ł͂���ȂɎ��Ԃ͂�����Ȃ������B");

                                 if (we.Truth_GiveLanaEarring)
                                 {
                                     UpdateMainMessage("�A�C���F�E�E�E�@�m���������B�@�v���o�����B");

                                     UpdateMainMessage("�A�C���F���i�ƈꏏ�Ƀ_���W�����֐i��ŁE�E�E���ꂩ��E�E�E");

                                     UpdateMainMessage("�A�C���F�������A���i�̃C�������O���B");

                                     UpdateMainMessage("�A�C���F���B�͂܂��_���W�����̒��ɂ���B");

                                     UpdateMainMessage("�A�C���F�_���W�������ŁA�{�X��|������A���͊m���Ɍ��Ă�B");

                                     UpdateMainMessage("�A�C���F���i�͂��̃C�������O�͂��̎��A�܂��t���Ă����B");

                                     UpdateMainMessage("�A�C���F�����������̕����ɕ����Ă���㕨����˂��B");

                                     UpdateMainMessage("�A�C���F�_���W�������̂ǂ����Ŗ������������B");

                                     UpdateMainMessage("�A�C���F���ꂪ���̕����ɂ�����Ă̂��l�����Ȃ��B");

                                     UpdateMainMessage("�A�C���F�����œ˂�����ł����āA�Ӗ��킩��˂����ǁE�E�E");

                                     UpdateMainMessage("�A�C���F���ƃ��i�̓_���W������i�߂�r���ŁE�E�E");

                                     UpdateMainMessage("�A�C���F�����d��Ȏ��s�����������B");

                                     UpdateMainMessage("�A�C���F������A���Ԃ��̂��Ȃ������E�E�E");

                                     UpdateMainMessage("�A�C���F�b�O�E�E�E�ʖڂ��B�������ǂ����Ă��v���o���˂��E�E�E");
                                 }
                                 else
                                 {
                                     GroundOne.WE2.TruthKey1 = true; // �����^�����E�ւ̃L�[���̂P�Ƃ���B
                                 }
                             }

                             UpdateMainMessage("�@�@�@�y�������͂��̌�A�Q�K�ւ̊K�i�𔭌����z");

                             UpdateMainMessage("�@�@�@�y���̂܂܁A�Q�K�ւƑ����^�񂾁z");

                             we.TruthCommunicationCompArea1 = true;
                         }

                         we.TruthCommunicationCompArea1 = true;
                         we.AlreadyRest = true;

                         using (ESCMenu esc = new ESCMenu())
                         {
                             esc.MC = this.MC;
                             esc.SC = this.SC;
                             esc.TC = this.TC;
                             esc.WE = this.we;
                             esc.KnownTileInfo = null;
                             esc.KnownTileInfo2 = null;
                             esc.KnownTileInfo3 = null;
                             esc.KnownTileInfo4 = null;
                             esc.KnownTileInfo5 = null;
                             esc.Truth_KnownTileInfo = this.Truth_KnownTileInfo;
                             esc.Truth_KnownTileInfo2 = this.Truth_KnownTileInfo2;
                             esc.Truth_KnownTileInfo3 = this.Truth_KnownTileInfo3;
                             esc.Truth_KnownTileInfo4 = this.Truth_KnownTileInfo4;
                             esc.Truth_KnownTileInfo5 = this.Truth_KnownTileInfo5;
                             esc.StartPosition = FormStartPosition.CenterParent;
                             esc.TruthStory = true;
                             esc.OnlySave = true;
                             esc.ShowDialog();
                         }

                         //this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                         ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                         this.BackColor = Color.WhiteSmoke;
                         this.Update();
                         UpdateMainMessage("", true);

                         SecondCommunicationStart();
                     }
                 }
                 #endregion
                #region "�Q�K����"
                 else if (this.we.TruthCompleteArea1 && this.we.TruthCommunicationCompArea1 && !we.Truth_CommunicationSecondHomeTown)
                 {
                     SecondCommunicationStart();
                 }
                #endregion
                #region "�Q�K�A�n�̕����A�I�����s"
                if (we.dungeonEvent206 && !we.dungeonEvent207 && we.dungeonEvent207FailEvent2)
                {
                    we.dungeonEvent207FailEvent2 = false;
                    if (!we.dungeonEvent207FailEvent)
                    {
                        we.dungeonEvent207FailEvent = true;

                        UpdateMainMessage("�A�C���F�b�Q�I���ɖ߂�������˂����I�I");

                        UpdateMainMessage("���i�F�����̋����]�ڑ��u�݂����Ȃ��̂�����B");

                        UpdateMainMessage("�A�C���F����A�����ȁB�����������܂����݂������B");

                        UpdateMainMessage("���i�F�_���W�����Q�[�g�E�E�E����Ȃ��݂�����B");

                        UpdateMainMessage("���i�F�����͎�d�����ɂ��邵���Ȃ��݂����ˁB�n�A�@�@�@�@�E�E�E");

                        UpdateMainMessage("�A�C���F�������E�E�E���̓~�X��Ȃ��悤�ɂ��邺�B");
                    }
                    else
                    {
                        UpdateMainMessage("�A�C���F�b�Q�I�܂�������܂����I�I");

                        UpdateMainMessage("���i�F������ƁE�E�E�u��v�̈Ӗ��������Ă�킯�H");

                        UpdateMainMessage("�A�C���F���A�����B������񂾁B���������B");

                        UpdateMainMessage("���i�F�������肵�Ă�ˁA�o�J�A�C���B�n�A�@�@�@�@�E�E�E");

                        UpdateMainMessage("�A�C���F�������E�E�E���x�����E�E�E");
                    }
                }
                else if (we.dungeonEvent208 && !we.dungeonEvent209 && we.dungeonEvent209FailEvent2)
                {
                    we.dungeonEvent209FailEvent2 = false;
                    if (!we.dungeonEvent209FailEvent)
                    {
                        we.dungeonEvent209FailEvent = true;

                        UpdateMainMessage("�A�C���F�b�Q�I���ɖ߂�������˂����I�I");

                        UpdateMainMessage("���i�F�����̋����]�ڑ��u�݂����Ȃ��̂�����B");

                        UpdateMainMessage("�A�C���F����A�����ȁB�����������܂����݂������B");

                        UpdateMainMessage("���i�F�_���W�����Q�[�g�E�E�E����Ȃ��݂�����B");

                        UpdateMainMessage("���i�F�����͎�d�����ɂ��邵���Ȃ��݂����ˁB�n�A�@�@�@�@�E�E�E");

                        UpdateMainMessage("�A�C���F�������E�E�E���̓~�X��Ȃ��悤�ɂ��邺�B");
                    }
                    else
                    {
                        UpdateMainMessage("�A�C���F�b�Q�I�܂�������܂����I�I");

                        UpdateMainMessage("���i�F������ƁE�E�E�u���v�̈Ӗ��������Ă�킯�H");

                        UpdateMainMessage("�A�C���F���A�����B������񂾁B���������B");

                        UpdateMainMessage("���i�F�������肵�Ă�ˁA�o�J�A�C���B�n�A�@�@�@�@�E�E�E");

                        UpdateMainMessage("�A�C���F�������E�E�E���x�����E�E�E");
                    }
                }
                else if (we.dungeonEvent210 && !we.dungeonEvent211 && we.dungeonEvent211FailEvent2)
                {
                    we.dungeonEvent211FailEvent2 = false;
                    if (!we.dungeonEvent211FailEvent)
                    {
                        we.dungeonEvent211FailEvent = true;

                        UpdateMainMessage("�A�C���F�b�Q�I���ɖ߂�������˂����I�I");

                        UpdateMainMessage("���i�F�����̋����]�ڑ��u�݂����Ȃ��̂�����B");

                        UpdateMainMessage("�A�C���F����A�����ȁB�����������܂����݂������B");

                        UpdateMainMessage("���i�F�_���W�����Q�[�g�E�E�E����Ȃ��݂�����B");

                        UpdateMainMessage("���i�F�����͎�d�����ɂ��邵���Ȃ��݂����ˁB�n�A�@�@�@�@�E�E�E");

                        UpdateMainMessage("�A�C���F�������E�E�E���̓~�X��Ȃ��悤�ɂ��邺�B");
                    }
                    else
                    {
                        UpdateMainMessage("�A�C���F�b�Q�I�܂�������܂����I�I");

                        UpdateMainMessage("���i�F������ƁE�E�E�u���v�̈Ӗ��������Ă�킯�H");

                        UpdateMainMessage("�A�C���F���A�����B������񂾁B���������B");

                        UpdateMainMessage("���i�F�������肵�Ă�ˁA�o�J�A�C���B�n�A�@�@�@�@�E�E�E");

                        UpdateMainMessage("�A�C���F�������E�E�E���x�����E�E�E");
                    }
                }
                #endregion
                #region "�Q�K�A�_�̎����N���A��"
                else if (GroundOne.WE2.TruthAnswerSuccess && we.dungeonEvent224 && !we.dungeonEvent225)
                {
                    we.dungeonEvent225 = true;

                    UpdateMainMessage("�A�C���F������A�߂��Ă����ȁB");

                    UpdateMainMessage("�A�C���F�Ȃ����i�B����������ӁA����Ă����Ȃ����H");

                    UpdateMainMessage("���i�F���H����B�ł��ǂ��ɍs���̂�H");

                    UpdateMainMessage("�A�C���F�w�������@�V�n�x�łǂ����H");

                    UpdateMainMessage("���i�F���̓X�����Z���Ȃ�������H�@�܂��������ǁB");

                    UpdateMainMessage("�A�C���F����A���������s���Ƃ��邩�I");

                    UpdateMainMessage("���i�F�܂������A����Ȗ��̂ǂ����ǂ��̂�����E�E�E");

                    CallSomeMessageWithAnimation("    �w�������@�V�n�x�ɂāE�E�E");

                    UpdateMainMessage("�A�C���F�Ӂ`�A����ς����̖��͍ō������I�b�n�b�n�b�n�I");

                    UpdateMainMessage("���i�F���A����܂�D������Ȃ��񂾂��ǁE�E�E���āA������ƕ����Ă�H");

                    UpdateMainMessage("�A�C���F�Ƃ���ŁA���̎������ǂ��B���i�̕ꂳ�񂪍�����̂��H");

                    UpdateMainMessage("���i�F�Ⴄ���B�ꂳ������̐�ォ��`������Ă�������������B");

                    UpdateMainMessage("�A�C���F���H");

                    UpdateMainMessage("���i�F������A�����痥����ɑ�X�`�����Ă��Ă��鎍��B");

                    UpdateMainMessage("���i�F�A�C���͏��������A���܂��܎��ƈꏏ�Ɍm�×��K���Ă�����B");

                    UpdateMainMessage("���i�F����ŋ��R�����Ă��̂�ˁB��������������A�o���ĂȂ��̂����ʂ�B");

                    UpdateMainMessage("�A�C���F�Ȃ�قǁE�E�E");

                    UpdateMainMessage("�A�C���F�ǂ���Ŏv���o���Ȃ��킯���B�b�n�n�n�E�E�E");

                    UpdateMainMessage("�A�C���F�ł��A�ς���ȁB");

                    UpdateMainMessage("���i�F�����H");

                    UpdateMainMessage("�A�C���F���̃_���W�����ł��B");

                    UpdateMainMessage("�A�C���F�Ȃ�ł���Ȏ����N���肤��񂾁H");

                    UpdateMainMessage("���i�F�E�E�E�����E�E�E���ƁE�E�E");

                    using (TruthDecision td = new TruthDecision())
                    {
                        td.MainMessage = "�@�y�@���i�ɂ��̂܂ܖ₢�l�߂Ă݂܂����H�@�z";
                        td.FirstMessage = "�₢�l�߂�B";
                        td.SecondMessage = "�₢�l�߂��A�b���ς���B";
                        td.StartPosition = FormStartPosition.CenterParent;
                        td.ShowDialog();
                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                        {
                            UpdateMainMessage("�A�C���F���i�͂��̃_���W�����̂��炭��A�ǂ��܂Ŕc�����Ă�H");

                            UpdateMainMessage("���i�F�����A���炭����ĉ��̎���H");

                            UpdateMainMessage("���i�F�m��Ȃ����A����Ȃ́B");

                            UpdateMainMessage("�A�C���F���ł��ǂ��B�m���Ă鎖������΋����Ă���B");

                            UpdateMainMessage("���i�F�E�E�E");

                            UpdateMainMessage("���i�F�m��Ȃ���B�{����B");

                            UpdateMainMessage("�A�C���F���������B�B�B");

                            UpdateMainMessage("�A�C���F�����ȁB�����������₢�l�߂��܂��āB");

                            UpdateMainMessage("���i�F�E�E�E�˂��A�A�C���B");

                            UpdateMainMessage("�A�C���F��A�����H");

                            UpdateMainMessage("���i�F���`��A���ł��Ȃ���B");

                            UpdateMainMessage("�A�C���F����A�ǂ��񂾁B���������ȁB");

                            UpdateMainMessage("���i�F�����A��������Ȃ��́B");

                            UpdateMainMessage("���i�F�����f�B�X�̂��t������ɁA�����ɂ����Ă݂Ȃ��H");

                            UpdateMainMessage("�A�C���F���H�@���̃{�P�t���ɂ��H");

                            UpdateMainMessage("���i�F�A�C�����{���ɍ����Ă鎞�A������܂�͂ɂȂ�ĂȂ��݂��������B");

                            UpdateMainMessage("���i�F�����f�B�X�̂��t������Ȃ�A���������Ă��ꂻ������Ȃ��H");

                            UpdateMainMessage("�A�C���F�ǂ����낤�Ȃ��E�E�E");

                            UpdateMainMessage("�A�C���F������Ƃł��ݖ���ԈႦ��ᏸ�V������ȁE�E�E�b�N�E�E�E");

                            UpdateMainMessage("���i�F�܂��A�����ɂƂ͌���Ȃ����ǁB");

                            UpdateMainMessage("�A�C���F������A�����Ă݂邺�B�r�r���Ă����Ă��傤���˂�����ȁB");

                            UpdateMainMessage("�A�C���F���낢��ƃT���L���[�ȁA���i�B");

                            UpdateMainMessage("���i�F���ӂӁA�ʂɗǂ����B�債�����͂���ĂȂ���B");

                            UpdateMainMessage("���i�F���Ⴀ�A������ƃ����f�B�X�̂��t������̏��ɍs���Ă݂܂����");

                            UpdateMainMessage("�A�C���F�����I");

                            we.dungeonEvent226 = true;
                        }
                        else
                        {
                            UpdateMainMessage("�A�C���F�����̂́A���i���m���Ă��񂾂�ȁH");

                            UpdateMainMessage("���i�F�����A�����ˁB�������x�������Ă����A�悭�o���Ă���B");

                            UpdateMainMessage("�A�C���F���̎��A���̃_���W�������������߂̃q���g�ɂȂ邩���m��˂��B");

                            UpdateMainMessage("�A�C���F�ꉞ�������Ƃ��Ă���邩�H");

                            UpdateMainMessage("���i�F����A�����������");

                        }
                    }
                }
                #endregion
                #region "�Q�K���e"
                else if (this.we.TruthCompleteArea2 && !this.we.TruthCommunicationCompArea2)
                {
                    UpdateMainMessage("�A�C���F�您���������A���B���B�I�I");

                    UpdateMainMessage("���i�F�b�t�t�t�A�N���A�����ゾ���F�ɒm�点�ɍs���܂����");

                    UpdateMainMessage("�A�C���F�����A�������ȁI");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "�A�C���͈�ʂ�A���̏Z�l�B�ɐ��������A���Ԃ����X�Ɖ߂��Ă������B";
                        md.ShowDialog();

                        md.Message = "���̓��̖�A�n���i�̏h�����ɂ�";
                        md.ShowDialog();
                    }

                    GroundOne.StopDungeonMusic();
                    GroundOne.PlayDungeonMusic(Database.BGM15, Database.BGM15LoopBegin);

                    // �W���o�[���S��False�A����
                    // �^�����̕����֓��B���Ă��Ȃ��ꍇ�ABADEND
                    if ((!GroundOne.WE2.TruthAnswer2_1) &&
                        (!GroundOne.WE2.TruthAnswer2_2) &&
                        (!GroundOne.WE2.TruthAnswer2_3) &&
                        (!GroundOne.WE2.TruthAnswer2_4) &&
                        (!GroundOne.WE2.TruthAnswer2_5) &&
                        (!GroundOne.WE2.TruthAnswer2_6) &&
                        (!GroundOne.WE2.TruthAnswer2_7) &&
                        (!GroundOne.WE2.TruthAnswer2_8) &&
                        (!GroundOne.WE2.TruthRecollection2))
                    {
                        if (!GroundOne.WE2.TruthAnswer2_OK)
                        {
                            UpdateMainMessage("�A�C���F�������A���Ȃ񂾂�ȁB");

                            UpdateMainMessage("���i�F������H");

                            UpdateMainMessage("�A�C���F���ǂ��B���̂悭������Ȃ����o�[�͉��������񂾂낤�ȁB");

                            UpdateMainMessage("���i�F�G���Ă����ɉ��������������������ˁB");

                            UpdateMainMessage("�A�C���F�󒆕����Ƃ��o�ĂĂ��B���o���������������ɓ����肪����������ȁB");

                            UpdateMainMessage("���i�F���̊K�ŉ����e�����o��Ƃ�����Ȃ�������H");

                            UpdateMainMessage("�A�C���F�ǂ����낤�ȁB�������Ɨǂ��񂾂��B");
                        }

                        UpdateMainMessage("���i�F�n���i�f�ꂳ��B���؂Ȃ����������肪�Ƃ��������܂���");

                        UpdateMainMessage("�n���i�F�͂���A�����̗[�т͂�����荋�؂ɂ��Ă���������ˁB");

                        UpdateMainMessage("���i�F���A���肪�Ƃ��������܂���");

                        UpdateMainMessage("�A�C���F�������I����̓X�Q�F�I���������܂��I�I");

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "�A�C���ƃ��i���[�т�H�ׂ���E�E�E";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("�A�C���F�ӂ��A�����H���Ȃ����B���΂����A���肪�ƁI");

                        UpdateMainMessage("�n���i�F��������Ȃ�A�A���^�̂��t������Ɍ����Ă����񂾂ˁB");

                        UpdateMainMessage("�A�C���F���ցI�H");

                        UpdateMainMessage("�n���i�F���������ė�����R�b�\���������ꂵ�Ă񂾂�B");

                        UpdateMainMessage("�A�C���F�b�}�W����I�H");

                        UpdateMainMessage("�n���i�F�i���}�l�j�w�����f�B�X�F���j�������H�N�\������˂��A����ɂ���Ă�x");

                        UpdateMainMessage("�n���i�F�Ƃ����Ƃ��s���đ��s�łǂ����ɍs�����܂�����B");

                        UpdateMainMessage("���i�F�t�t�t�A�����f�B�X������Ă���ς�ǂ��l����Ȃ��B");

                        UpdateMainMessage("�A�C���F���̎�����Ύj���`�̃{�P�t���Ɍ����Ă��E�E�E�n�n�n");

                        UpdateMainMessage("�n���i�F��Ƃ��͂�������A�܂������Ă���񂾂ˁA�A�b�n�n�n");

                        UpdateMainMessage("�A�C���F�n�A�b�n�n�n�n�E�E�E");

                        UpdateMainMessage("�n���i�F�A�C���B�Ƃ���łP�K�Ŏv���o���Ă����b�ɂ��ď����������Ă�����B");

                        UpdateMainMessage("�A�C���F�����E�E���ƁA�P�K�ł����H");

                        GroundOne.StopDungeonMusic();

                        ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_NIGHT);

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "�A�C�����\�񂵂Ă��������ɂ�";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("�A�C���F�ӂ��E�E�E�o�b�N�p�b�N�������ƁE�E�E");

                        UpdateMainMessage("�A�C���F�R�K���E�E�E�������炢�悢�����Ȃ�񂾂낤�ȁB");

                        UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E�@�n���i�̂��΂���A�����Ȃ艽�����Ă񂾂낤�E�E�E");

                        UpdateMainMessage("�A�C���F�P�K�Ŏv���o�����b�H");

                        UpdateMainMessage("�A�C���F�Q�K�����������̎��ɂȂ��āA�����Ȃ艽���E�E�E");

                        this.backgroundData = null;
                        this.BackColor = Color.Black;

                        UpdateMainMessage("�A�C���F�E�E�E�@�����Y��Ă�C������@�E�E�E");

                        UpdateMainMessage("�A�C���F���A������Ă��񂾂����E�E�E");

                        UpdateMainMessage("�A�C���F�܂�������A�ЂƂ܂��_���W�����̍ŉ��w�ցE�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E");

                        UpdateMainMessage(" �`�@THE�@END�@�`�@�i���\�ցj");

                        GroundOne.WE2.TruthBadEnd2 = true;
                    }
                    // ����ȊO��GOOD
                    else
                    {
                        if (!GroundOne.WE2.TruthAnswer2_OK)
                        {
                            UpdateMainMessage("�A�C���F�������A���Ȃ񂾂�ȁB");

                            UpdateMainMessage("���i�F������H");

                            UpdateMainMessage("�A�C���F���ǂ��B���̂悭������Ȃ����o�[�͉��������񂾂낤�ȁB");

                            UpdateMainMessage("���i�F�G���Ă����ɉ��������������������ˁB");

                            UpdateMainMessage("�A�C���F�󒆕����Ƃ��o�ĂĂ��B���o���������������ɓ����肪����������ȁB");

                            UpdateMainMessage("���i�F���̊K�ŉ����e�����o��Ƃ�����Ȃ�������H");

                            UpdateMainMessage("�A�C���F�ǂ����낤�ȁB�������Ɨǂ��񂾂��B");
                        }

                        UpdateMainMessage("���i�F�n���i�f�ꂳ��B���؂Ȃ����������肪�Ƃ��������܂���");

                        UpdateMainMessage("�n���i�F�͂���A�����̗[�т͂�����荋�؂ɂ��Ă���������ˁB");

                        UpdateMainMessage("���i�F���A���肪�Ƃ��������܂���");

                        UpdateMainMessage("�A�C���F�������I����̓X�Q�F�I���������܂��I�I");

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "�A�C���ƃ��i���[�т�H�ׂ���E�E�E";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("�A�C���F�ӂ��A�����H���Ȃ����B���΂����A���肪�ƁI");

                        UpdateMainMessage("�n���i�F��������Ȃ�A�A���^�̂��t������Ɍ����Ă����񂾂ˁB");

                        UpdateMainMessage("�A�C���F���ցI�H");

                        UpdateMainMessage("�n���i�F���������ė�����R�b�\���������ꂵ�Ă񂾂�B");

                        UpdateMainMessage("�A�C���F�b�}�W����I�H");

                        UpdateMainMessage("���i�F�t�t�t�A�����f�B�X������Ă���ς�ǂ��l����Ȃ��B");

                        UpdateMainMessage("�A�C���F���̎�����Ύj���`�̃{�P�t���Ɍ����Ă��E�E�E�n�n�n");

                        UpdateMainMessage("�n���i�F��Ƃ��͂�������A�܂������Ă���񂾂ˁA�A�b�n�n�n");

                        UpdateMainMessage("�A�C���F�n�A�b�n�n�n�n�E�E�E");

                        UpdateMainMessage("�n���i�F�A�C���B�Ƃ���łP�K�Ŏv���o���Ă����b�ɂ��ď����������Ă�����B");

                        UpdateMainMessage("�A�C���F�����E�E���ƁA�P�K�ł����H");

                        UpdateMainMessage("�A�C���F�������ƁA����H�@�������������A���i�I�H");

                        UpdateMainMessage("���i�F������Ɖ��Ń\�R�Ŏ��ɐU���Ă�̂�B�@�����Ŏv���o���Ȃ�����ˁB");

                        UpdateMainMessage("�A�C���F����E�E�E�����ƁA���������H�@�A���H");

                        UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                        UpdateMainMessage("�A�C���F�����I�������I���i�̃C�������O���I�I");

                        UpdateMainMessage("�A�C���F�����I����͖{���Ɉ��������I�I");

                        UpdateMainMessage("���i�F�b�t�t�t�A����͂����ǂ����Č���������Ȃ���");

                        UpdateMainMessage("���i�F���̌�Ŏv���o��������B�����v���o�����̂��b���Ă���Ȃ��H");

                        UpdateMainMessage("�A�C���F���̌�E�E�E�H");

                        UpdateMainMessage("�A�C���F�����A�I�[�P�[�I�[�P�[");

                        UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                        UpdateMainMessage("�A�C���F���i�̃C�������O����");

                        UpdateMainMessage("�A�C���F����̓��i�Ɖ����Q�K�֍~��鏊�ŁA���i�����Ƃ������m���B");

                        UpdateMainMessage("�A�C���F����������E����");

                        UpdateMainMessage("�A�C���F���i�A���O�ɓn�����B");

                        UpdateMainMessage("�A�C���F�����Ō����Ăē������������Ȃ肻������");

                        UpdateMainMessage("�A�C���F����͊m���ȋL�����B");

                        UpdateMainMessage("�A�C���F�ԈႢ�͂˂��B");

                        UpdateMainMessage("�A�C���F�������A�����炱��");

                        UpdateMainMessage("�A�C���F�P�K�𐧔e�������_�Ń��i�ɓn�����Ǝv�����񂾁B");

                        UpdateMainMessage("�n���i�F�悭������ˁB�A�C���B");

                        UpdateMainMessage("�n���i�F��������B");

                        UpdateMainMessage("�A�C���F���΂����͂���ς�m���Ă�񂾂ȁA�S�Ă��B");

                        UpdateMainMessage("�n���i�F�킽����{���ɉ����m��Ȃ���B");

                        UpdateMainMessage("�n���i�F���͂˃A�C���A���񂽂̎菕�������Ă邾������B");

                        UpdateMainMessage("�A�C���F��A�܂������Ȃ񂾂낤���ǂ��B");

                        UpdateMainMessage("�A�C���F���i�A���������Ȗ{���ɁB");

                        UpdateMainMessage("���i�F���Ŏӂ��Ă�̂�A�ʂɈ��������Ă�킯����Ȃ��񂾂���");

                        UpdateMainMessage("�A�C���F����A���₢��B�����������Ƃ͂����Ƒ��߂ɁE�E�E");

                        UpdateMainMessage("���i�F�����āA���͂��������ɖ߂낤���ȁ�");

                        UpdateMainMessage("���i�F���΂���A���₷�݂Ȃ�����");

                        UpdateMainMessage("�n���i�F������A���₷�݁B�@�������x�ނ񂾂�B");

                        UpdateMainMessage("�A�C���F�ӂ��E�E�E���j���̃n�Y�������̂ɁA�����������܂������ȁB");

                        UpdateMainMessage("�n���i�F�A�b�n�n�n�A�������Ă񂾂��B�A���^�͖{���Ƀo�J���ˁB");

                        UpdateMainMessage("�n���i�F�b�z���A�����͂����������x�ނ񂾂ˁA�����ɔ����āB");

                        UpdateMainMessage("�A�C���F���A�������B");

                        UpdateMainMessage("�A�C���F���Ⴀ�����͂��΂����A���낢��T���L���[�ȁI");

                        UpdateMainMessage("�n���i�F����܂�l�����ނ񂶂�Ȃ���A�Q��Ȃ��Ȃ邩��ˁH");

                        UpdateMainMessage("�A�C���F�����A�������Ă���āB���Ⴀ���₷�݂Ȃ����I");

                        GroundOne.StopDungeonMusic();

                        ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_NIGHT);

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "�A�C�����\�񂵂Ă��������ɂ�";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("�A�C���F�ӂ��E�E�E�o�b�N�p�b�N�������ƁE�E�E");

                        UpdateMainMessage("�A�C���F�R�K���E�E�E�������炢�悢�����Ȃ�񂾂낤�ȁB");

                        UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                        this.backgroundData = null;
                        this.BackColor = Color.Black;

                        if (GroundOne.WE2.TruthRecollection1)
                        {
                            UpdateMainMessage("�A�C���F���i�ɃC�������O��n�����̂͗ǂ����B");

                            UpdateMainMessage("�A�C���F���ꂾ���Ȃ�A���͒v���I�Ȍ��ۂɑ������邱�Ƃ͖��������B");

                            UpdateMainMessage("�A�C���F�_�̎����Ƃ��́A�ȒP�������B");

                            UpdateMainMessage("�A�C���F�w�@�_�X�̎��u�C�Ƒ�n�A�����ēV��v�@�x");

                            UpdateMainMessage("�A�C���F���i�̕ꂳ�񂩂�ǂ���������Ă������c���B");

                            UpdateMainMessage("�A�C���F�Q�K�������Ă���Ԃ̓n�b�L���v���o���Ȃ��������m���������B");

                            UpdateMainMessage("�A�C���F�����P�ɂ���𐺂ɂ��Ĕ����邾���B");

                            UpdateMainMessage("�A�C���F��������Ƃ����������B����̉����������Ȃ̂����ɂ͗������y�΂Ȃ������B");

                            UpdateMainMessage("�A�C���F�����āA�ǂ����Ă��v���o���Ȃ��̂��E�E�E");

                            UpdateMainMessage("�A�C���F�w���F���[�E�A�[�e�B�x�̑��݁B");

                            UpdateMainMessage("�A�C���F�`����FiveSeeker�A�Z�̒B�l���Ă̂͒m���Ă�B");

                            UpdateMainMessage("�A�C���F���̓_���W�����Q�[�g���Ŋm���������Ă�B");

                            UpdateMainMessage("�A�C���F�ނ͂��������Ă����B");

                            UpdateMainMessage("�@�@�@�w���F���[�F�A�C���N�A�͂��߂܂��āB�x");

                            UpdateMainMessage("�@�@�@�w�{����Verze Artie���Č����񂾁B��낵���ˁB�x�@�@");

                            UpdateMainMessage("�A�C���F�b�n�n�n�E�E�E���������΂������B");

                            UpdateMainMessage("�A�C���F���v���o������������B");

                            UpdateMainMessage("�A�C���F�����������B");

                            UpdateMainMessage("�A�C���F�u�͂��߂܂��āv�@�ǂ���̑�������˂��B");

                            UpdateMainMessage("�A�C���F�͂��߂܂��āA�ȃ��P���˂��񂾁B");

                            UpdateMainMessage("�A�C���F���́E�E�E���̐l�̎����E�E�E");

                            UpdateMainMessage("�A�C���F�����Ƃ����ƑO����m���Ă�B");

                            GroundOne.WE2.TruthKey2 = true; // �����^�����E�ւ̃L�[���̂Q�Ƃ���B
                        }

                        UpdateMainMessage("�@�@�@�y���͂��̕���̐^����m���Ă�̂����m��Ȃ��B�z");

                        UpdateMainMessage("�@�@�@�y����Ȋ�ȍ��o���o���A�R�K�ւƑ���i�߂��B�z");

                        we.TruthCommunicationCompArea2 = true;
                    }

                    we.TruthCommunicationCompArea2 = true;
                    CallRestInn(true);
                    
                    using (ESCMenu esc = new ESCMenu())
                    {
                        esc.MC = this.MC;
                        esc.SC = this.SC;
                        esc.TC = this.TC;
                        esc.WE = this.we;
                        esc.KnownTileInfo = null;
                        esc.KnownTileInfo2 = null;
                        esc.KnownTileInfo3 = null;
                        esc.KnownTileInfo4 = null;
                        esc.KnownTileInfo5 = null;
                        esc.Truth_KnownTileInfo = this.Truth_KnownTileInfo;
                        esc.Truth_KnownTileInfo2 = this.Truth_KnownTileInfo2;
                        esc.Truth_KnownTileInfo3 = this.Truth_KnownTileInfo3;
                        esc.Truth_KnownTileInfo4 = this.Truth_KnownTileInfo4;
                        esc.Truth_KnownTileInfo5 = this.Truth_KnownTileInfo5;
                        esc.StartPosition = FormStartPosition.CenterParent;
                        esc.TruthStory = true;
                        esc.OnlySave = true;
                        esc.ShowDialog();
                    }

                    //this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                    this.BackColor = Color.WhiteSmoke;
                    this.Update();
                    UpdateMainMessage("", true);
                    ThirdCommunicationStart();
                }
                #endregion
                // �_���W��������A�Ҍ�A�K�{�C�x���g�ȊO�̃I�v�V�����C�x���g�Ƃ��ėD��
                #region "�Ŕu���܂Ȃ��T���v�������Ƃ�"
                else if (this.firstDay >= 1 && we.BoardInfo14 &&
                         we.Truth_CommunicationJoinPartyLana == false && we.AvailableSecondCharacter == false)
                {
                    UpdateMainMessage("�A�C���F���i�A�o���܂Ȃ��p���ĈӖ��������Ă���B");

                    UpdateMainMessage("���i�F�A�C���͂��܂Ȃ��o�J��");

                    UpdateMainMessage("�A�C���F�҂đ҂āB�Ӗ��������Ă�����Č����Ă邾��H");

                    UpdateMainMessage("���i�F���t�ǂ����A�A�C���͋��X�܂ŗ]���Ƃ��Ȃ��o�J���ĈӖ����");

                    UpdateMainMessage("�A�C���F�Ȃ�قǂȁE�E�E�Ȃ�قǁE�E�E");

                    UpdateMainMessage("���i�F�Ȃ�قǂ��āE�E�E�[������Ă�����񂾂��ǁB");

                    UpdateMainMessage("�A�C���F�܂��A�Ŕ��������񂾁B���e�͂������B");

                    UpdateMainMessage("�w���܂Ȃ��A�T�������H�x�@���ƁB");

                    UpdateMainMessage("���i�F�Ȃ�قǁB���X�܂ŒT���Ă݂����H�ƌ����Ă�݂����ˁB");

                    UpdateMainMessage("���i�F���ŁA���X�܂ŒT���Ă݂��킯�H");

                    if (!we.BeforeSpecialInfo1)
                    {
                        UpdateMainMessage("�A�C���F�����I�������S���T�������I");

                        UpdateMainMessage("�A�C���F������A���܂Ȃ����X�܂őS���ȁI�b�n�b�n�b�n�I�I");

                        UpdateMainMessage("���i�F���t�_�u���Ă���B�z���b�g�o�J��ˁB�n�A�A�@�@�@�E�E�E");

                        UpdateMainMessage("���i�F�˂��A�������������Ƃ��Ƃ�����񂶂�Ȃ��́H�����ƒT���Ă݂��H");

                        if (!we.TruthSpecialInfo1)
                        {
                            UpdateMainMessage("�A�C���F��H�܂����X�܂őS�����Č����Ă��ȁB");

                            UpdateMainMessage("�A�C���F���������A�ŉ��w���e���ړI����H");

                            UpdateMainMessage("���i�F�ł��ŉ��w�ɍs�����߂̕K�v�ȏ�񂪂��邩���m��Ȃ����H");

                            UpdateMainMessage("�A�C���F���i�A���ł܂����O�́A�������������悤�Ȍ����������Ă�񂾁B");

                            UpdateMainMessage("�A�C���F���܂��E�E�E�Ђ���Ƃ��ĉ����m���Ă�̂��H");

                            UpdateMainMessage("���i�F���ׁA���ׂׂוʂɒm��Ȃ����I�I");

                            UpdateMainMessage("�A�C���F���������A�����������āB����ȓ��h����Ȃ��āB");

                            UpdateMainMessage("�A�C���F�܂��A�C���������炢�낢��T�����Ă݂�Ƃ��邳�B");

                            UpdateMainMessage("���i�F�Ŕɂ������Ă��鎖�����A�T�����đ��͂��Ȃ��͂���B");

                            UpdateMainMessage("�A�C���F�I�[�P�[�A�C�ɂȂ鏊�͂܂��T�����邳�B�T���L���[�ȁB", true);

                            // [�^�G���f�B���O����]

                            UpdateMainMessage("�A�C���F�E�E�E�Ȃ��A���i�B");

                            UpdateMainMessage("���i�F�Ȃɂ�H");

                            UpdateMainMessage("�A�C���F����������A�Ŕ̈Ӑ}���͂߂˂���������A�����ł��B");

                            UpdateMainMessage("�A�C���F�_���W�����A�ꏏ�ɗ��Ȃ����H");

                            UpdateMainMessage("���i�F���`��A�ǂ����悤���ȁB");

                            UpdateMainMessage("�A�C���F���O������Ɨ���ɂȂ邩��ȁB���ȁA���ނ��B");

                            //UpdateMainMessage("�@�@�@�y���i�͈�u�A�A�C���ɂ͌����Ȃ��悤�ɁA����������悤�ȏΊ��������E�E�E�z");
                            
                            UpdateMainMessage("�@�@�@�y�@���i�͂�����ƍl�����ނ��Ԃ�ŁA����������悤�ȏΊ�������@�z");

                            UpdateMainMessage("�@�@�@�y�@����͈�u�̂��Ƃł���A�A�C���ɂƂ��Ă��̕\��͕�����Ȃ������@�z");

                            UpdateMainMessage("���i�F�����������ˁB�����Ă��炦�邩�����");

                            UpdateMainMessage("�A�C���F�����A������H�����Ă݂��B");

                            UpdateMainMessage("���i�F�y�@�^���̓����@�z�@�@�T���Ă�ˁH");

                            UpdateMainMessage("�A�C���F���ȁB�������āH");

                            UpdateMainMessage("���i�F�b�t�t�A��k��B��k��@���Ⴀ�A��������͎����s������");

                            UpdateMainMessage("�A�C���F�T���L���[�I���ɂ��邺�I�I�@�b�n�b�n�b�n�I");

                            if (we.AvailablePotionshop)
                            {
                                UpdateMainMessage("�A�C���F���ƁA���������΃��i�B���O�̂��X�͂ǂ�����񂾂�H");

                                UpdateMainMessage("���i�F���S�z�Ȃ���@�����ƌق��Ă����������");

                                UpdateMainMessage("�A�C���F�b�}�W����I�H���ł���ȗp�ӎ����Ȃ񂾂�I�H");

                                UpdateMainMessage("���i�F�܂��A���A�ڋq�͂���܂�����ĂȂ��̂�ˁB������ꂿ�Ⴄ���B");

                                UpdateMainMessage("���i�F����Ȃ킯������A�S�z�����p���");
                            }

                            UpdateMainMessage("�A�C���F���Ⴀ�A���������낵�����ނ��I�I");

                            UpdateMainMessage("���i�F�n�C�n�C��@���Ⴀ�܂������ˁB");

                            CallSomeMessageWithAnimation("�y���i���p�[�e�B�ɉ����܂����B�z");

                            we.AvailableSecondCharacter = true;
                            we.Truth_CommunicationJoinPartyLana = true;
                        }
                        else
                        {
                            UpdateMainMessage("�A�C���F�y�͂͗͂ɂ��炸�A�͂͑S�Ăł���B�z");

                            UpdateMainMessage("���i�F�����I�H");

                            UpdateMainMessage("�A�C���F���ꂩ��E�E�E�y�������Ȃ������B�@�������S�͖������B�z");

                            UpdateMainMessage("�A�C���F�Ō�́@�y�݂͂̂Ɉˑ�����ȁB�S��΂ɂ���B�z�@���������ȁB");

                            UpdateMainMessage("���i�F�E�\�I�H����Ȃ̂����Ɗo���Ă�́I�H");

                            UpdateMainMessage("�A�C���F�o���Ă�Ƃ������A�v���o�����B�����_���W�������ł���Ȍ��t���o�Ă����ȁB");

                            UpdateMainMessage("�A�C���F���i�̕ꂿ��񂪂���Ă������痥����B�������̏\�P�̈����B");

                            UpdateMainMessage("�A�C���F���͂Ƃ��ɂ��̂V�Ԗڂ��D�����������ȁB");

                            UpdateMainMessage("���i�F���͂悭������Ȃ����ǂˁA���������ނ̂����́B");

                            UpdateMainMessage("���i�F�A�C��������ƁA�A�C����������ԁB����ŏ\����ˁ�");

                            UpdateMainMessage("�A�C���F���O�̂����������͉��Ƃ�����˂��̂���E�E�E�ł��܂�");

                            UpdateMainMessage("�@�@�@�i�A�C���͂��ɂȂ��A�^���Ȋ፷���������n�߁E�E�E�j");

                            UpdateMainMessage("�A�C���F���̃_���W�����B�����ǂ߂����B");

                            UpdateMainMessage("���i�F���H");

                            UpdateMainMessage("�A�C���F����ɐi�񂾂�ʖڂȂ񂾁B���̃_���W�����B");

                            UpdateMainMessage("���i�F�ǂ������Ӗ���H");

                            UpdateMainMessage("�A�C���F���̌��t�Ŏv���o�������Ƃ�����B");

                            UpdateMainMessage("���i�F�����v���o�����́H");

                            UpdateMainMessage("�A�C���F�_���t�F���g�D�[�V���Ɋւ��Ă��B");

                            UpdateMainMessage("�@�@�@�i���i�͂ق�̈�u�����A������Ɉ�炵�Ă���E�E�E�j");

                            UpdateMainMessage("���i�F�t�F���g�D�[�V�����ǂ������̂�H");

                            UpdateMainMessage("�A�C���F�˂��h���ꂽ�ҁA�����ȗ͂ɂ�鎀���}����");

                            UpdateMainMessage("�A�C���F�q�[�����O���ʂ��K�p���ꂸ�A�h�����@�������Ȃ��B");

                            UpdateMainMessage("�A�C���F�܂��ɏ����ȗ͂��̂��̂��B");

                            UpdateMainMessage("�A�C���F�����A�����v���o�����̂͂���Ȏ�����˂��B");

                            UpdateMainMessage("�A�C���F���i�A���O�����ɍŏ��ɂ��ꂽ���B���ꂪ�A�t�F���g�D�[�V������H�B");

                            UpdateMainMessage("���i�F�E�E�E��������C�Â��Ă��̂�H");

                            UpdateMainMessage("�A�C���F�{�P�t�������f�B�X�ɏo���킵�������B");

                            UpdateMainMessage("���i�F�����������́B���ꂩ��́A�C�Â��Ȃ��U�肵�Ă��́H");

                            UpdateMainMessage("�A�C���F����A���������킯����˂��B���M���^���������Ă̂������ȏ����B");

                            UpdateMainMessage("�A�C���F���̌��́A�ǂ��݂Ă��P�Ȃ�i�}�N�����B���ێg���Ă݂Ă��S�R�З͂��o�Ȃ����ȁB");

                            UpdateMainMessage("���i�F�ӂ���B����ł��t������ɉ���Ă���ǂ��ς�����̂�H");

                            UpdateMainMessage("�A�C���F�t���͂ǂ������̌��̓����Ɋւ��āA����������m���Ă�݂����Ȃ񂾁B");

                            UpdateMainMessage("�A�C���F����A���̌��Ɋւ�炸�A�S�ʓI�Șb�݂����������B����������Ă��ꂽ�B");

                            UpdateMainMessage("�A�C���F�S�𓕂��ĕ����Ȃ��ƁA�U���͂͏o�Ȃ��B��������Șb�������B");

                            UpdateMainMessage("���i�F�S�𓕂��āE�E�E���Ď��́B");

                            UpdateMainMessage("�A�C���F���̌��A�ō��U���͂��ُ�ɍ����B�����āA�Œ�U���͂��ُ�ɒႢ�B");

                            UpdateMainMessage("�A�C���F�S�𓕂��Ȃ�����A�ō��U���͂͏o�Ȃ��B�܂�A�i�}�N���Ȃ܂܂��Ă킯���B");

                            UpdateMainMessage("�A�C���F���ꂪ�����������_�ŁA���̗͂ɑ΂���l���͕ς�����B");

                            UpdateMainMessage("�A�C���F���̏\�P�̂V�ԖځB���̌��t�ʂ�A�͕͂K�v�����A�͂�������ʖڂ����Ď����B");

                            UpdateMainMessage("���i�F�˂��A�A�C��");

                            UpdateMainMessage("�A�C���F��H");

                            UpdateMainMessage("���i�F�_���W�����A���̂܂ܐi�߂���H");

                            UpdateMainMessage("�A�C���F�E�E�E�����B���͂��̂܂ܐi�߂�B");

                            UpdateMainMessage("�A�C���F���͂ǂ����A���낢��ƖY��Ă��܂��Ă�悤���B");

                            UpdateMainMessage("�A�C���F������v���o���Ȃ���Ȃ�˂��B");

                            UpdateMainMessage("�A�C���F�_���W���������܂Ȃ��T������΁A�v���o���ׂ�����������B");

                            UpdateMainMessage("�A�C���F���̃_���W�����A�ǂ���牽�����̉�����������݂������B");

                            UpdateMainMessage("�A�C���F���͂���������Ă݂���B�K���ȁB");

                            UpdateMainMessage("���i�F�����B�������S�����������B");

                            UpdateMainMessage("���i�F�A�C���A�P�K���e�̂ق��A�撣���ė��Ă�ˁ�");

                            UpdateMainMessage("�A�C���F�����A�C���Ă����B�P�K���e�ł�����A�A�����邩��ȁB");

                            UpdateMainMessage("���i�F���񂾂��B�P�K���e�̎��́A����ǂ����莝���Ă��Ă��炤�����");

                            UpdateMainMessage("�A�C���F�}�W����B����v������E�E�E�A���i�l�ɂ��v�����ă��P����B");

                            UpdateMainMessage("���i�F���ӂӂӁA�E�\��E�\�B���܂��߂Ɏ󂯂�����Ă�̂��");

                            UpdateMainMessage("�A�C���F�܂��A�����ǂ����̂������玝���Ă����B");

                            UpdateMainMessage("�A�C���F���Ⴀ�A�P�K���e�I����Ă���Ƃ��邩�I");

                            UpdateMainMessage("���i�F�y���݂ɂ��Ă���B���Ⴀ�A�܂������ˁB");

                            UpdateMainMessage("�A�C���F�����A�܂��ȁB");

                            we.CompleteTruth1 = true;
                        }
                    }
                    else
                    {
                        UpdateMainMessage("�A�C���F�y�͂͗͂ɂ��炸�A�͂͑S�Ăł���B�z");

                        UpdateMainMessage("���i�F�����I�H");

                        UpdateMainMessage("�A�C���F���ꂩ��E�E�E�y�������Ȃ������B�@�������S�͖������B�z");

                        UpdateMainMessage("�A�C���F�Ō�́@�y�݂͂̂Ɉˑ�����ȁB�S��΂ɂ���B�z�@���������ȁB");

                        UpdateMainMessage("���i�F�E�\�I�H����Ȃ̂����Ɗo���Ă�́I�H");

                        UpdateMainMessage("�A�C���F�o���Ă�Ƃ������A�v���o�����B�����_���W�������ł���Ȍ��t���o�Ă����ȁB");

                        UpdateMainMessage("�A�C���F���i�̕ꂿ��񂪂���Ă������痥����B�������̏\�P�̈����B");

                        UpdateMainMessage("�A�C���F���͂Ƃ��ɂ��̂V�Ԗڂ��D�����������ȁB");

                        UpdateMainMessage("���i�F���͂悭������Ȃ����ǂˁA���������ނ̂����́B");

                        UpdateMainMessage("���i�F�A�C��������ƁA�A�C����������ԁB����ŏ\����ˁ�");

                        UpdateMainMessage("�A�C���F���O�̂����������͉��Ƃ�����˂��̂���E�E�E�ł��܂�");

                        UpdateMainMessage("�@�@�@�i�A�C���͂��ɂȂ��A�^���Ȋ፷���������n�߁E�E�E�j");

                        UpdateMainMessage("�A�C���F���̃_���W�����B�����ǂ߂����B");

                        UpdateMainMessage("���i�F���H");

                        UpdateMainMessage("�A�C���F����ɐi�񂾂�ʖڂȂ񂾁B���̃_���W�����B");

                        UpdateMainMessage("���i�F�ǂ������Ӗ���H");

                        UpdateMainMessage("�A�C���F���̌��t�Ŏv���o�������Ƃ�����B");

                        UpdateMainMessage("���i�F�����v���o�����́H");

                        UpdateMainMessage("�A�C���F�_���t�F���g�D�[�V���Ɋւ��Ă��B");

                        UpdateMainMessage("�@�@�@�i���i�͂ق�̈�u�����A������Ɉ�炵�Ă���E�E�E�j");

                        UpdateMainMessage("���i�F�t�F���g�D�[�V�����ǂ������̂�H");

                        UpdateMainMessage("�A�C���F�˂��h���ꂽ�ҁA�����ȗ͂ɂ�鎀���}����");

                        UpdateMainMessage("�A�C���F�q�[�����O���ʂ��K�p���ꂸ�A�h�����@�������Ȃ��B");

                        UpdateMainMessage("�A�C���F�܂��ɏ����ȗ͂��̂��̂��B");

                        UpdateMainMessage("�A�C���F�����A�����v���o�����̂͂���Ȏ�����˂��B");

                        UpdateMainMessage("�A�C���F���i�A���O�����ɍŏ��ɂ��ꂽ���B���ꂪ�A�t�F���g�D�[�V������H�B");

                        UpdateMainMessage("���i�F�E�E�E��������C�Â��Ă��̂�H");

                        UpdateMainMessage("�A�C���F�{�P�t�������f�B�X�ɏo���킵�������B");

                        UpdateMainMessage("���i�F�����������́B���ꂩ��́A�C�Â��Ȃ��U�肵�Ă��́H");

                        UpdateMainMessage("�A�C���F����A���������킯����˂��B���M���^���������Ă̂������ȏ����B");

                        UpdateMainMessage("�A�C���F���̌��́A�ǂ��݂Ă��P�Ȃ�i�}�N�����B���ێg���Ă݂Ă��S�R�З͂��o�Ȃ����ȁB");

                        UpdateMainMessage("���i�F�ӂ���B����ł��t������ɉ���Ă���ǂ��ς�����̂�H");

                        UpdateMainMessage("�A�C���F�t���͂ǂ������̌��̓����Ɋւ��āA�����������m���Ă�݂����Ȃ񂾁B");

                        UpdateMainMessage("�A�C���F����A���̌��Ɋւ�炸�A�S�ʓI�Șb�݂����������B����������Ă��ꂽ�B");

                        UpdateMainMessage("�A�C���F�S�𓕂��ĕ����Ȃ��ƁA�U���͂͏o�Ȃ��B��������Șb�������B");

                        UpdateMainMessage("���i�F�S�𓕂��āE�E�E���Ď��́B");

                        UpdateMainMessage("�A�C���F���̌��A�ō��U���͂��ُ�ɍ����B�����āA�Œ�U���͂��ُ�ɒႢ�B");

                        UpdateMainMessage("�A�C���F�S�𓕂��Ȃ�����A�ō��U���͂͏o�Ȃ��B�܂�A�i�}�N���Ȃ܂܂��Ă킯���B");

                        UpdateMainMessage("�A�C���F���ꂪ�����������_�ŁA���̗͂ɑ΂���l���͕ς�����B");

                        UpdateMainMessage("�A�C���F���̏\�P�̂V�ԖځB���̌��t�ʂ�A�͕͂K�v�����A�͂�������ʖڂ����Ď����B");

                        UpdateMainMessage("���i�F�˂��A�A�C��");

                        UpdateMainMessage("�A�C���F��H");

                        UpdateMainMessage("���i�F�_���W�����A���̂܂ܐi�߂���H");

                        UpdateMainMessage("�A�C���F�E�E�E�����B���͂��̂܂ܐi�߂�B");

                        UpdateMainMessage("�A�C���F���͂ǂ����A���낢��ƖY��Ă��܂��Ă�悤���B");

                        UpdateMainMessage("�A�C���F������v���o���Ȃ���Ȃ�˂��B");

                        UpdateMainMessage("�A�C���F�_���W���������܂Ȃ��T������΁A�v���o���ׂ�����������B");

                        UpdateMainMessage("�A�C���F���̃_���W�����A�ǂ���牽�����̉�����������݂������B");

                        UpdateMainMessage("�A�C���F���͂���������Ă݂���B�K���ȁB");

                        UpdateMainMessage("���i�F�����B�������S�����������B");

                        UpdateMainMessage("���i�F�A�C���A�P�K���e�̂ق��A�撣���ė��Ă�ˁ�");

                        UpdateMainMessage("�A�C���F�����A�C���Ă����B�P�K���e�ł�����A�A�����邩��ȁB");

                        UpdateMainMessage("���i�F���񂾂��B�P�K���e�̎��́A����ǂ����莝���Ă��Ă��炤�����");

                        UpdateMainMessage("�A�C���F�}�W����B����v������E�E�E�A���i�l�ɂ��v�����ă��P����B");

                        UpdateMainMessage("���i�F���ӂӂӁA�E�\��E�\�B���܂��߂Ɏ󂯂�����Ă�̂��");

                        UpdateMainMessage("�A�C���F�܂��A�����ǂ����̂������玝���Ă����B");

                        UpdateMainMessage("�A�C���F���Ⴀ�A�P�K���e�I����Ă���Ƃ��邩�I");

                        UpdateMainMessage("���i�F�y���݂ɂ��Ă���B���Ⴀ�A�܂������ˁB");

                        UpdateMainMessage("�A�C���F�����A�܂��ȁB");

                        we.CompleteTruth1 = true;
                        // �Œ胁���o�[�ŃX�g�[���P�{���ǂ����E�E�E�ǂ�����I�H
                    }

                    we.AlreadyCommunicate = true;
                    return;
                }
                #endregion
                #region "�Ŕu�n�܂�̒n�v�������Ƃ�"
                else if (this.firstDay >= 1 &&
                    we.BoardInfo10 &&
                    we.Truth_CommunicationJoinPartyLana == false &&
                    we.Truth_CommunicationNotJoinLana == false &&
                    we.AvailableSecondCharacter == false)
                {
                    UpdateMainMessage("�A�C���F���i�A���O�o�ׂ��炸�p���ĈӖ��m���Ă邩�H");

                    UpdateMainMessage("���i�F����A�ˑR����Ȃ��ƕ����āB");

                    UpdateMainMessage("���i�F�u�`���Ă͂����Ȃ��B�v���Ď��B�܂�A������Ⴂ���Ȃ����Ď�����Ȃ��́H");

                    UpdateMainMessage("�A�C���F�������E�E�E�ƂȂ�ƁE�E�E");

                    UpdateMainMessage("�A�C���F���₵�����E�E�E�ǂ������Ӗ����E�E�E");

                    UpdateMainMessage("���i�F������A�Ȃɍl��������Ă�̂�B�����ƌ����Ȃ�����ˁH");

                    UpdateMainMessage("�A�C���F�����A�����A�Ŕ��������񂾂�B");

                    UpdateMainMessage("�w�n�܂�̒n�A�����Ƃ��ׂ��炸�B�x�@���ĂȁB");

                    UpdateMainMessage("���i�F���t�ʂ肶��Ȃ��B�u�n�܂�̏ꏊ�������Ƃ��Ȃ��悤�ɂ��Ȃ����v���ĈӖ���B");

                    UpdateMainMessage("�A�C���F�悭�킩��˂��񂾂�ȁB���ꂪ�B");

                    UpdateMainMessage("���i�F�����Ђ�������ł�����킯�H");

                    UpdateMainMessage("�A�C���F����A���ɂ˂����ǂ��B");

                    UpdateMainMessage("���i�F���Ⴀ����H");

                    UpdateMainMessage("�A�C���F���[��A�Ȃ�Č����񂾁B���������݂͂ɂ����Ǝv���Ă��B");

                    UpdateMainMessage("���i�F�܂��P�K�̎n�߂Ȃ񂾂��A�`���҂ւ̍ŏ��̌x�����ď�����Ȃ��́H");

                    UpdateMainMessage("�A�C���F���[��E�E�E�Ȃ�Č����񂾁E�E�E");

                    UpdateMainMessage("���i�F�E�E�E�b�n�C�A�|�[�V�����ł��ǂ�����");

                    GetGreenPotionForLana();

                    UpdateMainMessage("�A�C���F�����I�H�����ȁA�킴�킴�B����͂����炾�H");

                    UpdateMainMessage("���i�F�ǂ����A����Ȃ́B�Ƃ��Ƃ��Ȃ�����B");

                    UpdateMainMessage("�A�C���F���₢��A���܂˂��ȁB�T���L���[�I");

                    if (GroundOne.WE2.TruthBadEnd1)
                    {
                        UpdateMainMessage("�A�C���F�E�E�E�Ȃ��A���i�B�Ƃ���Řb�͕ς��񂾂��B");

                        UpdateMainMessage("���i�F�Ȃɂ�H");

                        using (TruthDecision td = new TruthDecision())
                        {
                            td.MainMessage = "�@�y�@���i���p�[�e�B�ɗU���܂����H�@�z";
                            td.FirstMessage = "���i���p�[�e�B�ɗU���B";
                            td.SecondMessage = "���i���p�[�e�B�ɗU��Ȃ��B";
                            td.StartPosition = FormStartPosition.CenterParent;
                            td.ShowDialog();
                            if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                            {
                                UpdateMainMessage("�A�C���F�_���W�����A�ꏏ�ɗ��Ȃ����H");

                                UpdateMainMessage("���i�F���`��A�ǂ����悤���ȁB");

                                UpdateMainMessage("�A�C���F���O������ƃ|�[�V�����Ƃ��_���W�����}�b�v������ɂȂ邩��ȁB���ȁA���ނ��B");

                                UpdateMainMessage("�@�@�@�y���i�͈�u�A�A�C���ɂ͌����Ȃ��悤�ɁA����������悤�ȏΊ��������E�E�E�z");

                                UpdateMainMessage("���i�F�����������ˁB�����Ă��炦�邩�����");

                                UpdateMainMessage("�A�C���F�����A������H�����Ă݂��B");

                                UpdateMainMessage("���i�F�y�@�^���̓����@�z�@�@�T���Ă�ˁH");

                                UpdateMainMessage("�A�C���F���ȁB�������āH");

                                UpdateMainMessage("���i�F�b�t�t�A��k��B��k��@���Ⴀ�A��������͎����s������");

                                UpdateMainMessage("�A�C���F�T���L���[�I���ɂ��邺�I�I�@�b�n�b�n�b�n�I");

                                if (we.AvailablePotionshop)
                                {
                                    UpdateMainMessage("�A�C���F���ƁA���������΃��i�B���O�̂��X�͂ǂ�����񂾂�H");

                                    UpdateMainMessage("���i�F���S�z�Ȃ���@�����ƌق��Ă����������");

                                    UpdateMainMessage("�A�C���F�b�}�W����I�H���ł���ȗp�ӎ����Ȃ񂾂�I�H");

                                    UpdateMainMessage("���i�F�܂��A���A�ڋq�͂���܂�����ĂȂ��̂�ˁB������ꂿ�Ⴄ���B");

                                    UpdateMainMessage("���i�F����Ȃ킯������A�S�z�����p���");
                                }

                                UpdateMainMessage("�A�C���F���Ⴀ�A���������낵�����ނ��I�I");

                                UpdateMainMessage("���i�F�n�C�n�C��@���Ⴀ�܂������ˁB");

                                CallSomeMessageWithAnimation("�y���i���p�[�e�B�ɉ����܂����B�z");

                                we.AvailableSecondCharacter = true;
                                we.Truth_CommunicationJoinPartyLana = true;
                            }
                            else
                            {
                                UpdateMainMessage("�A�C���F���A���₢��A���ł��˂��B");

                                UpdateMainMessage("���i�F�A�C���A�炵���Ȃ���ˁB�����������͐����Ɍ����Ă�ˁH");

                                UpdateMainMessage("�A�C���F�E�E�E�����A���������Ƃ��ȁB");

                                UpdateMainMessage("�A�C���F���i�A���O���p�[�e�B�ɗU�����Ǝv�����񂾂��B");

                                UpdateMainMessage("�A�C���F��߂��B����l�ōs���Ă݂��邺�B");

                                UpdateMainMessage("�A�C���F���܂˂��ȁB���i�B");

                                UpdateMainMessage("���i�F���`��A�ʂɗǂ����B�����Ƃ��������Ă����΁B");

                                UpdateMainMessage("�A�C���F���Ⴀ�A�܂���������_���W���������ė��邺�B");

                                UpdateMainMessage("���i�F�n�C�n�C�A�撣���Ă��Ă�ˁ�");

                                CallSomeMessageWithAnimation("�y���i���p�[�e�B�ɉ����܂���ł����B�z");

                                we.Truth_CommunicationNotJoinLana = true;
                            }
                        }
                    }
                    else
                    {
                        UpdateMainMessage("�A�C���F�E�E�E�Ȃ��A���i�B�|�[�V������������g�R�����񂾂��B");

                        UpdateMainMessage("���i�F�Ȃɂ�H");

                        UpdateMainMessage("�A�C���F�_���W�����A�ꏏ�ɗ��Ȃ����H");

                        UpdateMainMessage("���i�F���`��A�ǂ����悤���ȁB");

                        UpdateMainMessage("�A�C���F���O������ƃ|�[�V�����Ƃ��_���W�����}�b�v������ɂȂ邩��ȁB���ȁA���ނ��B");

                        UpdateMainMessage("�@�@�@�y���i�͈�u�A�A�C���ɂ͌����Ȃ��悤�ɁA����������悤�ȏΊ��������E�E�E�z");

                        UpdateMainMessage("���i�F�����������ˁB�����Ă��炦�邩�����");

                        UpdateMainMessage("�A�C���F�����A������H�����Ă݂��B");

                        UpdateMainMessage("���i�F�y�@�^���̓����@�z�@�@�T���Ă�ˁH");

                        UpdateMainMessage("�A�C���F���ȁB�������āH");

                        UpdateMainMessage("���i�F�b�t�t�A��k��B��k��@���Ⴀ�A��������͎����s������");

                        UpdateMainMessage("�A�C���F�T���L���[�I���ɂ��邺�I�I�@�b�n�b�n�b�n�I");

                        if (we.AvailablePotionshop)
                        {
                            UpdateMainMessage("�A�C���F���ƁA���������΃��i�B���O�̂��X�͂ǂ�����񂾂�H");

                            UpdateMainMessage("���i�F���S�z�Ȃ���@�����ƌق��Ă����������");

                            UpdateMainMessage("�A�C���F�b�}�W����I�H���ł���ȗp�ӎ����Ȃ񂾂�I�H");

                            UpdateMainMessage("���i�F�܂��A���A�ڋq�͂���܂�����ĂȂ��̂�ˁB������ꂿ�Ⴄ���B");

                            UpdateMainMessage("���i�F����Ȃ킯������A�S�z�����p���");
                        }

                        UpdateMainMessage("�A�C���F���Ⴀ�A���������낵�����ނ��I�I");

                        UpdateMainMessage("���i�F�n�C�n�C��@���Ⴀ�܂������ˁB");

                        CallSomeMessageWithAnimation("�y���i���p�[�e�B�ɉ����܂����B�z");

                        we.AvailableSecondCharacter = true;
                        we.Truth_CommunicationJoinPartyLana = true;
                    }
                    return;
                }
                #endregion
                #region "�Ŕu�ߓ��댯�v�������Ƃ�"
                //else if (this.firstDay >= 1 && we.BoardInfo11 &&
                //        we.Truth_CommunicationJoinPartyLana == false && we.AvailableSecondCharacter == false)
                //{
                //    UpdateMainMessage("�A�C���F�ߓ��ɂ͊댯�����ށE�E�E����Ⴛ�����낤�ȁE�E�E");

                //    UpdateMainMessage("���i�F�ǂ������̂�H����炵�Ĉ�l�Ńu�c�u�c");

                //    UpdateMainMessage("�A�C���F�����A���i���B���傤�Ǘǂ������B");

                //    UpdateMainMessage("�A�C���F���i�A�ꏏ�Ƀ_���W�������˂����H");

                //    UpdateMainMessage("���i�F�E�E�E���H");

                //    UpdateMainMessage("�A�C���F�ǂ����A���̃_���W�����͋ߓ������݂���炵���񂾁B");

                //    UpdateMainMessage("���i�F������A������Ƒ҂��Ă�B");

                //    UpdateMainMessage("�A�C���F�E�E�E�Ȃ񂾁H");

                //    UpdateMainMessage("���i�F���˂�ˁB�����Ȃ�ǂ�����������킯�H");

                //    UpdateMainMessage("�A�C���F����A�ʂɐ[���o�܂͂˂����E�E�E");

                //    UpdateMainMessage("�A�C���F�Ȃ񂾁A�ʖڂȂ̂��H");

                //    UpdateMainMessage("���i�F���`��A���������킯����Ȃ����ǁE�E�E");

                //    UpdateMainMessage("�A�C���F�s�������A���i�B�Q�l�ōs���̂Ƀf�����b�g�͖�������B");

                //    UpdateMainMessage("���i�F�܂����������ǂˁB���A�{���ɍs���Ă��ǂ��́H");

                //    // [�^�G���f�B���O����]

                //    UpdateMainMessage("�A�C���F�����A���R���B���O������Ɨ���ɂȂ邩��ȁB���ȁA���ނ��B");

                //    UpdateMainMessage("�@�@�@�y���i�͈�u�A�A�C���ɂ͌����Ȃ��悤�ɁA����������悤�ȏΊ��������E�E�E�z");

                //    UpdateMainMessage("���i�F�����������ˁB�����Ă��炦�邩�����");

                //    UpdateMainMessage("�A�C���F�����A������H�����Ă݂��B");

                //    UpdateMainMessage("���i�F�y�@�^���̓����@�z�@�@�T���Ă�ˁH");

                //    UpdateMainMessage("�A�C���F���ȁB�������āH");

                //    UpdateMainMessage("���i�F�b�t�t�A��k��B��k��@���Ⴀ�A��������͎����s������");

                //    UpdateMainMessage("�A�C���F�T���L���[�I���ɂ��邺�I�I�@�b�n�b�n�b�n�I");

                //    if (we.AvailablePotionshop)
                //    {
                //        UpdateMainMessage("�A�C���F���ƁA���������΃��i�B���O�̂��X�͂ǂ�����񂾂�H");

                //        UpdateMainMessage("���i�F���S�z�Ȃ���@�����ƌق��Ă����������");

                //        UpdateMainMessage("�A�C���F�b�}�W����I�H���ł���ȗp�ӎ����Ȃ񂾂�I�H");

                //        UpdateMainMessage("���i�F�܂��A���A�ڋq�͂���܂�����ĂȂ��̂�ˁB������ꂿ�Ⴄ���B");

                //        UpdateMainMessage("���i�F����Ȃ킯������A�S�z�����p���");
                //    }

                //    UpdateMainMessage("�A�C���F���Ⴀ�A���������낵�����ނ��I�I");

                //    UpdateMainMessage("���i�F�n�C�n�C��@���Ⴀ�܂������ˁB");

                //    CallSomeMessageWithAnimation("�y���i���p�[�e�B�ɉ����܂����B�z");

                //    we.AvailableSecondCharacter = true;
                //    we.Truth_CommunicationJoinPartyLana = true;
                //}
                #endregion
                #region "�ŔR������O�ł��A��L�Ԃɓ��B������"
                if ((we.dungeonEvent11KeyOpen || we.dungeonEvent12KeyOpen || we.dungeonEvent13KeyOpen || we.dungeonEvent14KeyOpen) &&
                    we.Truth_CommunicationJoinPartyLana == false && we.AvailableSecondCharacter == false)
                {
                    UpdateMainMessage("�A�C���F�ӂ��A�߂��Ă����̂͗ǂ����E�E�E");

                    UpdateMainMessage("�A�C���F���̑�L�ԁA�����炯�ŕ����������񂶂�˂��ȁB");

                    UpdateMainMessage("�A�C���F�������E�E�E����Ȏ��Ƀ��i�̃_���W�����}�b�v������΂ȁE�E�E");

                    UpdateMainMessage("���i�F���̃_���W�����}�b�v���ǂ��������́H");

                    UpdateMainMessage("�A�C���F�����I�H�@�r�b�N�����邶��˂����I");

                    UpdateMainMessage("���i�F������ȃr�r���Ă�킯�H�܂����A�܂��B��������Ȃ��ł��傤�ˁ�");

                    UpdateMainMessage("�A�C���F���A���₢��B�B���Ă�킯����˂��B");

                    UpdateMainMessage("�A�C���F�ł��܂��A����Ȃ킯���B�C�ɂ���ȁI�b�n�b�n�b�n�I");

                    UpdateMainMessage("���i�F�ӂ���E�E�E������A�������������Ł�");

                    UpdateMainMessage("�A�C���F���ȁI���������Ȃ񂾂�I�H");

                    UpdateMainMessage("���i�F�_���W�����}�b�v���ǂ��Ƃ��A�����Ă�����Ȃ��H�����ƌ����Ȃ�����ˁB");

                    UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                    UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E�@�܂��A�A�����B");

                    UpdateMainMessage("�@�@�@�w�b�V���S�I�I�H�H�I�I�x�i���i�̃G�������^���L�b�N���A�C���̓��̂��y��j�@�@");

                    UpdateMainMessage("�A�C���F���A�����������āB�҂đ҂āB");

                    UpdateMainMessage("�A�C���F�_���W������i�߂Ă�r���A�傫�ȑ�L�Ԃɏo���킵���񂾁B");

                    UpdateMainMessage("���i�F�ւ��A����ȏ�������񂾁B���ŁA�ǂ��������킯�H");

                    UpdateMainMessage("�A�C���F��L�Ԃɂ͊���̔�������񂾂��A���ꂪ�قƂ�ǌ��t���΂���B");

                    UpdateMainMessage("�A�C���F�����炭�A�Ⴄ���؂����ǂ��Ă���΁A�J����Ƃ͎v���񂾂��E�E�E");

                    UpdateMainMessage("�A�C���F�����A�}�b�v���悭�킩��˂��B");

                    UpdateMainMessage("�A�C���F�ȒP�Ɍ����΁A�}�b�v���悭�킩��˂��B");

                    UpdateMainMessage("�A�C���F���_�Ƃ��āA�}�b�v���悭�킩��˂��B");

                    UpdateMainMessage("���i�F�Ȃ�قǁ�@���A�ǂ����v�������������");

                    UpdateMainMessage("�A�C���F���i�A�ʂɂ��O�ɗ��Ă���Ƃ͌����Ă˂��B");

                    UpdateMainMessage("�A�C���F�}�b�v�������Ă��ꂳ������Ηǂ��񂾁B");

                    UpdateMainMessage("���i�F�ł��A�_���W�����Ɉꏏ�ɍs���Ȃ��ƃ}�b�v�͏����Ȃ��ł���H");

                    UpdateMainMessage("�A�C���F������A���Ȃ��Ă��ǂ��B�����g�����V�[�o�[�Ń��i�ƒʐM���s���B");

                    UpdateMainMessage("�A�C���F�w������A�C���B�������܍��W�|�C���g�w�Q�Q�C�R�R�x");

                    UpdateMainMessage("�A�C���F�i���}�l�j�w�����烉�i�B�������@�}�b�v�X�V���Ƃ�����x");

                    UpdateMainMessage("�A�C���F�w������A�C���B�������܍��W�|�C���g�w�R�S�C�Q�Q�x�@�󔠂𔭌��I");

                    UpdateMainMessage("�A�C���F�i���}�l�j�w�����烉�i�B�������@�}�b�v�X�V���Ƃ�����x");

                    UpdateMainMessage("�A�C���F�w������A�C���E�E�E�������܁E�E�E�x");

                    UpdateMainMessage("���i�F�E�E�E�ςȗ����o���Ȃ��ł�ˁB����A�S�R���ĂȂ�����B");

                    UpdateMainMessage("���i�F�����������ł���Ȗʓ|�ȒʐM��Ƃ��Ȃ���s���Ȃ��̂�B");

                    UpdateMainMessage("�A�C���F���ق�A�������������ɂ��_���W�����T����i�߂Ă���Ċ��������邾��H");

                    UpdateMainMessage("���i�F���������g�����V�[�o�[�Ȃ�Ă��́A�_���W�����Ŏg����킯�����ł���H");

                    UpdateMainMessage("�A�C���F�b�O�I�@���΁A����Ȕn���ȁI�I");

                    UpdateMainMessage("���i�F�͂������E�E�E�E���ł���ȃo�J�b���Ă�̂�����E�E�E���A�s����ˁB");

                    // [�^�G���f�B���O����]

                    UpdateMainMessage("�A�C���F�܁A�҂đ҂āI���k���A���i�I");

                    UpdateMainMessage("�A�C���F�ꏏ�Ƀ_���W�����s���˂����H");

                    UpdateMainMessage("�@�@�@�y���i�͈�u�A�A�C���ɂ͌����Ȃ��悤�ɁA����������悤�ȏΊ��������E�E�E�z");

                    UpdateMainMessage("���i�F�����������ˁB�����Ă��炦�邩�����");

                    UpdateMainMessage("�A�C���F�����A������H�����Ă݂��B");

                    UpdateMainMessage("���i�F�y�@�^���̓����@�z�@�@�T���Ă�ˁH");

                    UpdateMainMessage("�A�C���F���ȁB�������āH");

                    UpdateMainMessage("���i�F�b�t�t�A��k��B��k��@���Ⴀ�A��������͎����s������");

                    UpdateMainMessage("�A�C���F�T���L���[�I���ɂ��邺�I�I�@�b�n�b�n�b�n�I");

                    if (we.AvailablePotionshop)
                    {
                        UpdateMainMessage("�A�C���F���ƁA���������΃��i�B���O�̂��X�͂ǂ�����񂾂�H");

                        UpdateMainMessage("���i�F���S�z�Ȃ���@�����ƌق��Ă����������");

                        UpdateMainMessage("�A�C���F�b�}�W����I�H���ł���ȗp�ӎ����Ȃ񂾂�I�H");

                        UpdateMainMessage("���i�F�܂��A���A�ڋq�͂���܂�����ĂȂ��̂�ˁB������ꂿ�Ⴄ���B");

                        UpdateMainMessage("���i�F����Ȃ킯������A�S�z�����p���");
                    }

                    UpdateMainMessage("�A�C���F���Ⴀ�A���������낵�����ނ��I�I");

                    UpdateMainMessage("���i�F�n�C�n�C��@���Ⴀ�܂������ˁB");

                    CallSomeMessageWithAnimation("�y���i���p�[�e�B�ɉ����܂����B�z");

                    we.AvailableSecondCharacter = true;
                    we.Truth_CommunicationJoinPartyLana = true;
                }
                #endregion
                #region "�Ŕu�����o�[�\���ŕω��v�������Ƃ�"
                else if ((we.BoardInfo13) && we.Truth_CommunicationJoinPartyLana == false && we.AvailableSecondCharacter == false)
                {
                    UpdateMainMessage("�A�C���F�p�[�e�B�ɂ���ă����o�[�\���́E�E�E�ω�����E�E�E");

                    UpdateMainMessage("���i�F�ǂ������̂�H����炵�Ĉ�l�Ńu�c�u�c");

                    UpdateMainMessage("�A�C���F�����A���i���B���傤�Ǘǂ������B");

                    UpdateMainMessage("�A�C���F���i�A�ꏏ�Ƀ_���W�������˂����H");

                    UpdateMainMessage("���i�F�E�E�E���H");

                    UpdateMainMessage("�A�C���F�ǂ����A���̃_���W�����̓����o�[�\���ɂ���ĕω�����炵���񂾁B");

                    UpdateMainMessage("���i�F������A������Ƒ҂��Ă�B");

                    UpdateMainMessage("�A�C���F�E�E�E�Ȃ񂾁H");

                    UpdateMainMessage("���i�F���˂�ˁB�����Ȃ�ǂ�����������킯�H");

                    UpdateMainMessage("�A�C���F����A�ʂɐ[���o�܂͂˂����E�E�E");

                    UpdateMainMessage("�A�C���F�Ȃ񂾁A�ʖڂȂ̂��H");

                    UpdateMainMessage("���i�F���`��A���������킯����Ȃ����ǁE�E�E");

                    UpdateMainMessage("�A�C���F�s�������A���i�B�Q�l�ōs���̂Ƀf�����b�g�͖�������B");

                    UpdateMainMessage("���i�F�܂����������ǂˁB���A�{���ɍs���Ă��ǂ��́H");

                    // [�^�G���f�B���O����]

                    UpdateMainMessage("�A�C���F�����A���R���B���O������Ɨ���ɂȂ邩��ȁB���ȁA���ނ��B");

                    UpdateMainMessage("�@�@�@�y���i�͈�u�A�A�C���ɂ͌����Ȃ��悤�ɁA����������悤�ȏΊ��������E�E�E�z");

                    UpdateMainMessage("���i�F�����������ˁB�����Ă��炦�邩�����");

                    UpdateMainMessage("�A�C���F�����A������H�����Ă݂��B");

                    UpdateMainMessage("���i�F�y�@�^���̓����@�z�@�@�T���Ă�ˁH");

                    UpdateMainMessage("�A�C���F���ȁB�������āH");

                    UpdateMainMessage("���i�F�b�t�t�A��k��B��k��@���Ⴀ�A��������͎����s������");

                    UpdateMainMessage("�A�C���F�T���L���[�I���ɂ��邺�I�I�@�b�n�b�n�b�n�I");

                    if (we.AvailablePotionshop)
                    {
                        UpdateMainMessage("�A�C���F���ƁA���������΃��i�B���O�̂��X�͂ǂ�����񂾂�H");

                        UpdateMainMessage("���i�F���S�z�Ȃ���@�����ƌق��Ă����������");

                        UpdateMainMessage("�A�C���F�b�}�W����I�H���ł���ȗp�ӎ����Ȃ񂾂�I�H");

                        UpdateMainMessage("���i�F�܂��A���A�ڋq�͂���܂�����ĂȂ��̂�ˁB������ꂿ�Ⴄ���B");

                        UpdateMainMessage("���i�F����Ȃ킯������A�S�z�����p���");
                    }

                    UpdateMainMessage("�A�C���F���Ⴀ�A���������낵�����ނ��I�I");

                    UpdateMainMessage("���i�F�n�C�n�C��@���Ⴀ�܂������ˁB");

                    CallSomeMessageWithAnimation("�y���i���p�[�e�B�ɉ����܂����B�z");

                    we.AvailableSecondCharacter = true;
                    we.Truth_CommunicationJoinPartyLana = true;

                }
                #endregion
                #region "DUEL���Z��J��"
                else if (this.firstDay >= 3 && !we.AvailableDuelColosseum)
                {
                    UpdateMainMessage("���i�F�����A�A�C���B����ȏ��ɋ����̂ˁB");

                    UpdateMainMessage("�A�C���F�悤���i�A���̂悤���H");

                    UpdateMainMessage("���i�F�A�C����DUEL���Z��ɂ͎Q�����Ȃ��́H");

                    UpdateMainMessage("�A�C���FDUEL���Z���A����܎Q�����悤���Ďv�������͂˂��ȁB");

                    UpdateMainMessage("���i�F�Ӂ`��A�����Ȃ́H");

                    UpdateMainMessage("�A�C���F�A���͂Ȃ�����񂾁BDUEL����H");

                    UpdateMainMessage("���i�F������B");

                    UpdateMainMessage("�A�C���FDUEL��������ADUEL����H");

                    UpdateMainMessage("�@�@�@�w�b�o�O�V�I�x�i���i�̃G�������^���L�b�N���y��j�@�@");

                    UpdateMainMessage("�A�C���F�����������E�E�E�b�O�A�����������������I");

                    UpdateMainMessage("�A�C���F���ŁA���ɏo��Ƃł����������̂��H");

                    UpdateMainMessage("���i�F���ŐϋɓI�ɏo������Ȃ��̂��𕷂��Ă�̂�B");

                    UpdateMainMessage("�A�C���F�������ȁBDUEL���Ă̂͂�����^���������B");

                    UpdateMainMessage("���i�F�_���W�����s���Ă鎞�͐^����������Ȃ����P�H");

                    UpdateMainMessage("�A�C���F�ʂɂ����͌����Ă˂��B�����ADUEL�Ƃ͂܂������ʂ��B");

                    UpdateMainMessage("�A�C���F�_���W�����̃����X�^�[�͓K���ɂԂ��ׂ��Ηǂ���������H");

                    UpdateMainMessage("�A�C���F�����ADUEL�͖��炩�ɑ���̓����X�^�[����˂��B�l�Ԃ��B");

                    UpdateMainMessage("�A�C���F�K���ɂ����炤�̂��Ȃ񂾂��A�}�W�łԂ��ׂ��̂��Ȃ񂾁B");

                    UpdateMainMessage("�A�C���F�^���ɖʂƌ������Ă���Ă��Ȃ���\���󂪗����˂�����B");

                    UpdateMainMessage("���i�F���`��E�E�E�������悭������Ȃ���ˁB");

                    UpdateMainMessage("���i�F����ς�A��x�����ƎQ�����Ă݂�΁H");

                    UpdateMainMessage("�A�C���F�܂������ȁE�E�E�ǂ��������ȁE�E�E");

                    UpdateMainMessage("�A�C���F��A����������B����ł���邩�H");

                    UpdateMainMessage("���i�F�����H�\���A���ɑ΂��Č����Ă�́H");

                    UpdateMainMessage("�A�C���F�����A�������B");

                    UpdateMainMessage("���i�F�����A����Ȃ̓��e�����B���Ⴀ�A�����Ă݂Ȃ�����B");

                    UpdateMainMessage("�A�C���F��������������Ƃ��ADUEL�O��ł͏o������艴�̎��͂��痣��Ă���B�ǂ��ȁH");

                    UpdateMainMessage("���i�F�����H�@���悻��H");

                    UpdateMainMessage("�A�C���F���̏����A����ł�����DUEL�ɎQ�����Ă݂邺�B�ǂ����H");

                    UpdateMainMessage("���i�F���`��A�A�C�����Ă��B���܂ɗǂ�������Ȃ���������ˁE�E�E");

                    UpdateMainMessage("���i�F�܂��A�ł�����ȓ��e��������B�������");

                    UpdateMainMessage("�A�C���F������I���܂肾�I�I�r���邺�I�I�I");

                    UpdateMainMessage("�A�C���F�\�����݂Ƃ��̓o�^�\���A��������Ă���Ƃ��邩�I�b�n�b�n�b�n�I�I");

                    CallSomeMessageWithAnimation("�A�C����DUEL���Z��ւƌ������Ă������B");

                    UpdateMainMessage("���i�F�i�A�C���E�E�E����Ȋ��������ɁA�͂��Ⴂ�ŁE�E�E�j");

                    UpdateMainMessage("���i�F�i�E�E�E�@�E�E�E�j");

                    buttonDuel.Visible = true;

                    CallSomeMessageWithAnimation("�yDUEL���Z��֍s�������o����悤�ɂȂ�܂����B�z");

                    GroundOne.StopDungeonMusic();
                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                    CallSomeMessageWithAnimation("�|�|�|�@DUEL���Z��ɂā@�|�|�|");

                    UpdateMainMessage("�A�C���F�����I�������������ȁI�I");

                    UpdateMainMessage("���i�F���傤�Ǒΐ킪�n�܂������Ȃ񂶂�Ȃ��H");

                    UpdateMainMessage("�A�C���F�����A�����݂������ȁB������ƌ��Ă������H");

                    UpdateMainMessage("���i�F���`��A���͗ǂ���A�������Ƃ��B�A�C���o�^�\���ɗ����񂶂�Ȃ��́H");

                    UpdateMainMessage("�A�C���F�������ƁI�����������A�Y��Ă����I�I");

                    UpdateMainMessage("�A�C���F������A�����󂯕t���ɂł��s���Ă݂�Ƃ��邩�B");

                    UpdateMainMessage("�@�@�y��t��F�悤�����ADUEL���Z��ցB�z");

                    UpdateMainMessage("�A�C���F���Ƃ��ȁADUEL�Q���\�����݂��������񂾂��B");

                    UpdateMainMessage("�@�@�y��t��FDUEL�\���ł�����A������o�^�V�[�g�ɋL�������肢���܂��B�z");

                    UpdateMainMessage("�A�C���F�w���O�x���ƁB���悵�E�E�EEin�E�E�EWolence�E�E���ƁB");

                    UpdateMainMessage("�A�C���F�w���݂܂ł�DUEL�\�����݉񐔁x�E�E�E�m���A�R�V�[�Y�����ƁB");

                    UpdateMainMessage("�A�C���F�w���p�x�H���������B�B�B");

                    UpdateMainMessage("�A�C���F�u�A�^�b�N�I�I�v");

                    UpdateMainMessage("���i�F������ƁA�A�C���B�u�A�^�b�N�v�Ȃ�Đ�p�ł����ł��Ȃ����H");

                    UpdateMainMessage("�A�C���F�ǂ�����˂����B�e�L�g�[�ŗǂ��񂾂�A����Ȃ���́B");

                    UpdateMainMessage("�A�C���F�w���@�K���x�x�H�E�E�E�������ȁA�u�P�O�O���v���ƁB�B�B");

                    UpdateMainMessage("���i�F�W�B�`�`�E�E�E");

                    UpdateMainMessage("�A�C���F�킩�����A�����������āB�u�R�O���v���ƁB�B�B");

                    UpdateMainMessage("�A�C���F�w�񓁗��ہx�H�E�E�E����܂蓾�ӂ���˂����A�ꉞ�w�x���ƁB�B�B");

                    UpdateMainMessage("�A�C���F�w�X�^�b�N�L�����Z���ہx�H�E�E�E�܂��w�x�����ȁB");

                    UpdateMainMessage("���i�F���悻��H");

                    UpdateMainMessage("�A�C���F��H�����A���x�܂������Ă���B���X���ƁE�E�E�w���C�o���x");

                    UpdateMainMessage("�A�C���F�E�E�E�������ȁw�I���E�����f�B�X�x���ƁE�E�E");

                    UpdateMainMessage("���i�F�����f�B�X���t������̖��O����Ȃ��B�����Ă������킯�H");

                    UpdateMainMessage("�A�C���F���v����B�P�Ȃ�A���P�[�g�݂����Ȃ��񂾂낤���B���ӂ��A�Ōォ�B");

                    UpdateMainMessage("�A�C���F�w�D��������H�x�B�B�B�������Ȃ��E�E�E");

                    UpdateMainMessage("�u�b�n�b�n�b�n�I�I�I�v���Ƃ���Ȃ���");

                    UpdateMainMessage("���i�F�z���b�g�A������邮�炢�e�L�g�[��ˁB");

                    UpdateMainMessage("�A�C���F�܂��܂��A�ǂ�����˂����B�悵�A�z����B����őS���L���������B");

                    UpdateMainMessage("�@�@�y��t��F�o�^�V�[�g���󂯕t���܂����B�z");

                    UpdateMainMessage("�@�@�y��t��F�f�[�^�x�[�X�ɏƍ��E�K�p�����{���܂��B�z");

                    UpdateMainMessage("�@�@�y��t��F�ƍ����茋�ʂ͖����ƂȂ�܂��̂ŁA��������ΐ�o�^�\�ɐ����G���g���[����܂��B�z");

                    UpdateMainMessage("�@�@�y��t��F�ΐ푊��͑Ώۂ̘r��͗ʂɉ����Ė{���Z���莩���I�Ƀs�b�N�A�b�v�������܂��B�z");

                    UpdateMainMessage("�@�@�y��t��F�s�b�N�A�b�v���ꂽ���X�g���̑���Ƒΐ���s���Ă��������B�z");

                    UpdateMainMessage("�@�@�y��t��F�ΐ�͌����Ƃ��āA�L�����Z���E���ۂ͍s���܂���B�K���{���Z��ŋ����Ă��������܂��B�z");

                    UpdateMainMessage("�A�C���F����ł�����≴��������苑�ۂ�����ǂ��Ȃ�񂾁H");

                    UpdateMainMessage("�@�@�y��t��F�K���ΐ푊��Ƃc�t�d�k�����悤�蔤�𐮂��܂��B�z");

                    UpdateMainMessage("�A�C���F����ł����肪�f������ǂ��Ȃ�񂾁H");

                    UpdateMainMessage("�@�@�y��t��F�K���ΐ푊��Ƃc�t�d�k�����悤�蔤�𐮂��܂��B�z");

                    UpdateMainMessage("�A�C���F�}�W����E�E�E�܂��������B���ɏڍ׃��[���͂���̂��H");

                    UpdateMainMessage("�@�@�y��t��F�ڂ����c�t�d�k���[���Ɋւ��ẮA�f�[�^�x�[�X�K�p���I��莟�您�`���������܂��B�z");

                    UpdateMainMessage("�@�@�y��t��F�ȏ�ƂȂ�܂��B�����̘A�������҂����������B�z");

                    UpdateMainMessage("�A�C���F�����A���낢��Ƃ��肪�ƂȁB�T���L���[�I");

                    UpdateMainMessage("�A�C���F�����͓o�^�܂ł��B�܂������͖������Ď��ŁB�������A���i�B");

                    UpdateMainMessage("���i�F����H");

                    UpdateMainMessage("�A�C���F���i�A���O���Q�����Ă݂Ȃ����H");

                    UpdateMainMessage("���i�F�����I�H���I�H�@�C�C��悻��Ȃ́B�ǂ��������������Ⴄ���B");

                    UpdateMainMessage("�A�C���F�������Ă񂾁B���̖����߂ȃ��C�g�j���O�L�b�N�Ȃ��T�̑���͂��̏�ŉʂĂ邼�H");

                    UpdateMainMessage("���i�F���A�����A�����Ȃ�m��Ȃ��l�ɑ΂��āA����ȃL�b�N���܂����Ȃ����B");

                    UpdateMainMessage("�A�C���F��܂��A�������B������I���������DUEL���撣���Ă���Ƃ��邩�I�I");

                    UpdateMainMessage("���i�F�撣���ė��Ă�ˁB���҂��Ă����");

                    UpdateMainMessage("�A�C���F�����A���̃N�\�t���ɂ������������Ă݂��邺�I�C���Ă������āI�b�n�b�n�b�n�I�I");

                    UpdateMainMessage("���i�F���A��f�ނ̏W�߂Ƃ����邩��A���Ⴀ�܂���Ł�");

                    UpdateMainMessage("�A�C���F�����A�܂��ȁB");

                    CallSomeMessageWithAnimation("���i�͒��̒��ւƕ����Ă������E�E�E");

                    UpdateMainMessage("�A�C���F�i�_���W�����Ƃ�����A�c�t�d�k���B�B�B�j");

                    UpdateMainMessage("�A�C���F�i�c�t�d�k�E�E�E������������������ȁB�B�B�j");

                    UpdateMainMessage("�A�C���F�i������A����������撣���čs���Ƃ��邩�I�j", true);

                    GroundOne.StopDungeonMusic();
                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
                    we.AvailableDuelColosseum = true;
                }
                #endregion
                #region "DUEL���Z��ADUEL�J�n"
                else if (this.firstDay >= 4 && !we.AvailableDuelMatch)
                {
                    UpdateMainMessage("�@�@�y��t��F�A�C���l�A���҂����Ă���܂����B�z");

                    UpdateMainMessage("�A�C���F�您�A���̎��̎�t���񂶂�Ȃ����I�o�^�\���͂ǂ��Ȃ����H");

                    UpdateMainMessage("�@�@�y��t��F�A�C���l�̓o�^�\���̓f�[�^�x�[�X�ւƏƍ�����A�����ɏ�������܂����B�z");

                    UpdateMainMessage("�A�C���F��������I�킴�킴�����ɗ��Ă���ăT���L���[�I");

                    UpdateMainMessage("�@�@�y��t��F�{������A�A�C���l�͂c�t�d�k���Z��ł̑ΐ�҃��X�g�ɓo�^���ꂽ���ƂȂ�܂��B�z");

                    UpdateMainMessage("�@�@�y��t��F�߁X�\�肳��Ă���ΐ푊�胊�X�g���m�F�������ꍇ�́A�c�t�d�k���Z��܂ł��z�����������B�z");

                    UpdateMainMessage("�A�C���F�����A��ōs���Ă݂�Ƃ����B���肪�ƂȁI");

                    UpdateMainMessage("�@�@�y��t��F�Ȃ��A�A�C���l���u���C�o���v���ɃI���E�����f�B�X���L�ڂ���Ă������߁z");

                    UpdateMainMessage("�A�C���F���H�����E�E�E�m���ɏ��������E�E�E");

                    UpdateMainMessage("�@�@�y��t��F�{���Z��̃g�b�v�����J�[�A�I���l���ꌾ���`�����Ă����������e������Ƃ̎��ł��z");

                    UpdateMainMessage("�A�C���F�b�Q�I�I�I�@�}�W����I�H�I�H");

                    UpdateMainMessage("�@�@�y��t��F����ł́A�{���Z��ւƂ��ЂƂ����z�����������B���͂���ɂāB�z");

                    CallSomeMessageWithAnimation("��t�W���͓��Z��ւƖ߂��Ă������E�E�E");

                    UpdateMainMessage("�A�C���F�b�O�E�E�E���A���x�F�E�E�E");

                    UpdateMainMessage("�A�C���F�b�N�\�A�������Ă����Ȃ藈�Ă񂾂�B�B�B");

                    UpdateMainMessage("�A�C���F�����Ă��E�E�E�����炭���ʂ��낤�ȁB");

                    UpdateMainMessage("�A�C���F�����͓��Z��֍s�������Ȃ����B", true);

                    we.AvailableDuelMatch = true;
                }
                #endregion
                // �_���W��������A�Ҍ�A�K�{�C�x���g��������΁A�ȉ��C�ӃC�x���g
                #region "ESC���j���[�F�o�g���ݒ�"
                else if (!we.AvailableBattleSettingMenu && this.mc.Level >= 4)
                {
                    UpdateMainMessage("�A�C���F�X�g���[�g�X�}�b�V���ɁE�E�E���ꂩ��E�E�E�t���b�V���q�[���E�E�E");

                    UpdateMainMessage("���i�F������ȏ��ŗ��K���Ă�̂�H");

                    UpdateMainMessage("�A�C���F�����A���ƂȂ��v���o�����̂�̂Ɋ��ꂳ���悤�Ǝv���Ă��ȁB");

                    UpdateMainMessage("�A�C���F�������A�ǂ��������ȁB");

                    UpdateMainMessage("���i�F�₯�ɍl������ł��ˁB���k�Ȃ炢�ł������B");

                    UpdateMainMessage("�A�C���F�����A�����ȁB������Ƃ��������b�Ȃ񂾂��E�E�E");

                    UpdateMainMessage("�@�@�@�y�A�C���̉���Ȑ������A���i�֓W�J���E�E�E�z");

                    UpdateMainMessage("���i�F�b�_���I��������������񕪂���Ȃ��I�I");

                    UpdateMainMessage("���i�F�o�J�A�C���̘b���đS�R�������������A�ǂ����|�C���g�Ȃ̂�I�H");

                    UpdateMainMessage("�A�C���F�����炳�������猾���Ă邶��˂����A���̘A�������厖�Ȃ񂾂��āB");

                    UpdateMainMessage("���i�F������A���������������ۓI�Șb�͌��\��B");

                    UpdateMainMessage("���i�F�A�C���̘b�A�����܂�Řb���Ƃ����������Ƃ�ˁH");

                    UpdateMainMessage("���i�F�w�P�D�d�r�b���j���[���J���x");

                    UpdateMainMessage("���i�F�w�Q�D�V�����ǉ�����Ă���y�o�g���ݒ�z��I������x");

                    UpdateMainMessage("���i�F�w�R�D���ݏK�����Ă閂�@�E�X�L���\�����o�g���R�}���h�ɐݒ肷��x");

                    UpdateMainMessage("���i�F���ł���H");

                    UpdateMainMessage("�A�C���F����A����͂����Ȃ񂾂��A���������b�����Ă񂶂�˂��B");

                    UpdateMainMessage("�A�C���F�R�}���h�̏����A���������o�g���Ɋւ��鍪�{�I�ȗ��������܂ЂƂ��ȁB");

                    UpdateMainMessage("���i�F���͗ǂ��ł���B����Șb�͌�ł�����ł��o�Ă�����B");

                    UpdateMainMessage("���i�F�Ƃ肠�����o�������@�E�X�L�����p�p���Ɛݒ肵���Ⴂ�Ȃ�����B");

                    UpdateMainMessage("���i�F�z���b�g�A�ǁ[�ł����������Ńo�J�A�C���͋Â�o����ˁB");

                    UpdateMainMessage("�A�C���F�܂���������˂����B�ŏ��̓��ɂ���Ă����ɉz�������͂˂��B");

                    UpdateMainMessage("�A�C���F������A������������Ă݂邺�I");

                    CallSomeMessageWithAnimation("�yESC���j���[���u�o�g���ݒ�v���I���ł���悤�ɂȂ�܂����B�z");

                    CallSomeMessageWithAnimation("�y�K���������@�E�X�L�����o�g���R�}���h�ɐݒ�ł���悤�ɂȂ�܂��B�z");

                    we.AvailableBattleSettingMenu = true;
                }
                #endregion
                #region "�퓬�F�C���X�^���g�A�N�V����"
                else if (!we.AvailableInstantCommand && this.mc.Level >= 6)
                {
                    UpdateMainMessage("�A�C���F���̑O�́A�m������Ȋ����ł���Ă��C������񂾂��E�E�E");

                    UpdateMainMessage("���i�F����������Ȋ炵�Ă��ˁB�����v�������킯�H");

                    UpdateMainMessage("�A�C���F��`����A�ȑO�t���ɋ���������c�Ȃ񂾂��ǂȁB");

                    UpdateMainMessage("���i�F�����f�B�X�̂��t������H");

                    UpdateMainMessage("�A�C���F�����A�������B");

                    UpdateMainMessage("�A�C���F�C���X�^���g�A�N�V�������Ă����s���炵�����B");

                    UpdateMainMessage("�A�C���F�ȒP�Ɍ����ƁE�E�E");

                    UpdateMainMessage("�A�C���F�C���X�^���g�A�N�V�������I�I");

                    UpdateMainMessage("���i�F�����������o���ĂȂ�����Ȃ��E�E�E");

                    UpdateMainMessage("���i�F�܂�����͗ǂ��Ƃ��āA�o�������Ȃ́H");

                    UpdateMainMessage("�A�C���F�����A�������傢�̃n�Y���B�܂����ĂĂ����B");

                    UpdateMainMessage("�@�y�@�A�C���̓X�g���[�g�E�X�}�b�V���̑̐��ɓ������@�z");

                    UpdateMainMessage("�A�C���F�b�t�@�C�A�I�I");

                    UpdateMainMessage("���i�F�����I�H");

                    UpdateMainMessage("�@�y�@�A�C���̓_�~�[�f�U��N�Ƀt�@�C�A�E�{�[����������I�z");

                    UpdateMainMessage("�A�C���F�������I��������H�b�n�b�n�b�n�I�I");

                    UpdateMainMessage("���i�F�����E�E�E��������B�ǂ�����Ȃ̏o�����ˁH");

                    UpdateMainMessage("�A�C���F�����͊ȒP���B���i�A���O�ɂ����Ԃ�o������e�����B");

                    UpdateMainMessage("�A�C���F�v�́A�ŏ�������t�@�C�A�E�{�[������悤�ɂ��Ƃ��΂����̂��B");

                    UpdateMainMessage("���i�F�����ڂ̑f�U�肾�����X�g���[�g�E�X�}�b�V���ɂ��Ă����Ď��H");

                    UpdateMainMessage("�A�C���F����A�X�g���[�g�E�X�}�b�V���̑̐�����́A�X�g���[�g�E�X�}�b�V���͉\���B");

                    UpdateMainMessage("���i�F�E�E�E���ɂ��o����̂�����E�E�E");

                    UpdateMainMessage("�A�C���F���v�����āB����Ă݂���āB");

                    UpdateMainMessage("�@�y�@���i�͒ʏ�U���̑̐��ɓ������@�z");

                    UpdateMainMessage("���i�F���`��E�E�E���ƁA����������B�b�n�C�I");

                    UpdateMainMessage("�@�y�@���i�̓A�C�X�j�[�h�����_�~�[�f�U��N�ɕ������I�z");

                    UpdateMainMessage("�A�C���F�E�E�E����Ȋ������ȁI�o��������˂����I�@�b�n�b�n�b�n�I�I");

                    UpdateMainMessage("���i�F���`��A�A�C���̂Ƃ͏����Ⴄ�C�������񂾂��ǁB");

                    UpdateMainMessage("�A�C���F���̂��������o���Ă�΁A�퓬�X�^�C�������Ȃ蕝���g���邺�B");

                    UpdateMainMessage("���i�F�܂��m���ɒʏ�̐퓬�R�}���h�ɉ����āA���̍s�����o����̂͊�������ˁ�");

                    UpdateMainMessage("�A�C���F�y���݂ɂȂ��Ă����ȁI������A��������������K���Ă������I�b�n�b�n�b�n�I");

                    CallSomeMessageWithAnimation("�y�퓬���ɃC���X�^���g�A�N�V�������o����悤�ɂȂ�܂����B�z");

                    CallSomeMessageWithAnimation("�y�퓬���A�A�N�V�����R�}���h���E�N���b�N���鎖�Ŏg�p�\�ɂȂ�܂��B�z");

                    we.AvailableInstantCommand = true;
                }
                #endregion
             }
        }
        private void CallSomeMessageWithAnimation(string message)
        {
            using (MessageDisplay md = new MessageDisplay())
            {
                md.StartPosition = FormStartPosition.Manual;
                md.Location = new Point(this.Location.X, this.Location.Y + this.Size.Height);
                md.NeedAnimation = true;
                md.Message = message;
                md.ShowDialog();
            }
        }
        private void TruthHomeTown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                using (ESCMenu esc = new ESCMenu())
                {
                    esc.MC = this.MC;
                    esc.SC = this.SC;
                    esc.TC = this.TC;
                    esc.WE = this.we;
                    esc.KnownTileInfo = null;
                    esc.KnownTileInfo2 = null;
                    esc.KnownTileInfo3 = null;
                    esc.KnownTileInfo4 = null;
                    esc.KnownTileInfo5 = null;
                    esc.Truth_KnownTileInfo = this.Truth_KnownTileInfo;
                    esc.Truth_KnownTileInfo2 = this.Truth_KnownTileInfo2;
                    esc.Truth_KnownTileInfo3 = this.Truth_KnownTileInfo3;
                    esc.Truth_KnownTileInfo4 = this.Truth_KnownTileInfo4;
                    esc.Truth_KnownTileInfo5 = this.Truth_KnownTileInfo5;
                    esc.StartPosition = FormStartPosition.CenterParent;
                    esc.TruthStory = true;
                    esc.ShowDialog();
                    if (esc.DialogResult == DialogResult.Retry)
                    {
                        this.mc = esc.MC;
                        this.sc = esc.SC;
                        this.tc = esc.TC;
                        this.we = esc.WE;
                        this.Truth_KnownTileInfo = esc.Truth_KnownTileInfo;
                        this.Truth_KnownTileInfo2 = esc.Truth_KnownTileInfo2;
                        this.Truth_KnownTileInfo3 = esc.Truth_KnownTileInfo3;
                        this.Truth_KnownTileInfo4 = esc.Truth_KnownTileInfo4;
                        this.Truth_KnownTileInfo5 = esc.Truth_KnownTileInfo5;
                        this.DialogResult = DialogResult.Retry;
                    }
                    else if (esc.DialogResult == DialogResult.Cancel)
                    {
                        this.DialogResult = DialogResult.Cancel;
                    }
                }
            }
        }

        private void UpdateEndingMessage2(string message)
        {
            this.endingText2.Add(message + "\r\n");
        }
        private void UpdateEndingMessage(string message)
        {
            this.endingText.Add(message + "\r\n");
        }
        private void UpdateMainMessage(string message)
        {
            UpdateMainMessage(message, false);
        }
        private void UpdateMainMessage(string message, bool ignoreOK)
        {
            GroundOne.playbackMessage.Insert(0, message);
            GroundOne.playbackInfoStyle.Insert(0, TruthPlaybackMessage.infoStyle.normal);
            mainMessage.Text = message;
            mainMessage.Update();
            if (!ignoreOK)
            {
                ok.ShowDialog();
            }
        }

        // �_���W�����V�[�J�[�I
        private void button2_Click(object sender, EventArgs e)
        {
            if (!GroundOne.WE2.RealWorld && we.GameDay <= 1 && (!we.AlreadyCommunicate || !we.Truth_CommunicationGanz1 || !we.Truth_CommunicationHanna1 || !we.Truth_CommunicationLana1))
            {
                mainMessage.Text = "�A�C���F�_���W�����͂��������҂��Ă���B���̊F�Ɉ��A�����Ȃ�����ȁB";
            }
            else if (!GroundOne.WE2.RealWorld && we.TruthCompleteArea1 && (!we.Truth_CommunicationLana21 || !we.Truth_CommunicationGanz21 || !we.Truth_CommunicationHanna21 || !we.Truth_CommunicationOl21))
            {
                mainMessage.Text = "�A�C���F�_���W�����͂��������҂��Ă���B���̊F�Ɉ��A�����Ȃ�����ȁB";
            }
            else if (!GroundOne.WE2.RealWorld && we.TruthCompleteArea2 && (!we.Truth_CommunicationLana31 || !we.Truth_CommunicationGanz31 || !we.Truth_CommunicationHanna31 || !we.Truth_CommunicationOl31 || !we.Truth_CommunicationSinikia31))
            {
                mainMessage.Text = "�A�C���F�_���W�����͂��������҂��Ă���B���̊F�Ɉ��A�����Ȃ�����ȁB";
            }
            else if (!GroundOne.WE2.RealWorld && we.TruthCompleteArea3 && (!we.Truth_CommunicationLana41 || !we.Truth_CommunicationGanz41 || !we.Truth_CommunicationHanna41 || !we.Truth_CommunicationOl41 || !we.Truth_CommunicationSinikia41))
            {
                mainMessage.Text = "�A�C���F�_���W�����͂��������҂��Ă���B���̊F�Ɉ��A�����Ȃ�����ȁB";
            }
            else if (GroundOne.WE2.RealWorld && (!GroundOne.WE2.SeekerEvent602 || !GroundOne.WE2.SeekerEvent603 || !GroundOne.WE2.SeekerEvent604))
            {
                mainMessage.Text = "�A�C���F�_���W�����͂��������҂��Ă���B���̊F�Ɉ��A�����Ȃ�����ȁB";
            }
            else if (!we.AlreadyRest)
            {
                mainMessage.Text = "�A�C���F���o�Ă����΂��肾���H��x�e�����Ă���B";
            }
            else
            {
                if (GroundOne.WE2.RealWorld && GroundOne.WE2.SeekerEvent602 && GroundOne.WE2.SeekerEvent603 && GroundOne.WE2.SeekerEvent604 && !GroundOne.WE2.SeekerEvent605)
                {
                    UpdateMainMessage("�A�C���F�i�E�E�E�悵�E�E�E�s�����I�j");

                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("���i�F������ƁA�҂��Ȃ�����B");

                    GroundOne.PlayDungeonMusic(Database.BGM19, Database.BGM19LoopBegin);

                    UpdateMainMessage("�A�C���F�E�E�E���i���E�E�E");

                    UpdateMainMessage("���i�F������_���W�����Ɍ������C��ˁH");

                    UpdateMainMessage("�A�C���F�����A���̂��肾�B");

                    UpdateMainMessage("���i�F�E�E�E");

                    UpdateMainMessage("�A�C���F�E�E�E");

                    UpdateMainMessage("���i�F�y�@�^���̓����@�z�E�E�E���������H");

                    UpdateMainMessage("�A�C���F�E�E�E");

                    UpdateMainMessage("�A�C���F�����A�������Ă�B");

                    UpdateMainMessage("���i�F�������Ă݂āA�����Ă����邩��B");

                    UpdateMainMessage("�A�C���F�킩�����B");

                    UpdateMainMessage("�A�C���F�y�͂͗͂ɂ��炸�A�͂͑S�Ăł���B�z");

                    UpdateMainMessage("�A�C���F�y�������Ȃ������B�@�������S�͖������B�z");

                    UpdateMainMessage("�A�C���F�y�݂͂̂Ɉˑ�����ȁB�S��΂ɂ���B�z");

                    UpdateMainMessage("�A�C���F���i�̕ꂿ��񂪂���Ă������痥����B�������̏\�P�̈���B");

                    UpdateMainMessage("���i�F�������E�E�E�����Ɗo���Ă��̂ˁB");

                    UpdateMainMessage("�A�C���F�����B���̎��͕�����Ȃ��������A���A�悤�₭������n�߂��񂾁B");

                    UpdateMainMessage("�A�C���F�͂���������E������A���ꂾ������_���Ȃ񂾁B");

                    UpdateMainMessage("�A�C���F�ł�������ƌ����āA�M�O��z�������������ĂĂ��ʖڂ��B");

                    UpdateMainMessage("�A�C���F�����Ƃ����������ď��߂ĈӖ����o�Ă���B");

                    UpdateMainMessage("�A�C���F����Ȋ������B");

                    UpdateMainMessage("���i�F�����E�E�E���ɂ͂悭������Ȃ�����");

                    UpdateMainMessage("���i�F�A�C�������������̓������^���Ȃ̂ˁA�����ƁB");

                    UpdateMainMessage("�A�C���F����������Ă��ꂽ�̂��A���̌����B");

                    UpdateMainMessage("���i�F���̗��K�p�̌��H�@���������ꂳ�񂩂����������ˁB");

                    UpdateMainMessage("�A�C���F�����A�������B");

                    UpdateMainMessage("�A�C���F���ꂪ�_���t�F���g�D�[�V�����ƒm��܂łɂ͂����Ԃ�Ǝ��Ԃ����������B");

                    UpdateMainMessage("�A�C���F���̍��́A�ǂ��݂Ă��P�Ȃ�i�}�N���̌��ɂ��������Ȃ���������ȁB");

                    UpdateMainMessage("���i�F�E�E�E��������C�Â��Ă��̂�H");

                    UpdateMainMessage("�A�C���F�{�P�t�������f�B�X�ɏo���킵�������B");

                    UpdateMainMessage("���i�F�����������́B���ꂩ��́A�C�Â��Ȃ��U�肵�Ă��́H");

                    UpdateMainMessage("�A�C���F����A���������킯����˂��B���M���^���������Ă̂������ȏ����B");

                    UpdateMainMessage("�A�C���F���̌��́A�ǂ��݂Ă��P�Ȃ�i�}�N�����B���ێg���Ă݂Ă��S�R�З͂��o�Ȃ����ȁB");

                    UpdateMainMessage("���i�F�ӂ���B����ł��t������ɉ���Ă���ǂ��ς�����̂�H");

                    UpdateMainMessage("�A�C���F�t���͂ǂ������̌��̓����Ɋւ��āA����������m���Ă�݂����������񂾁B");

                    UpdateMainMessage("�A�C���F����A���̌��Ɋւ�炸�A�S�ʓI�Șb�݂����������B����������Ă��ꂽ�B");

                    UpdateMainMessage("�A�C���F�S�𓕂��ĕ����Ȃ��ƁA�З͔͂�������Ȃ��B��������Șb�������B");

                    UpdateMainMessage("���i�F�S�𓕂��āE�E�E���Ď��́B");

                    UpdateMainMessage("�A�C���F���̌��A�ō��U���͂��ُ�ɍ����B�����āA�Œ�U���͂��ُ�ɒႢ�B");

                    UpdateMainMessage("�A�C���F�S�𓕂��Ȃ�����A�ō��U���͂͏o�Ȃ��B�܂�A�i�}�N���Ȃ܂܂��Ă킯���B");

                    UpdateMainMessage("�A�C���F���ꂪ�����������_�ŁA���̗͂ɑ΂���l���͕ς�����B");

                    UpdateMainMessage("�A�C���F���̏\�P�̂V�ԖځB���̌��t�ʂ�A�͕͂K�v�����A�͂�������ʖڂ����Ď����B");

                    UpdateMainMessage("���i�F����E�E�E");

                    UpdateMainMessage("���i�F�A�C�����āE�E�E������ˁB");

                    UpdateMainMessage("�A�C���F�ȁA���₢��A�����Ȃ񂩂˂����āB");

                    UpdateMainMessage("���i�F������A�����������ɍl�����s���͂��̂͐������B������l�������Ȃ����́B");

                    UpdateMainMessage("�A�C���F����A���̏���ȉ��߂�����ȁB�Ԉ���Ă�\���̕����������B");

                    UpdateMainMessage("���i�F������A���߂��Ԉ���Ă�Ƃ����������b����Ȃ��́B");

                    UpdateMainMessage("���i�F�A�C���̕��͋C���̂��̂��A�����ς��̂�B");

                    UpdateMainMessage("���i�F������ÂŁE�E�E�I�𓾂Ă��āE�E�E");

                    UpdateMainMessage("���i�F�����̃A�C������Ȃ��݂����B");

                    UpdateMainMessage("�A�C���F�܁E�E�E");
                    
                    UpdateMainMessage("�A�C���F�܂��A�����������ʂ����邳�I�@�b�n�b�n�b�n�I");

                    UpdateMainMessage("���i�F�ǂ��̂�A�������ĕ��͋C�ς��Ȃ��āA�b�t�t��");

                    UpdateMainMessage("�A�C���F��A�����ȁE�E�E");

                    UpdateMainMessage("���i�F�b�t�t�A�ǂ����Č����Ă邶��Ȃ��́�");

                    UpdateMainMessage("���i�F�ł��A���łɌ��킹�Ă��炤��ˁB");

                    UpdateMainMessage("�A�C���F�ȁA�Ȃ񂾁H");

                    UpdateMainMessage("���i�F�A�C���A���񂽎��Ɏ�������Ă�ł���H");

                    UpdateMainMessage("�A�C���F������H�H�@��̉��̘b���B");

                    UpdateMainMessage("���i�F�퓬�X�^�C���̎���B");

                    UpdateMainMessage("�A�C���F�퓬�E�E�E�X�^�C���H");

                    UpdateMainMessage("���i�F������B�����x��������Ȃ�o���Ȃ��Ƃł��v���Ă��̂�����B");

                    UpdateMainMessage("�A�C���F����A���͎�����Ȃ�Ă��ĂȂ����B�C�̂�������Ȃ��̂��H");

                    UpdateMainMessage("���i�F�������A�A���^���b���P�����𑲋Ƃ�����A�R�b�\���Ǝ��ŗ��K���Ă��鏊�B");

                    UpdateMainMessage("���i�F����ȓ����E�E�E���������Ȃ��X�s�[�h��������B");

                    UpdateMainMessage("�A�C���F�܁A�҂āB����͂��ȁE�E�E");

                    UpdateMainMessage("���i�F�����̂�B�����ᐳ���A�ǂ����Ȃ����x���������B");

                    UpdateMainMessage("���i�F����ؑփ^�C�~���O�A�r�����x�A����U�邤���x�B");

                    UpdateMainMessage("���i�F�S�Ă��ʎ�����������B");

                    UpdateMainMessage("���i�F�ǂ����āE�E�E���Ɍ����Ă���Ȃ��̂�����H");

                    UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                    UpdateMainMessage("�A�C���F���܂˂��B");

                    UpdateMainMessage("���i�F�ӂ�Ȃ��ł�E�E�E�ǂ��Ȃ́H�{���̏��������Ă��傤������B");

                    UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E�@�E�E�E");

                    UpdateMainMessage("���i�F����ς�E�E�E������������ˁB");

                    UpdateMainMessage("�A�C���F�܂āA��������˂��񂾁I");

                    UpdateMainMessage("�A�C���F���������͖̂{�����B");

                    UpdateMainMessage("�A�C���F���i�A���O�ɂ����͂��������Ƃ������������Ȃ������񂾁B");

                    UpdateMainMessage("�A�C���F�m��ꂽ���E�E�E�Ȃ������񂾁B");

                    UpdateMainMessage("�A�C���F���O�������A���̂����������ʂ�m���Ă��܂��΁E�E�E");

                    UpdateMainMessage("�A�C���F���̑O����E�E�E���Ȃ��Ȃ�񂶂�Ȃ������āE�E�E");

                    UpdateMainMessage("���i�F�͗ʂɍ����o�Ă�����A�����A�C�����痣��Ă����B�����l�������Ď��H");

                    UpdateMainMessage("�A�C���F�E�E�E�����E�E�E");

                    UpdateMainMessage("���i�F�E�E�E�@�E�E�E");

                    UpdateMainMessage("���i�F�b�t�A�b�t�t��@�ȁ[�Ɍ����Ă�̂�����A�o�b�J����Ȃ��̃A���^�I�H");

                    UpdateMainMessage("���i�F�͗ʂȂ�āE�E�E���������A���^�Ɏ����ǂ����郏�P�Ȃ��ł���I�H");

                    UpdateMainMessage("�A�C���F���A���i�E�E�E");

                    UpdateMainMessage("���i�F���悻��E�E�E���炵���Ⴄ���z���g�B�A���^�̎��͂��Ăǂ񂾂��Ȃ̂�{���B");

                    UpdateMainMessage("���i�F�B���Ƃ��B���Ȃ��Ƃ��E�E�E������Ȃ����΂�����l���āE�E�E");
                    
                    UpdateMainMessage("���i�F�B���Ȃ��Ⴂ���Ȃ����x���ɂȂ�������Ă�A�������������킯�I�H");

                    UpdateMainMessage("�A�C���F�����E�E�E");

                    UpdateMainMessage("���i�F���̗��K���e�َ̈����݂����ȃX�s�[�h����@����ɁA������������ˁI�H");

                    UpdateMainMessage("���i�F���Ȃ񂩂���B�B�B��΂ɂ���Ȃ̏o�������Ȃ�����B�B�B");

                    UpdateMainMessage("�A�C���F����A���͏o���Ȃ��Ƃ��E�E�E");

                    UpdateMainMessage("���i�F����ȕ��ɋC���g��Ȃ��ŁB�@���A�����̎��͕������Ă���肾����B");

                    UpdateMainMessage("���i�F�E�E�E�@�E�E�E");

                    UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                    UpdateMainMessage("���i�F�b�t�t�E�E�E����������ˁB�̂̏��������̃A�C�����Ă��A�������ォ�������B");

                    UpdateMainMessage("���i�F�����������Ă΂�����B�ŁA��������������Ă����Ă��̂ɁE�E�E");

                    UpdateMainMessage("���i�F���̊Ԃɂ���Ȃɘr���グ������Ă��̂�����A�M�����Ȃ���{���B");

                    UpdateMainMessage("�A�C���F�b�n�n�E�E�E�������ȁA�������₻��Ȏ����E�E�E");

                    UpdateMainMessage("���i�F�E�E�E�@�E�E�E");

                    UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                    UpdateMainMessage("���i�F������A�A�C���B");

                    UpdateMainMessage("���i�F�A�C�������ɑ΂��āA�ςɋC���g���Ă����͋����Ă�����B");

                    UpdateMainMessage("�A�C���F��E�E�E���������ȁA�}�W�ŁB");

                    UpdateMainMessage("�A�C���F���ꂩ��́E�E�E�������ȁA���܂�C���g�킸�ɁE�E�E�B");

                    UpdateMainMessage("���i�F�����A�����[�[�[���I�I�I");

                    UpdateMainMessage("�A�C���F�����I�H�Ȃ񂾂����Ȃ�I�H");

                    UpdateMainMessage("���i�F���A�ǂ����v�������������");

                    UpdateMainMessage("���i�F�o�J�A�C���A�����猾���͖̂��߂�B�����ƕ����Ȃ�����ˁB");

                    UpdateMainMessage("�A�C���F�ȁA�����H");

                    UpdateMainMessage("���i�F���A�������ŃA�C����DUEL������\�����ނ�B");

                    UpdateMainMessage("�A�C���F�ȁI�I�I");

                    UpdateMainMessage("���i�F�ŁA��������t���������B�����Ȃ����B");

                    UpdateMainMessage("�A�C���F�ȁA�������̏������Ă̂́H");

                    UpdateMainMessage("���i�F���񂽍��x�����{���ɁA�����̏�Ŏ���������Ɏ��ɒ���ł��炤���B");

                    UpdateMainMessage("���i�F���ꂪ��΂̏�����B�ǂ��H");

                    UpdateMainMessage("�A�C���F�����E�E�E");

                    UpdateMainMessage("�A�C���F�����������������Ă���E�E�E�ǂ��Ȃ�H");

                    UpdateMainMessage("���i�F���̎��́A���̓A���^�Ƃ����R���r�͑g�܂Ȃ���B");
                    
                    UpdateMainMessage("���i�F���������Ă܂ňꏏ�ɋ������Ȃ�����B");

                    UpdateMainMessage("�A�C���F�E�E�E���������B");
                    
                    UpdateMainMessage("�A�C���F���̈��A��΂Ɏ�����͂��˂��B�񑩂��I");

                    UpdateMainMessage("�A�C���F�E�E�E�����I�܁A�܂Ă�I�H");
                    
                    UpdateMainMessage("�A�C���F�����ꂻ��ŁA���������Ă��܂�����ǂ��Ȃ�񂾁H");

                    UpdateMainMessage("�A�C���F����ς�E�E�E���̎����E�E�E");

                    UpdateMainMessage("���i�F�E�E�E�b�v");
                    
                    UpdateMainMessage("���i�F�b�t�t�t�A�A�[�b�n�n�n�n��");

                    UpdateMainMessage("���i�F������ȐS�z���Ă�̂�A���v���");

                    UpdateMainMessage("���i�F��������ĂȂ��{�C�̃A���^��������������B");

                    UpdateMainMessage("���i�F�A���^��{�I�ɏ����ē��R�Ȃ񂾂���A�܂��N�_���i�C���l���Ȃ��ł�˃z���g��");

                    UpdateMainMessage("���i�F�i�@�ǂ����ɂ���E�E�E�{���ɗ��ꂽ�肷��킯�E�E�E�@�j");

                    UpdateMainMessage("�A�C���F�����E�E�E�H");

                    UpdateMainMessage("���i�F�z�[���z���z���z���A���Ⴀ�s�����B�����ƍ\���Ȃ�����ˁ�");

                    UpdateMainMessage("�A�C���F���A�����B������Ƒ҂��Ă���ȁB");

                    UpdateMainMessage("�A�C���F�E�E�E�悵�A�n�j���B");

                    UpdateMainMessage("���i�F�����ǂ�����");

                    UpdateMainMessage("�A�C���F���Ⴀ�A���^�����̖{�C���B����������ōs�����I");

                    UpdateMainMessage("���i�F�n�߂���A�R");

                    UpdateMainMessage("�A�C���F�Q");

                    UpdateMainMessage("���i�F�P");

                    UpdateMainMessage("�A�C���F�O�I�I");

                    bool failCount1 = false;
                    bool failCount2 = false;
                    while (true)
                    {
                        bool result = BattleStart(Database.ENEMY_LAST_RANA_AMILIA, true);

                        if (failCount1 && failCount2)
                        {
                            using (YesNoReqWithMessage ynrw = new YesNoReqWithMessage())
                            {
                                ynrw.StartPosition = FormStartPosition.CenterParent;
                                ynrw.MainMessage = "�퓬���X�L�b�v���A����������Ԃ���X�g�[���[��i�߂܂����H\r\n�퓬�X�L�b�v�ɂ��y�i���e�B�͂���܂���B";
                                ynrw.ShowDialog();
                                if (ynrw.DialogResult == DialogResult.Yes)
                                {
                                    result = true;
                                }
                            }
                        }

                        if (result)
                        {
                            // ����
                            UpdateMainMessage("���i�F�b�L���I�I");

                            UpdateMainMessage("�A�C���F���܂����I�I�@���v���A���i�I�H");

                            UpdateMainMessage("���i�F�����E�E�E���v��A�����ł�������������B");

                            UpdateMainMessage("�A�C���F���A����Ƃ����Ă˂����H���v�Ȃ̂��H�ɂ����͂Ȃ����I�H");

                            UpdateMainMessage("���i�F���[������[�Ԃ����Č����Ă�ł���[���B�z���z�����C���");

                            UpdateMainMessage("�A�C���F��E�E�E�ǂ������B�{���ɑ��v���ȁH");

                            UpdateMainMessage("���i�F��������ˁB�R�肩�������B");

                            UpdateMainMessage("�A�C���F����A�킩�����B");

                            UpdateMainMessage("���i�F�ŁE�E�E������͂��ĂȂ����ˁH");

                            UpdateMainMessage("�A�C���F������񂳁I�@���̓��Ӑ�p�����̂܂܎g��������ȁI");

                            UpdateMainMessage("���i�F�ł��A�܂�������ȃ^�C�~���O�������Ă���Ƃ͎v��Ȃ�������B");

                            UpdateMainMessage("���i�F�A�C�����Ă��A�ǂ��ł��������̊o���Ă��Ă�́H");

                            UpdateMainMessage("�A�C���F�ǂ����Č����Ă��ȁE�E�E�t���Ƃ���Ă邤���Ɏ��R�ƁE�E�E���ȁB");

                            UpdateMainMessage("���i�F�Ӂ`��E�E�E����ς胉���f�B�X�̂��t�����񂪉e�����Ă�킯�ˁB");

                            UpdateMainMessage("�A�C���F���Ƃ́E�E�E�����Ȃ�ɁA�R�\�R�\���Ƃ��ȁE�E�E");

                            UpdateMainMessage("�A�C���F���ɂ�DUEL���Z����ώ@���ĂāA�����ɂ͂Ȃ��g�R���ώ@���ȁB");

                            UpdateMainMessage("�A�C���F�b���P������̊�b�P�����ڂ����܂ɓǂݕԂ��Ĕ������K�͂��Ă�B");

                            UpdateMainMessage("�A�C���F�����X�^�[���̎����A���i�g��Ȃ��V��p��������Ă݂���B");

                            UpdateMainMessage("�A�C���F���Ƃ́E�E�E");

                            UpdateMainMessage("���i�F���`���A�����C�C�I�@���̕����I�I");

                            UpdateMainMessage("�A�C���F������A���܂˂��A�����������āB");

                            UpdateMainMessage("���i�F������A�ǂ��́B�{�C�������Ă��ꂽ�񂾂��A�X�b�L���������");

                            UpdateMainMessage("�A�C���F�n�b�E�E�E�n�n�n�E�E�E");

                            UpdateMainMessage("���i�F�_���W�����A����U��Ȃ��ň�l�ł�������Ȃ�ł���H");

                            UpdateMainMessage("�A�C���F�����E�E�E");

                            UpdateMainMessage("���i�F�o�J�A�C���͉R��肪���肭�������Ȃ̂�B����Ȃ̂����ʂ���B");

                            UpdateMainMessage("�A�C���F�n�n�n�E�E�E�܂��E�E�E");

                            UpdateMainMessage("�A�C���F�R�Ƃ������A�����p�[�e�B�ɗU������͂������B");

                            UpdateMainMessage("�A�C���F����͖{�����B");

                            UpdateMainMessage("���i�F�E�E�E�@�E�E�E");

                            UpdateMainMessage("�A�C���F�ł��A���ꂶ��E�E�E�ʖڂ݂����Ȃ񂾁B");

                            UpdateMainMessage("�A�C���F���́E�E�E");

                            UpdateMainMessage("�A�C���F�����𔲂������Ȃ���Ȃ�Ȃ��񂾁B");

                            UpdateMainMessage("���i�F���Ƃ��E�E�E�H��������Ȃ́H");

                            UpdateMainMessage("�A�C���F�����A���O�̃C�������O���z���B");

                            UpdateMainMessage("���i�F�����E�E�E");

                            UpdateMainMessage("�A�C���F���́A������ɂ����܂܂̏�Ԃ��B");

                            UpdateMainMessage("�A�C���F�E�E�E���������񂾁A�ǂ����Ȃ���΂����Ȃ����B");

                            UpdateMainMessage("���i�F�E�E�E");

                            UpdateMainMessage("���i�F���肪�ƁB����ȏ��܂Ŋ撣���Ă���āB");

                            UpdateMainMessage("�A�C���F�o�J�����ȁB�����g�̖�肾�B");

                            UpdateMainMessage("�A�C���F��΂ɉ��Ƃ����Ă��B�C����B");

                            UpdateMainMessage("���i�F����A���肢�B���҂��Ă邩���");

                            UpdateMainMessage("�A�C���F���Ⴀ�ȁA�s���Ă��邺�I�I");

                            break;
                        }
                        else
                        {
                            using (YesNoReqWithMessage yerw = new YesNoReqWithMessage())
                            {
                                UpdateMainMessage("�A�C���F�b�O�E�E�I�I");
                                if (!failCount1)
                                {
                                    failCount1 = true;

                                    UpdateMainMessage("���i�F���̂œ�����Ȃ�āA�A�C���炵���Ȃ���ˁB");

                                    UpdateMainMessage("�A�C���F�b�N�\�E�E�E���������肾�����񂾂��ȁB");

                                    UpdateMainMessage("���i�F���̃A�C���E�E�E����ς蓮�����݂��Ă���B");

                                    UpdateMainMessage("���i�F�����Ă��傤������A�{���̓������B");

                                    UpdateMainMessage("�A�C���F���A�����B���x�����I");
                                }
                                else if (!failCount2)
                                {
                                    failCount2 = true;
                                    UpdateMainMessage("���i�F��������g�̂ɐ��ݍ���ł���݂����ˁB�������x���������B");

                                    UpdateMainMessage("�A�C���F���i���肾�ƁE�E�E�������k���܂��Ă�̂��E�E�E");

                                    UpdateMainMessage("���i�F���̂���[�������Ȃ���A�A�C���{�C�������Ă��傤�����B");

                                    UpdateMainMessage("�A�C���F�����A���x�����I");
                                }
                                else
                                {
                                    UpdateMainMessage("���i�F���̂���[�������Ȃ���A�A�C���{�C�������Ă��傤�����B");

                                    UpdateMainMessage("�A�C���F�����A���x�����I");
                                }
                            }
                        }
                    }

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "�_���W�����Q�[�g�̓�����ɂ�";
                        md.ShowDialog();
                    }

                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("�A�C���F�i�E�E�E�_���W�����ցE�E�E���͌������E�E�E�j");

                    UpdateMainMessage("�A�C���F�i���i�̃C�������O�͎�ɂ����܂܂��j");

                    UpdateMainMessage("�A�C���F�i���͂���̈Ӗ���m���Ă���j");

                    UpdateMainMessage("�A�C���F�i�E�E�E�@�E�E�E�@�E�E�E�j");

                    UpdateMainMessage("�A�C���F�i�s�����A�_���W�����ցj");

                    this.targetDungeon = 1;
                    GroundOne.WE2.RealDungeonArea = 1;
                    GroundOne.WE2.SeekerEvent605 = true;
                    Method.AutoSaveTruthWorldEnvironment();
                    Method.AutoSaveRealWorld(this.MC, this.SC, this.TC, this.WE, null, null, null, null, null, this.Truth_KnownTileInfo, this.Truth_KnownTileInfo2, this.Truth_KnownTileInfo3, this.Truth_KnownTileInfo4, this.Truth_KnownTileInfo5);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    string Opponent = WhoisDuelPlayer();
                    if (Opponent != String.Empty)
                    {
                        DuelSupportMessage(SupportType.FromDungeonGate, Opponent);

                        CallDuel(Opponent, true);
                    }
                    else
                    {
                        #region "�_���W�����K�w��I��"
                        if (we.TruthCompleteArea1)
                        {
                            mainMessage.Text = "�A�C���F���āA���K����n�߂邩�ȁB";
                            mainMessage.Update();
                            using (SelectDungeon sd = new SelectDungeon())
                            {
                                sd.StartPosition = FormStartPosition.Manual;
                                sd.Location = new Point(this.Location.X + 50, this.Location.Y + 50);
                                //if (we.CompleteArea5) sd.MaxSelectable = 5;
                                if (we.TruthCompleteArea4) sd.MaxSelectable = 5;
                                else if (we.TruthCompleteArea3) sd.MaxSelectable = 4;
                                else if (we.TruthCompleteArea2) sd.MaxSelectable = 3;
                                else if (we.TruthCompleteArea1) sd.MaxSelectable = 2;
                                sd.ShowDialog();
                                this.targetDungeon = sd.TargetDungeon;
                            }
                        }
                        if (this.targetDungeon == 1)
                        {
                            if (!we.TruthCompleteArea1)
                            {
                                mainMessage.Text = "�A�C���F���āA�P�K��˔j���邺�I";
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
                        #endregion

                        #region "���i�A�K���c�A�n���i�̈�ʉ�b�����͂����Ŕ��f���܂��B"
                        if (this.firstDay >= 1 && !we.Truth_CommunicationLana1 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationLana1 = true;
                        else if (this.firstDay >= 2 && !we.Truth_CommunicationLana2 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationLana2 = true;
                        else if (this.firstDay >= 3 && !we.Truth_CommunicationLana3 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationLana3 = true;
                        else if (this.firstDay >= 4 && !we.Truth_CommunicationLana4 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationLana4 = true;
                        else if (this.firstDay >= 5 && !we.Truth_CommunicationLana5 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationLana5 = true;
                        else if (this.firstDay >= 6 && !we.Truth_CommunicationLana6 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationLana6 = true;
                        else if (this.firstDay >= 7 && !we.Truth_CommunicationLana7 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationLana7 = true;
                        else if (this.firstDay >= 8 && !we.Truth_CommunicationLana8 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationLana8 = true;
                        else if (this.firstDay >= 9 && !we.Truth_CommunicationLana9 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationLana9 = true;
                        else if (this.firstDay >= 10 && !we.Truth_CommunicationLana10 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationLana10 = true;

                        if (this.firstDay >= 1 && !we.Truth_CommunicationHanna1 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationHanna1 = true;
                        else if (this.firstDay >= 2 && !we.Truth_CommunicationHanna2 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationHanna2 = true;
                        else if (this.firstDay >= 3 && !we.Truth_CommunicationHanna3 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationHanna3 = true;
                        else if (this.firstDay >= 4 && !we.Truth_CommunicationHanna4 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationHanna4 = true;
                        else if (this.firstDay >= 5 && !we.Truth_CommunicationHanna5 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationHanna5 = true;
                        else if (this.firstDay >= 6 && !we.Truth_CommunicationHanna6 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationHanna6 = true;
                        else if (this.firstDay >= 7 && !we.Truth_CommunicationHanna7 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationHanna7 = true;
                        else if (this.firstDay >= 8 && !we.Truth_CommunicationHanna8 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationHanna8 = true;
                        else if (this.firstDay >= 9 && !we.Truth_CommunicationHanna9 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationHanna9 = true;
                        else if (this.firstDay >= 10 && !we.Truth_CommunicationHanna10 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationHanna10 = true;

                        if (this.firstDay >= 1 && !we.Truth_CommunicationGanz1 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationGanz1 = true;
                        else if (this.firstDay >= 2 && !we.Truth_CommunicationGanz2 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationGanz2 = true;
                        else if (this.firstDay >= 3 && !we.Truth_CommunicationGanz3 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationGanz3 = true;
                        else if (this.firstDay >= 4 && !we.Truth_CommunicationGanz4 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationGanz4 = true;
                        else if (this.firstDay >= 5 && !we.Truth_CommunicationGanz5 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationGanz5 = true;
                        else if (this.firstDay >= 6 && !we.Truth_CommunicationGanz6 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationGanz6 = true;
                        else if (this.firstDay >= 7 && !we.Truth_CommunicationGanz7 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationGanz7 = true;
                        else if (this.firstDay >= 8 && !we.Truth_CommunicationGanz8 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationGanz8 = true;
                        else if (this.firstDay >= 9 && !we.Truth_CommunicationGanz9 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationGanz9 = true;
                        else if (this.firstDay >= 10 && !we.Truth_CommunicationGanz10 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationGanz10 = true;
                        #endregion

                        we.AlreadyShownEvent = false;
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                }
            }
        }

        private void CallDuel(string OpponentDuelist, bool fromGoDungeon)
        {
            UpdateMainMessage("�@�@�y��t��F�R�z");

            UpdateMainMessage("�@�@�y��t��F�Q�z");

            UpdateMainMessage("�@�@�y��t��F�P�z");

            UpdateMainMessage("�@�@�y��t��F�O�z");


            bool result = BattleStart(OpponentDuelist, true);
            if (result)
            {
                GroundOne.WE2.DuelWin += 1;

                UpdateMainMessage("�@�@�y��t��F���� �A�C���E�E�H�[�����X�I�z");

                UpdateMainMessage("�A�C���F������I���̏��������I�I");
            }
            else
            {
                GroundOne.WE2.DuelLose += 1;
                UpdateMainMessage("�@�@�y��t��F���� " + OpponentDuelist + "�I�z");

                UpdateMainMessage("�A�C���F�b�N�\�E�E�E�������܂������E�E�E");
            }

            #region "�U���Q��DUEL�퓬��̃Z���t"
            if (OpponentDuelist == Database.DUEL_SCOTY_ZALGE)
            {
                if (result)
                {
                    we.DuelWinZalge = true;

                    UpdateMainMessage("�U���Q�F�b�Q�z�I�I�H�E�E�E�b�O�E�E�E����ȎG���ɉ��l���E�E�E");

                    UpdateMainMessage("�A�C���F�G���ň��������ȁB");

                    UpdateMainMessage("�U���Q�F�b�N�\�H�H�H�E�E�E�������A�L�[�i��I�I");

                    UpdateMainMessage("�U���Q�F�����猾���b���悧�����������Ă�����I");

                    UpdateMainMessage("�U���Q�F�R�O�A�����X�g�b�v��������ƌ����āE�E�E");

                    UpdateMainMessage("�@�@�@�i�A�C���͓ˑR�吺�ŁE�E�E�j");

                    UpdateMainMessage("�A�C���F�c�t�d�k���^�c�̉�����I");

                    UpdateMainMessage("�A�C���F�Ȃ��A�ǂ����ɋ���񂾂�I�H");

                    UpdateMainMessage("�@�@�@�i�E�E�E�ϋq�����������A�ǂ�߂��n�߂��E�E�E�j");

                    UpdateMainMessage("�A�C���F��t����Ċm���A�c�t�d�k�^�c�T�|�[�g�������Ȃ񂾂�H");

                    UpdateMainMessage("�A�C���F���̂܂܂���A�c�t�d�k�̉^�c�S�̂Ɏx����������񂶂�Ȃ��̂��H");

                    UpdateMainMessage("�A�C���F�����Ȃ�O�ɂ��B�T�|�[�g�ɓO���鎖���o������ɂ��Ă���Ă���B");

                    UpdateMainMessage("�A�C���F���񂾂��I�H");

                    UpdateMainMessage("�@�@�@�i�E�E�E����E�E�E�U���U���U���E�E�E�j");

                    UpdateMainMessage("�U���Q�F���A�����A��������˂����ƒ񌾂��Ă񂾂�I�H�������I�H");

                    UpdateMainMessage("�U���Q�F��ȉ�Ȃ�ăR�R��ӂɂ���킯�˂����낤���I�H");

                    UpdateMainMessage("�A�C���F���������F���ł��Ȃ��قǋC�z���B���Ă邾�����B�K���ǂ����Ō��Ă�͂����B");

                    UpdateMainMessage("�U���Q�F���Ď��͉����H���ɓS�Ƃł�������Ă����I�H");

                    UpdateMainMessage("�U���Q�F�E�E�E�E�E�E");

                    UpdateMainMessage("�U���Q�F���ĉ����N���˂�����˂����I�I�@�Q�n�n�n�n�n�n�I");

                    UpdateMainMessage("�U���Q�F�����āA����̑������A�ǂ������L�[�i�삿���I�H���͂Ȃ��I�I");

                    UpdateMainMessage("�@�@�@�i�ˑR�A�U���Q�ɑM���������ꂽ�I�I�I�j");

                    UpdateMainMessage("�@�@�@�i�b�s�b�b�V�C�C�C�C�C�B�B�B�B�B�I�I�I�j");

                    UpdateMainMessage("�@�@�@�i�E�E�E�@�E�E�E�@�E�E�E�j");

                    UpdateMainMessage("�A�C���F���A�������E�E�E");

                    UpdateMainMessage("�A�C���F�b�T�E�E�E�T���L���[�T���L���[�I�@�b�n�b�n�b�n�I");

                    UpdateMainMessage("�@�@�@�i���΂炭�̊ԁA���Z��ł́A�A�C���֏܎^�̔��肪����ꂽ�j");

                    UpdateMainMessage("�A�C���F�i�L�[�i�삳��A���ꂩ���肭�s���Ɨǂ��ȁE�E�E)");

                    UpdateMainMessage("�@�@�y��t��F�E�E�E�i�b�S�z���j�z");
                }
                else
                {
                    UpdateMainMessage("�U���Q�F�A�C���E�E�H�[�����X�l�A�{���͂��肪���ƃH�������܂����B");

                    UpdateMainMessage("�U���Q�F����������܂��Ă��E�E�E�����G�z�Ȃ���L�[�i��Ƃ̍�����s�����䂪�^���B");

                    UpdateMainMessage("�U���Q�F���̎��͂ǂ������V�̏�ցE�E�E�b�N�A�b�N�N�N�n�n�n");

                    UpdateMainMessage("�U���Q�F���z�����������܂����Ă����I�H�@�Q�n�n�n�n�n�n�I�I");

                    UpdateMainMessage("�A�C���F�����E�E�E");

                    UpdateMainMessage("�U���Q�F�����A�L�[�i��I�@�������Ă񂾂낤�Ȃ��I�H");

                    UpdateMainMessage("�U���Q�F�����񂶂�˂����I�H�@�������炲���e�l���ǂ��Ȃ邩�������Ă񂾂낤�Ȃ��I�H");

                    UpdateMainMessage("�A�C���F�����A�����c�t�d�k�͏I�����Ă�񂾁B�֌W�̖����b�͂���ȁB");

                    UpdateMainMessage("�U���Q�F�͂������I�H�������z�������ق�����Ă񂾂��I�H");

                    UpdateMainMessage("�A�C���F�b�O�E�E�E");

                    UpdateMainMessage("�U���Q�F�������͂Ƃ��ƂƑޏꂷ��񂾂ȁI�z���A�����̎�t�삳��A�]���𑁂��B");

                    UpdateMainMessage("�@�@�@�i�E�E�E��t��͏��������݂ɐk���Ă���E�E�E�j");

                    UpdateMainMessage("�U���Q�F�����Ƃ��ƁA�����̎�t��̓��[���ʂ�ɓ������ɗ����������Ă����I�H");

                    UpdateMainMessage("�U���Q�F�Q�n�n�n�n�n�n�I�I");

                    UpdateMainMessage("�A�C���F�b�`�E�E�E�����]�������ďI���񂾁A�L�[�i����B");

                    UpdateMainMessage("�@�@�y��t��F����ł͂c�t�d�k�͏I���ƂȂ�܂��B���҂Ƃ��]�������Ă��������܂��B�z");

                    UpdateMainMessage("�@�@�@�i�b�o�V���E�E���E�E�E�j");

                    UpdateMainMessage("�A�C���F�i�����́E�E�E�������A�I������̂��j");

                    UpdateMainMessage("�@�@�y��t��F�A�C���l�A�����_��" + GroundOne.WE2.DuelWin.ToString() + " �� " + GroundOne.WE2.DuelLose.ToString() + " �s�ƂȂ�܂��B�z");

                    UpdateMainMessage("�A�C���F���A���܂˂��ȁE�E�E���́E�E�E");

                    UpdateMainMessage("�@�@�y��t��F����̂�����A���҂��Ă���܂��B����ł́B�z");

                    UpdateMainMessage("�A�C���F���A�����E�E�E");

                    UpdateMainMessage("�A�C���F�i�㖡�̈������e���ȁE�E�E�b�N�\�E�E�E�j");

                    if (mc != null)
                    {
                        mc.CurrentLife = mc.MaxLife;
                        mc.CurrentSkillPoint = mc.MaxSkillPoint;
                        mc.CurrentMana = mc.MaxMana;
                    }
                    return;
                }
            }
            #endregion

            UpdateMainMessage("�@�@�y��t��F�A�C���l�A�����_��" + GroundOne.WE2.DuelWin.ToString() + " �� " + GroundOne.WE2.DuelLose.ToString() + " �s�ƂȂ�܂��B�z");

            UpdateMainMessage("�@�@�y��t��F����̂�����A���҂��Ă���܂��B����ł́B�z");

            if (fromGoDungeon)
            {
                CallSomeMessageWithAnimation("�_���W�����Q�[�g�̓�����ւƋ������҂���܂���");
            }
            else
            {
                CallSomeMessageWithAnimation("DUEL���Z��̓�����Q�[�g�ւƑ��҂���܂���");
            }

            if (OpponentDuelist == Database.DUEL_EONE_FULNEA)
            {
                if (fromGoDungeon)
                {
                    UpdateMainMessage("�A�C���F���ƁA�I������瑦���҂���B�{���ɗL�������킳�����ȁB");
                }
                else
                {
                    UpdateMainMessage("�A�C���F���ƁA�I������瓬�Z�������ւƑ��҂����̂��B�֗��ȃV�X�e�����ȁB");
                }
            }

            if (fromGoDungeon)
            {
                UpdateMainMessage("�A�C���F���āA�_���W�����s���Ƃ��邩�I", true);
            }
            else
            {
                UpdateMainMessage("�A�C���F���āADUEL���I�����������A�_���W�����ɂł��s���Ƃ��邩�I", true);
            }

            // [�x��]�F�I�u�W�F�N�g�̎Q�Ƃ��S�Ă̏ꍇ�A�N���X�Ƀ��\�b�h��p�ӂ��Ă�����R�[���������������B
            if (mc != null)
            {
                mc.CurrentLife = mc.MaxLife;
                mc.CurrentSkillPoint = mc.MaxSkillPoint;
                mc.CurrentMana = mc.MaxMana;
            }
        }

        string PracticeSwordLevel(MainCharacter player)
        {
            string[] targetName = { Database.POOR_PRACTICE_SWORD_ZERO, Database.POOR_PRACTICE_SWORD_1, Database.POOR_PRACTICE_SWORD_2, Database.COMMON_PRACTICE_SWORD_3, Database.COMMON_PRACTICE_SWORD_4, Database.RARE_PRACTICE_SWORD_5, Database.RARE_PRACTICE_SWORD_6, Database.EPIC_PRACTICE_SWORD_7, Database.LEGENDARY_FELTUS };
            string detectName = String.Empty;

            for (int ii = 0; ii < targetName.Length; ii++)
            {
                if ((player != null) && (player.MainWeapon != null) && (player.MainWeapon.Name == targetName[ii]))
                {
                    detectName = targetName[ii];
                    break;
                }
                if ((player != null) && (player.SubWeapon != null) && (player.SubWeapon.Name == targetName[ii]))
                {
                    detectName = targetName[ii];
                    break;
                }
                if ((player != null) && (player.MainArmor != null) && (player.MainArmor.Name == targetName[ii]))
                {
                    detectName = targetName[ii];
                    break;
                }
                if ((player != null) && (player.Accessory != null) && (player.Accessory.Name == targetName[ii]))
                {
                    detectName = targetName[ii];
                    break;
                }
                if ((player != null) && (player.Accessory2 != null) && (player.Accessory2.Name == targetName[ii]))
                {
                    detectName = targetName[ii];
                    break;
                }
                ItemBackPack[] backpack = player.GetBackPackInfo();
                for (int kk = 0; kk < backpack.Length; kk++)
                {
                    if ((backpack[kk] != null) && (backpack[kk].Name == targetName[ii]))
                    {
                        detectName = targetName[ii];
                        break;
                    }
                }

                if (detectName != string.Empty)
                {
                    // ���m�������߁A�����s�v
                    break;
                }
            }
            return detectName;
        }

        // �K���c���
        private void button4_Click(object sender, EventArgs e)
        {
            if (we.TruthCompleteArea1) we.AvailableEquipShop2 = true; // �O�҂Ŋ��Ɏ��m�̂��߁A����͕s�v�B
            if (we.TruthCompleteArea2) we.AvailableEquipShop3 = true; // �O�҂Ŋ��Ɏ��m�̂��߁A����͕s�v�B
            if (we.TruthCompleteArea3) we.AvailableEquipShop4 = true; // �O�҂Ŋ��Ɏ��m�̂��߁A����͕s�v�B
            if (we.TruthCompleteArea4) we.AvailableEquipShop5 = true; // �O�҂Ŋ��Ɏ��m�̂��߁A����͕s�v�B

            #region "�������E"
            if (GroundOne.WE2.RealWorld && GroundOne.WE2.SeekerEvent601 && !GroundOne.WE2.SeekerEvent604)
            {
                UpdateMainMessage("�A�C���F�K���c�f������A���܂����[�H");

                UpdateMainMessage("�K���c�F�A�C�����B�悭���Ă��ꂽ�B");

                UpdateMainMessage("�A�C���F����X�A�J���Ă܂����H");

                UpdateMainMessage("�K���c�F�����A�J�X���Ă���̂Ō��Ă����Ɨǂ��B");

                UpdateMainMessage("�A�C���F������I��������I���Ⴀ���������Ă��炤�Ƃ��邺�I�I");

                UpdateMainMessage("�K���c�F�D���Ȃ������Ă����Ɨǂ��B");

                UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E�@�E�E�E");

                UpdateMainMessage("�K���c�F�A�C����B���ꂩ��_���W�����֌������̂��ȁH");

                UpdateMainMessage("�A�C���F�͂��B");

                UpdateMainMessage("�K���c�F�A�C����A�ł͐S�\����������Đi���悤�B");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�K���c�͗���������ԂŁA�N�ւƂ��Ȃ��A�󒆂֌��n�߂��B";
                    md.ShowDialog();
                }

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

                UpdateMainMessage("�A�C���F�b�n�C�I�I");

                UpdateMainMessage("�K���c�F�S�����߂��悤���ȁB�ǂ����͋C���B");

                UpdateMainMessage("�K���c�F�A�C����A���i���Ȃ����B");

                UpdateMainMessage("�A�C���F�b�n�C�I�I�I");
                
                GroundOne.WE2.SeekerEvent604 = true;
                Method.AutoSaveTruthWorldEnvironment();
                Method.AutoSaveRealWorld(this.MC, this.SC, this.TC, this.WE, null, null, null, null, null, this.Truth_KnownTileInfo, this.Truth_KnownTileInfo2, this.Truth_KnownTileInfo3, this.Truth_KnownTileInfo4, this.Truth_KnownTileInfo5);
            }
            else if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd && GroundOne.WE2.SeekerEvent604)
            {
                UpdateMainMessage("�K���c�F�A�C����A���i���Ȃ����B", true);
            }
            #endregion
            #region "�I���E�����f�B�X�����O��"
            else if (we.AvailableDuelMatch && !we.MeetOlLandis)
            {
                if (!we.MeetOlLandisBeforeGanz)
                {
                    UpdateMainMessage("�A�C���F���񂿂�[�B");

                    UpdateMainMessage("�K���c�F�A�C����B");

                    UpdateMainMessage("�A�C���F�́A�͂��B�Ȃ�ł��傤�H");

                    UpdateMainMessage("�K���c�F�I�������A�ɗ��Ă��������B");

                    UpdateMainMessage("�A�C���F�́A�n�n�n�E�E�E�����ł������B�����͗ǂ������ł��ˁB");

                    UpdateMainMessage("�K���c�F�A�C����B");

                    UpdateMainMessage("�A�C���F�́A�͂��n�C�I");

                    UpdateMainMessage("�K���c�F���i���Ȃ����B");

                    UpdateMainMessage("�A�C���F�n�C�I�I�I���Ⴀ�A����Ŏ��炢�����܂��B");

                    UpdateMainMessage("�@�@�i�b�o�^���E�E�E�j");

                    UpdateMainMessage("�A�C���F�i�͂��E�E�E���肳��Ă邶��˂����E�E�E�j");

                    UpdateMainMessage("�A�C���F�i���傤���˂��A�������Z��֍s�������˂��݂Ă����ȁB�j", true);
                    we.MeetOlLandisBeforeGanz = true;
                }
                else
                {
                    UpdateMainMessage("�A�C���F�i���傤���˂��A�������Z��֍s�������˂��݂Ă����ȁB�j", true);
                }
                return;
            }
            #endregion
            #region "�������@�E�X�L���������Ă��炤�C�x���g"
            else if (we.TruthCompleteArea1 && !we.AvailableMixSpellSkill && mc.Level >= 21)
            {
                if (!we.AlreadyEquipShop)
                {
                    we.AlreadyEquipShop = true;

                    UpdateMainMessage("�A�C���F�ǂ����A���񂿂�[");

                    UpdateMainMessage("�K���c�F�A�C����A������Ƃ�����֗��Ȃ����B");

                    UpdateMainMessage("�A�C���F�b�Q�A���ł��傤�H");

                    UpdateMainMessage("�K���c�F�S�z�͂����B�����̊Ԃ������B");

                    UpdateMainMessage("�A�C���F�͂��B���ꂶ��E�E�E");

                    UpdateMainMessage("�A�C���F�i����A���̓����āA�_���W�����Q�[�g�֍s�����肩�H�j");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "�_���W�����Q�[�g���̍L��ɂ�";
                        md.ShowDialog();
                    }

                    UpdateMainMessage("�K���c�F�������ȁB");

                    UpdateMainMessage("�A�C���F�����ƁA�����܂��񎿖�Ȃ�ł����ǁH");

                    UpdateMainMessage("�K���c�F�Ȃ񂾂ˁB");

                    UpdateMainMessage("�A�C���F����ȗ��L��܂ŗ��Ĉ�̉����H");

                    UpdateMainMessage("�K���c�F�E�E�E�A�C����A������֗��Ă݂Ȃ����B");

                    UpdateMainMessage("�A�C���F��H����́E�E�E�ςȉ~�䂪�E�E�E");

                    UpdateMainMessage("�K���c�F��ԓ]�ڑ��u�A�����������炢�͂��邾�낤�B");

                    UpdateMainMessage("�A�C���F�}�W�������I�H���ꂪ�E�E�E�ւ������������I�I");

                    UpdateMainMessage("�A�C���F���H�@���Ă��ǂ����ɍs������Ȃ�ł����H");

                    UpdateMainMessage("�@�@�h���I�I �i�A�C���͓ˑR�˂���΂��ꂽ�j");

                    UpdateMainMessage("�A�C���F���I������I�I�@�����傿��I�I");

                    UpdateMainMessage("�K���c�F�A�C����A���i�Ȃ����B");

                    UpdateMainMessage("�@�@�b�o�V���E�E�D�D�D�D��");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "�A�C���͕ʂ̏ꏊ�ւƔ�΂���Ă��܂����B";
                        md.ShowDialog();
                    }

                    this.buttonHanna.Visible = false;
                    this.buttonDungeon.Visible = false;
                    this.buttonRana.Visible = false;
                    this.buttonGanz.Visible = false;
                    this.buttonPotion.Visible = false;
                    this.buttonDuel.Visible = false;
                    ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_SECRETFIELD_OF_FAZIL);
                    this.Invalidate();

                    UpdateMainMessage("�A�C���F�����ŁI");

                    UpdateMainMessage("�A�C���F�b�e�e�e�E�E�E�̐������������������A����o���ꂿ�܂����B");

                    UpdateMainMessage("�A�C���F�������E�E�E�s���悮�炢�����Ă���Ă������̂ɁB");

                    UpdateMainMessage("�H�H�H�F�M�N���A�A�C���E�E�H�[�����X���H");

                    UpdateMainMessage("�A�C���F���H���A�������������E�E�E");

                    UpdateMainMessage("�A�C���F���āA���킟���I�I");

                    UpdateMainMessage("�@�@�@�w����̊�̉E�ڂ��M�������Ɠ����Ă���x");

                    UpdateMainMessage("�A�C���F�r�b�N�������Ȃ��E�E�E�[�Ⴉ��");

                    UpdateMainMessage("�A�C���F�����ƁE�E�E");

                    UpdateMainMessage("�H�H�H�F�ǂ�A���������Ă��炤���B");

                    UpdateMainMessage("�@�@�@�w�[�Ⴊ�M�������Ɠ����͂��߂��I�x");

                    UpdateMainMessage("�A�C���F�����������Ȃ��E�E�E");

                    UpdateMainMessage("�H�H�H�F�E�E�E�@�E�E�E�@�E�E�E");

                    UpdateMainMessage("�H�H�H�F�X�y�������w��  ��  ���x�@�����");

                    UpdateMainMessage("�H�H�H�F�X�L�������w��  ��  �S��x���B�@�Ȃ�قǁB");

                    UpdateMainMessage("�A�C���F�Ȃ��I�I");

                    UpdateMainMessage("�H�H�H�F��_�ȍU���X�^�C���A����ɑ@�ׂȐ�p���������B");

                    UpdateMainMessage("�H�H�H�F�����ɂ͈Ӑ}�I�ɒ��ޕ������A�̐S�Ȗʂ͂������");

                    UpdateMainMessage("�H�H�H�F�����U�������łȂ��A���@�ɂ�������B�S�̓I�ȃI�[�����E���_�[");

                    UpdateMainMessage("�H�H�H�F�����Łw���܂�x�Ɣ��f����΁A��C�Ɏd�|����^�C�v�B");

                    UpdateMainMessage("�H�H�H�F�b�t�n�n�A�ʔ����B�@�����f�B�X�����������ċ����D�����B");

                    UpdateMainMessage("�A�C���F���A�t����m���Ă�̂��H");

                    UpdateMainMessage("�@�@�@�w�[�Ⴊ�X�ɃM�������Ɠ������I�x");

                    UpdateMainMessage("�H�H�H�F�������A�G��S�͂Œׂ��ɂ������A�l�q���̖ʂ������B");

                    UpdateMainMessage("�H�H�H�F��l�Ŏ��X�Ɠ|�����Ƃ�����A�`�[���A�g���l�����ē����^�C�v�B");

                    UpdateMainMessage("�H�H�H�F�{���Ȃ��l�ŏo����f�������邪�A�\�ɂ͌����Č����Ȃ��B");

                    UpdateMainMessage("�H�H�H�F�Ȃ�قǁA��������̎������ӎ����Ă���ȁB");

                    UpdateMainMessage("�H�H�H�F���̎蔲�������E�E�E遂�ł͂Ȃ��A���ӎ��I�ɂ����邢�́B");

                    UpdateMainMessage("�H�H�H�F�������ɁA���̂܂܂ł͒v���I�Ȕs�k�͊ԓ����ȁB");

                    UpdateMainMessage("�A�C���F���A���₢�₢��E�E�E");

                    UpdateMainMessage("�A�C���F�i���Ă��A���������疭�ɂ��̊��o�E�E�E�j");

                    UpdateMainMessage("�@�@�y�y�y�@�A�C���͔w�؂Ɉُ�ȋ��|�����o���Ă���B�@�z�z�z");

                    UpdateMainMessage("�A�C���F�i���̃{�P�t������˂����ǁA������ƈ�����|��������E�E�E�j");

                    UpdateMainMessage("�A�C���F�i�x�z�A�����E�E�E����Ȋ������j");

                    UpdateMainMessage("�A�C���F���񂽁A��̒N�Ȃ񂾂�H");

                    UpdateMainMessage("�H�H�H�F���̋����������ɕۂ�����A�x���S�͋M�N�Ȃ�ɍő�Ƃ����킯���ȁB");

                    UpdateMainMessage("�H�H�H�F������̎˒��A�M�N�̑z����y���ɒ����Ă���B");

                    UpdateMainMessage("�A�C���F���ȁI�I�I");

                    UpdateMainMessage("�@�@�y�y�y�@�A�C���͖c��Ȋ���̒��Ɋ������B�@�z�z�z");

                    UpdateMainMessage("�A�C���F�i�b���A���x�F�E�E�E�������x�F�E�E�E�I�I�I�j");

                    UpdateMainMessage("�A�C���F���āA���Ă��A���낻�됳�̂��������I�H");

                    UpdateMainMessage("�A�C���F�A���^�A�G����Ȃ��񂾂�I�H");

                    UpdateMainMessage("�H�H�H�F���̕ӂ����������B�������Ă�����B");

                    UpdateMainMessage("�H�H�H�F��̖��́w�V�j�L�A�E�J�[���n���c�x�ł���B");

                    UpdateMainMessage("�@�@�w�A�C���͊����X�D���ƈ����Ă����̂��������B�x");

                    UpdateMainMessage("�A�C���F�i�b�z�E�E�E�Ȃ񂾂����񂾍��̂́E�E�E�j");

                    UpdateMainMessage("�A�C���F�E�E�E�͂��߂܂��āA�A�C���ƌ����܂��B");

                    UpdateMainMessage("�A�C���F���āA�V�j�L�A���Ă܂����A�`����FiveSeeker�I�I�I");

                    UpdateMainMessage("�J�[���F�`����FiveSeeker�ȂǂƂ����p���������ʂ薼�͎~�߂Ă��炨���B");

                    UpdateMainMessage("�J�[���F��̓J�[���Ƃł��Ăׂ΂悢�B");

                    UpdateMainMessage("�A�C���F�͂��E�E�E�����ƁA���Ⴀ�̃J�[������B");

                    UpdateMainMessage("�A�C���F�K���c�f������͉��ł����։����H");

                    UpdateMainMessage("�J�[���F�M�N��b����悤�����Ă���B");

                    UpdateMainMessage("�A�C���F�����ł����H");

                    UpdateMainMessage("�J�[���F�������B");

                    UpdateMainMessage("�J�[���F��̏ꍇ�A�b������@�͐퓬�P���ł͂Ȃ��B");

                    UpdateMainMessage("�J�[���F�s�������܂����_�B");

                    UpdateMainMessage("�J�[���F�M�N�ɂ͂��ꂪ�����Ă���B");

                    UpdateMainMessage("�A�C���F�����ƁE�E�E��̓I�ɂ͉����H");

                    UpdateMainMessage("�J�[���F��̌������A���ׂĂ��L������B");

                    UpdateMainMessage("�A�C���F�L���I�H�@�ËL������Ď��ł����I�H");

                    UpdateMainMessage("�J�[���F�������B�ł͍s�����B");

                    UpdateMainMessage("�@�@�@�w�J�[���̍u�`�����X�Ə��ꎞ�ԑ������̂��E�E�E�x");

                    UpdateMainMessage("�J�[���F�������@�̊�b�Ɋւ��ẮA�ȏゾ�B");

                    UpdateMainMessage("�A�C���F�E�E�E");

                    UpdateMainMessage("�@�@�@�b�o�^�E�E�E�i�A�C���͂��̏�ŐÂ��ɗ������j");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "�A�C���͕������@�E�X�L���̊�b���K�������I";
                        md.ShowDialog();
                    }
                    we.AvailableMixSpellSkill = true;
                    GroundOne.WE2.AvailableMixSpellSkill = true;

                    UpdateMainMessage("�J�[���F�ǂ������B�܂��܂���͒������B");

                    UpdateMainMessage("�A�C���F�����E�E�E���������̂͑ʖڂ��E�E�E");

                    UpdateMainMessage("�A�C���F�Ȃ��A������Ƃł��ǂ������B���H�ŋ����Ă����H");

                    UpdateMainMessage("�J�[���F�ʖڂ��B");

                    UpdateMainMessage("�A�C���F�v�́A���Ɖ΂�g�ݍ��킹����Ď��Ȃ񂾂�H");

                    UpdateMainMessage("�J�[���F�Ⴄ�ȁB");

                    UpdateMainMessage("�A�C���F��̓I�Ɉ�񂾂������Ă����E�E�E");

                    UpdateMainMessage("�J�[���F�ʖڂ��B");

                    UpdateMainMessage("�A�C���F�΂Ɛ����đ������ǂ����Ď��Ȃ񂾂�H");

                    UpdateMainMessage("�J�[���F�Ⴄ�ȁB");

                    UpdateMainMessage("�A�C���F���ƈł͔��΁E�E�E�݂����ȁH");

                    UpdateMainMessage("�J�[���F�Ⴄ�ȁB");

                    UpdateMainMessage("�A�C���F�J�[���搶�A��񂾂����ނ��I");

                    UpdateMainMessage("�J�[���F�ʖڂ��B");

                    UpdateMainMessage("�A�C���F�g�z�z�E�E�E");

                    UpdateMainMessage("�J�[���F�S�z���H");

                    UpdateMainMessage("�A�C���F���H�����܂��A����Ă݂������o����̂�������");

                    UpdateMainMessage("�J�[���F�C���[�W�̊�{�́A�K�������m�����痈��B");

                    UpdateMainMessage("�J�[���F����̓W�J�́A���ꂼ��̒m�b����h�����Đ���B");

                    UpdateMainMessage("�A�C���F��A�܁A�܂��Ȃ�ƂȂ����̕ӂ́E�E�E");

                    UpdateMainMessage("�J�[���F�S�z����ȁB�M�N�͂��łɏK�����������R���B");

                    UpdateMainMessage("�A�C���F���I�H�@����ȁA�P����m�F���ĂȂ��ł����ǁH");

                    UpdateMainMessage("�J�[���F�N�ɋ���������Ǝv���Ă���B�����M���邩�H");

                    UpdateMainMessage("�A�C���F���A���₢��I����Ȃ��肶�Ⴒ�����܂���I�I");

                    UpdateMainMessage("�J�[���F�܂��悢�B��ԓ]�ڑ��u�𕜊������Ă������B�A�邪�ǂ��B");

                    UpdateMainMessage("�A�C���F�n�C�E�E�E�ǂ������肪�Ƃ��������܂����B");

                    UpdateMainMessage("�@�@�b�o�V���E�E�D�D�D�D��");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "�A�C���̓_���W�����Q�[�g�̗��L��ɖ߂��Ă���";
                        md.ShowDialog();
                    }

                    if (!we.AlreadyRest)
                    {
                        ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_EVENING);
                    }
                    else
                    {
                        ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                    }
                    this.buttonHanna.Visible = true;
                    this.buttonDungeon.Visible = true;
                    this.buttonRana.Visible = true;
                    this.buttonGanz.Visible = true;
                    this.buttonPotion.Visible = true;
                    this.buttonDuel.Visible = true;


                    UpdateMainMessage("�K���c�F�ǂ����������ˁB");

                    UpdateMainMessage("�A�C���F�ǂ��������E�E�E���������܂����B");

                    UpdateMainMessage("�K���c�F�������ˁB�ł͖߂�Ƃ��悤�B");

                    UpdateMainMessage("�A�C���F�͂��E�E�E");

                    UpdateMainMessage("�A�C���F�i�K���c�f����������������āA��������ȁE�E�E�j");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "�A�C���B�̓K���c�̕���܂Ŗ߂��Ă���";
                        md.ShowDialog();
                    }

                    UpdateMainMessage("�K���c�F�ł́A���V�͂���ŁB");

                    UpdateMainMessage("�A�C���F��������A������Ǝ��₪");

                    UpdateMainMessage("�K���c�F�����ˁB");

                    UpdateMainMessage("�A�C���F���̓]�ڂ��ꂽ�ꏊ���Ăǂ���ӂȂ�ł����H");

                    UpdateMainMessage("�K���c�F����̓J�[���݂̊�]�ɂ�蓚������B");

                    UpdateMainMessage("�A�C���F�����Ȃ̂��E�E�E����A�����������Ƃ���ꏊ�ȋC���������");

                    UpdateMainMessage("�K���c�F�ȂɁA������ǂ��m���Ă���ꏊ��B");

                    UpdateMainMessage("�A�C���F�����Ȃ�ł����H�@���`��E�E�E");

                    UpdateMainMessage("�A�C���F�܂��A�ǂ���B��������A���肪�Ƃ��������܂����I");

                    UpdateMainMessage("�K���c�F���ށA���i������B");

                    UpdateMainMessage("�@�@�@�w�K���c�͓X�̒��ւƖ߂��Ă������E�E�E�x");

                    UpdateMainMessage("�A�C���F�����O�_�O�_�ɔ�ꂽ�C�����邪�E�E�E");

                    UpdateMainMessage("�A�C���F�������E�E�E���ɂ��o����悤�ɂȂ�Ƃ�����");


                }
                else
                {
                    CallEquipmentShop();
                    mainMessage.Text = "";
                }
            }
            else if (we.TruthCompleteArea1 && we.AvailableMixSpellSkill && !buttonShinikia.Visible)
            {
                if (!we.AlreadyEquipShop)
                {
                    we.AlreadyEquipShop = true;

                    if ((mc.Level >= 21) && (!mc.FlashBlaze))
                    {
                        UpdateMainMessage("�A�C���F�ǂ����A���񂿂�[");

                        UpdateMainMessage("�K���c�F�A�C����A������Ƃ�����ցB");

                        UpdateMainMessage("�A�C���F���A�͂����ł��傤�H");

                        UpdateMainMessage("�K���c�F�ȑO���猩�āA�܂����������Ȃ����ƌ�����ȁB");

                        UpdateMainMessage("�A�C���F���₢��A����قǂł�����܂��񂪁E�E�E");

                        UpdateMainMessage("�K���c�F�J�[���݂̏��֍s���ė��Ȃ����B");

                        UpdateMainMessage("�A�C���F�b�Q�A�܂��ł����I�H");

                        UpdateMainMessage("�K���c�F���������Ă���A�C���A����Ȃ畡���n�ȂǗe�Ղ����낤�B");

                        UpdateMainMessage("�A�C���F���`��E�E�E���̐l���Ȃ񂾂�Ȃ��E�E�E");

                        UpdateMainMessage("�K���c�F�A�C����A���i���ɍs���Ă��Ȃ����B");

                        UpdateMainMessage("�A�C���F�n�C�E�E�E");

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "�_���W�����Q�[�g���̍L��ɂ�";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("�A�C���F�����ƁA�m�����̕ӂ������ȁE�E�E");

                        UpdateMainMessage("�A�C���F�I�b�P�[�A�����������ƁI");

                        UpdateMainMessage("�A�C���F������A�����]�����Ă��炨�����I");

                        UpdateMainMessage("�@�@�b�o�V���E�E�D�D�D�D��");

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "�A�C���͕ʂ̏ꏊ�ւƔ�΂���Ă��܂����B";
                            md.ShowDialog();
                        }

                        this.buttonHanna.Visible = false;
                        this.buttonDungeon.Visible = false;
                        this.buttonRana.Visible = false;
                        this.buttonGanz.Visible = false;
                        this.buttonPotion.Visible = false;
                        this.buttonDuel.Visible = false;
                        ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_SECRETFIELD_OF_FAZIL);

                        UpdateMainMessage("�A�C���F���ƂƁE�E�E�������݂�������");

                        UpdateMainMessage("�A�C���F�����ƁE�E�E�J�[���n���c�݂͂ǂ��ɁE�E�E");

                        UpdateMainMessage("�J�[���F�R�R���B");

                        UpdateMainMessage("�A�C���F���āA���킟���I�I");

                        UpdateMainMessage("�@�@�@�w�J�[���݂͓ˑR�������Ƃ��Ȃ��t�@�C�A�{�[��������Ă����I�x");

                        UpdateMainMessage("�A�C���F�b�Q�I�I�I");

                        UpdateMainMessage("�@�@�@�w�A�C���͂Ƃ����̔��f�Őg�����킵���x");

                        UpdateMainMessage("�J�[���F�z���z���z���I");

                        UpdateMainMessage("�@�@�@�w�J�[���݂͎��X�Ɩ��@�̒e�ۂ���������ł��Ă���I�I�x");

                        UpdateMainMessage("�A�C���F������I�I�����������I�I");

                        UpdateMainMessage("�@�@�@�w�A�C���͂T���̃t�@�C�A�{�[���炵���e�ۂ����Ƃ�������������x");

                        UpdateMainMessage("�A�C���F�b�^�^�A�^���}�^���}�I�I");

                        UpdateMainMessage("�A�C���F���̃{�P�t������T�����ǁA���񂽂������ꒃ���Ȃ����Ȃ�E�E�E");

                        UpdateMainMessage("�J�[���F�b�t�n�n�A�����Ƃ͌�����������Ɖ�����Ă�悤�����B");

                        UpdateMainMessage("�A�C���F�����A����Ȃ�������H����Ă���L������������B");

                        UpdateMainMessage("�J�[���F�]�����u��ł́A�G���҂��\���Ă�ꍇ�������B�C��t����̂��ȁB");

                        UpdateMainMessage("�A�C���F�i�O�O�E�E�E���̐l����ς�G�Ȃ񂶂�E�E�E�j");

                        UpdateMainMessage("�A�C���F�Ƃ������A�������Ɩ������@���������E�E�E");

                        UpdateMainMessage("�A�C���F�Ђ���Ƃ��č��̂��I�H");

                        UpdateMainMessage("�J�[���F���Ɖ΂̕������@�u�t���b�V���E�u���C�Y�v���B");

                        UpdateMainMessage("�J�[���F����Ă݂邪�ǂ��B");

                        UpdateMainMessage("�A�C���F���A�����Ȃ�ł����I�H");

                        UpdateMainMessage("�J�[���F��̋����A�o���Ă��邾�낤�B");

                        UpdateMainMessage("�J�[���F�����̒ʂ�ɂ��Ɨǂ��A�M�N�͏K���ς݂ł���ƌ������n�Y���B");

                        UpdateMainMessage("�A�C���F���E�E�E�������Ȃ��E�E�E���Ⴀ�E�E�E");

                        UpdateMainMessage("�@�@�@�w�A�C���͖��@�r���̍\�����n�߂��x");

                        UpdateMainMessage("�A�C���F�i�����E�E�E�΂ɖ������Y����悤�ɂ��āE�E�E�j");

                        UpdateMainMessage("�@�@�@�b�o�V���I�I�@�@�@");

                        UpdateMainMessage("�A�C���F�b�Q�I�I�I");

                        UpdateMainMessage("�J�[���F�܂��܂������A�ЂƂ܂��o�����悤���ȁB");

                        UpdateMainMessage("�A�C���F�����A����Ȗ{���ɂP��ڂŁE�E�E");

                        UpdateMainMessage("�J�[���F���������B");

                        UpdateMainMessage("�A�C���F���E�E�E�X�Q�F���I�I�@����I�I�I");

                        UpdateMainMessage("�J�[���F�����Ɗ����ŏK�����Ă����M�N�ɂƂ��ẮA�V�N�Ȋ��o�ł��낤�B");

                        UpdateMainMessage("�A�C���F�E�E�E���̍u�`�̂������ł����ˁH");

                        UpdateMainMessage("�J�[���F���R���B�����Ԃ�Ɩ���Ȏ��₾�ȁB");

                        UpdateMainMessage("�A�C���F���₢�₢��I�X���}�Z���ł����I�I");

                        UpdateMainMessage("�J�[���F����͂����܂ł��ȁA�܂�����Ɨǂ��B");

                        UpdateMainMessage("�A�C���F�z���g�ǂ������肪�Ƃ��������܂����I");

                        mc.FlashBlaze = true;
                        ShowActiveSkillSpell(mc, Database.FLASH_BLAZE);

                        if (!we.AlreadyRest)
                        {
                            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_EVENING);
                        }
                        else
                        {
                            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                        }
                        this.buttonHanna.Visible = true;
                        this.buttonDungeon.Visible = true;
                        this.buttonRana.Visible = true;
                        this.buttonGanz.Visible = true;
                        this.buttonPotion.Visible = true;
                        this.buttonDuel.Visible = true;


                        UpdateMainMessage("�K���c�F�ǂ����������ˁB");

                        UpdateMainMessage("�A�C���F�E�E�E�����܂����I�I");

                        UpdateMainMessage("�K���c�F���̗l�q�A�ǂ����g�ɕt�����悤���ˁB");

                        UpdateMainMessage("�A�C���F���ꂪ�����Ȃ�ł���I�I");

                        UpdateMainMessage("�A�C���F�n�߂���A�N���[���ɉr������������ł���I�I");

                        UpdateMainMessage("�K���c�F��قǊ����������ƌ�����B����قǂ��ˁH");

                        UpdateMainMessage("�A�C���F����ȑ̌��͏��߂Ăł�����I�I");

                        UpdateMainMessage("�A�C���F�����A�͂��߂�����ł���E�E�E�͂��߂�����E�E�E");

                        UpdateMainMessage("�K���c�F�A�C����A������͍D���ȃ^�C�~���O�Ŕނ̌��֖K��邪�悢�B");

                        UpdateMainMessage("�A�C���F���A�͂��B�܂��s���Ă݂܂��I");

                        UpdateMainMessage("�K���c�F���ށA���i������B");

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "�A�C���́u�Q�[�g���@�]�����u�v�֍s����悤�ɂȂ�܂����B";
                            md.ShowDialog();
                        }
                        buttonShinikia.Visible = true;
                        we.AvailableBackGate = true;
                        this.we.alreadyCommunicateCahlhanz = true; // �J�[���݂ɋ����Ă�������΂���̂��߁ATrue���w�肵�Ă����B
                    }


                }
                else
                {
                    CallEquipmentShop();
                    mainMessage.Text = "";
                }
            }
            #endregion
            #region "�S�K�J�n��"
            else if (we.TruthCompleteArea3 && !we.Truth_CommunicationGanz41)
            {
                we.Truth_CommunicationGanz41 = true;

                UpdateMainMessage("�A�C���F���񂿂�[�B");

                UpdateMainMessage("�K���c�F�A�C����A���ς�炸���C��������́B");

                UpdateMainMessage("�A�C���F�b�n�n�E�E�E");

                UpdateMainMessage("�K���c�F�S�K�ւ́A��͂�i�ނ��肩�B");

                UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                UpdateMainMessage("�A�C���F�͂��B");

                UpdateMainMessage("�K���c�F�~�߂����͂Ȃ��������ȁB");

                UpdateMainMessage("�A�C���F�����A�Ȃ�Ƃ����e���Ă݂����ł��B");

                UpdateMainMessage("�K���c�F�ӂށA���i���Ȃ����B");

                UpdateMainMessage("�A�C���F�n�C�A����ł́E�E�E");

                UpdateMainMessage("�K���c�F�҂��Ȃ����B");

                UpdateMainMessage("�K���c�F�A�C����A���������Ă���񂩂ˁB");

                UpdateMainMessage("�A�C���F���E�E�E�H");

                UpdateMainMessage("�K���c�F���K�p�̌����ȑO�n�����ł��낤�B");

                UpdateMainMessage("�A�C���F���A�����I�@������Ƒ҂��Ă��������B");

                UpdateMainMessage("�A�C���F�����ƁE�E�E���ꂾ�B�n�C�A�ǂ���");

                UpdateMainMessage("�K���c�F�ӂ�");

                UpdateMainMessage("�K���c�F�E�E�E");

                string detectName = PracticeSwordLevel(mc);

                if (detectName == Database.POOR_PRACTICE_SWORD_ZERO)
                {
                    UpdateMainMessage("�K���c�F���@" + detectName + "�@�����B");

                    UpdateMainMessage("�K���c�F�����������Ă����悤���ȁB");

                    UpdateMainMessage("�A�C���F���E�E�E�H");

                    UpdateMainMessage("�A�C���F���A�������Č����܂����H");

                    UpdateMainMessage("�K���c�F���̌��͎g����̐S�݂̍����ǂ݉����A�����ċ��ɐ������Ă䂭�B");

                    UpdateMainMessage("�K���c�F���̎�́A�Ȃ̐S�𐬒�������΁A���Ƌ��ɔ���I�Ȑi������������B");

                    UpdateMainMessage("�K���c�F�����`�����Ă���B");

                    UpdateMainMessage("�A�C���F���A������������ł����E�E�E");

                    UpdateMainMessage("�K���c�F�ł鎖�͂Ȃ��B����������΁A�܂��g���Ă݂Ȃ����B");

                    UpdateMainMessage("�A�C���F�n�A�n�C�I�ǂ������݂܂���ł����I");

                    UpdateMainMessage("�K���c�F�ӂ�K�v�͂Ȃ��B");

                    UpdateMainMessage("�K���c�F�A�C����A���i���Ȃ����B");

                    UpdateMainMessage("�A�C���F�n�C�A����ł͎��炵�܂��B");

                    UpdateMainMessage("   �������@�b�o�^���@�i�A�C���͕���̊O�ւƏo���j  ������");

                    UpdateMainMessage("�A�C���F�i���̌��E�E�E�����������̂��E�E�E�j");

                    UpdateMainMessage("�A�C���F�i�S�K�w�̓G����ɂ��̏�Ԃ���g������ɂȂ�˂����E�E�E�j");

                    UpdateMainMessage("�A�C���F�i�܂��A�C����������g���Ă݂邩�j");
                }
                else if ((detectName == Database.POOR_PRACTICE_SWORD_1) ||
                         (detectName == Database.POOR_PRACTICE_SWORD_2) ||
                         (detectName == Database.COMMON_PRACTICE_SWORD_3))
                {
                    UpdateMainMessage("�K���c�F���@" + detectName + "�@�����B");

                    UpdateMainMessage("�K���c�F�ق�̏��������A�������Ă���悤���ȁB");

                    UpdateMainMessage("�A�C���F�����E�E�E���ƂȂ������ǁA���������}�V�ɐU�镑����悤�ɂ͂Ȃ�܂����B");

                    UpdateMainMessage("�A�C���F�����荡�A�������Č����܂����H");

                    UpdateMainMessage("�K���c�F���̌��͎g����̐S�݂̍����ǂ݉����A�����ċ��ɐ������Ă䂭�B");

                    UpdateMainMessage("�K���c�F���̎�́A�Ȃ̐S�𐬒�������΁A���Ƌ��ɔ���I�Ȑi������������B");

                    UpdateMainMessage("�K���c�F�����`�����Ă���B");

                    UpdateMainMessage("�A�C���F���A������������ł����E�E�E");

                    UpdateMainMessage("�K���c�F�ł鎖�͂Ȃ��B����������΁A�܂��g���Ă݂Ȃ����B");

                    UpdateMainMessage("�A�C���F�n�A�n�C�I�ǂ������݂܂���ł����I");

                    UpdateMainMessage("�K���c�F�ӂ�K�v�͂Ȃ��B");

                    UpdateMainMessage("�K���c�F�A�C����A���i���Ȃ����B");

                    UpdateMainMessage("�A�C���F�n�C�A����ł͎��炵�܂��B");

                    UpdateMainMessage("   �������@�b�o�^���@�i�A�C���͕���̊O�ւƏo���j  ������");

                    UpdateMainMessage("�A�C���F�i���̌��E�E�E�����������̂��E�E�E�j");

                    UpdateMainMessage("�A�C���F�i�S�K�w�̓G����ɂ��̏�Ԃ���g������ɂȂ�˂����E�E�E�j");

                    UpdateMainMessage("�A�C���F�i�܂��A�C����������g���Ă݂邩�j");
                }
                else if ((detectName == Database.COMMON_PRACTICE_SWORD_4) ||
                         (detectName == Database.RARE_PRACTICE_SWORD_5) ||
                         (detectName == Database.RARE_PRACTICE_SWORD_6))
                {
                    UpdateMainMessage("�K���c�F���@" + detectName + "�@�����B");

                    UpdateMainMessage("�K���c�F�Ȃ��Ȃ��A�������Ă��Ă���悤���ȁB");

                    UpdateMainMessage("�A�C���F�����E�E�E�������A���̌��A�s�v�c�ł���ˁB");

                    UpdateMainMessage("�A�C���F�g���Ύg���قǏn���x���オ����Ă������E�E�E");

                    UpdateMainMessage("�A�C���F�g���悤�ɂ���āA�ǂ�ǂ�U���_���[�W���オ���Ă��Ă銴���������ł���B");

                    UpdateMainMessage("�K���c�F����̌����Ƃ���B");

                    UpdateMainMessage("�A�C���F���H");

                    UpdateMainMessage("�K���c�F���̌��͎g����̐S�݂̍����ǂ݉����A�����ċ��ɐ������Ă䂭�B");

                    UpdateMainMessage("�K���c�F���̎�́A�Ȃ̐S�𐬒�������΁A���Ƌ��ɔ���I�Ȑi������������B");

                    UpdateMainMessage("�K���c�F�����`�����Ă���B");

                    UpdateMainMessage("�A�C���F���A�������B�ǂ���ŁE�E�E");

                    UpdateMainMessage("�K���c�F���̒��q�ŁA���̌����g�����Ȃ��Ă݂Ȃ����B");

                    UpdateMainMessage("�K���c�F�A�C���A����͂����Ƌ����Ȃ��B");

                    UpdateMainMessage("�A�C���F�n�A�n�C�I�ǂ������肪�Ƃ��������܂��I");

                    UpdateMainMessage("�K���c�F�ӂ�K�v�͂Ȃ��A���i���Ȃ����B");

                    UpdateMainMessage("�A�C���F�n�C�A����ł͎��炵�܂��B");

                    UpdateMainMessage("   �������@�b�o�^���@�i�A�C���͕���̊O�ւƏo���j  ������");

                    UpdateMainMessage("�A�C���F�i���̌��E�E�E�m���ɈЗ͂��ǂ�ǂ�オ���Ă��Ă���E�E�E�j");

                    UpdateMainMessage("�A�C���F�i�S�K�w�A��C�Ɏg�����Ȃ���悤�ɐU�����Ă݂邩�j");
                }
                else if (detectName == Database.EPIC_PRACTICE_SWORD_7)
                {
                    UpdateMainMessage("�K���c�F���@" + detectName + "�@�����B");

                    UpdateMainMessage("�K���c�F�A�C����B����͂��̌����A���ł��邩�͗������Ă��邩�H");

                    UpdateMainMessage("�A�C���F�����E�E�E�ł����H");

                    UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                    UpdateMainMessage("�A�C���F�����A���������܂ł́E�E�E");

                    UpdateMainMessage("�K���c�F�ӂށA�ǂ��S�\�����B");

                    UpdateMainMessage("�K���c�F�A�C����A�����͂����ڂ̑O�ł��銴�o�͂��邩�ˁH");

                    UpdateMainMessage("�A�C���F�����E�E�E�����ȏ��A�������ƂȂ��́E�E�E");

                    UpdateMainMessage("�K���c�F�A�C����A����͂����\���ɋ����Ȃ����B");

                    UpdateMainMessage("�K���c�F�A�C����A��X�A���i���Ȃ����B");

                    UpdateMainMessage("�A�C���F�n�C�A�ǂ������肪�Ƃ��������܂��B");

                    UpdateMainMessage("   �������@�b�o�^���@�i�A�C���͕���̊O�ւƏo���j  ������");

                    UpdateMainMessage("�A�C���F�i���̌��ւ́E�E�E�����E�E�E�j");

                    UpdateMainMessage("�A�C���F�i���ƈꑧ�Ȋ����͂��Ă���E�E�E�j");

                    UpdateMainMessage("�A�C���F�i�����꒴���撣��Ƃ��邩�I�j");
                }
                else if (detectName == Database.LEGENDARY_FELTUS)
                {
                    UpdateMainMessage("�K���c�F���@" + detectName + "�@�����B");

                    UpdateMainMessage("�K���c�F�悭�������܂ŁB�������B");

                    UpdateMainMessage("�A�C���F�����A����͉����P�ɓ��������Ă��������ł�����B");

                    UpdateMainMessage("�K���c�F�����ł͂Ȃ��B�����������Ă������ʂ��B�ډ������鎖�͂Ȃ��B");

                    UpdateMainMessage("�A�C���F�͂��B");

                    UpdateMainMessage("�K���c�F�t�F���g�D�[�V���A������́A���̎�ɏ������Ă���B");

                    UpdateMainMessage("�A�C���F�����A�m���ɂ��̎�ɁB");

                    UpdateMainMessage("�K���c�F���鎖�Ȃ��A�i�߂邪�ǂ��B");

                    UpdateMainMessage("�A�C���F�͂��B");

                    UpdateMainMessage("�K���c�F������");

                    UpdateMainMessage("�K���c�F�����āA������ȁA�A�C����B");

                    UpdateMainMessage("�K���c�F���i��ӂ炸�A�i�߂�B�A�C���E�E�H�[�����X�B");

                    UpdateMainMessage("�A�C���F������܂����I");

                    UpdateMainMessage("   �������@�b�o�^���@�i�A�C���͕���̊O�ւƏo���j  ������");

                    UpdateMainMessage("�A�C���F�i�t�F���g�D�[�V���ɂ�艴�́E�E�E�j");

                    UpdateMainMessage("�A�C���F�i�������Ă�A�i�ނ񂾁j");

                    UpdateMainMessage("�A�C���F�i���͕K���A���̎�Łj");

                    UpdateMainMessage("�A�C���F�i������t���Ă݂���B�j");
                }
            }
            #endregion
            #region "�R�K�J�n��"
            else if (we.TruthCompleteArea2 && !we.Truth_CommunicationGanz31)
            {
                we.Truth_CommunicationGanz31 = true;

                if (!we.AlreadyEquipShop)
                {
                    UpdateMainMessage("�A�C���F���񂿂�[�B");

                    UpdateMainMessage("�K���c�F�A�C����B���悢��R�K�ւƐi�ނ��肩�B");

                    UpdateMainMessage("�A�C���F�͂��A��������X�^�[�g���������ł��B");

                    UpdateMainMessage("�K���c�F�ӂށA���V���猾�����͓��ɂȂ��B");

                    UpdateMainMessage("�K���c�F�A�C����A���i���Ȃ����B");

                    UpdateMainMessage("�A�C���F�����ƁA�n�C�I���肪�Ƃ��������܂����I�I");

                    UpdateMainMessage("�K���c�F�����A������Ă����˂΂Ȃ�񎖂�����B");

                    UpdateMainMessage("�A�C���F�i�b�Q�A���ɖ����ƌ������̂ɁE�E�E���̓W�J�́E�E�E�j");

                    UpdateMainMessage("�K���c�F�A�C����A�ǂ��֌������H");

                    UpdateMainMessage("�A�C���F�ǂ����āA�_���W�����R�K�ł��B");

                    UpdateMainMessage("�K���c�F�o�J�̐U��͕s�v�B��������Ɠ����Ȃ����B");

                    UpdateMainMessage("�A�C���F���`��A���������Ă��E�E�E");

                    if (GroundOne.WE2.TruthRecollection1 && GroundOne.WE2.TruthRecollection2)
                    {
                        UpdateMainMessage("�A�C���F�n�܂�̒n�ցB");

                        UpdateMainMessage("�A�C���F�L��ȑ����Ɩ����Ɋg������B");

                        UpdateMainMessage("�A�C���F�����ŉ��́A�P��������B");

                        UpdateMainMessage("�K���c�F�E�E�E�E�E�E�ӂށE�E�E�E�E�E");

                        UpdateMainMessage("�K���c�F���i���Ȃ����A�A�C����B");

                        UpdateMainMessage("�K���c�F�����ĕ����Ă͂Ȃ��B�悢�ȁH");

                        UpdateMainMessage("�A�C���F�����A�C���Ă���B");

                        UpdateMainMessage("�A�C���F��΂ɍ��x�����B");

                        UpdateMainMessage("�K���c�F���ށA�s���Ă��Ȃ����B�C�����ĂȁB");

                        UpdateMainMessage("�A�C���F�����A�����I");
                    }
                    else
                    {
                        UpdateMainMessage("�A�C���F�_���W�����ŉ��w���B");

                        UpdateMainMessage("�A�C���F���͐�΂ɂ��̃_���W�����𐧔e���Ă݂���I");

                        UpdateMainMessage("�K���c�F�ӂށA���̐����A�Y��ʂ悤�ɂȁB");

                        UpdateMainMessage("�A�C���F�K���c�f������Ƙb���Ă���ƌ��C���o���A�T���L���[�B");

                        UpdateMainMessage("�K���c�F���������͂��ʂ悤�ɂȁA�������������萶���Ȃ����B");

                        UpdateMainMessage("�A�C���F�����A���Ⴀ�s���Ă��邺�I");
                    }
                    we.AlreadyEquipShop = true;
                }
                else
                {
                    CallEquipmentShop();
                    mainMessage.Text = "";
                }
            }
            #endregion
            #region "�Q�K�J�n��"
            else if (we.TruthCompleteArea1 && !we.Truth_CommunicationGanz21)
            {
                if (!we.AlreadyEquipShop)
                {
                    UpdateMainMessage("�A�C���F���񂿂�[�B");

                    UpdateMainMessage("�K���c�F�A�C���B�Q�K�֌������悤���ȁB");

                    UpdateMainMessage("�A�C���F���A�����B�������炻�̂���ł��B");

                    UpdateMainMessage("�K���c�F�Ȃ�΁A����ł������čs���Ɨǂ����낤�B");

                    CallSomeMessageWithAnimation("�A�C����" + Database.POOR_PRACTICE_SWORD_ZERO + "����ɓ��ꂽ�B");

                    UpdateMainMessage("�A�C���F����́E�E�E���K�p�̌��H");

                    UpdateMainMessage("�K���c�F���̕���ɂ͓���Ȍ��ʂ��������߂��Ă���B");

                    UpdateMainMessage("�A�C���F�����Ȃ�ł����H");

                    UpdateMainMessage("�K���c�F���V�Ȃ�ɍl���Ă݂����A�A�C����B");

                    UpdateMainMessage("�A�C���F�n�C�B");

                    UpdateMainMessage("�K���c�F�E�E�E����A���O�Ȃ�Ɏg���Ă݂�Ɨǂ��B");

                    UpdateMainMessage("�A�C���F�����ƁA�ǂ��������ł��傤���H");

                    UpdateMainMessage("�K���c�F�A�C����A���i���Ȃ����B");

                    UpdateMainMessage("�A�C���F�����ƁA�n�C�I���肪�Ƃ��������܂����I�I");

                    UpdateMainMessage("   �������@�b�o�^���@�i�A�C���͕���̊O�ւƏo���j  ������");

                    UpdateMainMessage("�A�C���F�i�ǂ��݂Ă�����͒P�Ȃ���K�p�̌������E�E�E�j");

                    UpdateMainMessage("�A�C���F�i�E�E�E����A����Ȃ킯���˂���ȁj");

                    UpdateMainMessage("�A�C���F�i������A���������Ȃ񂾂��A�g���Ă݂�Ƃ��邩�I�j");

                    GetItemFullCheck(mc, Database.POOR_PRACTICE_SWORD_ZERO);

                    we.Truth_CommunicationGanz21 = true;
                    we.AlreadyEquipShop = true;
                }
                else
                {
                    CallEquipmentShop();
                    mainMessage.Text = "";
                }
            }
            #endregion
            #region "�P����"
            else if (this.firstDay >= 1 && !we.Truth_CommunicationGanz1)
            {
                // if (!we.AlreadyRest) //  1���ڂ̓A�C�����N�����΂���Ȃ̂ŁA�{�t���O�𖢎g�p�Ƃ��܂��B
                if (!we.AlreadyEquipShop)
                {
                    UpdateMainMessage("�A�C���F�K���c�f������A���܂����[�H");

                    UpdateMainMessage("�K���c�F�A�C�����B�悭���Ă��ꂽ�B");

                    UpdateMainMessage("�A�C���F����X�A�J���Ă܂����H");

                    UpdateMainMessage("�K���c�F�����A�����̓��@�X�^�ꂩ��̕����z�����ǂ��ĂȁB�J�X���Ă���̂Ō��Ă����Ɨǂ��B");

                    UpdateMainMessage("�A�C���F������I��������I���Ⴀ���������Ă��炤�Ƃ��邺�I�I");

                    we.AlreadyEquipShop = true;
                    we.AvailableEquipShop = true;
                    we.Truth_CommunicationGanz1 = true; // �������ڂ̂݁A���i�A�K���c�A�n���i�̉�b�𕷂������ǂ������肷�邽�߁A������TRUE�Ƃ��܂��B

                    CallEquipmentShop();
                    mainMessage.Text = "";

                    UpdateMainMessage("�A�C���F�f������A�܂����邺�B");

                    UpdateMainMessage("�K���c�F�A�C����B���ꂩ��_���W�����֌������̂��ȁH");

                    UpdateMainMessage("�A�C���F�͂��B");

                    UpdateMainMessage("�K���c�F�A�C����A�ł͐S�\����������Đi���悤�B");

                    UpdateMainMessage("�A�C���F�b�}�W�������I�H�n�n�A��������I���肪�Ƃ��������܂��I");

                    UpdateMainMessage("�K���c�F�_���W�����ŎE���������X�^�[����́A���ɗ��ޗ����������̂��B");

                    UpdateMainMessage("�A�C���F�b�n�C�I");

                    UpdateMainMessage("�K���c�F�����X�^�[��蓾���镔�ށA�f�ނ����V�̏��֎����Ă���Ɨǂ��B");

                    UpdateMainMessage("�A�C���F�b�n�C�I�I");

                    UpdateMainMessage("�K���c�F����畔�ށA�f�ނ�g�ݍ��킹�A���V���r�ɂ��������ĐV�����������낤�B");

                    UpdateMainMessage("�A�C���F�b�n�C�I�I�I");

                    UpdateMainMessage("�K���c�F�A�C����A���i���Ȃ����B");

                    UpdateMainMessage("�K���c�F�ł͗��񂾂��B");

                    UpdateMainMessage("�A�C���F�b�n�C�I�@���肪�Ƃ��������܂����I�I");

                    UpdateMainMessage("   �������@�b�o�^���@�i�A�C���͕���̊O�ւƏo���j  ������");

                    UpdateMainMessage("�A�C���F�i�҂Ă�E�E�E����͂Ђ���Ƃ��āE�E�E�j");

                    UpdateMainMessage("�A�C���F�i�Ō㌋�ǁu���i���Ȃ����v���������Ă˂���ȁE�E�E�j");

                    UpdateMainMessage("�A�C���F�i�ł��܂��A�K���c�f������̐V������������B�y���݂��ȁB�j");

                    UpdateMainMessage("�A�C���F�i�����X�^�[���瓾��ꂽ���ށE�f�ނ̓K���c�f������̃g�R�֎����Ă����Ƃ��邩�B�j");
                }
                else
                {
                    CallEquipmentShop();
                    mainMessage.Text = "";
                }
            }
            #endregion
            #region "���̑�"
            else
            {
                CallEquipmentShop();
                mainMessage.Text = "";
            }
            #endregion
        }

        private void GoToKahlhanz()
        {
            this.buttonHanna.Visible = false;
            this.buttonDungeon.Visible = false;
            this.buttonRana.Visible = false;
            this.buttonGanz.Visible = false;
            this.buttonPotion.Visible = false;
            this.buttonDuel.Visible = false;
            this.buttonShinikia.Visible = false;
            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_SECRETFIELD_OF_FAZIL);
        }
        private void BackToTown()
        {
            if (!we.AlreadyRest)
            {
                ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_EVENING);
            }
            else
            {
                ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
            }
            this.buttonHanna.Visible = true;
            this.buttonDungeon.Visible = true;
            this.buttonRana.Visible = true;
            this.buttonGanz.Visible = true;
            this.buttonPotion.Visible = true;
            this.buttonDuel.Visible = true;
            this.buttonShinikia.Visible = true;
        }

        // �J�[���݂̍u�` / �t�@�[�W���{�a
        private void button8_Click(object sender, EventArgs e)
        {
            #region "�t�@�[�W���{�a or �J�[���n���c�݂̌P�����I��"
            if (we.AvailableFazilCastle)
            {
                using (SelectDungeon sd = new SelectDungeon())
                {
                    sd.StartPosition = FormStartPosition.Manual;
                    sd.Location = new Point(this.Location.X + 20, this.Location.Y + 525);
                    sd.MaxSelectable = 2;
                    sd.FirstName = "�J�[���݂̌P����";
                    sd.SecondName = "�t�@�[�W���{�a";
                    sd.AdjustWidth = 200;
                    sd.ShowDialog();
                    if (sd.TargetDungeon == -1)
                    {
                        return;
                    }
                    else if (sd.TargetDungeon == 1)
                    {
                        if (we.alreadyCommunicateCahlhanz)
                        {
                            UpdateMainMessage("�A�C���F�J�[���n���c�݂ɂ͂܂����x�����Ă��炤�Ƃ��悤�B", true);
                            return;
                        }
                        else
                        {
                            UpdateMainMessage("�A�C���F�J�[���݂̌P����֕����Ƃ��邩�B", true);
                            System.Threading.Thread.Sleep(1000);
                        }
                    }
                    else
                    {
                        if (!we.AvailableOneDayItem && we.AlreadyCommunicateFazilCastle)
                        {
                            UpdateMainMessage("�A�C���F�t�@�[�W���{�a�́A�܂����x�s���Ă݂悤�B", true);
                            return;
                        }
                        if (we.AvailableOneDayItem && we.AlreadyCommunicateFazilCastle && we.AlreadyGetOneDayItem && we.AlreadyGetMonsterHunt)
                        {
                            UpdateMainMessage("�A�C���F�t�@�[�W���{�a�́A�܂����x�s���Ă݂悤�B", true);
                            return;
                        }

                        #region "���߂Ẵt�@�[�W���{�a"
                        if (!we.Truth_Communication_FC31)
                        {
                            UpdateMainMessage("���i�F������@�t�@�[�W���{�a�ɍs���Ă݂�́H");

                            UpdateMainMessage("�A�C���F�����A���̂��肾�B�Ȃ񂾃��P�Ɋy���������ȁB");

                            UpdateMainMessage("���i�F�����āA����̃G���~�l�ɉ��\��������񂾂��́A�y�����Ȃ����");

                            UpdateMainMessage("�A�C���F�ȂɁE�E�E����Ȃ��̂Ȃ̂��H");

                            UpdateMainMessage("���i�F����Ⴛ����B�����Ȃ�N�ł�������B�o�J�A�C�����m��Ȃ����Ȃ�����B");

                            UpdateMainMessage("�A�C���F����܂��E�E�E�����������͉��ɂ͊m���ɕ������B");

                            UpdateMainMessage("���i�F�܂��A�C�C��敪����Ȃ��Ă��B�������A�s���܂����@");

                            UpdateMainMessage("�A�C���F�������A�߂��炵���@�����ǂ��ȁB�܂��s���Ƃ��邩�I");

                            UpdateMainMessage("���i�F�h�߂��炵���h�Ȃ�����ˁA�����@���ǂ��ł����@");

                            UpdateMainMessage("�A�C���F��A�����������������B�b�n�n�n�E�E�E�s�������B");

                            System.Threading.Thread.Sleep(1000);
                            CallFazilCastle();

                            UpdateMainMessage("���i�F���Ⴀ�A�����̒�������ˁB�Y��Ȃ��ł�˃z���g�B");

                            UpdateMainMessage("�A�C���F�����A���𗹉��I");
                        }
                        else if (!we.Truth_Communication_FC32)
                        {
                            UpdateMainMessage("���i�F���Ⴀ�A�t�@�[�W���{�a�ɍs���܂����");

                            UpdateMainMessage("�A�C���F������A�����l�A���ܗl�Ƃ��Ζʂ��ȁB");

                            UpdateMainMessage("�A�C���F���Ⴀ�A�]�����邺�I");

                            System.Threading.Thread.Sleep(1000);
                            CallFazilCastle();

                            UpdateMainMessage("�A�C���F�ӂ��A�߂���ƁB");

                            UpdateMainMessage("���i�F���[��E�E�E");

                            UpdateMainMessage("�A�C���F�Ȃ񂾁A�ǂ����������H");

                            UpdateMainMessage("���i�F����̂ǂ�����V�ɓ����郏�P�Ȃ̂��A������Ƌ����Ă��炦�Ȃ�������H");

                            UpdateMainMessage("�A�C���F�����A���̌����B");

                            UpdateMainMessage("�A�C���F�Ȃ�Č����񂾂낤�ȁB");

                            UpdateMainMessage("�A�C���F�v���������������Ƃ���B");

                            UpdateMainMessage("���i�F����B");

                            UpdateMainMessage("�A�C���F���̗v���ɑ��肪�����Ă��ꂽ�Ƃ���B");

                            UpdateMainMessage("���i�F���񂤂�B");

                            UpdateMainMessage("�A�C���F���ŁA�v���͖�������郏�P���B");

                            UpdateMainMessage("���i�F������ˁA���ꂪ�ړI�Ȃ񂾂���B");

                            UpdateMainMessage("�A�C���F���ꂶ��܂�˂�����H");

                            UpdateMainMessage("�A�C���F�������U�v���͒u���Ƃ��āA���̋@��ɂ���̂��B");

                            UpdateMainMessage("���i�F�Ȃ�ŁA���[�Ȃ�̂�H�@�Ӗ����킩��Ȃ���B");

                            UpdateMainMessage("�A�C���F�Ȃ�ł��Č����Ă��ȁE�E�E���ƂȂ��Ƃ����E�E�E");

                            UpdateMainMessage("���i�F���`��E�E�E");

                            UpdateMainMessage("���i�F�_���A�S���R�킩��Ȃ���B");

                            UpdateMainMessage("�A�C���F�n�n�n�E�E�E���������B");

                            UpdateMainMessage("���i�F�܂��A�ǂ���B���̃T���f�B���Đl�����ꂵ���������������A����ł��傤�Ǘǂ��̂����m��Ȃ���ˁB");

                            UpdateMainMessage("�A�C���F�܂��܂��A�����Ȃ�Đl���ꂼ�ꂳ�B�y�����s�������I");

                            UpdateMainMessage("���i�F�͂������E�E�E�ʂɂ������ǁA������͗��ނ�˃z���g�B");

                            UpdateMainMessage("�A�C���F���𗹉��I�@�C���Ă����I");
                        }
                        #endregion
                        #region "�Q�x�ڈȍ~�̒ʏ����"
                        else
                        {
                            UpdateMainMessage("�A�C���F�����A�t�@�[�W���{�a�ɂł��s���Ă݂邩�B");

                            System.Threading.Thread.Sleep(1000);
                            CallFazilCastle();
                        }
                        #endregion
                        return;
                    }
                }
            }
            #endregion
            #region "�l�K�J�n��"
            if (we.TruthCompleteArea3 && !we.Truth_CommunicationSinikia41 && !we.alreadyCommunicateCahlhanz)
            {
                we.Truth_CommunicationSinikia41 = true;
                we.alreadyCommunicateCahlhanz = true;
                GoToKahlhanz();

                UpdateMainMessage("�A�C���F���ƂƁE�E�E�������݂�������");

                UpdateMainMessage("�A�C���F���̂����܂���H");

                UpdateMainMessage("�A�C���F�E�E�E�J�[���搶�H");

                UpdateMainMessage("�A�C���F�E�E�E");

                UpdateMainMessage("�A�C���F�i�E�E�E����̂͂킩��񂾂��E�E�E�j");

                UpdateMainMessage("�A�C���F�E�E�E");

                UpdateMainMessage("�A�C���F�J�[���搶�A�o�Ă��Ă���B");

                UpdateMainMessage("�A�C���F������Ƙb������񂾁A���ށB");

                UpdateMainMessage("�A�C���F�E�E�E");

                UpdateMainMessage("�@�@�@�w�b�o�V���I�I�x�i�J�[���͈�u�ł��̏�Ɏp���������I");

                UpdateMainMessage("�J�[���F�M�N���B");

                UpdateMainMessage("�A�C���F���ށA�ꐶ�̂��肢���B�����Ă���B");

                UpdateMainMessage("�J�[���F�����Ă݂邪�ǂ��B");

                UpdateMainMessage("�A�C���FFiveSeeker�Ɂy�����z�ɂ��Ă��B");

                UpdateMainMessage("�J�[���F�y�����z�ւ̖₢�������B�\���Ă݂�B");

                UpdateMainMessage("�A�C���FFiveSeeker�B�͂ǂ����Ă����܂ł̋�������ɓ��ꂽ�񂾁H");

                UpdateMainMessage("�A�C���F���ށA�����Ă���B");

                UpdateMainMessage("�J�[���F���m�ȉ��͖����B");

                UpdateMainMessage("�J�[���F�S�Ă͓��X�̐ςݏd�ˁB");

                UpdateMainMessage("�A�C���F����͉������i������Ă���肾�B�ǂ����Ⴄ�H");

                UpdateMainMessage("�J�[���F���K�ʂ͂����炭�����x�B���̎��������炭�����ł��낤�B");

                UpdateMainMessage("�A�C���F�E�E�E");

                UpdateMainMessage("�A�C���F�_�_��ς������Ă���B");

                UpdateMainMessage("�A�C���F�J�[���搶�͖�������̐��E����ȁH");

                UpdateMainMessage("�J�[���F�����ɂ��B");

                UpdateMainMessage("�A�C���F���Ⴀ�ǂ����āA�����܂ł̃X�s�[�h���o����񂾁H");

                UpdateMainMessage("�A�C���F�Z�ɒ����Ă��郔�F���[�Ƃقړ��N���X�̃X�s�[�h�ȋC������񂾂��E�E�E");

                UpdateMainMessage("�J�[���F�A�[�e�B�̎����B");

                UpdateMainMessage("�J�[���F�䎩�g���A�z�قǂ̃X�s�[�h�������o�����͂Ȃ��B");

                UpdateMainMessage("�A�C���F����A���F���[�قǂ���Ȃ��ɂ��Ă��ł���B");

                UpdateMainMessage("�A�C���F����ɂ������āA���������B");

                UpdateMainMessage("�A�C���F���������̔閧��������Ď��Ȃ񂶂�Ȃ��ł����H");

                UpdateMainMessage("�J�[���F��b�I�Ȓb���͑ӂ鎖�͌����ĂȂ��B");

                UpdateMainMessage("�J�[���F�X�s�[�h���グ�閂�@�����푽�l�B����ɉ�����{�I�ȑ��x�����������P���͓��X�̐ςݏd�ˁB");

                UpdateMainMessage("�J�[���F���̂��Ƃ͋M�N�Ƃďd�X���m�̂͂��B�Ⴄ���ˁB");

                UpdateMainMessage("�A�C���F�܁A�܂�����Ⴛ�������E�E�E");

                UpdateMainMessage("�J�[���F�M�N�̌����y�����z�Ƃ�����`�́A����₨���Ƃ��Ă���H");

                UpdateMainMessage("�A�C���F���`��E�E�E����������ƂȁE�E�E");

                UpdateMainMessage("�J�[���F���S�����ȋ����ȂǁA���̐��ɂ͑��݂��Ȃ��B");

                UpdateMainMessage("�J�[���F���X�̒b���A�����āA���L���m���̏K���A�����E�E�E");

                UpdateMainMessage("�A�C���F���A���₢�₢��B������ƃ^�C���I");

                UpdateMainMessage("�A�C���F���������b�͌��ƌ����قǕ����Ă�񂾁B���������b����˂��񂾁B���ށI");

                UpdateMainMessage("�J�[���F�ł́A����x�\���Ă݂�B");

                UpdateMainMessage("�J�[���F�M�N�̒m�肽���y�����z�Ƃ͉����H");

                UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E�@�E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E");
                
                UpdateMainMessage("�A�C���F�J�[���搶�A�t�@�C�A�E�{�[���Q�A������x�����Ă���Ȃ����H");

                UpdateMainMessage("�J�[���F�e�Ղ����ƁA�ł͍s�����B");

                UpdateMainMessage("�@�����@�J�[���́A���̏�ő̈ʂ��ق�̏��������ω������E�E�E�@����");

                UpdateMainMessage("�J�[���F�b�t�@�C�A�E�{�[��");

                UpdateMainMessage("�@�����@�b�{�A�b�{�V���E�D�D���E�E�E�@����");

                UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                UpdateMainMessage("�A�C���F���ށI�@������񂾂��I");

                UpdateMainMessage("�J�[���F�b�t�B");

                UpdateMainMessage("�J�[���F�b�t�n�n�n�n�A�M�N�͖{���ɖʔ����B");

                UpdateMainMessage("�J�[���F�ǂ����낤�A�ł͍s�����B");

                UpdateMainMessage("�@�����@�J�[���́A�����̕������ق�̏��������h�炵�n�߁E�E�E�@����");

                UpdateMainMessage("�A�C���F�b�^�A�^�C���I�����̂���I�I");

                UpdateMainMessage("�J�[���F���ށB");

                UpdateMainMessage("�A�C���F���̎��_�ŁA�r���͎n�܂��Ă���̂��H");

                UpdateMainMessage("�J�[���F�܂����B");

                UpdateMainMessage("�A�C���F�����A���܂˂��E�E�E�~�߂��܂��āB���x�͎~�߂˂�����B");

                UpdateMainMessage("�J�[���F�ǂ��A�ł͂�����x�s�����B");

                UpdateMainMessage("�@�����@�J�[���́A�E��̐�������ɓ��삳���E�E�E�@����");

                UpdateMainMessage("�A�C���F�i�E�E�E���񃂁[�V�����������ɈႤ�E�E�E�j");

                UpdateMainMessage("�J�[���F�b�t�@�C�A�E�{�[��");

                UpdateMainMessage("�A�C���F�i����ς�E�E�E�r���^�C�~���O�Ŋ��ɉ��̉�2�o�Ă���B�j");

                UpdateMainMessage("�@�����@�b�{�@����");

                UpdateMainMessage("�A�C���F�i���̏u�ԂɂQ�������ă��P�ł��˂�����E�E�E�j");

                UpdateMainMessage("�@�����@�b�{�V���E�E�E�D�D�D���E�E�E�@����");

                UpdateMainMessage("�A�C���F�i�Q�C���E�E�B���h�ł��˂���ȁE�E�E�j");
                
                UpdateMainMessage("�A�C���F�i���Ă��Ƃ́A�ςȃJ���N���⏬�׍H�͂˂��ȁE�E�E�j");
                
                UpdateMainMessage("�A�C���F�i�ǂ��Ȃ��Ă񂾁E�E�E�����m�̂����ɁA���̃X�s�[�h�E�E�E�j");

                UpdateMainMessage("�J�[���F�ȏ�");

                UpdateMainMessage("�A�C���F���₟�E�E�E");

                UpdateMainMessage("�A�C���F����ρA�X�Q�F��E�E�E�M�����˂����B");

                UpdateMainMessage("�A�C���F�J�[���搶�A����ϋ����������B");

                UpdateMainMessage("�J�[���F�M�N�̖₢�ɑ΂���𓚂͌�����ꂽ���H");

                UpdateMainMessage("�A�C���F������E�E�E");
                
                UpdateMainMessage("�A�C���F��������񕪂���˂��I�I");
                
                UpdateMainMessage("�A�C���F�A�[�b�n�b�n�b�n�I");

                UpdateMainMessage("�J�[���F�b�t�n�n�A�������ȓz���B");

                UpdateMainMessage("�A�C���F�����ق�̏����ł��͂߂邩�Ǝv������ł����A�����܂��܂��ł��B");
                
                UpdateMainMessage("�A�C���F������΂ɁA�{�P�t����J�[���搶�ɒǂ����Č����܂��I");

                UpdateMainMessage("�J�[���F�m���͑S�Ă̌��A�Y���ȁB");

                UpdateMainMessage("�A�C���F�͂��A�ǂ������肪�Ƃ��������܂����I");

                BackToTown();
            }
            #endregion
            #region "�O�K�J�n��"
            else if (we.TruthCompleteArea2 && !we.Truth_CommunicationSinikia31 && !we.alreadyCommunicateCahlhanz)
            {
                if (!we.Truth_CommunicationLana31)
                {
                    UpdateMainMessage("�A�C���F����E�E�E���̑O�ɁA���i�ɂЂƂ܂����A���Ă������B", true);
                    return;
                }
                if (!we.Truth_CommunicationOl31)
                {
                    UpdateMainMessage("�A�C���F����E�E�E���̑O�ɁA�t���ɂЂƂ܂����A���Ă������B", true);
                    return;
                }
                we.Truth_CommunicationSinikia31 = true;
                we.alreadyCommunicateCahlhanz = true;
                we.AlreadyCommunicateFazilCastle = true;

                UpdateMainMessage("�A�C���F�悵�A�]�����u�Q�[�g�ɒ��������B");

                UpdateMainMessage("���i�F�m���A���̕ӂ�̖؂̎}��E�E�E�����ƂˁB");

                UpdateMainMessage("���i�F�����A��������A�R���ˁ�");

                UpdateMainMessage("�A�C���F�ǂ��݂Ă����ʂ̎}�����ǂȁB");

                UpdateMainMessage("���i�F�P�Ȃ�}�������班���Ȃ��邮�炢�ł���H");

                UpdateMainMessage("�A�C���F�܂��A���������ǂȁB�����Ƀw�V�܂�Ȃ�H");

                UpdateMainMessage("���i�F������ƁA����ˁB�A���n�̓o�J�A�C���݂����Ɋ�䂶��Ȃ��񂾂���B");

                UpdateMainMessage("�A�C���F�i����������w�V�܂����Ȃ̂��E�E�E�j");

                UpdateMainMessage("���i�F������Ɓ�");

                UpdateMainMessage("�@�@�@�y�]�����u�Q�[�g����������n�߂��I�z");

                UpdateMainMessage("�@�@�@�y�E�E�E�D�D�D�u�D���E�E�D�D�D���E�E�E�z");

                UpdateMainMessage("���i�F�b�z���A���Č��āI�@������ł����");

                UpdateMainMessage("�A�C���F�ւ��A������l�����������ς���Ă�ȁB����������˂����I");

                UpdateMainMessage("���i�F����ő����t�@�[�W���{�a�֒ʂ���Q�[�g�ɂȂ�������A�s���Ă݂܂����");

                UpdateMainMessage("�A�C���F��������A���ꂶ�ᑁ���s�����I");

                UpdateMainMessage("�@�@�@�y�A�C���ƃ��i�͓]�����u�Q�[�g�ւƑ����^�񂾁E�E�E�z");

                UpdateMainMessage("�@�@�@�y���̏u�Ԃ������z");

                Blackout();

                UpdateMainMessage("�@�@�@�y�E�E�E�b�p�L�C�B�B�B�����E�E�E�z");

                UpdateMainMessage("�A�C���F�i�b�Q�A�Ȃ񂾍��̉��I�I�j");

                UpdateMainMessage("�@�@�@�y�C�B�B�B�����E�E�E�z");

                UpdateMainMessage("�A�C���F�i�ȁI�H�@�������v�Ȃ̂���A���̓]���I�H�j");

                UpdateMainMessage("�@�@�@�y�A�C���͓˔@�A�]���Q�[�g�������o���ꂽ�I�I�z");

                ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_FIELD_OF_FIRSTPLACE);
                this.BackColor = Color.WhiteSmoke;
                this.Update();

                UpdateMainMessage("�A�C���F�b�C�f�I�I�I");

                UpdateMainMessage("�A�C���F�b�c�c�c�E�E�E�������`���N�`���ȓ]���������ȁE�E�E");

                UpdateMainMessage("�A�C���F���āA�ǂ��Ȃ񂾂�A�R�R�́E�E�E");

                UpdateMainMessage("�@�@�@�������@���̎��A��̕����A�C���̑S�̂֐G�ꂽ�B����ȋC�������B�@�������@�@");

                UpdateMainMessage("�A�C���F�E�E�E�I�H");

                UpdateMainMessage("�A�C���F�E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E");

                UpdateMainMessage("�A�C���F�C�̂�����");

                UpdateMainMessage("�@�@�@�w�A�C���N�B�@�@�n�߂܂��āB�@�@���ˁB�x�@�@");

                UpdateMainMessage("�@�@�y�y�y�@���̎��A�A�C���͎��������̂ƂȂ�̂𒼊��Ŋ������B�@�z�z�z");

                UpdateMainMessage("�A�C���F�b�ȁI�I");

                UpdateMainMessage("�@�@�������@�A�C���́y�����z���A�����Ɍ��֐U������A����˂����Ă��B�@�������@�@");

                UpdateMainMessage("�H�H�H�F�悤�����A�t�@�[�W���{�a���ӂ̃G�X���~�A�������ցB");

                UpdateMainMessage("�@�@�������@�������A���ɂ͊��ɐl�̋C�z�݂̂����݂��邾���������B�@�������@�@");

                UpdateMainMessage("�A�C���F�i�b�o�E�E�E�n���ȁI�I�I�j");

                UpdateMainMessage("�@�@�y�y�y�@�A�C���͗₦�؂�������@���Ȃ��܂܁A���ɑ΂��錃���������𑱂��Ă���B�@�z�z�z");

                UpdateMainMessage("�A�C���F�i�E�E�E�Ǐ]������˂��E�E�E�E�\����I�H�j");

                UpdateMainMessage("�H�H�H�F�A�C���N�A���݂܂���A����ȂɌx�����Ȃ��Ă��ǂ��ł���B");

                UpdateMainMessage("�A�C���F�I�I�I");

                UpdateMainMessage("�@�@�@�������@��̗D���������܂��A�A�C���̑S�̂֐G�ꂽ�B�@�������@�@");

                UpdateMainMessage("�H�H�H�F�{�N�̖���Verze Artie�B");

                UpdateMainMessage("�@�@�@�������@������ɓ͂� ������    ");

                UpdateMainMessage("�H�H�H�F��낵���ˁA�A�C���N�B");

                UpdateMainMessage("�@�@�@�������@�����Ă悤�₭�A�A�C���ɔނ�ڎ����錠�����^����ꂽ�B�@�������@�@");

                UpdateMainMessage("�A�C���F�ŁE�E�E�`����FiveSeeker�A���F���[�E�A�[�e�B�I�H");

                UpdateMainMessage("���F���[�F���̌Ăі��͎~�߂Ă��������B�P���Ƀ��F���[�ō\���܂����B");

                UpdateMainMessage("�A�C���F�ǁA�ǂ��Ȃ񂾃R�R�́I�H");

                UpdateMainMessage("���F���[�F��قǂ��q�ׂ܂������A�t�@�[�W���{�a���ӂ̃G�X���~�A�������ł��B");

                UpdateMainMessage("�@�@�������@�A�C���͏������A���̒����������Ȃ��Ă����̂��������B�@������");

                UpdateMainMessage("�A�C���F�G�X���~�A�������E�E�E");

                UpdateMainMessage("�A�C���F�����A�t�@�[�W����X���̏����E�ɍs�������̕ӂ肩�B");

                UpdateMainMessage("�A�C���F�E�E�E����I�H�@���i�͂ǂ��ɍs�����񂾁H");

                UpdateMainMessage("���F���[�F�ޏ��ł�����A������ŋx���̐Q���𗧂ĂĂ��܂���B");

                UpdateMainMessage("���F���[�F�]�����u����̒E�o���A�����������͂���������݂����ł��ˁB");

                UpdateMainMessage("�A�C���F���A���v�Ȃ̂���I�H");

                UpdateMainMessage("���F���[�F�����A���ɖ��͂���܂���B�y���C������������̗l�ł��B");

                UpdateMainMessage("�A�C���F�n�A�A�@�@�@�E�E�E�E�܂��������A�z���g�B�ł点��Ȃ�E�E�E");

                UpdateMainMessage("�A�C���F�܂������A���i�̓z�͂��܂ɕςȏ��Ɏ��˂��������Ƃ��邩��E�E�E");

                UpdateMainMessage("���F���[�F�Ƃ���ŁA�ǂ����ăR�R�֗��悤�ƍl�����̂ł����H");

                UpdateMainMessage("�A�C���F�����A�t�����]�����u�̑O�ŁA��Ȏ}��G���Ă�̂����i���ڌ����Ă��ȁE�E�E");

                UpdateMainMessage("�A�C���F����ŃR�R�ɗ����܂������Ă킯���B�B�B");

                UpdateMainMessage("�A�C���F���ƁA���I�I");

                UpdateMainMessage("�A�C���F�����A�X�~�}�Z���I�@�����y�������Œ����Ă��܂��āI�@�\����Ȃ��ł��I");

                UpdateMainMessage("���F���[�F����A�C�ɂ��Ȃ��ł��������B�A�C���N�̎��̓I���E�����f�B�X���畷���Ă��܂�����B");

                UpdateMainMessage("�A�C���F���A�b�n�n�n�E�E�E�����Ȃ񂾁B���₢��A�ł��{���ɂ��݂܂���B");

                UpdateMainMessage("���F���[�F�Ƃ���ŁA�A�C���N�B");

                UpdateMainMessage("�A�C���F�͂��A�Ȃ�ł��傤�H");

                UpdateMainMessage("�@�@�y�y�y�@���̎��A�A�C���́@�z�z�z");

                UpdateMainMessage("�@�@�y�y�y�@�ĂсA�������̒m��Ȃ����̒�����S�̂Ŋ����n�߂��I�@�z�z�z");

                UpdateMainMessage("�A�C���F�i�b�N�\�E�E�E�������̊��G�́I�H�j");

                UpdateMainMessage("�A�C���F�i���̐l����G�ӂ͑S���ƌ����Ă����قǊ�����鎖���ł��Ȃ��B����͊m�����B�j");

                UpdateMainMessage("�A�C���F�i�Ȃ̂ɁA���̂����̊��G�����������Ă���E�E�E�j");

                UpdateMainMessage("�A�C���F�i�����̌�������Ă��邩������˂��E�E�E����ȋ��|���B�j");

                UpdateMainMessage("���F���[�F���́A�I���E�����f�B�X����˗�����Ă��鎖������܂��B");

                UpdateMainMessage("�A�C���F�b�Q�E�E�E���̎t������ł����I�H");

                UpdateMainMessage("�A�C���F�܂����A�n���̃g���[�j���O�Ƃ��E�E�E");

                UpdateMainMessage("���F���[�F�͂��A���̒ʂ�ł��B");

                UpdateMainMessage("�A�C���F�Q�Q�F�I�@�}�W����I�H");

                UpdateMainMessage("�A�C���F���ƂƁA���t�����E�E�E�����܂���B");

                UpdateMainMessage("���F���[�F�{���ɋC�ɂ��Ȃ��ėǂ��ł���A�����ʂ�̊��o�Œ����Ă��������B");

                UpdateMainMessage("���F���[�F�����łȂ��ƁA�{���̃A�C���N���m�F�o���܂��񂩂�ˁB");

                UpdateMainMessage("�A�C���F���A�������E�E�E���𗹉��B");

                UpdateMainMessage("�A�C���F���Ⴀ�A�����t�ɊÂ��āB");

                UpdateMainMessage("�A�C���F�ŁA�g���[�j���O���Ă̂͂ǂ������̂�������Ȃ񂾁H");

                UpdateMainMessage("���F���[�F�ȒP�ł���B");

                UpdateMainMessage("���F���[�F�{�N��DUEL�����ƍs���܂��񂩁H");

                UpdateMainMessage("�A�C���F���I�����Ȃ�DUEL�ł����I�H");

                UpdateMainMessage("���F���[�F�͂��A��낵�����肢�������Ǝv���܂��B");

                UpdateMainMessage("�A�C���F�E�E�E�E���`���E�E�E");

                UpdateMainMessage("�A�C���F���A���₢��B�����ł��I��낵�����肢���܂��I");

                UpdateMainMessage("���F���[�F���肪�Ƃ��������܂��B");

                UpdateMainMessage("���F���[�F����ł͑����B");

                UpdateMainMessage("�A�C���F���A��������ƃ^���}�I�I");

                UpdateMainMessage("���F���[�F�͂��A�Ȃ�ł��傤�H");

                UpdateMainMessage("�A�C���F�i�b�z�E�E�E���F���[����͂����Ƒ҂��Ă����񂾂ȁE�E�E�j");

                UpdateMainMessage("�A�C���F���̏����A���������ɉ����ĉ�����������̂��H");

                UpdateMainMessage("���F���[�F�������A���ɉ�������܂����B");

                UpdateMainMessage("���F���[�F�����Șr�����A���ꂾ���ł��B");

                UpdateMainMessage("�A�C���F�������A�܂�DUEL�͌��X�����������񂾂��ȁB");

                UpdateMainMessage("�A�C���F������A�I�[�P�[�I�[�P�[�I�@���ł��ǂ����I");

                UpdateMainMessage("���F���[�F�ł́A�n�߂�Ƃ��܂��傤�B");

                UpdateMainMessage("���F���[�F�R");

                UpdateMainMessage("�A�C���F�Q");

                UpdateMainMessage("���F���[�F�P");

                bool result = BattleStart(Database.VERZE_ARTIE, true);
                if (result)
                {
                    UpdateMainMessage("�A�C���F������I���̃^�C�~���O���I�I");

                    UpdateMainMessage("���F���[�F���āA�ǂ��ł��傤�B");

                    UpdateMainMessage("�@�@�������@�A�C�����~�߂̈ꌂ���J��o�������̏u�ԁI�@������");

                    UpdateMainMessage("�@�@�������@�b�o�V���I�I�@������");

                    UpdateMainMessage("�A�C���F�����I�@���������ƁI�H");

                    UpdateMainMessage("�@�@�y�y�y�@�A�C���́A�ُ�Ȃ܂ł̎��̒����Ɛ������������I�I�@�z�z�z");

                    UpdateMainMessage("�A�C���F���b�A���x�F�I�I�I�I�I");

                    UpdateMainMessage("���F���[�F�S�Ă͈�ƂȂ��");

                    UpdateMainMessage("�@�@�������@�E�E�E���݂̂������n��E�E�E�@������");

                    UpdateMainMessage("���F���[�F���̈�͑S�ĂւƊg�U����B");

                    UpdateMainMessage("�@�@�������@�ނ̑��݂��A�C���̎��E�ɓ������u�ԁ@������");

                    UpdateMainMessage("���F���[�F�y�b�Z�zLadarynte�ECaotic�ESchema�I");

                    UpdateMainMessage("�@�@�������@�t�@�V���D���E�E�E�@������");

                    UpdateMainMessage("�A�C���F�E�E�E�H");

                    UpdateMainMessage("�@�@�������@���߂̐Î�@������");

                    UpdateMainMessage("�A�C���F�E�E");

                    UpdateMainMessage("�@�@�������@��u�������@������");

                    UpdateMainMessage("�A�C���F�I�I�@�b���܂��I�I�I");

                    UpdateMainMessage("�@�@�������@���𔭂���Ԃ��Ȃ��@������");

                    UpdateMainMessage("�@�@�������@�b�h�V���b�@������");

                    UpdateMainMessage("�A�C���F�b�O�I�I");

                    UpdateMainMessage("�@�@�������@�b�h�V���A�b�h�V���I�@������");

                    UpdateMainMessage("�A�C���F�b�O�n�I�@��A�����I�I");

                    UpdateMainMessage("�@�@�������@�b�h�b�h�h�h�V���I�@������");

                    UpdateMainMessage("�A�C���F�b�O�A�b�Q�z�I�I�E�O�b�I");

                    UpdateMainMessage("�@�@�������@���̍U�����A���p�A�^�C�~���O�@������");

                    UpdateMainMessage("�@�@�������@�����A���@������");

                    UpdateMainMessage("�@�@�������@�b�h�V���h�V���h�V���b�h�V���h�V���h�V���h�h�h�V���I�@������");

                    UpdateMainMessage("�A�C���F�b�O�@�I�I�E�E�E�@�E�E�E");

                    UpdateMainMessage("���F���[�F�Ƃǂ߂ł��A�n�A�@�@�@�@�I�I�I");

                    UpdateMainMessage("�@�@�������@�K�V���b�b�I�@������");

                    UpdateMainMessage("�A�C���F�b�K�n�I�I");

                    UpdateMainMessage("�@�@�y�y�y�A�C���̎������m���Ȃ��̂ƂȂ��Ă����z�z�z");

                    UpdateMainMessage("���F���[�F���̕ӂŏ\���ł��傤���B");

                    UpdateMainMessage("���F���[�F�Z���X�e�B�A���E�m���@�B");

                    UpdateMainMessage("�@�@�������A�C���̏��������Ă䂭�E�E�E������");

                    UpdateMainMessage("�A�C���F�b�n�@�E�E�b�n�@�E�E�E");

                    UpdateMainMessage("���F���[�F���݂܂���A�ǂ���珟������̂悤�ł��ˁB");

                    UpdateMainMessage("�A�C���F�����E�E�E�������Ǝv�����񂾂��ǂȁE�E�E�b�n�@�E�E�E�b�n�@�E�E�E");
                }
                else
                {
                    UpdateMainMessage("�A�C���F�b�O�n�E�E�E�b�N�E�E�E");

                    UpdateMainMessage("���F���[�F���݂܂���A�ǂ���珟������̂悤�ł��ˁB");

                    UpdateMainMessage("�A�C���F�����E�E�E�������܂������E�E�E");
                }

                UpdateMainMessage("�A�C���F�Č������A��������E�E�E�S���ǂ�����C�����˂��E�E�E");

                UpdateMainMessage("���F���[�F�����A���݂܂��񂪁A����ɂ͗��R������܂��B");

                UpdateMainMessage("���F���[�F�{�N�̃A�N�Z�T�������������܂��傤�B�R���ł��B");

                UpdateMainMessage("�A�C���F���̌�����E�E�E�Ђ���Ƃ��āI�I");

                UpdateMainMessage("���F���[�F�y�V��̗��z�@�_�X�̈�Y�̈�ł��B");

                UpdateMainMessage("�A�C���F�{�P�t���̋Ɉ��O���[�u�Ɠ����ނ̃��c����ȁI�H");

                UpdateMainMessage("���F���[�F�c�O�Ȃ��炻���������ɂȂ�܂��B");

                UpdateMainMessage("���F���[�F���̗���t���Ă���ꍇ�A�y�Z�z�̃p�����^���ُ�Ȃ܂łɑ�������܂��B");

                UpdateMainMessage("���F���[�F�����������ł�����A�������͌����Ă��鎖�ɂȂ�܂��B");

                UpdateMainMessage("�A�C���F�E�E�E����");

                UpdateMainMessage("�A�C���F���������̂͊֌W�˂��A���̕������B");

                UpdateMainMessage("���F���[�F�ǂ��S�\���ł��B");

                UpdateMainMessage("�A�C���F���₢��A���܂łɂ͐�Ώ������炢�͒ǂ�����悤�ɂȂ��Ă�邺�B");

                UpdateMainMessage("���F���[�F�n�n�n�A�A�C���N�͂����Ƌ����Ȃ�܂���B");

                UpdateMainMessage("���F���[�F���āE�E�E����͂��Ă����B");

                UpdateMainMessage("���F���[�F�����ȑO����N���Ă���̂ł͂Ȃ��ł��傤���H");

                UpdateMainMessage("�A�C���F�H�@�Ȃ�̘b���H");

                UpdateMainMessage("���F���[�F���i����́A���łɋN���Ă��܂���ˁH");

                UpdateMainMessage("���i�F�E�\�I�H�@�Ȃ�Ńo��������Ă�̂�I");

                UpdateMainMessage("�A�C���F�����A���i�I�@�����������񂾂ȁI");

                UpdateMainMessage("�A�C���F���₠�悩�����ǂ������I�@�b�n�b�n�b�n�I�I");

                UpdateMainMessage("�A�C���F��̂�������N���Ă����񂾁H");

                UpdateMainMessage("���i�F�o�J�A�C�����󒆎U�����Ă鏊������");

                UpdateMainMessage("�A�C���F�n�C�n�C�A�����|���ꂽ�u�Ԃ͌���ꂽ���Ď��ˁE�E�E");

                UpdateMainMessage("���i�F�ł��A�C�������\���Ă������E�E�E");

                UpdateMainMessage("�A�C���F��H�Ȃ񂾂�B");

                UpdateMainMessage("���i�F��`�A���ł������B�C�̂����ˁ�");

                UpdateMainMessage("�A�C���F�Ȃ񂾁A�܂��\������H�@�n�b�L�������Ă����B");

                UpdateMainMessage("���i�F�債��������Ȃ����A���ł��������");

                UpdateMainMessage("���F���[�F���i����́A�A�C���N�Ɠ����]�����u�ŗ�����ł���ˁH");

                UpdateMainMessage("���i�F���I�H���A�����E�E�E�ł��ǂ����Ăł����H");

                UpdateMainMessage("���F���[�F�A�C���N�Ƃ͏������ꂽ���œ|��Ă����̂ŁA���X�C�ɂȂ��������ł��B");

                UpdateMainMessage("���F���[�F�A�C���N�ƍs���悪�����̂��߁A�����ɓ������̂ł͂���܂��񂩁H");

                UpdateMainMessage("���i�F�����E�E�E�A�C���Ƃ͓����^�C�~���O�œ]�����u�ɓ������̂͊m���ł��B");

                UpdateMainMessage("�A�C���F�����ɓ���̂́A���܂�ǂ��Ȃ��̂��H");

                UpdateMainMessage("���F���[�F��ʓI�ɂ͗ǂ��Ƃ͂���Ă��܂���ˁB");

                UpdateMainMessage("���F���[�F�]�����u�͂P�l��p�̂��߁A�Q�l�����̏ꍇ�A���B�n�_�\���͕s�\�ł��B");

                UpdateMainMessage("�A�C���F���x�E�E�E������P�l�����邩�E�E�E");

                UpdateMainMessage("���i�F�S�����Ȃ����A����������ƕ�����Ă������m��Ȃ���B");

                UpdateMainMessage("���F���[�F�������A����̂悤�ȃP�[�X���́A���ɋH���Ǝv���܂��B");

                UpdateMainMessage("���F���[�F�N�������ȃg���b�v�ł��d���܂Ȃ�����A�ő��ȃA�N�V�f���g�͂���܂���B");

                UpdateMainMessage("���F���[�F�ق�̏����^�C�~���O���Y�������x�ő��v���Ǝv���܂���B");

                UpdateMainMessage("���i�F������܂����A�C�����܂��B");

                UpdateMainMessage("�A�C���F���ĂƁE�E�E���̑�����悩��t�@�[�W���{�a���āE�E�E");

                UpdateMainMessage("���F���[�F��������������܂��ˁB");

                UpdateMainMessage("���F���[�F�]�����u�͎������������e�i���X�����Ă����܂��̂�");

                UpdateMainMessage("���F���[�F����͈�U�߂��Ă͂������ł��傤���H");

                UpdateMainMessage("�A�C���F��E�E�E�܂��A�������B�@�ǂ�����A���i�H");

                UpdateMainMessage("���i�F����A��U�߂�܂���B");

                UpdateMainMessage("�A�C���F�������A���Ⴀ�߂�Ƃ��邩�B");

                UpdateMainMessage("�A�C���F���F���[����A���낢�날�肪�Ƃ��ȁB");

                UpdateMainMessage("���F���[�F�����A�����炱���B");

                UpdateMainMessage("���F���[�F����ƋC���˂Ȃ��b�����߂ɂ��A���F���[�ƌĂю̂Ăō\���܂����B");

                UpdateMainMessage("�A�C���F����FiveSeeker�l����ɌĂю̂Ă��C�������邪�E�E�E");

                UpdateMainMessage("���F���[�F�n�߂ɐ��������͂��ł��B�I���E�����f�B�X�ɕ񍐂��܂��傤���H");

                UpdateMainMessage("�A�C���F���₠�A����Ƃ���낵���ȁI���F���[�I");

                UpdateMainMessage("���F���[�F�n�n�n�A�A�C���N�͖{���ɖʔ����ł��ˁB");

                UpdateMainMessage("�A�C���F���Ⴀ�A�{���Ɋ�Ȃ��������肪�ƂȁA�܂��ǂ����ŉ���B");

                UpdateMainMessage("���i�F�������ɍs���Ă��ˁB");

                UpdateMainMessage("�A�C���F�����B");

                UpdateMainMessage("�@�@�������@���i�͓]�����u�Ō��̏ꏊ�ւƖ߂��Ă������@������");

                UpdateMainMessage("�A�C���F�����ꂶ��I");

                UpdateMainMessage("���F���[�F�͂��B");

                ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                ButtonVisibleControl(true);

                UpdateMainMessage("�@�@�������@�A�C���͓]�����u�Ō��̏ꏊ�ւƋA���Ă����@������");

                UpdateMainMessage("�A�C���F���ӂ��E�E�E");

                UpdateMainMessage("�@�@�@�y�E�E�E�D�D�D�u�D���E�E�D�D�D���E�E�E�z");

                UpdateMainMessage("�A�C���F��H");

                UpdateMainMessage("�@�@�������@�]�����u���Ăь���o�����@������");

                UpdateMainMessage("�A�C���F�E�E�E�}�W����I�H");

                UpdateMainMessage("���F���[�F���т��ы������Ă��݂܂���B");

                UpdateMainMessage("���F���[�F���������΁A�����Y��Ă�����������܂��B");

                UpdateMainMessage("�A�C���F���A�����B�����ƁA�Ȃ�ł��傤�H");

                UpdateMainMessage("���F���[�F����A����͂��肢�Ȃ̂ł����B");

                UpdateMainMessage("���F���[�F���̎����A�A�C���N�̃p�[�e�B�ɉ����Ă��炦�܂��񂩁H");

                UpdateMainMessage("�A�C���F�Ȃ��I�I�@�}�W�ŁI�H");

                UpdateMainMessage("���F���[�F�͂��B");

                UpdateMainMessage("�A�C���F�E�E�E�E�E�E");

                UpdateMainMessage("���i�F������ƁA�ǂ�����̂�B�A�C���H");

                UpdateMainMessage("�A�C���F�E�E�E�E�E�E");

                UpdateMainMessage("���i�F���A�{�P���Ƃ��Ă�̂�B�����Ȃ�����H");

                UpdateMainMessage("�A�C���F��E�E�E");

                UpdateMainMessage("�A�C���F������I�I�I");

                UpdateMainMessage("�A�C���F�����炱���A���肢���܂��I�I�I�I�I");

                UpdateMainMessage("���F���[�F�ǂ����OK�̂悤�ł��ˁA���肪�Ƃ��������܂��B");

                CallSomeMessageWithAnimation("���F���[�E�A�[�e�B���p�[�e�B�ɉ����܂����B");

                UpdateMainMessage("�A�C���F���₠�A�ł��{���ɗǂ���ł����H");

                UpdateMainMessage("���F���[�F�ǂ������Ӗ��ł��傤�H");

                UpdateMainMessage("�A�C���F���B�ƃ_���W�����Ɍ������Ă��Ȃ�̓��ɂ��Ȃ�Ȃ��ł���H");

                UpdateMainMessage("���F���[�F�E�E�E");

                UpdateMainMessage("���F���[�F�n�n�n�n�A����͂܂��ʔ������������܂��ˁB");

                UpdateMainMessage("�A�C���F�A�n�n�E�E�E�i�ʔ����̂��H�j");

                UpdateMainMessage("���F���[�F�����邽�߂ɁA�_���W�����֌������Ă��郏�P�ł͂���܂����B");

                UpdateMainMessage("���i�F�o�J�A�C���݂����ɊF���[�������l���Ă�Ǝv�������ԈႢ��B");

                UpdateMainMessage("�A�C���F�n�C�n�C�E�E�E�ǂ������͐������������ړI�ł���E�E�E");

                UpdateMainMessage("���F���[�F�n�n�n�A���������Ӗ��ł̓{�N�������悤�Ȃ��̂ł���B");

                UpdateMainMessage("�A�C���F�z������B���F���[�����ē����悤�Ȃ��񂾂��Č����Ă邶��˂����B");

                UpdateMainMessage("���i�F�z���b�g�؋�����o�J�ˁA�A���^�ɍ��킹�Ă邾���ł��傤���B");

                UpdateMainMessage("���F���[�F�ړI�͐l�ɂ���ĈႢ�܂��B�@�����΂��b���܂���B");

                UpdateMainMessage("���F���[�F���āA����ł̓_���W�����֌����܂��傤�B");

                UpdateMainMessage("�A�C���F��������A��낵�����ނ��I");

                we.AvailableThirdCharacter = true;
                tc = null;
                tc = new MainCharacter();
                tc.FullName = "���F���[�E�A�[�e�B";
                tc.Name = "���F���[";
                tc.Strength = Database.VERZE_ARTIE_SECOND_STRENGTH;
                tc.Agility = Database.VERZE_ARTIE_SECOND_AGILITY;
                tc.Intelligence = Database.VERZE_ARTIE_SECOND_INTELLIGENCE;
                tc.Stamina = Database.VERZE_ARTIE_SECOND_STAMINA;
                tc.Mind = Database.VERZE_ARTIE_SECOND_MIND;
                tc.Level = 0;
                tc.Exp = 0;
                for (int ii = 0; ii < 35; ii++)
                {
                    tc.BaseLife += tc.LevelUpLifeTruth;
                    tc.BaseMana += tc.LevelUpManaTruth;
                    tc.Level++;
                }
                tc.CurrentLife = tc.MaxLife;
                tc.BaseSkillPoint = 100;
                tc.CurrentSkillPoint = 100;
                tc.CurrentMana = tc.MaxMana;
                tc.MainWeapon = new ItemBackPack(Database.RARE_WHITE_SILVER_SWORD_REPLICA);
                tc.MainArmor = new ItemBackPack(Database.RARE_BLACK_AERIAL_ARMOR_REPLICA);
                tc.Accessory = new ItemBackPack(Database.RARE_HEAVENLY_SKY_WING_REPLICA);
                tc.BattleActionCommand1 = Database.NEUTRAL_SMASH;
                tc.BattleActionCommand2 = Database.INNER_INSPIRATION;
                tc.BattleActionCommand3 = Database.MIRROR_IMAGE;
                tc.BattleActionCommand4 = Database.DEFLECTION;
                tc.BattleActionCommand5 = Database.STANCE_OF_FLOW;
                tc.BattleActionCommand6 = Database.GALE_WIND;
                tc.BattleActionCommand7 = Database.STRAIGHT_SMASH;
                tc.BattleActionCommand8 = Database.SURPRISE_ATTACK;
                tc.BattleActionCommand9 = Database.NEGATE;
                tc.AvailableMana = true;
                tc.AvailableSkill = true;

                tc.FireBall = true;
                tc.StraightSmash = true;
                tc.CounterAttack = true;
                tc.FreshHeal = true;
                tc.StanceOfFlow = true;
                tc.DispelMagic = true;
                tc.WordOfPower = true;
                tc.EnigmaSence = true;
                tc.BlackContract = true;
                tc.Cleansing = true;
                tc.GaleWind = true;
                tc.Deflection = true;
                tc.Negate = true;
                tc.InnerInspiration = true;
                tc.FrozenLance = true;
                tc.Tranquility = true;
                tc.WordOfFortune = true;
                tc.SkyShield = true;
                tc.NeutralSmash = true;
                tc.Glory = true;
                tc.BlackFire = true;
                tc.SurpriseAttack = true;
                tc.MirrorImage = true;
                tc.WordOfMalice = true;
                tc.StanceOfSuddenness = true;
                tc.CrushingBlow = true;
                tc.Immolate = true;
                tc.AetherDrive = true;
                tc.TrustSilence = true;
                tc.WordOfAttitude = true;
                tc.OneImmunity = true;
                tc.AntiStun = true;
                tc.FutureVision = true;

                we.AvailableFazilCastle = true;
            }
            #endregion
            #region "�J�[���n���c�݂̌P����"
            else if (!we.alreadyCommunicateCahlhanz)
            {
                we.alreadyCommunicateCahlhanz = true;

                GoToKahlhanz();

                #region "�G�����C�W�E�u���X�g"
                if ((mc.Level >= 22) && (!mc.EnrageBlast))
                {
                    UpdateMainMessage("�A�C���F���ƂƁE�E�E�������݂�������");

                    UpdateMainMessage("�J�[���F�������A����ł͍u�`���n�߂�Ƃ��悤�B");

                    UpdateMainMessage("�A�C���F�͂��A���肢���܂��I");

                    UpdateMainMessage("�@�@�@�w�A�C���͏W�����ču�`�̓��e�𕷂����I�x");

                    UpdateMainMessage("�A�C���F�搶�A����B");

                    UpdateMainMessage("�J�[���F�����Ă݂邪�ǂ��B");

                    UpdateMainMessage("�@�@�y�y�y�@�A�C���͔w�؂Ɉُ�ȈЈ����������Ă���B�@�z�z�z");

                    UpdateMainMessage("�A�C���F�i�������ُ̈�ȈЈ����́E�E�E���ʂʁE�E�E�j");

                    UpdateMainMessage("�J�[���F�ǂ������B");

                    UpdateMainMessage("�A�C���F�������Ƃł��ˁB�΂Ɨ���Z�������鏊�͂Ȃ�ƂȂ��������ł���");

                    UpdateMainMessage("�J�[���F���ƂȂ��Ƃ����������̂��̂��낤���B");

                    UpdateMainMessage("�A�C���F�����ƁA�n�C�E�E�E");

                    UpdateMainMessage("�J�[���F���Ƃ́A���̐��́y���R�z�A�y�����z�A�y�����z���̂��̂��w���B");

                    UpdateMainMessage("�J�[���F�����ĉ΂Ƃ́A�y�򉻁z�A�y�G�l���M�[�z�A�y�i�s�z���̂��̂��w���B");

                    UpdateMainMessage("�J�[���F�����𔭓W������C���[�W�𔺂킹��ɂ́A���̕��ՓI�ȊT�O���\�z���邪�ǂ��B");

                    UpdateMainMessage("�A�C���F���A�n�C�B�����Ȃ�ł����ǁE�E�E");

                    UpdateMainMessage("�J�[���F�����Ă݂邪�ǂ��B");

                    UpdateMainMessage("�@�@�y�y�y�@�A�C���͔w�؂Ɉُ�ȈЈ����������Ă���B�@�z�z�z");

                    UpdateMainMessage("�A�C���F�i���̈Ј��������Ƃ����Ă���E�E�E�b�O�O�O�E�E�E�j");

                    UpdateMainMessage("�J�[���F��Ɠ����W�J�B�ǂ������B");

                    UpdateMainMessage("�A�C���F�w�΁x���ĉ��������E�E�E�܂Ƃ܂肪�����āA��Ȃ��������C���[�W����Ȃ��ł����B");

                    UpdateMainMessage("�A�C���F�ł��w���x���Ă͉̂�����E�E�E�S�Ă���т��ăr�V�[���Ƌ؂��ʂ��Ă�ƌ������E�E�E");

                    UpdateMainMessage("�A�C���F�����Z����������ď������ƂȂ��E�E�E");

                    UpdateMainMessage("�J�[���F�w�΁x�̓���͌����ă����_���ł͂Ȃ��B");

                    UpdateMainMessage("�J�[���F�w�΁x�̈ڂ�䂭���ہA����͗\�ߒ�߂�ꂽ�O�Ղ�H�錻�ۂł���B");

                    UpdateMainMessage("�J�[���F�w���x�Ƃē��`�B�S�Ă͌���Â���ꂽ���ۂ��w���ꍇ�����邪�A");

                    UpdateMainMessage("�J�[���F�n�܂�̏����t���Ō��ʂ͐獷���ʁB����́w�΁x�̓��삻�̂��̂ł�����̂��B");

                    UpdateMainMessage("�J�[���F���̎n�܂�ƂȂ�̂͌Ȏ��g�A�܂�M�N�̃C���[�W���n�܂肾�ƍl����Ηǂ��B");

                    UpdateMainMessage("�A�C���F�E�E�E�@���E�E�E");

                    UpdateMainMessage("�A�C���F�������I�I�I");

                    UpdateMainMessage("�A�C���F�J�[���搶�A����σA���^�V�˂ł���I�I�I");

                    UpdateMainMessage("�A�C���F���͍u�`�ł���Ȃɂ��C���[�W���s���͂��͍̂��܂Ŗ���������Ƃ���������ŁE�E�E");

                    UpdateMainMessage("�A�C���F����A���₢�₢��A�z���b�g�ǂ����ł��I");

                    UpdateMainMessage("�J�[���F�]�����u�̎��Ԃ��A���낻��A�邪�ǂ��B");

                    UpdateMainMessage("�J�[���F�m���͑S�Ă̌��A�Y���ȁB");

                    UpdateMainMessage("�A�C���F���肪�Ƃ��������܂����I");

                    UpdateMainMessage("�J�[���F�i�E�E�E�@�E�E�E�j");

                    UpdateMainMessage("�J�[���F�i��x�ł����܂ŏK�����Ă���Ƃ́B�����f�B�X�������y���������낤�ȁj");

                    mc.EnrageBlast = true;
                    ShowActiveSkillSpell(mc, Database.ENRAGE_BLAST);
                    UpdateMainMessage("", true);
                }
                #endregion
                #region "�z�[���[�E�u���C�J�["
                else if ((mc.Level >= 23) && (!mc.HolyBreaker))
                {
                    UpdateMainMessage("�A�C���F���ƂƁE�E�E�������݂�������");

                    UpdateMainMessage("�J�[���F�������A����ł͍u�`���n�߂�Ƃ��悤�B");

                    UpdateMainMessage("�A�C���F�͂��A���肢���܂��I");

                    UpdateMainMessage("�@�@�@�w�A�C���͏W�����ču�`�̓��e�𕷂����I�x");

                    UpdateMainMessage("�A�C���F�r���X�^�C���Ȃ�ł����ǁA�ǂ����������藈�Ȃ���ł���B");

                    UpdateMainMessage("�J�[���F��a����������̂́A�w���x�w���x�̑������ǂ��A���i���̃C���[�W���������邪�́B");

                    UpdateMainMessage("�J�[���F�M�N�͖{���A���̋C����L���Ă���͂��A���������i�͏o���Ă��Ȃ��B�Ⴄ���ȁH");

                    UpdateMainMessage("�A�C���F�E�E�E����");

                    UpdateMainMessage("�A�C���F����A���₢��B����Ȃ񂶂�Ȃ��ł��A���\�����ēK���h�Ȃ��");

                    UpdateMainMessage("�J�[���F�����f�B�X�̌����Ă��M�N�̕a�C�B���ӎ��ɂ܂œ��荞��ł�悤���ȁB");

                    UpdateMainMessage("�A�C���F����A���̃{�P�t���ɂ�����ꂽ���͂��邯�ǁB");

                    UpdateMainMessage("�A�C���F���₢��A������������̃z�[���[�E�u���C�J�[�͍U�����U���Ƃ��Ē��˕Ԃ����Ď��ł���ˁH");

                    UpdateMainMessage("�A�C���F���ꂾ���̎����Ǝv�����A�����g���������藈�ĂȂ������ł��B");

                    UpdateMainMessage("�@�@�@�w�[�Ⴊ�M�������Ɠ����͂��߂��I�x");

                    UpdateMainMessage("�J�[���F��̑O�ł��̂悤�ȑԓx�A���ʕ�������ƐS����B");

                    UpdateMainMessage("�@�@�y�y�y�@�A�C���͔w�؂ɍX�ɐq��ł͂Ȃ��Ј������������B�@�z�z�z");

                    UpdateMainMessage("�A�C���F�������藈�Ȃ����Ă񂶂�Ȃ��āE�E�E");

                    UpdateMainMessage("�A�C���F����͉����g�̖��B�����l���܂��B");

                    UpdateMainMessage("�J�[���F�Ȏ��g����Ԕc�����Ă���̂��낤�B�Ȏ��g�ɑ΂��Č��������Ɨǂ��B");

                    UpdateMainMessage("�J�[���F�z�[���[�E�u���C�J�[�͍U���_���[�W�̕������̂܂ܑ���ɒ��˕Ԃ��B");

                    UpdateMainMessage("�J�[���F���̕��A�������g�����C�t������鎖�ɂ͑���͂Ȃ��B");

                    UpdateMainMessage("�J�[���F�����A�M�N���^�̘A�g�����߂Ă���̂ł���΁A���̃X�^�C���͈�U�̂Ă鎖�܂ōl�����������B");

                    UpdateMainMessage("�A�C���F���E�E�E����́E�E�E");

                    UpdateMainMessage("�J�[���F�m���͑S�Ă̌��A�Y���ȁB");

                    UpdateMainMessage("�A�C���F���A�͂��B���肪�Ƃ��������܂����I");

                    mc.HolyBreaker = true;
                    ShowActiveSkillSpell(mc, Database.HOLY_BREAKER);
                    UpdateMainMessage("", true);
                }
                #endregion
                #region "�T�[�N���E�X���b�V��"
                else if ((mc.Level >= 27) && (!mc.CircleSlash))
                {
                    UpdateMainMessage("�A�C���F���ƂƁE�E�E�������݂�������");

                    UpdateMainMessage("�A�C���F���́E�E�E�ŏ������p�������Ȃ���ł����ǁE�E�E");

                    UpdateMainMessage("�@�@�y�y�y�@�A�C���͔w�؂Ɉُ�ȈЈ����������Ă���B�@�z�z�z");

                    UpdateMainMessage("�A�C���F�i�������̈Ј����́E�E�E�b�O�E�E�E�j");

                    UpdateMainMessage("�J�[���F�ǂ������B");

                    UpdateMainMessage("�A�C���F�����I�I�@���������ƁI�I");

                    UpdateMainMessage("�A�C���F�ق���Ƌ������Ȃ��ł���������E�E�E");

                    UpdateMainMessage("�J�[���F�G�̋C�z���炢�A���O�Ɏ@�m����B");

                    UpdateMainMessage("�A�C���F�ł��A���݂����ȕ��͋C���ƁA�ǂ����悤���Ȃ��ł���B");

                    UpdateMainMessage("�J�[���F�M�N�́w���x�Ɓw���x�����˔����Ă���B");

                    UpdateMainMessage("�J�[���F���͈�̂��ЂƂ܂��؂蕥���Ă݂���ǂ����H");

                    UpdateMainMessage("�A�C���F�}�ɂ���Ȏ��A�ł���킯���E�E�E");

                    UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                    UpdateMainMessage("�J�[���F���̕ӂ̑f���A�����f�B�X�����������̎��͂���悤����");

                    UpdateMainMessage("�@�@�@�w�A�C���͂����̃X�g���[�g�X�}�b�V���̍\�����n�߂��x");

                    UpdateMainMessage("�A�C���F�i��������E�E�E�̂̎����u���������A�Ӑ}�I�ɗ͂����߂�΁E�E�E");

                    UpdateMainMessage("�A�C���F�������ƁI");

                    UpdateMainMessage("�@�@�@���I�I�H�H���I�I�@�@�@");

                    UpdateMainMessage("�J�[���F�w�T�[�N���E�X���b�V���x�Ƃł����Â��Ă����������B");

                    UpdateMainMessage("�A�C���F�i��������E�E�E���X�ƁE�E�E�o�J�ȁE�E�E�j");

                    UpdateMainMessage("�A�C���F�J�[���t��");

                    UpdateMainMessage("�J�[���F��͎t���ł͂Ȃ��B�@�Ȃ񂾂ˁB");

                    UpdateMainMessage("�A�C���F���̉��̃T�[�N���E�X���b�V�����āA�܂��܂��ł���ˁH");

                    UpdateMainMessage("�J�[���F���R���B");

                    UpdateMainMessage("�A�C���F�X���}�Z���A�ǂ�����������ƍu�`�����肢���܂��B");

                    UpdateMainMessage("�J�[���F�����Ԃ�Ǝꏟ�ȐS�����A��قǋC�ɓ������悤���ȁB");

                    UpdateMainMessage("�A�C���F�m���̏W�񂾂��ł����܂ŗ���Ƃ͎v���Ă܂���ł����B");

                    UpdateMainMessage("�J�[���F�m���̖��������炷��΁A������������B���R�̔������B");

                    UpdateMainMessage("�A�C���F������Ēm��Ȃ��l�͈ꐶ�C�t���Ȃ��񂶂�Ȃ��ł����H");

                    UpdateMainMessage("�J�[���F�l�̑ԓx���悾�B");

                    UpdateMainMessage("�A�C���F�E�E�E�������E�E�E");

                    UpdateMainMessage("�J�[���F�E�E�E�]�����u�̎��Ԑ؂ꂪ�߂��A�����͖߂�Ɨǂ����낤�B");

                    UpdateMainMessage("�A�C���F���I�ǂ������肪�Ƃ��������܂����I");

                    mc.CircleSlash = true;
                    ShowActiveSkillSpell(mc, Database.CIRCLE_SLASH);
                    UpdateMainMessage("", true);
                }
                #endregion
                #region "�o�C�I�����g�E�X���b�V��"
                else if ((mc.Level >= 28) && (!mc.ViolentSlash))
                {
                    UpdateMainMessage("�A�C���F���ƂƁE�E�E�������݂�������");

                    UpdateMainMessage("�J�[���F�������A����ł͍u�`���n�߂�Ƃ��悤�B");

                    UpdateMainMessage("�A�C���F�͂��A���肢���܂��I");

                    UpdateMainMessage("�@�@�@�w�A�C���͏W�����ču�`�̓��e�𕷂����I�x");

                    UpdateMainMessage("�J�[���F�����������͂��邩�B");

                    UpdateMainMessage("�A�C���F���A���Ⴀ�P�����B�����Ƃł��ˁE�E�E");

                    UpdateMainMessage("�A�C���F���������ǂ��l����Ηǂ���ł����ˁH");

                    UpdateMainMessage("�J�[���F���̌��������炵�āA�J�E���^�[�̊T�O�𕷂������̂��낤�B");

                    UpdateMainMessage("�A�C���F�����Ƃ����ł��A�X�C�}�Z���E�E�E���������A�J�E���^�[�̊T�O�ł��B");

                    UpdateMainMessage("�J�[���F���̃o�C�I�����g�E�X���b�V���̓J�E���^�[����Ȃ��X�L���ł���B");

                    UpdateMainMessage("�J�[���F�����M�N�͊��Ɏ@�����悤�����A����͑���Ƀq���g���^����B");

                    UpdateMainMessage("�A�C���F�����Ȃ�ł���B�J�E���^�[����Ȃ����炻������ꍇ�̓P�[�X�����肳���B");

                    UpdateMainMessage("�A�C���F�v���_���[�W�Ɏ��邩�A�܂��͂ǂ����Ă������_���[�W�ɕt�����鉽����ʂ��������B�ł��E�E�E");

                    UpdateMainMessage("�J�[���F���̒ʂ�A���ꂱ���J�E���^�[�̊i�D�̓I�B");

                    UpdateMainMessage("�J�[���F�J�E���^�[�͂���Ȃ����A�q�[�����@�̃X�^�b�N��ςގ����炢�e�Ղł��낤�B");

                    UpdateMainMessage("�J�[���F�܂��v���_���[�W�Ɏ���Ȃ��̂ł���΁A����͕ʂ̑厖�Ȏ�������Ŕ������邾�낤�B");

                    UpdateMainMessage("�J�[���F�ŁA����Ƃ���΃J�E���^�[����Ȃ������̂ɑ債�����ʂ͖]�߂Ȃ��B");

                    UpdateMainMessage("�J�[���F�J�E���^�[�Ƃ͂ǂ��l����Ηǂ����A�ƌ������ƂɂȂ�B����ŗǂ����ˁB");

                    UpdateMainMessage("�A�C���F���A�������������I�I�I");

                    UpdateMainMessage("�A�C���F�}�W�����Ȃ�ł���I�@�\�R�������Ă��������I�I");

                    UpdateMainMessage("�J�[���F�M�N�Ȃ�̉��߂͎����Ă��邩�ˁA����Ό����Ă݂邪�ǂ��B");

                    UpdateMainMessage("�A�C���F�����E�E�E�ł��˂��E�E�E");

                    UpdateMainMessage("�A�C���F�ǂ��������������ꍇ�A������C���X�^���g�l��~���Ă���B���Ƃ����");

                    UpdateMainMessage("�A�C���F�q���g�炵���q���g��^���Ȃ��s���B���̃^�C�~���O�ŕ��Ă΂����E�E�E���ȁH");

                    UpdateMainMessage("�J�[���F�؂͗ǂ��B");

                    UpdateMainMessage("�A�C���F������I�@�b�n�b�n�b�n�I");

                    UpdateMainMessage("�J�[���F�����A�y��_�ł͂Ȃ��B");

                    UpdateMainMessage("�A�C���F�i�b�K�N�E�E�E�j");

                    UpdateMainMessage("�J�[���F���͂�������s�ׁA���ꂪ���̃o�C�I�����g�E�X���b�V���̈�Ԃ̎g�����B");

                    UpdateMainMessage("�A�C���F���́H");

                    UpdateMainMessage("�J�[���F�v���Ɏ���Ȃ���Ԃ���A���̃X�L����H�炤�����Ƃ���B���Ƃ����");

                    UpdateMainMessage("�A�C���F�܂��A�H�炤�����������Ďv�����炢���E�E�E�_���[�W�͐H��킴������Ȃ��Ƃ��āE�E�E");

                    UpdateMainMessage("�A�C���F�������������������A�������I�I�I");

                    UpdateMainMessage("�J�[���F�����Ă݂邪�ǂ��B");

                    UpdateMainMessage("�A�C���F�З͔{���I�N���e�B�J���I�Q�C���E�E�B���h�I�Ȃ�Ƃł������͂��邶��˂����I�I");

                    UpdateMainMessage("�A�C���F�܂胉�C�t���^���ł��A���ꂪ��������Ⴛ�ꎩ�̂����Ђ��̂��̂��Ď����I�I");

                    UpdateMainMessage("�A�C���F�v���Ɏ���P�[�X����Ȃ��āA�ނ���n�߂����狇�n�ɒǂ����ގ������荞�܂���B");

                    UpdateMainMessage("�A�C���F�v�́A����Ɉ��͂����O�Ɏd���߂�ő�̎�@���ă��P���I�I");

                    UpdateMainMessage("�J�[���F�y��_���B");

                    UpdateMainMessage("�J�[���F�{���A�u�`�ł������������e�܂ł͓��ݍ��܂Ȃ����A�M�N�̂ݓ��ʂł���B");

                    UpdateMainMessage("�A�C���F���A�����Ȃ񂷂ˁE�E�E�z���g���肪�Ƃ��������܂��B");

                    UpdateMainMessage("�J�[���F�܂��܂����͐[���B�����Ȃ�̃X�L���g�p�\�z�̃X�^�C����z���Ɨǂ����낤�B");

                    UpdateMainMessage("�J�[���F���낻��]�����u�̎��Ԃ��A�A�邪�ǂ��B");

                    UpdateMainMessage("�A�C���F���肪�Ƃ��������܂����I");

                    mc.ViolentSlash = true;
                    ShowActiveSkillSpell(mc, Database.VIOLENT_SLASH);
                    UpdateMainMessage("", true);
                }
                #endregion
                #region "�����u���E�V���E�g"
                else if ((mc.Level >= 29) && (!mc.RumbleShout))
                {
                    UpdateMainMessage("�A�C���F���ƂƁE�E�E�������݂�������");

                    UpdateMainMessage("�J�[���F�������A����ł͍u�`���n�߂�Ƃ��悤�B");

                    UpdateMainMessage("�A�C���F�͂��A���肢���܂��I");

                    UpdateMainMessage("�@�@�@�w�A�C���͏W�����ču�`�̓��e�𕷂����I�x");

                    UpdateMainMessage("�J�[���F�G�̒��ӂ������̂ɂ́A���S�̂��ӎ����ď��߂ĉ\�ł���B");

                    UpdateMainMessage("�J�[���F�{�X�L���g�p���A�K�������Ƀ_���[�W�������͕��̂a�t�e�e���ʂ������邽�߁A���s�͋�����ʁB");

                    UpdateMainMessage("�A�C���F�m���ɁA���������X�L���͎g�������ԈႦ�����͖����ȁB");

                    UpdateMainMessage("�A�C���F�E�E�E");

                    UpdateMainMessage("�J�[���F�ǂ������A�����Ă݂邪�ǂ��B");

                    UpdateMainMessage("�A�C���F�G���G�p�[�e�B�Ƀ��C�t�񕜂����悤�Ƃ��Ă��ꍇ�͂ǂ��Ȃ�H");

                    UpdateMainMessage("�J�[���F�ΏۊO���B");

                    UpdateMainMessage("�A�C���F�����Ώۂ̎��A������g���Ƃǂ��Ȃ�H");

                    UpdateMainMessage("�J�[���F�Ώۂ͕ς��ʁB");

                    UpdateMainMessage("�A�C���F�Ώۂ����Ȃ��S�̌n�͎���������ΏۂɕύX���邱�Ƃ́H");

                    UpdateMainMessage("�J�[���F�s�\���B");

                    UpdateMainMessage("�A�C���F�Ώۂ�������Ɍ���������A�G���Ώۂ�I�тȂ������́H");

                    UpdateMainMessage("�J�[���F��قǂ̓��Ⴊ��������A�s�\���B���̂��߂̃X�L���ł�����B");

                    UpdateMainMessage("�A�C���F�T���L���[�B�����������邺�B");

                    UpdateMainMessage("�J�[���F���̔����ڂ̖����B�Ⴂ���̉�Ɨގ�����_������B");

                    UpdateMainMessage("�A�C���F�}�A�}�W�������I�H");

                    UpdateMainMessage("�J�[���F�������A���z�̌��_���܂��܂��t�فB");

                    UpdateMainMessage("�A�C���F�ƂقفE�E�E");

                    UpdateMainMessage("�J�[���F�m���͑S�Ă̌��A�Y���ȁB");

                    UpdateMainMessage("�A�C���F���肪�Ƃ��������܂����I");

                    mc.RumbleShout = true;
                    ShowActiveSkillSpell(mc, Database.RUMBLE_SHOUT);
                    UpdateMainMessage("", true);
                }
                #endregion
                #region "���[�h�E�I�u�E�A�e�B�`���[�h"
                else if ((mc.Level >= 30) && (!mc.WordOfAttitude))
                {
                    UpdateMainMessage("�A�C���F���ƂƁE�E�E�������݂�������");

                    UpdateMainMessage("�J�[���F�������A����ł͍u�`���n�߂�Ƃ��悤�B");

                    UpdateMainMessage("�A�C���F�͂��A���肢���܂��I");

                    UpdateMainMessage("�J�[���F�������A����̍u�`�͏��X����Ȃ��̂ƂȂ�B");

                    UpdateMainMessage("�A�C���F�ǂ������Ӗ��ł����H");

                    UpdateMainMessage("�J�[���F�M�N�ɋt�����Ɋւ��錴�_��������O��I�ɒ@�����ށB");

                    UpdateMainMessage("�A�C���F�t�����H");

                    UpdateMainMessage("�J�[���F�ł́A���̑S�Ă������狳����B�S���ċL������B");

                    UpdateMainMessage("�@�@�@�w�A�C���͏W�����ču�`�̓��e�𕷂����I�x");

                    UpdateMainMessage("�J�[���F�{���A�C���X�^���g�l�̉񕜂͐����̎��R�񕜂��x�[�X�Ƃ��Ă���B");

                    UpdateMainMessage("�J�[���F�w���[�h�E�I�u�E�A�e�B�`���[�h�x���́w���x�Ɓw���x�ɂ�镡�����@�͂�����\�Ƃ�����́B");

                    UpdateMainMessage("�J�[���F�M�N�̏ꍇ�A�w�΁x�x�[�X�ł��邽�߁A�w���x�w���x�͘_���������������B");

                    UpdateMainMessage("�J�[���F�������A�w�΁x�̋t�ƂȂ�w���x�����C���[�W�̌���Ƃ���Ή\�ƂȂ�B��Ɏ��s���Ă݂邪�ǂ��낤�B");

                    UpdateMainMessage("�A�C���F�_�������E�E�E���E�E�E");

                    UpdateMainMessage("�J�[���F�ǂ������B");

                    UpdateMainMessage("�A�C���F�_�����Ă̂̓C�}�C�`�͂߂Ȃ��A����Ȋ��������Ă��B");

                    UpdateMainMessage("�A�C���F�ǂ��܂ł��_���ŁA�ǂ����炪�_������Ȃ��̂��E�E�E");

                    UpdateMainMessage("�A�C���F�J�[���搶�̌����Ă鎖�͎󂯓�����Ȃ����e����Ȃ��B");

                    UpdateMainMessage("�A�C���F�ނ���A�b���̂͋؂���������ʂ��Ă��āA�����ĂăX���Ɠ����Ă��邵�A�X�Q�F������B");

                    UpdateMainMessage("�A�C���F�����炱���A�_���������Č�����ƁA���������Ȃ��ł���B�ǂ��ł����H");

                    UpdateMainMessage("�J�[���F�����Ă݂邪�ǂ��B");

                    UpdateMainMessage("�A�C���F����������\�R�ŏI��肶��Ȃ��̂��H");

                    UpdateMainMessage("�J�[���F�ԈႢ�Ȃ��I��肾�B");

                    UpdateMainMessage("�A�C���F��������t�������̂��̂ɖ���������񂶂�H");

                    UpdateMainMessage("�J�[���F������������A���ɓ��R�B");

                    UpdateMainMessage("�A�C���F�b�Q�A�}�W����E�E�E���Ⴀ�����ł��傤�H");

                    UpdateMainMessage("�J�[���F�l�Ԃ͘_���I�����Ɋׂ����Ǝ����������A�S�I�_���[�W�͔��ɑ傫���B");

                    UpdateMainMessage("�J�[���F�N�ɂł��o���郂�m�ł͂Ȃ��B�������������B");

                    UpdateMainMessage("�J�[���F�M�N�͉�̍��܂ł̍u�`�𕷂��A�����č��������ɂ���B");

                    UpdateMainMessage("�A�C���F���A�܂��E�E�E");

                    UpdateMainMessage("�J�[���F�ł���΁A�M�N�Ɏ����͂���B����𓥂܂��邪�悢�B");

                    UpdateMainMessage("�@�@�@�w�A�C���͂����̕\�������A���܂łɂȂ��^���ȕ\��ł����������x");

                    UpdateMainMessage("�A�C���F�����A�o���Ȃ����c��������Ď��ł����H");

                    UpdateMainMessage("�J�[���F�������������B�s�����H");

                    UpdateMainMessage("�A�C���F�s���Ƃ�����Ȃ��āA�o���Ȃ����c�͂ǂ�����΂�����ł����H");

                    UpdateMainMessage("�J�[���F�o���Ȃ��҂́A����������̑O�Ɍ���鎖�Ȃ��A���R�Ɨ���s���B");

                    UpdateMainMessage("�J�[���F�E�E�E�ӂށA�Ȃ�قǁB���̓��h�A�����ȊO�̒N�����@���Ă̎��ƌ�����B");

                    UpdateMainMessage("�A�C���F�b�O�E�E�E");

                    UpdateMainMessage("�J�[���F�M�N���R�R�ɏ��߂ė������������ł������ȁB");

                    UpdateMainMessage("�J�[���F���傤�Ǘǂ��A��̑O�ɂ��̎҂��R�R�ɘA��ė���Ɨǂ��B");

                    UpdateMainMessage("�A�C���F�����A�ǂ���ł����H");

                    UpdateMainMessage("�J�[���F�M�N�̂��ُ̈�Ȃ܂ł̐S�̋C�z��A����𒼂��˂΂Ȃ�܂��B");

                    UpdateMainMessage("�A�C���F����A�C�z��Ȃ�Ă��ĂȂ��ł���B");

                    UpdateMainMessage("�J�[���F�悢�A�����͂����܂ł��B�@�m���͑S�Ă̌��A�Y���ȁB");

                    UpdateMainMessage("�A�C���F���A�͂��B���낢��˂����񂾏��܂ŃX���}�Z���A���肪�Ƃ��������܂����I");

                    mc.WordOfAttitude = true;
                    ShowActiveSkillSpell(mc, Database.WORD_OF_ATTITUDE);
                    UpdateMainMessage("", true);
                }
                #endregion
                #region "�X�J�C�E�V�[���h"
                else if ((mc.Level >= 31) && (!mc.SkyShield))
                {
                    UpdateMainMessage("�A�C���F���ƂƁE�E�E�������݂�������");

                    UpdateMainMessage("�A�C���F����H�E�E�E���Ȃ��E�E�E");

                    UpdateMainMessage("�A�C���F���₢��E�E�E�܂��ςȏ�����d�|���Ă���\�����E�E�E");

                    UpdateMainMessage("�J�[���F�M�N�ɂƂ��Ă̑��t�����w���x�B�ł͑������H�ɂ���Ƃ��悤�B");

                    UpdateMainMessage("�A�C���F�����������I�I�@�r�r�т����肷�邶��Ȃ��ł����I�I");

                    UpdateMainMessage("�A�C���F���ƁE�E�E���H�H");

                    UpdateMainMessage("�J�[���F�f���͏\���Ɋ�������B");

                    UpdateMainMessage("�A�C���F���H���Ď��H�ł���ˁI�H�@������A�҂��Ă܂����I�@���Ⴀ�����I");

                    UpdateMainMessage("�J�[���F�������������Ƃ��悤�B");

                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�x�i�J�[���͈�u�Ō��������֎p���ړ��������I");

                    UpdateMainMessage("�A�C���F�i�������́E�E�E�e���|�[�g�݂����Ȍ��ۂ��������E�E�E�j");

                    UpdateMainMessage("�J�[���F������M�N�Ƀt���C���E�X�g���C�N�𗐎˂���Ƃ��悤�B");

                    UpdateMainMessage("�J�[���F���H�̒��ŁA��̍U�����󂯎~�߂閂�@�A�V�����������Ă݂���B");

                    UpdateMainMessage("�A�C���F���͂��H");

                    UpdateMainMessage("�J�[���F�s�����B");

                    UpdateMainMessage("�@�@�@�w�b�{�V���I�b�{�{�{�V���I�I�x");

                    UpdateMainMessage("�A�C���F�b�Q�I�H�@����������傿��b�^���}�I�I�I");

                    UpdateMainMessage("�@�@�@�w�b�h�V���I�x�i�A�C���Ɉꌂ���������I�j");

                    UpdateMainMessage("�A�C���F�b�O�n�@�I�I�@�b�O�A�����E�E�E�V�����ɂȂ��Ă˂��_���[�W���B");

                    UpdateMainMessage("�J�[���F�ǂ������B���H�ł͒N���M�N�̃y�[�X�z���ȂǑ҂��Ă���͂��񂼁B");

                    UpdateMainMessage("�@�@�@�w�b�{�V���I�b�{�V���I�b�{�{�{�V���I�I�x");

                    UpdateMainMessage("�A�C���F�b�N�\�E�E�E���������˂��A��������I�I");

                    UpdateMainMessage("�@�@�@�w�b�h�V���I�x�i�A�C���ɂ����ꌂ���������I�j");

                    UpdateMainMessage("�A�C���F�b�O�I�I�@�b�O�E�E�E");

                    UpdateMainMessage("�J�[���F�z���z���z���I�I�I");

                    UpdateMainMessage("�@�@�@�w�b�h�V���I�x�i�A�C���ɂ����ꌂ���������I�j");

                    UpdateMainMessage("�A�C���F�b�O�A�Q�z�E�E�E�{�P�t���Ɠ����m������˂����A�N�\�E�E�E");

                    UpdateMainMessage("�A�C���F����Ȓ��ŁE�E�E�C���[�W�Ȃ񂩏o���邩�����́B");

                    UpdateMainMessage("�J�[���F�����Ă�����");

                    UpdateMainMessage("�@�@�@�w�b�{�V���I�x");

                    UpdateMainMessage("�J�[���F�M�N�����ʂ܂ł���͑����B");

                    UpdateMainMessage("�@�@�@�w�b�{�{�{�V���I�x");

                    UpdateMainMessage("�@�@�@�w�b�h�h�h�V���I�x�i�A�C���ɒǉ��łR�����������I�j");

                    UpdateMainMessage("�A�C���F�b�O�I�I�b�O�A�A�@�@�@�I�I�I");

                    UpdateMainMessage("�A�C���F�i�ʖڂ��E�E�E�����悤�Ȃ�Ă͖̂���������E�E�E�j");

                    UpdateMainMessage("�A�C���F�z�[���[�E�u���C�J�[�I");

                    UpdateMainMessage("�@�@�@�w�b�h�V���I�x�i�A�C���ɂ����ꌂ���������I�j");

                    UpdateMainMessage("�J�[���F�c�O�����A����ł͖��@�_���[�W�͖h���ʁB");

                    UpdateMainMessage("�@�@�@�w�b�h�V���I�x�i�A�C���ɂ����ꌂ���������I�j");

                    UpdateMainMessage("�J�[���F�C���[�W���􂳂���B�M�N�Ȃ�o����͂��B");

                    UpdateMainMessage("�@�@�@�w�b�h�V���I�x�i�A�C���ɂ����ꌂ���������I�j");

                    UpdateMainMessage("�A�C���F�b�O�I�I�E�E�E�����A�{�P�t�������������A�ǂ����Ă��������ꒃ�ȁE�E�E");

                    UpdateMainMessage("�A�C���F�i�E�E�E�C���[�W�̔����Ăǂ�����������A�t�����̐��ŁE�E�E�H");

                    UpdateMainMessage("�@�@�@�w�b�h�h�h�h�V���I�x�i�A�C���ɂ����S������A�v���I�ȃ_���[�W�ƂȂ����I�j");

                    UpdateMainMessage("�A�C���F�b�O�A�A�@�@�@�I�I�I");

                    UpdateMainMessage("�@�@�@�w�A�C���͈ӎ����������O�ŁA����C���[�W�������΂����I�x");

                    UpdateMainMessage("�J�[���F�b���I");

                    UpdateMainMessage("�J�[���F�ł́A�g�h�����B�@�����@�E�A�j�q���[�V�����I");

                    UpdateMainMessage("�@�@�@�w�b�Y�S�S�I�H�H�H���E�E�E�x");

                    UpdateMainMessage("�@�@�@�w�E�E�E�@�E�E�E�@�E�E�E�x");

                    UpdateMainMessage("�A�C���F�b�n�@�E�E�E�E�b�n�@�E�E�E");

                    UpdateMainMessage("�J�[���F����ł��������f�B�X�̒�q�ƌ����悤�B");

                    UpdateMainMessage("�A�C���F���@�_���[�W���O�ɂ���E�E�E�X�J�C�E�V�[���h�E�E�E");

                    UpdateMainMessage("�A�C���F���Ă��E�E�E�����_���E�E�E");

                    UpdateMainMessage("�J�[���F�M�N�͂���ŁA�΂̋t�����ƂȂ�w���x�Ƃ̕������܂���K���������ƂȂ�B");

                    UpdateMainMessage("�J�[���F�܂��A�{���@�͂R��܂Œ~�ω\�Ȗ��@�ł���B��Œm���K���̎��Ԃ�^���悤�B");

                    UpdateMainMessage("�A�C���F������E�E�E�|�ꂳ���Ă��������E�E�E");

                    UpdateMainMessage("�J�[���F�m���͑S�Ă̌��A�Y���ȁB");

                    UpdateMainMessage("�A�C���F�i�E�E�E�b�o�^�j");

                    mc.SkyShield = true;
                    ShowActiveSkillSpell(mc, Database.SKY_SHIELD);
                    UpdateMainMessage("", true);
                }
                #endregion
                #region "�t���[�Y���E�I�[��"
                else if ((mc.Level >= 32) && (!mc.FrozenAura))
                {
                    UpdateMainMessage("�A�C���F���ƂƁE�E�E�������݂�������");

                    UpdateMainMessage("�J�[���F�������B");

                    UpdateMainMessage("�A�C���F���́A������ƃ^���}�I�I");

                    UpdateMainMessage("�J�[���F�ǂ������B�����Ă݂邪�ǂ��B");

                    UpdateMainMessage("�A�C���F�����͂�����Ǝ��H�͗ǂ���ōu�`�ł��肢���܂��I");

                    UpdateMainMessage("�J�[���F��قǑO��̂��������ƌ�����B���Ȃ炢�ł����H����ɂȂ낤�B");

                    UpdateMainMessage("�A�C���F���₢�₢��A������z���g���فE�E�E�I");

                    UpdateMainMessage("�J�[���F�b�t�n�n�n�A�y���݂ɂ��Ă��邼�B");

                    UpdateMainMessage("�A�C���F�n�n�n�E�E�E�i����ς��̐l�G�Ȃ񂶂�E�E�E�j");

                    UpdateMainMessage("�@�@�@�w�A�C���͏W�����ču�`�̓��e�𕷂����I�x");

                    UpdateMainMessage("�A�C���F�E�E�E�܂肱����āA�w�΁x�Ɓw���x���Ď��ł���ˁH");

                    UpdateMainMessage("�J�[���F���̒ʂ肾�B");

                    UpdateMainMessage("�J�[���F���S�Ȃ�t�������m�̕������@�ƂȂ邽�߁A�r���`�Ԃ͋ɂ߂ē���B");

                    UpdateMainMessage("�J�[���F�����āA�C���[�W�̌�����n�߂��瑊�����郂�m���C���[�W����K�v������B");

                    UpdateMainMessage("�A�C���F�{���ɂ���Ȃ̂��\�Ȃ̂���E�E�E");

                    UpdateMainMessage("�J�[���F���@���ʎ��̂́A���ɕX��t�^����̂݁B");

                    UpdateMainMessage("�J�[���F�t�����̊�b���K�������M�N�Ȃ瑢����������ƁB");

                    UpdateMainMessage("�A�C���F�����A���������m��܂��񂯂ǁE�E�E");

                    UpdateMainMessage("�A�C���F�t���C���E�I�[���Ō��ɉΑ�����t�^���Ă�������Ȃ��ł����B");

                    UpdateMainMessage("�A�C���F�ŁA��t���ł��̃t���[�Y���E�I�[�����t�^�\�����Č����Ă��ł���ˁH");

                    UpdateMainMessage("�J�[���F�\���ǂ����͋M�N����B");

                    UpdateMainMessage("�A�C���F�ƂقفE�E�E�{�P�t���ƃm�����ꏏ����Ȃ��������g�R�E�E�E");

                    UpdateMainMessage("�A�C���F����A�܂Ă�I�H");

                    UpdateMainMessage("�J�[���F�ǂ������B");

                    UpdateMainMessage("�A�C���F�{�P�t�������������̏o������Ď��ł���ˁH�H");

                    UpdateMainMessage("�J�[���F���R�B");

                    UpdateMainMessage("�A�C���F�E�E�E�҂Ă�҂Ă�E�E�E");

                    UpdateMainMessage("�J�[���F�b�t�n�n�n�n�A�����f�B�X�ɑ΂����p�\�z���B");

                    UpdateMainMessage("�A�C���F�����A�����ł���I�@���̃{�P�t���͉����������ۂ������Ă�C�����Ă��񂾂�B");

                    UpdateMainMessage("�A�C���F������A�t���[�Y���E�I�[����΂Ɏg�����Ȃ��Ă�邺�B");

                    UpdateMainMessage("�A�C���F�ŁA�t���C���E�I�[�����t���āA���x�����{�R�{�R�ɂ��Ă��B");

                    UpdateMainMessage("�J�[���F��A�������Ă������B");

                    UpdateMainMessage("�J�[���F���S�Ȃ�t�����̗Z���̂��߁A���̕����Ɣ�ׂāA�r���R�X�g�͋ɂ߂č����B");

                    UpdateMainMessage("�J�[���F�}�i�̌͊��ɂ͋C�����鎖���B");

                    UpdateMainMessage("�A�C���F�Ȃ�قǁE�E�E�����I�I");

                    UpdateMainMessage("�J�[���F�m���͑S�Ă̌��A�Y���ȁB");

                    UpdateMainMessage("�A�C���F���肪�Ƃ��������܂����I");

                    mc.FrozenAura = true;
                    ShowActiveSkillSpell(mc, Database.FROZEN_AURA);
                    UpdateMainMessage("", true);
                }
                #endregion
                #region "�V���[�v�E�O���A"
                else if ((mc.Level >= 33) && (!mc.SharpGlare))
                {
                    UpdateMainMessage("�A�C���F���ƂƁE�E�E�������݂�������");

                    UpdateMainMessage("�J�[���F�������A����ł͍u�`���n�߂�Ƃ��悤�B");

                    UpdateMainMessage("�J�[���F�{������́A�̏p�̕��ɐ�O����B�S����B");

                    UpdateMainMessage("�A�C���F�͂��A���肢���܂��I");

                    UpdateMainMessage("�@�@�@�w�A�C���͏W�����ču�`�̓��e�𕷂����I�x");

                    UpdateMainMessage("�J�[���F�̏p�Ɋւ��ẮA�����f�B�X���牽�x���P���͎󂯂Ă��邾�낤�B");

                    UpdateMainMessage("�A�C���F���Ƃ����قǁE�E�E");

                    UpdateMainMessage("�J�[���F�w�Áx�Ɓw�S��x�ɂ�镡���X�L���w�V���[�v�E�O���A�x");

                    UpdateMainMessage("�J�[���F�M�N�̏ꍇ�A�w���x����{�����ł��邽�߁A�w�Áx�͋t�̐����ƂȂ�B");

                    UpdateMainMessage("�J�[���F�������A�����f�B�X�̎��H�P����ς�ł���́A�M�N�ɂ��̌��O�͕s�v�B");

                    UpdateMainMessage("�A�C���F����Ȃ��̂Ȃ̂��E�E�E����ł����񂾂낤���E�E�E");

                    UpdateMainMessage("�J�[���F���́w�V���[�v�E�O���A�x�͐g�̂ւ̑Ō��ɍۂ��A���@�r�������s��������ʂ����B");

                    UpdateMainMessage("�A�C���F����̃C���X�^���g�s�����ɖ��@�r���������ꍇ���A����ŃJ�E���^�[�͉\�H");

                    UpdateMainMessage("�J�[���F�������������B");

                    UpdateMainMessage("�J�[���F�����āA���ٌ��ʂ������ԑ����B���@�r�����C���̎҂ɂƂ��Ă͌x�����ׂ��X�L���ƂȂ낤�B");

                    UpdateMainMessage("�A�C���F���ٌ��ʂ�������x�������Ď��́E�E�E�A���`�n�̃X�L�����Ď��ɂȂ�ȁB");

                    UpdateMainMessage("�J�[���F���̒ʂ肾�B");

                    UpdateMainMessage("�J�[���F�������A�j�Q�C�g�ɔ�׏���R�X�g�͑����B�X�L���|�C���g�̃y�[�X�z���ɂ��C��z�邪�ǂ��낤�B");

                    UpdateMainMessage("�J�[���F�m���͑S�Ă̌��A�Y���ȁB");

                    UpdateMainMessage("�A�C���F���肪�Ƃ��������܂����I");

                    mc.SharpGlare = true;
                    ShowActiveSkillSpell(mc, Database.SHARP_GLARE);
                }
                #endregion
                #region "���t���b�N�X�E�X�s���b�g"
                else if ((mc.Level >= 34) && (!mc.ReflexSpirit))
                {
                    UpdateMainMessage("�A�C���F���ƂƁE�E�E�������݂�������");

                    UpdateMainMessage("�J�[���F�������A����ł͍u�`���n�߂�Ƃ��悤�B");

                    UpdateMainMessage("�A�C���F�͂��A���肢���܂��I");

                    UpdateMainMessage("�@�@�@�w�A�C���͏W�����ču�`�̓��e�𕷂����I�x");

                    UpdateMainMessage("�A�C���F�w�Áx�̗v�f���Ăق�Ƃ��������̂������ł���ˁB");

                    UpdateMainMessage("�A�C���F���ʂ������Ȃ������Ȃ�����E�E�E");

                    UpdateMainMessage("�J�[���F�X�^���A��ჁA�����ւ̑ϐ������̂͐�p���_��ł́A�ɂ߂ďd�v�B");

                    UpdateMainMessage("�A�C���F�܂������ł���ˁB����Ȃ̂������o����Ƃ���΁E�E�E");

                    UpdateMainMessage("�A�C���F������U���ɓ]���邺�A���Č����Ă�悤�ȃ������ȁB");

                    UpdateMainMessage("�J�[���F�E�E�E�t���B");

                    UpdateMainMessage("�J�[���F�M�N�̂��̊�{�Z���X�A������V����L���Ă���悤�����B");

                    UpdateMainMessage("�J�[���F���ꂪ�����f�B�X�ɂƂ��ẮA�i�D�̓I�Ƃ�������B");

                    UpdateMainMessage("�A�C���F�b�Q�E�E�E");

                    UpdateMainMessage("�J�[���F�M�N�͍l���������Ɉ�т��Ă���A���A��������Ă����");

                    UpdateMainMessage("�J�[���F�M�N�̍s���ɂ͗����h�炬���������ɂ������߁A��ɂƂ��Ă͔��ɒ݂͂₷���B");

                    UpdateMainMessage("�A�C���F�}�W���E�E�E");

                    UpdateMainMessage("�J�[���F�{�X�L���͊��S�Ȃ�h��ɓO���邽�߂̐�p�B���������Ă��ǂ����낤�B");

                    UpdateMainMessage("�A�C���F����ĂĂ����ĂȂ��Ȃ��ł����H");

                    UpdateMainMessage("�J�[���F�K�[�h�X�L���������ʂ̎��p��₤���̂ł���Ƃ���΁B");

                    UpdateMainMessage("�J�[���F���邢�́A���i��p�̈�p����킹�邽�߂̃_�~�[�s���B");

                    UpdateMainMessage("�J�[���F�X�ɂ���Ƃ���΁A�Q���C���̐�p�����݂ɍs�����߂̕z�΂ł���Ƃ��l������B");

                    UpdateMainMessage("�A�C���F�m���Ƀ{�P�t���͂����ŏ������i�D�̃N�Z�ɁA��T�����������ꒃ����ȁE�E�E");

                    UpdateMainMessage("�A�C���F�������E�E�E��̍s���ɕt���A��葽���̑I�������l��������Ď��ł���ˁH");

                    UpdateMainMessage("�J�[���F���̒ʂ肾�B");

                    UpdateMainMessage("�A�C���F�w�Áx���E�E�E�h�q�I�퓬�X�^�C���Ƃ������肻�����ȁE�E�E�m���ɁE�E�E");

                    UpdateMainMessage("�J�[���F�m���͑S�Ă̌��A�Y���ȁB");

                    UpdateMainMessage("�A�C���F�͂��A���肪�Ƃ��������܂����I");

                    mc.ReflexSpirit = true;
                    ShowActiveSkillSpell(mc, Database.REFLEX_SPIRIT);
                    UpdateMainMessage("", true);
                }
                #endregion
                #region "�j���[�g�����E�X�}�b�V��"
                else if ((mc.Level >= 35) && (!mc.NeutralSmash))
                {
                    UpdateMainMessage("�A�C���F���ƂƁE�E�E�������݂�������");

                    UpdateMainMessage("�J�[���F�������A����ł͍u�`���n�߂�Ƃ��悤�B");

                    UpdateMainMessage("�A�C���F�͂��A���肢���܂��I");

                    UpdateMainMessage("�@�@�@�w�A�C���͏W�����ču�`�̓��e�𕷂����I�x");

                    UpdateMainMessage("�J�[���F���S�t�����ƂȂ�w���x�Ɓw�Áx�A���̕����ɂ����Ă��ɂ߂ē���B");

                    UpdateMainMessage("�J�[���F����̈�тƂ��āA���ւƓ]����̌^�ɉ����A�Â̑̌^�������Ă͂Ȃ�Ȃ��B");

                    UpdateMainMessage("�J�[���F�̌^�̃C���[�W�͑����ē��ƐÂ����E����A�ʏ�s���X�^�C���Ɖ���ς��͖����Ȃ�B");

                    UpdateMainMessage("�J�[���F�X�L����������S�ɂȂ����A���S�Ȃ�ʏ�U���B�C���X�^���g�s�����\�B");

                    UpdateMainMessage("�J�[���F�w�j���[�g�����E�X�}�b�V���x�A�g�����Ȃ��Ă݂�Ɨǂ��B");

                    UpdateMainMessage("�A�C���F���S�Ȃ�E�E�E�ʏ�U���E�E�E");

                    UpdateMainMessage("�A�C���F�E�E�E�������A����͂ǂ��Ȃ��Ă�񂾁H");

                    UpdateMainMessage("�A�C���F���E�E�E���ƁE�E�E����A�҂Ă�E�E�E");

                    UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E�@�E�E�E");

                    UpdateMainMessage("�@�@�@�w�A�C���͂����̕\�������A���܂łɂȂ��^���ȕ\��ƂȂ����B�x");

                    UpdateMainMessage("�A�C���F�E�E�E���������A������A�҂��Ă����R�����āE�E�E");

                    UpdateMainMessage("�A�C���F�X�L���|�C���g�̓y�[�X�z�����̂��B����Ȃ̂ɁA���̃X�L���ɂ͂��ꂪ�����B");

                    UpdateMainMessage("�A�C���F��������FiveSeeker�ɂ́A�Z�̒B�l�����܂�����ˁH");

                    UpdateMainMessage("�J�[���F�C�Â����ǂ��ȁB�����A�ށu���F���[�E�A�[�e�B�v�͍D��ł���𑽗p���Ă����B");

                    UpdateMainMessage("�A�C���F�Z���オ��΁A�C���X�^���g�l�̉񕜂͑������Ď��́E�E�E");

                    UpdateMainMessage("�@�@�@�w�A�C���ُ͈�Ȃ܂łɗ�⊾�������n�߂��I�x");

                    UpdateMainMessage("�A�C���F�I�C�I�C�E�E�E�������������I�@��k����˂����R���I�I");

                    UpdateMainMessage("�J�[���F�C�Â����l���ȁB���̒ʂ肾�B");

                    UpdateMainMessage("�A�C���F�P�^�[���ɂ����钼�ڍU���񐔂��c��オ�邶��˂����I�I�I");

                    UpdateMainMessage("�A�C���F���A�����m���ɃC���X�^���g�s�����ɁA�������ĉ��������ꂽ�猙�����ǂ��B");

                    UpdateMainMessage("�A�C���F���Ă��A�X�L������˂����A�قƂ�ǔC�ӂ̃^�C�~���O����˂����I�H");

                    UpdateMainMessage("�J�[���F���̎g�p���@�͂قږ����B");

                    UpdateMainMessage("�J�[���F�M�N�����A�����g�ɂ������ƂȂ�B");

                    UpdateMainMessage("�J�[���F�v�������Ɏg�p����Ɨǂ����낤�B");

                    UpdateMainMessage("�A�C���F�J�A�J�[���搶�I");

                    UpdateMainMessage("�A�C���F���̂Ȃ�Č����ėǂ����E�E�E���肪�Ƃ��������܂����I");

                    UpdateMainMessage("�J�[���F�M�N�̃|�e���V�����͔��ɍ����B");

                    UpdateMainMessage("�J�[���F��̋�����K����������悤�ɂȂ鎖�����҂���B");

                    UpdateMainMessage("�J�[���F��́A�����f�B�X�Ƃ̎��H�P���ł�����Ɨǂ����낤�B");

                    UpdateMainMessage("�A�C���F�n�A�n�C�I");

                    UpdateMainMessage("�J�[���F�m���͑S�Ă̌��A�Y���ȁB");

                    UpdateMainMessage("�A�C���F���肪�Ƃ��������܂����I");

                    mc.NeutralSmash = true;
                    ShowActiveSkillSpell(mc, Database.NEUTRAL_SMASH);
                }
                #endregion
                #region "�y���j�z�K��"
                else if ((mc.Level >= 40) && (!we.availableArchetypeCommand))
                {
                    if (we.Truth_CommunicationSinikia30DuelFail == false)
                    {
                        UpdateMainMessage("�A�C���F���ƂƁE�E�E�������݂������ȁB");

                        UpdateMainMessage("�A�C���F�J�[���搶�A���邩�H");

                        UpdateMainMessage("�A�C���F�E�E�E���˂����ȁE�E�E");

                        UpdateMainMessage("�A�C���F�i�������A���ƂȂ������E�E�E)");

                        UpdateMainMessage("�A�C���F�i�C�z�͂˂����A���ȈЈ�������C�ɕY���Ă₪��j");

                        UpdateMainMessage("�@�@�y�y�y�@�A�C���͈Ј����̌����T��n�߂��B�@�z�z�z");

                        UpdateMainMessage("�A�C���F�J�[���搶�A����񂾂�H");

                        UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                        UpdateMainMessage("�A�C���F�i��΂ɂǂ����ɂ���B���̊��o�A�ԈႢ�˂��B�j");

                        UpdateMainMessage("�@�@�@�w���̏u�ԁA�A�C���̖ڂ̑O�ɂR�{�̃c�������˔@���������I�I�x");

                        UpdateMainMessage("�A�C���F�������I�I");

                        UpdateMainMessage("�@�@�@�w�A�C���͂Ƃ����ɔ����悤�Ƃ��E�E�E�x");

                        UpdateMainMessage("�J�[���F�u���[�o���b�g�ɑ����āA���[�h�E�I�u�E�A�e�B�`���[�h�����B");

                        UpdateMainMessage("�J�[���F�����đ����ɁA�u���b�N�E�t�@�C�A���B");

                        UpdateMainMessage("�@�@�@�w�b�{�D�I�I�I�x");

                        UpdateMainMessage("�A�C���F�b�O�n�I");

                        UpdateMainMessage("�@�@�@�w�A�C���̖��@�h��͂�������ꂽ�I�x");

                        UpdateMainMessage("�J�[���F�H��������A��ϋM�N�ɂ͐\����Ȃ����A��������H�u�`���s���B");

                        UpdateMainMessage("�A�C���F���A�������I�H�@�}�W����I�H�@�����ɐ퓬���Ȃ񂶂�˂��̂���I�H");

                        UpdateMainMessage("�J�[���F�M�N��");

                        UpdateMainMessage("�A�C���F���H");

                        UpdateMainMessage("�J�[���F���`�w���j�x�̊�b�������悤�B");

                        UpdateMainMessage("�A�C���F���`�H");

                        UpdateMainMessage("�J�[���F�b�t�A�_�[�P���E�t�B�[���h�I");

                        UpdateMainMessage("�A�C���F�b�Q�I�V�}�b���I");

                        UpdateMainMessage("�J�[���F�b�t�n�n�A�W�����؂�Ă���悤���ȁB");

                        UpdateMainMessage("�A�C���F�I�C�I�C�I�C�A�ǂ����Ȃ񂾂�A�b�N�\�I");

                        UpdateMainMessage("�J�[���F���`�w���j�x�͈꒩��[�łǂ��ɂ��Ȃ���̂ł͂Ȃ��B");

                        UpdateMainMessage("�J�[���F�A�E�X�e���e�B�E�}�g���N�X�A�����B");

                        UpdateMainMessage("�A�C���F���Ƃ��A�����̓X�^���X�E�I�u�E�A�C�Y�ŃJ�E���^�[���I");

                        UpdateMainMessage("�J�[���F�C���X�^���g�ŁA���b�h�E�h���S���E�E�B���B");

                        UpdateMainMessage("�@�@�@�w�J�[���݂́y�΁z�����̖��@�U���͂��i�i�ɏ㏸�����I�x");

                        UpdateMainMessage("�A�C���F�������I�I�I");

                        UpdateMainMessage("�J�[���F�b�t�n�n�A���̂܂܏R�U�炳���Ă��炨���B");

                        UpdateMainMessage("�A�C���F�������A���`�̘b�͂ǂ��Ȃ����񂾂�E�E�E");

                        UpdateMainMessage("�J�[���F�ł́A���̂܂�DUEL������s���B");

                        UpdateMainMessage("�A�C���F�������������A����ȏ����炩��I�H");

                        UpdateMainMessage("�J�[���F�b�t�n�n�n�A��k���BBUFF���ʂ⃉�C�t�A�}�i�Ȃǂ͑S�đS������{���[���ł��邩��ȁB");

                        UpdateMainMessage("�A�C���F�b�z�E�E�E�i�ł�����ρA���̐l���`���N�`�����E�E�E�j");

                        UpdateMainMessage("�J�[���F�Ƃ���ŁA�����f�B�X�͋M�N�ɑ΂��A���Ȃ�w���I�ȍs��������Ă���悤�����A");

                        UpdateMainMessage("�A�C���F�b�Q�F�E�E�E����̂ǂ����w���I�Ȃ񂾂�E�E�E");

                        UpdateMainMessage("�J�[���F���c�͋M�N�ɑ΂��āA�Â�����B");

                        UpdateMainMessage("�J�[���F���̂悤�Ȏ��ł́A����A���`�y���j�z�͏K���ł��Ȃ����̂Ǝv���B");

                        UpdateMainMessage("�A�C���F���ہA�ǂ�����Ⴂ���񂾁H");

                        UpdateMainMessage("�J�[���F�ӂށB");

                        UpdateMainMessage("�J�[���F���`�y���j�z�Ƃ͂��̌X�̖{�����̂��̂��w���B");

                        UpdateMainMessage("�J�[���F���̌X�̖{���Ƃ́A�{�l�ɂ̂ݒm�肤����̂ł����āA���҂��M�N�ɋ�������������肷����̂ł͂Ȃ��B");

                        UpdateMainMessage("�A�C���F���Ă��Ƃ́E�E�E�J�[���搶���狳���Ă��炤���Ă킯�ɂ͍s���Ȃ��̂��H");

                        UpdateMainMessage("�J�[���F���̂Ƃ��肾�B");

                        UpdateMainMessage("�A�C���F���[��E�E�E");

                        UpdateMainMessage("�J�[���F�����A�����o�����߂̎w��A������x�ł���Ή\�ł���B");

                        UpdateMainMessage("�J�[���F����Ă݂邩�ˁA�A�C���E�E�H�[�����X�B");

                        UpdateMainMessage("�A�C���F�����I�������ł���A����I�I");

                        UpdateMainMessage("�A�C���F�ŁA�ǂ�����Ηǂ���ł����H");

                        UpdateMainMessage("�J�[���F���̉䎩��A�^��������DUEL���M�N�ɐ\�����ށB");

                        UpdateMainMessage("�A�C���F���ȁI�I�I");

                        UpdateMainMessage("�@�@�y�y�y�@�A�C���͓˔@�A�w�؂Ɉُ�ȈЈ����������n�߂��@�z�z�z");

                        UpdateMainMessage("�A�C���FDUEL�����Ă��A��������k����");

                        UpdateMainMessage("�J�[���F�M�N�ɍ���x�A�₨���B");

                        UpdateMainMessage("�J�[���F�{�C�Ŏ����퓬�p�Ƃ͉������B");

                        UpdateMainMessage("�A�C���F������Ăǂ������Ӗ����H");

                        UpdateMainMessage("�J�[���F�����f�B�X�ɕ�������A�M�N�͂�����ǖʂɂ�����");

                        UpdateMainMessage("�J�[���F������A������蔲�����s���Ă���B");

                        UpdateMainMessage("�A�C���F���₢��A���Ă˂����āBDUEL�ł͓��ɂ��̂��肾�B");

                        UpdateMainMessage("�J�[���F���̐S�\���A��ɂ͓������ł��邱�Ƃ�m��B");

                        UpdateMainMessage("�J�[���F��̖₢�̈Ӑ}�́A�����͂��Ă��邾�낤�B");

                        UpdateMainMessage("�J�[���F���`������邩�ǂ����́A�����܂ŋM�N����B");

                        UpdateMainMessage("�@�@�y�y�y�@�ُ�ȈЈ����͎E�C�ւƕς��n�߂�@�z�z�z");

                        UpdateMainMessage("�J�[���F��͋M�N���E������ōs���B");

                        UpdateMainMessage("�J�[���F�M�N������E������Œ��ނƗǂ����낤�B");

                        UpdateMainMessage("�J�[���F�����Ȃ��΁A�M�N�͂��̏�ŉʂĂ�B������݂̂��B");

                        UpdateMainMessage("�A�C���F�i����ׂ��E�E�E�}�W�ŏ��Ă����ɂ˂��E�E�E�j");

                        UpdateMainMessage("�A�C���F�i�ł��E�E�E��邵���E�E�E�˂��I�I�j");

                        UpdateMainMessage("�A�C���F�����������E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E�ӂ�������");

                        UpdateMainMessage("�A�C���FDUEL�ŁA������͂��˂��B����ɑ΂��Ď��炾����ȁB");

                        UpdateMainMessage("�@�@�w�A�C���̓J�[���n���c�ɑ΂��āA���\��̊�t���Ńb�X���ƌ����\���n�߂��x");

                        UpdateMainMessage("�A�C���F�E�E�E�s�����B");

                        UpdateMainMessage("�J�[���F���邪�����B");
                    }
                    else
                    {
                        UpdateMainMessage("�A�C���F���ƂƁE�E�E�������݂������ȁB");

                        UpdateMainMessage("�A�C���F�J�[���搶�B");

                        UpdateMainMessage("�J�[���F�ǂ������B");

                        UpdateMainMessage("�A�C���F���킳���Ă���A���`�w���j�x�̏K���B");

                        UpdateMainMessage("�J�[���F���ގp���͔F�߂悤�B");

                        UpdateMainMessage("�J�[���F�����A�M�N������E������ŗ��Ȃ���΁A���`�K���̓��͖����Ǝv���B");

                        UpdateMainMessage("�A�C���F�����A���������B");

                        UpdateMainMessage("�@�@�w�A�C���̓J�[���n���c�ɑ΂��āA���\��̊�t���Ńb�X���ƌ����\���n�߂��x");

                        UpdateMainMessage("�A�C���FDUEL�E�E�E�������B");

                        UpdateMainMessage("�J�[���F���邪����");
                    }
                    bool result = BattleStart(Database.DUEL_SINIKIA_KAHLHANZ, true);
                    if ((result) ||
                        ((result == false) && (we.Truth_CommunicationSinikia30DuelFailCount >= 3)))
                    {
                        // �������ꍇ�A���̉�b��
                        GroundOne.WE2.WinOnceSinikiaKahlHanz = true;
                        GroundOne.WE2.AvailableArcheTypeCommand = true;
                        mc.Syutyu_Danzetsu = true;
                        we.availableArchetypeCommand = true;

                        if ((result == false) && (we.Truth_CommunicationSinikia30DuelFailCount >= 3))
                        {
                            UpdateMainMessage("�A�C���F�b�O�A�@�I�I�@�E�E�E�b�E�E�E");

                            UpdateMainMessage("�@�@�y�y�y�@�A�C���͉�œI�ȃ_���[�W����炢�A��ʂ̌���f���������@�z�z�z");

                            UpdateMainMessage("�J�[���F�����܂ł̂悤���ȁB");

                            UpdateMainMessage("�A�C���F�b�D�E�E�E�b�N�I");

                            UpdateMainMessage("�A�C���F�R�E�E�E�R�R���I�I�I");

                            UpdateMainMessage("�J�[���F�b�I");

                            UpdateMainMessage("�@�@�w�@�@�@����́@�@�@�@�@�x");

                            UpdateMainMessage("�@�@�w�@�@�@��u�̏o�����@�@�@�@�@�x");

                            UpdateMainMessage("�J�[���F�b���I");

                            UpdateMainMessage("�@�@�w�@�@�@�J�[���n���c�݂̏u���̉r���J�n�^�C�~���O�@�@�x");

                            UpdateMainMessage("�J�[���F�����E�C���[�E�E�E");

                            UpdateMainMessage("�@�@�w�@�@�@�A�C���E�E�H�[�����X�́@�@�x");

                            UpdateMainMessage("�A�C���F�i�������E�E�E�r���^�C�~���O�I�I�I�j");

                            UpdateMainMessage("�@�@�w�@�@�@�Ɍ��̏󋵂̒��@�@�x");

                            UpdateMainMessage("�J�[���F�b�I�I");

                            UpdateMainMessage("�@�@�w�@�@�u�ԓI�Ȃ鎞�Ԓ�~�ɂ����������߁@�@�x�@�@");

                            UpdateMainMessage("�A�C���F�b���A�A�A�@�I");

                            UpdateMainMessage("�@�@�w�@�@�b�h�V���E�E�E�I�I�I�@�@�x");

                            UpdateMainMessage("�J�[���F�b�O�E�E�E�n�E�E�E");

                            UpdateMainMessage("�A�C���F�i���A���߂��E�E�E�ӎ����E�E�E�j");
                        }
                        else
                        {
                            UpdateMainMessage("�J�[���F�b�I");

                            UpdateMainMessage("�@�@�w�@�@�@����́@�@�@�@�@�x");

                            UpdateMainMessage("�A�C���F�������I�I�������x�F�I");

                            UpdateMainMessage("�@�@�w�@�@�@��u�̏o�����@�@�@�@�@�x");

                            UpdateMainMessage("�J�[���F�b���I");

                            UpdateMainMessage("�@�@�w�@�@�@�J�[���n���c�݂̏u���̉r���J�n�^�C�~���O�@�@�x");

                            UpdateMainMessage("�J�[���F�����E�C���[�E�E�E");

                            UpdateMainMessage("�@�@�w�@�@�@�A�C���E�E�H�[�����X�́@�@�x");

                            UpdateMainMessage("�A�C���F�i�X�^�e�B�b�N�E�o���A���烏���E�C���[�j�e�B�Ɍ��������E�E�E�R�R�I�I�I�j");

                            UpdateMainMessage("�@�@�w�@�@�@�Ɍ��̏󋵂̒��@�@�x");

                            UpdateMainMessage("�J�[���F�b�I�I");

                            UpdateMainMessage("�@�@�w�@�@�u�ԓI�Ȃ鎞�Ԓ�~�ɂ����������߁@�@�x�@�@");

                            UpdateMainMessage("�A�C���F�b���A�A�A�@�I");

                            UpdateMainMessage("�@�@�w�@�@�b�h�V���E�E�E�I�I�I�@�@�x");

                            UpdateMainMessage("�J�[���F�b�O�E�E�E�n�E�E�E");

                            UpdateMainMessage("�A�C���F�b�N�\�A�n�Y�ꂽ���I�@���܂����I�I�I");

                            UpdateMainMessage("�A�C���F���x�F�I�I�C���[�j�e�B���烔�H���J�j�b�N�E�E�F�C���A�����I�I�I");
                        }

                        UpdateMainMessage("�@�@�w�@�@�E�E�E�i�h�T�b�E�E�E�j�@�@�x");

                        UpdateMainMessage("�@�@�w�@�@�J�[���n���c�̓��̂͂킸���ȉ��Ƌ��ɁA���̏�ɕ������B�@�@�x");

                        UpdateMainMessage("�A�C���F�����I�H");

                        UpdateMainMessage("�A�C���F�J�A�J�[���搶���v�ł����I�I�I");

                        UpdateMainMessage("�J�[���F�b�t�E�E�E");

                        UpdateMainMessage("�J�[���F�b�t�n�n�A�b�t�n�n�n�n�n�n�I");

                        UpdateMainMessage("�A�C���F���A���[�ƁE�E�E");

                        UpdateMainMessage("�J�[���F��̕������B");

                        if ((result == false) && (we.Truth_CommunicationSinikia30DuelFailCount >= 3))
                        {
                            UpdateMainMessage("�J�[���FDUEL�͏I�����A�ЂƂ܂��񕜎����������Ă����Ă�낤�B");

                            UpdateMainMessage("�J�[���F�Q�C���E�B���h�A�����ăT�[�N���b�h�q�[���B");

                            UpdateMainMessage("�@�@�w�A�C���͂ق�̂�񕜂����C�������x");
                        }

                        UpdateMainMessage("�J�[���F���̈ꌂ�A�����Ȃ�B");

                        UpdateMainMessage("�A�C���F���̈ꌂ�H");

                        UpdateMainMessage("�@�@�w�@�@�J�[���n���c�̓��̂����̂܂ܕ����オ��l�ɂ��Ă��Ƃ̗����p���ɖ߂����B�@�@�x");

                        UpdateMainMessage("�J�[���F���̗l�q�ł́A�������g�Œ݂͂���Ă���ʊ������ȁB");

                        UpdateMainMessage("�A�C���F���A�J�[���搶�ɂ���Ȓv���I�Ȉꌂ��^���Ă��܂������H�H");

                        UpdateMainMessage("�J�[���F������Ȃ��B");

                        UpdateMainMessage("�A�C���F���H");

                        UpdateMainMessage("�J�[���F�䂪�����钼�O�ɁB");

                        UpdateMainMessage("�A�C���F�ǂ�ȕ��ɁH");

                        UpdateMainMessage("�J�[���F���ɂ��a�荞�݁B");

                        UpdateMainMessage("�A�C���F�E�E�E�����g���H�H");

                        UpdateMainMessage("�J�[���F���̒ʂ肾�B");

                        UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                        UpdateMainMessage("�J�[���F���傪���t����Ɨǂ��B");

                        UpdateMainMessage("�A�C���F���H");

                        UpdateMainMessage("�J�[���F���`�w���j�x�͐l�ɂ��獷���ʁB");

                        UpdateMainMessage("�J�[���F�M�N���g���[���̍s�����̂�����Ɨǂ����낤�B");

                        UpdateMainMessage("�A�C���F���[�ƁE�E�E���́E�E�E���́E�E�E");

                        UpdateMainMessage("�A�C���F�����낤�E�E�E���Ă��A�S�R�ǂ���������v���o���Ȃ��񂾂�");

                        UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                        UpdateMainMessage("�A�C���F�W���E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E�@�W���Ɓ@�E�E�E");

                        UpdateMainMessage("�A�C���F�f��");

                        UpdateMainMessage("�J�[���F�t��");

                        UpdateMainMessage("�A�C���F�w�W���ƒf��x�ŁA�ǂ����ȁH�J�[���搶�B");

                        UpdateMainMessage("�J�[���F�M�N�̋C�̂䂭�܂܂ŗǂ��낤�B");

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "�A�C���͉��`�y���j�z�w�W���ƒf��x���K�������I";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("�A�C���F�Ȃ񂩂��E�E�E");

                        UpdateMainMessage("�J�[���F�ǂ������B");

                        UpdateMainMessage("�A�C���F�����Ƃ����A�u��������I�I�v�Ƃ��u���ɂ����I�I�I�v���Ċ��G������Ǝv�����񂾂�");

                        UpdateMainMessage("�A�C���F���́A�S�R�ƌ����Ă����قǎ����������E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E����Ȏ������ȁB");

                        UpdateMainMessage("�J�[���F�{�l�ɂ̂݁A�m�o�\�ȗ̈�ł���A�{�l�ɂƂ��Ă̗B�ꖳ��B");

                        UpdateMainMessage("�J�[���F�{�l�ɂƂ��āA�܊��ł͔F�������Ȃ��̈�ł���A�{�l�̐[���ɖ���S�ɂ̂ݔF��������B");

                        UpdateMainMessage("�J�[���F���̖{�l�̐S�ɂ̂݃R���^�N�g����ꂽ�u�Ԃ��甭�����\�ƂȂ�B");

                        UpdateMainMessage("�J�[���F�T��A�����o���̂ł͂Ȃ��A�����瑶�݂��Ă���S�B������M�N���g���̌�������B");

                        UpdateMainMessage("�J�[���F���`�w���j�x�́A�����������̂ł���B");

                        UpdateMainMessage("�A�C���F�E�E�E�����E�E�E�m���ɁB");

                        UpdateMainMessage("�A�C���F�����A���������R���B���_�Ƃ��Ă��A����ʂ�������R�������B");

                        UpdateMainMessage("�A�C���F�������A�J�[���搶��|�������Ă̂����肦�Ȃ����E�E�E���������C�����Ȃ���������ȁB");

                        UpdateMainMessage("�A�C���F�J�[���搶���A���\�I�[�o�[�A�N�V�����œ|��Ă��ꂽ�񂾂�H");

                        UpdateMainMessage("�J�[���F���̒ʂ肾�B");

                        UpdateMainMessage("�A�C���F���A�}�W����I�@����ς�A�������Ă����x�������񂾂�B���������E�E�E");

                        UpdateMainMessage("�J�[���F���������鎖�͂Ȃ��B���̂܂܋A��Ɨǂ��B");

                        UpdateMainMessage("�A�C���F�������E�E�E�������c�ɂ������ǁE�E�E");

                        UpdateMainMessage("�A�C���F�����͖{���A���肪�Ƃ��������܂����I�I");

                        UpdateMainMessage("�J�[���F�����悢�A�s�����悢�B");

                        UpdateMainMessage("�A�C���F�͂��A�ǂ����ł����I�I�I");

                        UpdateMainMessage("�@�@�w�A�C���͓]�����u�ɂ�蒬�ւƖ߂��Ă������B�@�x");

                        UpdateMainMessage("�J�[���F�E�E�E");

                        UpdateMainMessage("�J�[���F�b�O�E�E�E�b�O�z�H�I�I�I");

                        UpdateMainMessage("�@�@�w�J�[���n���c�͂��̏�ő�ʂ̓f�������A���̂���Ԃ������ʂɗ����n�߂��I�I�@�x");

                        UpdateMainMessage("�J�[���F�b�O�E�E�E�b���D�E�E�E�O�A�b�O�z�I�b�S�z�I�I");

                        UpdateMainMessage("�H�H�H�F���v�ł����H�J�[���n���c�B");

                        UpdateMainMessage("�J�[���F�b�O�E�E�E�M�l");

                        UpdateMainMessage("�J�[���F�t�E�E�E�t�@�����B");

                        UpdateMainMessage("�J�[���F�b�O�E�E�E�b�Q�z�A�b�S�z�I�I�I");

                        UpdateMainMessage("�t�@���F�E�t�t�A�ǂ����A���Ȃ�H�����܂ꂽ�݂����ł��ˁi�O�O");

                        UpdateMainMessage("�t�@���F�Z���X�e�B�A���E�m���@�E�G�O�[");

                        UpdateMainMessage("�@�@�w�J�[���n���c�̒v�������݂�݂�񕜂��n�߂��x�@");

                        UpdateMainMessage("�J�[���F�b�O�E�E�E�b�t�D�E�E�E");

                        UpdateMainMessage("�t�@���F�͂��A�������v���Ǝv���܂���i�O�O");

                        UpdateMainMessage("�J�[���F���b���������B");

                        UpdateMainMessage("�t�@���F�Ђ���Ƃ��āA���̉񕜎����ȊO��������A�y���z�������̂ł͂���܂��񂩁H");

                        UpdateMainMessage("�J�[���F�Ύ~");

                        UpdateMainMessage("�t�@���F�ǂ���瓖����̂悤�ł��ˁi�O�O");

                        UpdateMainMessage("�t�@���F�������Ă��܂������ǁA�ނ̎a�荞�݁A�����Ȃ��̂ł�����B");

                        UpdateMainMessage("�J�[���F�����f�B�X���ڂ�t���闝�R�B������Ȃ����Ȃ��B");

                        UpdateMainMessage("�J�[���F���̎��_�Ły���j�z�ɂ��_���[�W�����̈З͂ƂȂ�΁A�����炭�B");

                        UpdateMainMessage("�t�@���F�����ˁA���e�͒��ڍU�����ꌂ����������B");

                        UpdateMainMessage("�t�@���F�E�t�t�A�����͂����ƌ���Ȃ������ɋ߂��_���[�W�Ȃ肻���ˁi�O�O");

                        UpdateMainMessage("�t�@���F�悩������ˁA�{���Ɏ��ȂȂ��āi�O�O");

                        UpdateMainMessage("�J�[���F�b�t�n�n�n�A��k���߂���̂ł͂Ȃ����A�t�@�����܁B");

                        UpdateMainMessage("�t�@���F�E�t�t�A�������Ȃ��ł��������ˁA��������񕜍�Ƃ���ςł�����i�O�O");

                        UpdateMainMessage("�J�[���F�t���A�̂ɖ�����B");

                        if (!we.AlreadyRest)
                        {
                            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_EVENING);
                        }
                        else
                        {
                            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                        }
                        this.buttonHanna.Visible = true;
                        this.buttonDungeon.Visible = true;
                        this.buttonRana.Visible = true;
                        this.buttonGanz.Visible = true;
                        this.buttonPotion.Visible = true;
                        this.buttonDuel.Visible = true;
                        this.buttonShinikia.Visible = true;

                        UpdateMainMessage("�@�@�w�@�A�C���͓]�����u���璬�ւƖ߂��Ă��āE�E�E�@�x");

                        UpdateMainMessage("�A�C���F���ӂ����E�E�E�߂���ƁE�E�E");

                        UpdateMainMessage("�A�C���F���ƁA������ƂƁI�I");

                        UpdateMainMessage("�@�@�w�@�A�C���͓˔@�A��������Ă��܂����B�@�x");

                        UpdateMainMessage("�A�C���F���ƁA�N�\�E�E�E�Ȃ�ł��˂����ŁA�ςɑ��ɂ����ȁB");

                        UpdateMainMessage("�A�C���F�E�E�E�S�Ȃ����E�E�E");

                        UpdateMainMessage("�A�C���F�i���ɖ��ɗ͂�����˂��B�j");

                        UpdateMainMessage("�A�C���F�i�����������������`�͎v�������g�̂ɕ��S���傫���݂������ȁE�E�E�j");

                        UpdateMainMessage("�A�C���F�i����Ⴀ�A����ɏo���ĂP�񂾂ȁB�A���͂ł��������˂��B�j");

                        UpdateMainMessage("�A�C���F�i�g���ǂ���͓�������A�C��t���Ȃ��ƂȁB�j");

                        UpdateMainMessage("�A�C���F����͂����ƁE�E�E��Ń��i�ɂ��A���`�̘b��`���Ă��Ƃ��邩�I");
                    }
                    else
                    {
                        UpdateMainMessage("�A�C���F�b�O�A�@�I�I�@�E�E�E�b�E�E�E");

                        UpdateMainMessage("�@�@�y�y�y�@�A�C���͉�œI�ȃ_���[�W����炢�A��ʂ̌���f���������@�z�z�z");

                        UpdateMainMessage("�J�[���F�����܂ł̂悤���ȁB");

                        UpdateMainMessage("�A�C���F�b�D�E�E�E");

                        UpdateMainMessage("�J�[���F���̍U���ŁA�Ȃ����������Ă���̂́A�܎^�ɒl����B");

                        UpdateMainMessage("�A�C���F�E�E�E�b�E�E�E");

                        UpdateMainMessage("�J�[���F���̂܂܎E���̂͐ɂ����A�񕜎����������Ă����Ă�낤�B");

                        UpdateMainMessage("�J�[���F�Q�C���E�B���h�A�����ăT�[�N���b�h�q�[���B");

                        UpdateMainMessage("�@�@�w�A�C���͂ق�̂�񕜂����C�������x");

                        UpdateMainMessage("�A�C���F�b�O�E�E�E�b�c�E�E�E");

                        UpdateMainMessage("�J�[���F�M�N�ɂ͑f�������������ƌ�����B���̂܂܋A�邪�悢�B");

                        we.Truth_CommunicationSinikia30DuelFail = true;
                        we.Truth_CommunicationSinikia30DuelFailCount++;

                        if (!we.AlreadyRest)
                        {
                            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_EVENING);
                        }
                        else
                        {
                            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                        }
                        this.buttonHanna.Visible = true;
                        this.buttonDungeon.Visible = true;
                        this.buttonRana.Visible = true;
                        this.buttonGanz.Visible = true;
                        this.buttonPotion.Visible = true;
                        this.buttonDuel.Visible = true;
                        this.buttonShinikia.Visible = true;

                    }
                }
                #endregion
                else
                {
                    UpdateMainMessage("�A�C���F���ƂƁE�E�E�������݂�������");

                    UpdateMainMessage("�A�C���F���̂����܂���H");

                    UpdateMainMessage("�J�[���F�ǂ������B");

                    UpdateMainMessage("�A�C���F�����܂���A������ƍu�`�ł����肢�������̂ł����A");

                    UpdateMainMessage("�J�[���F���A�����鎖�͂Ȃ��B�@�A�邪�悢�B");

                    UpdateMainMessage("�A�C���F�n�C�E�E�E");
                }

                BackToTown();
            }
            else
            {
                UpdateMainMessage("�A�C���F�J�[���n���c�݂ɂ͂܂����x�����Ă��炤�Ƃ��悤�B", true);
            }
            #endregion
        }

        private void ButtonVisibleControl(bool visible)
        {
            this.buttonHanna.Visible = visible;
            this.buttonDungeon.Visible = visible;
            this.buttonRana.Visible = visible;
            this.buttonGanz.Visible = visible;
            if (we.AvailablePotionshop)
            {
                this.buttonPotion.Visible = visible;
            }
            if (we.AvailableDuelColosseum)
            {
                this.buttonDuel.Visible = visible;
            }
            if (we.AvailableBackGate)
            {
                this.buttonShinikia.Visible = visible;
            }
        }

        private void Blackout()
        {
            this.buttonHanna.Visible = false;
            this.buttonDungeon.Visible = false;
            this.buttonRana.Visible = false;
            this.buttonGanz.Visible = false;
            this.buttonPotion.Visible = false;
            this.buttonDuel.Visible = false;
            this.buttonShinikia.Visible = false;
            this.BackColor = Color.Black;
            ChangeBackgroundData(null);
            this.Invalidate();
        }
        
        private void ShowActiveSkillSpell(MainCharacter player, string skillSpellName)
        {
            using (TruthSkillSpellDesc skillSpell = new TruthSkillSpellDesc())
            {
                skillSpell.StartPosition = FormStartPosition.CenterParent;
                skillSpell.SkillSpellName = skillSpellName;
                skillSpell.Player = player;
                skillSpell.ShowDialog();
            }
        }

        // �c������i
        private void button3_Click(object sender, EventArgs e)
        {
            #region "�������E"
            if (GroundOne.WE2.RealWorld && GroundOne.WE2.SeekerEvent601 && !GroundOne.WE2.SeekerEvent602)
            {
                UpdateMainMessage("���i�F������A�ӊO�Ƒ�������Ȃ��B");

                UpdateMainMessage("�A�C���F�����A�������Q�o�߂��ǂ��񂾁B���������q�S�������I");

                UpdateMainMessage("���i�F�o�J�Ȏ������ĂȂ��ŁA�z���z���A�����͂�ł��H�ׂ܂���B");

                UpdateMainMessage("�A�C���F�����A�������ȁI���Ⴀ�A�n���i�f�ꂳ��Ƃ��ŐH�ׂ悤���B");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�n���i�̏h���i�������j�ɂāE�E�E";
                    md.ShowDialog();
                }

                UpdateMainMessage("�A�C���F�����������A�f�ꂳ��I�����̔т��������|����ȁI");

                UpdateMainMessage("�n���i�F�A�b�n�b�n�A�悭���C�ɐH�ׂ�ˁB�܂���R���邩��ˁA�ǂ�ǂ�H�ׂȁB");

                UpdateMainMessage("���i�F�A�C���A�����͍T���Ȃ�����ˁB�p��������������B");

                UpdateMainMessage("�A�C���F�����A�T���邺�B������ȁI�b�n�b�n�b�n�I�I�I");

                UpdateMainMessage("�@�@�@�w�b�h�X�I�x�i���i�̃T�C�����g�u���[���A�C���̉������y��j�@�@");

                UpdateMainMessage("�A�C���F�����������E�E�E������H���Ă鎞�ɂ�������Ȃ��āE�E�E");

                UpdateMainMessage("�A�C���F�E�E�E�b���O�E�E�E������������I���ł��A���i�B");

                UpdateMainMessage("���i�F���H");

                UpdateMainMessage("�A�C���F�I���̓_���W�����֌��������B");

                UpdateMainMessage("�A�C���F�����āA���̍ŉ��w�փI���͒H��t���Ă݂���I");

                UpdateMainMessage("���i�F������A���悢���Ȃ蓂�˂ɁB");

                UpdateMainMessage("���i�F�S�R��������������Ȃ��B����A�{���ɂ���ȃg�R�s�������킯�H");

                UpdateMainMessage("�A�C���F�����A�{�����B");

                UpdateMainMessage("�A�C���F�����҂��Ŏ��x�𐬂藧��������Ă̂����R�Ȃ񂾂��A");

                UpdateMainMessage("�A�C���F�`����FiveSeeker�ɒǂ��������C���������邵�A����ɉ����B");

                UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E�@�E�E�E");

                UpdateMainMessage("�A�C���F�s���Ȃ�����A�Ȃ�Ȃ��񂾁B");

                UpdateMainMessage("���i�F���A�����E�E�E");

                UpdateMainMessage("�A�C���F���ƁA���������΂������B�Y��Ȃ������ɁE�E�E");

                UpdateMainMessage("���i�F���T���Ă�̂�H");

                UpdateMainMessage("�A�C���F�m���|�P�b�g�ɓ��ꂽ�͂��E�E�E");

                while(true)
                {
                    using (TruthDecision td = new TruthDecision())
                    {
                        td.MainMessage = "�@�y�@���i�ɃC�������O��n���܂����H�@�z";
                        td.FirstMessage = "���i�ɃC�������O��n���B";
                        td.SecondMessage = "���i�ɃC�������O��n�����A�|�P�b�g�ɂ��܂��Ă����B";
                        td.StartPosition = FormStartPosition.CenterParent;
                        td.ShowDialog();
                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                        {
                            UpdateMainMessage("�A�C���F�i�E�E�E����E�E�E�j");

                        }
                        else
                        {
                            UpdateMainMessage("�A�C���F�i�E�E�E���̃C�������O�E�E�E�j");

                            UpdateMainMessage("�A�C���F�i����������Ă�ƁA�����v���o���������E�E�E�j");

                            UpdateMainMessage("�A�C���F�i���i�ɂ͈������A�������������Ă������E�E�E�j");

                            UpdateMainMessage("�A�C���F����A���ł��˂��񂾁B");

                            UpdateMainMessage("���i�F���A�|�P�b�g���S�\�S�\���Ă�����Ȃ��́H");

                            UpdateMainMessage("�A�C���F���A���₢��B���ł��˂��A�b�n�b�n�b�n�I");

                            UpdateMainMessage("���i�F����A�����炳�܂ɉ������������H���̂́E�E�E");

                            UpdateMainMessage("�A�C���F�����A�_���W�����I�b�n�b�n�b�n�I");
                            break;
                        }
                    }
                    we.AlreadyCommunicate = true;
                }
                GroundOne.WE2.SeekerEvent602 = true;
                Method.AutoSaveTruthWorldEnvironment();
                Method.AutoSaveRealWorld(this.MC, this.SC, this.TC, this.WE, null, null, null, null, null, this.Truth_KnownTileInfo, this.Truth_KnownTileInfo2, this.Truth_KnownTileInfo3, this.Truth_KnownTileInfo4, this.Truth_KnownTileInfo5);
            }
            else if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd && GroundOne.WE2.SeekerEvent602)
            {
                UpdateMainMessage(MessageFormatForLana(1002), true);
            }
            #endregion
            #region "�l�K�J�n��"
            else if (we.TruthCompleteArea3 && !we.Truth_CommunicationLana41)
            {
                we.Truth_CommunicationLana41 = true;
                UpdateMainMessage("�A�C���F�您���i�A�S�K�֍s�������͏o�������H");

                UpdateMainMessage("���i�F���͓��ROK��B�A�C���̕��́H");

                UpdateMainMessage("�A�C���F���͂܂���̃I�b�P�[���ȁB");

                UpdateMainMessage("�A�C���F�����肾�A���i�����Ă���B��C�ɂȂ��Ă鎖������񂾁B");

                UpdateMainMessage("���i�F���A�Ȃɂ悢���Ȃ�H");

                UpdateMainMessage("�A�C���F�N���F���^�X���Ă��邾��H�ق�A�K���c��������̒m�荇���́E�E�E");

                UpdateMainMessage("���i�F���@�X�^�������܂̎��H");

                UpdateMainMessage("�A�C���F���������I���̃��@�X�^��������I");

                UpdateMainMessage("���i�F����������Č�������A�{������E�E�E");

                UpdateMainMessage("�A�C���F���₢��A���������B�ł��A���̃��@�X�^��������Ȃ񂾂�");

                UpdateMainMessage("�A�C���F�Ɍ��[�����M�A�X���Ċm���E�E�E");

                UpdateMainMessage("���i�F�����ˁA�m�����@�X�^�������܂��l���̑S�Ă������āA�T�O�N�z���Ŋ��������������������");

                UpdateMainMessage("�A�C���F�����A�m��������������ȁB");

                UpdateMainMessage("���i�F���ꂪ�ǂ��������́H�H");

                UpdateMainMessage("�A�C���F�E�E�E���ƁA������Ȃ񂩂�������ȁB");

                UpdateMainMessage("���i�F�o���W���m�E�Z���X�e�B�t�@�[�W���{�a�ɏ����Ă��鎊��̈��B");

                UpdateMainMessage("�A�C���F�����A���ꂾ���ꂻ��B");

                UpdateMainMessage("���i�F���ꂻ����āE�E�E�����Ŏv���o���Ȃ�����ˁB�܂������E�E�E");

                bool detectFeltus = false;
                if ((mc != null) && (mc.MainWeapon != null) && (mc.MainWeapon.Name == Database.LEGENDARY_FELTUS))
                {
                    detectFeltus = true;
                }
                if ((mc != null) && (mc.SubWeapon != null) && (mc.SubWeapon.Name == Database.LEGENDARY_FELTUS))
                {
                    detectFeltus = true;
                }
                if ((mc != null) && (mc.MainArmor != null) && (mc.MainArmor.Name == Database.LEGENDARY_FELTUS))
                {
                    detectFeltus = true;
                }
                if ((mc != null) && (mc.Accessory != null) && (mc.Accessory.Name == Database.LEGENDARY_FELTUS))
                {
                    detectFeltus = true;
                }
                if ((mc != null) && (mc.Accessory2 != null) && (mc.Accessory2.Name == Database.LEGENDARY_FELTUS))
                {
                    detectFeltus = true;
                }
                ItemBackPack[] backpack = mc.GetBackPackInfo();
                for (int ii = 0; ii < backpack.Length; ii++)
                {
                    if (backpack[ii] != null)
                    {
                        if (backpack[ii].Name == Database.LEGENDARY_FELTUS)
                        {
                            detectFeltus = true;
                            break;
                        }
                    }
                }

                UpdateMainMessage("�A�C���F�ŁA�Ō�̈���E�E�E");

                UpdateMainMessage("���i�F�_���t�F���g�D�[�V���B");

                UpdateMainMessage("���i�F���̕ꂳ�񂪌`���Ƃ��Ă��ꂽ���m�ˁB");

                if (detectFeltus)
                {
                    UpdateMainMessage("�A�C���F���͉����E�E�E�������ď������Ă���B");
                }

                UpdateMainMessage("�A�C���F���̃t�F���g�D�[�V���̓��i�̕ꂳ�񂪍�������ł������񂾂�H");

                UpdateMainMessage("���i�F�����A������B�ꂳ�񂪁A�Ⴂ���Ƀ��@�X�^�������܂��炨�a���蒸�������炵���́B");

                UpdateMainMessage("�A�C���F���Ⴀ�A���@�X�^�������񂪍�������Ă̂��H�H");

                UpdateMainMessage("���i�F����͈Ⴄ�񂶂�Ȃ�������B�����ċɌ�����葱����̂ɕK���������͂������B");

                UpdateMainMessage("�A�C���F��{�̌��ɂT�O�N��₵�Ă�ԂɁA���͂�����{����Ă܂����E�E�E");

                UpdateMainMessage("�A�C���F����Ȃ킯�˂���ȁB�������ɁB");

                UpdateMainMessage("���i�F����B");

                if (detectFeltus)
                {
                    UpdateMainMessage("�A�C���F���Ⴀ�A���̐_���Ɋւ��ẮA�N��������̂��̓n�b�L�����ĂȂ��H");
                }
                else
                {
                    UpdateMainMessage("�A�C���F���Ⴀ�A���̐_�����Ă̂͒N��������̂��̓n�b�L�����ĂȂ��H");
                }

                UpdateMainMessage("���i�F�����������ɂȂ��ˁB");

                UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                UpdateMainMessage("�A�C���F�N�Ȃ񂾁H�H");

                UpdateMainMessage("���i�F�m��Ȃ����B");

                UpdateMainMessage("�A�C���F��`�̏��݂����Ȃ͖̂����̂��H�����A�쐬�҂��ڂ��Ă銪���݂����ȁE�E�E");

                UpdateMainMessage("���i�F�������B");

                if (detectFeltus)
                {
                    UpdateMainMessage("�A�C���F���̕�����ɁE�E�E�ǂ��������Ă˂����ȁE�E�E");
                }
                else
                {
                    UpdateMainMessage("�A�C���F���̕����A�₩�����ɏ����������Ă���Ƃ��E�E�E");
                }

                UpdateMainMessage("���i�F�������Č����Ă邶��Ȃ��B�V�c�R�C��ˁB");

                UpdateMainMessage("�A�C���F�_�����Č������炢������A�_�l��������Ƃ��H");

                UpdateMainMessage("���i�F����A�_�l�����̃A���^������ŗǂ���Ȃ�A�����������ŗǂ��񂶂�Ȃ��������");

                UpdateMainMessage("�A�C���F�n�n�b�A�����Ă���邺�B�܂��A�_�l�͒u���Ƃ��Ă��ȁB");

                UpdateMainMessage("�A�C���F���ǁA�킩�炸���܂����Ď��ɂȂ�̂��B");

                UpdateMainMessage("���i�F���`��E�E�E");

                UpdateMainMessage("�A�C���F��H");

                UpdateMainMessage("���i�F�˂��A�A�C�����Č��ǉ����m�肽�������́H");

                UpdateMainMessage("�A�C���F�������Č����Ă��ȁB");
                
                UpdateMainMessage("�A�C���F�P�ɐ_���t�F���g�D�[�V���̍����m�肽���������B�����ȋ������Ă���ȁB");

                UpdateMainMessage("���i�F�E�E�E���`��E�E�E");

                UpdateMainMessage("�A�C���F�ȁA������B�Ȃ񂩃I�J�V�C���H�H");

                UpdateMainMessage("���i�F����A�ʂɃI�J�V�C�킯����Ȃ��񂾂��ǂˁB");

                UpdateMainMessage("�A�C���F���������A�n�b�L�������Ă����H");

                UpdateMainMessage("���i�F���`��E�E�E");

                UpdateMainMessage("���i�F�E�E�E�˂�");

                UpdateMainMessage("���i�F�A�C���͂��̍���̐l���������m�鎖���ł�����E�E�E");

                UpdateMainMessage("���i�F���̎��A�A�C���͂ǂ��������킯�H");

                UpdateMainMessage("�A�C���F�ǂ��E�E�E���Č����Ă��ȁB");

                UpdateMainMessage("�A�C���F���ʂȖړI�͖����ȁB");

                UpdateMainMessage("���i�F�Ӂ`��A�����Ȃ́H");

                UpdateMainMessage("�A�C���F�����B");

                UpdateMainMessage("���i�F���Ⴀ�E�E�E������Ƃ����Ȃ�B");

                UpdateMainMessage("�A�C���F������Ƃ����H�H");

                UpdateMainMessage("���i�F����B");

                UpdateMainMessage("�A�C���F�����A�}�W�Ŏ��͒m���Ă���Ęb���I�H�����Ă����I�H");

                UpdateMainMessage("���i�F�����������ƁA�����Ȃ萷��オ��Ȃ��ł�B�債����񂶂�Ȃ��񂾂���B");

                UpdateMainMessage("�A�C���F���₢�₢��A�ǂ�ȍ��ׂȎ��ł��ǂ����A�����Ă���I���ށI�I");

                UpdateMainMessage("���i�F���`��A���҂������Ⴄ�ƃA���Ȃ񂾂��ǁA�܂��ǂ���B������ˁB");

                UpdateMainMessage("�A�C���F�����A���ނ��B");

                UpdateMainMessage("���i�F���̕ꂳ��Ɏ�n�����O�̓��@�X�^�������܂��������Ă������͂����m���Ă��ˁH");

                UpdateMainMessage("�A�C���F�����A�������ȁB");

                UpdateMainMessage("���i�F���̃��@�X�^�������܂��A�N�������󂯂��̂������������蕷�����������������̂�B");

                UpdateMainMessage("�A�C���F���i�A���O�͖{���������蕷���̑�D������ȁB");

                UpdateMainMessage("���i�F�b�t�t�A�S�����ˁ�@�ŁA���@�X�^�������܂���A���Ă����������ˁB");

                UpdateMainMessage("���i�F�i���}�l�j�w���@�X�^�F�N�������󂯂��������A���i�����A������������͖ڂ̑O�ɂ�����Ă����I�x");

                UpdateMainMessage("�A�C���F�����́E�E�E�ڂ̑O�H�H");

                UpdateMainMessage("���i�F�łˁA���ꂶ�Ⴟ����ƕ�����Ȃ�����A���������ڂ��������Ă݂��̂�B");

                UpdateMainMessage("���i�F��������A���������Ă����́B");

                UpdateMainMessage("���i�F�i���}�l�j�w���@�X�^�F���`�₢�₢��A�}�C�b�^�I�@�����A���̌����炟���ڂ͌����˂��A���~�߂���Ă񂾁B�x");

                UpdateMainMessage("�A�C���F���~�߁E�E�E�ǂ����������A�S�R�킩��˂��B");

                UpdateMainMessage("�A�C���F�m��ꂿ�።����Ęb�Ȃ̂��H�H");

                UpdateMainMessage("���i�F�ǂ��������炵�����B");

                UpdateMainMessage("���i�F�ł������[���s���Ȃ��ȂƎv���āA�H���������Ă݂��́B");

                UpdateMainMessage("�A�C���F���O�悭����Ȃ��E�E�E���������閧�\���B");

                UpdateMainMessage("���i�F�܂��A�ǂ�����Ȃ��A�łˁA��������ˁB");

                UpdateMainMessage("���i�F�i���}�l�j�w���@�X�^�F���������E�E�E�E�E�E�܂������Č��₟�E�E�E�E�E�E�x");

                UpdateMainMessage("���i�F�i���}�l�j�w���@�X�^�F�E�H�[�����X�I�I�I�x");

                UpdateMainMessage("���i�F�i���}�l�j�w���@�X�^�F�b�K�[�b�n�n�n�n�n�n�I�x");

                UpdateMainMessage("�A�C���F�i�b�u�t�D�I�I�I�j");

                UpdateMainMessage("���i�F������A�����o���Ȃ��ł�A�����B");

                UpdateMainMessage("�A�C���F���₢�₢��A�����̔�������������Ă邶��˂����I�I���~�߂���Ă񂶂�˂��̂���I�I");

                UpdateMainMessage("�A�C���F���āA���̐��Ɠ�������˂����I�I�I");

                UpdateMainMessage("���i�F���ł���H�������r�b�N����������āB");

                UpdateMainMessage("�A�C���F���̕��́A�����Ă���Ȃ������̂��H");

                UpdateMainMessage("���i�F����A�������ɂ���ȏ�͉��x�H���������Ă��_����������B");

                UpdateMainMessage("�A�C���F�������E�E�E�܂��A����Ⴛ�����낤�ȁB");

                UpdateMainMessage("���i�F�b�͂���ŃI�V�}�C��B�Q�l�ɂȂ����H");

                UpdateMainMessage("�A�C���F�����A�\�������B�T���L���[�ȁB");

                UpdateMainMessage("�A�C���F�������E�E�E�E�H�[�����X���Đ��̐l�͌��\���邵�ȁB");

                UpdateMainMessage("���i�F�����ˁA�A�C���ɒ��ڊ֌W���邩�ǂ����܂ł͕�����Ȃ���ˁB");

                UpdateMainMessage("�A�C���F�܂��@�����΁A���@�X�^��������ɔS�荘�ŕ����Ă݂�Ƃ��邩�B");

                UpdateMainMessage("���i�F�b�t�t�A�����ˁB�܂����x�ꏏ�ɃN���F���^�X�ɍs���Ă݂܂����");

                UpdateMainMessage("�A�C���F�����A�܂��s�������B");
            }
            #endregion
            #region "�O�K�J�n��"
            else if (we.TruthCompleteArea2 && !we.Truth_CommunicationLana31)
            {
                we.Truth_CommunicationLana31 = true;

                UpdateMainMessage("�A�C���F�您�A���C�ł���Ă邩���i�B");

                UpdateMainMessage("���i�F����͂������̃Z���t��B�R�K�����������͖��S�Ȃ́H");

                UpdateMainMessage("�A�C���F�����A�o�b�N�p�b�N�͐����ς݂��B�C���Ă������āI");

                UpdateMainMessage("���i�F���������Ӗ�����Ȃ����B�S�\���̘b��B");

                UpdateMainMessage("�A�C���F�S�\�����H�������Ȃ��E�E�E");

                UpdateMainMessage("�A�C���F�r���Ŏ~�߂���͂��˂����B");
                
                UpdateMainMessage("�A�C���F�L�b�`�������Ă݂���A�C���Ă����B");

                UpdateMainMessage("���i�F�t�t�t�A�����Ă���邶��Ȃ��B���S�������");

                UpdateMainMessage("���i�F�����A���������΁A�A�C���B");

                UpdateMainMessage("�A�C���F��H������B");

                UpdateMainMessage("���i�F���`��A�ǂ���������ȁB����ς�A�����Ȃ���");

                UpdateMainMessage("�A�C���F�b�Q�A�����Ȃ�܂����̃p�^�[������H�@�����Ă����B");

                UpdateMainMessage("���i�F���`��A�܂��ǂ���B���񂾂����ʁ�");

                UpdateMainMessage("���i�F�J�[���݂̏��֍s���]�����u�Ȃ񂾂��ǁB");

                UpdateMainMessage("�A�C���F�����A���ꂪ�ǂ����������H");

                UpdateMainMessage("���i�F���͂���A�t�@�[�W���{�a�ɂ��q�����Ă���B");

                UpdateMainMessage("�A�C���F�b�}�W����I�H");

                UpdateMainMessage("�A�C���F�Ă������A�Ȃ�ł���Ȏ���������񂾂�I�H");

                UpdateMainMessage("���i�F�����ȃg�R�A���������͋��R��������B");

                UpdateMainMessage("���i�F�����f�B�X�̂��t�����񂪂��̓]�����u�̏����E���̎��؂̎}���������Ă��̂�ˁB");

                UpdateMainMessage("���i�F����łȂ񂩂���񂶂�Ȃ����Ȃ��Ďv���đ��������猩�Ă���E�E�E");

                UpdateMainMessage("���i�F�i���}�l�j�w�����f�B�X�F�����A�����̖��B�����p���B�x");

                UpdateMainMessage("�A�C���F�b�Q�A�o�����̂���E�E�E�I�̑��ɂ���Ȃ��������I�H");

                UpdateMainMessage("���i�F�{�R�{�R�ɂ����̂��ăo�J�A�C�����Ώۂ̎��������");

                UpdateMainMessage("�A�C���F����ω������q�f�F�����Ȃ̂��E�E�E�܂���������Ɏ��������t������˂����ǂ��B");
                
                UpdateMainMessage("�A�C���F�ŁA�ǂ������b�������񂾁H");

                UpdateMainMessage("���i�F�y�����Ă��ł����H�z���ĕ��ʂɕ����Ă݂���B");

                UpdateMainMessage("���i�F�i���}�l�j�w�����f�B�X�F�A�[�e�B�ƃG���ɘA�����ɍs���g�R���B���Ⴀ�ȁB�x");

                UpdateMainMessage("���i�F���āA���ꂾ�������ē]�����Ă������̂�B");

                UpdateMainMessage("�A�C���F�A�[�e�B�E�E�E�G���E�E�E");

                UpdateMainMessage("�A�C���F�����A�Ȃ�قǁI�@����Ńt�@�[�W���{�a�Ɍq�����Ă���Ď����I");

                UpdateMainMessage("���i�F�����������Ɓ�@���ˁA���x�s���Ă݂܂����");

                UpdateMainMessage("�A�C���F�����������ȁI�@���������Ȃ񂾂��s���Ă݂�Ƃ��邩�I�@�b�n�b�n�b�n�I");
            }
            #endregion
            #region "���i�E�������@�E�X�L���̊�b�K��"
            else if (we.AvailableMixSpellSkill)
            {
                if (!we.AlreadyCommunicate)
                {
                    we.AlreadyCommunicate = true;

                    #region "���i�K���ς�"
                    if (sc.Level >= 20 && mc.FlashBlaze && !we.Truth_CommunicationLana22)
                    {
                        we.Truth_CommunicationLana22 = true;

                        UpdateMainMessage("�A�C���F�����A����ȃg�R�ɋ����̂���I");

                        UpdateMainMessage("���i�F�A�C������Ȃ��B�ǂ��������́H�@�₯�Ɋ�������������");

                        UpdateMainMessage("�A�C���F�����Ă���A�������񂾂悱�ꂪ�I�I");

                        UpdateMainMessage("���i�F�n�C�n�C�A�܂������̃o�J�����ˁE�E�E");

                        UpdateMainMessage("�A�C���F�܂��������킸�A���m�͈ꌩ�ɂ������I�I�I");

                        UpdateMainMessage("���i�F���m����Ȃ��ĕS����B�܂�����ȑO�ɂP�������ĂȂ��񂾂��ǁB");

                        UpdateMainMessage("�A�C���F�����������Ȃ��āB������A�����Ō��Ă��H");

                        UpdateMainMessage("���i�F�n�C�n�C�E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E�s�����A���ɉ��`�I�I");

                        UpdateMainMessage("�A�C���F�X�[�p�[�E�n�C�p�[�E�A���e�B���b�g�E�S�b�h�E�T���_�[�I�I�I");

                        UpdateMainMessage("�@�@�@�b�o�V���I�I�i�A�C���̓t���b�V���E�u���C�Y��������j�@�@�@");

                        UpdateMainMessage("�A�C���F������I���܂����I�I");

                        UpdateMainMessage("�A�C���F�b�n�b�n�b�n�b�n�b�n�I�I");

                        UpdateMainMessage("���i�F�E�E�E�@�E�E�E");

                        UpdateMainMessage("�A�C���F�ǂ����A���i�B�ǂ����ǂ����ǂ����I�H");
                        
                        UpdateMainMessage("�A�C���F�����J�����܂܍ǂ����Ă˂��悤���ȁI�@�b�n�b�n�b�n�I�I");

                        UpdateMainMessage("���i�F�����Ǝv���΁A���̃t���b�V���E�u���C�Y�ł���H");

                        UpdateMainMessage("���i�F��ƂȂ�Α����̃t�@�C�A�E�{�[���ɐ������̃_���[�W��ǉ����ʂŕt������ˁB");

                        UpdateMainMessage("���i�F�r�����@�́A�Α����̉r�����ɐ��C���[�W��Z�������邩��A�����P�����K�v�ˁB");

                        UpdateMainMessage("���i�F�A�C���A�����o�J�����疳�����Ǝv���Ă��񂾂��ǁA�ǂ��ŏK�������̂�H");

                        UpdateMainMessage("�A�C���F�E�E�E�b�O�E�E�E");

                        UpdateMainMessage("���i�F�Ђ���Ƃ��ēƊw�ŕ҂ݏo�����킯����Ȃ����ˁH");

                        UpdateMainMessage("�A�C���F���i�I�@���O�����Œm���Ă�񂾂�I�H");

                        UpdateMainMessage("���i�F�m���Ă�������A�m���Ă�Ɍ��܂��Ă邶��Ȃ���");

                        UpdateMainMessage("�A�C���F�����ʁI�I�@�Ȃ����I�I�I");

                        UpdateMainMessage("���i�F�u�����ʁv���Ăǂ��̑䎌�܂킵�g���Ă�̂�E�E�E");

                        UpdateMainMessage("���i�F�z���A�����Đ��t���[�����w�@�ɔ�ы��Œʂ��Ă��ł����");

                        UpdateMainMessage("�A�C���F�t�@���l���ݗ������w�@���炢�m���Ă邺�A���ꂪ�ǂ��������Ă񂾁H");

                        UpdateMainMessage("���i�F�����ŕ������@����ѕ����X�L���̊�b���w�񂾂̂�B");

                        UpdateMainMessage("���i�F�������A��ы���G���[�g�N���X�̐��k����̘b�Ȃ񂾂��ǂˁ�");

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "���i�͕������@�E�X�L���̊�b���K���ς݂������I";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("�A�C���F�E�E�E�b�N�E�E�E��������̂��瓪�͗ǂ�������ȁE�E�E");
                        
                        UpdateMainMessage("�A�C���F���Ⴀ�A���ɏK�����Ă����Ď����H");

                        UpdateMainMessage("���i�F���`��A���ꂪ�ˁB�����ł��Ȃ���B");
                        
                        UpdateMainMessage("���i�F�w�@������A���H�s�ׂ͋֎~����Ă��̂�B");

                        UpdateMainMessage("���i�F��ʐ��k�ɂ͋����ĂȂ����e�����A����҂�Ȍ��J�͋֎~���������ăg�R������B");

                        UpdateMainMessage("�A�C���F���Ⴀ���A���͂ǂ��Ȃ񂾂�H�������@�͉r���ł���̂��H");

                        UpdateMainMessage("���i�F���`��E�E�E�̂P�񂾂��w�@���Ŏ��������͂��邮�炢�ˁB");

                        UpdateMainMessage("�A�C���F�֎~����Ȃ������̂���E�E�E");

                        UpdateMainMessage("�A�C���F�܂��A�̂͗ǂ����āB���͂ǂ��Ȃ񂾁H");

                        UpdateMainMessage("���i�F�R�c���v���o���܂łɏ������Ԃ͂�����Ǝv�����ǁB");

                        UpdateMainMessage("���i�F���Ԃ���v���");

                        UpdateMainMessage("�A�C���F���i�E�E�E���O�͂���ς��������E�E�E");

                        UpdateMainMessage("���i�F�ł��z���g��������B�����������������ǁA�A�C���͂ǂ�����ďK�������̂�H");

                        UpdateMainMessage("�A�C���F�������閧�ɂ��Ă��������B�@�܂����x�b�����B");

                        UpdateMainMessage("���i�F�����A����������B");

                        UpdateMainMessage("���i�F���Ⴀ�A���x����͋@�����Ώ����P�����Ă�����ˁB");

                        UpdateMainMessage("�A�C���F�����A�b�������ď����邺�B���Ⴀ�A������͗��񂾂��I");

                        UpdateMainMessage("���i�F�t�t�t�A�C���Ă����āB���������͓̂��ӂ������", true);
                    }
                    #endregion
                    #region "�u���[�E�o���b�g"
                    else if ((sc.Level >= 21) && (!sc.BlueBullet))
                    {
                        UpdateMainMessage("���i�F���`��E�E�E���̃^�C�~���O����E�E�E");

                        UpdateMainMessage("�A�C���F���A�ǂ��������i�H�@�^�C�~���O���ǂ������񂾁H");

                        UpdateMainMessage("���i�F�A�C���A������Ƃ����ɋ��Ă��傤�����B");

                        UpdateMainMessage("�A�C���F�����B");

                        UpdateMainMessage("���i�F���������Ȃ����A�X�̒e�ہB�@�u���[�o���b�g�I");

                        UpdateMainMessage("�@�@�@�b�h�h�h�I�I�@�@");

                        UpdateMainMessage("�A�C���F�������������A�҂đ҂đ҂āI�I");

                        UpdateMainMessage("�A�C���F�񂾍��̂́B�@�A�C�X�E�j�[�h�����ĘA�˕s�\����˂��̂���H");

                        UpdateMainMessage("���i�F�傫�ȕX�̌`��ɑ΂��āA�������Ŗ��@�ŕ��f���銴���ŉr������́B");

                        UpdateMainMessage("���i�F��������ƁA�������e�ۏ�̕X���o���オ��킯���");

                        UpdateMainMessage("�A�C���F�ǂ��ł��������A����������ɂ���񂶂�Ȃ��B");

                        UpdateMainMessage("���i�F����Ȏ������Ă��A�ǂ����������Ⴄ�ł���B");

                        UpdateMainMessage("�A�C���F���̂͂��܂��ܔ�����ꂽ�������B");

                        UpdateMainMessage("���i�F�����Ȃ��ł����ƐH����Ă�ˁA��������Ȃ��ƈЗ͂��m�F�ł��Ȃ��񂾂����");

                        UpdateMainMessage("�A�C���F�܂Ă܂āA���������̂͂����Ƒ��̃^�[�Q�b�g�ŁE�E�E");

                        UpdateMainMessage("�@�@�@�b�h�h�h�h�h�h�h�h�h�I�I�@�@");

                        sc.BlueBullet = true;
                        ShowActiveSkillSpell(sc, Database.BLUE_BULLET);
                        UpdateMainMessage("", true);
                    }
                    #endregion
                    #region "���@�j�b�V���E�E�F�C��"
                    else if ((sc.Level >= 22) && (!sc.VanishWave))
                    {
                        UpdateMainMessage("���i�F�m������Ȋ���������������B");

                        UpdateMainMessage("�A�C���F�����A����Ă�ȃ��i�B�V�Z����I���H");

                        UpdateMainMessage("���i�F����A������ƍ���ˁA�̓ǂ�ł����@���T���܂��ǂݕԂ��Ă��́B");

                        UpdateMainMessage("�A�C���F���T�E�E�E�b�O�A���C���E�E�E");

                        UpdateMainMessage("���i�F�b�t�t�A�܂�������Ƃ���Ă݂��ˁB");

                        UpdateMainMessage("���i�F���Ⴀ�s�����A���@�j�b�V���E�E�F�C���I");

                        UpdateMainMessage("�A�C���F�u�A�b�t�H�I�H�H�E�E�E�~�]�ɂ���ɁE�E�E");

                        UpdateMainMessage("�A�C���F���́E�E�E��̉������񂾂�H");

                        UpdateMainMessage("���i�F���āA�o�J�A�C���͍��̏�ԂŖ��@�����Ă邩�����");

                        UpdateMainMessage("�A�C���F�b�Q�A�܂����I");

                        UpdateMainMessage("�A�C���F�i�E�E�E�b�t�@�C�A�I�j");

                        UpdateMainMessage("�A�C���F�b�N�\�A����ς�I�H");

                        UpdateMainMessage("�A�C���F�i�E�E�E�b�t�@�C�A�A�t�@�C�A�I�j");

                        UpdateMainMessage("���i�F���̒ʂ��@���@�̉r���������������������钾�ٌ��ʂ��");

                        UpdateMainMessage("�A�C���F�i�E�E�E�b�t�@�C�A�A�t�@�C�A�A�t�@�C�A�I�j");

                        UpdateMainMessage("���i�F������Ƃ��̕ӂł�߂Ƃ��Ȃ�����A���������m�Ȏp��A�b�t�t�t��");

                        UpdateMainMessage("�A�C���F�������傤�E�E�E���ꂢ�܂ő����񂾂�H");

                        UpdateMainMessage("���i�F����Ȃɒ����͖������ǂ�����x�͑������B");

                        UpdateMainMessage("�A�C���F���₢��A���^�[���Ȃ񂾂�I�H");

                        UpdateMainMessage("���i�F�R�^�[������Ȃ�����������B");

                        UpdateMainMessage("�A�C���F�����Ȃ̂��E�E�E���āA���\�����ȁB");

                        UpdateMainMessage("���i�F�܂��A���΂炭�͒��߂Ȃ�����");

                        UpdateMainMessage("�A�C���F�i�E�E�E�t�@�C�A�I�j");

                        sc.VanishWave = true;
                        ShowActiveSkillSpell(sc, Database.VANISH_WAVE);
                        UpdateMainMessage("", true);
                    }
                    #endregion
                    #region "�_�[�P���E�t�B�[���h"
                    else if ((sc.Level >= 23) && (!sc.DarkenField))
                    {
                        UpdateMainMessage("���i�F�t�B�[���h�W�J�^�̉r���`�Ԃ͓���̃^�[�Q�b�g�����֌����ĕ��o����̂ł͂Ȃ��E�E�E");

                        UpdateMainMessage("�A�C���F���ς�炸�A���̓�����Ȏ��T��ǂ�ł�񂾂ȁB");

                        UpdateMainMessage("���i�F���̏ꍇ�A�o�J�A�C���ƈ���Ēm�����܂���������g�R����n�߂�̂�B");

                        UpdateMainMessage("�A�C���F����A�����Ӗ�����˂����āB�ǂ����������g�R����o���Ă�����ȁB");

                        UpdateMainMessage("���i�F���[��A�����ė����������Ƃǂ��ɂ��Ȃ�Ȃ��Ǝv��Ȃ��H");

                        UpdateMainMessage("�A�C���F�܂��E�E�E�ŋ߂͂����v����߂��E�E�E�B");

                        UpdateMainMessage("���i�F�܁A�ǂ���B�Ƃ肠��������Ă݂��ˁB");

                        UpdateMainMessage("���i�F��n�̉��b���Ւf�����ŁA�����_�[�P���E�t�B�[���h�I");

                        UpdateMainMessage("�@�@�@�i�A�C���̎��ӈ�̂����Â��ł������n�߂��I�j");

                        UpdateMainMessage("�A�C���F����E�E�E�Ȃ񂾂���I�H�}�ɉ����A�_�E�����銴�����E�E�E");

                        UpdateMainMessage("���i�F���̃t�B�[���h�ɂ���Ԃ́A�����h��Ɩ��@�h�䂪�_�E�������ԂƂȂ��B");

                        UpdateMainMessage("�A�C���F�}�W����A�{���ʓ|�������̂������ȁA�������@���Ă̂́B");

                        UpdateMainMessage("�A�C���F���Ⴀ�A�E�E�E�_�b�V���ł��̃t�B�[���h����I");

                        UpdateMainMessage("�@�@�@�i�A�C�����_�b�V������ƁA�t�B�[���h�S�̂��A�C���ߕӂ�Ǐ]���Ă���I�j");

                        UpdateMainMessage("�A�C���F�����I�H�������Ȃ��̂���I�H");

                        UpdateMainMessage("���i�F�����ˁA�t�B�[���h�W�J�͓���̃^�[�Q�b�g�������킯����Ȃ��񂾂���");

                        UpdateMainMessage("���i�F����������t�B�[���h���ɋ������̂͑S�ă^�[�Q�b�g�ɏo����݂����B�S�̖��@�ˁB");

                        UpdateMainMessage("�A�C���F�Q�l�����Ƃ��āA�Q�l�ʁX�ɓ�������ǂ��Ȃ�񂾂�H");

                        UpdateMainMessage("���i�F�Q�l���ɂ����ƕ������ꂽ��Ԃł��̃t�B�[���h���Ǐ]����̂�B");

                        UpdateMainMessage("�A�C���F���ȁA�Ȃ�Ă��s����`�ȁE�E�E");

                        UpdateMainMessage("���i�F���A���������B�܂������������ĂȂ�������ˁA�{���ɖh��_�E�����Ă�̂������");

                        UpdateMainMessage("�A�C���F�����āA���͂��낻�낱�̕ӂŁE�E�E");

                        UpdateMainMessage("���i�F���ʂ�A�����������_�~�[�f�U��N���A�C���ɃZ�b�g���Ă����������");

                        UpdateMainMessage("�@�@�@�i�_�~�[�f�U��N�́y���݉��`�F�W���ƒf��z������Ă����I�j");

                        UpdateMainMessage("�A�C���F�i�E�E�E�b�o�^�j");

                        sc.DarkenField = true;
                        ShowActiveSkillSpell(sc, Database.DARKEN_FIELD);
                        UpdateMainMessage("", true);
                    }
                    #endregion
                    #region "�t���[�`���[�E���B�W����"
                    else if ((sc.Level >= 27) && (!sc.FutureVision))
                    {
                        UpdateMainMessage("���i�F���`��A�ʓ|��������ˁA�z���g�E�E�E");

                        UpdateMainMessage("�A�C���F�������ȁA���i�ł��ʓ|��������̂��H");

                        UpdateMainMessage("���i�F�_�~�[�f�U��N�̃Z�b�e�B���O��B���͍��ڂ��������Ėʓ|�Ȃ̂�B");

                        UpdateMainMessage("�A�C���F�����A�����V�����Z�ł��K�����悤���Ă̂��H�@���Ȃ牴�����݂����B");

                        UpdateMainMessage("���i�F�����Ȃ񂾂��ǁE�E�E");

                        UpdateMainMessage("���i�F�o�J�A�C�����ƁE�E�E");

                        UpdateMainMessage("�A�C���F�����ے�I�Ȃ��߂炢�����ȁB�@�����Ꮥ���ɂȂ�Ȃ��Ƃł��H");

                        UpdateMainMessage("���i�F���`��A���������킯����Ȃ���B");

                        UpdateMainMessage("���i�F���Ⴂ����B������ƃ\�R�ɗ����āB");

                        UpdateMainMessage("�A�C���F�_���[�W�n�͔����邼�A�ǂ���ȁH");

                        UpdateMainMessage("���i�F�ʖڂ�B�_���[�W�n�͂����ƐH����Ă�ˁB");

                        UpdateMainMessage("�A�C���F���ʂʁE�E�E");

                        UpdateMainMessage("���i�F�܂�����̓_���[�W����Ȃ���B���S���ā�");

                        UpdateMainMessage("�A�C���F�ӂ��A�����͏����邺�B�@�ŁA�ǂ����H");

                        UpdateMainMessage("���i�F������Ƒ҂��ĂĂˁE�E�E�����ƁE�E�E");

                        UpdateMainMessage("�@�@�@�i���i�͑傫���[�ċz����x�s�����B�j");

                        UpdateMainMessage("���i�F����A����������B");

                        UpdateMainMessage("�A�C���F�͂��H");

                        UpdateMainMessage("���i�F�����������Č����Ă�̂�B����������B");

                        UpdateMainMessage("�A�C���F�����H");

                        UpdateMainMessage("���i�F��A���ʎ������Ԃ����邩��A��������Ă�ˁB");

                        UpdateMainMessage("�A�C���F������A��������H�@�S�R�킩��˂����B");

                        UpdateMainMessage("���i�F�����C���X�^���g�ōs�����āB�@�����ƕ����邩��B");

                        UpdateMainMessage("�A�C���F�����A�ǂ��̂���B�@���Ⴀ�E�E�E");

                        UpdateMainMessage("�A�C���F�t�@�C�A�I");

                        UpdateMainMessage("�A�C���F���ƁI�H�@�����I�I�I");

                        UpdateMainMessage("�@�@�@�i�A�C���͂����܂��r���̐�������A�t�@�C�A�E�{�[���r���Ɏ��s�����B�j");

                        UpdateMainMessage("�A�C���F��A���������B������Ƒ҂��Ă���B�����P�񔭓������邩��B");

                        UpdateMainMessage("���i�F�t�t�A�����ǂ����B�A���K�g��");

                        UpdateMainMessage("�A�C���F���₢��A�P�Ɏ��s�������������炳�B");

                        UpdateMainMessage("���i�F��������Ȃ��̂�B�g���K�[�h�C�x���g�^�X�L���Ȃ̂�B");

                        UpdateMainMessage("�A�C���F���́H�����y���ƃx���g�H");

                        UpdateMainMessage("���i�F�g�E���E�K�E�|�E�h�E�C�E�x�E���E�g");

                        UpdateMainMessage("���i�F�A���^�̃C���X�^���g�s�������m���Ă�����J�E���^�[����X�L����B");

                        UpdateMainMessage("���i�F������A���̉r�����s�͎����d�|�����g���K�[�������������Ă킯�B");

                        UpdateMainMessage("�A�C���F�܂������������āB����͗ǂ����炳�A�����P�񔭓������邩��҂��Ă�B");

                        UpdateMainMessage("���i�F�����`�`�E�E�E������o�J�A�C���Ƃ͂�肽���Ȃ������̂�E�E�E");

                        UpdateMainMessage("���i�F����ǂ���A�����P��t�@�C�A�E�{�[�����肢�B");

                        UpdateMainMessage("�A�C���F������I�C����I");

                        UpdateMainMessage("�A�C���F�t�@�C�A�I");

                        UpdateMainMessage("�@�@�@�i�{�V���E�D�D�j");

                        UpdateMainMessage("�A�C���F�E�E�E�ŁH");

                        UpdateMainMessage("���i�F����A�m�Â͏I�����");

                        UpdateMainMessage("�A�C���F�����l�^���悭�킩��˂����E�E�E");

                        UpdateMainMessage("���i�F�b�z���z���A���Ⴀ�܂��ˁB����̓A���K�g��");

                        UpdateMainMessage("�A�C���F���A�������E�E�E");

                        sc.FutureVision = true;
                        ShowActiveSkillSpell(sc, Database.FUTURE_VISION);
                        UpdateMainMessage("", true);
                    }
                    #endregion
                    #region "���J�o�["
                    else if ((sc.Level >= 28) && (!sc.Recover))
                    {
                        UpdateMainMessage("���i�F�i����͑��肪�K�v��ˁE�E�E�j");

                        UpdateMainMessage("�A�C���F������A���Ⴀ��������ɂȂ��Ă�邺�I");

                        UpdateMainMessage("���i�F�b���I�A�C�������́H�r�b�N�����邶��Ȃ��B");

                        UpdateMainMessage("�A�C���F���i���������l������ł�����ȁB�C�Â��Ȃ���������H");

                        UpdateMainMessage("���i�F�����ˁA�����C�z�����������Ċ��������E�E�E");

                        UpdateMainMessage("���i�F���A���������B�ǂ��������");

                        UpdateMainMessage("�A�C���F�����A���ł��������I");

                        UpdateMainMessage("���i�F���Ⴀ�A�܂��͂��̃_�~�[�f�U��N�̍U����H����Ă��傤������");

                        UpdateMainMessage("�A�C���F�}�W����E�E�E���Ⴀ�E�E�E");

                        UpdateMainMessage("�@�@�@�i�{�O�b�V���A�@�@�@�E�E�E�i�A�C���͋C�₵���j�j");

                        UpdateMainMessage("���i�F�C�₵���݂����ˁE�E�E���Ⴀ�A�b�n�C�I");

                        UpdateMainMessage("�@�@�@�i�A�C���͋C�₩��񕜂����j");

                        UpdateMainMessage("�A�C���F�b�c�A�c�c�c�E�E�E�ɂ��ȁE�E�E");

                        UpdateMainMessage("�A�C���F���������A������ƃ_�~�[�N�̍U�����������˂����H�f�U�背�x������˂�����A���̂�");

                        UpdateMainMessage("���i�F�ǂ�����Ȃ��A�o�J�A�C���������v��");

                        UpdateMainMessage("���i�F���Ⴀ�A�����P���");

                        UpdateMainMessage("�@�@�@�i�{�O�b�V���A�@�@�@�E�E�E�i�A�C���͋C�₵���j�j");

                        UpdateMainMessage("���i�F�C�₵���݂����ˁE�E�E���Ⴀ�A�b�n�C�I");

                        UpdateMainMessage("�@�@�@�i�A�C���͋C�₩��񕜂����j");

                        UpdateMainMessage("�A�C���F�b�ɁE�E�E���A���������҂đ҂āA���ʂ͂Ȃ�ƂȂ��킩�����I�C������̃X�L������I�H");

                        UpdateMainMessage("���i�F���Ⴀ�A�����P���");

                        UpdateMainMessage("�A�C���F�҂āA�҂đ҂đ҂āI�I");

                        UpdateMainMessage("�@�@�@�i�{�O�b�V���A�@�@�@�E�E�E�i�A�C���͋C�₵���j�j");

                        sc.Recover = true;
                        ShowActiveSkillSpell(sc, Database.RECOVER);
                        UpdateMainMessage("", true);
                    }
                    #endregion
                    #region "�g���X�g�E�T�C�����X"
                    else if ((sc.Level >= 29) && (!sc.TrustSilence))
                    {
                        UpdateMainMessage("���i�F����ƁA����͂�����ˁE�E�E�ŁA���ꂩ��E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E�i�R�b�\���j");

                        UpdateMainMessage("���i�F�Ƃ���ŁA�o�J�A�C���͎�����ɂł��Ȃ��Ă����̂������");

                        UpdateMainMessage("�A�C���F�������I�Ȃ�ŕ��������񂾂�I�H");

                        UpdateMainMessage("���i�F�A���^���́i�R�b�\���j�����o�܂���ł���B����Ȃ̒N�����Ď@�m�o������B");

                        UpdateMainMessage("�A�C���F�i�O�̂��̎��́A��肭�s�����̂ɁE�E�E�i�[�E�E�E�j");

                        UpdateMainMessage("���i�F�܂��ł��E�E�E����̂�������₱�����̂�ˁE�E�E");

                        UpdateMainMessage("�A�C���F���v���B�L�b�`�������t���Ă����Α��v�I");

                        UpdateMainMessage("���i�F�A���^�̂��������g�R���]�v��₱�����񂶂�Ȃ��E�E�E�܂��ǂ���B");

                        UpdateMainMessage("���i�F����̂͐�ɐ������Ă�����ˁB");

                        UpdateMainMessage("���i�F�r�����u�g���X�g�E�T�C�����X�v");

                        UpdateMainMessage("���i�F���ʂ̑Ώۂ͎������g�����B");

                        UpdateMainMessage("���i�F���ʂ̓��e�́A�Ώۂ֒���/�È�/�U�f�ɑ΂���ϐ��i���ʂ�h���j��t�^����B");

                        UpdateMainMessage("���i�F���ʂ�������ꂽ���ɂ��ꂪ������̂ł͂Ȃ��A���������u�ԂɌ��ʂ���菜�����B");

                        UpdateMainMessage("���i�F�����Ă݂�΁A�\�ߏ������Ă����āA�̐��𐮂��Ă����悤�Ȋ����ɂȂ��ˁB");

                        UpdateMainMessage("���i�F���Ȃ݂ɁA���̌��ʂ͈�x�L���B��U���ق�ÈŌ��ʂ�������ꂽ�ꍇ�A�����ɉ����ƂȂ��B");

                        UpdateMainMessage("���i�F�d�˂������o���Ȃ��݂���������A�ߓx�Ȋ��҂͏o���Ȃ������m��Ȃ���ˁB");

                        UpdateMainMessage("���i�F�C���X�^���g�^�C�~���O�ł��o���邩��Ƃ����ɂ�����̂��x�X�g�ȃ^�C�~���O�ɂȂ肻����B");

                        UpdateMainMessage("���i�F�E�E�E���āA������ƁE�E�E���������Ă�H");

                        UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E�b�n�I�I");

                        UpdateMainMessage("�A�C���F�b�n�b�n�b�n�b�n�I�@�ǂ��������i�I�@�������A��̃I�b�P�[�I�I");

                        UpdateMainMessage("�@�@�@�w�b�h�S�H�H�H�H���I�I�I�x�i���i�̃T�C�N���C�h�E�u���[���A�C�����y��j�@�@");

                        sc.TrustSilence = true;
                        ShowActiveSkillSpell(sc, Database.TRUST_SILENCE);
                        UpdateMainMessage("", true);
                    }
                    #endregion
                    #region "�X�J�C�E�V�[���h"
                    else if ((sc.Level >= 30) && (!sc.SkyShield))
                    {
                        UpdateMainMessage("���i�F�b�t�t�A���ꂪ�o����Α�����ˁ�");

                        UpdateMainMessage("�A�C���F�Ȃ񂾂����Ԃ�Ɗy���������ȁB");

                        UpdateMainMessage("���i�F�w���x���@�Ƃ̕������@�̉r����b���悤�₭�K���ł��������");

                        UpdateMainMessage("�A�C���F�b�o�A�o�J�ȁI�\�Ȃ̂���I�H");

                        UpdateMainMessage("���i�F�\�ˁA��b�����������Ă���΁�");

                        UpdateMainMessage("�A�C���F�}�W����E�E�E�b�n�n�n�A�����������A���i�B");

                        UpdateMainMessage("���i�F����A�w���x�w���x�ɂ�鋭�͂ȕ������@��B�ǂ��H�@���ĂĂ�ˁH");

                        UpdateMainMessage("�A�C���F���A�����B���O�����ǂ���΁A���܂ł����ĂĂ�邺�B");

                        UpdateMainMessage("�@�@�@�w�b�V���S�I�H�H�I�H�H�I�I�I�x�i���i�̃��C�g�j���O�L�b�N���A�C�����y��j�@�@");

                        UpdateMainMessage("�A�C���F�ȁE�E�E�i�[�E�E�E�J�߂����肪�E�E�E�i�b�O�z�j");

                        UpdateMainMessage("���i�F�����C���C�������錾���������������B�@�����͍l���Č��t��I�т�����B");

                        UpdateMainMessage("���i�F�܂��ǂ���A���ĂāB");

                        UpdateMainMessage("�@�@�@�i���i�͉̂Ɏ����悤�Ȋ����ŁA�Y��ȉr�������n�߂��E�E�E�j");

                        UpdateMainMessage("�A�C���F�i�E�E�E�ւ��E�E�E���̃��i���ˁE�E�E�j");

                        UpdateMainMessage("���i�F���ɂ̓V��������������A�X�J�C�E�V�[���h�I");

                        UpdateMainMessage("�@�@�@�i�b�p�L�B�B���E�E�E�j");

                        UpdateMainMessage("���i�F�܂��܂��̏o���h���ˁ�");

                        UpdateMainMessage("�A�C���F�����A�����h��ǂ݂����Ȃ̂��o�����ȁB");

                        UpdateMainMessage("���i�F�A�C���̃o�J�t�@�C�A�ł������Ă݂ā�");

                        UpdateMainMessage("�A�C���F�o�J�t�@�C�A�ł͂Ȃ��A�t�@�C�A�{�[�����B�@���Ⴀ�s�����I");

                        UpdateMainMessage("�A�C���F�t�@�C�A�I");

                        UpdateMainMessage("�@�@�@�i�b�p�V�C�B�B�I�I�j");

                        UpdateMainMessage("���i�F�b�t�t�t�A�������");

                        UpdateMainMessage("�A�C���F�����I�H�}�W���悻��I�H");

                        UpdateMainMessage("���i�F�������A����͂ˁB�d�˂������\�Ȃ́B���Ăā�");

                        UpdateMainMessage("�@�@�@�i���i�͂R��X�J�C�E�V�[���h���r�������j");

                        UpdateMainMessage("���i�F���v�R��܂Ŗ��@�_���[�W�𖳌����o����̂��");

                        UpdateMainMessage("�A�C���F�I�C�I�C�I�C�A��������˂��̂���H");

                        UpdateMainMessage("���i�F�퓬�ł͂R��r������^�C�~���O�͖��������m��Ȃ����A�����܂Ŕ�������Ȃ����B");

                        UpdateMainMessage("�A�C���F�܂��A���������m��˂����ǂ��E�E�E���������đ��ς�炸���Ȋ�������ȁB");

                        UpdateMainMessage("�A�C���F�Ƃ���ŁA�ǂ�Ȗ��@�_���[�W�ł��ΏۂɂȂ�̂��H");

                        UpdateMainMessage("���i�F�����ˁA������ʂƂ��͓��ɖ�����B");

                        UpdateMainMessage("�A�C���F�t���C���E�I�[���݂����Ȓǉ����ʂ̖��@�_���[�W�́H");

                        UpdateMainMessage("���i�F�ǂ����ɋC�Â���ˁA���������̂��Ώۂ�B");

                        UpdateMainMessage("�A�C���F���i�����ӂ̃u���[�E�o���b�g�͂R����Ď��ɂȂ�̂��H");

                        UpdateMainMessage("���i�F�����ˁA�u���[�o���b�g�������ꍇ�A�R�񕪏���Ă��܂����ɂȂ��B");

                        UpdateMainMessage("�A�C���F�Ȃ�قǁA��������T���L���[�ȁB");

                        UpdateMainMessage("���i�F���P�ɑf����ˁA�����C�����������B�������̂ł��H�ׂ��́H");

                        UpdateMainMessage("�A�C���F����A�������n�̃��c������ȁB���O�ɒm���Ă������������̂��B");

                        UpdateMainMessage("���i�F�Ӂ`��A�����Ȃ񂾁B�܂��A��͐�p������Ċ����ˁB");

                        UpdateMainMessage("���i�F���Ȃ݂ɁA�o�J�A�C���ɂ͐�΂����Ȃ������");

                        UpdateMainMessage("�A�C���F�}�W����E�E�E�s���`�Ȏ��͗��ނ��A�z���g�B");

                        UpdateMainMessage("���i�F�l���Ă����Ă������");

                        sc.SkyShield = true;
                        ShowActiveSkillSpell(sc, Database.SKY_SHIELD);
                        UpdateMainMessage("", true);
                    }
                    #endregion
                    #region "�X�^�[�E���C�g�j���O"
                    else if ((sc.Level >= 31) && (!sc.StarLightning))
                    {
                        UpdateMainMessage("�A�C���F���i�A���܂ɂ͔тł��H�ׂɍs�����H");

                        UpdateMainMessage("���i�F���[��A�s���������Ȃ񂾂��ǁA���̖��@��������Ă�����������B");

                        UpdateMainMessage("�A�C���F�����A��邶��˂����B������A�g�R�g���t���������I");

                        UpdateMainMessage("���i�F���E�E�E���Ⴀ�A�I����Ă���ł������Ȃ�A�H�ׂɍs���܂����");

                        UpdateMainMessage("�A�C���F���₢��A����Ȕт��炢�ǂ��ł��������āA�����I��낤���I");

                        UpdateMainMessage("���i�F���A�I����Ă���s�����Č����Ă邶��Ȃ��I�H�@�\�b�`����U���������ɁB");

                        UpdateMainMessage("�A�C���F���A���₢��B���Ⴀ�I����Ă���s�����I�@���ȁI");

                        UpdateMainMessage("���i�F���[�C�C���A�ʓ|�����������ŗǂ��񂶂�Ȃ�������B");

                        UpdateMainMessage("�A�C���F���₢�₢��E�E�E");

                        UpdateMainMessage("���i�F�������I�C���C�����Ă�����I�I�@�s�����I�I�I");

                        UpdateMainMessage("�A�C���F�i�q�G�F�F�E�E�E�j");

                        UpdateMainMessage("���i�F���S�_�A�V�󂩂��n�֖����Ȃ闋���������̃o�J�ɕ�����I�I�@�X�^�[���C�g�j���O�I�I�I");

                        UpdateMainMessage("���i�F��炢�Ȃ����I�I�@�i���ɋC�₵�Ă邪�ǂ���I�I�I");

                        UpdateMainMessage("�@�@�@�i�b�o�o�o�o�o���o���o���o���I�I�I�i�X�[�p�[�N���e�B�J���q�b�g���y��j");

                        UpdateMainMessage("�@�@�@�i�A�C���͓|���u�ԁA�y�b�̗���ɂ͋C��z�낤�z�ƐS�ꂩ�琾�����B�j");

                        sc.StarLightning = true;
                        ShowActiveSkillSpell(sc, Database.STAR_LIGHTNING);
                        UpdateMainMessage("", true);
                    }
                    #endregion
                    #region "�T�C�L�b�N�E�g�����X"
                    else if ((sc.Level >= 32) && (!sc.PsychicTrance))
                    {
                        UpdateMainMessage("���i�F���ӂ��E�E�E����͖{���ɏo���Ȃ���ˁE�E�E");

                        UpdateMainMessage("�A�C���F�������ȁA���i���K���ɋ�킷��Ȃ�āB");

                        UpdateMainMessage("���i�F�A�C���͂ǂ��Ȃ́H�t�����Ɋւ��ẮB");

                        UpdateMainMessage("�A�C���F�������ȁA�����������ӂ��ɂ���I�ڂƂ����킯�ɂ͂����˂��B");

                        UpdateMainMessage("�A�C���F�J�[���݂ɍu�`�͂��Ă�����Ă��񂾂��A���ꂾ�����ǂ����Ă����ЂƂ��ȁB");

                        UpdateMainMessage("���i�F������Ƒ҂��Ă�I");

                        UpdateMainMessage("�A�C���F��H������B");

                        UpdateMainMessage("�A�C���F�E�E�E�b�n�I�@���A���Ȃ�Č������I�H");

                        UpdateMainMessage("���i�F�J�[���݂��Ăǂ���������A�܂����V�j�L�A�E�J�[���n���c���݂���Ȃ��ł��傤�ˁI�H");

                        UpdateMainMessage("�A�C���F�E�E�E�n�C�A���̒ʂ�ł����E�E�E");

                        UpdateMainMessage("���i�F�E�E�E�{���ɁH");

                        UpdateMainMessage("�A�C���F�ȁA������B���̕s�v�c�ȃI�[���́E�E�E������Ƒ҂����A����Ȃ�H");

                        UpdateMainMessage("���i�F�{�����ǂ����𕷂��Ă邾����A�{���Ȃ́H");

                        UpdateMainMessage("�A�C���F���A�����E�E�E�{�����B���̈Ј����A�ԈႢ�Ȃ��{�����B");

                        UpdateMainMessage("���i�F�E�\�ł���E�E�E���Ńo�J�A�C��������ȏ��ɒʂ��Ă�̂�H");

                        UpdateMainMessage("�A�C���F�����A�F�X�ƃ��P�������Ă��ȁB");

                        if (mc.Level >= 30)
                        {
                            UpdateMainMessage("�A�C���F�i�҂Ă�A���������x�A��ė������Č����Ă��ȁE�E�E�j");
                        }

                        UpdateMainMessage("�A�C���F�Ȃ�Ȃ�A���i���ꏏ�ɗ��Ă݂邩�H");

                        UpdateMainMessage("���i�F�����A�o���鎖�Ȃ炨�肢���������炢��B");

                        UpdateMainMessage("�A�C���F�悵�A���������B���Ⴀ�������֗��Ă���B");

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "�A�C���ƃ��i�͓]�����u�ŃJ�[���݂̌��ւƌ��������B";
                            md.ShowDialog();
                        }

                        this.buttonHanna.Visible = false;
                        this.buttonDungeon.Visible = false;
                        this.buttonRana.Visible = false;
                        this.buttonGanz.Visible = false;
                        this.buttonPotion.Visible = false;
                        this.buttonDuel.Visible = false;
                        this.buttonShinikia.Visible = false;
                        ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_SECRETFIELD_OF_FAZIL);

                        UpdateMainMessage("�A�C���F�����A���������B");

                        UpdateMainMessage("���i�F���H�@�{���ɂ���ȏ��ɋ���킯�H");

                        UpdateMainMessage("�J�[���F�M���̓��i�E�A�~���A���H");

                        UpdateMainMessage("���i�F�J�[���n���c���݁A�{������I�@�����ɂ��ڂɂ�����܂��B");

                        UpdateMainMessage("�A�C���F�������i�A���P�ɋX�����ȁB");

                        UpdateMainMessage("���i�F�i��������ƁA�A���^�ǂꂾ���o�J�߂���̂�A�T���Ă�ˁA�z���b�g�j");

                        UpdateMainMessage("�A�C���F���₢��A�Ă������Ј���������߂��āA����ȍT����ɂȂ񂩖����񂾂��ă}�W�ŁB");

                        UpdateMainMessage("�J�[���F�M���A���炾���A���X�q�������Ă��炤�B");

                        UpdateMainMessage("�@�@�@�w�[�Ⴊ�M�������Ɠ����͂��߂��I�x");

                        UpdateMainMessage("���i�F���Ɍ��h�̋ɂ݁A�S���Ĕq��d��܂��B");

                        UpdateMainMessage("�J�[���F�E�E�E�@�E�E�E�@�E�E�E");

                        UpdateMainMessage("�J�[���F�X�y�������w��  ��  ��x�@�T�^�I������");

                        UpdateMainMessage("�A�C���F�T�^�I�I�H�@�ǂ����I�H");

                        UpdateMainMessage("���i�F�i�ǂ�����ق��ĂȂ�����E�E�E�z���b�g�����E�E�E�j");

                        UpdateMainMessage("�J�[���F�X�L�������w��  �_  ���S�x�@���H���R�h");

                        UpdateMainMessage("�J�[���F�\���čU������X�^�C���Ƃ͈Ⴂ�A�G�̋}���Ƀ^�[�Q�b�g���i��̂ɐ�O�B");

                        UpdateMainMessage("�J�[���F�삯������S����ɂ͎������܂��A�����܂Ŏ��H�E�������d������X���B");

                        UpdateMainMessage("�J�[���F���@�U���������͑̏p�̃_���[�W���厲�Ƃ��A�O�q�E��q�̂�������\�B");

                        UpdateMainMessage("�J�[���F����̔g�◬��Ɉˑ������A�L�b�`���Ƃ������_�ő�����d���߂�B");

                        UpdateMainMessage("�J�[���F�Ȃ�قǁA�m���ɂ����̋M�N�̑��_�Ƃ��āA�ӂ��킵���^�C�v�ł͂���B");

                        UpdateMainMessage("�@�@�@�w�[�Ⴊ�X�ɃM�������Ɠ������I�x");

                        UpdateMainMessage("�J�[���F�������A�u�ԓI�ǖʂɂ����āA�œK��p�̑I���ɖR�����B");

                        UpdateMainMessage("�A�C���F�I�I");

                        UpdateMainMessage("�J�[���F�p�[�e�B�A�g��P�l�Z�I�͂��������o������̂́A�_��Ȏ�荇�킹�ł͂Ȃ��p�^�[������Ɋׂ�X���B");

                        UpdateMainMessage("�J�[���F���i�o����͂��̍s�����A���̋ǖʂɂ��Ĕ��������A�v�����Ɏ���P�[�X�����݁B");

                        UpdateMainMessage("�J�[���F�������A����قǂ̋ǖʂ͂���قǂȂ��낤�B���ɋ�����̂��J�o�[���Ă���Ώ��X�̎��B");

                        UpdateMainMessage("�A�C���F�i�E�E�E��k����E�E�E�����肷�����E�E�E�������̐l�E�E�E�j");

                        UpdateMainMessage("�J�[���F�ӂށA���̎��ɑ΂��ẮA���ɒb�B������X��ς�ł���悤���ȁB");

                        UpdateMainMessage("�J�[���F�ǂ��S�\�����B���̒b�B�A�Y�ꂸ�ɑ�����Ɨǂ��B");

                        UpdateMainMessage("���i�F����ʉ䂪�g�ɑ΂��A�őP�Ȃ�[���B�ނ�Œo�܂ʓw�͂������Ă��������܂��B");

                        UpdateMainMessage("�A�C���F�����ƁE�E�E�@�E�E�E�����A���i�B�ǂ��Ȃ��Ă񂾂����B");

                        UpdateMainMessage("���i�F�V�j�L�A�E�J�[���n���c���݂́A���t���[�����w�@�̓Ɨ����s�@�ւ̒���B");

                        UpdateMainMessage("���i�F���@�̎g�p�Ɋւ��Ă͈�ʓI�Ȍ��̏�ɂ����āA���ł����F�߂��Ă邯��");

                        UpdateMainMessage("���i�F�{���̎g��������������@�ŁA�l���ɔ�Q���������肷��y���������������̂�B");

                        UpdateMainMessage("���i�F�ł��A�����������ނ�͌��݂����ɂ͂��Ȃ��B�Ȃ�����������H");

                        UpdateMainMessage("�A�C���F�}�W���E�E�E����Ȑ����l�Ȃ̂���B�`����FiveSeeker��������Ȃ������̂��B");

                        UpdateMainMessage("�J�[���F���̂悤�Șb�͂ǂ��ł��ǂ����B�Y��Ȃ����B");

                        UpdateMainMessage("�A�C���F���炢�����܂����B���A�������ƁE�E�E�ł́A�J�A�J�A�J�[���n���E�E�E");

                        UpdateMainMessage("�J�[���F���܂Œʂ�ŗǂ��B���ꂩ��M�����A�����Ǝ��R�̂Őڂ��Ă����������B");

                        UpdateMainMessage("���i�F�E�E�E�n�C�B");

                        UpdateMainMessage("�J�[���F�v���͂������߁A�t���������̏K���Ƃ����������B");

                        UpdateMainMessage("���i�F�n�C�B");

                        UpdateMainMessage("�A�C���F���������A�}�W����E�E�E�ǂ񂾂������ʂ��Ȃ񂾂�B");

                        UpdateMainMessage("�J�[���F�M���͗��_�̖����̌́A���o���Ȃ��悤���ȁB����͂���Ő������B");

                        UpdateMainMessage("�J�[���F�����A���̋t���������̏ꍇ�Ɍ���y���O�̗��z���C���[�W���Ȃ���΂Ȃ�Ȃ��B");

                        UpdateMainMessage("�J�[���F���_�̌��E�Ƃ͐��_���̂��̂Ɍ���������B������C���[�W���Ă���𒴂���悤�ɂ��Ȃ����B");

                        UpdateMainMessage("�J�[���F��x���������悤�B�@���͂��Ȃ����B");

                        UpdateMainMessage("�J�[���F�䂪���_�։i���Ȃ���������点�A�y�T�C�L�b�N�E�g�����X�z");

                        UpdateMainMessage("�@�@�@�i�J�[���͖ڂɂ��~�܂�ʑ����ŉr�����s�����I�j");

                        UpdateMainMessage("�A�C���F�i���ȁI�@�́A�����������I�I�I�j");

                        UpdateMainMessage("�J�[���F�������B����ŉ䂪���@�U���͍͂X�ɑ������ꂽ���ƂȂ�B");

                        UpdateMainMessage("�J�[���F�������㏞�Ƃ��Ė��@�h��͂͊i�i�ɗ�����B�g�������̐S�Ȃ̂Ŋo���Ă����Ȃ����B");

                        UpdateMainMessage("���i�F�n�C�A���肪�Ƃ��������܂����B");

                        UpdateMainMessage("�A�C���F�����ƁE�E�E���̑������đS�R�����Ȃ������񂾂��E�E�E");

                        UpdateMainMessage("�J�[���F�����͏d�v�ł͂Ȃ��B�C���[�W�������炵��������c������Ɨǂ��B");

                        UpdateMainMessage("�A�C���F���A�͂��B�ǂ����ł��B");

                        UpdateMainMessage("�J�[���F���ɕ����������͂��邩�ˁB");

                        UpdateMainMessage("�A�C���F�����A���Ⴀ�ǂ��ł����B���̂������̉r���^�C�~���O�̃g�R�ł�����");

                        UpdateMainMessage("���i�F�i������ƁI�@�o�J�A�C���A�{���ɖ����˃A���^�j");

                        UpdateMainMessage("�J�[���F�悢�A�����Ȃ����B");

                        UpdateMainMessage("�A�C���F��u��������̍��킹�������t�ɏd�ˍ��킹�Ă��悤�Ɍ�������ł����ǁH");

                        UpdateMainMessage("�J�[���F�t�����Ə����������荇�킹������������������m�A����e�Ƃ��ėZ�����������́B");

                        UpdateMainMessage("�J�[���F�M�N�ɂ������ꋳ���悤�B");

                        UpdateMainMessage("�A�C���F���A���肪�Ƃ������܂��I");

                        UpdateMainMessage("�J�[���F�]�����u�𕜊������Ă������B���X�ɖ߂�Ɨǂ��B");

                        UpdateMainMessage("���i�F���̓x�A���肪�Ƃ��������܂����B�܂��A���������������B");

                        sc.PsychicTrance = true;
                        ShowActiveSkillSpell(sc, Database.PSYCHIC_TRANCE);

                        if (!we.AlreadyRest)
                        {
                            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_EVENING);
                        }
                        else
                        {
                            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                        }
                        this.buttonHanna.Visible = true;
                        this.buttonDungeon.Visible = true;
                        this.buttonRana.Visible = true;
                        this.buttonGanz.Visible = true;
                        this.buttonPotion.Visible = true;
                        this.buttonDuel.Visible = true;
                        this.buttonShinikia.Visible = true;

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "�A�C���ƃ��i�͓]�����u�ŊX�֖߂��Ă����B";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("�A�C���F����A�������E�E�E���̑��x�̓X�Q�F�E�E�E");

                        UpdateMainMessage("���i�F�������Ă�̂�A����Ŏ���A���^�Ɍ�����`�ɂ��Ă���Ă��񂶂�Ȃ��B");

                        UpdateMainMessage("�A�C���F�{�P�t���̏ꍇ�����������ǂ��BFiveSeeker���ĉ����i�Ⴂ�̃��x������ȁB");

                        UpdateMainMessage("�A�C���F���̖��ȈЈ����Ƃ������ʂ����A��{�I�ȑS�̔\�͂��ʎ���������B");

                        UpdateMainMessage("�A�C���FFiveSeeker���ő��x�ɓ�������Z�n�͊m�����F���[�E�A�[�e�B�̂͂����B");

                        UpdateMainMessage("���i�F�u�p�߂炦�����̑��݂����v������A���Ԃ񂻂��������ɂȂ��ˁB");

                        UpdateMainMessage("�A�C���F���Ƃ���ƁA�J�[���݂̎蔲������ł����A���̃��x�����Ď��́E�E�E");

                        UpdateMainMessage("�A�C���F�����\�b�`�̕����l����ƁA�������ȁB");

                        UpdateMainMessage("���i�F���B�Ȃ�āA�܂��܂����ꂩ��Ȃ񂾂��A����Ȃɏł鎖�Ȃ����B");

                        UpdateMainMessage("�A�C���F�܂��撣�莟��Ȃ񂾂낤���ǁB");

                        UpdateMainMessage("���i�F�b�t�t�A���������݂������Ă��ꂽ�菇�̒��̈����������ł���H");

                        UpdateMainMessage("�A�C���F���A�����E�E�E�Ȃ�ƂȂ��ȁB");

                        UpdateMainMessage("���i�F�P��Ō��؂�Ă��琦�����A���̊��o��L�΂������Ȃ��񂶂�Ȃ��������");

                        UpdateMainMessage("�A�C���F�������ȁA�T���L���[�B");

                        UpdateMainMessage("���i�F�����K�����ɍs�����̂ɁA���Ŏ����A���^���܂��Ă�̂�B");

                        UpdateMainMessage("�A�C���F�����A�����������Ȉ��������B�ŁA�o�������Ȃ̂��H");

                        UpdateMainMessage("���i�F�����A��x�����Ă���������A���Ԃ�o����悤�ɂȂ��Ă��B�A���K�g��");

                        UpdateMainMessage("�A�C���F�����A�����͗ǂ������I�@��������ނ��A���i�l�I");

                        UpdateMainMessage("���i�F�����A�o�J�A�C�����s���`�Ȏ��ɖ��@�h��𗎂Ƃ��Ă����邩���");

                        UpdateMainMessage("�A�C���F�b�n�b�n�b�n�I�@���̂��炢�ł��傤�Ǘǂ��A���񂾂��I");

                        UpdateMainMessage("���i�F�o�J�̓o�J�ˁE�E�E�n�A�@�@�@�E�E�E");
                    }
                    #endregion
                    #region "�T�C�L�b�N�E�E�F�C�u"
                    else if ((sc.Level >= 33) && (!sc.PsychicWave))
                    {
                        UpdateMainMessage("���i�F�ӂ��E�E�E�����ˁA���������̂́E�E�E");

                        UpdateMainMessage("�A�C���F�您�A���i�B�ǂ�������킵�Ă�݂�������˂����B");

                        UpdateMainMessage("���i�F�ǂ����������������A�̏p�Ƃ��ẴC���[�W�������݂͂ɂ�����B");

                        UpdateMainMessage("���i�F�w�_�x�Ɓw�S��x�̍��킹�Z�Ȃ񂾂��ǁA�w�S��x�͎��ɂƂ��ċt�����A������]�v��ςȂ̂�B");

                        UpdateMainMessage("�A�C���F�ǂ��������e�Ȃ񂾁H");

                        UpdateMainMessage("���i�F�����ƂˁE�E�E�Ȃ�Č����̂�����E�E�E");

                        UpdateMainMessage("���i�F���@�r������̂�B�̏p���g���ĂˁB");

                        UpdateMainMessage("�A�C���F�E�E�E�}�W�ŁH");

                        UpdateMainMessage("���i�F���ʁA�̏p���g�����͂������̃��[�V��������̃N�_�����K�v�ł���H");

                        UpdateMainMessage("���i�F�ł��A���̑̏p�����͓��ʁB���@�r���̃��[�V�����������́B");

                        UpdateMainMessage("���i�F�̏p�ɂ�閂�@�̑n�o�A�_���[�W�������́y�́z�ł͂Ȃ��y�m�z���Ď��ɂȂ��B");

                        UpdateMainMessage("�A�C���F�E�E�E�ւ��E�E�E�Ȃ�قǁE�E�E");

                        UpdateMainMessage("���i�F�b�z���A��������āE�E�E��������E�E�E");

                        UpdateMainMessage("���i�F�s�����A�T�C�L�b�N�E�E�F�C�u�I");

                        UpdateMainMessage("�@�@�@�i�b���@�V���E�D�D�D���I�j");

                        UpdateMainMessage("�A�C���F���ʂ��I�h��I");

                        UpdateMainMessage("�@�@�@�i�������A�A�C�����h��̐���������ɂ��ւ�炸�A�A�C���Ɍ��ɂ��������I�j");

                        UpdateMainMessage("�A�C���F���O�A�I�ɂ��E�E�E�h��s����B�`�N�V���E�B");

                        UpdateMainMessage("���i�F�ӂ��A�ꉞ�o������ˁB�ł��A���ꐦ������̂�B");

                        UpdateMainMessage("�A�C���F�����R���E�E�E���[�h�E�I�u�E�p���[�݂Ă����ȁB");

                        UpdateMainMessage("�A�C���F����̂��傤�ǔ��΂ƌ������E�E�E�o�q�݂����Ȃ��񂾂ȁB");

                        UpdateMainMessage("���i�F�悭���������g�R�ɂ����C�Â���ˁB�Z���X�����͈�l�O���Ď�������B");

                        UpdateMainMessage("�A�C���F�V�˃A�C���l������ȁB�b�n�b�n�b�n�I");

                        UpdateMainMessage("���i�F���Ⴀ�A����B���̃X�L���̓~���[�E�C���[�W�ƃf�t���N�V�����̂ǂ�����ђʂ��邩�����");

                        using (SelectAction sa = new SelectAction())
                        {
                            sa.StartPosition = FormStartPosition.CenterParent;
                            sa.ForceChangeWidth = 300;
                            sa.ElementA = "�~���[�E�C���[�W����I";
                            sa.ElementB = "���R�A�f�t���N�V�������ȁI";
                            sa.ElementC = "���́A�����E�C���[�j�e�B�I";
                            sa.ShowDialog();
                            if (sa.TargetNum == 1)
                            {
                                UpdateMainMessage("�A�C���F���R�A�f�t���N�V�������ȁI");

                                UpdateMainMessage("���i�F�����E�E�E�����ˁE�E�E");

                                UpdateMainMessage("�A�C���F���E�E�E���������A������H���̃e���V�����̉������́B");

                                UpdateMainMessage("���i�F��A���ł��Ȃ�����@�܂��A�o�J�Ȃ�ɍl�������Ď��ˁB");

                                UpdateMainMessage("�A�C���F�������ă��}�𒣂��āA���R���Ă鎖������B");

                                UpdateMainMessage("���i�F����ς�e�L�g�[�������́I�H�@�����ƍl���ē����Ȃ�����ˁI�H");

                                UpdateMainMessage("�A�C���F���A�����������āE�E�E�����炿���ƍl���܂��E�E�E");
                            }
                            else
                            {
                                UpdateMainMessage("���i�F�n�E�Y�E����@����ς�A�o�J�A�C���̓o�J�Ō����");

                                UpdateMainMessage("�A�C���F���ʂʂʁE�E�E");
                            }
                        }

                        sc.PsychicWave = true;
                        ShowActiveSkillSpell(sc, Database.PSYCHIC_WAVE);
                        UpdateMainMessage("", true);
                    }
                    #endregion
                    #region "�V���[�v�E�O���A"
                    else if ((sc.Level >= 34) && (!sc.SharpGlare))
                    {
                        UpdateMainMessage("���i�F�E�E�E��_�����@���E�E�E");

                        UpdateMainMessage("�A�C���F�i�b�g�g�E�E�E���i���B�����u�c�u�c�����Ă�ȁE�E�E�j");

                        UpdateMainMessage("���i�F�~�]�I�`��_�����@���A�G�̉��ɓ��荞�݁A��C�ɓ˂��グ��B");

                        UpdateMainMessage("���i�F�Ώێ҂ֈ��̃_���[�W��^���A���A�����Ԃ̒��ق�t�^����B");

                        UpdateMainMessage("���i�F�Ȃ񂾁A�w�S��x���݂����������Ǝv������A���i�o�J�A�C���ɂ���Ă郄�c����Ȃ���");

                        UpdateMainMessage("�A�C���F�i�b�\�H�b�g�E�E�E���񂾂��̓T���o�E�E�E�j");

                        UpdateMainMessage("���i�F�o�J�A�C���A�n�E�b�E�P�E����");

                        UpdateMainMessage("�A�C���F�b�N�\�A���������I�@�z�[���[�E�u���C�J�[�I");

                        UpdateMainMessage("���i�F�_���[�W���]���Ă��A���ʂ͓K�p�������B�c�O�ˁ�");
                        
                        UpdateMainMessage("���i�F���Ⴀ�s�����A�b�Z�C�I�I");

                        UpdateMainMessage("�@�@�@�w�b�Y�b�h�H�H���I�I�x�i���i�̃V���[�v�E�O���A���A�C�����y��j�@�@");

                        UpdateMainMessage("���i�F���܂������@���Ⴀ�A�����������@�r�����Ă݂ā�");

                        UpdateMainMessage("�A�C���F�����ł��E�E�E���ق��Ă��������E�E�E");

                        sc.SharpGlare = true;
                        ShowActiveSkillSpell(sc, Database.SHARP_GLARE);
                        UpdateMainMessage("", true);
                    }
                    #endregion
                    #region "�X�^���X�E�I�u�E�T�b�h�l�X"
                    else if ((sc.Level >= 35) && (!sc.StanceOfSuddenness))
                    {
                        UpdateMainMessage("���i�F�t�D�E�E�E�����ˁB");

                        UpdateMainMessage("�A�C���F���i�A���܂�L���L���ɋl�߂Ă̗��K�͗ǂ��Ȃ����B");

                        UpdateMainMessage("���i�F�ł��A�܂��P����������ĂȂ�����A�o����܂ł���Ă݂��B");

                        UpdateMainMessage("�A�C���F�ǂ�ȓ��e�Ȃ񂾂�H");

                        UpdateMainMessage("���i�F�w�X�^���X�E�I�u�E�T�b�h�l�X�x�A��������S�t�����̗ނˁB");

                        UpdateMainMessage("���i�F�w�S��x�Ɓw���S�x�̃R���r�l�[�V����������A����̂�z���g�B");

                        UpdateMainMessage("�A�C���F���ʂ́H");

                        UpdateMainMessage("���i�F�Ώۂ̃C���X�^���g�s�����L�����Z�������鎖���\�ɂȂ��B");

                        UpdateMainMessage("���i�F����ɁA�{�C���X�^���g�ɑ΂��āA�ǉ��ŃX�^�b�N�͏悹���Ȃ����ď���B");

                        UpdateMainMessage("�A�C���F�}�W�ŁI�H�@�������̖ⓚ���p�ȃA���`�����́I�H");

                        UpdateMainMessage("���i�F�A���`�������Č�傪�I�J�V�C���E�E�E����͗ǂ��Ƃ���");

                        UpdateMainMessage("���i�F�^�C�~���O�̒͂ݕ�������������Ȃ��̂�ˁE�E�E");

                        UpdateMainMessage("�A�C���F�����ł�͂�A���̋��͂��K�v���ă��P���ȁA�C����B");

                        UpdateMainMessage("���i�F���Ⴀ�A����肢�o���邩����B");

                        UpdateMainMessage("�A�C���F������A�����I�@�C���Ă����I�I");

                        UpdateMainMessage("���i�F�����Ŏ����Ƀt�@�C�A�E�{�[������������ł݂āB");

                        UpdateMainMessage("�A�C���F�b�u�t�D�I");

                        UpdateMainMessage("�A�C���F���i�ɑ΂��Č�������ʖڂȂ̂���I�H");

                        UpdateMainMessage("���i�F���悻��Ȃ́A������͂��Ȃ�����Ȃ��B");

                        UpdateMainMessage("�A�C���F����A���Ⴀ�t���b�V���E�q�[�������O�ɑ΂��ĉr�����悤�A�ǂ����H");

                        UpdateMainMessage("���i�F���Ń��C�t���^���̎��Ƀq�[��������K�v������̂�B�~�߂�K�v����������Ȃ��B");

                        UpdateMainMessage("�A�C���F����E�E�E�Ƃɂ����A�����t�@�C�A�͊��ق��B");

                        UpdateMainMessage("���i�F���`��A�ǂ����悤������E�E�E");

                        UpdateMainMessage("�A�C���F�������A�܂��J�[���݂̏��ɂł��s�����H");

                        UpdateMainMessage("���i�F�ǂ��́H���x���s�����玸��ɓ�����Ȃ�������B");

                        UpdateMainMessage("�A�C���F���v�����āA���������Ċ���Ȑl���B");

                        UpdateMainMessage("���i�F�A���^�̂��������������A���̂����[�����炾����C�����Ă�ˁE�E�E�z���g�B");

                        UpdateMainMessage("�A�C���F�����ƁA���������B���ᑁ���s�������I");

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "�A�C���ƃ��i�͓]�����u�ŃJ�[���݂̌��ւƌ��������B";
                            md.ShowDialog();
                        }

                        this.buttonHanna.Visible = false;
                        this.buttonDungeon.Visible = false;
                        this.buttonRana.Visible = false;
                        this.buttonGanz.Visible = false;
                        this.buttonPotion.Visible = false;
                        this.buttonDuel.Visible = false;
                        this.buttonShinikia.Visible = false;
                        ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_SECRETFIELD_OF_FAZIL);

                        UpdateMainMessage("�A�C���F�����A���������B");

                        UpdateMainMessage("���i�F���ƂƁE�E�E���̓]�����u�A��������Ȃ���ˁB");

                        UpdateMainMessage("�J�[���F���̓]�����u�͖{���A�M���̂��߂ɗp�ӂ��ꂽ���̂ł͂Ȃ��B");

                        UpdateMainMessage("�A�C���F���A�ǂ������Ӗ��ł����H");

                        UpdateMainMessage("�J�[���F�]�����u�͌X�̐����ɏ����������m�ɒ������K�v���B");

                        UpdateMainMessage("�J�[���F�s�K���ł���A�������Ⴂ���m���g���΁A���_�Ɏx����������B");

                        UpdateMainMessage("�A�C���F�b�Q�Q�E�E�E");

                        UpdateMainMessage("�J�[���F�Ă���ȁA�M�������鎖��z�肵�A�䂪���ɒ������Ă������B");

                        UpdateMainMessage("�J�[���F�����̃t�����͂��邾�낤���A�C�ɗ��߂���ł͂Ȃ��B���S���Ďg���Ɨǂ��B");

                        UpdateMainMessage("���i�F�������v���A�T���������āA�Ȍ�ނނ悤�ɒv���܂��B");

                        UpdateMainMessage("�J�[���F�ǂ��B�ȑO�ɂ������������R�̂Őڂ���悤�ɂ���B");

                        UpdateMainMessage("���i�F�E�E�E�n�C�B");

                        UpdateMainMessage("�J�[���F�t�����́w�S��x�Ɓw���S�x�ɂ�镡���X�L���w�X�^���X�E�I�u�E�T�b�h�l�X�x�Ɋւ��Ă���");

                        UpdateMainMessage("���i�F�n�C�B");

                        UpdateMainMessage("�A�C���F������҂��Ă����B������A�J�[���搶�͉��ŗv�������O�ɕ������Ă�񂾂�H");

                        UpdateMainMessage("���i�F�i��������ƁI�@�T���Ă�ˁA�o�J�A�C���I�j");

                        UpdateMainMessage("�A�C���F�i���A��������˂����ʂɁB�C�ɂȂ�񂾂��E�E�E�j");

                        UpdateMainMessage("�J�[���F�ǂ��A�������������悤�B");

                        UpdateMainMessage("�J�[���F��̌��؂�ł́A�M�����i�E�A�~���A�͊��S�t�����܂��͋t�����̈����ɋꂵ��ł���B");

                        UpdateMainMessage("�A�C���F������č����ĕ����������Ď��ł���ˁH");

                        UpdateMainMessage("�J�[���F�M�N�����߂ɘA��ė���������ł���B");

                        UpdateMainMessage("�A�C���F�E�\�A�}�W���E�E�E");

                        UpdateMainMessage("�J�[���F�_���W�J�͂̂��鏗���ɂƂ��āA���̊T�O�͔��ɋ�ɂƂȂ�ł��낤�B");

                        UpdateMainMessage("�A�C���F���i�A���O�m���T�C�L�b�N�E�g�����X�o����悤�ɂȂ��Ă���ȁH");

                        UpdateMainMessage("���i�F��A����͏o����悤�ɂȂ�����ˁ�");

                        UpdateMainMessage("�A�C���F���������i�����A���Ⴀ���̃X�L���̕�����������H");

                        UpdateMainMessage("�J�[���F�T����A�A�C���E�E�H�[�����X�B");

                        UpdateMainMessage("�@�@�y�y�y�@�A�C���͓ˑR�A�w�؂Ɉُ�ȈЈ������������I�@�z�z�z");

                        UpdateMainMessage("�A�C���F�E�E�E�͂��B");

                        UpdateMainMessage("�J�[���F���i�E�A�~���A�͂���Ƃ͐������Ⴄ�B");

                        UpdateMainMessage("�J�[���F�A�C���E�E�H�[�����X�A�M�N�͐�ɋA�邪�悢�B");

                        UpdateMainMessage("�A�C���F����E�E�E�킩��܂����B");

                        UpdateMainMessage("�J�[���F���ɑ΂���Ӗ��ł͂Ȃ��B��̃P�[�X���䂪�R���g���[�����邽�߁A�����v���ȁB");

                        UpdateMainMessage("�A�C���F�����A���������B�{���ɃX�C�}�Z���ł����A����͂���Ŏ��炵�܂��B");

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "�A�C���͓]�����u�ŊX�ւƖ߂��Ă������B";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("�J�[���F�ł́A�M�����i�E�A�~���A�ɖ₨���B");

                        UpdateMainMessage("���i�F�n�C�B");

                        UpdateMainMessage("�J�[���F�����ĈӐ}���āw�S��x��I�������������q�ׂ��܂��B");

                        UpdateMainMessage("�J�[���F�w�S��x�Ɓw���S�x�̑I���́A���́w�Áx�w���x�A�܂��́w�_�x�w���x�Ɣ�ׂĂ��ɂ߂č���B");

                        UpdateMainMessage("���i�F�̂���A�A�C���������̕����D��Ă�����ł��B");

                        UpdateMainMessage("���i�F�ł����́A��������Ȃ��B");
                        
                        UpdateMainMessage("���i�F���A�������Ă�ł��A�����̓A�C���̕����������ƁB");

                        UpdateMainMessage("���i�F�ł��A����ȏ��Œu���Ă��ڂ�ɂ͂Ȃ�Ȃ��B���̕�����Փx�̍����̂�����Ă̂��Ȃ����");

                        UpdateMainMessage("���i�F�łȂ���΁A�A�C�������ɑ΂��āA���ȏ�Ɏ���������Ă��܂��B");

                        UpdateMainMessage("���i�F���ꂪ���Ȃ�ł��B�܂�Ŏ����P�Ȃ鑫�g�݂����Ȋ��������āE�E�E");

                        UpdateMainMessage("�J�[���F�M���̏ꍇ�A�����T�C�L�b�N�E�g�����X�̉r���͋ꂵ���ƌ��󂯂���A�ԈႢ�͂Ȃ��ȁH");

                        UpdateMainMessage("���i�F�n�C�B");

                        UpdateMainMessage("�J�[���F�w�S��x�Ɓw���S�x�ւ̒���́A�M���̐��_����ɂ�����������e�ł���B");

                        UpdateMainMessage("�J�[���F�䂦�ɁA�h���Ɗ������ꍇ�A�K��������߂̐S���������A���Ԃ�u���悤�ɂ��鎖���B");

                        UpdateMainMessage("���i�F�n�C�B");

                        UpdateMainMessage("�J�[���F�M���ɕs�����Ă���̂́A�����ȋx���̎��ԁB");

                        UpdateMainMessage("�J�[���F���܂ł̒b�B�A�\���������܎^�������e�ł͂���B");

                        UpdateMainMessage("�J�[���F�������A���ꂾ���ł͋M���̐g���댯�ł��邱�Ƃɂ��ς��͂Ȃ��B");

                        UpdateMainMessage("�J�[���F�m���Ƃ͑����ɏK���������̂ł͂Ȃ��B");

                        UpdateMainMessage("�J�[���F�ꍏ�̏W���I�ȓ�������A��΂����̗���s�����Ԃ��K�v�ƂȂ�B");

                        UpdateMainMessage("���i�F�����E�E�E�ꂵ���Ȃ��Ă��܂��āE�E�E");

                        UpdateMainMessage("���i�F���E�E�E�A�C���ɒǂ��������ɂȂ��������E�E�E");

                        UpdateMainMessage("���i�F���A���݂܂���A��������ȏ��Ŏ��E�E�E");

                        UpdateMainMessage("�@�@�i�@���i�͂��̏�Ŋ�𕚂��A�킸���Ȑ��H��n�ʂɗ��Ƃ��� �j");

                        UpdateMainMessage("�J�[���F���܂ł̖����Ȓb�B�Ƃ��̐��ʁA�����̋�J���d�˂ė������ł��낤�B");

                        UpdateMainMessage("�J�[���F�����A�M�������ƌ����������x���̒b�B�͏��X��������B");

                        UpdateMainMessage("�J�[���F���X�̃X�^�C����������Ɩ����A�b�B���邪�ǂ��B");

                        UpdateMainMessage("���i�F���݂܂���A����ȏ��ŋ����Ă��܂��āE�E�E");

                        UpdateMainMessage("�J�[���F�悢�B�C�ɕa�ޕK�v�͂Ȃ��B");

                        UpdateMainMessage("�J�[���F�A�C���E�E�H�[�����X�͈�ށB�߂��ł��̐��������͂��邮�炢�̐S�\���Ƃ���B");

                        UpdateMainMessage("�J�[���F�����⌨���ׂ̂���ŗ�܂ʂ悤�ɁB");

                        UpdateMainMessage("���i�F�n�C�A�킩��܂����B");

                        UpdateMainMessage("�J�[���F�ł́A�w�S��x�Ɓw���S�x�ɂ��w�X�^���X�E�I�u�E�T�b�h�l�X�x�A�����Đi���悤�B");

                        UpdateMainMessage("���i�F�n�C�A���肢���܂��B");

                        UpdateMainMessage("�@�@�@�w���i�͏W�����ču�`�̓��e�𕷂����I�x");

                        UpdateMainMessage("�J�[���F����ōu�`�͏I���ł���B");

                        UpdateMainMessage("���i�F���肪�Ƃ��������܂����B");

                        UpdateMainMessage("�J�[���F�M�����悭�������܂ŒH�蒅�����B���̗ދH�Ȃ�w�͂��܎^�ƌh�ӂ�]���A");

                        UpdateMainMessage("�J�[���F��x�w�X�^���X�E�I�u�E�T�b�h�l�X�x�̋ɈӁA������Ƃ��悤�B");

                        UpdateMainMessage("���i�F�����A�{���ł����I�H");

                        UpdateMainMessage("�J�[���F�\�����܂��B");

                        UpdateMainMessage("���i�F�A�b�A�n�C�I�ł͂��肢�������܂��I");

                        UpdateMainMessage("�J�[���F�䂭���B");

                        UpdateMainMessage("���i�F���ĕX�̒e�ہA�u���[�E�o���b�g�I");

                        if (!we.AlreadyRest)
                        {
                            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_EVENING);
                        }
                        else
                        {
                            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                        }
                        this.buttonHanna.Visible = true;
                        this.buttonDungeon.Visible = true;
                        this.buttonRana.Visible = true;
                        this.buttonGanz.Visible = true;
                        this.buttonPotion.Visible = true;
                        this.buttonDuel.Visible = true;
                        this.buttonShinikia.Visible = true;

                        UpdateMainMessage("�A�C���F�E�E�E�������Ȃ��E�E�E");

                        UpdateMainMessage("�@�@�@�i�b�o�V���I�j");

                        UpdateMainMessage("���i�F�ӂ��A�������܁B");

                        UpdateMainMessage("�A�C���F�������A����ƋA���Ă������I�@�ǂ���������H");

                        UpdateMainMessage("���i�F�b�t�t�t�A��񂾂��A���Ƀt�@�C�A�E�{�[�����͂Ȃ��Ă��ǂ�����");

                        UpdateMainMessage("�A�C���F����Ϗo�����̂���I�@�����������A���i�A�b�n�b�n�b�n�I�I");

                        UpdateMainMessage("�A�C���F���₢��A�{�����i�͍ō������I���������t���Ă����˂����z���g�I");

                        UpdateMainMessage("���i�F�������������������ăz���g���ꂵ����ˁE�E�E�ǂ�����A���������Ă݂Ȃ�����B");

                        UpdateMainMessage("�A�C���F�����A�������ȁE�E�E���Ⴀ�s�����I");

                        UpdateMainMessage("�A�C���F�t�@�C�A�I");

                        UpdateMainMessage("���i�F���؂�����A�\���ˁI");

                        UpdateMainMessage("�A�C���F�I�I�H");

                        UpdateMainMessage("�@�@�@�i�b�V���b�p�@�@�@���E�E�E�i�t�@�C�A�E�{�[�����u���ɋ󒆂ł͂�����񂾁j");

                        UpdateMainMessage("�A�C���F���E�E�E�X�Q�F�X�Q�F�I�@�b�n�b�n�b�n�b�n�I�I");

                        UpdateMainMessage("�A�C���F�������������A������R���I�@�}�W���惉�i�I�I");

                        UpdateMainMessage("���i�F����R�X�g�͌��\�����݂��������瑽�p�͂ł��Ȃ����ǂˁ�");

                        UpdateMainMessage("�A�C���F���₢�₻��ł����̖ⓚ���p�̈ꔭ�L�����Z���͂������I");

                        UpdateMainMessage("�A�C���F���i�A���҂��Ă邺�B");

                        UpdateMainMessage("�A�C���F���O�ɏo��ĉ��͍ō��ɍK�����I�@�b�n�b�n�b�n�I");

                        UpdateMainMessage("�@�@�@�w�b�V���S�I�H�H�I�H�H�I�I�I�x�i���i�̃f�o�X�e�C�g�u���[���A�C�����y�􂵂��j�@�@");

                        UpdateMainMessage("���i�F���t�I�тȂ����E�E�E�o�J�A�C��");

                        UpdateMainMessage("�A�C���F���E�E�E���������J�߂��̂ɁE�E�E");
                        
                        UpdateMainMessage("�A�C���F�i�E�E�E�o�^�j");

                        sc.StanceOfSuddenness = true;
                        ShowActiveSkillSpell(sc, Database.STANCE_OF_SUDDENNESS);
                        UpdateMainMessage("", true);
                    }
                    #endregion
                    else if (!we.AlreadyRest)
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
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
            #region "�I���E�����f�B�X�����O��"
            else if (we.AvailableDuelMatch && !we.MeetOlLandis)
            {
                if (!we.MeetOlLandisBeforeLana)
                {
                    UpdateMainMessage("���i�F�A�C���A�ǂ������́H�����炪�Ⴆ�Ȃ��݂��������ǁB");

                    UpdateMainMessage("�A�C���F�E�E�E�{�P�t���̃����f�B�X�����Z��ɗ��Ă�݂������B");

                    UpdateMainMessage("���i�F�����A�����Ȃ́I�H�@�ǂ���������Ȃ���@�����A��ɍs���Ă������");

                    UpdateMainMessage("�A�C���F�o�������A�������񂶂܂�����H");

                    UpdateMainMessage("���i�F�Ȃɂ���ȃr�r���Ă�̂�B��������A������Ɋ��킹���ė��悤���ȁ�");

                    UpdateMainMessage("�A�C���F�҂āI�@�҂đ҂āI�I�@���ꂾ���͑ʖڂ��I�I�I");

                    UpdateMainMessage("���i�F���ł�H");

                    UpdateMainMessage("�A�C���F�����������ꍇ�A������Ɋ��킹�ɍs���Ȃ���Ȃ�˂��񂾁B");

                    UpdateMainMessage("�A�C���F����˂��ƁA�}�W�ŎE���ꂿ�܂��B�@������ɉ���Ă���B");

                    UpdateMainMessage("���i�F�����Ȃ́B�܂��ǂ���A���Ⴀ�����Ƃ�����ė���΂�������Ȃ���");

                    UpdateMainMessage("�A�C���F�N�b�\�A���ł��O����Ȋy�������Ȃ񂾂�B�B�B�܂��������B");

                    UpdateMainMessage("�A�C���F��������A����؂肪�t�������B�����s���Ă��邺�B");

                    UpdateMainMessage("���i�F���A�A�C���̂��߂ɂ���Ȃ��̗p�ӂ��Ă��������B�n�C�A�|�[�V������");

                    CallSomeMessageWithAnimation("�A�C����" + Database.COMMON_REVIVE_POTION_MINI + "����ɓ��ꂽ�B");

                    UpdateMainMessage("�A�C���F�v�邩���������A����Ȃ̂����������I�I�I");

                    UpdateMainMessage("���i�F�t�t�A���Ⴀ�s���Ă�����Ⴂ��", true);

                    GetItemFullCheck(mc, Database.COMMON_REVIVE_POTION_MINI);

                    we.MeetOlLandisBeforeLana = true;
                }
                else
                {
                    UpdateMainMessage("���i�F�s���Ă�����Ⴂ��", true);
                }
                return;
            }
            #endregion
            #region "�k���S�ȍ~�A�X�^���X�̏K����b"
            else if (this.firstDay >= 5 && mc.Level >= 4 && we.Truth_CommunicationLana1_1 == false && we.AvailableSecondCharacter)
            {
                if (!we.AlreadyCommunicate)
                {
                    UpdateMainMessage("���i�F�o�J�A�C���A������Ƃ����H");

                    UpdateMainMessage("�A�C���F���̓o�J����˂����āB�����H");

                    UpdateMainMessage("���i�F�t���b�V���E�q�[���B�o�������ˁB");

                    UpdateMainMessage("�A�C���F��H�����A�������ȁB�Ȃ�ƂȂ��v���o�������B");

                    UpdateMainMessage("�A�C���F�q�[�� �� �A�^�b�N�I ��{������ȁI");

                    UpdateMainMessage("���i�F�̂����炻����ˁE�E�E���̒P���ȃm���E�E�E");

                    UpdateMainMessage("�A�C���F�ʂɂ�������˂����B�_���[�W���[�X�̊�{����B");

                    UpdateMainMessage("���i�F���̃X�^���X�A�ς��悤�Ǝv�������͈�x���Ȃ��킯�H");

                    using (TruthDecision td = new TruthDecision())
                    {
                        td.MainMessage = "�@�y�@�X�^���X��ς��悤�Ǝv���܂����H�@�z";
                        td.FirstMessage = "�v��Ȃ��ȁB���܂łǂ���s�����B";
                        td.SecondMessage = "�����A��x���炢�͍l�������Ƃ�����B";
                        td.StartPosition = FormStartPosition.CenterParent;
                        td.ShowDialog();
                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                        {
                            UpdateMainMessage("�A�C���F�v��Ȃ��ȁB���܂łǂ���s�����B");

                            UpdateMainMessage("���i�F�����B�܂��A���X�ς���Ƃ͎v���ĂȂ��������ǁB");

                            UpdateMainMessage("�A�C���F����σ_���[�W���K�c�K�c����ɗ^���Ȃ��ƂȁI�b�n�b�n�b�n�I");

                            UpdateMainMessage("���i�F���Ⴀ�A�A�C���͑O�q�U���^���Ď��ɂȂ��ˁB");

                            UpdateMainMessage("�A�C���F�O�q�U���^�H");

                            UpdateMainMessage("���i�F�O�ɏo�āA�_���[�W���K�c�K�c�����̎���B");

                            UpdateMainMessage("�A�C���F���A�����B�������������Ӗ�����B");

                            UpdateMainMessage("���i�F���Ⴀ�A���܂łǂ���A��������O�ɏo�āA�U����낵����");

                            UpdateMainMessage("�A�C���F�����A�C���Ă����I�b�n�b�n�b�n�I");

                            mc.Stance = PlayerStance.FrontOffence;

                            CallSomeMessageWithAnimation("�y�A�C���͑O�q�U���^�ɂȂ�܂����I�z");

                            CallSomeMessageWithAnimation("�y�����U���T���t�o�A���@�U���T���t�o�z");
                        }
                        else
                        {
                            UpdateMainMessage("�A�C���F�����A��x���炢�͍l�������Ƃ�����B");

                            UpdateMainMessage("���i�F�����B�܂��A���X�ς���Ƃ͎v���ĂȂ��������ǁB");

                            UpdateMainMessage("�A�C���F�E�E�E�����A�l�������͂���ƌ����Ă邾��B");

                            UpdateMainMessage("���i�F�E�E�E���������������������I�I�H�H");

                            UpdateMainMessage("�A�C���F�����I�r�b�N��������Ȃ��āB���O�̋������̓I�[�o�[�����邼�B");

                            UpdateMainMessage("���i�F�����Ɨ������Č����Ă�킯�H");

                            UpdateMainMessage("�A�C���F����̕����_���[�W�������ꍇ�͎��̂��厖�����ȁB");

                            UpdateMainMessage("�A�C���F������̑̐�������Ȃ��悤�ɂ��āA�������U����D�������B");

                            UpdateMainMessage("�A�C���F�f�B�t�F���X �� �A�^�b�N���ď����ȁB");

                            UpdateMainMessage("���i�F���E�E�E�m���ɂ���ȏ��ˁE�E�E������ƈႤ���ǁB");

                            UpdateMainMessage("���i�F�A�C�����炻��Ȍ��t���o��Ƃ͈ӊO����B");

                            UpdateMainMessage("�A�C���F���i�ƃe���|�悭�U������̂��ǂ����ǂȁB");

                            UpdateMainMessage("�A�C���F�ق�A���܂Ƀs���`�Ȏ����邾��B����Ă��ǂ��������Ďv�������B");

                            UpdateMainMessage("���i�F���A�����A�܂������ˁB�B�B");

                            UpdateMainMessage("���i�F���Ⴀ�A�O�q�h�q�^���Ď��ɂȂ��ˁB");

                            UpdateMainMessage("�A�C���F�O�q�h�q�^�H���������B");

                            UpdateMainMessage("���i�F�O�ł�������Ƒ���̃_���[�W���󂯎~�߂���̎���B");

                            UpdateMainMessage("���i�F����ȊO�ɂ��A�^�C�~���O�悭������X�^���������肵�Ă�ˁB");

                            UpdateMainMessage("���i�F���ꂩ��G�����X�^�[�̒��ӂ������ƈ����Ă�ˁB");

                            UpdateMainMessage("���i�F����܂�h���ӓ|�ɂȂ�Ȃ��ł�ˁB�^�C�~���O�悭�U���������ĂˁB");

                            UpdateMainMessage("���i�F�ǂ����Ă����Ď��̓��C�t�񕜂������Ƃ���Ă�ˁB");

                            UpdateMainMessage("�A�C���F�E�E�E�ʓ|�����������I�I�I");

                            mc.Stance = PlayerStance.FrontDefense;

                            CallSomeMessageWithAnimation("�y�A�C���͑O�q�h�q�^�ɂȂ�܂����I�z");

                            CallSomeMessageWithAnimation("�y�����h��T���t�o�A���@�h��T���t�o�z");

                        }
                    }

                    we.AlreadyCommunicate = true;
                }
                else
                {
                    mainMessage.Text = MessageFormatForLana(1001);
                }

                we.Truth_CommunicationLana1_1 = true;
            }
            #endregion
            #region "�P�l�ŗ��݂��t�����V�X�ɑ����ς݂̏ꍇ"
            else if (this.firstDay >= 1 &&
                !we.Truth_CommunicationLana1_2 &&
                Truth_KnownTileInfo[252] == true &&
                !we.TruthCompleteSlayBoss1
                )
            {
                if (!we.AlreadyCommunicate)
                {
                    UpdateMainMessage("�A�C���F�������E�E�E");

                    UpdateMainMessage("���i�F�ǂ������̂�H");

                    UpdateMainMessage("�A�C���F�����A�P�K�̃{�X�Ȃ񂾂��ȁB");

                    UpdateMainMessage("�A�C���F���ꂪ�����������Ă��ȁA�S�R���������˂��B");

                    UpdateMainMessage("���i�F�����ɓ˂����݂����Ȃ񂶂�Ȃ��́H");

                    if (mc.Level < 15)
                    {
                        UpdateMainMessage("���i�F���āA�o�J�A�C���E�E�E���x��" + mc.Level.ToString() + "����Ȃ��̂�I�H");

                        UpdateMainMessage("���i�F����Ȃ�ŏ��Ă�킯�Ȃ��ł���E�E�E�͂������������E�E�E");

                        UpdateMainMessage("�A�C���F����A�_���Ă�����킯����˂��B");

                        UpdateMainMessage("�A�C���F�P�ɓ˂�����ł���A�H��t�����܂�������ȁI�b�n�b�n�b�n�I");

                        UpdateMainMessage("���i�F������o�J���Č����̂�����E�E�E�͂��������������E�E�E");
                    }
                    else
                    {
                        UpdateMainMessage("���i�F���x���́E�E�E" + mc.Level.ToString() + "�ˁB�܂�����Ȃ�ɂ���Ă�݂��������ǁB");
                    }

                    UpdateMainMessage("���i�F�܂��ǂ���B������ƁA�悭�����Ȃ�����ˁB");

                    UpdateMainMessage("���i�F���A�����ƃ��x�����グ�鎖�B�ǂ���ˁH");

                    if (mc.Level < 15)
                    {
                    }
                    else
                    {
                        UpdateMainMessage("���i�FLV" + mc.Level.ToString() + "�����A����Ȃ�ɂ���Ă�݂��������ǁB");

                        UpdateMainMessage("���i�F�p�����^�̊���U��͐T�d�ɂˁB�����ȏグ�����Ă�ƌ�X�ꂵ���Ȃ���B");
                    }

                    UpdateMainMessage("���i�F���A�����l�����ɃY���Y�J�i�܂Ȃ����ƁB");

                    if (we.dungeonEvent21KeyOpen || we.dungeonEvent22KeyOpen)
                    {
                        UpdateMainMessage("���i�F�x���������悤�ȊŔƂ��A�r���ɉ������������킯�H");

                        UpdateMainMessage("�A�C���F����A���������ǂȁB�ł�����Ȃ�Ƀ_���W�����T�����Ȃ���i�߂Ă������肾�B");

                        UpdateMainMessage("���i�F�Ӂ[��A�Ȃ炢���񂾂��ǁE�E�E");
                    }
                    else
                    {
                        UpdateMainMessage("���i�F�x���������悤�ȊŔƂ��A�r���ɉ������������킯�H");

                        UpdateMainMessage("�A�C���F�b�Q�E�E�E");

                        UpdateMainMessage("���i�F����ς肠�����񂾁B�����͂����������ɂ����ӂ��Ă�ˁA�z���g�B");
                    }

                    UpdateMainMessage("���i�F��O�A���ꂪ���\�厖�Ȃ񂾂��ǁA�ǂ������𑵂���悤�ɂ��邱�ƁB");
                    if (mc.MainWeapon != null)
                    {
                        if (mc.MainWeapon.Rare == ItemBackPack.RareLevel.Rare || mc.MainWeapon.Rare == ItemBackPack.RareLevel.Epic)
                        {
                            UpdateMainMessage("�A�C���F���������Ă�̂́y" + mc.MainWeapon.Name + "�z�ŁA����Ȃ�ɗǂ����������B");

                            UpdateMainMessage("���i�F�ւ��A���\�����������Ă邶��Ȃ��􂶂Ⴀ������������ˁB");
                        }
                        else if (mc.MainWeapon.Rare == ItemBackPack.RareLevel.Common)
                        {
                            UpdateMainMessage("���i�F�A�C�����������Ă�̂��āy" + mc.MainWeapon.Name + "�z��ˁB");

                            UpdateMainMessage("���i�F���ʂɎg���鑕���݂��������ǁA�����������������T���Ƃ������B");
                        }
                        else
                        {
                            UpdateMainMessage("���i�F��������ƁE�E�E�y" + mc.MainWeapon.Name + "�z���Ďg���Ȃ��������Ă��ˁB");

                            if (mc.MainWeapon.Name == Database.POOR_PRACTICE_SWORD)
                            {
                                UpdateMainMessage("���i�F�܂����̑��������Ȃ̂��A�������Ă邩�͂ǂ����͕ʂƂ��Ă��E�E�E");
                            }
                            UpdateMainMessage("���i�F�_���W���������ƒT�����Ă�΁A���������}�V�ȕ��킪������͂���B");
                        }
                    }

                    UpdateMainMessage("���i�F�Ƃɂ����A���x���A�b�v�B�_���W�����T���B�����̏[����B");

                    UpdateMainMessage("���i�F���ꂳ�����΁A����Ȃ�ɉ�����悤�ɂ͂Ȃ��Ă邩��A�撣���Ă�ˁB�z���g�B");

                    UpdateMainMessage("�A�C���F�E�E�E�n�C�B");

                    we.AlreadyCommunicate = true;
                }
                else
                {
                    mainMessage.Text = MessageFormatForLana(1001);
                }
                we.Truth_CommunicationLana1_2 = true;
            }
            #endregion
            #region "�Q�K�J�n��"
            else if (we.TruthCompleteArea1 && !we.Truth_CommunicationLana21)
            {
                UpdateMainMessage("�A�C���F�����A����ȏ��ɋ����̂��B���i�B");

                UpdateMainMessage("���i�F�o�J�A�C���A���傤�Ǘǂ��������");

                UpdateMainMessage("�A�C���F�b�Q�A���̂����炳�܂Ȉ��A�B�B�B");

                UpdateMainMessage("���i�F���͂ˁA�Ƃ��Ă��C�C�����v�������́B������ƃR�b�`�R�b�`��");

                UpdateMainMessage("�A�C���F���A�����A�����������āB�ǂ��s���񂾂�H");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "����������i�X�̑O�ɂāE�E�E";
                    md.ShowDialog();
                }

                UpdateMainMessage("�A�C���F�����A��i�X�̑O����Ȃ����B");

                UpdateMainMessage("���i�F�A�C���A������ƃR������ł݂Ă��");

                UpdateMainMessage("�A�C���F���F�̃|�[�V�������ȁB��̉����N����񂾁H");

                UpdateMainMessage("���i�F������ƃR������ł݂Ă��");

                UpdateMainMessage("�A�C���F�i���^�Ȃ̂��H��퓬���ł��g����̂��H");

                UpdateMainMessage("���i�F����ł݂Ă��");

                UpdateMainMessage("�A�C���F�b�O�E�E�E�ŏ����猙�ȗ\���͂��Ă����E�E�E");

                UpdateMainMessage("�A�C���F�v���򂶂�˂���ȁH");

                UpdateMainMessage("���i�F����ȃ��P�Ȃ��ł���B�b�z���A�Ƃ��Ƃƈ��߁�");

                UpdateMainMessage("�A�C���F������A�s�����I�I");

                UpdateMainMessage("�@�@�@�w�b�S�N���E�E�E�x");

                UpdateMainMessage("�A�C���F���ɕω��͊������Ȃ��悤�����E�E�E");

                UpdateMainMessage("�A�C���F�����I�I�I");

                UpdateMainMessage("���i�F�ǂ��H�����Ă����H");

                UpdateMainMessage("�A�C���F�������������̂��������܂������������邺�B");

                UpdateMainMessage("�A�C���F��̂ǂ�Ȍ��ʂ�_�����񂾂�H");

                UpdateMainMessage("���i�F�g�̂̑ϐ��\�͂𑝋���������B���̂͑ω�Ō��ʂȂ񂾂���");

                UpdateMainMessage("���i�F�ʏ�̉�ō�p�͂��������ʂ͂���񂾂���");

                UpdateMainMessage("���i�F����ɉ����āA�퓬���ł���΁A���̌ジ���Ɠőϐ����t�����Ă킯��");

                UpdateMainMessage("�A�C���F�ւ��I�H����������˂����A����I�I");

                UpdateMainMessage("���i�F�܂��A���̂Ƃ����ō�p�̃J�e�S���[�������Ȃ��񂾂��ǂˁB");

                UpdateMainMessage("�A�C���F���₢��A���ꂾ���ł��債�����񂾂��I");

                UpdateMainMessage("�A�C���F���i�A���O�͂���ς��������I�b�n�b�n�b�n�I�I");

                UpdateMainMessage("���i�F����A���肪�Ɓ�");

                UpdateMainMessage("���i�F���낢�냌�p�[�g���[���₵�Ă�����ˁA���w�����肢���܂���");

                UpdateMainMessage("�A�C���F�����A���񂾂��I�܂������ɗ��邩��ȁI");

                CallSomeMessageWithAnimation("�A�C����" + Database.COMMON_RESIST_POISON + "����ɓ��ꂽ�B");
                GetItemFullCheck(mc, Database.COMMON_RESIST_POISON);

                we.Truth_CommunicationLana21 = true;

            }
            #endregion

            #region "�P����"
            else if (this.firstDay >= 1 && !we.Truth_CommunicationLana1)
            {
                // if (!we.AlreadyRest) // 1���ڂ̓A�C�����N�����΂���Ȃ̂ŁA�{�t���O�𖢎g�p�Ƃ��܂��B
                if (!we.AlreadyCommunicate)
                {
                    UpdateMainMessage("���i�F������A�ӊO�Ƒ�������Ȃ��B");

                    UpdateMainMessage("�A�C���F�����A�������Q�o�߂��ǂ��񂾁B���������q�S�������I");

                    UpdateMainMessage("���i�F�o�J�Ȏ������ĂȂ��ŁA�z���z���A�����͂�ł��H�ׂ܂���B");

                    UpdateMainMessage("�A�C���F�����A�������ȁI���Ⴀ�A�n���i�f�ꂳ��Ƃ��ŐH�ׂ悤���B");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "�n���i�̏h���i�������j�ɂāE�E�E";
                        md.ShowDialog();
                    }

                    UpdateMainMessage("�A�C���F�����������A�f�ꂳ��I�����̔т��������|����ȁI");

                    UpdateMainMessage("�n���i�F�A�b�n�b�n�A�悭���C�ɐH�ׂ�ˁB�܂���R���邩��ˁA�ǂ�ǂ�H�ׂȁB");

                    UpdateMainMessage("���i�F�A�C���A�����͍T���Ȃ�����ˁB�p��������������B");

                    UpdateMainMessage("�A�C���F�����A�T���邺�B������ȁI�b�n�b�n�b�n�I�I�I");

                    UpdateMainMessage("�@�@�@�w�b�h�X�I�x�i���i�̃T�C�����g�u���[���A�C���̉������y��j�@�@");

                    UpdateMainMessage("�A�C���F�����������E�E�E������H���Ă鎞�ɂ�������Ȃ��āE�E�E");

                    UpdateMainMessage("�A�C���F�E�E�E�b���O�E�E�E������������I���ł��A���i�B");

                    UpdateMainMessage("���i�F���H");

                    UpdateMainMessage("�A�C���F�I���̓_���W�����֌��������B");

                    UpdateMainMessage("�A�C���F�����āA���̍ŉ��w�փI���͒H��t���Ă݂���I");

                    UpdateMainMessage("���i�F������A���悢���Ȃ蓂�˂ɁB");

                    UpdateMainMessage("���i�F�S�R��������������Ȃ��B����A�{���ɂ���ȃg�R�s�������킯�H");

                    if (GroundOne.WE2.TruthBadEnd1)
                    {
                        UpdateMainMessage("�A�C���F�܂��{���ɍs�������Ƃ������Ă��Ȃ��E�E�E");

                        UpdateMainMessage("�A�C���F�����҂��Ŏ��x�𐬂藧��������Ă̂����R�Ȃ񂾂��A");

                        UpdateMainMessage("�A�C���F�`����FiveSeeker�ɒǂ��������C���������邪�E�E�E");

                        UpdateMainMessage("�A�C���F����͕ʂƂ��āA�Ƃɂ����s���Ȃ�����Ȃ�˂��B����ȋC������񂾁B");

                        UpdateMainMessage("���i�F�Ӂ[��A�����B���ȓ����ˁB");

                        UpdateMainMessage("���i�F�܂��A�����������B�����Ⴀ�A�͂��R����");
                    }
                    else
                    {
                        UpdateMainMessage("�A�C���F�������Ă�񂾁A���i�B�������̉҂������Ȃ̂��Y�ꂽ�̂��H");

                        UpdateMainMessage("�A�C���F���B�̎��x�̓_���W�����Ő��藧���Ă邾��B�����҂��Ȃ��ƂȁB");

                        UpdateMainMessage("���i�F����A�܂�����͕������Ă�����B�ł����ōŉ��w�ɍs��������́H");

                        UpdateMainMessage("�A�C���F���ł����āH����Ⴀ���܂��Ă邾��I");

                        UpdateMainMessage("�A�C���F�`����FiveSeeker�l�B�ɒǂ������߂��I�I");

                        UpdateMainMessage("���i�F�A�C�����Đ̂�����FiveSeeker�l�̎��A��D����ˁB�͂��Ⴂ������āA�b�t�t�t�B");

                        UpdateMainMessage("�A�C���F�������������HFiveSeeker�͂��ׂĂ̖`���҂ɂƂ��Ă̓���̓I���낤�H�ڕW�ɂ��ē��R����B");

                        UpdateMainMessage("���i�F�����������B�����Ⴀ�A�͂��R����");
                    }


                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.Message = "�y�����̐����z����ɓ���܂����B";
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.ShowDialog();
                    }

                    GetItemFullCheck(mc, Database.RARE_TOOMI_BLUE_SUISYOU);

                    UpdateMainMessage("�A�C���F���A�y�����̐����z����˂����B�����邺�I");

                    UpdateMainMessage("���i�F�������Ȃ��ł�H���ꌋ�\���A���Œl�i������̂Ȃ񂾂���B");

                    UpdateMainMessage("�A�C���F��H�����A�C���Ă������āI�b�n�b�n�b�n�I�I");

                    UpdateMainMessage("�A�C���F���ƁA�������B�Y��Ȃ������ɁE�E�E");

                    UpdateMainMessage("�A�C���F�E�E�E�i���������j�E�E�E");

                    UpdateMainMessage("���i�F���T���Ă�̂�H");

                    UpdateMainMessage("�A�C���F�m���|�P�b�g�ɓ��ꂽ�͂��E�E�E");

                    using (TruthDecision td = new TruthDecision())
                    {
                        td.MainMessage = "�@�y�@���i�ɃC�������O��n���܂����H�@�z";
                        td.FirstMessage = "���i�ɃC�������O��n���B";
                        td.SecondMessage = "���i�ɃC�������O��n�����A�|�P�b�g�ɂ��܂��Ă����B";
                        td.StartPosition = FormStartPosition.CenterParent;
                        td.ShowDialog();
                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                        {
                            UpdateMainMessage("�A�C���F�������������B���i�A������n���Ă������B");

                            UpdateMainMessage("���i�F����A���̃C�������O����Ȃ��B�ǂ��ŏE�����̂�H");

                            UpdateMainMessage("�A�C���F�ǂ����āA���̕����ɗ����Ă����B���i�����Ƃ��Ă������񂾂�H");

                            UpdateMainMessage("���i�F�E�E�E�������I�H�������A����ȃ��P��������Ȃ��I�I");

                            UpdateMainMessage("�A�C���F�Ȃ�ł���ȍQ�ĂĂ񂾂�B�܂��Ԃ��Ă������B�b�z���I");

                            UpdateMainMessage("���i�F���ƂƁA�E�E�E�A���K�g��");

                            UpdateMainMessage("�A�C���F���O�͕ςȏ��Ŕ����Ă邩��ȁA�������莝���Ă��ȁB");

                            UpdateMainMessage("�A�C���F����A�s���Ă��邩�ȁI�����A�_���W�����I�b�n�b�n�b�n�I");

                            mc.DeleteBackPack(new ItemBackPack("���i�̃C�������O"));
                            we.Truth_GiveLanaEarring = true;
                        }
                        else
                        {
                            if (GroundOne.WE2.TruthBadEnd1)
                            {
                                UpdateMainMessage("�A�C���F�i�E�E�E���̃C�������O�E�E�E�j");

                                UpdateMainMessage("�A�C���F�i����������Ă�ƁA�����v���o�������Ȃ񂾂��E�E�E�j");

                                UpdateMainMessage("�A�C���F�i���i�ɂ͈������A�������������Ă������E�E�E�j");

                                UpdateMainMessage("�A�C���F����A���ł��˂��񂾁B");

                                UpdateMainMessage("���i�F���A�|�P�b�g���S�\�S�\���Ă�����Ȃ��́H");

                                UpdateMainMessage("�A�C���F���A���₢��B���ł��˂��A�b�n�b�n�b�n�I");

                                UpdateMainMessage("���i�F����A�����炳�܂ɉ������������H���̂́E�E�E");

                                UpdateMainMessage("�A�C���F�����A�_���W�����I�b�n�b�n�b�n�I");
                            }
                            else
                            {
                                UpdateMainMessage("�A�C���F�����������ȁE�E�E�m���Ƀ|�P�b�g�ɓ��ꂽ�͂������E�E�E");

                                UpdateMainMessage("���i�F�����T�����ł����Ă�́H");

                                UpdateMainMessage("�A�C���F���A���₢��B���ł��˂��A�b�n�b�n�b�n�I");

                                UpdateMainMessage("���i�F����A��������ˁE�E�E");

                                UpdateMainMessage("�A�C���F����A�s���Ă��邩�ȁI�����A�_���W�����I�b�n�b�n�b�n�I");
                            }
                        }
                    }
                    we.AlreadyCommunicate = true;
                }
                else
                {
                    UpdateMainMessage(MessageFormatForLana(1002));
                }
                we.Truth_CommunicationLana1 = true; // �������ڂ̂݁A���i�A�K���c�A�n���i�̉�b�𕷂������ǂ������肷�邽�߁A������TRUE�Ƃ��܂��B
            }
            #endregion
            #region "�Q����"
            else if (this.firstDay >= 2 && !we.Truth_CommunicationLana2)
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        UpdateMainMessage("�A�C���F�您���i�I�_���W��������߂��Ă��ċC�Â����񂾂��B");

                        UpdateMainMessage("���i�F�ԃ|�[�V�����ł��~�����킯�H");

                        UpdateMainMessage("�A�C���F���ȁI�H�@���ŕ��������I�I�H");

                        UpdateMainMessage("���i�F��̃|�[�V�����ЂƂ��������Ƀ_���W�����ɓ˂����ރ��c�Ȃ�Ăǂ��ɋ���̂�B");

                        UpdateMainMessage("�A�C���F�����E�E�E�����������āA�Ȃ��R�R�͂ЂƂ��ނ��I");

                        UpdateMainMessage("���i�F���傤���Ȃ���ˁA���񂾂���B�b�z����");
                        GetPotionForLana();

                        UpdateMainMessage("�A�C���F������I�T���L���[�T���L���[�I");

                        UpdateMainMessage("���i�F�����A��������Ƃ�����ƁB");

                        UpdateMainMessage("�A�C���F��H������H");

                        UpdateMainMessage("���i�F�b�t�t�A����ς�閧���ȁ�");

                        UpdateMainMessage("�A�C���F�����A�҂đ҂āB�������C�ɂȂ邶��˂����H������H");

                        UpdateMainMessage("���i�F�ґ򌾂�Ȃ��́B�z���A�Ƃ��ƂƖ����̃_���W�����ɔ����Ȃ�����B");

                        UpdateMainMessage("�A�C���F�ґ򌾂��Ă����͂˂����E�E�E�I�[�P�[�B�����ɔ�����Ƃ��邩�B");

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
                        UpdateMainMessage("�A�C���F�您���i�I����̃_���W�����T�����Ɏv�������Ȃ񂾂��B");

                        UpdateMainMessage("���i�F�ԃ|�[�V�����ł��~�����킯�H");

                        UpdateMainMessage("�A�C���F���ȁI�H�@���ŕ��������I�I�H");

                        UpdateMainMessage("���i�F��̃|�[�V�����ЂƂ��������Ƀ_���W�����ɓ˂����ރ��c�Ȃ�Ăǂ��ɋ���̂�B");

                        UpdateMainMessage("�A�C���F�����E�E�E�����������āA�Ȃ��R�R�͂ЂƂ��ނ��I");

                        UpdateMainMessage("���i�F���傤���Ȃ���ˁA���񂾂���B�b�z����");
                        GetPotionForLana();

                        UpdateMainMessage("�A�C���F������I�T���L���[�T���L���[�I");

                        UpdateMainMessage("���i�F�����A��������Ƃ�����ƁB");

                        UpdateMainMessage("�A�C���F��H������H");

                        UpdateMainMessage("���i�F�b�t�t�A����ς�閧���ȁ�");

                        UpdateMainMessage("�A�C���F�����A�҂đ҂āB�������C�ɂȂ邶��˂����H������H");

                        UpdateMainMessage("���i�F�ґ򌾂�Ȃ��́B�z���A�Ƃ��Ƃƃ_���W�����s���ė��Ȃ�����B");

                        UpdateMainMessage("�A�C���F�ґ򌾂��Ă����͂˂����E�E�E�܂��s���ė��邳�I�C���Ă����I");

                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "�R����"
            else if (this.firstDay >= 2 && !we.Truth_CommunicationLana3)
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        UpdateMainMessage("���i�F���悵�A����������");

                        UpdateMainMessage("�A�C���F���ӂ��E�E�E����ς��̂܂܂��Ɛh���ȁB");

                        UpdateMainMessage("���i�F�����A�o�J�A�C������Ȃ��B���傤�Ǘǂ�������B�R�b�`�ɗ��Ă݂ā�");

                        UpdateMainMessage("�A�C���F�I���̓o�J����Ȃ��ƌ����Ă邾��B������H");

                        UpdateMainMessage("���i�F�܂��A�ǂ�����ǂ�����A�������Ȃ�����B�z���z���z���I");

                        UpdateMainMessage("�A�C���F������Ƃ��ƁI�H�@���������A�����������āB����ȂЂ��ς�Ȃ��āB");

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "�K���c�̕���X�����鉡�X���ɂāE�E�E";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("�A�C���F��H�����A�K���c�f������̕���X�̑O����˂����B");

                        UpdateMainMessage("���i�F�����ĂƁE�E�E����A���ɂ��Ă��āB");

                        UpdateMainMessage("�A�C���F�����A���������ǂ��s���񂾂�I�H�������̊p�ɂ͉��ɂ��E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E�����������I�H�񂾂���́I�I�I");

                        this.we.AvailablePotionshop = true;
                        this.buttonPotion.Visible = true;
                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "�w���i�̃���������i�X��x�Ƃ����Ŕ��A�C���̖ڂɓ������B";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("�A�C���F���ȁE�E�E���ǂ��Ȃ��Ă₪��I�H�E�E�E���̊Ԃɂ���ȏ������I�H");

                        UpdateMainMessage("�A�C���F��I�H���i�̃��c�A���̊Ԃɋ��Ȃ��Ȃ��āI�H");

                        UpdateMainMessage("�A�C���F�҂āA�҂đ҂āB���������E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E�ӂ�");

                        UpdateMainMessage("�A�C���F���ُ̈�Ȃ܂łɔh��ȑ������{����Ă鏬���ɓ����Ă݂邵���Ȃ����B");

                        UpdateMainMessage("�A�C���F���񂿂�[�����B�@�i���āA�������Ă񂾉��j");

                        UpdateMainMessage("���i�F��������Ⴂ�܂���@�D���Ȗ�i�����������E�E�E�߂����E�E���E�E�B");

                        UpdateMainMessage("���i�F���������߂��������܂����");

                        UpdateMainMessage("�A�C���F���O�A��������`���N�`������˂����B�����Ɨ��K�����̂���H");

                        UpdateMainMessage("���i�F��������Ȃ��A�ʂɁB�債�č��͖������B");

                        UpdateMainMessage("�A�C���F���������q�������񂾁B�Ί�ŏo�}�����ȁH");

                        UpdateMainMessage("���i�F�ŏ��Ί��񋟂����񂾂���ǂ�����Ȃ��B");

                        UpdateMainMessage("�A�C���F����A���₢��A���������Ӗ�����Ȃ��āE�E�E�܂��ǂ����B");

                        UpdateMainMessage("�A�C���F�ւ����`�B���\�����Ă񂶂�˂����I");

                        UpdateMainMessage("�A�C���F���̊Ԃɂ���Ȃɑ������񂾂�I�H");

                        UpdateMainMessage("���i�F����A���܂ł̊ԂɌ��\�����A���������ė��Ă����̂�B");

                        UpdateMainMessage("���i�F�ꉞ���͂��낦������A�v���؂��Ďn�߂悤�Ǝv�����̂��");

                        UpdateMainMessage("�A�C���F����B�ǂ��񂶂�˂����H�@�R�C�c�͋C�ɓ��������A���i�I");

                        UpdateMainMessage("���i�F�b�t�t�A���肪�Ƃˁ�");

                        UpdateMainMessage("�A�C���F�������B���������Ă����Ă��ǂ��̂��H");

                        UpdateMainMessage("���i�F����B�l�i�͂������߂ɂ��Ƃ�������B�ǂ������čs���ĂˁB");

                        CallPotionShop();
                        mainMessage.Text = "";

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
                        UpdateMainMessage("���i�F���悵�A����������");

                        UpdateMainMessage("�A�C���F���ӂ��E�E�E����ς��̂܂܂��Ɛh���ȁB");

                        UpdateMainMessage("���i�F�����A�o�J�A�C������Ȃ��B���傤�Ǘǂ�������B�R�b�`�ɗ��Ă݂ā�");

                        UpdateMainMessage("�A�C���F�I���̓o�J����Ȃ��ƌ����Ă邾��B������H");

                        UpdateMainMessage("���i�F�܂��A�ǂ�����ǂ�����A�������Ȃ�����B�z���z���z���I");

                        UpdateMainMessage("�A�C���F������Ƃ��ƁI�H�@���������A�����������āB����ȂЂ��ς�Ȃ��āB");

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "�K���c�̕���X�����鉡�X���ɂāE�E�E";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("�A�C���F��H�����A�K���c�f������̕���X�̑O����˂����B");

                        UpdateMainMessage("���i�F�����ĂƁE�E�E����A���ɂ��Ă��āB");

                        UpdateMainMessage("�A�C���F�����A���������ǂ��s���񂾂�I�H�������̊p�ɂ͉��ɂ��E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E�����������I�H�񂾂���́I�I�I");

                        this.we.AvailablePotionshop = true;
                        this.buttonPotion.Visible = true;
                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "�w���i�̃���������i�X��x�Ƃ����Ŕ��A�C���̖ڂɓ������B";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("�A�C���F���ȁE�E�E���ǂ��Ȃ��Ă₪��I�H�E�E�E���̊Ԃɂ���ȏ������I�H");

                        UpdateMainMessage("�A�C���F��I�H���i�̃��c�A���̊Ԃɋ��Ȃ��Ȃ��āI�H");

                        UpdateMainMessage("�A�C���F�҂āA�҂đ҂āB���������E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E");

                        UpdateMainMessage("�A�C���F�E�E�E�ӂ�");

                        UpdateMainMessage("�A�C���F���ُ̈�Ȃ܂łɔh��ȑ������{����Ă鏬���ɓ����Ă݂邵���Ȃ����B");

                        UpdateMainMessage("�A�C���F���񂿂�[�����B�@�i���āA�������Ă񂾉��j");

                        UpdateMainMessage("���i�F��������Ⴂ�܂���@�D���Ȗ�i�����������E�E�E�߂����E�E���E�E�B");

                        UpdateMainMessage("���i�F���������߂��������܂����");

                        UpdateMainMessage("�A�C���F���O�A��������`���N�`������˂����B�����Ɨ��K�����̂���H");

                        UpdateMainMessage("���i�F��������Ȃ��A�ʂɁB�債�č��͖������B");

                        UpdateMainMessage("�A�C���F���������q�������񂾁B�Ί�ŏo�}�����ȁH");

                        UpdateMainMessage("���i�F�ŏ��Ί��񋟂����񂾂���ǂ�����Ȃ��B");

                        UpdateMainMessage("�A�C���F����A���₢��A���������Ӗ�����Ȃ��āE�E�E�܂��ǂ����B");

                        UpdateMainMessage("�A�C���F�ւ����`�B���\�����Ă񂶂�˂����I");

                        UpdateMainMessage("�A�C���F���̊Ԃɂ���Ȃɑ������񂾂�I�H");

                        UpdateMainMessage("���i�F����A���܂ł̊ԂɌ��\�����A���������ė��Ă����̂�B");

                        UpdateMainMessage("���i�F�ꉞ���͂��낦������A�v���؂��Ďn�߂悤�Ǝv�����̂��");

                        UpdateMainMessage("�A�C���F����B�ǂ��񂶂�˂����H�@�R�C�c�͋C�ɓ��������A���i�I");

                        UpdateMainMessage("���i�F�b�t�t�A���肪�Ƃˁ�");

                        UpdateMainMessage("�A�C���F�������B���������Ă����Ă��ǂ��̂��H");

                        UpdateMainMessage("���i�F����B�l�i�͂������߂ɂ��Ƃ�������B�ǂ������čs���ĂˁB");

                        CallPotionShop();
                        mainMessage.Text = "";

                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion

            //else if (!we.Truth_CommunicationLana4)
            //{
            //    if (!we.AlreadyRest)
            //    {
            //        if (!we.AlreadyCommunicate)
            //        {
            //            we.AlreadyCommunicate = true;
            //        }
            //        else
            //        {
            //            mainMessage.Text = MessageFormatForLana(1001);
            //        }
            //    }
            //    else
            //    {
            //        if (!we.AlreadyCommunicate)
            //        {


            //            we.AlreadyCommunicate = true;
            //        }
            //        else
            //        {
            //            mainMessage.Text = MessageFormatForLana(1002);
            //        }
            //    }
            //}

            #region "�C�x���g�����̏ꍇ"
            else
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        UpdateMainMessage("���i�F�ǂ��H���q�̂ق��́H");

                        UpdateMainMessage("�A�C���F���R���R�B�C���Ƃ����āI�b�n�b�n�b�n�I", true);

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
                        UpdateMainMessage("���i�F���ȂȂ��悤�ɂ���΂鎖�ˁB", true);
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion

        }

        // �n���i�h��
        private void button1_Click(object sender, EventArgs e)
        {
            #region "�������E"
            if (GroundOne.WE2.RealWorld && GroundOne.WE2.SeekerEvent601 && !GroundOne.WE2.SeekerEvent603)
            {
                UpdateMainMessage("�A�C���F�f�ꂳ��A���܂����H");

                UpdateMainMessage("�n���i�F�A�C������Ȃ����B���̗p�����H");

                UpdateMainMessage("�A�C���F����A���ɗp���Ă킯����Ȃ��񂾂��E�E�E");

                UpdateMainMessage("�n���i�F�ǂ������񂾂��A�����C�ɂȂ鎖�ł�����̂����B");

                UpdateMainMessage("�A�C���F�f�ꂳ��̍��т��Ă��B���̐�������������Ȃ��ł����H");

                UpdateMainMessage("�n���i�F�A�b�n�n�n�A���肪�Ƃ��ˁB���������������ł�����̂����H");

                UpdateMainMessage("�A�C���F�ǂ�����āA����Ȕ������т�����悤�ɂȂ�����ł����B");

                UpdateMainMessage("�n���i�F���`��A�ǂ��ƌ����Ă��˂��B����݂����Ȃ��񂳁B�A�b�n�n�n");

                UpdateMainMessage("�A�C���F�n�n�n�E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E�@�E�E�E");

                UpdateMainMessage("�n���i�F�ǂ������񂾂��A������_���W�����֌������񂶂�Ȃ��̂����H");

                UpdateMainMessage("�A�C���F�����B");

                UpdateMainMessage("�n���i�F�Y��ł�悤���ˁB�����Ă݂ȁB");

                UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E�@�E�E�E");

                UpdateMainMessage("�A�C���F����A�����s���Ȃ�����B");

                UpdateMainMessage("�A�C���F�f�ꂳ��A�{���ɂǂ������肪�Ƃ��B");

                UpdateMainMessage("�n���i�F�A�b�n�n�n�A�ςȎq���ˁB�������ች�����ĂȂ���B");

                UpdateMainMessage("�A�C���F�E�E�E����A���肪�Ƃ��B");

                UpdateMainMessage("�A�C���F����A�s���Ă���B");

                UpdateMainMessage("�n���i�F�����A�s���Ă��Ȃ����B�̂ɋC������񂾂�B");

                UpdateMainMessage("�A�C���F����");

                GroundOne.WE2.SeekerEvent603 = true;
                Method.AutoSaveTruthWorldEnvironment();
                Method.AutoSaveRealWorld(this.MC, this.SC, this.TC, this.WE, null, null, null, null, null, this.Truth_KnownTileInfo, this.Truth_KnownTileInfo2, this.Truth_KnownTileInfo3, this.Truth_KnownTileInfo4, this.Truth_KnownTileInfo5);
            }
            #endregion
            #region "�I���E�����f�B�X�����O��"
            else if (we.AvailableDuelMatch && !we.MeetOlLandis)
            {
                if (!we.MeetOlLandisBeforeHanna)
                {
                    UpdateMainMessage("�A�C���F�ӂ��������E�E�E���񂿂�[�����E�E�E");

                    UpdateMainMessage("�n���i�F����܁A�ǂ������񂾂��B�炵���Ȃ����ߑ��Ȃ񂩕t���āB");

                    UpdateMainMessage("�A�C���F����A���͂ł��ˁE�E�E");

                    UpdateMainMessage("�A�C���F���̃{�P�t�����c�t�d�k���Z��֗��Ă�݂����Ȃ�ł���E�E�E");

                    UpdateMainMessage("�n���i�F�����{�������H�ǂ���������Ȃ����B");

                    UpdateMainMessage("�A�C���F�͂����������E�E�E");

                    UpdateMainMessage("�n���i�F������Ƃ����ő҂��ĂȂ����ȁB");

                    UpdateMainMessage("�A�C���F���H���A�͂��B");

                    CallSomeMessageWithAnimation("�n���i�͐~�[���牽���������Ă���");

                    UpdateMainMessage("�A�C���F����͈�́E�E�E�Ȃ�ł����H");

                    UpdateMainMessage("�n���i�F�L�c�`���h���X�p�C�X���������A���h�J���[����A����ƐH�ׂȁB");

                    UpdateMainMessage("�A�C���F�}�W����E�E�E�b�n�b�n�b�n�A�����Ȃ��΂����B");

                    UpdateMainMessage("�A�C���F�������ȁA�l���ĂĂ����傤���˂���ȁB���Ⴂ�������܂����ƁI");

                    UpdateMainMessage("�A�C���F�O�I�H�H�I�I�I���A�h�����������I�I�I");

                    UpdateMainMessage("�n���i�F���̂����ɃL�c���p���`��������Ă����񂾂ˁB�A�b�n�n�n�B", true);
                    we.MeetOlLandisBeforeHanna = true;
                }
                else
                {
                    UpdateMainMessage("�n���i�F�������A���ƂȂ����c�t�d�k���Z��֍s���Ă���񂾂ˁB", true);
                }
                return;
            }
            #endregion
            #region "�S�K�J�n��"
            else if (we.TruthCompleteArea3 && !we.Truth_CommunicationHanna41)
            {
                we.Truth_CommunicationHanna41 = true;

                UpdateMainMessage("�n���i�F����A�ǂ������񂾂��B");

                UpdateMainMessage("�A�C���F���܂˂��A�u���h�����N����{���炦�邩�ȁB");

                UpdateMainMessage("�n���i�F�͂���B");

                UpdateMainMessage("�A�C���F�����A�T���L���[�B");

                UpdateMainMessage("�n���i�F���悢��A�S�K�ɐi�ނ̂����B");

                UpdateMainMessage("�A�C���F�����A�܂��E�E�E");

                UpdateMainMessage("�n���i�F�A�b�n�b�n�A��������Ȃɕ|�C�Â��Ă�񂾂��B");

                UpdateMainMessage("�A�C���F����A�|�C�Â��Ă�킯����Ȃ��񂾂��E�E�E");

                UpdateMainMessage("�A�C���F���ƂȂ����ȁE�E�E�b�n�n");

                UpdateMainMessage("�n���i�F����ȏ��A���i�����ɂ͌������Ȃ��ˁB�䖳������B");

                UpdateMainMessage("�A�C���F���₢�₢��A�Ȃ�ŃA�C�c���o�Ă����ł����B");

                UpdateMainMessage("�n���i�F����A�o�Ă����ራ���̂����H");

                UpdateMainMessage("�A�C���F����A�֌W�˂��b���ȂƎv���āE�E�E");

                UpdateMainMessage("�n���i�F�ŁA�ǂ������񂾂��H�|�C�Â����񂶂�Ȃ��Ƃ�����");

                UpdateMainMessage("�A�C���F�����A�����Ă��ł���A���B");

                UpdateMainMessage("�A�C���F�q�g�S�g�݂����Ɍ����Ă�̂��I�J�V�C��ł����ǁB");

                UpdateMainMessage("�A�C���F�y�������@���Ȃ��z���Č������炢���̂��E�E�E�Ȃ񂾂�B");

                UpdateMainMessage("�A�C���F���܂ł̂����̖A�ɂȂ�����A���čl����ƁA��ɐi�߂Ȃ��Ȃ��ł���B");

                UpdateMainMessage("�n���i�F�S�K�ɍ��s�������Ȃ���Ȃ�A�P���L�΂�����ǂ������B");

                UpdateMainMessage("�A�C���F����A�s�������Ȃ��킯����Ȃ���ł���B");

                UpdateMainMessage("�n���i�F�s���̂��A�|���̂����H");

                UpdateMainMessage("�A�C���F����A�|���킯�ł��Ȃ��E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E�Ȃ�ƂȂ��ł����E�E�E");

                UpdateMainMessage("�A�C���F�y������z���Ă������G���P���Ă��銴���Ȃ�ł���B");

                UpdateMainMessage("�A�C���F�i�߂ΐi�ނقǁA���̊��o�������Ȃ銴�������āE�E�E");

                UpdateMainMessage("�n���i�F�y������z�Ƃ����̂͊��o�̖�肾��B");

                UpdateMainMessage("�n���i�F���E���猩��΁A�y������z�y�����������z�͑��݂��Ȃ��B");

                UpdateMainMessage("�A�C���F����Ɋւ��ẮA�{�P�t�����猙�Ƃ����قǒm�炳��Ă܂��A�������Ă��ł��B");

                UpdateMainMessage("�A�C���F�����炱������R�Ƃ��Ă͈Ⴄ�C�����ĂāE�E�E");

                UpdateMainMessage("�n���i�F�A�C���A�[���@�肷���Ȃ������̐S����B");

                UpdateMainMessage("�n���i�F���񂽂͐̂��炻�̓Ɠ��ȃN�Z������݂���������ˁB");

                UpdateMainMessage("�A�C���F�N�Z���A�b�n�n�n�E�E�E�m���ɂ��������B");

                UpdateMainMessage("�n���i�F�y������z���Ƃ��������܂܂̏�ԂŁA�i�߂Ȃ����B");

                UpdateMainMessage("�n���i�F�y�����������z�őO��Ői�ސS�ӋC���c���ł��Ă�̂Ȃ�");

                UpdateMainMessage("�n���i�F���̏�Ԃ́y������z�����@�m������Ői�߂�̂��S�\���͓������Ƃ͎v��Ȃ������H");

                UpdateMainMessage("�A�C���F����������@�m������ŁE�E�E");

                UpdateMainMessage("�A�C���F�Ȃ�قǁE�E�E�Ȃ�قǁA���������ȁI");

                UpdateMainMessage("�A�C���F�������ȁI�������A�������I��������I�T���L���[�I");

                UpdateMainMessage("�A�C���F���₠�A���΂����̃g�R����Ɩ{�������ɏ����邺�I�b�n�b�n�b�n�I");

                UpdateMainMessage("�n���i�F�A�b�n�n�n�A���������B���C�ɂȂꂽ��Ȃ�ǂ���B");

                UpdateMainMessage("�n���i�F�A���^���₦��ƁA�ׂ̃��i�������₦����ł��邩��ˁB�܂��A�C�����ȁB");

                UpdateMainMessage("�A�C���F���₢�₢��A������A�C�c�͖{�������֌W�Ȃ��ł��傤���A�܂������E�E�E");

                UpdateMainMessage("�n���i�F�A�b�n�n�n�n�A�����������ɂ��Ă�����B");

                UpdateMainMessage("�n���i�F�b�z���A���Ⴀ�撣���čs���Ă��Ȃ����B");

                UpdateMainMessage("�A�C���F�����A���肪�ƁI�@����ȁI");
            }
            #endregion
            #region "�R�K�J�n��"
            else if (we.TruthCompleteArea2 && !we.Truth_CommunicationHanna31)
            {
                we.Truth_CommunicationHanna31 = true;

                UpdateMainMessage("�n���i�F����A�ǂ������񂾂��B");

                UpdateMainMessage("�A�C���F����A�g����t���炦�邩�ȁB");

                UpdateMainMessage("�n���i�F�͂���B");

                UpdateMainMessage("�A�C���F�����āA�ǂ��������ȁE�E�E�z���g�B");

                UpdateMainMessage("�n���i�F�Ȃ�̘b�����H");

                UpdateMainMessage("�A�C���F�X�L���A�b�v�̘b���B");

                UpdateMainMessage("�A�C���F���͂����\�������Ȃ����A�����v�����H�f�ꂿ���B");

                UpdateMainMessage("�n���i�F�A�b�n�n�n�n�A�����\�������񂶂�Ȃ��̂����H");

                UpdateMainMessage("�A�C���F��Ȃ킯�˂���ȁE�E�E�������Ăĕ����Ă񂾂��ǂ��A�n�n�n�B");

                UpdateMainMessage("�n���i�F�������Ă邩�͕�����Ȃ����ǁA�R�������͌������B");

                UpdateMainMessage("�n���i�F�A�C���A���񂽂͋������ނɓ�����B");

                UpdateMainMessage("�A�C���F�������E�E�E�������Ȃ񂩗ǂ��ł���B");

                UpdateMainMessage("�A�C���F�����̃E�B�[�N�|�C���g�Ȃ񂩎R�قǂ��邵�A�S�R�����Ȃ�Ȃ���ł���B");

                UpdateMainMessage("�n���i�F������A�������̗��̐l�����Ă����A�^�V�������񂾂���A�ԈႢ�Ȃ���B");

                UpdateMainMessage("�A�C���F���A���₢��A�{���E�E�E");

                UpdateMainMessage("�n���i�F���₢��A���񂽂͖{���ɋ�����B");

                UpdateMainMessage("�A�C���F���`��A�{���ł����H");

                UpdateMainMessage("�n���i�F�{���̖{�����Ă��񂳁A�A�b�n�n�n�n�B");

                UpdateMainMessage("�A�C���F�n�n�n�E�E�E���肪�ƂȁB�f�ꂿ���B");

                UpdateMainMessage("�A�C���F�����A�R�K���������炳�B");

                UpdateMainMessage("�n���i�F�Ȃ񂾂��B");

                UpdateMainMessage("�A�C���F�܂����낢��Ƌ����Ă���B");

                UpdateMainMessage("�n���i�F�������Ă񂾂��A�A�^�V���狳�����鎖�Ȃ�Ė�����B");

                UpdateMainMessage("�n���i�F�܂������B�@�b�z���z���A�s���O���痎�������Ă񂶂�Ȃ����B");

                UpdateMainMessage("�n���i�F�L�b�`���R�K���N���A���Ă���񂾂ˁA�s���Ă��ȁB");

                UpdateMainMessage("�A�C���F���A�����I�@�I�[�P�[�I");

                if (we.Truth_CommunicationOl31)
                {
                    UpdateMainMessage("�n���i�F����₾�A���������ΖY��Ă�����A�A�C���B");

                    UpdateMainMessage("�A�C���F��H�@��������̂��H");

                    UpdateMainMessage("�n���i�F�A���^�̎t������a�����Ă���B�ו��B");

                    UpdateMainMessage("�A�C���F���A�����B��������ʂ�ۂ���Ȏ������Ă��ȁB");

                    UpdateMainMessage("�A�C���F�I�o�`�����A�ו��Ǘ��Ƃ�������Ă�̂��H");

                    UpdateMainMessage("�n���i�F�A�b�n�n�n�n�A����ĂȂ���ˁB");

                    UpdateMainMessage("�A�C���F�����A�ł��t���̉ו���a�����Ă���Ă�񂾂�H");

                    UpdateMainMessage("�n���i�F�n�C�n�C�A�������炿����Ƒ҂��ĂȁA��U�O�ɏo�Ă�����B");

                    UpdateMainMessage("�A�C���F�����H���A�����E�E�E");
                    we.Truth_CommunicationHanna31_2 = true;
                }
            }
            #endregion
            #region "�ו��a���ǉ�"
            else if (we.TruthCompleteArea2 && !we.AvailableItemBank && we.Truth_CommunicationOl31)
            {
                if (we.Truth_CommunicationHanna31_2 == false)
                {
                    UpdateMainMessage("�n���i�F����A���������΁A�Y��Ă���B");

                    UpdateMainMessage("�A�C���F��H");

                    UpdateMainMessage("�n���i�F�A���^�̎t������a�����Ă���B�ו��B");

                    UpdateMainMessage("�A�C���F���A�����B��������ʂ�ۂ���Ȏ������Ă��ȁB");

                    UpdateMainMessage("�A�C���F�I�o�`�����A�ו��Ǘ��Ƃ�������Ă�̂��H");

                    UpdateMainMessage("�n���i�F�A�b�n�n�n�n�A����ĂȂ���ˁB");

                    UpdateMainMessage("�A�C���F�����A�ł��t���̉ו���a�����Ă���Ă�񂾂�H");

                    UpdateMainMessage("�n���i�F�n�C�n�C�A�������炿����Ƒ҂��ĂȁA��U�O�ɏo�Ă�����B");

                    UpdateMainMessage("�A�C���F�����H���A�����E�E�E");
                }

                UpdateMainMessage("�n���i�F�A�C���A�ق炱��������B");

                UpdateMainMessage("�A�C���F���A�����B�B�B");

                UpdateMainMessage("�A�C���F�i�z���g���B�����ƒu���Ă��Ă���Ă��񂾂ȁE�E�E�j");

                UpdateMainMessage("�n���i�F���������āA�Ƃꉮ������ˁB�A���^�̎t���́B");

                UpdateMainMessage("�n���i�F�A���^�Ɋ��҂��Ă�݂�����������B���ӂ��Ȃ����A�b�z���I");

                UpdateMainMessage("�A�C���F���A�����A�����E�E�E�T���L���[�ȁA�I�o�`�����B");

                UpdateMainMessage("�n���i�F�A�b�n�n�n�n�A�A�^�V����Ȃ��āA���t������Ɋ��ӂ��Ȃ����B");

                UpdateMainMessage("�A�C���F�n�n�E�E�E�m���ɁB");

                UpdateMainMessage("�A�C���F�������ˑR�n����Ă��ȁE�E�E");

                UpdateMainMessage("�A�C���F�I�o�`�����A���������̊ԁA�ۊǂ��Ă����Ă��炦�邩�H");

                UpdateMainMessage("�n���i�F�����A���`��������B�����ƌ��킸���΂炭�͂����ƕۊǂ��Ƃ��Ă������B");

                UpdateMainMessage("�n���i�F�D���Ȏ��Ɏ����čs���񂾂ˁB");

                UpdateMainMessage("�A�C���F���ƁA���̃A�C�e�����o����΁E�E�E");

                UpdateMainMessage("�n���i�F���`�����\��Ȃ���B�a���������m�͗a���Ă����ȁB");

                UpdateMainMessage("�A�C���F���₠�A�z���b�g�����邺�A�T���L���[�I");

                we.AvailableItemBank = true;
                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�n���i�̏h���Łu�ו��̗a���E�󂯎��v���\�ɂȂ�܂����I";
                    md.ShowDialog();
                }

                UpdateMainMessage("�n���i�F�����A�����ɂ͎󂯎��Ȃ���B��������q�ɂ͌����Ă邩��ˁB");

                UpdateMainMessage("�A�C���F���₢��A���������ł��B�{��������܂��B���肪�Ƃ��������܂��I");

                UpdateMainMessage("�n���i�F��́A�A���^�̍D���Ȃ悤�ɐ������ȁB�C�������B");

                UpdateMainMessage("�A�C���F���肪�Ƃ��������܂����I�g�킹�Ă��炢�܂��I�ǂ����ł��I�I");
            }
            #endregion
            #region "�Q�K�J�n��"
            else if (we.TruthCompleteArea1 && !we.Truth_CommunicationHanna21)
            {
                UpdateMainMessage("�n���i�F����A�A�C������Ȃ����B�ǂ������񂾂��H");

                UpdateMainMessage("�A�C���F�f�ꂿ���A�G�������̍g����t���������B");

                UpdateMainMessage("�n���i�F������B�����҂��Ă�񂾂ˁB");

                UpdateMainMessage("�n���i�F�͂��A�ǂ��������オ��ȁB");

                UpdateMainMessage("�A�C���F�T���L���[�A�f�ꂿ���B");

                UpdateMainMessage("�A�C���F�ӂ��E�E�E");

                UpdateMainMessage("�n���i�F�ǂ������񂾂��B�����Ă����B");

                UpdateMainMessage("�A�C���F�Q�K�s���Ă��邺�B");

                UpdateMainMessage("�n���i�F���������A�撣���ė��ȁB");

                UpdateMainMessage("�A�C���F�����E�E�E");

                UpdateMainMessage("�A�C���F���E�E�E��肭�����Ȃ��񂾂��E�E�E");

                UpdateMainMessage("�n���i�F��肭�s���Ă�؋��ƍl������ǂ������H");

                UpdateMainMessage("�A�C���F�E�E�E���͂��H");

                UpdateMainMessage("�n���i�F����������ԂȂ�A����ȕ��ɂ͂Ȃ�Ȃ���B");

                UpdateMainMessage("�n���i�F�����z����������B�Ⴄ�����H");

                UpdateMainMessage("�A�C���F�����A�����E�E�E�܂������ł��B");

                UpdateMainMessage("�n���i�F��������A���̒ʂ�ɐi��ł݂���ǂ������B");

                UpdateMainMessage("�n���i�F�i�܂Ȃ�����A�����Ȃ�Č���������Ȃ�����ˁB");

                UpdateMainMessage("�A�C���F�E�E�E�������A�Ȃ�قǁE�E�E");

                UpdateMainMessage("�A�C���F�f�ꂿ���A���肪�ƂȁB���x�����A�Q�K�s���Ă��邺�I");

                UpdateMainMessage("�n���i�F������A�s���Ă�����Ⴂ�B");

                we.Truth_CommunicationHanna21 = true;
            }
            #endregion
            #region "�����"
            else if (this.firstDay >= 1 && !we.Truth_CommunicationHanna1 && mc.Level >= 1)
            {
                UpdateMainMessage("�A�C���F�f�ꂳ��A���܂����H");

                UpdateMainMessage("�n���i�F�A�C������Ȃ����B���̗p�����H");

                UpdateMainMessage("�A�C���F����A���ɗp���Ă킯����Ȃ��񂾂��E�E�E");

                UpdateMainMessage("�n���i�F�ǂ������񂾂��A�����C�ɂȂ鎖�ł�����̂����B");

                UpdateMainMessage("�A�C���F�f�ꂳ��̍��т��Ă��B���̐�������������Ȃ��ł����H");

                UpdateMainMessage("�n���i�F�A�b�n�n�n�A���肪�Ƃ��ˁB���������������ł�����̂����H");

                UpdateMainMessage("�A�C���F�ǂ�����āA����Ȕ������т�����悤�ɂȂ�����ł����B");

                UpdateMainMessage("�n���i�F���`��A�ǂ��ƌ����Ă��˂��B����΂���͌o����ςނ����Ȃ���B");

                UpdateMainMessage("�A�C���F�o���E�E�E���B");

                UpdateMainMessage("�n���i�F�A�C���B�ЂƂ��܂�Ă���Ȃ������H");

                UpdateMainMessage("�n���i�F�A�C���͍�����_���W�����֌������񂾂ˁH");

                UpdateMainMessage("�A�C���F�͂��B");

                UpdateMainMessage("�n���i�F�_���W�����œ����A�C�e���ŁA�H�ނɂȂ镨�����^�V�̏��֎����Ă��Ă���Ȃ����ˁH");

                UpdateMainMessage("�n���i�F����������A����܂ł������Ɨǂ��[�т��o����悤�ɂȂ邩��ˁB");

                UpdateMainMessage("�A�C���F�}�W�������I�H�Ȃ�A���Ŏ����Ă��܂���I�C���Ă����Ă��������I");

                UpdateMainMessage("�n���i�F�A�b�n�n�n�A���҂��đ҂��Ă��B�����A�����Ă�����Ⴂ�ȁB", true);

                we.Truth_CommunicationHanna1 = true; // �������ڂ̂݁A���i�A�K���c�A�n���i�̉�b�𕷂������ǂ������肷�邽�߁A������TRUE�Ƃ��܂��B
                return;
            }
            #endregion
            #region "���̑�"
            else
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
                        yesno.Location = new Point(this.Location.X + 784, this.Location.Y + 708);
                        yesno.Large = true;
                        yesno.ShowDialog();
                        if (yesno.DialogResult == DialogResult.Yes)
                        {
                            mainMessage.Text = "�n���i�F�͂���A�����͋󂢂Ă��B�������Ƌx�݂ȁB";
                            ok.ShowDialog();
                            mainMessage.Text = "�A�C���F�T���L���[�A���΂����B";
                            ok.ShowDialog();

                            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_NIGHT);

                            mainMessage.Text = "�n���i�F�����͉����H�ׂĂ��������H";
                            ok.ShowDialog();
                            using (TruthRequestFood trf = new TruthRequestFood())
                            {
                                trf.StartPosition = FormStartPosition.CenterParent;
                                trf.MC = this.mc;
                                trf.SC = this.sc;
                                trf.TC = this.tc;
                                trf.WE = this.we;
                                trf.ShowDialog();
                                this.mc = trf.MC;
                                this.sc = trf.SC;
                                this.tc = trf.TC;
                                this.we = trf.WE;
                                if (trf.DialogResult == System.Windows.Forms.DialogResult.OK)
                                {
                                    UpdateMainMessage("�A�C���F���΂����A�w" + trf.CurrentSelect + "�x�𗊂ނ��B");

                                    UpdateMainMessage("�n���i�F�w" + trf.CurrentSelect + "�x���ˁB�����҂��ĂȁB");

                                    UpdateMainMessage("�n���i�F�͂���A���҂����B����Ə����オ��B");

                                    UpdateMainMessage("�@�@�y�A�C���͏\���ȐH�������܂����B�z");

                                    UpdateMainMessage("�A�C���F�ӂ��`�A�H�����H�����E�E�E");

                                    UpdateMainMessage("�A�C���F���΂����A�����������܁I");

                                    UpdateMainMessage("�n���i�F������A��͖����ɔ����Ă������x�݂ȁB");

                                }
                            }

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
                    if (we.AvailableItemBank)
                    {
                        using (SelectDungeon sd = new SelectDungeon())
                        {
                            sd.StartPosition = FormStartPosition.Manual;
                            sd.Location = new Point(this.Location.X + 350, this.Location.Y + 550);
                            sd.MaxSelectable = 2;
                            sd.FirstName = "��b";
                            sd.SecondName = "�q��";
                            sd.ShowDialog();
                            if (sd.TargetDungeon == -1)
                            {
                                return;
                            }
                            else if (sd.TargetDungeon == 1)
                            {
                                mainMessage.Text = "�n���i�F����������B�������撣���Ă�����Ⴂ�B";
                            }
                            else
                            {
                                UpdateMainMessage("�n���i�F�ו��q�ɂ����H�z���A�R�b�`����B", true);
                                mainMessage.Update();
                                System.Threading.Thread.Sleep(1000);
                                CallItemBank();
                                UpdateMainMessage("�n���i�F�܂��p������������񂾂ˁB", true);
                            }
                        }
                    }
                    else
                    {
                        mainMessage.Text = "�n���i�F����������B�������撣���Ă�����Ⴂ�B";
                    }
                }
            }
            #endregion
        }

        // DUEL���Z��
        private void button7_Click(object sender, EventArgs e)
        {
            #region "�S�K�J�n��"
            if (we.TruthCompleteArea3 && !we.Truth_CommunicationOl41)
            {
                we.Truth_CommunicationOl41 = true;

                string detectSword = PracticeSwordLevel(mc);

                UpdateMainMessage("�A�C���F�ӂ��E�E�E�ΐ푊��̐���`�F�b�N���ƁE�E�E");

                UpdateMainMessage("�A�C���F�E�E�E�t���A���������Ă邩�ȁB");

                UpdateMainMessage("���i�F�����A�A�C���B����ȏ��ɋ����̂ˁB");

                UpdateMainMessage("�A�C���F�����A���i���B�ǂ������񂾁H");

                UpdateMainMessage("���i�F�Ȃ񂩂ˁA���Z��̎�t�̐l���A�A�C����T���Ă��݂�����B");

                UpdateMainMessage("�A�C���F�������B���Ⴀ������Ǝ�t�܂ōs���Ă���B�T���L���[�B");

                CallSomeMessageWithAnimation("�|�|�|�@�A�C���͎�t�܂ŏo�������@�|�|�|");

                UpdateMainMessage("�@�@�y��t��F�c�t�d�k���Z��ւ悤�����B�z");

                UpdateMainMessage("�A�C���F�悤�A��t����B���ɉ����p�ł��������̂��H");

                UpdateMainMessage("�@�@�y��t��F�͂��A�V���̓`���������Ă���܂��B�z");

                UpdateMainMessage("�A�C���F�`���I�H����Ȑ��x������̂��H");

                UpdateMainMessage("�@�@�y��t��F�͂��A����܂��B�z");

                UpdateMainMessage("�A�C���F�܁A�܂��������B�B�B�ŁA�ǂ�ȓ��e�Ȃ񂾁H");

                UpdateMainMessage("�@�@�y��t��F������ɁA����ID<3-297761 Ol_Landis>�̉����f�[�^���������`�b�v������܂��B�z");

                UpdateMainMessage("�A�C���F�ւ��A���������`�b�v���ȁB");

                UpdateMainMessage("�A�C���F���āA����ID���E�E�E�b�O�E�E�E");

                UpdateMainMessage("�@�@�y��t��F�����`�b�v�͎�O����������ɂ���܂��A������̃f���f���N�ɃZ�b�g���Ă��g�����������B�z");

                UpdateMainMessage("�A�C���F�f���f���N�H�H");

                UpdateMainMessage("�@�@�y��t��F�ڂ����͒[���ɂ��鑀�������ǂ�ł����p���������B�z");

                UpdateMainMessage("�A�C���F���A�����E�E�E");

                UpdateMainMessage("�A�C���F(�������A����Ȃ��̂�����Ƃ́E�E�E�j");

                UpdateMainMessage("�A�C���F�m���R�b�`���ȁB");

                UpdateMainMessage("�A�C���F�����A���ꂾ�ȁB���[�Ƃǂ�ǂ�E�E�E");

                UpdateMainMessage("�A�C���i���ǁj�F�u�`�b�v�𑕒u���ɂ��鍷�����݌��ɑ}�����APUSH�X�^�[�g�������Ă��������B�v");

                UpdateMainMessage("�A�C���F���ꂩ�E�E�E�悵�B");

                UpdateMainMessage("�@�@�y�y�y�@���̏u�ԁB�@�A�C���̔]���ɒ��ڃI���E�����f�B�X�̉������`����Ă����I�I�@�z�z�z");

                UpdateMainMessage("�����f�B�X�F�s�悧�A�U�R�A�C���t");

                UpdateMainMessage("�A�C���F�����I�I�т����肷��ȁB���ڕ�������̂���A����B");

                UpdateMainMessage("�����f�B�X�F�s����𕷂��Ă���Ď��́A�ЂƂ܂��A�l�K�ւƐi�ߎn�߂����Ď����ȁt");

                UpdateMainMessage("�A�C���F�܁A�܂������Ă݂邩�E�E�E");

                UpdateMainMessage("�����f�B�X�F�s�������A�悭�����t");

                UpdateMainMessage("�����f�B�X�F�s�����牴���������́A�S�Ď������t");

                UpdateMainMessage("�A�C���F�����H");

                UpdateMainMessage("�����f�B�X�F�s�Ă߂����󂯎~�߂邩�ǂ����Ɋւ��ẮA�Ă߂��Ō��߂�t");

                UpdateMainMessage("�@�@�y�y�y�@�A�C���͂ق�̏��������ċz���~�܂����@�z�z�z");

                UpdateMainMessage("�����f�B�X�F�s������Ă߂��ɋN���肤�鎖�ۂ�S�ē`����t");

                UpdateMainMessage("�����f�B�X�F�s�܂��A�Ă߂��̉��ɂ���A�[�e�B�����t");

                UpdateMainMessage("�����f�B�X�F�s�l�K�J�n�Ɠ����Ɏp�������t");

                UpdateMainMessage("�@�@�y�y�y�@����́@�z�z�z");

                UpdateMainMessage("�����f�B�X�F�s���Ɏl�K�̓��e�����t");

                UpdateMainMessage("�����f�B�X�F�s��������Ɠ��ؒʂ�i�߂�͂����t");

                UpdateMainMessage("�����f�B�X�F�s�����|�C���g�͂قƂ�ǂ˂��A��؂ǂ��肾�t");

                UpdateMainMessage("�@�@�y�y�y�@�S�̂ǂ�������Ł@�z�z�z");

                if (detectSword == Database.LEGENDARY_FELTUS)
                {
                    UpdateMainMessage("�����f�B�X�F�s�����Ă߂��́A�����̒m��Ȃ��ԂɁt");

                    UpdateMainMessage("�����f�B�X�F�s���Ă߂�����ɂ��Ă��邻�̐_���t�F���g�D�[�V�����A���̊Ԃɂ��������t");
                }
                else
                {
                    UpdateMainMessage("�����f�B�X�F�s�����Ă߂��́A���̂܂ܐi�ݑ����t");

                    UpdateMainMessage("�����f�B�X�F�s�_���t�F���g�D�[�V�����i���Ɏ�ɂ���@��������t");
                }

                UpdateMainMessage("�@�@�y�y�y�@���ɔF�����Ă������̗l�ȗ₽�����G�@�z�z�z");

                if (detectSword == Database.LEGENDARY_FELTUS)
                {
                    UpdateMainMessage("�����f�B�X�F�s�_���t�F���g�D�[�V��������������ԂŁA�_���W������i�ݑ����t");
                }
                else
                {
                    UpdateMainMessage("�����f�B�X�F�s�_���t�F���g�D�[�V�������o���Ă��Ȃ��܂܂̏�ԂŁA�_���W������i�ݑ����t");
                }

                UpdateMainMessage("�����f�B�X�F�s�l�K�A�Ō�̎����y�_�̑I�����z�ɑ����t");

                UpdateMainMessage("�����f�B�X�F�s�Ă߂��́A�����ŁE�E�E�t");

                UpdateMainMessage("�@�@�y�y�y�@�S�̋��X�ɂ܂ŁA�^�����ȃC���N�����ݍ��ނ悤�Ɂ@�z�z�z");

                UpdateMainMessage("�����f�B�X�F�s����I������t");

                UpdateMainMessage("�����f�B�X�F�s�y�_�̑I�����z���Ă̂́A�����������񂾁t");

                UpdateMainMessage("�����f�B�X�F�s���̌�Ă߂��́t");

                UpdateMainMessage("�@�@�y�y�y�@��]�Ƃ����F�ʂ��̒��𕢂����@�z�z�z");

                UpdateMainMessage("�����f�B�X�F�s��]����t");

                UpdateMainMessage("�����f�B�X�F�s�ŉ��w�ւ̓��B�́A�I���_�Ȃ񂩂����˂��t");

                UpdateMainMessage("�����f�B�X�F�s�I���̎n�܂�t");

                UpdateMainMessage("�����f�B�X�F�s��΂ɁA�ŉ��w�ւ̓�������񂶂�˂����t");

                UpdateMainMessage("�����f�B�X�F�s�������A�킩�����ȁt");

                UpdateMainMessage("�A�C���F�E�E�E");

                UpdateMainMessage("�A�C���F�i�Ȃ񂾁E�E�E����E�E�E�j");

                UpdateMainMessage("�A�C���F�i�t�����A���̂��ꂩ���ɋN���肤�鎖��m���Ă�񂾁j");

                UpdateMainMessage("�A�C���F�i���A���₢�₢��B�������������Ȃ�ĕ�����͂����˂��񂾂��j");

                UpdateMainMessage("�A�C���F�i�ł����̌������́E�E�E�j");

                UpdateMainMessage("�A�C���F�i�^�ɔ��錾�����A�܂�u���t��Њd����˂��j");

                UpdateMainMessage("�A�C���F�i�{���̎����L�b�`���������Ɏg�������j");

                UpdateMainMessage("�A�C���F�i���Ƃ�����E�E�E�j");

                UpdateMainMessage("���i�F�˂��A�ǂ������̂�H����������~�܂��Ă�݂���������");

                UpdateMainMessage("�A�C���F�ǂ��킠���I�I�I");

                UpdateMainMessage("�A�C���F���i���B�����������Ȃ�B");

                UpdateMainMessage("���i�F���ʂɐ��������������Ȃ̂ɁA�ߏ�Ƀr�r���Ă�̂͂���������Ȃ��B");

                UpdateMainMessage("�A�C���F���A�������E�E�E�b�n�n�n�E�E�E");

                UpdateMainMessage("���i�F�f���f���N����́A�ǂ����͓���ꂽ�́H");

                UpdateMainMessage("�A�C���F�����A�܂��ȁB����Ȃ�ɁB");

                UpdateMainMessage("���i�F�ӂ���A�܂��F���͂��Ȃ����ǁB");

                UpdateMainMessage("�A�C���F�����A���Ⴀ�撣���čs���Ƃ��邩�I");

                UpdateMainMessage("���i�F�܂��������ς�炸�Q���L����ˁB���Z��ŖY�ꕨ�Ƃ����Ȃ��ł�H");

                UpdateMainMessage("�A�C���F�����A�������Ă���āB�����A�s�����I");
            }
            #endregion
            #region "�R�K�J�n��"
            else if (we.TruthCompleteArea2 && !we.Truth_CommunicationOl31)
            {
                we.Truth_CommunicationOl31 = true;

                UpdateMainMessage("�A�C���F�t���A���邩�H");

                UpdateMainMessage("�����f�B�X�F�Ȃ񂾁A�U�R�A�C���B");

                UpdateMainMessage("�A�C���F�R�K�Ɍ����āA�����班�����^�C�����E�E�E");

                UpdateMainMessage("�����f�B�X�F�������A�����Y��Ă���������B");

                UpdateMainMessage("�A�C���F������H");

                UpdateMainMessage("�����f�B�X�F�I���͔�����B");

                UpdateMainMessage("�A�C���F���H");

                UpdateMainMessage("�����f�B�X�F�ȏゾ�B");

                UpdateMainMessage("�A�C���F�E�E�E�������������I�H���ł���I�H");

                UpdateMainMessage("�����f�B�X�F�}�ȗp�����B�@�e���F�̂����̓R�R�܂ł��B");

                UpdateMainMessage("�A�C���F�ȁA������ˑR�I�H�@�p�����ĉ�����I");

                UpdateMainMessage("�����f�B�X�F�������A�ق�U�R�B�Ă߂��ɂ͊֌W�˂��B");

                UpdateMainMessage("�A�C���F�������������E�E�E�}�W����E�E�E");

                UpdateMainMessage("�����f�B�X�F�ו��̌������A�y�n���i�������h���z�ɗa���Ă������B");

                UpdateMainMessage("�����f�B�X�F�D���Ȏ��ɉו��������Ƃ��B");

                UpdateMainMessage("�����f�B�X�F�ȏゾ�B");

                CallSomeMessageWithAnimation("�I���E�����f�B�X�͂��̏ꂩ�痧�������Ă������E�E�E");

                Method.RemoveParty(we, tc);

                UpdateMainMessage("�A�C���F�E�E�E�������A�Ȃ�̑O�G�����������B");

                UpdateMainMessage("���i�F�ł������f�B�X����́A�A�C��������̂��ꉞ�҂��Ă������P��ˁB");

                UpdateMainMessage("�A�C���F��H���E�E�E�܂��m���ɂ����Ȃ̂����B");

                UpdateMainMessage("���i�F�t�t�t�A�����I�J�V�C��ˁB�A�C�����\�C�ɓ����Ă�񂶂�Ȃ��́�");

                UpdateMainMessage("�A�C���F�E�\���A����Ȃ̃e�L�g�[���y��`�҂���B�B�B");

                UpdateMainMessage("���i�F�ŁA�_���W�����͂ǂ�����킯�H");

                UpdateMainMessage("�A�C���F�P�l���鎖�ŁA�_���W�����̃����X�^�[��Փx����������邾��B");

                UpdateMainMessage("�A�C���F���܂���~�߂Ă��ǂ����Ȃ�킯����Ȃ����ȁB���s���B");

                UpdateMainMessage("���i�F�A�C�������s�Ȃ�A���������������čs������");

                UpdateMainMessage("�A�C���F�����A�������Ă���B�����邺�I");

                if (we.Truth_CommunicationLana31)
                {
                    UpdateMainMessage("���i�F�Ƃ���ŁA�]�����u����t�@�[�W���{�a�ɍs���Ă݂Ȃ��H");

                    UpdateMainMessage("�A�C���F�����A�����������ȁI�@����A�s���Ƃ��邩�I");
                }

                return;
            }
            #endregion

            #region "Duel�\����"
            if (!we.AvailableDuelMatch && !we.MeetOlLandis)
            {
                if (!we.AlreadyRest)
                {
                    UpdateMainMessage("�A�C���F�܂��A�o�^�\�����݂������B�����܂ő҂Ƃ��邩�B", true);
                }
                else
                {
                    UpdateMainMessage("�A�C���F��t�����B���̓o�^�\���͂܂����H");

                    UpdateMainMessage("�@�@�y��t��F�������΂炭���҂����������B�z");

                    UpdateMainMessage("�A�C���F�������A���Ⴀ�܂��ȁB", true);
                }
                return;
            }
            #endregion

            #region "�I���E�����f�B�X����"
            if (we.AvailableDuelMatch && !we.MeetOlLandis)
            {
                GroundOne.StopDungeonMusic();
                GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                UpdateMainMessage("�@�@�y��t��F�c�t�d�k���Z��ւ悤�����B�z");

                UpdateMainMessage("�A�C���F�您��t����B�����͂�����Ɗ���o���������Ȃ񂾁B");

                UpdateMainMessage("�A�C���F���₢��A�b�z���g�B���ɗp���͂˂��񂾁B");

                UpdateMainMessage("�A�C���F�ז������ȁA�b�n�b�n�b�n�I");

                UpdateMainMessage("�A�C���F������A�܂����x�ȁI");

                UpdateMainMessage("�@�@�y�y�y�@���̏u�ԁB�@�A�C���͔w�؂̊��G�������Ȃ�قǓ�������B�@�z�z�z");

                UpdateMainMessage("�����f�B�X�F�悧�A�U�R�A�C���B");

                UpdateMainMessage("�A�C���F�E�E�E�l�Ⴂ���B���̓U�R�A�C������˂��B");

                UpdateMainMessage("�����f�B�X�F�ق��A���Ⴀ�N�Ȃ񂾁H");

                UpdateMainMessage("�A�C���F����E�E�E");

                UpdateMainMessage("�����f�B�X�F�w����A���₢�₢��B�x�@���B");

                UpdateMainMessage("�����f�B�X�F�Ă߂��B�S�������������R�������Ă˂��悤���ȁB");

                UpdateMainMessage("�A�C���F����E�E�E������A�҂��A������A�b�^���}�I");

                UpdateMainMessage("�����f�B�X�F�͂��H�ǂ��^���}�Ȃ񂾁H");

                UpdateMainMessage("�A�C���F���H���A����A�����A�������Ă���񂶂�");

                UpdateMainMessage("�����f�B�X�F�O�j���������ă��P���B�您�������������������I�ǂ��S�\�����B");

                UpdateMainMessage("�A�C���F����A����A����������傿��I�^���}�^���}�^���}�I�I");

                GroundOne.StopDungeonMusic();

                UpdateMainMessage("�����f�B�X�F����ł����₠�������������I�I�I");

                UpdateMainMessage("�@�@�y�@�h�h�h�X�h�X�h�X�h�X�h�h�h�h�h�X�h�X�h�X�h�X�h�X�@�z");

                UpdateMainMessage("�@�@�y�@�h�K�K�K�K�K�K�h�K�K�K�K�h�h�K�K�K�K�K�K�K�K�@�z");

                UpdateMainMessage("�@�@�y�@�{�{�{�{�{�{�O�b�V���A�A�@�@�@�@�E�E�E�@�z");

                CallSomeMessageWithAnimation("�|�|�|�@�A�C���C�₩��A�P���Ԃ��o�߂��ā@�|�|�|");

                GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                UpdateMainMessage("�A�C���F�������A���`���N�`�������A�z���g�B�B�B�����E�E�E");

                UpdateMainMessage("�����f�B�X�F�Ă߂��A�ق���Ƃɐ������Ă˂��ȁB");

                UpdateMainMessage("�A�C���F�����Ȃ�˂��������Ă���̂������񂾂낤���B");

                UpdateMainMessage("�����f�B�X�F�����Ȃ�˂��������Ă˂����낧���B");

                UpdateMainMessage("�A�C���F����A�������̂����E�E�E�O�H���A�A�A�A���ė���Ȃ����́B");

                UpdateMainMessage("�����f�B�X�F�V���l�G�ȁA��Ȃ́B");

                UpdateMainMessage("�����f�B�X�F�Ă߂����シ����B���ꂾ�����B");

                UpdateMainMessage("�A�C���F����A�܂��c�t�d�k����ꏊ�ł��������œ˂��������Ă���Ȃ��́B");

                UpdateMainMessage("�����f�B�X�F����������ǂ��񂾁H");

                UpdateMainMessage("�A�C���F����E�E�E���₢��A��������Ȃ��āB");

                UpdateMainMessage("�����f�B�X�F�܂����B���́@�w����A���₢�₢��B�x");

                UpdateMainMessage("�A�C���F����A�Ⴄ�B��������ȁE�E");

                UpdateMainMessage("�����f�B�X�F�Ă߂��A�c�t�d�k�ɎQ�킷�邻�����ȁB");

                UpdateMainMessage("�A�C���F���H�����A�Q�킷�邳�B");

                UpdateMainMessage("�����f�B�X�F���l����U�R�A�C���ցA�ꌾ�����Ă�낤�Ǝv���ĂȁB");

                UpdateMainMessage("�A�C���F���ȁA������H");

                UpdateMainMessage("�@�@���I���E�����f�B�X�͎����̑����֎w��������E�E�E��");

                UpdateMainMessage("�����f�B�X�F�@�@����g�R�܂ŁA���Ă݂���B�@");

                UpdateMainMessage("�A�C���F�E�E�E�����E�E�E���R���I");

                UpdateMainMessage("�A�C���F���R�s���Ă�邳�I���Ă��ȁI�I");

                UpdateMainMessage("�@�@���I���E�����f�B�X�͏������΂ނƁE�E�E��");

                UpdateMainMessage("�����f�B�X�F�b�t�A�܂��K���o����B�U�R�A�C���B");

                CallSomeMessageWithAnimation("�I���E�����f�B�X�͂��̏ꂩ�痧�������Ă������E�E�E");

                UpdateMainMessage("�A�C���F���������B���ǁA����ꑹ����E�E�E");

                UpdateMainMessage("���i�F�����A�A�C���B����������");

                UpdateMainMessage("�A�C���F���i�B���̂܂ɗ��Ă��񂾁H");

                UpdateMainMessage("���i�F�A�C�����C�₵�Ă���ʂ��炢������");

                UpdateMainMessage("�A�C���F�����C�₵�Ă�g�R�����Ă����Ď�����B");

                UpdateMainMessage("���i�F�ł��{���A����ȂɐH����Ă�̂ɁA�ӊO�ƃA�C�����C��ˁB");

                UpdateMainMessage("�A�C���F�t���͐����Ɋ댯���y�ڂ��}���U���͂��˂��^�C�v�Ȃ񂾁B");

                UpdateMainMessage("�A�C���F������A��T���C��A�������͕a�@���肪�ւ̎R���ă��P���B");

                UpdateMainMessage("���i�F�a�@����ɂȂ����Ⴄ�l������̂ˁB�܂��c�t�d�k���Č����ȏサ�傤���Ȃ��񂾂낤���ǁB");

                UpdateMainMessage("���i�F���A���������B��������Q��\�ɂȂ�����ł���H");

                UpdateMainMessage("�A�C���F�܂��A�������ȁB�������������A��������ΐ킵�Ă݂鏊�Ȃ񂾂�");

                UpdateMainMessage("���i�F�c�t�d�k�ɂ�����ڍ׃��[���́A���Ă݂��H");

                UpdateMainMessage("�A�C���F����A�܂����ȁB���i�͒m���Ă�̂��H");

                UpdateMainMessage("���i�F������A�m��Ȃ����B");

                UpdateMainMessage("���i�F�c�t�d�k�Q��҂݂̂ɒʒB�����݂��������B���͓o�^���ĂȂ�����ˁB");

                UpdateMainMessage("�A�C���F�܂��A��t�ɕ����Ă݂�Ƃ��邩�B���[���A��t����B");

                UpdateMainMessage("�@�@�y��t��F�c�t�d�k���Z��ւ悤�����B�z");

                UpdateMainMessage("�A�C���F���܂˂��A�������͗p���˂����Č������񂾂��A�c�t�d�k�ڍ׃��[�����Ă̌����Ă��炦�邩�H");

                UpdateMainMessage("�@�@�y��t��F�A�C���l�ł��ˁA�����������܂����B�z");

                UpdateMainMessage("�@�@����t�W���͉���������Ă��鎆�؂���P�������Ă����B��");

                UpdateMainMessage("�@�@�y��t��F�A�C���l�Ɋւ���c�t�d�k�ڍ׃��[���͂��̒ʂ�ł��B���Q�Ƃ��������B�z");

                UpdateMainMessage("�A�C���F�T���L���[�I�@���̎��́A���̓z�ɂ������Ă����̂��H");

                UpdateMainMessage("�@�@�y��t��F�\���܂���B�z");

                UpdateMainMessage("�A�C���F�������A�킴�킴���肪�ƂȁB");

                UpdateMainMessage("�A�C���F���i�A������Ă������B���Ⴀ�A���Ă݂邩�B");

                UpdateMainMessage("���i�F����A���ď����Ă���H");

                UpdateMainMessage("�A�C���F�ǂ�ǂ�E�E�E");

                using (TruthDuelRule tdr = new TruthDuelRule())
                {
                    tdr.StartPosition = FormStartPosition.CenterParent;
                    tdr.ShowDialog();
                }

                UpdateMainMessage("�A�C���F�Ȃ�قǂȁB��̕����������B");

                UpdateMainMessage("���i�F�ꉞ������X�e�[�^�X�l�Ȃ񂩂͌����Ă��炦��킯�ˁB");

                UpdateMainMessage("�A�C���F�����݂������ȁB");

                UpdateMainMessage("���i�F���̂����A���������ꏏ������A���݂���̓����������������Ⴄ��ˁB");

                UpdateMainMessage("�A�C���F�����݂������ȁB");

                UpdateMainMessage("���i�F���C�t�O�ɂȂ������_�ŏ��s�����܂�B���Ă��Ƃ͒P���ɑ����|���Ηǂ��̂�ˁB");

                UpdateMainMessage("�A�C���F�����݂������ȁB");

                UpdateMainMessage("���i�F�����͉����ꏏ�ɐH�ׂĂ��H");

                UpdateMainMessage("�A�C���F�����݂������ȁB");

                UpdateMainMessage("�@�@�@�w�V���S�I�H�H���I�I�x�i���i�̃h���X�e�B�b�N�L�b�N���A�C���̃~�]�I�`���y��j�@�@");

                UpdateMainMessage("�A�C���F�������E�E�E���������A�����������āB");

                UpdateMainMessage("�A�C���F���܂��A������ƂP�񂾂��ΐ킳���Ă���B���̌�ŁA�ѐH�ׂɂ��������B");

                UpdateMainMessage("���i�F��A���Ⴀ�҂��Ă��ˁB��邩��ɂ́A�����Ə����Ă�ˁ�");

                UpdateMainMessage("�A�C���F�����A�C���Ƃ����āI�b�n�b�n�b�n�I�I");

                we.MeetOlLandis = true;
                return;
            }
            #endregion

            #region "�Q�K�J�n��"
            else if (we.TruthCompleteArea1 && !we.Truth_CommunicationOl21)
            {
                GroundOne.StopDungeonMusic();
                GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                UpdateMainMessage("�@�@�y��t��F�c�t�d�k���Z��ւ悤�����B�z");

                UpdateMainMessage("�A�C���F�您��t����B������������Ɗ���o�����������B");

                UpdateMainMessage("�A�C���F������ȁI�b�n�b�n�b�n�I");

                UpdateMainMessage("�@�@�y�y�y�@���̏u�ԁB�@�A�C���͔w�؂̊��G�������Ȃ�قǓ�������B�@�z�z�z");

                UpdateMainMessage("�����f�B�X�F�悧�B�킴�킴����J�Ȃ������B");

                UpdateMainMessage("�A�C���F����A�����͗p���������ė����B");

                UpdateMainMessage("�����f�B�X�F�ق��H");

                UpdateMainMessage("�A�C���F���������A�P�K�B");

                UpdateMainMessage("�����f�B�X�F��邶��˂����B�債�����񂾁B");

                UpdateMainMessage("�A�C���F���̂܂܁A�i�ނ��B");

                UpdateMainMessage("�A�C���F�Q�K���e���y�����I�b�n�b�n�b�n�I");

                UpdateMainMessage("�����f�B�X�F�b�t�A�܂�����΂��B�U�R�A�C���B");

                UpdateMainMessage("�A�C���F�҂āA�����͂��������b�����ɗ����񂶂�˂��B");

                UpdateMainMessage("�A�C���F�t���A���肢������񂾁B�����Ă���邩�H");

                UpdateMainMessage("�����f�B�X�F�����Ă݂�B");

                UpdateMainMessage("�A�C���F�t�����_���W�����ֈꏏ�ɗ��Ă���Ȃ����H");

                UpdateMainMessage("�����f�B�X�F�E�E�E");

                UpdateMainMessage("�����f�B�X�F�E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E");

                UpdateMainMessage("�����f�B�X�F�E�E�E");

                UpdateMainMessage("�����f�B�X�F�E�E�E");

                UpdateMainMessage("�����f�B�X�F�E�E�E");

                UpdateMainMessage("�A�C���F�E�E�E");

                UpdateMainMessage("�����f�B�X�F�ʖڂ��ȁB");

                UpdateMainMessage("�A�C���F�E�E�E�������B");

                UpdateMainMessage("�����f�B�X�F�����͐������Ă�݂Ă�����˂����B");

                UpdateMainMessage("�A�C���F�E�E�E���H");

                UpdateMainMessage("�����f�B�X�F���ł��˂��B�I���I�I�Ƃ��ƂƂQ�K���e���Ă��₪��I�I�I");

                UpdateMainMessage("�A�C���F���A��������I���������I�����傿�傿��I�I�^���}�^���}�^���}�I�I�I");

                UpdateMainMessage("�@�@�y�@�Y�h�b�h�h�h�h�h�b�h�h�H�H�h�h�h�h�@�z");

                UpdateMainMessage("�@�@�y�@���L�{�O�b�V���A�@�@�@�E�E�E�@�z");

                CallSomeMessageWithAnimation("�|�|�|�@�A�C���C�₩��A�P���Ԃ��o�߂��ā@�|�|�|");

                GroundOne.StopDungeonMusic();
                GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                UpdateMainMessage("���i�F���ŁA�f��ꂿ������킯�H");

                UpdateMainMessage("�A�C���F�����݂������ȁB�C�b�c�c�c�E�E�E");

                UpdateMainMessage("���i�F�ł��ǂ��U���C�ɂȂꂽ��ˁH���E�s�ׂ���Ȃ��H�H");

                UpdateMainMessage("�A�C���F�ł����B�t��������΁A�_������I�Ƀp���[�A�b�v���邾��H");

                UpdateMainMessage("���i�F���[��A�܂������f�B�X�̂��t�����񂪋��Ă��ꂽ��S������ˁB");

                UpdateMainMessage("�A�C���F�ł�����ȗ��R���ᒇ�Ԃɓ����Ă���Ȃ���ȁB");

                UpdateMainMessage("���i�F������ˁE�E�E���`��E�E�E");

                UpdateMainMessage("�A�C���F���₢��A�ǂ��񂾁B�s�������Q�K�B");

                UpdateMainMessage("���i�F�ǂ��́H");

                UpdateMainMessage("�A�C���F�����A���͂��̂܂ܐi�ނ����˂��B");

                UpdateMainMessage("�A�C���F����������Ă����L�b�J�P�̂悤�ȃ��m������Ă݂��邳�B");

                UpdateMainMessage("���i�F�������ˁB����A�Q�K���e�Ɍ����Ċ撣��܂����");

                UpdateMainMessage("�A�C���F�����I", true);

                we.Truth_CommunicationOl21 = true;
                return;
            }
            #endregion

            #region "�I���E�����f�B�X�𒇊Ԃɂ���Ƃ���"
            else if (we.dungeonEvent226 && !we.Truth_CommunicationOl22)
            {
                //we.Truth_CommunicationOl22 = true;
                if (!we.Truth_CommunicationOl22Fail)
                {
                    GroundOne.StopDungeonMusic();
                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                    UpdateMainMessage("�@�@�y��t��F�c�t�d�k���Z��ւ悤�����B�z");

                    UpdateMainMessage("�A�C���F�您��t����I");

                    UpdateMainMessage("�A�C���F����`�`�A���₢�₢��I�b�n�b�n�b�n�I");

                    UpdateMainMessage("�@�@�y�y�y�@���̏u�ԁB�@�A�C���͔w�؂̊��G�������Ȃ�قǓ�������B�@�z�z�z");

                    UpdateMainMessage("�����f�B�X�F�����A��t����ɉ������Ȃ�΂��Ă₪��B");

                    UpdateMainMessage("�A�C���F�t���A�����Ă���B");

                    UpdateMainMessage("�@�@�w�����f�B�X�͈�u���i�֎������ڂ��E�E�E�x");

                    UpdateMainMessage("�����f�B�X�F�������������H");

                    UpdateMainMessage("�A�C���F���̃_���W�����B�ǂ��Ȃ��Ă�H");

                    UpdateMainMessage("�����f�B�X�F�ǂ����������˂��B�P�Ȃ�_���W�������B");

                    UpdateMainMessage("�A�C���F����̎����A�N���A�������B");

                    UpdateMainMessage("�����f�B�X�F��邶��˂����B�U�R�A�C���ɂ�����債�����񂾁B");

                    UpdateMainMessage("�A�C���F�m�̕������N���A�܂ł��ƈ�����B");

                    UpdateMainMessage("�����f�B�X�F�Ă߂��A���̘b�����ɂ����H");

                    UpdateMainMessage("�@�@�y�y�y�@�A�C���͂���ɔw�؂ɐ�ɂ��������B�@�z�z�z");

                    UpdateMainMessage("�A�C���F���I�@�b�^�C���I�I");

                    UpdateMainMessage("�A�C���F�����Ă���B�t���B");

                    UpdateMainMessage("�����f�B�X�F����m��Ă��񂾁H");
                }
                else
                {
                    if (!we.Truth_CommunicationOl22Progress1)
                    {
                        UpdateMainMessage("�A�C���F�t���A�����Ă���I�@���ނ��I");

                        UpdateMainMessage("�����f�B�X�F����m��Ă��񂾁H");
                    }
                    else if (!we.Truth_CommunicationOl22Progress2)
                    {
                        UpdateMainMessage("�A�C���F�t���E�E�E���ށA�����P�񂾂��`�����X���I");

                        UpdateMainMessage("�����f�B�X�F�����E�E�E���傤���˂��B");
                    }
                    else
                    {

                        UpdateMainMessage("�����f�B�X�F�ǂ������B");

                        UpdateMainMessage("�A�C���F���܂��܂��I�@�������DUEL���I�I");

                        UpdateMainMessage("�����f�B�X�F���x�ł��������Ă�����A�U�R�A�C���B");
                    }
                }

                using (TruthDecision td = new TruthDecision())
                {
                    td.StartPosition = FormStartPosition.CenterParent;

                    bool firstQuestion = we.Truth_CommunicationOl22Progress1;
                    if (!firstQuestion)
                    {
                        GroundOne.StopDungeonMusic();
                        GroundOne.PlayDungeonMusic(Database.BGM16, Database.BGM16LoopBegin);

                        td.MainMessage = "�@�y�@�I���E�����f�B�X�ւ̎����I�����Ă��������B�@�z";
                        td.FirstMessage = "���̃_���W�����A�ǂ������Ă����Ηǂ��H";
                        td.SecondMessage = "���̃_���W�����A�ǂ�����Ή�����񂾁H";
                        td.ShowDialog();
                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                        {
                            UpdateMainMessage("�A�C���F���̃_���W�����A�ǂ������Ă����Ηǂ��H");

                            UpdateMainMessage("�����f�B�X�F�ǂ����������˂��B�����Ȃ�ɉ����Ă݂�B");

                            UpdateMainMessage("�A�C���F����̎������Ă̂��������񂾁B");

                            UpdateMainMessage("�����f�B�X�F�ق��B");

                            UpdateMainMessage("�A�C���F�����ł́A�w�_�X�̎��x���񓚂��邱�ƂɂȂ��Ă����B");

                            UpdateMainMessage("�����f�B�X�F�񓚂́H");

                            UpdateMainMessage("�A�C���F�o�������B");

                            UpdateMainMessage("�����f�B�X�F�ŁA���ꂪ�ǂ������H");

                            UpdateMainMessage("�A�C���F������ǂ������ėǂ��̂����A�킩��˂��B");

                            td.MainMessage = "�@�y�@�I���E�����f�B�X�ւ̎����I�����Ă��������B�@�z";
                            td.FirstMessage = "�t���́w�_�X�̎��x�Ɋւ��āA�����m��Ȃ����H";
                            td.SecondMessage = "�t���̎����A�_���W�����U�����A����ȑ�����H";
                            td.StartPosition = FormStartPosition.CenterParent;
                            td.ShowDialog();
                            if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                            {
                                UpdateMainMessage("�A�C���F�t���́w�_�X�̎��x�Ɋւ��āA�����m��Ȃ����H");

                                UpdateMainMessage("�����f�B�X�F�m��˂��ȁB");

                                UpdateMainMessage("�A�C���F���ށB���ł��ǂ�����m���Ă鎖��");

                                UpdateMainMessage("�����f�B�X�F�S�`���S�`���Ƃ��邹���B�A��B");
                                we.Truth_CommunicationOl22Fail = true;
                            }
                            else
                            {
                                UpdateMainMessage("�A�C���F�t���̎����A�_���W�����U�����A����ȑ�����H");

                                UpdateMainMessage("�����f�B�X�F�����B");

                                UpdateMainMessage("�A�C���F�ǂ�ȓ��e�������񂾁H");

                                UpdateMainMessage("�����f�B�X�F�Ă߂��ɂ͊֌W�˂��B");

                                UpdateMainMessage("�A�C���F�����Ă���Ă��ǂ�����H");

                                UpdateMainMessage("�����f�B�X�F�����Ă��Ӗ����˂��B");

                                td.MainMessage = "�@�y�@�I���E�����f�B�X�ւ̎����I�����Ă��������B�@�z";
                                td.FirstMessage = "�Ӗ����˂����āE�E�E�ǂ������Ӗ����H";
                                td.SecondMessage = "�Ӗ����˂����ǂ����́A�����Ȃ��ᕪ����Ȃ�����H";
                                td.StartPosition = FormStartPosition.CenterParent;
                                td.ShowDialog();
                                if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                {
                                    UpdateMainMessage("�A�C���F�Ӗ����˂����āE�E�E�ǂ������Ӗ����H");

                                    UpdateMainMessage("�����f�B�X�F���t�ʂ肾�B���������ňӖ��͂˂��B");

                                    UpdateMainMessage("�A�C���F���ɂ͓��Ă͂܂�Ȃ��E�E�E���Ď����H");

                                    UpdateMainMessage("�����f�B�X�F�ǂ��������Ă邶��˂����B");

                                    td.MainMessage = "�@�y�@�I���E�����f�B�X�ւ̎����I�����Ă��������B�@�z";
                                    td.FirstMessage = "�܂�A����͉��́y�����z�Ɋ֌W���Ă���Ď����H";
                                    td.SecondMessage = "�܂�A����͉��́y�ߋ��z�Ɋ֌W���Ă���Ď����H";
                                    td.ShowDialog();
                                    if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        UpdateMainMessage("�A�C���F�܂�A����͉��́y�����z�Ɋ֌W���Ă���Ď����H");

                                        UpdateMainMessage("�����f�B�X�F������Ă߂��̓U�R�A�C�������Č����Ă񂾁B");

                                        UpdateMainMessage("�A�C���F�Ⴄ�̂���H���ނ���A�����Ă����H");

                                        UpdateMainMessage("�����f�B�X�F�S�`���S�`���Ƃ��邹���B�A��B");
                                        we.Truth_CommunicationOl22Fail = true;
                                    }
                                    else
                                    {
                                        UpdateMainMessage("�A�C���F�܂�A����͉��́y�ߋ��z�Ɋ֌W���Ă���Ď����H");

                                        UpdateMainMessage("�����f�B�X�F���Ƃ�����A�ǂ�����B");

                                        td.MainMessage = "�@�y�@�I���E�����f�B�X�ւ̎����I�����Ă��������B�@�z";
                                        td.FirstMessage = "�ߋ�����Ƃ��āA���𓱂��o�����Ď����H";
                                        td.SecondMessage = "�ߋ���R�����āA��������������Ď����H";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            GroundOne.StopDungeonMusic();

                                            UpdateMainMessage("�A�C���F�ߋ�����Ƃ��āA���𓱂��o�����Ď����H");

                                            UpdateMainMessage("�����f�B�X�F�����ȁB");

                                            UpdateMainMessage("�A�C���F�ǂ��Ȃ񂾂�H");

                                            UpdateMainMessage("�����f�B�X�F�����ōl����B");

                                            UpdateMainMessage("�A�C���F�����E�E�E");

                                            UpdateMainMessage("�����f�B�X�F���������A�܂Ƃ��ɂȂ��Ă�������˂����B");

                                            UpdateMainMessage("�A�C���F�E�E�E���H");

                                            UpdateMainMessage("�����f�B�X�F���x�́A���������₤�B");

                                            UpdateMainMessage("�����f�B�X�F������B");

                                            UpdateMainMessage("�A�C���F���A�����I");

                                            UpdateMainMessage("", true);

                                            we.Truth_CommunicationOl22Progress1 = true;
                                            firstQuestion = true;
                                            // ����
                                        }
                                        else
                                        {
                                            UpdateMainMessage("�A�C���F�ߋ���R�����āA��������������Ď����H");

                                            UpdateMainMessage("�����f�B�X�F�����Ȃ�Ă���͂˂��B");

                                            UpdateMainMessage("�A�C���F���Ⴀ�A�ߋ�������̑���̌��Ƃǂ��֌W���Ă�񂾂�H");

                                            UpdateMainMessage("�����f�B�X�F�Ă߂��A���𕷂��ɂ����H");

                                            UpdateMainMessage("�A�C���F�����E�E�E");

                                            UpdateMainMessage("�����f�B�X�F�b�ɂȂ�˂��ȁB");

                                            UpdateMainMessage("�����f�B�X�F�A��A�Ă߂��ɋ����邱�Ƃ͂˂��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                }
                                else
                                {
                                    UpdateMainMessage("�A�C���F�Ӗ����˂����ǂ����́A�����Ȃ��ᕪ����Ȃ�����H");

                                    UpdateMainMessage("�����f�B�X�F�b�`�E�E�E�b�ɂȂ�˂��B");

                                    UpdateMainMessage("�A�C���F���܁A�҂��Ă���I�I");

                                    UpdateMainMessage("�����f�B�X�F�Ӗ����˂����́A�����ɕ����Ăǂ��Ȃ�H");

                                    UpdateMainMessage("�A�C���F�����E�E�E");

                                    UpdateMainMessage("�����f�B�X�F�A��A�Ă߂��ɋ����邱�Ƃ͂˂��B");
                                    we.Truth_CommunicationOl22Fail = true;
                                }
                            }
                        }
                        else
                        {
                            UpdateMainMessage("�A�C���F���̃_���W�����A�ǂ�����Ή�����񂾁H");

                            UpdateMainMessage("�����f�B�X�F�m��˂��ȁB�����ŒT���B");
                            we.Truth_CommunicationOl22Fail = true;
                        }
                    }

                    if (!we.Truth_CommunicationOl22Progress1)
                    {
                        GroundOne.StopDungeonMusic(); 
                        GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
                        return;
                    } // �������ĂȂ��ꍇ�A���̎��_�ň�U�ݖ�I��


                    bool secondQuestion = we.Truth_CommunicationOl22Progress2;

                    if (!secondQuestion)
                    {
                        GroundOne.StopDungeonMusic();
                        GroundOne.PlayDungeonMusic(Database.BGM16, Database.BGM16LoopBegin);

                        td.MainMessage = "�@�y�@�Ă߂��A���ŉ��l�̏��ɗ���C�ɂȂ����H�@�z";
                        td.FirstMessage = "���i�Ƒ��k�������ʁA�t���ɕ��������Ď��ŁB";
                        td.SecondMessage = "����̉񓚂�������A�s�v�c�Ƃ������������炾�B";
                        td.ShowDialog();
                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                        {
                            // Fail
                            td.MainMessage = "�@�y�@�����𕷂��Ă񂶂�˂��B�Ă߂��͂ǂ��Ȃ񂾁H�@�z";
                            td.FirstMessage = "����̓N���A�����B�����A���ȂЂ���������o�����B";
                            td.SecondMessage = "�ǂ����āE�E�E���ɂǂ����Ă킯����Ȃ����E�E�E";
                            td.ShowDialog();
                            if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                            {
                                // Fail
                                td.MainMessage = "�@�y�@�����������������̂��A�c���͂��Ă�̂��H�@�z";
                                td.FirstMessage = "�c���͂ł��Ă˂����A����Ȃ�̈�a���́E�E�E";
                                td.SecondMessage = "����E�E�E���ꂪ���Ȃ̂��͂킩��˂��E�E�E";
                                td.ShowDialog();
                                if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                {
                                    // Fail
                                    td.MainMessage = "�@�y�@�͂����肵�˂��ȁB�h�b�`�Ȃ񂾁H�@�z";
                                    td.FirstMessage = "���A���܂˂��E�E�E";
                                    td.SecondMessage = "����͉������B�ł��܂����ꂪ�����Ȃ��񂾁B";
                                    td.ShowDialog();
                                    if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        // Fail
                                        td.MainMessage = "�@�y�@�Ōゾ�B���ŁA�_���W��������ł�H�@�z";
                                        td.FirstMessage = "�����A����́E�E�E";
                                        td.SecondMessage = "�_���W�����ŉ҂��Ȃ�����Ȃ�˂��B���ꂾ����";
                                        td.ShowDialog();
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("�����f�B�X�F�ʖڂ��B�b�ɂȂ�˂��A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("�����f�B�X�F�b�`�A������e���F�͑ʖڂ������Ă񂾁A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                    else
                                    {
                                        // Fail
                                        td.MainMessage = "�@�y�@������Ă͉̂����w���Č����Ă�H�@�z";
                                        td.FirstMessage = "������Ă̂́A�܂�E�E�E";
                                        td.SecondMessage = "����Ȃ́A���ɂ����Ă킩��˂���B";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("�����f�B�X�F�ʖڂ��B�b�ɂȂ�˂��A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("�����f�B�X�F�b�`�A������e���F�͑ʖڂ������Ă񂾁A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                }
                                else
                                {
                                    // Fail
                                    td.MainMessage = "�@�y�@�ߋ��Ƒ���̊֌W���炢�͕������Ă񂾂낤�ȁH�@�z";
                                    td.FirstMessage = "�������A�킩���Ă邳�I";
                                    td.SecondMessage = "�b�O�E�E�E";
                                    td.ShowDialog();
                                    if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        // Fail
                                        td.MainMessage = "�@�y�@���Ⴀ�A�����Ă݂�B�@�z";
                                        td.FirstMessage = "���A����́E�E�E";
                                        td.SecondMessage = "�ߋ��̏o����������ł̐ݖ�ɂȂ�B��������H";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("�����f�B�X�F�ʖڂ��B�b�ɂȂ�˂��A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("�����f�B�X�F�b�`�A������e���F�͑ʖڂ������Ă񂾁A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                    else
                                    {
                                        // Fail
                                        td.MainMessage = "�@�y�@�_���W�����̍U�����@�A�c���x�����͂ǂ��Ȃ񂾁H�@�z";
                                        td.FirstMessage = "����Ȃ�ɁA�T�����Ă邵�������Ă���肾���B";
                                        td.SecondMessage = "���E�E�E�������炢�Ȃ�E�E�E";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("�����f�B�X�F�b�`�A������e���F�͑ʖڂ������Ă񂾁A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("�����f�B�X�F�ʖڂ��B�b�ɂȂ�˂��A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // Fail
                                td.MainMessage = "�@�y�@�_���W�����̍U����͂ǂ��Ȃ񂾁H�@�z";
                                td.FirstMessage = "�������B�؂�Ȃ��i��ł�B";
                                td.SecondMessage = "�������ă��P����˂��E�E�E";
                                td.ShowDialog();
                                if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                {
                                    // Fail
                                    td.MainMessage = "�@�y�@�U���̈Ӗ��͕������Ă񂾂낤�ȁH�@�z";
                                    td.FirstMessage = "�U���́E�E�E�Ӗ��H";
                                    td.SecondMessage = "�ŉ��w�֐i�߂邽�߂̓�������B��������H";
                                    td.ShowDialog();
                                    if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        // Fail
                                        td.MainMessage = "�@�y�@�_���W�����̍U���x�������B���܂��Ă邾��H�@�z";
                                        td.FirstMessage = "�����A����Ȃ�y�������B";
                                        td.SecondMessage = "������N���A�����������E�E�E";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("�����f�B�X�F�b�`�A������e���F�͑ʖڂ������Ă񂾁A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("�����f�B�X�F�ʖڂ��B�b�ɂȂ�˂��A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                    else
                                    {
                                        // Fail
                                        td.MainMessage = "�@�y�@�ŉ��w�H�@��H�@�������Ă񂾃e���F�́B�@�z";
                                        td.FirstMessage = "�����H���A�Ⴄ�̂���H";
                                        td.SecondMessage = "���H�@���ƁE�E�E";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("�����f�B�X�F�b�`�A������e���F�͑ʖڂ������Ă񂾁A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("�����f�B�X�F�ʖڂ��B�b�ɂȂ�˂��A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                }
                                else
                                {
                                    // Fail
                                    td.MainMessage = "�@�y�@����̂ǂ����C�ɂȂ����H�����Ă݂�B�@�z";
                                    td.FirstMessage = "���̓��e�͉ߋ��ɕ�������������B�������E�E�E";
                                    td.SecondMessage = "�Ŕ̑O�ɁA�ˑR�o�Ă������Ď����炢���ȁB";
                                    td.ShowDialog();
                                    if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        // Fail
                                        td.MainMessage = "�@�y�@�n�b�L�����˂��ȁB�킩��˂��̂��H�@�z";
                                        td.FirstMessage = "���A�����E�E�E";
                                        td.SecondMessage = "�ߋ��Ƃ̌��ʂ����邽�߂ɁI���Ď�����H";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("�����f�B�X�F�ʖڂ��B�b�ɂȂ�˂��A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("�����f�B�X�F�b�`�A������e���F�͑ʖڂ������Ă񂾁A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                    else
                                    {
                                        // Fail
                                        td.MainMessage = "�@�y�@���ꂪ�ǂ������H�@�z";
                                        td.FirstMessage = "���A������E�E�E";
                                        td.SecondMessage = "�E�E�E�@�E�E�E";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("�����f�B�X�F�ʖڂ��B�b�ɂȂ�˂��A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("�����f�B�X�F�ʖڂ��B�b�ɂȂ�˂��A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            //Sucess
                            td.MainMessage = "�@�y�@������N���A�����Ӗ��͂킩���Ă邩�H�@�z";
                            td.FirstMessage = "����A�܂��f�ГI�Ȏ������A�킩��˂��B";
                            td.SecondMessage = "�����A���R�������Ă��邳�I";
                            td.ShowDialog();
                            if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                            {
                                // Success
                                td.MainMessage = "�@�y�@����ǂ�����Đi�߂Ă����肾�H�@�z";
                                td.FirstMessage = "���ւ̊K�i��T���o���A�ŉ��w��ڎw���܂ł��B";
                                td.SecondMessage = "�_���W�����������܂Ȃ��T�����Ȃ���i�߂邳�B";
                                td.ShowDialog();
                                if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                {
                                    // Fail
                                    td.MainMessage = "�@�y�@�ŉ��w�܂ōs�����Ƃ��āA�ǂ�������肾�H�@�z";
                                    td.FirstMessage = "�ǂ����āE�E�E�����Ƌ����Ȃ��Ă�邳�B";
                                    td.SecondMessage = "�ǂ����āE�E�E";
                                    td.ShowDialog();
                                    if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        // Fail
                                        td.MainMessage = "�@�y�@�����Ȃ�����͂ǂ����邩�𕷂��Ă񂾁B�@�z";
                                        td.FirstMessage = "���̌�́E�E�E���́E�E�E";
                                        td.SecondMessage = "������Ηǂ��񂾂�H���ꂪ�t���̋�������˂����B";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("�����f�B�X�F�ʖڂ��B�b�ɂȂ�˂��A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("�����f�B�X�F�b�`�A������e���F�͑ʖڂ������Ă񂾁A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                    else
                                    {
                                        // Fail
                                        td.MainMessage = "�@�y�@�Ōゾ�B���ŁA�_���W��������ł�H�@�z";
                                        td.FirstMessage = "�����A����́E�E�E";
                                        td.SecondMessage = "�_���W�������e���̂��̂��ړI���A����ȏ�̈Ӗ��͖����B";
                                        td.ShowDialog();
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("�����f�B�X�F�ʖڂ��B�b�ɂȂ�˂��A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("�����f�B�X�F�b�`�A������e���F�͑ʖڂ������Ă񂾁A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                }
                                else
                                {
                                    // Success
                                    td.MainMessage = "�@�y�@�T�����āA�_���W�����̎d�|���͔c�������̂��H�@�z";
                                    td.FirstMessage = "����Ȃ�A�����ƌ��������B�N���A�������B";
                                    td.SecondMessage = "����̈ꕔ���炢�����E�E�E�S�̑��͂܂����Ƃ��E�E�E";
                                    td.ShowDialog();
                                    if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        // Fail
                                        td.MainMessage = "�@�y�@�Ă߂��̂��]�݂��Ă̂́A����������̂���H�@�z";
                                        td.FirstMessage = "���C���̎d�|�����������B�T���Ƃ��Ă͐�������H";
                                        td.SecondMessage = "����E�E�E���������킯����E�E�E";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("�����f�B�X�F�b�`�A������e���F�͑ʖڂ������Ă񂾁A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("�����f�B�X�F�ʖڂ��B�b�ɂȂ�˂��A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }

                                    }
                                    else
                                    {
                                        // Success
                                        td.MainMessage = "�@�y�@�Ōゾ�B�ǂ����ă_���W�����֒��ދC�ɂȂ����H�@�z";
                                        td.FirstMessage = "�r���������������B���ꂾ�����B";
                                        td.SecondMessage = "�E�E�E�@�E�E�E�@";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            // Fail
                                            UpdateMainMessage("�����f�B�X�F�b�`�A������e���F�͑ʖڂ������Ă񂾁A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            GroundOne.StopDungeonMusic();

                                            // Success
                                            UpdateMainMessage("�����f�B�X�F�E�E�E�ق��B");
                                            we.Truth_CommunicationOl22Progress2 = true;
                                            secondQuestion = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // Fail
                                td.MainMessage = "�@�y�@���Ⴀ�A�����Ă݂�B�@�z";
                                td.FirstMessage = "����͉ߋ��Ɋ֘A���Ă�B�ߋ�����ɉ��𓱂��o���΂����B";
                                td.SecondMessage = "���̎������̃_���W�����ɂ�����ő�̌����B";
                                td.ShowDialog();
                                if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                {
                                    // Fail
                                    td.MainMessage = "�@�y�@�ǂ������o�������Ă񂾁H�����Ă݂�B�@�z";
                                    td.FirstMessage = "�ߋ��̏o�������v���o���A�_���W�����������[�g�𓱂��o���B";
                                    td.SecondMessage = "�ǁA�ǂ����āE�E�E";
                                    td.ShowDialog();
                                    if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        // Fail
                                        td.MainMessage = "�@�y�@�������ƁH�@�Ă߂��A�������̉����w�񂾁H�@�z";
                                        td.FirstMessage = "�����I���A���₢�₢��I�I";
                                        td.SecondMessage = "�B���Ȃ��ł����B�������[�g����񂾂�H";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("�����f�B�X�F�ʖڂ��B�b�ɂȂ�˂��A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("�����f�B�X�F�b�`�A������e���F�͑ʖڂ������Ă񂾁A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                    else
                                    {
                                        // Fail
                                        td.MainMessage = "�@�y�@�����邩�A�����Ȃ����B�n�b�L������B�@�z";
                                        td.FirstMessage = "���A���܂˂��E�E�E";
                                        td.SecondMessage = "�����ւƂȂ���L�[���[�h��T���Ηǂ��񂾁I";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("�����f�B�X�F�ʖڂ��B�b�ɂȂ�˂��A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("�����f�B�X�F�b�`�A������e���F�͑ʖڂ������Ă񂾁A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                }
                                else
                                {
                                    // Fail
                                    td.MainMessage = "�@�y�@�ő�̌��B�ǂ��Ŏg���񂾁H�@�z";
                                    td.FirstMessage = "�ǁA�ǂ����āE�E�E";
                                    td.SecondMessage = "�ŉ��w���B�Ō�Ŏg���񂾂�A���������̂́B";
                                    td.ShowDialog();
                                    if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        // Fail
                                        td.MainMessage = "�@�y�@�n�b�L�����˂��ȁB�킩��˂��̂��H�@�z";
                                        td.FirstMessage = "�ŉ��w���I";
                                        td.SecondMessage = "��ԍŏ����I";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("�����f�B�X�F�ʖڂ��B�b�ɂȂ�˂��A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("�����f�B�X�F�ʖڂ��B�b�ɂȂ�˂��A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                    else
                                    {
                                        // Fail
                                        td.MainMessage = "�@�y�@�Ōゾ�B�ǂ����ă_���W�����֒��ދC�ɂȂ����H�@�z";
                                        td.FirstMessage = "�t���́y���_�O���[�u�z�݂����ȃ��c�������~�������炳�B";
                                        td.SecondMessage = "�������A�ŉ��w���B�Ŏt���ɕ��Ԃ��߂��B";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("�����f�B�X�F�b�`�A������e���F�͑ʖڂ������Ă񂾁A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("�����f�B�X�F�b�`�A������e���F�͑ʖڂ������Ă񂾁A�A��B");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                }

                            }
                        }
                    }
                    if (!we.Truth_CommunicationOl22Progress2)
                    {
                        GroundOne.StopDungeonMusic();
                        GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
                        return;
                    } // �������ĂȂ��ꍇ�A���̎��_�ň�U�ݖ�I��

                    GroundOne.StopDungeonMusic();
                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                    if (!we.Truth_CommunicationOl22DuelFail)
                    {
                        UpdateMainMessage("�A�C���F�t���A���肢���ė��݂�����B");

                        UpdateMainMessage("�����f�B�X�F�����A�����Ă݂�B");

                        UpdateMainMessage("�A�C���F�t���A���̃_���W�����ꏏ�ɗ��Ă���B���ނ��I");

                        UpdateMainMessage("�����f�B�X�F�E�E�E");

                        UpdateMainMessage("�����f�B�X�F�����Ƙr�����Ă݂�B");

                        UpdateMainMessage("�A�C���F���H");

                        UpdateMainMessage("�����f�B�X�F�R");

                        UpdateMainMessage("�����f�B�X�F�Q");

                        UpdateMainMessage("�A�C���F������I�}�W����I�H");

                        UpdateMainMessage("�����f�B�X�F�P");

                        UpdateMainMessage("�A�C���F�b�N�E�E�E�����I�I");
                    }

                    bool result = BattleStart(Database.DUEL_OL_LANDIS, true);
                    if (result)
                    {
                        // �������ꍇ�A���̉�b��
                        GroundOne.WE2.WinOnceOlLandis = true;
                    }
                    else
                    {
                        if (we.Truth_CommunicationOl22DuelFailCount >= 3)
                        {
                            // ���������Ȃ̂ŁA���̂܂ܒʂ��B�������AWinOnceOlLandis�͂��Ȃ��B
                        }
                        else
                        {
                            // �������ꍇ�A�������g���C
                            UpdateMainMessage("�����f�B�X�F�A��A�Ă߂��ɋ����邱�Ƃ͂˂��B");

                            UpdateMainMessage("�A�C���F�b�O�E�E�E");

                            we.Truth_CommunicationOl22Fail = true;
                            we.Truth_CommunicationOl22DuelFail = true;
                            we.Truth_CommunicationOl22DuelFailCount++;
                            return;
                        }
                    }

                    GroundOne.StopDungeonMusic();
                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                    UpdateMainMessage("�@�@�y��t��F������̕��X�I�I�������ΐ�𒆎~���Ă��������I�I�@�z");

                    UpdateMainMessage("�A�C���F�b�E���E�E�E���x�E�E�E");

                    UpdateMainMessage("�@�@�y��t��F���Z����ł̏���ȑΐ�́A���[�����ւƂȂ��Ă���܂��B�@�z");

                    UpdateMainMessage("�����f�B�X�F�b�`�A�������������������āA�삿���B");

                    UpdateMainMessage("�@�@�y��t��F������A���O��ǂݏグ�܂��B�@�z");

                    UpdateMainMessage("�@�@�y��t��F�I���E�����f�B�X�l�@�z");

                    UpdateMainMessage("�@�@�y��t��F�A�C���E�E�H�[�����X�l�@�z");

                    UpdateMainMessage("�@�@�y��t��F�ǂݏグ��ꂽ�҂́A���Ƃ���DUEL����ɂP�s���������܂��B�z");

                    UpdateMainMessage("�A�C���F�b�Q�I�I�}�W����I�H");

                    UpdateMainMessage("�����f�B�X�F������񃋁[�����ȁB");

                    UpdateMainMessage("�@�@�y��t��F�������A�A�C���E�E�H�[�����X�l��DUEL�Q�����" + we.GameDay.ToString() + "���ȓ��̂��߁A���Ꮬ�O�Ƃ��܂��B�z");

                    UpdateMainMessage("�A�C���F�����������E�E�E�b�z�E�E�E");

                    UpdateMainMessage("�@�@�y��t��F���킹�āA�I���E�����f�B�X�l�ɂ͗ݐϔ��Ƃ��čX�ɂQ�s���������܂��B�z");

                    UpdateMainMessage("�����f�B�X�F����ɕt���Ƃ��B");

                    UpdateMainMessage("�@�@�y��t��F�Ȃ��A����������čs�����ꍇ�ADUEL����ɗݐϓI�Ȕs�k�������Z����܂��B�z");

                    UpdateMainMessage("�@�@�y��t��F���ꂮ������Z����ł̏����DUEL�͂��Ȃ��悤�A���肢�������܂��B�z");

                    UpdateMainMessage("�A�C���F�����A���������ȁA��t����B������͋C�������B");

                    UpdateMainMessage("�����f�B�X�F������ɋC���g���K�v�͂˂��B�U�R�A�C���B");

                    UpdateMainMessage("�A�C���F���ł���B�^�c���̎�t���񂾂�H�ʂɗǂ�����˂����B");

                    UpdateMainMessage("�����f�B�X�F�Ă߂��̂��������g�R�E�E�E");

                    UpdateMainMessage("�A�C���F��t���񂾂��Đl�Ԃ��B�ǂ�����H");

                    UpdateMainMessage("�����f�B�X�F���Â��Â���񂾂ȃe���F�́E�E�E�D���ɂ���B");

                    UpdateMainMessage("�A�C���F���ӂ��E�E�E�}�W�Ŕ�ꂽ���B");

                    UpdateMainMessage("�A�C���F�t���Ƃ��Ƃ����S�͂��E�E�E���������˂��E�E�E");

                    UpdateMainMessage("�����f�B�X�F�_���W�����B");

                    UpdateMainMessage("�A�C���F�����H");

                    UpdateMainMessage("�����f�B�X�F�s���Ă���Ă��ǂ��B");

                    UpdateMainMessage("�A�C���F���������I�I�I�@�}�W�ŁI�H�@������I�I�I");

                    UpdateMainMessage("�����f�B�X�F����������B");

                    UpdateMainMessage("�A�C���F���A�����B�����Ă���B");

                    UpdateMainMessage("�����f�B�X�F���i���O���B");

                    UpdateMainMessage("�A�C���F�E�E�E�@�����@�E�E�E");

                    UpdateMainMessage("�����f�B�X�F��k���B�^�Ɏ󂯂�ȃ{�P�B");

                    UpdateMainMessage("�A�C���F���ȁE�E�E������B���l�����܂�������˂����E�E�E");

                    UpdateMainMessage("�����f�B�X�F���i�Ə���������u���B");

                    UpdateMainMessage("�A�C���F���A���₢��A�������Ă�񂾂�B");

                    UpdateMainMessage("�A�C���F�������Ƃ������b�ɂȂ�Ȃ����x�̋����ł����E�E�E");

                    UpdateMainMessage("�����f�B�X�F���������Ă񂾁A�{�P�B");

                    UpdateMainMessage("�����f�B�X�F���̃_���W�����A�����Ă��񂾂�H");

                    UpdateMainMessage("�A�C���F���A�������R�I�@�ڎw���͍ŉ��w���B���I�I");

                    UpdateMainMessage("�����f�B�X�F�������狗����u���B");

                    UpdateMainMessage("�A�C���F����̂ǂ����_���W�����U���ɁE�E�E");

                    UpdateMainMessage("�����f�B�X�F�ȏゾ�B");

                    UpdateMainMessage("�����f�B�X�F�p�[�e�B�ɓ������ȏ�A���ʂ܂Œb���Ă��B");

                    UpdateMainMessage("�����f�B�X�F�o�債�Ƃ���A�U�R�A�C���B");

                    UpdateMainMessage("�A�C���F���A�����I�I");

                    UpdateMainMessage("�A�C���F�T���L���[�ȁI�@�t���I�I�@");
                    CallSomeMessageWithAnimation("�y�I���E�����f�B�X���p�[�e�B�ɉ����܂����B�z");

                    we.AvailableThirdCharacter = true;
                    we.Truth_CommunicationOl22 = true;

                    // �u�R�����g�v����݌v�Ō�҂R�l�ڂ����F���[�A�[�e�B�ŃZ�[�u���Ă��܂��Ă��邽�߁A
                    // �����ōĐݒ肵�Ȃ���΂Ȃ�Ȃ��Ȃ����B
                    tc.FullName = "�I���E�����f�B�X";
                    tc.Name = "�����f�B�X";
                    tc.Strength = Database.OL_LANDIS_FIRST_STRENGTH;
                    tc.Agility = Database.OL_LANDIS_FIRST_AGILITY;
                    tc.Intelligence = Database.OL_LANDIS_FIRST_INTELLIGENCE;
                    tc.Stamina = Database.OL_LANDIS_FIRST_STAMINA;
                    tc.Mind = Database.OL_LANDIS_FIRST_MIND;
                    tc.Level = 35;
                    tc.Exp = 0;
                    tc.BaseLife = 2080;
                    tc.CurrentLife = tc.MaxLife;
                    tc.BaseSkillPoint = 100;
                    tc.CurrentSkillPoint = 100;
                    //td.TC.Gold = 10; // [�x��]�F�S�[���h�̏����͕ʃN���X�ɂ���ׂ��ł��B
                    tc.BaseMana = 1290;
                    tc.CurrentMana = tc.MaxMana;
                    tc.MainWeapon = new ItemBackPack(Database.POOR_GOD_FIRE_GLOVE_REPLICA);
                    tc.MainArmor = new ItemBackPack(Database.COMMON_AURA_ARMOR);
                    tc.Accessory = new ItemBackPack(Database.COMMON_FATE_RING);
                    tc.Accessory2 = new ItemBackPack(Database.COMMON_LOYAL_RING);
                    tc.BattleActionCommand1 = Database.ATTACK_EN;
                    tc.BattleActionCommand2 = Database.DEFENSE_EN;
                    tc.BattleActionCommand3 = Database.STRAIGHT_SMASH;
                    tc.BattleActionCommand4 = Database.VOLCANIC_WAVE;
                    tc.BattleActionCommand5 = Database.LIFE_TAP;
                    tc.BattleActionCommand6 = Database.SEVENTH_MAGIC;
                    tc.BattleActionCommand7 = Database.ONE_IMMUNITY;

                    tc.AvailableMana = true;
                    tc.AvailableSkill = true;
                    tc.StraightSmash = true;
                    tc.FireBall = true;
                    tc.DarkBlast = true;
                    tc.DoubleSlash = true;
                    tc.ShadowPact = true;
                    tc.FlameAura = true;
                    tc.StanceOfStanding = true;
                    tc.DispelMagic = true;
                    tc.LifeTap = true;
                    tc.HeatBoost = true;
                    tc.Negate = true;
                    tc.BlackContract = true;
                    tc.InnerInspiration = true;
                    tc.RiseOfImage = true;
                    tc.Deflection = true;
                    tc.FlameStrike = true;
                    tc.Tranquility = true;
                    tc.VoidExtraction = true;
                    tc.BlackFire = true;
                    tc.Immolate = true;
                    tc.DarkenField = true;
                    tc.DevouringPlague = true;
                    tc.VolcanicWave = true;
                    tc.OneImmunity = true;
                    tc.CircleSlash = true;
                    tc.OuterInspiration = true;
                    tc.SmoothingMove = true;
                    tc.WordOfMalice = true;
                    tc.EnrageBlast = true;
                    tc.SwiftStep = true;
                    tc.Recover = true;
                    tc.SurpriseAttack = true;
                    tc.SeventhMagic = true;
                }

                GroundOne.StopDungeonMusic();
                GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                return;

            }
            #endregion

            #region "�����ɉ����āADuel�����{���܂��B"
            else
            {
                string Opponent = WhoisDuelPlayer();
                if (Opponent != String.Empty && we.AlreadyRest)
                {
                    we.AlreadyDuelComplete = true;

                    DuelSupportMessage(SupportType.FromDuelGate, Opponent);

                    CallDuel(Opponent, false);
                    return;
                }
                else
                {
                    // �ΐ푊�肪���Ȃ��ꍇ�A���������Ȃ��܂܉��֍s���B
                }
            }
            #endregion

            GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);
            UpdateMainMessage("�A�C���F������A�ΐ푊��ł��m�F���Ă��������B", true);
            using (TruthDuelSelect tds = new TruthDuelSelect())
            {
                tds.StartPosition = FormStartPosition.Manual;
                tds.Location = new Point(this.Location.X + 330, this.Location.Y + 30);
                tds.MC = this.mc;
                tds.SC = this.sc;
                tds.TC = this.tc;
                tds.WE = this.we;
                tds.ShowDialog();
            }
            GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
        }


        private void CallRestInn()
        {
            CallRestInn(false);
        }
        private void CallRestInn(bool noAction)
        {
            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);

            if (noAction == false)
            {
                GroundOne.PlaySoundEffect("RestInn.mp3");
                using (MessageDisplay md = new MessageDisplay())
                {
                    md.Message = "�x�����Ƃ�܂���";
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.ShowDialog();
                }
            }

            we.AlreadyRest = true;
            // [�x��]�F�I�u�W�F�N�g�̎Q�Ƃ��S�Ă̏ꍇ�A�N���X�Ƀ��\�b�h��p�ӂ��Ă�����R�[���������������B
            if (mc != null)
            {
                mc.CurrentLife = mc.MaxLife;
                mc.CurrentSkillPoint = mc.MaxSkillPoint;
                mc.CurrentMana = mc.MaxMana;
                mc.AlreadyPlayArchetype = false;
            }
            if (sc != null)
            {
                sc.CurrentLife = sc.MaxLife;
                sc.CurrentSkillPoint = sc.MaxSkillPoint;
                sc.CurrentMana = sc.MaxMana;
                sc.AlreadyPlayArchetype = false;
            }
            if (tc != null)
            {
                tc.CurrentLife = tc.MaxLife;
                tc.CurrentSkillPoint = tc.MaxSkillPoint;
                tc.CurrentMana = tc.MaxMana;
                tc.AlreadyPlayArchetype = false;
            }
            we.AlreadyUseSyperSaintWater = false;
            we.AlreadyUseRevivePotion = false;
            we.AlreadyUsePureWater = false;
            we.AlreadyGetOneDayItem = false;
            we.AlreadyGetMonsterHunt = false;
            we.AlreadyDuelComplete = false;

            this.we.GameDay += 1;
            dayLabel.Text = we.GameDay.ToString() + "����";

            we.AlreadyCommunicateFazilCastle = false;

            if (noAction == false)
            {
                if (WhoisDuelPlayer() != String.Empty)
                {
                    DuelSupportMessage(SupportType.Begin, WhoisDuelPlayer());
                }
            }
        }

        private void CallEquipmentShop()
        {
            using (TruthEquipmentShop ES = new TruthEquipmentShop())
            {
                ES.StartPosition = FormStartPosition.CenterParent;
                ES.MC = this.mc;
                ES.SC = this.sc;
                ES.TC = this.tc;
                ES.WE = this.we;
                ES.ShowDialog();
            }
        }

        private void CallPotionShop()
        {
            if (we.TruthCompleteArea1) we.AvailablePotion2 = true;
            if (we.TruthCompleteArea2) we.AvailablePotion3 = true;
            if (we.TruthCompleteArea3) we.AvailablePotion4 = true;
            if (we.TruthCompleteArea4) we.AvailablePotion5 = true;

            using (TruthPotionShop PS = new TruthPotionShop())
            {
                PS.StartPosition = FormStartPosition.CenterParent;
                PS.MC = this.mc;
                PS.SC = this.sc;
                PS.TC = this.tc;
                PS.WE = this.we;
                PS.ShowDialog();
            }
        }

        private void CallItemBank()
        {
            using (TruthItemBank tib = new TruthItemBank())
            {
                tib.StartPosition = FormStartPosition.CenterParent;
                tib.MC = this.mc;
                tib.SC = this.sc;
                tib.TC = this.tc;
                tib.WE = this.we;
                tib.ShowDialog();
            }
        }

        private void CallFazilCastle()
        {
            we.AlreadyCommunicateFazilCastle = true;

            this.buttonHanna.Visible = false;
            this.buttonDungeon.Visible = false;
            this.buttonRana.Visible = false;
            this.buttonGanz.Visible = false;
            this.buttonPotion.Visible = false;
            this.buttonDuel.Visible = false;
            this.buttonShinikia.Visible = false;
            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_FAZIL_CASTLE);

            GroundOne.StopDungeonMusic();
            GroundOne.PlayDungeonMusic(Database.BGM13, Database.BGM13LoopBegin);

            #region "���߂Ă̖K��"
            if (!we.Truth_Communication_FC31)
            {
                we.Truth_Communication_FC31 = true;
                UpdateMainMessage("�A�C���F���Ƃ��A�t�@�[�W���{�a�������ƁB");

                UpdateMainMessage("���i�F�A�C���A��������āI�@�������");

                UpdateMainMessage("�A�C���F��H�ǂ�ǂ�H");

                UpdateMainMessage("�A�C���F���A��������I�@�񂾂����I�I");

                UpdateMainMessage("�w�@�@�{�a�O�̏��Q�[�g�ɂ́A��ʎs�����s��𐶐����Ă���@�@�@�x");

                UpdateMainMessage("�A�C���F���������A����ȕ���ŁA��̂Ȃɂ�����񂾂�I�H");

                UpdateMainMessage("���i�F�t�@�[�W���{�a�����̃��A�����k�s�񂶂�Ȃ��A�m��Ȃ��́H");

                UpdateMainMessage("�A�C���F�Ȃ񂾂���A�m��킯���������낤�B");

                UpdateMainMessage("�A�C���F�ŁA���ǉ��ŕ���ł�񂾁H�@�����Ă����B");

                UpdateMainMessage("���i�F���A������ƃz���g�m��Ȃ��킯�H�n�A�@�@�@�E�E�E�܂��ǂ�����");

                UpdateMainMessage("���i�F�t�@�[�W���{�a�ł̓G���~��������уt�@�����܂����̐��ɒ��ڎ����X����悤�ɂ��Ă���̂�B");

                UpdateMainMessage("�A�C���F����ŁA���̍s�񂾂��Ă̂��I�H�@��̂ǂ񂾂������Ă񂾂�I�H");
            
                UpdateMainMessage("���i�F������7:00�`12:00�B������12:30�`18:00�A�Ō��18:30�`22:00�܂ł̎O���\���ˁB");

                UpdateMainMessage("�A�C���F�I�C�I�C�I�C�A������Ƒ҂Ă�I�I�@�قƂ�ǋx�݂˂�����˂����I�I");

                UpdateMainMessage("���i�F���ꂾ���A���̎���O���ɒu���Ă�����Ď���ˁB�����R���͐^���ł��Ȃ���B");

                UpdateMainMessage("�A�C���F�͂����E�E�E�}�W����E�E�E�����������S����΂��肾�ȁB");

                UpdateMainMessage("�A�C���F���Ăǂ����񂾂�H����Ȃ̕���ł�����L�����������B");

                UpdateMainMessage("���i�F���v��B���ԂɊւ��Ă͊��S�ɗ\�񐧂Ȃ́B�z�������ɋL�����X�g������ł����");

                UpdateMainMessage("�A�C���F��H�������������̂�����̂��B���������Ă����B");

                UpdateMainMessage("�A�C���F�������ƁE�E�E�L���������B");

                UpdateMainMessage("���i�F���̗ʂ��Ƃ����ˁE�E�E�����̒����ɍs���Ƃ�����ˁB");

                UpdateMainMessage("�A�C���F�ւ��A�悭����Ȑ��m�ɕ�����ȁH");

                UpdateMainMessage("���i�F������O����Ȃ��B�����\�̂̍��A�����ɒʂ����炢�s���Ă��񂾂����");

                UpdateMainMessage("�A�C���F�b�Q�A�}�W����I�H");
                
                UpdateMainMessage("�A�C���F�������A�����C�ɓ����Ă񂾂ȁA�G���~���̎��E�E�E");

                UpdateMainMessage("���i�F�b�v�E�E�E�b�v�v");
                
                UpdateMainMessage("���i�F���`�I�J�V�C�A�t�t�t��");

                UpdateMainMessage("�A�C���F���ȁA�������������H");

                UpdateMainMessage("���i�F�t�t�t�A�Ȃ�ł���������@�����A����͂����܂łˁA��U�A��܂����");

                UpdateMainMessage("�A�C���F����ȃI�J�V�����e���������E�E�E");

                UpdateMainMessage("�A�C���F�܂������A�m���ɂ���ȏ��邱�Ƃ��˂��B�߂�Ƃ��邩�B");
            }
            #endregion
            #region "�y���J�n"
            else if (!we.Truth_Communication_FC32)
            {
                we.Truth_Communication_FC32 = true;

                UpdateMainMessage("�A�C���F���āA���������B");

                UpdateMainMessage("�A�C���F�����Ɨ\�񏇂͂ǂ�ǂ�E�E�E");

                UpdateMainMessage("�A�C���F�����A�{�����B�㏭���ŉ��B�̔Ԃ��ȁB");

                UpdateMainMessage("���i�F���ł����");

                UpdateMainMessage("�A�C���F�ł��A���S�\�񐧂Ȃ炱���܂ł��ĕ��ԕK�v�͂˂��񂶂�˂��̂��H");

                UpdateMainMessage("���i�F�\�񏇏��Ŏ����̏��Ԃ��������A�Y���̐l�����Ȃ������ꍇ�́A������ł�l�����荞�݂ŉy�����鎖��������Ă�̂�B");

                UpdateMainMessage("�A�C���F�Ȃ�قǁA���Ⴀ�d�v�ȗv�����������ł�l�́A����ł�������y���܂ł̎��Ԃ��Z�k�����ꍇ��������Ď����B");

                UpdateMainMessage("���i�F�����ˁB���ƁA���荞�݂��P�O���[�v���邩��A���̕������\�񎞊ԑт��啝�ɃY���鎖���Ȃ��Ȃ�킯��B");

                UpdateMainMessage("�A�C���F�����܂Ōv�Z���Ẵ��[�����Ă킯���E�E�E�z���g�X�S�������ȁE�E�E");

                UpdateMainMessage("�@�@�y�߉q���F�A�C���E�E�H�[�����X�I�@�A�C���E�E�H�[�����X�͂��̏�ɋ��邩�I�I�z");

                UpdateMainMessage("�A�C���F�����ƁA�Ă΂ꂽ�݂������B�s���Ȃ�����ȁI");

                UpdateMainMessage("�A�C���F�q���̃I�b�T���I�����I���I�@���������ɍs�����I");

                UpdateMainMessage("�@�@�y�߉q���F�����A���܂ɑ΂��A����̖����悤�őP�̐S���������ĉy���ɖ]�܂ꂽ���I�I�z");

                UpdateMainMessage("�A�C���F������A���𗹉��I�I");

                UpdateMainMessage("�A�C���F���Ⴀ�s�������A���i�B");

                UpdateMainMessage("���i�F�����A�y���݂�ˁ�");

                UpdateMainMessage("�@�@�@�w�@�y���̊ԂɂāE�E�E�@�x");

                UpdateMainMessage("�A�C���F�ւ��E�E�E�ӊO�ƕ��ʂ̕������ȁB�����ƍ��؈�ࣂȃg�R���Ǝv�������B");

                UpdateMainMessage("���i�F���Ƃ̐e�ߊ��𓾂邽�߁A�Ӑ}�I�ɂ��̕����̕��͋C������Ă�̂�B");

                UpdateMainMessage("�A�C���F�}�W����E�E�E�����܂ł���̂��B");

                UpdateMainMessage("���i�F�����A�z��������I�@���Â��ɁI");

                UpdateMainMessage("�A�C���F�E�E�E�i�h�L�h�L�E�E�E�j");

                UpdateMainMessage("�����G���~�F�A�C���E�E�H�[�����X�ƃ��i���񂾂ˁB��낵���B");

                UpdateMainMessage("���܃t�@���F�G�����A�̍g��������Ă�������B�ǂ���΂ǂ����B");

                UpdateMainMessage("���i�F���A���肪�Ƃ��������܂���@�����Ȃ���");

                UpdateMainMessage("���i�F�G���~�l�́A��������i�ƃJ�b�R�C�C�ł��ˁ�@���C�ł���Ă܂����H");

                UpdateMainMessage("�����G���~�F�n�n�n�A���i����͂�������Ȓ��q���ȁA���̂Ƃ��茳�C�ł���Ă��B");

                UpdateMainMessage("���i�F�t�@���l���A���[�z���g�������ł��B�������t�@���l���Q�l�ɂ��Ă��ł����");

                UpdateMainMessage("���܃t�@���F�E�t�t�A���肪�Ƃ��B");

                UpdateMainMessage("���i�F�����A�v���͂ł��ˁB�\�R�Ƀ{�[���Ɠ˂������Ă���o�J�A�C���������܂��̂ŕ����Ă���������");

                UpdateMainMessage("�A�C���F�E�E�E���ȁE�E�E");

                UpdateMainMessage("�A�C���F�Ȃ�ł���ȓ����b���ۂ��񂾂�I�H");

                UpdateMainMessage("�����G���~�F���Ɖ�b���鎞�́A���̒��q�Œ��������Ԉӌ��������o���₷������ˁB");

                UpdateMainMessage("���i�F�G���~�l�́A��ʓ����b�Ɋւ��Ă͏㋉�N���X�̎��i���K�����Ă�̂�B�z���g�������ˁB");

                UpdateMainMessage("�A�C���F�����E�E�E����Ȃ̂�����̂��E�E�E");

                UpdateMainMessage("�A�C���F���Ă��A����ς肠�ꂩ�B���d���Z���t��������ł����ē����b���ۂ����Ă�ƁE�E�E�H");

                UpdateMainMessage("�����G���~�F�܂��A����ȏ����ˁB�C�ɂ��Ȃ��ŗǂ���{���ɁB");

                UpdateMainMessage("���܃t�@���F�E�t�t�A�ł͗v�����ǂ����A�A�C������i�O�O�j");

                UpdateMainMessage("�A�C���F���A�����E�E�E���A���Ⴀ�����ƁE�E�E");

                UpdateMainMessage("���i�F�b�R���A������ƁI�H�@���ǂ��܂����Ă�̂�A�����B");

                UpdateMainMessage("���i�F�b�r�V���Ɨv���������Ȃ�����ˁB�X�p�X�p���ƁB");

                UpdateMainMessage("�A�C���F���A�����B���Ⴀ�A���߂āB");

                UpdateMainMessage("�A�C���F�v���͊ȒP���B");

                UpdateMainMessage("�A�C���F�����̈˗��͓����ĂȂ����H");

                UpdateMainMessage("�����G���~�F�����B���ꂪ�ǂ������񂾂��H");

                UpdateMainMessage("�A�C���F�o����΂�������B�ɔC���ė~�����B�ڍׂ������Ă���Ȃ����H");

                UpdateMainMessage("�����G���~�F�\��Ȃ���B����Ă�����Ȃ�A�劽�}���B");

                UpdateMainMessage("�����G���~�F��V�͉��ɂ��悤���B���ړI�Ȏ����ł��������H");

                UpdateMainMessage("�A�C���F�����A���ꂪ��ԏ�����B");

                UpdateMainMessage("�����G���~�F����ł́A�߉q���ɑ΂��āA�A�C���E�E�H�[�����X�̓����˗��\�����������F�߂鎖��`���Ă������B");

                UpdateMainMessage("���܃t�@���F�G���~�B���̌��Ȃ���ɁA�y���O�ɋ߉q���T���f�B�ɓ`���Ă����܂�����B");

                UpdateMainMessage("�����G���~�F�����ƁA���������΂����������ȁB�������t�@���B");

                UpdateMainMessage("�A�C���F���ȁI�I�H�@�Ȃ�ŕ������Ă��񂾂�I�H");

                UpdateMainMessage("���i�F�b�t�t�A��������ˁB�@������G���~�l�̓J�b�R�C�C�񂶂�Ȃ���");

                UpdateMainMessage("�A�C���F�����₢�₢��I�@���������Ӗ��Ō����g�R����I�H");

                UpdateMainMessage("�����G���~�F�y���̊Ԃ܂ŗ���Ƃ������ŁA�����͂قڌ����Ă���B");

                UpdateMainMessage("�����G���~�F�\��L�����Z���҂��̗�ɂ�����łȂ��悤�����؉H�l�܂������e�ł͂Ȃ��Ƃ����");

                UpdateMainMessage("�����G���~�F�G�k���A�������������A�܂��͎G���֘A�Ƃ����������A���������ڈ��͕t�����̂Ȃ񂾂�B");

                UpdateMainMessage("�����G���~�F�A�C���N�͗E�҉ʊ��Ȑ����B�@���ꎩ�̂͑O�X���玨�ɓ͂��Ă����B");

                UpdateMainMessage("�����G���~�F�ƂȂ�ƁB�@�����͕������ˁB");

                UpdateMainMessage("�A�C���F�E�E�E���₢�₢��E�E�E�������܂��E�E�E");

                UpdateMainMessage("���܃t�@���F�ł��ˁB�A�C������ƃ��i����ɗ��Ă��������ď����Ɋ�������ł���A�����G���~���i�O�O�j");

                UpdateMainMessage("�A�C���F���₠�E�E�E���₢�₢��A���������Ȃ����t���B���肪�Ƃ��������܂��B");

                UpdateMainMessage("���i�F�G���~�l�A�܂��V�тɗ��Ă������ł�����");

                UpdateMainMessage("�����G���~�F������񂾂�B����ȂƂ���ŗǂ���΁A���x�ł����Ă���č\��Ȃ���B");

                UpdateMainMessage("���܃t�@���F���҂����Ă܂��ˁi�O�O/�j");

                UpdateMainMessage("�A�C���F�����E�E�E�܂����܂��I�I�I");

                UpdateMainMessage("���i�F�z�[���A�����ŕ�����Ȃ��́I�@�z���b�g�ɂ����E�E�E");

                UpdateMainMessage("�A�C���F���Ⴀ�A�{���ɂ��肪�Ƃ��������܂����B���炵�܂��B");

                UpdateMainMessage("�����G���~�F�����A�܂��ˁB");

                UpdateMainMessage("�@�@�@�w�@���Q�[�g�O�ɂāE�E�E�@�x");

                UpdateMainMessage("�A�C���F�����ƁA�߉q���T���f�B����́E�E�E�ƁE�E�E");

                UpdateMainMessage("�@�@�y�߉q���F�A�C���E�E�H�[�����X�I�@�A�C���E�E�H�[�����X�͂��̏�ɋ��邩�I�I�z");

                UpdateMainMessage("�A�C���F������I�I�������ƁA�n�C�n�C�B���������ɍs�����B");

                UpdateMainMessage("�@�@�y�߉q���F�A�C���E�E�H�[�����X�ɒʒB����I�z");

                UpdateMainMessage("�@�@�y�߉q���F�����̎����A�A�C���E�E�H�[�����X�ɓ����˗��\���̎󗝂��s��������^���鎖�Ƃ���I�z");

                UpdateMainMessage("�@�@�y�߉q���F�����˗��̃��X�g�́A���̎��G�K���g�E�T���f�B���������Ă���I�I�z");

                UpdateMainMessage("�@�@�y�߉q���F���X�g���e����������΁A���̎��G�K���g�E�T���f�B��q�˂�Ƃ悢�I�I�z");

                UpdateMainMessage("�A�C���F�����A���A�������E�E�E���𗹉��I");

                UpdateMainMessage("�@�@�y�߉q���F�A�C���E�E�H�[�����X��I�@�\��������������Ή��Ȃ�ƕ������悢�I�I�z");

                UpdateMainMessage("�A�C���F�����E�E�E���Ⴀ�Ƃ肠�����A������B");

                UpdateMainMessage("�A�C���F�����ƁA������̓T���f�B���ČĂ�ł��ǂ����H");

                UpdateMainMessage("�A�C���F���[���߉q�����ČĂԂ̂����ƂȂ��ς����ȁB�\��Ȃ����H");

                UpdateMainMessage("�@�@�y�߉q���F���m���������I�z");

                UpdateMainMessage("�@�@�y�߉q���F����ł͈ȍ~�A���̎��̓T���f�B�ƌĂԂ��ǂ��I�I�z");

                UpdateMainMessage("�A�C���F���[���A�T���L���[�T���L���[�B���Ⴀ��낵���ȁI");

                UpdateMainMessage("���i�F������ƁA�ǂ������̃g�R�����񂾂��ǁA�̐S�̓����˗����X�g�͌��Ă����Ȃ��́H");

                UpdateMainMessage("�A�C���F��H�����A������厖�Ȃ񂾂��ǂȁB����͂ЂƂ܂��R�R�܂ł��Ď��ɂ����Ă���B�����ȁB");

                UpdateMainMessage("���i�F�ӂ���A�����Ȃ񂾁B�����A�o�J�A�C�����Ė{���ɕςȎ��������ˁB");

                UpdateMainMessage("�A�C���F�܂��܂��A��������˂����B�����������Ƃ�����V�̈���B");

                UpdateMainMessage("���i�F�E�E�E������ė�V�ɂȂ��Ă�킯�H");

                UpdateMainMessage("�A�C���F���Ⴀ�A���肪�ƂȁA�T���f�B�B���܂���ɗ��邩��A���񎞂ɓ������X�g�����Ă���I");

                UpdateMainMessage("�T���f�B�F�y���m���������I�z");
            }
            #endregion
            else if (!we.AvailableOneDayItem)
            {
                we.AvailableOneDayItem = true;

                UpdateMainMessage("�A�C���F���āA���������B");

                UpdateMainMessage("���i�F�����A�A�C�����Č��Ă������̕��ŉ����l�����肪�o���Ă���B");

                UpdateMainMessage("�A�C���F�����A�{�����B�Ȃ񂩂������̂��ȁH");

                UpdateMainMessage("���i�F������ƍs���Ă݂܂����");

                UpdateMainMessage("�T���f�B�F�y�F�̎҂ɓ`�ߎ���������I�z");

                UpdateMainMessage("�A�C���F�����A�T���f�B���B���������C�ɂ���Ă�ȁB");

                UpdateMainMessage("���i�F�b�t�t�A�����傫�����ˁA�T���f�B����B");

                UpdateMainMessage("�A�C���F�����A��������ł����������Ɏc�銴������ȁB");

                UpdateMainMessage("�T���f�B�F�y�{�����I�z");

                UpdateMainMessage("�T���f�B�F�y�t�@�[�W���{�a�ɕ������ہI�z");

                UpdateMainMessage("�T���f�B�F�y�{�a���ʃQ�[�g�O�̉��ʂ�ɂāI�z");

                UpdateMainMessage("�T���f�B�F�y�t�@�[�W����������A�S�Ă̖��ɑ΂��āI�z");

                UpdateMainMessage("�T���f�B�F�y���ӂƌh�ӂ̔O�����߁I�z");

                UpdateMainMessage("�T���f�B�F�y�����P�񂸂A���y���ݒ��I���𔭍s����I�I�I�z");

                UpdateMainMessage("�A�C���F�����A���y���ݒ��I���I�H");

                UpdateMainMessage("���i�F�Ȃ񂾂��A�ʔ������ˁ�");

                UpdateMainMessage("�T���f�B�F�y���I�œ�����A�C�e���͑e�i���獋�؏ܕi�܂ŗl�X�I�z");

                UpdateMainMessage("�T���f�B�F�y����Ƃ������p�����I�I�z");

                UpdateMainMessage("�A�C���F�}�W����B�����͊��������e���ȁB");

                UpdateMainMessage("�A�C���F���ۂɂ͂ǂ�ȏ��i��������񂾁H�ꗗ���X�g�Ƃ�����̂��ȁH");

                UpdateMainMessage("�T���f�B�F�y�Ȃ��A�S�Ă̖��ɉ����āA�Ώۏܕi�͒���X�V����A���A���̐��͖c��I�z");

                UpdateMainMessage("�T���f�B�F�y�䂦�ɁA�ܕi���X�g�����J���邱�Ƃ͏o���Ȃ��I�z");

                UpdateMainMessage("�T���f�B�F�y�ȂɂƂ��A���������������������I�z");

                UpdateMainMessage("�A�C���F�S�Ă̖��ɉ����āA������āE�E�E�������ȁE�E�E");

                UpdateMainMessage("���i�F�ǂ������d�g�݂Ȃ̂�����A�z�������Ȃ���ˁB");

                UpdateMainMessage("�A�C���F�܂��A����Ă݂Ă���̂��y���݂��ď����B");

                UpdateMainMessage("���i�F�A�C���A�����؏ܕi������������A�����Ǝ��ɒ��Ղ�ˁ�");

                UpdateMainMessage("�A�C���F�Q�b�E�E�E���A���ꂾ���́E�E�E");

                UpdateMainMessage("���i�F������܂ŁA�����o�V�o�V����Ē��Ձ�");

                UpdateMainMessage("�A�C���F���₢�₢��E�E�E");

                UpdateMainMessage("���i�F���܂��");

                UpdateMainMessage("�A�C���F�n�A�n�n�n�E�E�E");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "�t�@�[�W���{�a�Łu���y���ݒ��I���v���󂯎�鎖���\�ɂȂ�܂����I";
                    md.ShowDialog();
                }
                UpdateMainMessage("", true); 
            }
            #region "�����C�x���g�������ꍇ"
            else
            {
                UpdateMainMessage("�T���f�B�F�y�悭���Q��ꂽ�I�z", true);

                using (SelectDungeon sd = new SelectDungeon())
                {
                    sd.StartPosition = FormStartPosition.Manual;
                    sd.Location = new Point(this.Location.X + 50, this.Location.Y + 550);
                    sd.MaxSelectable = 3;
                    sd.FirstName = "���I��";
                    sd.SecondName = "����";
                    sd.ThirdName = "��������";
                    if (we.AvailableOneDayItem)
                    {
                        sd.ShowDialog();
                    }
                    else
                    {
                        sd.TargetDungeon = 2;
                    }
                    if (sd.TargetDungeon == 1)
                    {
                        if (!we.AlreadyGetOneDayItem)
                        {
                            UpdateMainMessage("�T���f�B�F�y���y���ݒ��I���͐��ʃQ�[�g�������ĉE���ł���I�z");

                            UpdateMainMessage("�A�C���F�T���L���[�B����s���Ă��邺�B");

                            UpdateMainMessage("�@�E�E�E�@���΂炭��������@�E�E�E");

                            if (!we.Truth_FirstOneDayItem)
                            {
                                UpdateMainMessage("�A�C���F�������A���̔��݂����Ȃ���B");

                                UpdateMainMessage("���i�F���A���ꂶ��Ȃ��́H");

                                UpdateMainMessage("�A�C���F���A�{�����I�@�ǂ�ǂ�E�E�E");
                            }
                            else
                            {
                                UpdateMainMessage("�A�C���F�悵�A�m�����̔��������ȁB");
                            }

                            UpdateMainMessage("�@�y�@���y���ݒ��I���������߂̕��́A�w���s�x�{�^���������Ă��������@�z");

                            UpdateMainMessage("�A�C���F���Ⴀ�s���ƁE�E�E");

                            UpdateMainMessage("�@�y�@�b�K�K�K�K�E�E�E�@�z");

                            UpdateMainMessage("�@�y�@���肪�Ƃ��������܂��B�����ɔ��s����܂����@�z");

                            if (!we.Truth_FirstOneDayItem)
                            {
                                UpdateMainMessage("�A�C���F���A�����I��������I");
                            }

                            UpdateMainMessage("�@�y�@���I���������āA���̂܂܉E�ւ��i�݂��������@�z");

                            if (!we.Truth_FirstOneDayItem)
                            {
                                UpdateMainMessage("�A�C���F������A�����ȁI");

                                UpdateMainMessage("���i�F�����Ƃ����B���l������ł��B");

                                UpdateMainMessage("�A�C���F�悵�A������������ł݂悤���B");

                                UpdateMainMessage("�A�C���F�E�E�E�Ȃ����ȁE�E�E");

                                UpdateMainMessage("���i�F�����҂����Ȃ���ˁB");

                                UpdateMainMessage("�A�C���F�ӂ��E�E�E");

                                UpdateMainMessage("���i�F�Ƃ���ŁA�ǂ����������g���́H");

                                UpdateMainMessage("�A�C���F����A����͂ǂ����ł��ǂ����낤�B");

                                UpdateMainMessage("���i�F���[�A�������Ă�̂�o�J�A�C���H�@�厖�ȃg�R����Ȃ��́B");

                                UpdateMainMessage("�A�C���F���₢�₢��A���I�Ȃ񂾂���A�N������Ă���������H");

                                UpdateMainMessage("���i�F�ł��A���^�̐l�����ƁA���đ����Ɉ������Ă�l���Ă��邶��Ȃ��H");

                                UpdateMainMessage("�A�C���F�m���ɂ��܂ɋ���悤�ȁA���������z�́B");

                                UpdateMainMessage("���i�F�ł���H������A�����A�C���̂ǂ������ŁA���ʂ��ς��킯���");

                                UpdateMainMessage("�A�C���F�}�W���E�E�E�֌W�˂��C�����邯�ǂȂ��E�E�E");

                                UpdateMainMessage("���i�F�����������P������A�ǂ����������g�������߂Ă��傤������");

                                UpdateMainMessage("�A�C���F���₢�₢��E�E�E�������Ȃ��E�E�E");

                                UpdateMainMessage("�A�C���F�E�E�E");

                                UpdateMainMessage("�A�C���F�_�����A�킩��˂��I");

                                UpdateMainMessage("�A�C���F�����g�p���钼�O�Ō��߂悤�I�I�I");

                                UpdateMainMessage("���i�F�����A���悻��B�@�����ƌ��߂Ă�ˁB");

                                UpdateMainMessage("�A�C���F���₢��A���Č����񂾁B���߂悤���������B");

                                UpdateMainMessage("�A�C���F���̎����̎��̒��ςɗ��낤�B���ȁI�H");

                                UpdateMainMessage("���i�F���[��A�����ߑR�Ƃ��Ȃ����ǁE�E�E");

                                UpdateMainMessage("�A�C���F�����A�O���J�������I�������̔Ԃ���Ȃ����H");

                                UpdateMainMessage("���i�F���A�{���ˁB���Ⴀ������������Ă݂܂����");

                                UpdateMainMessage("�@�y�@���I�����V�[�g�}�����ɍ�������ł��������@�z");

                                UpdateMainMessage("�A�C���F�悵�A���Ⴀ�������������E�E�E");

                                UpdateMainMessage("���i�F�ǂ���������Ă݂�H");
                            }
                            else
                            {
                                UpdateMainMessage("���i�F�˂��A�ǂ���������Ă݂�H");
                            }

                            UpdateMainMessage("�A�C���F�������Ȃ��A�����́E�E�E");

                            string newItem = String.Empty;
                            using (TruthDecision td = new TruthDecision())
                            {
                                td.MainMessage = "�ǂ��炪���I�����g���܂����H";
                                td.FirstMessage = "�A�C��";
                                td.SecondMessage = "���i";
                                td.StartPosition = FormStartPosition.CenterParent;
                                td.ShowDialog();
                                if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                {
                                    UpdateMainMessage("�A�C���F�����A������낤");

                                    UpdateMainMessage("���i�F�撣���Ăˁ�");

                                    UpdateMainMessage("�A�C���F�C���Ă����I");
                                    
                                    UpdateMainMessage("�@�y�@���I����F���������܂����B�@���΂炭���҂����������B�@�z");

                                    UpdateMainMessage("�A�C���F�����E�E�E�����I�I");
                                }
                                else
                                {
                                    UpdateMainMessage("�A�C���F���i�A�C�����B");

                                    UpdateMainMessage("���i�F���Ⴀ�A����Ă݂��ˁB");

                                    UpdateMainMessage("�@�y�@���I����F���������܂����B�@���΂炭���҂����������B�@�z");

                                    UpdateMainMessage("���i�F�܂��A����ȂɊ��҂͂��Ȃ����ǁE�E�E");
                                }
                            }

                            GroundOne.StopDungeonMusic();

                            UpdateMainMessage("�@�y�@���ʂ𔭕\���܂��@�z");

                            UpdateMainMessage("�@�y�@�ܕi�́E�E�E�@�z");

                            UpdateMainMessage("�@�y�@�E�E�E�@�z");

                            UpdateMainMessage("�@�y�@�E�E�E�@�z");

                            UpdateMainMessage("�@�y�@�E�E�E�@�z");

                            newItem = Method.GetNewItem(Method.NewItemCategory.Lottery, mc, null, 4);

                            GroundOne.PlaySoundEffect("LvUp.mp3");
                            UpdateMainMessage("�@�y�@��" + newItem + "����������܂����I�z");

                            GroundOne.PlayDungeonMusic(Database.BGM13, Database.BGM13LoopBegin);

                            UpdateMainMessage("�@�y�@�ܕi��]���������܂��̂ŁA�{�b�N�X����󂯎���Ă��������@�z");

                            UpdateMainMessage("�@�y�@�b�K�R���I�I�I�@�z");

                            UpdateMainMessage("�@�y�@�܂������p���������@�z");

                            if (!we.Truth_FirstOneDayItem)
                            {
                                UpdateMainMessage("�A�C���F�������E�E�E���̃f�b�p�������瑦�o�Ă���̂���B");

                                UpdateMainMessage("���i�F�ǂ������d�|���Ȃ̂�����B�S�A�C�e���������Ă�悤�ɂ��v���Ȃ����E�E�E");

                                UpdateMainMessage("�A�C���F�܂��A�ׂ����d�|���͋C�ɂ��Ȃ��ł������B�Ƃɂ�������Ă��������I");
                            }
                            else
                            {
                                UpdateMainMessage("�A�C���F������A����Ă������I");
                            }

                            CallSomeMessageWithAnimation(newItem + "����ɓ��ꂽ�B");

                            GetItemFullCheck(mc, newItem);

                            if (!we.Truth_FirstOneDayItem)
                            {
                                we.Truth_FirstOneDayItem = true;
                                UpdateMainMessage("�A�C���F�܂����x����Ă݂悤���B");

                                UpdateMainMessage("���i�F�����A�����ˁB");
                            }
                            we.AlreadyGetOneDayItem = true;
                        }
                        else
                        {
                            UpdateMainMessage("�T���f�B�F�y���y���ݒ��I���͖{�����ɔ��s�ςƂȂ����I�z");

                            UpdateMainMessage("�A�C���F�������A���Ⴀ�܂����x���ȁB");

                            UpdateMainMessage("�T���f�B�F�y�܂��A�Q����I�z");
                        }
                    }
                    else if (sd.TargetDungeon == 2)
                    {
                        UpdateMainMessage("�A�C���F�您�A�T���f�B�B�ǂ������瓢�����X�g�������Ă���Ȃ����H");

                        UpdateMainMessage("�T���f�B�F�y���܂ʂ��A�������X�g�͖�������Ă���ʁI�z");

                        UpdateMainMessage("�T���f�B�F�y�����΂炭�҂����I�z");

                        UpdateMainMessage("�A�C���F�E�Q�E�E�E���Ⴀ�A���傤���˂��A�߂邩�E�E�E");
                        we.AlreadyGetMonsterHunt = true;
                    }
                    else
                    {
                        UpdateMainMessage("�T���f�B�F�y�܂��A�Q����I�z", true);
                        System.Threading.Thread.Sleep(1000);
                    }
                }
            }
            #endregion

            if (!we.AlreadyRest)
            {
                ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_EVENING);
            }
            else
            {
                ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
            }
            this.buttonHanna.Visible = true;
            this.buttonDungeon.Visible = true;
            this.buttonRana.Visible = true;
            this.buttonGanz.Visible = true;
            this.buttonPotion.Visible = true;
            this.buttonDuel.Visible = true;
            this.buttonShinikia.Visible = true;

            GroundOne.StopDungeonMusic();
            GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
        }

        private void GetGreenPotionForLana()
        {
            string potionName = Database.POOR_SMALL_GREEN_POTION;

            if (!we.CompleteArea1 && !we.CompleteArea2 && !we.CompleteArea3 && !we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = Database.POOR_SMALL_GREEN_POTION;
            }
            else if (we.CompleteArea1 && !we.CompleteArea2 && !we.CompleteArea3 && !we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = Database.COMMON_NORMAL_GREEN_POTION;
            }
            else if (we.CompleteArea1 && we.CompleteArea2 && !we.CompleteArea3 && !we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = Database.COMMON_LARGE_GREEN_POTION;
            }
            else if (we.CompleteArea1 && we.CompleteArea2 && we.CompleteArea3 && !we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = Database.COMMON_HUGE_GREEN_POTION; ;
            }
            else if (we.CompleteArea1 && we.CompleteArea2 && we.CompleteArea3 && we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = Database.COMMON_GORGEOUS_GREEN_POTION;
            }
            else
            {
                potionName = Database.POOR_SMALL_GREEN_POTION;
            }
            bool result = mc.AddBackPack(new ItemBackPack(potionName));
            if (!result)
            {
                UpdateMainMessage("�A�C���F���܂����A�o�b�N�p�b�N�������ς����B�����̂ĂȂ��ƂȁE�E�E");
                using (TruthStatusPlayer sp = new TruthStatusPlayer())
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

        private void GetPotionForLana()
        {
            string potionName = Database.POOR_SMALL_RED_POTION;

            if (!we.CompleteArea1 && !we.CompleteArea2 && !we.CompleteArea3 && !we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = Database.POOR_SMALL_RED_POTION;
            }
            else if (we.CompleteArea1 && !we.CompleteArea2 && !we.CompleteArea3 && !we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = Database.COMMON_NORMAL_RED_POTION;
            }
            else if (we.CompleteArea1 && we.CompleteArea2 && !we.CompleteArea3 && !we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = Database.COMMON_LARGE_RED_POTION;
            }
            else if (we.CompleteArea1 && we.CompleteArea2 && we.CompleteArea3 && !we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = Database.COMMON_HUGE_RED_POTION;
            }
            else if (we.CompleteArea1 && we.CompleteArea2 && we.CompleteArea3 && we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = Database.COMMON_GORGEOUS_RED_POTION;
            }
            else
            {
                potionName = Database.POOR_SMALL_RED_POTION;
            }
            bool result = mc.AddBackPack(new ItemBackPack(potionName));
            if (!result)
            {
                UpdateMainMessage("�A�C���F���܂����A�o�b�N�p�b�N�������ς����B�����̂ĂȂ��ƂȁE�E�E");
                using (TruthStatusPlayer sp = new TruthStatusPlayer())
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
                    sp.WE = this.we;
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

        private string MessageFormatForLana(int num)
        {
            MainCharacter currentPlayer = new MainCharacter();
            currentPlayer.Name = "���i";
            switch (num)
            {
                case 1001:
                    if (!we.AvailableSecondCharacter)
                    {
                        return currentPlayer.GetCharacterSentence(num);
                    }
                    else
                    {
                        return currentPlayer.GetCharacterSentence(1003);
                    }

                case 1002:
                    if (!we.AvailableSecondCharacter)
                    {
                        return currentPlayer.GetCharacterSentence(num);
                    }
                    else
                    {
                        return currentPlayer.GetCharacterSentence(1004);
                    }
                default:
                    return currentPlayer.GetCharacterSentence(num);
            }
        }
        
        // ���i�̃���������i�X
        private void button6_Click(object sender, EventArgs e)
        {
            CallPotionShop();
            mainMessage.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
//            BattleStart(Database.DUEL_SHUVALTZ_FLORE, true);
//            BattleStart(Database.DUEL_KILT_JORJU, false);
            //BattleStart(Database.DUEL_DUMMY_SUBURI, true);
            //BattleStart(Database.DUEL_SINIKIA_KAHLHANZ, true);
            // BattleStart(Database.ENEMY_LAST_RANA_AMILIA, true);
           // BattleStart(Database.ENEMY_LAST_SINIKIA_KAHLHANZ, true);
             //BattleStart(Database.ENEMY_LAST_OL_LANDIS, true);
            BattleStart(Database.DUEL_DUMMY_SUBURI, true);
            //3vs3�͂�����
            //BattleStart(Database.DUEL_DUMMY_SUBURI, Database.DUEL_DUMMY_SUBURI, Database.DUEL_DUMMY_SUBURI, Database.RANA_AMILIA, Database.VERZE_ARTIE, false);
//            BattleStart(Database.ENEMY_GENAN_HUNTER, "", "", Database.RANA_AMILIA, "", false);
        }

        private bool BattleStart(string targetName, bool duel)
        {
            return BattleStart(targetName, null, null, null, null, duel);
        }
        private bool BattleStart(string targetName, string targetName2, string targetName3, string allyName2, string allyName3, bool duel)
        {
            GroundOne.StopDungeonMusic();

            bool result = EncountBattle(targetName, targetName2, targetName3, allyName2, allyName3, duel);
            GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
            return result;
        }

        // DUEL��p�̃o�g���Z�b�e�B���O�ł��B�ʏ�퓬�̌Ăяo���Ƃ͐����ɍ��ق�����܂��B
        private bool EncountBattle(string enemyName, string enemyName2, string enemyName3, string allyName2, string allyName3, bool duel)
        {
            GroundOne.StopDungeonMusic();

            bool endFlag = false;
            while (!endFlag)
            {
                System.Threading.Thread.Sleep(100);
                using (TruthBattleEnemy be = new TruthBattleEnemy())
                {
                    MainCharacter tempMC = new MainCharacter();
                    MainCharacter tempSC = new MainCharacter();
                    MainCharacter tempTC = new MainCharacter();
                    WorldEnvironment tempWE = new WorldEnvironment();
                    TruthWorldEnvironment tempWE2 = new TruthWorldEnvironment(); // ��Ғǉ�

                    tempMC.MainArmor = this.MC.MainArmor;
                    tempMC.SubWeapon = this.MC.SubWeapon;
                    tempMC.MainWeapon = this.MC.MainWeapon;
                    tempMC.Accessory = this.MC.Accessory;
                    tempMC.Accessory2 = this.MC.Accessory2;

                    tempSC.MainArmor = this.SC.MainArmor;
                    tempSC.SubWeapon = this.SC.SubWeapon;
                    tempSC.MainWeapon = this.SC.MainWeapon;
                    tempSC.Accessory = this.SC.Accessory;
                    tempSC.Accessory2 = this.SC.Accessory2;

                    tempTC.MainArmor = this.TC.MainArmor;
                    tempTC.SubWeapon = this.TC.SubWeapon;
                    tempTC.MainWeapon = this.TC.MainWeapon;
                    tempTC.Accessory = this.TC.Accessory;
                    tempTC.Accessory2 = this.TC.Accessory2;

                    ItemBackPack[] tempBackPack = new ItemBackPack[this.MC.GetBackPackInfo().Length];
                    tempBackPack = mc.GetBackPackInfo();
                    be.MC = tempMC;
                    for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
                    {
                        if (tempBackPack[ii] != null)
                        {
                            int stack = tempBackPack[ii].StackValue;
                            for (int jj = 0; jj < stack; jj++)
                            {
                                be.MC.AddBackPack(tempBackPack[ii]);
                            }
                        }
                    }


                    if (allyName2 != null && allyName2 != string.Empty)
                    {
                        ItemBackPack[] tempBackPack2 = new ItemBackPack[this.SC.GetBackPackInfo().Length];
                        tempBackPack2 = sc.GetBackPackInfo();
                        be.SC = tempSC;
                        for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
                        {
                            if (tempBackPack2[ii] != null)
                            {
                                int stack = tempBackPack2[ii].StackValue;
                                for (int jj = 0; jj < stack; jj++)
                                {
                                    be.SC.AddBackPack(tempBackPack2[ii]);
                                }
                            }
                        }
                    }
                    else
                    {
                        be.SC = null;
                    }

                    if (allyName3 != null && allyName3 != string.Empty)
                    {
                        ItemBackPack[] tempBackPack3 = new ItemBackPack[this.TC.GetBackPackInfo().Length];
                        tempBackPack3 = tc.GetBackPackInfo();
                        be.TC = tempTC;
                        for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
                        {
                            if (tempBackPack3[ii] != null)
                            {
                                int stack = tempBackPack3[ii].StackValue;
                                for (int jj = 0; jj < stack; jj++)
                                {
                                    be.TC.AddBackPack(tempBackPack3[ii]);
                                }
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
                        // s ��Ғǉ�
                        else if (pi.PropertyType == typeof(PlayerStance))
                        {
                            try
                            {
                                pi.SetValue(tempMC, (PlayerStance)(Enum.Parse(typeof(PlayerStance), type.GetProperty(pi.Name).GetValue(this.MC, null).ToString())), null);
                                pi.SetValue(tempSC, (PlayerStance)(Enum.Parse(typeof(PlayerStance), type.GetProperty(pi.Name).GetValue(this.SC, null).ToString())), null);
                                pi.SetValue(tempTC, (PlayerStance)(Enum.Parse(typeof(PlayerStance), type.GetProperty(pi.Name).GetValue(this.TC, null).ToString())), null);
                            }
                            catch { }
                        }
                        // e ��Ғǉ�
                        // s ��Ғǉ�
                        else if (pi.PropertyType == typeof(AdditionalSpellType))
                        {
                            try
                            {
                                pi.SetValue(tempMC, (AdditionalSpellType)(Enum.Parse(typeof(AdditionalSpellType), type.GetProperty(pi.Name).GetValue(this.MC, null).ToString())), null);
                                pi.SetValue(tempSC, (AdditionalSpellType)(Enum.Parse(typeof(AdditionalSpellType), type.GetProperty(pi.Name).GetValue(this.SC, null).ToString())), null);
                                pi.SetValue(tempTC, (AdditionalSpellType)(Enum.Parse(typeof(AdditionalSpellType), type.GetProperty(pi.Name).GetValue(this.TC, null).ToString())), null);
                            }
                            catch { }
                        }
                        else if (pi.PropertyType == typeof(AdditionalSkillType))
                        {
                            try
                            {
                                pi.SetValue(tempMC, (AdditionalSkillType)(Enum.Parse(typeof(AdditionalSkillType), type.GetProperty(pi.Name).GetValue(this.MC, null).ToString())), null);
                                pi.SetValue(tempSC, (AdditionalSkillType)(Enum.Parse(typeof(AdditionalSkillType), type.GetProperty(pi.Name).GetValue(this.SC, null).ToString())), null);
                                pi.SetValue(tempTC, (AdditionalSkillType)(Enum.Parse(typeof(AdditionalSkillType), type.GetProperty(pi.Name).GetValue(this.TC, null).ToString())), null);
                            }
                            catch { }
                        }
                        // e ��Ғǉ�
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


                    Type type3 = tempWE2.GetType();
                    foreach (PropertyInfo pi in type3.GetProperties())
                    {
                        // [�x��]�Fcatch�\����Set�v���p�e�B���Ȃ��ꍇ�����A����ȊO�̃P�[�X�������Ȃ��Ȃ��Ă��܂��̂ŗv���͕��@�����B
                        if (pi.PropertyType == typeof(System.Int32))
                        {
                            try
                            {
                                pi.SetValue(tempWE2, (System.Int32)(type3.GetProperty(pi.Name).GetValue(GroundOne.WE2, null)), null);
                            }
                            catch { }
                        }
                        else if (pi.PropertyType == typeof(System.String))
                        {
                            try
                            {
                                pi.SetValue(tempWE2, (string)(type3.GetProperty(pi.Name).GetValue(GroundOne.WE2, null)), null);
                            }
                            catch { }
                        }
                        else if (pi.PropertyType == typeof(System.Boolean))
                        {
                            try
                            {
                                pi.SetValue(tempWE2, (System.Boolean)(type3.GetProperty(pi.Name).GetValue(GroundOne.WE2, null)), null);
                            }
                            catch { }
                        }
                    }

                    TruthEnemyCharacter ec1 = null;
                    TruthEnemyCharacter ec2 = null;
                    TruthEnemyCharacter ec3 = null;
                    if (enemyName != null  && enemyName  != string.Empty) ec1 = new TruthEnemyCharacter(enemyName);
                    if (enemyName2 != null && enemyName2 != string.Empty) ec2 = new TruthEnemyCharacter(enemyName2);
                    if (enemyName3 != null && enemyName3 != string.Empty) ec3 = new TruthEnemyCharacter(enemyName3);
                    be.EC1 = ec1;
                    be.EC2 = ec2;
                    be.EC3 = ec3;
                    be.WE = tempWE;
                    be.StartPosition = FormStartPosition.CenterParent;
                    //be.IgnoreApplicationDoEvent = true;
                    be.DuelMode = duel;
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
                        return false;
                    }
                    else if (be.DialogResult == DialogResult.Ignore)
                    {
                        endFlag = true;
                        return false;
                    }
                    else
                    {
                        if (we.AvailableFirstCharacter)
                        {
                            this.MC = tempMC;
                            this.MC.ReplaceBackPack(tempMC.GetBackPackInfo());
                        }
                        return true;
                    }
                }
            }

            return false;
        }


        private void ReConstractWorldEnvironment()
        {
            if (GroundOne.WE2.TruthBadEnd1 && we.TruthCommunicationCompArea1)
            {
                we.TruthCompleteSlayBoss1 = false;
                we.TruthCompleteArea1 = false;
                we.TruthCompleteArea1Day = 0;
                we.TruthCommunicationCompArea1 = false;

                we.dungeonEvent11KeyOpen = false;
                we.dungeonEvent11NotOpen = false;
                we.dungeonEvent12KeyOpen = false;
                we.dungeonEvent12NotOpen = false;
                we.dungeonEvent13KeyOpen = false;
                we.dungeonEvent13NotOpen = false;
                we.dungeonEvent14KeyOpen = false;
                we.dungeonEvent14NotOpen = false;
                we.dungeonEvent15 = false;
                we.dungeonEvent16 = false;
                we.dungeonEvent17 = false;
                we.dungeonEvent18 = false;
                we.dungeonEvent19 = false;
                we.dungeonEvent20 = false;
                we.dungeonEvent21KeyOpen = false;
                we.dungeonEvent21NotOpen = false;
                we.dungeonEvent22KeyOpen = false;
                we.dungeonEvent22NotOpen = false;
                we.dungeonEvent23KeyOpen = false;
                we.dungeonEvent23NotOpen = false;
                we.dungeonEvent24KeyOpen = false;
                we.dungeonEvent24NotOpen = false;
                we.dungeonEvent25 = false;
                we.dungeonEvent26 = false;
                we.dungeonEvent27 = false;
                we.dungeonEvent28KeyOpen = false;
                we.dungeonEvent29 = false;
                we.dungeonEvent30 = false;
                we.dungeonEvent31 = false;

                we.BoardInfo10 = false;
                we.BoardInfo11 = false;
                we.BoardInfo12 = false;
                we.BoardInfo13 = false;
                we.BoardInfo14 = false;

                we.MeetOlLandis = false;
                we.MeetOlLandisBeforeGanz = false;
                we.MeetOlLandisBeforeHanna = false;
                we.MeetOlLandisBeforeLana = false;

                we.AvailableDuelColosseum = false;
                we.AvailableDuelMatch = false;
                we.AvailablePotionshop = false;

                we.Truth_Communication_Dungeon11 = false;
                we.Truth_CommunicationJoinPartyLana = false;
                we.Truth_CommunicationNotJoinLana = false;
                we.Truth_CommunicationFirstHomeTown = false;

                we.Truth_CommunicationLana1_1 = false;

                we.Truth_CommunicationLana1 = false;
                we.Truth_CommunicationLana2 = false;
                we.Truth_CommunicationLana3 = false;
                we.Truth_CommunicationLana4 = false;
                we.Truth_CommunicationLana5 = false;
                we.Truth_CommunicationLana6 = false;
                we.Truth_CommunicationLana7 = false;
                we.Truth_CommunicationLana8 = false;
                we.Truth_CommunicationLana9 = false;
                we.Truth_CommunicationLana10 = false;
                
                we.Truth_CommunicationGanz1 = false;
                we.Truth_CommunicationGanz2 = false;
                we.Truth_CommunicationGanz3 = false;
                we.Truth_CommunicationGanz4 = false;
                we.Truth_CommunicationGanz5 = false;
                we.Truth_CommunicationGanz6 = false;
                we.Truth_CommunicationGanz7 = false;
                we.Truth_CommunicationGanz8 = false;
                we.Truth_CommunicationGanz9 = false;
                we.Truth_CommunicationGanz10 = false;

                we.Truth_CommunicationHanna1 = false;
                we.Truth_CommunicationHanna10 = false;
                we.Truth_CommunicationHanna2 = false;
                we.Truth_CommunicationHanna3 = false;
                we.Truth_CommunicationHanna4 = false;
                we.Truth_CommunicationHanna5 = false;
                we.Truth_CommunicationHanna6 = false;
                we.Truth_CommunicationHanna7 = false;
                we.Truth_CommunicationHanna8 = false;
                we.Truth_CommunicationHanna9 = false;

                we.Truth_GiveLanaEarring = false;

                we.AvailableSecondCharacter = false;

                we.GameDay = 1; // ���߂Ă̏ꍇ�P���ڂƂ���B
                we.SaveByDungeon = false; // ���߂Ă̏ꍇ�A������X�^�[�g����B
                we.DungeonPosX = 1 + Database.DUNGEON_BASE_X + (Database.FIRST_POS % Database.TRUTH_DUNGEON_COLUMN) * Database.DUNGEON_MOVE_LEN;
                we.DungeonPosY = 1 + Database.DUNGEON_BASE_Y + (Database.FIRST_POS / Database.TRUTH_DUNGEON_COLUMN) * Database.DUNGEON_MOVE_LEN;
                we.DungeonArea = 1; // ���߂Ă̏ꍇ�_���W��������X�^�[�g���Ă��邽�߁A�P�Ƃ���B
                we.AvailableFirstCharacter = true; // ���߂Ă̏ꍇ�A��l���A�C����o�^���Ă����B

                we.AlreadyCommunicate = false;
                we.AlreadyRest = true; // [�x��] ��҂̃X�g�[���[�ݒ��A�Ӑ}�I��True�����݌v�v�z�Ƃ��Ă͊�Ȃ��B

                we.AlreadyUseSyperSaintWater = false;
                we.AlreadyUseRevivePotion = false;
                we.AlreadyUsePureWater = false;
                we.AlreadyGetOneDayItem = false;
                we.AlreadyGetMonsterHunt = false;
                we.AlreadyDuelComplete = false;               

                if (mc != null)
                {
                    mc.CurrentLife = mc.MaxLife;
                    mc.CurrentSkillPoint = mc.MaxSkillPoint;
                    mc.CurrentMana = mc.MaxMana;
                    mc.AlreadyPlayArchetype = false;
                }
                if (sc != null)
                {
                    sc.CurrentLife = sc.MaxLife;
                    sc.CurrentSkillPoint = sc.MaxSkillPoint;
                    sc.CurrentMana = sc.MaxMana;
                    sc.AlreadyPlayArchetype = false;
                }
                if (tc != null)
                {
                    tc.CurrentLife = tc.MaxLife;
                    tc.CurrentSkillPoint = tc.MaxSkillPoint;
                    tc.CurrentMana = tc.MaxMana;
                    tc.AlreadyPlayArchetype = false;
                }

                this.firstDay = 1;
                this.dayLabel.Text = we.GameDay.ToString() + "����";
            }
        }

        private void GetItemFullCheck(MainCharacter player, string itemName)
        {
            bool result = player.AddBackPack(new ItemBackPack(itemName));
            if (result) return;

            UpdateMainMessage("�A�C���F���܂����A�o�b�N�p�b�N�������ς����B�����̂ĂȂ��ƂȁE�E�E");

            using (TruthStatusPlayer sp = new TruthStatusPlayer())
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
                sp.WE = this.we;
                sp.OnlySelectTrash = true;
                if ((itemName == Database.RARE_EARRING_OF_LANA) ||
                    (itemName == Database.RARE_TOOMI_BLUE_SUISYOU))
                {
                    sp.CannotSelectTrash = itemName;
                }
                sp.StartPosition = FormStartPosition.CenterParent;
                sp.ShowDialog();
                if (this.DialogResult == DialogResult.Cancel)
                {
                }
                else
                {
                    mc = sp.MC;
                    mc.AddBackPack(new ItemBackPack(itemName));
                }
            }
            UpdateMainMessage("", true);
        }

        private void TruthHomeTown_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (GroundOne.sound != null)
            {
                GroundOne.sound.StopMusic();
                //GroundOne.sound.Disactive();
            }
        }

        Image backgroundData = null;
        public static Image AdjustBrightness(Image img, float b)
        {
            //���邳��ύX�����摜�̕`���ƂȂ�Image�I�u�W�F�N�g���쐬
            Bitmap newImg = new Bitmap(img.Width, img.Height);
            //newImg��Graphics�I�u�W�F�N�g���擾
            Graphics g = Graphics.FromImage(newImg);
            
            float[][] colorMatrixElements = { 
                new float[] {1,    0,    0,    0, 0},
                new float[] {0,    1,    0,    0, 0},
                new float[] {0,    0,    1,    0, 0},
                new float[] {0,    0,    0,    1, 0},
                new float[] {b,    b,    b,    0, 1}};

            //ColorMatrix�I�u�W�F�N�g�̍쐬
            System.Drawing.Imaging.ColorMatrix cm = new System.Drawing.Imaging.ColorMatrix(colorMatrixElements);

            //ImageAttributes�I�u�W�F�N�g�̍쐬
            System.Drawing.Imaging.ImageAttributes ia =
                new System.Drawing.Imaging.ImageAttributes();
            //ColorMatrix��ݒ肷��
            ia.SetColorMatrix(cm);

            //ImageAttributes���g�p���ĕ`��
            g.DrawImage(img,
                new Rectangle(0, 0, img.Width, img.Height),
                0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);

            //���\�[�X���������
            g.Dispose();

            return newImg;
        }
        private void ChangeBackgroundData(string filename, float darkValue = 0)
        {
            if (filename == null || filename == String.Empty || filename == "")
            {
                this.backgroundData = null;
            }
            else
            {
                Image current = Image.FromFile(filename);
                if (darkValue > 0)
                {
                    System.Drawing.Imaging.ImageAttributes imageAttributes = new System.Drawing.Imaging.ImageAttributes();

                    Image newImg = AdjustBrightness(current, darkValue);
                    this.backgroundData = newImg;
                }
                else
                {
                    this.backgroundData = current;
                }
            }
            this.Invalidate();
        }

        private float GetOpacity(float y)
        {
            float opacity = 0;
            float border_top = 100.0F;
            float border_top2 = 400.0F;
            float border_bottom = 700.0F;
            float border_bottom2 = 768.0F;
            if (y <= border_top)
            {
                opacity = 255;
            }
            else if (border_top < y && y <= border_top2)
            {
                opacity = (border_top2 - y) / (border_top2 - border_top) * 255.0F;
            }
            else if (border_top2 < y && y <= border_bottom)
            {
                opacity = 0;
            }
            else if (border_bottom < y && y <= border_bottom2)
            {
                opacity = (y - border_bottom) / (border_bottom2 - border_bottom) * 255.0F;
            }
            else
            {
                opacity = 255;
            }
            return opacity;
        }
        Font fontA = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
        PointF point = new PointF(50, 768);
        List<string> endingText = new List<string>();
        List<string> endingText2 = new List<string>();
        List<string> endingText3 = new List<string>();
        int endingText3Count = 0;
        StringFormat format = new StringFormat();
        SolidBrush b = new SolidBrush(Color.Black);

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (GroundOne.WE2.SeekerEnd)
            {
                if (this.backgroundData != null)
                {
                    e.Graphics.DrawImage(this.backgroundData, new Rectangle(0, 0, 1024, 768));
                }
            }
            else if (this.backgroundData != null && GroundOne.WE2.SeekerEndingRoll == false)
            {
                e.Graphics.DrawImage(this.backgroundData, new Rectangle(0, 0, 1024, 768));
            }

            if (!GroundOne.WE2.SeekerEnd && GroundOne.WE2.SeekerEvent1103)
            {
                Graphics g = e.Graphics;
                for (int ii = 0; ii < this.endingText.Count; ii++)
                {
                    float y = point.Y + ii * 50;
                    float opacity = GetOpacity(y);
                    if (opacity < 255)
                    {
                        b.Color = Color.FromArgb(255, (int)opacity, (int)opacity, (int)opacity);
                        g.DrawString(endingText[ii], this.fontA, b, new PointF(point.X, y));
                    }

                    if (ii < endingText2.Count)
                    {
                        float y2 = point.Y + ii * 160;
                        float opacity2 = GetOpacity(y2);
                        if (opacity2 < 255)
                        {
                            b.Color = Color.FromArgb(255, (int)opacity2, (int)opacity2, (int)opacity2);
                            format.Alignment = StringAlignment.Center;
                            g.DrawString(endingText2[ii], this.fontA, b, new RectangleF(512, y2, 512, 1000), format);
                        }
                    }
                }

                for (int ii = 0; ii < this.endingText3.Count; ii++)
                {
                    float opacity3 = 0;
                    if (0 <= this.endingText3Count && this.endingText3Count < 200)
                    {
                        opacity3 = (float)((200 - this.endingText3Count) / 200.0F * 255.0F);
                    }
                    else
                    {
                        opacity3 = (float)((this.endingText3Count - 200) / 400.0F * 255.0F);
                        if (opacity3 > 255) { opacity3 = 255; }
                    }
                    if (opacity3 < 255)
                    {
                        b.Color = Color.FromArgb(255, (int)opacity3, (int)opacity3, (int)opacity3);
                        format.Alignment = StringAlignment.Near;
                        g.DrawString(endingText3[ii], this.fontA, b, new RectangleF(0 + this.endingText3Count / 5, 409, 1024 - this.endingText3Count / 5, 1000), format);
                    }
                    this.endingText3Count++;
                }
            }
        }

        private void buttonLevelup_Click(object sender, EventArgs e)
        {
            mc.Exp += 70000;
            Method.LevelUpCharacter(we, mc, sc, tc, false, System.Drawing.Color.LightSkyBlue);
        }

    }
}