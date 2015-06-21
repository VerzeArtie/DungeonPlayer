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
    public partial class ESCMenu : System.Windows.Forms.Form
    {
        private MainCharacter mc;
        private MainCharacter sc;
        private MainCharacter tc;
        private WorldEnvironment we;
        private bool[] knownTileInfo;
        private bool[] knownTileInfo2;
        private bool[] knownTileInfo3;
        private bool[] knownTileInfo4;
        private bool[] knownTileInfo5;

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

        protected bool onlySave = false;
        public bool OnlySave
        {
            get { return onlySave; }
            set { onlySave = value; }
        }

        public bool[] Truth_KnownTileInfo { get; set; } // 後編追加
        public bool[] Truth_KnownTileInfo2 { get; set; } // 後編追加
        public bool[] Truth_KnownTileInfo3 { get; set; } // 後編追加
        public bool[] Truth_KnownTileInfo4 { get; set; } // 後編追加
        public bool[] Truth_KnownTileInfo5 { get; set; } // 後編追加

        public bool TruthStory { get; set;} // 後編追加

        public ESCMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd)
            {
                if (!GroundOne.WE2.AutoSaveInfo)
                {
                    using (TruthPlayerInformation TPI = new TruthPlayerInformation())
                    {
                        TPI.StartPosition = FormStartPosition.CenterParent;
                        TPI.SetupMessage = "ここまでの記録は自動セーブとなります。次回起動は、ここから再開となります。";
                        TPI.ShowDialog();
                    }
                    GroundOne.WE2.AutoSaveInfo = true;
                    Method.AutoSaveTruthWorldEnvironment();
                    Method.AutoSaveRealWorld(this.MC, this.SC, this.TC, this.WE, this.knownTileInfo, this.knownTileInfo2, this.knownTileInfo3, this.knownTileInfo4, this.knownTileInfo5, this.Truth_KnownTileInfo, this.Truth_KnownTileInfo2, this.Truth_KnownTileInfo3, this.Truth_KnownTileInfo4, this.Truth_KnownTileInfo5);
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    return;
                }
                else
                {
                    Method.AutoSaveTruthWorldEnvironment();
                    Method.AutoSaveRealWorld(this.MC, this.SC, this.TC, this.WE, this.knownTileInfo, this.knownTileInfo2, this.knownTileInfo3, this.knownTileInfo4, this.knownTileInfo5, this.Truth_KnownTileInfo, this.Truth_KnownTileInfo2, this.Truth_KnownTileInfo3, this.Truth_KnownTileInfo4, this.Truth_KnownTileInfo5);
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    return;
                }
            }

            using (YesNoReqWithMessage ynrw = new YesNoReqWithMessage())
            {
                ynrw.StartPosition = FormStartPosition.CenterParent;
                ynrw.MainMessage = "セーブしていない場合、現在データは破棄されます。セーブしますか？";
                ynrw.ShowDialog();
                if (ynrw.DialogResult == DialogResult.Yes)
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
                        esc.Truth_KnownTileInfo = this.Truth_KnownTileInfo; // 後編追加
                        esc.Truth_KnownTileInfo2 = this.Truth_KnownTileInfo2; // 後編追加
                        esc.Truth_KnownTileInfo3 = this.Truth_KnownTileInfo3; // 後編追加
                        esc.Truth_KnownTileInfo4 = this.Truth_KnownTileInfo4; // 後編追加
                        esc.Truth_KnownTileInfo5 = this.Truth_KnownTileInfo5; // 後編追加                        esc.StartPosition = FormStartPosition.CenterParent;
                        esc.StartPosition = FormStartPosition.CenterParent;
                        esc.OnlySave = true;
                        esc.ShowDialog();
                    }
                }

                ynrw.MainMessage = "タイトルへ戻りますか？";
                ynrw.ShowDialog();
                if (ynrw.DialogResult == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.Cancel;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!this.TruthStory)
            {
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
                    sp.StartPosition = FormStartPosition.CenterParent;
                    sp.ShowDialog();
                    mc = sp.MC;
                    if (we.AvailableSecondCharacter)
                    {
                        sc = sp.SC;
                    }
                    if (we.AvailableThirdCharacter)
                    {
                        tc = sp.TC;
                    }
                    if (sp.DialogResult == DialogResult.Abort)
                    {
                        this.DialogResult = DialogResult.Abort;
                    }

                    if (Owner != null)
                    {
                        if (we.AvailableFirstCharacter)
                        {
                            try
                            {
                                // 前編用
                                ((Form1)Owner).currentLife1.Width = (int)((double)((double)mc.CurrentLife / (double)mc.MaxLife) * 100.0f);
                                ((Form1)Owner).currentSkillPoint1.Width = (int)((double)((double)mc.CurrentSkillPoint / (double)mc.MaxSkillPoint) * 100.0f);
                                ((Form1)Owner).currentManaPoint1.Width = (int)((double)((double)mc.CurrentMana / (double)mc.MaxMana) * 100.0f);
                            }
                            catch
                            {
                                // s 後編追加
                                ((TruthDungeon)Owner).currentLife1.Width = (int)((double)((double)mc.CurrentLife / (double)mc.MaxLife) * 100.0f);
                                ((TruthDungeon)Owner).currentSkillPoint1.Width = (int)((double)((double)mc.CurrentSkillPoint / (double)mc.MaxSkillPoint) * 100.0f);
                                ((TruthDungeon)Owner).currentManaPoint1.Width = (int)((double)((double)mc.CurrentMana / (double)mc.MaxMana) * 100.0f);
                                // e 後編追加
                            }
                        }
                        if (we.AvailableSecondCharacter)
                        {
                            try
                            {
                                // 前編用
                                ((Form1)Owner).currentLife2.Width = (int)((double)((double)sc.CurrentLife / (double)sc.MaxLife) * 100.0f);
                                ((Form1)Owner).currentSkillPoint2.Width = (int)((double)((double)sc.CurrentSkillPoint / (double)sc.MaxSkillPoint) * 100.0f);
                                ((Form1)Owner).currentManaPoint2.Width = (int)((double)((double)sc.CurrentMana / (double)sc.MaxMana) * 100.0f);
                            }
                            catch
                            {
                                // s 後編追加
                                ((TruthDungeon)Owner).currentLife2.Width = (int)((double)((double)sc.CurrentLife / (double)sc.MaxLife) * 100.0f);
                                ((TruthDungeon)Owner).currentSkillPoint2.Width = (int)((double)((double)sc.CurrentSkillPoint / (double)sc.MaxSkillPoint) * 100.0f);
                                ((TruthDungeon)Owner).currentManaPoint2.Width = (int)((double)((double)sc.CurrentMana / (double)sc.MaxMana) * 100.0f);
                                // e 後編追加
                            }
                        }
                        else
                        {
                            // s 後編追加
                            try
                            {
                                // 前編用
                                ((Form1)Owner).SecondPlayerPanel.Visible = false;
                            }
                            catch
                            {
                                ((TruthDungeon)Owner).SecondPlayerPanel.Visible = false;
                            }
                            // e 後編追加
                        }

                        if (we.AvailableThirdCharacter)
                        {
                            try
                            {
                                // 前編用
                                ((Form1)Owner).currentLife3.Width = (int)((double)((double)tc.CurrentLife / (double)tc.MaxLife) * 100.0f);
                                ((Form1)Owner).currentSkillPoint3.Width = (int)((double)((double)tc.CurrentSkillPoint / (double)tc.MaxSkillPoint) * 100.0f);
                                ((Form1)Owner).currentManaPoint3.Width = (int)((double)((double)tc.CurrentMana / (double)tc.MaxMana) * 100.0f);
                            }
                            catch
                            {
                                // s 後編追加
                                ((TruthDungeon)Owner).currentLife3.Width = (int)((double)((double)tc.CurrentLife / (double)tc.MaxLife) * 100.0f);
                                ((TruthDungeon)Owner).currentSkillPoint3.Width = (int)((double)((double)tc.CurrentSkillPoint / (double)tc.MaxSkillPoint) * 100.0f);
                                ((TruthDungeon)Owner).currentManaPoint3.Width = (int)((double)((double)tc.CurrentMana / (double)tc.MaxMana) * 100.0f);
                                // e 後編追加
                            }
                        }
                        else
                        {
                            try
                            {
                                // 前編用
                                ((Form1)Owner).ThirdPlayerPanel.Visible = false;
                            }
                            catch
                            {
                                // s 後編追加
                                ((TruthDungeon)Owner).ThirdPlayerPanel.Visible = false;
                                // e 後編追加
                            }
                        }
                    }
                }
            }
            else
            {

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
                    sp.StartPosition = FormStartPosition.CenterParent;
                    sp.ShowDialog();
                    mc = sp.MC;
                    if (we.AvailableSecondCharacter)
                    {
                        sc = sp.SC;
                    }
                    if (we.AvailableThirdCharacter)
                    {
                        tc = sp.TC;
                    }
                    if (sp.DialogResult == DialogResult.Abort)
                    {
                        this.DialogResult = DialogResult.Abort;
                    }

                    if (Owner != null)
                    {
                        if (we.AvailableFirstCharacter)
                        {
                            try
                            {
                                // 前編用
                                ((Form1)Owner).currentLife1.Width = (int)((double)((double)mc.CurrentLife / (double)mc.MaxLife) * 100.0f);
                                ((Form1)Owner).currentSkillPoint1.Width = (int)((double)((double)mc.CurrentSkillPoint / (double)mc.MaxSkillPoint) * 100.0f);
                                ((Form1)Owner).currentManaPoint1.Width = (int)((double)((double)mc.CurrentMana / (double)mc.MaxMana) * 100.0f);
                            }
                            catch
                            {
                                ((TruthDungeon)Owner).SetupPlayerStatus(); // 後編追加
                            }
                        }
                        if (we.AvailableSecondCharacter)
                        {
                            try
                            {
                                // 前編用
                                ((Form1)Owner).currentLife2.Width = (int)((double)((double)sc.CurrentLife / (double)sc.MaxLife) * 100.0f);
                                ((Form1)Owner).currentSkillPoint2.Width = (int)((double)((double)sc.CurrentSkillPoint / (double)sc.MaxSkillPoint) * 100.0f);
                                ((Form1)Owner).currentManaPoint2.Width = (int)((double)((double)sc.CurrentMana / (double)sc.MaxMana) * 100.0f);
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            // s 後編追加
                            try
                            {
                                // 前編用
                                ((Form1)Owner).SecondPlayerPanel.Visible = false;
                            }
                            catch
                            {
                                ((TruthDungeon)Owner).SecondPlayerPanel.Visible = false;
                            }
                            // e 後編追加
                        }

                        if (we.AvailableThirdCharacter)
                        {
                            try
                            {
                                // 前編用
                                ((Form1)Owner).currentLife3.Width = (int)((double)((double)tc.CurrentLife / (double)tc.MaxLife) * 100.0f);
                                ((Form1)Owner).currentSkillPoint3.Width = (int)((double)((double)tc.CurrentSkillPoint / (double)tc.MaxSkillPoint) * 100.0f);
                                ((Form1)Owner).currentManaPoint3.Width = (int)((double)((double)tc.CurrentMana / (double)tc.MaxMana) * 100.0f);
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            try
                            {
                                // 前編用
                                ((Form1)Owner).ThirdPlayerPanel.Visible = false;
                            }
                            catch
                            {
                                // s 後編追加
                                ((TruthDungeon)Owner).ThirdPlayerPanel.Visible = false;
                                // e 後編追加
                            }
                        }
                    }
                }

            }
        }

        private void ESCMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd)
            {
                using (TruthPlayerInformation TPI = new TruthPlayerInformation())
                {
                    TPI.StartPosition = FormStartPosition.CenterParent;
                    TPI.SetupMessage = "ここまでの記録は自動セーブとなります。ゲームを終わりたい場合は、ゲーム終了を押してください。";
                    TPI.ShowDialog();
                }
                return;
            }

            using (SaveLoad sl = new SaveLoad())
            {
                sl.MC = this.MC;
                sl.SC = this.SC;
                sl.TC = this.TC;
                sl.WE = this.WE;
                sl.KnownTileInfo = this.knownTileInfo;
                sl.KnownTileInfo2 = this.knownTileInfo2;
                sl.KnownTileInfo3 = this.knownTileInfo3;
                sl.KnownTileInfo4 = this.knownTileInfo4;
                sl.KnownTileInfo5 = this.knownTileInfo5;
                sl.Truth_KnownTileInfo = this.Truth_KnownTileInfo; // 後編追加
                sl.Truth_KnownTileInfo2 = this.Truth_KnownTileInfo2; // 後編追加
                sl.Truth_KnownTileInfo3 = this.Truth_KnownTileInfo3; // 後編追加
                sl.Truth_KnownTileInfo4 = this.Truth_KnownTileInfo4; // 後編追加
                sl.Truth_KnownTileInfo5 = this.Truth_KnownTileInfo5; // 後編追加
                sl.SaveMode = true;
                sl.StartPosition = FormStartPosition.CenterParent;
                sl.ShowDialog();
            }


        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd)
            {
                using (TruthPlayerInformation TPI = new TruthPlayerInformation())
                {
                    TPI.StartPosition = FormStartPosition.CenterParent;
                    TPI.SetupMessage = "ここまでの記録は自動セーブとなります。ゲームを終わりたい場合は、ゲーム終了を押してください。";
                    TPI.ShowDialog();
                }
                return;
            }

            using (SaveLoad sl = new SaveLoad())
            {
                sl.StartPosition = FormStartPosition.CenterParent;
                sl.ShowDialog();
                if (sl.DialogResult == DialogResult.Cancel)
                {
                    return;
                }
                else
                {
                    this.MC = sl.MC;
                    this.SC = sl.SC;
                    this.TC = sl.TC;
                    this.WE = sl.WE;
                    this.KnownTileInfo = sl.KnownTileInfo;
                    this.KnownTileInfo2 = sl.KnownTileInfo2;
                    this.KnownTileInfo3 = sl.KnownTileInfo3;
                    this.KnownTileInfo4 = sl.KnownTileInfo4;
                    this.KnownTileInfo5 = sl.KnownTileInfo5;
                    this.Truth_KnownTileInfo = sl.Truth_KnownTileInfo; // 後編追加
                    this.Truth_KnownTileInfo2 = sl.Truth_KnownTileInfo2; // 後編追加
                    this.Truth_KnownTileInfo3 = sl.Truth_KnownTileInfo3; // 後編追加
                    this.Truth_KnownTileInfo4 = sl.Truth_KnownTileInfo4; // 後編追加
                    this.Truth_KnownTileInfo5 = sl.Truth_KnownTileInfo5; // 後編追加
                    this.DialogResult = DialogResult.Retry;
                }
            }
        }

        // s 後編追加
        private void button6_Click(object sender, EventArgs e)
        {
            using (TruthBattleSetting tbs = new TruthBattleSetting())
            {
                tbs.StartPosition = FormStartPosition.CenterParent;
                tbs.MC = this.mc;
                tbs.SC = this.sc;
                tbs.TC = this.tc;
                tbs.WE = this.we;
                tbs.ShowDialog();
                if (tbs.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    this.mc = tbs.MC;
                    this.sc = tbs.SC;
                    this.tc = tbs.TC;
                    this.we = tbs.WE;
                }
            }
        }
        // e 後編追加

        private void ESCMenu_Load(object sender, EventArgs e)
        {
            // s 後編追加
            if (this.we.AvailableBattleSettingMenu == false)
            {
                this.button2.Location = new System.Drawing.Point(12, 12);
                this.button3.Location = new System.Drawing.Point(12, 68);
                this.button4.Location = new System.Drawing.Point(12, 124);
                this.button5.Location = new System.Drawing.Point(12, 180);
                this.button1.Location = new System.Drawing.Point(12, 236);
                this.button6.Visible = false;
            }
            // e 後編追加

            if (this.onlySave)
            {
                button4_Click(sender, e);
                this.Close();
            }
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.AliceBlue;
            button3.BackColor = Color.AliceBlue;
            button4.BackColor = Color.AliceBlue;
            button5.BackColor = Color.AliceBlue;
            button6.BackColor = Color.AliceBlue; // 後編追加
            button2.BackColor = Color.NavajoWhite;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackColor = Color.AliceBlue;
        }

        private void button2_Enter(object sender, EventArgs e)
        {
            button1.BackColor = Color.AliceBlue;
            button3.BackColor = Color.AliceBlue;
            button4.BackColor = Color.AliceBlue;
            button5.BackColor = Color.AliceBlue;
            button6.BackColor = Color.AliceBlue; // 後編追加
            button2.BackColor = Color.NavajoWhite;
        }

        private void button2_Leave(object sender, EventArgs e)
        {
            button2.BackColor = Color.AliceBlue;
        }

        private void button3_Enter(object sender, EventArgs e)
        {
            button1.BackColor = Color.AliceBlue;
            button2.BackColor = Color.AliceBlue;
            button4.BackColor = Color.AliceBlue;
            button5.BackColor = Color.AliceBlue;
            button6.BackColor = Color.AliceBlue; // 後編追加
            button3.BackColor = Color.NavajoWhite;
        }

        private void button3_Leave(object sender, EventArgs e)
        {
            button3.BackColor = Color.AliceBlue;
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.AliceBlue;
            button2.BackColor = Color.AliceBlue;
            button4.BackColor = Color.AliceBlue;
            button5.BackColor = Color.AliceBlue;
            button6.BackColor = Color.AliceBlue; // 後編追加
            button3.BackColor = Color.NavajoWhite;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color.AliceBlue;
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.AliceBlue;
            button2.BackColor = Color.AliceBlue;
            button3.BackColor = Color.AliceBlue;
            button5.BackColor = Color.AliceBlue;
            button6.BackColor = Color.AliceBlue; // 後編追加
            button4.BackColor = Color.NavajoWhite;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.BackColor = Color.AliceBlue;
        }

        private void button4_Enter(object sender, EventArgs e)
        {
            button1.BackColor = Color.AliceBlue;
            button2.BackColor = Color.AliceBlue;
            button3.BackColor = Color.AliceBlue;
            button5.BackColor = Color.AliceBlue;
            button6.BackColor = Color.AliceBlue; // 後編追加
            button4.BackColor = Color.NavajoWhite;
        }

        private void button4_Leave(object sender, EventArgs e)
        {
            button4.BackColor = Color.AliceBlue;
        }

        private void button5_Enter(object sender, EventArgs e)
        {
            button1.BackColor = Color.AliceBlue;
            button2.BackColor = Color.AliceBlue;
            button3.BackColor = Color.AliceBlue;
            button4.BackColor = Color.AliceBlue;
            button6.BackColor = Color.AliceBlue; // 後編追加
            button5.BackColor = Color.NavajoWhite;
        }

        private void button5_Leave(object sender, EventArgs e)
        {
            button5.BackColor = Color.AliceBlue;
        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.AliceBlue;
            button2.BackColor = Color.AliceBlue;
            button3.BackColor = Color.AliceBlue;
            button4.BackColor = Color.AliceBlue;
            button6.BackColor = Color.AliceBlue; // 後編追加
            button5.BackColor = Color.NavajoWhite;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.BackColor = Color.AliceBlue;
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button2.BackColor = Color.AliceBlue;
            button3.BackColor = Color.AliceBlue;
            button4.BackColor = Color.AliceBlue;
            button5.BackColor = Color.AliceBlue;
            button6.BackColor = Color.AliceBlue; // 後編追加
            button1.BackColor = Color.NavajoWhite;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.AliceBlue;
        }

        private void button1_Enter(object sender, EventArgs e)
        {
            button2.BackColor = Color.AliceBlue;
            button3.BackColor = Color.AliceBlue;
            button4.BackColor = Color.AliceBlue;
            button5.BackColor = Color.AliceBlue;
            button6.BackColor = Color.AliceBlue; // 後編追加
            button1.BackColor = Color.NavajoWhite;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            button1.BackColor = Color.AliceBlue;
        }

        // s 後編追加
        private void button6_Enter(object sender, EventArgs e)
        {
            button1.BackColor = Color.AliceBlue;
            button2.BackColor = Color.AliceBlue;
            button3.BackColor = Color.AliceBlue;
            button4.BackColor = Color.AliceBlue;
            button5.BackColor = Color.AliceBlue;
            button6.BackColor = Color.NavajoWhite;
        }

        private void button6_Leave(object sender, EventArgs e)
        {
            button6.BackColor = Color.AliceBlue;
        }

        private void button6_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.AliceBlue;
            button2.BackColor = Color.AliceBlue;
            button3.BackColor = Color.AliceBlue;
            button4.BackColor = Color.AliceBlue;
            button5.BackColor = Color.AliceBlue;
            button6.BackColor = Color.NavajoWhite;
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            button6.BackColor = Color.AliceBlue;
        }
        // e 後編追加

    }
}