using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class TruthDuelSelect : MotherForm // 後編編集
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

        public string targetName { get; set; }

        public TruthDuelSelect()
        {
            InitializeComponent();
        }

        const string TITLE_HONOR_1 = "無名の新参者";
        const string TITLE_HONOR_2 = "オル・ランディスの弟子";
        const string TITLE_HONOR_3 = "ベテラン・キラー";
        const string TITLE_HONOR_4 = "DUELマスター";
        const string TITLE_HONOR_5 = "TOP Duelist 8";
        const string TITLE_HONOR_6 = "伝説を継ぐもの";
        const string TITLE_HONOR_7 = "DUEL闘技場の覇者";

        // 対戦相手のステータスを確認
        private void button7_Click(object sender, EventArgs e)
        {
            using (TruthDuelPlayerStatus tdps = new TruthDuelPlayerStatus())
            {
                tdps.StartPosition = FormStartPosition.CenterScreen;
                // mc.Level < XX を撤廃して、レベル超えていても戦えるようにした。
                // 7, 10, 13, 16, 19, 20, 23, 26, 29, 32, 35, 38, 41, 44, 47, 50, 52, 54, 56, 58, 60
                // ダンジョンエリア毎に対戦相手を制御するのを追加した。
                if (!we.TruthDuelMatch1 && !we.TruthCompleteArea1)
                {
                    tdps.tec = new TruthEnemyCharacter(Database.DUEL_EONE_FULNEA);
                }
                else if (!we.TruthDuelMatch2 && !we.TruthCompleteArea1)
                {
                    tdps.tec = new TruthEnemyCharacter(Database.DUEL_MAGI_ZELKIS);
                }
                else if (!we.TruthDuelMatch3 && !we.TruthCompleteArea1)
                {
                    tdps.tec = new TruthEnemyCharacter(Database.DUEL_SELMOI_RO);
                }
                else if (!we.TruthDuelMatch4 && !we.TruthCompleteArea1)
                {
                    tdps.tec = new TruthEnemyCharacter(Database.DUEL_KARTIN_MAI);
                }
                else if (!we.TruthDuelMatch5 && !we.TruthCompleteArea1)
                {
                    tdps.tec = new TruthEnemyCharacter(Database.DUEL_JEDA_ARUS);
                }
                else if (!we.TruthDuelMatch6 && !we.TruthCompleteArea1)
                {
                    tdps.tec = new TruthEnemyCharacter(Database.DUEL_SINIKIA_VEILHANTU);
                }
                else if (!we.TruthDuelMatch7 && !we.TruthCompleteArea2)
                {
                    tdps.tec = new TruthEnemyCharacter(Database.DUEL_ADEL_BRIGANDY);
                }
                else if (!we.TruthDuelMatch8 && !we.TruthCompleteArea2)
                {
                    tdps.tec = new TruthEnemyCharacter(Database.DUEL_LENE_COLTOS);
                }
                else if (!we.TruthDuelMatch9 && !we.TruthCompleteArea2)
                {
                    tdps.tec = new TruthEnemyCharacter(Database.DUEL_SCOTY_ZALGE);
                }
                else if (!we.TruthDuelMatch10 && !we.TruthCompleteArea2)
                {
                    tdps.tec = new TruthEnemyCharacter(Database.DUEL_PERMA_WARAMY);
                }
                else if (!we.TruthDuelMatch11 && !we.TruthCompleteArea2)
                {
                    tdps.tec = new TruthEnemyCharacter(Database.DUEL_KILT_JORJU);
                }
                else if (!we.TruthDuelMatch12 && !we.TruthCompleteArea3)
                {
                    tdps.tec = new TruthEnemyCharacter(Database.DUEL_BILLY_RAKI);
                }
                else if (!we.TruthDuelMatch13 && !we.TruthCompleteArea3)
                {
                    tdps.tec = new TruthEnemyCharacter(Database.DUEL_ANNA_HAMILTON);
                }
                else if (!we.TruthDuelMatch14 && !we.TruthCompleteArea3)
                {
                    tdps.tec = new TruthEnemyCharacter(Database.DUEL_CALMANS_OHN);
                }
                else if (!we.TruthDuelMatch15 && !we.TruthCompleteArea3)
                {
                    tdps.tec = new TruthEnemyCharacter(Database.DUEL_SUN_YU);
                }
                else if (!we.TruthDuelMatch16 && !we.TruthCompleteArea3)
                {
                    tdps.tec = new TruthEnemyCharacter(Database.DUEL_SHUVALTZ_FLORE);
                }
                else if (!we.TruthDuelMatch17 && !we.TruthCompleteArea4)
                {
                    tdps.tec = new TruthEnemyCharacter(Database.DUEL_RVEL_ZELKIS);
                }
                else if (!we.TruthDuelMatch18 && !we.TruthCompleteArea4)
                {
                    tdps.tec = new TruthEnemyCharacter(Database.DUEL_VAN_HEHGUSTEL);
                }
                else if (!we.TruthDuelMatch19 && !we.TruthCompleteArea4)
                {
                    tdps.tec = new TruthEnemyCharacter(Database.DUEL_OHRYU_GENMA);
                }
                else if (!we.TruthDuelMatch20 && !we.TruthCompleteArea4)
                {
                    tdps.tec = new TruthEnemyCharacter(Database.DUEL_LADA_MYSTORUS);
                }
                else if (!we.TruthDuelMatch21 && !we.TruthCompleteArea4)
                {
                    tdps.tec = new TruthEnemyCharacter(Database.DUEL_SIN_OSCURETE);
                }
                else
                {
                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.Message = "現在、対戦相手の候補は設定されていません。";
                        md.StartPosition = FormStartPosition.CenterScreen;
                        md.ShowDialog();
                    }
                    return;
                }
                tdps.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (TruthDuelRule TDR = new TruthDuelRule())
            {
                TDR.StartPosition = FormStartPosition.CenterScreen;
                TDR.ShowDialog();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void TruthDuelSelect_Load(object sender, EventArgs e)
        {
            label2.Text = "戦歴　" + GroundOne.WE2.DuelWin.ToString() + " 勝" + GroundOne.WE2.DuelLose.ToString() + " 敗";

            if (GroundOne.WE2.DuelWin >= 21)
            {
                labelTitleHonor.Text = TITLE_HONOR_7;
            }
            else if (GroundOne.WE2.DuelWin >= 20)
            {
                labelTitleHonor.Text = TITLE_HONOR_6;
            }
            else if (GroundOne.WE2.DuelWin >= 16)
            {
                labelTitleHonor.Text = TITLE_HONOR_5;
            }
            else if (GroundOne.WE2.DuelWin >= 12)
            {
                labelTitleHonor.Text = TITLE_HONOR_4;
            }
            else if (GroundOne.WE2.DuelWin >= 8)
            {
                labelTitleHonor.Text = TITLE_HONOR_3;
            }
            else if (GroundOne.WE2.DuelWin >= 4)
            {
                labelTitleHonor.Text = TITLE_HONOR_2;
            }
            else
            {
                labelTitleHonor.Text = TITLE_HONOR_1;
            }
        }

    }
}
