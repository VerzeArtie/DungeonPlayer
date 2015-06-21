using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace DungeonPlayer
{
    public partial class GameSetting : MotherForm
    {
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

        private bool firstEnableBGM = false; // Œã•Ò’Ç‰Á

        public GameSetting()
        {
            InitializeComponent();
        }

        private void GameSetting_Load(object sender, EventArgs e)
        {
            this.checkBox1.Checked = GroundOne.EnableBGM;
            this.checkBox2.Checked = GroundOne.EnableSoundEffect;
            this.battleSpeedBar.Value = this.battleSpeed;
            this.difficultyBar.Value = this.difficulty;

            this.firstEnableBGM = this.checkBox1.Checked; // Œã•Ò’Ç‰Á
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GroundOne.EnableBGM = this.checkBox1.Checked;
            GroundOne.EnableSoundEffect = this.checkBox2.Checked;
            this.battleSpeed = battleSpeedBar.Value;
            this.difficulty = difficultyBar.Value;

            XmlTextWriter xmlWriter = new XmlTextWriter(Database.GameSettingFileName, Encoding.UTF8);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteWhitespace("\r\n");

            xmlWriter.WriteStartElement("Body");
            xmlWriter.WriteWhitespace("\r\n");
            xmlWriter.WriteElementString("DateTime", DateTime.Now.ToString());
            xmlWriter.WriteWhitespace("\r\n");
            xmlWriter.WriteElementString("EnableBGM", GroundOne.EnableBGM.ToString());
            xmlWriter.WriteWhitespace("\r\n");
            xmlWriter.WriteElementString("EnableSoundEffect", GroundOne.EnableSoundEffect.ToString());
            xmlWriter.WriteWhitespace("\r\n");
            xmlWriter.WriteElementString("BattleSpeed", this.battleSpeed.ToString());
            xmlWriter.WriteWhitespace("\r\n");
            xmlWriter.WriteElementString("Difficulty", this.difficulty.ToString());
            xmlWriter.WriteWhitespace("\r\n");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndDocument();
            xmlWriter.Close();

            this.Close();
        }

        // s Œã•Ò’Ç‰Á
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (this.firstEnableBGM == false)
                {
                    this.firstEnableBGM = true;
                    GroundOne.EnableBGM = true;
                    GroundOne.PlayDungeonMusic(Database.BGM12, Database.BGM12LoopBegin);
                }
                else
                {
                    GroundOne.ResumeDungeonMusic();
                }
            }
            else
            {
                GroundOne.TempStopDungeonMusic();
            }
        }
        // e Œã•Ò’Ç‰Á
    }
}