using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class TruthInformation : MotherForm
    {
        Font defaultFont;
        public TruthInformation()
        {
            InitializeComponent();
            defaultFont = CommandLabel_JP.Font;
            baseWidth = AttributeButton1.Width;
            baseHeight = AttributeButton2.Height;
            extWidth = 100;
            extHeight = 40;
        }

        private void TruthInformation_Load(object sender, EventArgs e)
        {
            if (GroundOne.WE2 != null && GroundOne.WE2.AvailableMixSpellSkill == false)
            {
                MixSpellButton.Visible = false;
                MixSkillButton.Visible = false;
            }
            if (GroundOne.WE2 != null && GroundOne.WE2.AvailableArcheTypeCommand == false)
            {
                ArcheTypeButton.Visible = false;
            }
            SpellButton_Click(null, null);
            button1_Click(null, null);
        }

        private void TruthInformation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void TruthInformation_MouseClick(object sender, MouseEventArgs e)
        {
            //this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void TruthInformation_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        int baseWidth = 0;
        int baseHeight = 0;
        int extWidth = 0;
        int extHeight = 0;
        int TL_LocX = 30;
        int TL_LocY = 110;
        int TL_Margin = 110;
        int TLE_Margin_X = 100;
        int TLE_Margin_Y = 40;
        float baseFontSize = 20.25F;
        float extFontSize = 14.00F;
        int ARCHETYPE_SIZE_X = 160;
        int ARCHETYPE_SIZE_Y = 80;
        int ARCHETYPE_LocX = 100;
        int ARCHETYPE_LocY = 170;

        private void SpellButton_Click(object sender, EventArgs e)
        {
            // まずボタンのサイズを決定
            AttributeButton1.Size = new System.Drawing.Size(baseWidth, baseHeight);
            AttributeButton2.Size = new System.Drawing.Size(baseWidth, baseHeight);
            AttributeButton3.Size = new System.Drawing.Size(baseWidth, baseHeight);
            AttributeButton4.Size = new System.Drawing.Size(baseWidth, baseHeight);
            AttributeButton5.Size = new System.Drawing.Size(baseWidth, baseHeight);
            AttributeButton6.Size = new System.Drawing.Size(baseWidth, baseHeight);
            // 次はレイアウト。つまり位置
            AttributeButton1.Location = new Point(TL_LocX, TL_LocY);
            AttributeButton2.Location = new Point(TL_LocX, TL_LocY + TL_Margin);
            AttributeButton3.Location = new Point(TL_LocX + TL_Margin, TL_LocY);
            AttributeButton4.Location = new Point(TL_LocX + TL_Margin, TL_LocY + TL_Margin);
            AttributeButton5.Location = new Point(TL_LocX + TL_Margin * 2, TL_LocY);
            AttributeButton6.Location = new Point(TL_LocX + TL_Margin * 2, TL_LocY + TL_Margin);
            MixAttribute7.Location = new Point(-100, -100);
            MixAttribute8.Location = new Point(-100, -100);
            MixAttribute9.Location = new Point(-100, -100);
            MixAttribute10.Location = new Point(-100, -100);
            MixAttribute11.Location = new Point(-100, -100);
            MixAttribute12.Location = new Point(-100, -100);
            MixAttribute13.Location = new Point(-100, -100);
            MixAttribute14.Location = new Point(-100, -100);
            MixAttribute15.Location = new Point(-100, -100);
            // 不要なボタンは非表示（上）にして・・・
            MixAttribute7.Visible = false;
            MixAttribute8.Visible = false;
            MixAttribute9.Visible = false;
            MixAttribute10.Visible = false;
            MixAttribute11.Visible = false;
            MixAttribute12.Visible = false;
            MixAttribute13.Visible = false;
            MixAttribute14.Visible = false;
            MixAttribute15.Visible = false;
            // 必要なボタンは表示（上）
            AttributeButton1.Visible = true;
            AttributeButton2.Visible = true;
            AttributeButton3.Visible = true;
            AttributeButton4.Visible = true;
            AttributeButton5.Visible = true;
            AttributeButton6.Visible = true;
            // 必要なボタンは表示（下）させて・・・
            CommandButton1.Visible = true;
            CommandButton2.Visible = true;
            CommandButton3.Visible = true;
            CommandButton4.Visible = true;
            CommandButton5.Visible = true;
            CommandButton6.Visible = true;
            CommandButton7.Visible = true;
            // 上ボタン、色更新
            AttributeButton1.BackColor = Color.Gold;
            AttributeButton2.BackColor = Color.DarkGray;
            AttributeButton3.BackColor = Color.OrangeRed;
            AttributeButton4.BackColor = Color.CornflowerBlue;
            AttributeButton5.BackColor = Color.LimeGreen;
            AttributeButton6.BackColor = Color.White;
            // 上ボタン、テキスト更新
            AttributeButton1.Text = "聖";
            AttributeButton1.Font = new Font(AttributeButton2.Font.FontFamily, baseFontSize, FontStyle.Bold);
            AttributeButton2.Text = "闇";
            AttributeButton2.Font = new Font(AttributeButton2.Font.FontFamily, baseFontSize, FontStyle.Bold);
            AttributeButton3.Text = "火";
            AttributeButton3.Font = new Font(AttributeButton3.Font.FontFamily, baseFontSize, FontStyle.Bold);
            AttributeButton4.Text = "水";
            AttributeButton4.Font = new Font(AttributeButton2.Font.FontFamily, baseFontSize, FontStyle.Bold);
            AttributeButton5.Text = "理";
            AttributeButton5.Font = new Font(AttributeButton5.Font.FontFamily, baseFontSize, FontStyle.Bold);
            AttributeButton6.Text = "空";
            AttributeButton6.Font = new Font(AttributeButton2.Font.FontFamily, baseFontSize, FontStyle.Bold);
            // 最初の下項目のボタンを選択しておく！
            button1_Click(null, null);
            button7_Click(CommandButton1, null);
        }

        private void SkillButton_Click(object sender, EventArgs e)
        {
            // まずボタンのサイズを決定
            AttributeButton1.Size = new System.Drawing.Size(baseWidth, baseHeight);
            AttributeButton2.Size = new System.Drawing.Size(baseWidth, baseHeight);
            AttributeButton3.Size = new System.Drawing.Size(baseWidth, baseHeight);
            AttributeButton4.Size = new System.Drawing.Size(baseWidth, baseHeight);
            AttributeButton5.Size = new System.Drawing.Size(baseWidth, baseHeight);
            AttributeButton6.Size = new System.Drawing.Size(baseWidth, baseHeight);
            // 次はレイアウト。つまり位置
            AttributeButton1.Location = new Point(TL_LocX, TL_LocY);
            AttributeButton2.Location = new Point(TL_LocX, TL_LocY + TL_Margin);
            AttributeButton3.Location = new Point(TL_LocX + TL_Margin, TL_LocY);
            AttributeButton4.Location = new Point(TL_LocX + TL_Margin, TL_LocY + TL_Margin);
            AttributeButton5.Location = new Point(TL_LocX + TL_Margin * 2, TL_LocY);
            AttributeButton6.Location = new Point(TL_LocX + TL_Margin * 2, TL_LocY + TL_Margin);
            MixAttribute7.Location = new Point(-100, -100);
            MixAttribute8.Location = new Point(-100, -100);
            MixAttribute9.Location = new Point(-100, -100);
            MixAttribute10.Location = new Point(-100, -100);
            MixAttribute11.Location = new Point(-100, -100);
            MixAttribute12.Location = new Point(-100, -100);
            MixAttribute13.Location = new Point(-100, -100);
            MixAttribute14.Location = new Point(-100, -100);
            MixAttribute15.Location = new Point(-100, -100);
            // 不要なボタンは非表示（上）にして・・・
            MixAttribute7.Visible = false;
            MixAttribute8.Visible = false;
            MixAttribute9.Visible = false;
            MixAttribute10.Visible = false;
            MixAttribute11.Visible = false;
            MixAttribute12.Visible = false;
            MixAttribute13.Visible = false;
            MixAttribute14.Visible = false;
            MixAttribute15.Visible = false;
            // 必要なボタンは表示（上）
            AttributeButton1.Visible = true;
            AttributeButton2.Visible = true;
            AttributeButton3.Visible = true;
            AttributeButton4.Visible = true;
            AttributeButton5.Visible = true;
            AttributeButton6.Visible = true;
            // 必要なボタンは表示（下）させて・・・
            CommandButton1.Visible = true;
            CommandButton2.Visible = true;
            CommandButton3.Visible = true;
            CommandButton4.Visible = true;
            CommandButton5.Visible = false;
            CommandButton6.Visible = false;
            CommandButton7.Visible = false;
            // 上ボタン、色更新
            AttributeButton1.BackColor = Color.Gold;
            AttributeButton2.BackColor = Color.DarkGray;
            AttributeButton3.BackColor = Color.OrangeRed;
            AttributeButton4.BackColor = Color.CornflowerBlue;
            AttributeButton5.BackColor = Color.LimeGreen;
            AttributeButton6.BackColor = Color.White;
            // 上ボタン、テキスト更新
            AttributeButton1.Text = "動";
            AttributeButton1.Font = new Font(AttributeButton2.Font.FontFamily, baseFontSize, FontStyle.Bold);
            AttributeButton2.Text = "静";
            AttributeButton2.Font = new Font(AttributeButton2.Font.FontFamily, baseFontSize, FontStyle.Bold);
            AttributeButton3.Text = "柔";
            AttributeButton3.Font = new Font(AttributeButton3.Font.FontFamily, baseFontSize, FontStyle.Bold);
            AttributeButton4.Text = "剛";
            AttributeButton4.Font = new Font(AttributeButton2.Font.FontFamily, baseFontSize, FontStyle.Bold);
            AttributeButton5.Text = "心眼";
            AttributeButton5.Font = new Font(AttributeButton5.Font.FontFamily, baseFontSize, FontStyle.Bold);
            AttributeButton6.Text = "無心";
            AttributeButton6.Font = new Font(AttributeButton6.Font.FontFamily, baseFontSize, FontStyle.Bold);
            // 最初の下項目のボタンを選択しておく！
            button1_Click(null, null);
            button7_Click(CommandButton1, null);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            // まずボタンのサイズを決定
            AttributeButton1.Size = new System.Drawing.Size(extWidth, extHeight);
            AttributeButton2.Size = new System.Drawing.Size(extWidth, extHeight);
            AttributeButton3.Size = new System.Drawing.Size(extWidth, extHeight);
            AttributeButton4.Size = new System.Drawing.Size(extWidth, extHeight);
            AttributeButton5.Size = new System.Drawing.Size(extWidth, extHeight);
            AttributeButton6.Size = new System.Drawing.Size(extWidth, extHeight);
            // 次はレイアウト。つまり位置
            AttributeButton1.Location = new Point(TL_LocX, TL_LocY);
            AttributeButton2.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY);
            AttributeButton3.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY);
            AttributeButton4.Location = new Point(TL_LocX, TL_LocY + TLE_Margin_Y);
            AttributeButton5.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY + TLE_Margin_Y);
            AttributeButton6.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY + TLE_Margin_Y);
            MixAttribute7.Location = new Point(TL_LocX, TL_LocY + TLE_Margin_Y * 2);
            MixAttribute8.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY + TLE_Margin_Y * 2);
            MixAttribute9.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY + TLE_Margin_Y * 2);
            MixAttribute10.Location = new Point(TL_LocX, TL_LocY + TLE_Margin_Y * 3);
            MixAttribute11.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY + TLE_Margin_Y * 3);
            MixAttribute12.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY + TLE_Margin_Y * 3);
            MixAttribute13.Location = new Point(TL_LocX, TL_LocY + TLE_Margin_Y * 4);
            MixAttribute14.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY + TLE_Margin_Y * 4);
            MixAttribute15.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY + TLE_Margin_Y * 4);
            // 不要なボタンは非表示（上）にして・・・
            // 必要なボタンは表示（上）
            AttributeButton1.Visible = true;
            AttributeButton2.Visible = true;
            AttributeButton3.Visible = true;
            AttributeButton4.Visible = true;
            AttributeButton5.Visible = true;
            AttributeButton6.Visible = true;
            MixAttribute7.Visible = true;
            MixAttribute8.Visible = true;
            MixAttribute9.Visible = true;
            MixAttribute10.Visible = true;
            MixAttribute11.Visible = true;
            MixAttribute12.Visible = true;
            MixAttribute13.Visible = true;
            MixAttribute14.Visible = true;
            MixAttribute15.Visible = true;
            // 必要なボタンは表示（下）させて・・・
            CommandButton1.Visible = true;
            CommandButton2.Visible = true;
            CommandButton3.Visible = true;
            CommandButton4.Visible = false;
            CommandButton5.Visible = false;
            CommandButton6.Visible = false;
            CommandButton7.Visible = false;
            // 上ボタン、テキスト更新
            AttributeButton1.Text = "聖/火";
            AttributeButton1.Font = new Font(AttributeButton1.Font.FontFamily, extFontSize, FontStyle.Bold);
            AttributeButton2.Text = "聖/理";
            AttributeButton2.Font = new Font(AttributeButton2.Font.FontFamily, extFontSize, FontStyle.Bold);
            AttributeButton3.Text = "火/理";
            AttributeButton3.Font = new Font(AttributeButton3.Font.FontFamily, extFontSize, FontStyle.Bold);
            AttributeButton4.Text = "闇/水";
            AttributeButton4.Font = new Font(AttributeButton4.Font.FontFamily, extFontSize, FontStyle.Bold);
            AttributeButton5.Text = "闇/空";
            AttributeButton5.Font = new Font(AttributeButton5.Font.FontFamily, extFontSize, FontStyle.Bold);
            AttributeButton6.Text = "水/空";
            AttributeButton6.Font = new Font(AttributeButton6.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute7.Text = "聖/水";
            MixAttribute7.Font = new Font(MixAttribute7.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute8.Text = "聖/空";
            MixAttribute8.Font = new Font(MixAttribute8.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute9.Text = "火/空";
            MixAttribute9.Font = new Font(MixAttribute9.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute10.Text = "闇/火";
            MixAttribute10.Font = new Font(MixAttribute10.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute11.Text = "闇/理";
            MixAttribute11.Font = new Font(MixAttribute11.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute12.Text = "水/理";
            MixAttribute12.Font = new Font(MixAttribute12.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute13.Text = "聖/闇";
            MixAttribute13.Font = new Font(MixAttribute13.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute14.Text = "火/水";
            MixAttribute14.Font = new Font(MixAttribute14.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute15.Text = "理/空";
            MixAttribute15.Font = new Font(MixAttribute15.Font.FontFamily, extFontSize, FontStyle.Bold);
            // ボタン配色を変更！
            AttributeButton1.BackColor = Color.Cyan;
            AttributeButton2.BackColor = Color.Cyan;
            AttributeButton3.BackColor = Color.Cyan;
            AttributeButton4.BackColor = Color.Cyan;
            AttributeButton5.BackColor = Color.Cyan;
            AttributeButton6.BackColor = Color.Cyan;
            MixAttribute7.BackColor = Color.Yellow;
            MixAttribute8.BackColor = Color.Yellow;
            MixAttribute9.BackColor = Color.Yellow;
            MixAttribute10.BackColor = Color.Yellow;
            MixAttribute11.BackColor = Color.Yellow;
            MixAttribute12.BackColor = Color.Yellow;
            MixAttribute13.BackColor = Color.Magenta;
            MixAttribute14.BackColor = Color.Magenta;
            MixAttribute15.BackColor = Color.Magenta;
            // 最初の下項目のボタンを選択しておく！
            button1_Click(null, null);
            button7_Click(CommandButton1, null);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            // まずボタンのサイズを決定
            AttributeButton1.Size = new System.Drawing.Size(extWidth, extHeight);
            AttributeButton2.Size = new System.Drawing.Size(extWidth, extHeight);
            AttributeButton3.Size = new System.Drawing.Size(extWidth, extHeight);
            AttributeButton4.Size = new System.Drawing.Size(extWidth, extHeight);
            AttributeButton5.Size = new System.Drawing.Size(extWidth, extHeight);
            AttributeButton6.Size = new System.Drawing.Size(extWidth, extHeight);
            // 次はレイアウト。つまり位置
            AttributeButton1.Location = new Point(TL_LocX, TL_LocY);
            AttributeButton2.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY);
            AttributeButton3.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY);
            AttributeButton4.Location = new Point(TL_LocX, TL_LocY + TLE_Margin_Y);
            AttributeButton5.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY + TLE_Margin_Y);
            AttributeButton6.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY + TLE_Margin_Y);
            MixAttribute7.Location = new Point(TL_LocX, TL_LocY + TLE_Margin_Y * 2);
            MixAttribute8.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY + TLE_Margin_Y * 2);
            MixAttribute9.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY + TLE_Margin_Y * 2);
            MixAttribute10.Location = new Point(TL_LocX, TL_LocY + TLE_Margin_Y * 3);
            MixAttribute11.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY + TLE_Margin_Y * 3);
            MixAttribute12.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY + TLE_Margin_Y * 3);
            MixAttribute13.Location = new Point(TL_LocX, TL_LocY + TLE_Margin_Y * 4);
            MixAttribute14.Location = new Point(TL_LocX + TLE_Margin_X, TL_LocY + TLE_Margin_Y * 4);
            MixAttribute15.Location = new Point(TL_LocX + TLE_Margin_X * 2, TL_LocY + TLE_Margin_Y * 4);
            // 不要なボタンは非表示（上）にして・・・
            // 必要なボタンは表示（上）
            AttributeButton1.Visible = true;
            AttributeButton2.Visible = true;
            AttributeButton3.Visible = true;
            AttributeButton4.Visible = true;
            AttributeButton5.Visible = true;
            AttributeButton6.Visible = true;
            MixAttribute7.Visible = true;
            MixAttribute8.Visible = true;
            MixAttribute9.Visible = true;
            MixAttribute10.Visible = true;
            MixAttribute11.Visible = true;
            MixAttribute12.Visible = true;
            MixAttribute13.Visible = true;
            MixAttribute14.Visible = true;
            MixAttribute15.Visible = true;
            // 必要なボタンは表示（下）させて・・・
            CommandButton1.Visible = true;
            CommandButton2.Visible = true;
            CommandButton3.Visible = false;
            CommandButton4.Visible = false;
            CommandButton5.Visible = false;
            CommandButton6.Visible = false;
            CommandButton7.Visible = false;
            // 上ボタン、テキスト更新
            AttributeButton1.Text = "動/柔";
            AttributeButton1.Font = new Font(AttributeButton1.Font.FontFamily, extFontSize, FontStyle.Bold);
            AttributeButton2.Text = "動/心眼";
            AttributeButton2.Font = new Font(AttributeButton2.Font.FontFamily, extFontSize, FontStyle.Bold);
            AttributeButton3.Text = "柔/心眼";
            AttributeButton3.Font = new Font(AttributeButton3.Font.FontFamily, extFontSize, FontStyle.Bold);
            AttributeButton4.Text = "静/剛";
            AttributeButton4.Font = new Font(AttributeButton4.Font.FontFamily, extFontSize, FontStyle.Bold);
            AttributeButton5.Text = "静/無心";
            AttributeButton5.Font = new Font(AttributeButton5.Font.FontFamily, extFontSize, FontStyle.Bold);
            AttributeButton6.Text = "剛/無心";
            AttributeButton6.Font = new Font(AttributeButton6.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute7.Text = "動/剛";
            MixAttribute7.Font = new Font(MixAttribute7.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute8.Text = "動/無心";
            MixAttribute8.Font = new Font(MixAttribute8.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute9.Text = "柔/無心";
            MixAttribute9.Font = new Font(MixAttribute9.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute10.Text = "静/柔";
            MixAttribute10.Font = new Font(MixAttribute10.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute11.Text = "静/心眼";
            MixAttribute11.Font = new Font(MixAttribute11.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute12.Text = "剛/心眼";
            MixAttribute12.Font = new Font(MixAttribute12.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute13.Text = "動/静";
            MixAttribute13.Font = new Font(MixAttribute13.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute14.Text = "柔/剛";
            MixAttribute14.Font = new Font(MixAttribute14.Font.FontFamily, extFontSize, FontStyle.Bold);
            MixAttribute15.Text = "心眼/無心";
            MixAttribute15.Font = new Font(MixAttribute15.Font.FontFamily, extFontSize - 2, FontStyle.Bold);
            // ボタン配色を変更！
            AttributeButton1.BackColor = Color.Cyan;
            AttributeButton2.BackColor = Color.Cyan;
            AttributeButton3.BackColor = Color.Cyan;
            AttributeButton4.BackColor = Color.Cyan;
            AttributeButton5.BackColor = Color.Cyan;
            AttributeButton6.BackColor = Color.Cyan;
            MixAttribute7.BackColor = Color.Yellow;
            MixAttribute8.BackColor = Color.Yellow;
            MixAttribute9.BackColor = Color.Yellow;
            MixAttribute10.BackColor = Color.Yellow;
            MixAttribute11.BackColor = Color.Yellow;
            MixAttribute12.BackColor = Color.Yellow;
            MixAttribute13.BackColor = Color.Magenta;
            MixAttribute14.BackColor = Color.Magenta;
            MixAttribute15.BackColor = Color.Magenta;
            // 最初の下項目のボタンを選択しておく！
            button1_Click(null, null);
            button7_Click(CommandButton1, null);
        }

        private void Archetype_Click(object sender, EventArgs e)
        {
            // まずボタンのサイズを決定
            AttributeButton1.Size = new System.Drawing.Size(ARCHETYPE_SIZE_X, ARCHETYPE_SIZE_Y);
            // 次はレイアウト。つまり位置
            AttributeButton1.Location = new Point(ARCHETYPE_LocX, ARCHETYPE_LocY);
            // 不要なボタンは非表示（上）にして・・・
            AttributeButton2.Location = new Point(-100, -100);
            AttributeButton3.Location = new Point(-100, -100);
            AttributeButton4.Location = new Point(-100, -100);
            AttributeButton5.Location = new Point(-100, -100);
            AttributeButton6.Location = new Point(-100, -100);
            MixAttribute7.Location = new Point(-100, -100);
            MixAttribute8.Location = new Point(-100, -100);
            MixAttribute9.Location = new Point(-100, -100);
            MixAttribute10.Location = new Point(-100, -100);
            MixAttribute11.Location = new Point(-100, -100);
            MixAttribute12.Location = new Point(-100, -100);
            MixAttribute13.Location = new Point(-100, -100);
            MixAttribute14.Location = new Point(-100, -100);
            MixAttribute15.Location = new Point(-100, -100);
            AttributeButton2.Visible = false;
            AttributeButton3.Visible = false;
            AttributeButton4.Visible = false;
            AttributeButton5.Visible = false;
            AttributeButton6.Visible = false;
            MixAttribute7.Visible = false;
            MixAttribute8.Visible = false;
            MixAttribute9.Visible = false;
            MixAttribute10.Visible = false;
            MixAttribute11.Visible = false;
            MixAttribute12.Visible = false;
            MixAttribute13.Visible = false;
            MixAttribute14.Visible = false;
            MixAttribute15.Visible = false;
            // 必要なボタンは表示（上）
            // 必要なボタンは表示（下）させて・・・
            CommandButton1.Visible = true;
            CommandButton2.Visible = false;
            CommandButton3.Visible = false;
            CommandButton4.Visible = false;
            CommandButton5.Visible = false;
            CommandButton6.Visible = false;
            CommandButton7.Visible = false;
            // 上ボタン、テキスト更新
            AttributeButton1.Text = "元核";
            AttributeButton1.Font = new Font(AttributeButton1.Font.FontFamily, baseFontSize, FontStyle.Bold);
            // ボタン配色を変更！
            AttributeButton1.BackColor = Color.SlateBlue;
            // 最初の下項目のボタンを選択しておく！
            button1_Click(null, null);
            button7_Click(CommandButton1, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Color targetColor = Color.Gold;
            if (AttributeButton1.Text == "聖")
            {
                CommandButton1.Text = Database.FRESH_HEAL;
                CommandButton2.Text = Database.PROTECTION;
                CommandButton3.Text = Database.HOLY_SHOCK;
                CommandButton4.Text = Database.SAINT_POWER;
                CommandButton5.Text = Database.GLORY;
                CommandButton6.Text = Database.RESURRECTION;
                CommandButton7.Text = Database.CELESTIAL_NOVA;
            }
            else if (AttributeButton1.Text == "動")
            {
                CommandButton1.Text = Database.STRAIGHT_SMASH;
                CommandButton2.Text = Database.DOUBLE_SLASH;
                CommandButton3.Text = Database.CRUSHING_BLOW;
                CommandButton4.Text = Database.SOUL_INFINITY;
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
            }
            else if (AttributeButton1.Text == "聖/火")
            {
                CommandButton1.Text = Database.FLASH_BLAZE;
                CommandButton2.Text = Database.LIGHT_DETONATOR;
                CommandButton3.Text = Database.ASCENDANT_METEOR;
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
                targetColor = Color.Cyan;
            }
            else if (AttributeButton1.Text == "動/柔")
            {
                CommandButton1.Text = Database.SWIFT_STEP;
                CommandButton2.Text = Database.VIGOR_SENSE;
                CommandButton3.Text = "";
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
                targetColor = Color.Cyan;
            }
            else if (AttributeButton1.Text == "元核")
            {
                CommandButton1.Text = Database.ARCHETYPE_EIN_JP;
                CommandButton2.Text = "";// Database.ARCHETYPE_RANA_JP;
                CommandButton3.Text = "";// Database.ARCHETYPE_OL_JP;
                CommandButton4.Text = "";// Database.ARCHETYPE_VERZE_JP;
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
                targetColor = Color.SlateBlue;
            }

            CommandButton1.BackColor = targetColor;
            CommandButton2.BackColor = targetColor;
            CommandButton3.BackColor = targetColor;
            CommandButton4.BackColor = targetColor;
            CommandButton5.BackColor = targetColor;
            CommandButton6.BackColor = targetColor;
            CommandButton7.BackColor = targetColor;
            Description.BackColor = targetColor;
            button7_Click(CommandButton1, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Color targetColor = Color.DarkGray;
            if (AttributeButton2.Text == "闇")
            {
                CommandButton1.Text = Database.DARK_BLAST;
                CommandButton2.Text = Database.SHADOW_PACT;
                CommandButton3.Text = Database.LIFE_TAP;
                CommandButton4.Text = Database.BLACK_CONTRACT;
                CommandButton5.Text = Database.DEVOURING_PLAGUE;
                CommandButton6.Text = Database.BLOODY_VENGEANCE;
                CommandButton7.Text = Database.DAMNATION;
            }
            else if (AttributeButton2.Text == "静")
            {
                CommandButton1.Text = Database.COUNTER_ATTACK;
                CommandButton2.Text = Database.PURE_PURIFICATION;
                CommandButton3.Text = Database.ANTI_STUN;
                CommandButton4.Text = Database.STANCE_OF_DEATH;
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
            }
            else if (AttributeButton2.Text == "聖/理")
            {
                CommandButton1.Text = Database.HOLY_BREAKER;
                CommandButton2.Text = Database.EXALTED_FIELD;
                CommandButton3.Text = Database.HYMN_CONTRACT;
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
                targetColor = Color.Cyan;
            }
            else if (AttributeButton2.Text == "動/心眼")
            {
                CommandButton1.Text = Database.RUMBLE_SHOUT;
                CommandButton2.Text = Database.ONSLAUGHT_HIT;
                CommandButton3.Text = "";
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
                targetColor = Color.Cyan;
            }
            CommandButton1.BackColor = targetColor;
            CommandButton2.BackColor = targetColor;
            CommandButton3.BackColor = targetColor;
            CommandButton4.BackColor = targetColor;
            CommandButton5.BackColor = targetColor;
            CommandButton6.BackColor = targetColor;
            CommandButton7.BackColor = targetColor;
            Description.BackColor = targetColor;
            button7_Click(CommandButton1, null);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Color targetColor = Color.OrangeRed;
            if (AttributeButton3.Text == "火")
            {
                CommandButton1.Text = Database.FIRE_BALL;
                CommandButton2.Text = Database.FLAME_AURA;
                CommandButton3.Text = Database.HEAT_BOOST;
                CommandButton4.Text = Database.FLAME_STRIKE;
                CommandButton5.Text = Database.VOLCANIC_WAVE;
                CommandButton6.Text = Database.IMMORTAL_RAVE;
                CommandButton7.Text = Database.LAVA_ANNIHILATION;
            }
            else if (AttributeButton3.Text == "柔")
            {
                CommandButton1.Text = Database.STANCE_OF_FLOW;
                CommandButton2.Text = Database.ENIGMA_SENSE;
                CommandButton3.Text = Database.SILENT_RUSH;
                CommandButton4.Text = Database.OBORO_IMPACT;
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
            }
            else if (AttributeButton3.Text == "火/理")
            {
                CommandButton1.Text = Database.ENRAGE_BLAST;
                CommandButton2.Text = Database.PIERCING_FLAME;
                CommandButton3.Text = Database.SIGIL_OF_HOMURA;
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
                targetColor = Color.Cyan;
            }
            else if (AttributeButton3.Text == "柔/心眼")
            {
                CommandButton1.Text = Database.PSYCHIC_WAVE;
                CommandButton2.Text = Database.NOURISH_SENSE;
                CommandButton3.Text = "";
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
                targetColor = Color.Cyan;
            }
            CommandButton1.BackColor = targetColor;
            CommandButton2.BackColor = targetColor;
            CommandButton3.BackColor = targetColor;
            CommandButton4.BackColor = targetColor;
            CommandButton5.BackColor = targetColor;
            CommandButton6.BackColor = targetColor;
            CommandButton7.BackColor = targetColor;
            Description.BackColor = targetColor;
            button7_Click(CommandButton1, null);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Color targetColor = Color.CornflowerBlue;
            if (AttributeButton4.Text == "水")
            {
                CommandButton1.Text = Database.ICE_NEEDLE;
                CommandButton2.Text = Database.ABSORB_WATER;
                CommandButton3.Text = Database.CLEANSING;
                CommandButton4.Text = Database.FROZEN_LANCE;
                CommandButton5.Text = Database.MIRROR_IMAGE;
                CommandButton6.Text = Database.PROMISED_KNOWLEDGE;
                CommandButton7.Text = Database.ABSOLUTE_ZERO;
            }
            else if (AttributeButton4.Text == "剛")
            {
                CommandButton1.Text = Database.STANCE_OF_STANDING;
                CommandButton2.Text = Database.INNER_INSPIRATION;
                CommandButton3.Text = Database.KINETIC_SMASH;
                CommandButton4.Text = Database.CATASTROPHE;
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
            }
            else if (AttributeButton4.Text == "闇/水")
            {
                CommandButton1.Text = Database.BLUE_BULLET;
                CommandButton2.Text = Database.DEEP_MIRROR;
                CommandButton3.Text = Database.DEATH_DENY;
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
                targetColor = Color.Cyan;
            }
            else if (AttributeButton4.Text == "静/剛")
            {
                CommandButton1.Text = Database.REFLEX_SPIRIT;
                CommandButton2.Text = Database.FATAL_BLOW;
                CommandButton3.Text = "";
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
                targetColor = Color.Cyan;
            }
            CommandButton1.BackColor = targetColor;
            CommandButton2.BackColor = targetColor;
            CommandButton3.BackColor = targetColor;
            CommandButton4.BackColor = targetColor;
            CommandButton5.BackColor = targetColor;
            CommandButton6.BackColor = targetColor;
            CommandButton7.BackColor = targetColor;
            Description.BackColor = targetColor;
            button7_Click(CommandButton1, null);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Color targetColor = Color.LimeGreen;
            if (AttributeButton5.Text == "理")
            {
                CommandButton1.Text = Database.WORD_OF_POWER;
                CommandButton2.Text = Database.GALE_WIND;
                CommandButton3.Text = Database.WORD_OF_LIFE;
                CommandButton4.Text = Database.WORD_OF_FORTUNE;
                CommandButton5.Text = Database.AETHER_DRIVE;
                CommandButton6.Text = Database.GENESIS;
                CommandButton7.Text = Database.ETERNAL_PRESENCE;
            }
            else if (AttributeButton5.Text == "心眼")
            {
                CommandButton1.Text = Database.TRUTH_VISION;
                CommandButton2.Text = Database.HIGH_EMOTIONALITY;
                CommandButton3.Text = Database.STANCE_OF_EYES;
                CommandButton4.Text = Database.PAINFUL_INSANITY;
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
            }
            else if (AttributeButton5.Text == "闇/空")
            {
                CommandButton1.Text = Database.DARKEN_FIELD;
                CommandButton2.Text = Database.DOOM_BLADE;
                CommandButton3.Text = Database.ECLIPSE_END;
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
                targetColor = Color.Cyan;
            }
            else if (AttributeButton5.Text == "静/無心")
            {
                CommandButton1.Text = Database.TRUST_SILENCE;
                CommandButton2.Text = Database.MIND_KILLING;
                CommandButton3.Text = "";
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
                targetColor = Color.Cyan;
            }
            CommandButton1.BackColor = targetColor;
            CommandButton2.BackColor = targetColor;
            CommandButton3.BackColor = targetColor;
            CommandButton4.BackColor = targetColor;
            CommandButton5.BackColor = targetColor;
            CommandButton6.BackColor = targetColor;
            CommandButton7.BackColor = targetColor;
            Description.BackColor = targetColor;
            button7_Click(CommandButton1, null);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Color targetColor = Color.White;
            if (AttributeButton6.Text == "空")
            {
                CommandButton1.Text = Database.DISPEL_MAGIC;
                CommandButton2.Text = Database.RISE_OF_IMAGE;
                CommandButton3.Text = Database.DEFLECTION;
                CommandButton4.Text = Database.TRANQUILITY;
                CommandButton5.Text = Database.ONE_IMMUNITY;
                CommandButton6.Text = Database.WHITE_OUT;
                CommandButton7.Text = Database.TIME_STOP;
            }
            else if (AttributeButton6.Text == "無心")
            {
                CommandButton1.Text = Database.NEGATE;
                CommandButton2.Text = Database.VOID_EXTRACTION;
                CommandButton3.Text = Database.CARNAGE_RUSH;
                CommandButton4.Text = Database.NOTHING_OF_NOTHINGNESS;
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
            }
            else if (AttributeButton6.Text == "水/空")
            {
                CommandButton1.Text = Database.VANISH_WAVE;
                CommandButton2.Text = Database.VORTEX_FIELD;
                CommandButton3.Text = Database.BLUE_DRAGON_WILL;
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
                targetColor = Color.Cyan;
            }
            else if (AttributeButton6.Text == "剛/無心")
            {
                CommandButton1.Text = Database.OUTER_INSPIRATION;
                CommandButton2.Text = Database.HARDEST_PARRY;
                CommandButton3.Text = "";
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
                targetColor = Color.Cyan;
            }
            CommandButton1.BackColor = targetColor;
            CommandButton2.BackColor = targetColor;
            CommandButton3.BackColor = targetColor;
            CommandButton4.BackColor = targetColor;
            CommandButton5.BackColor = targetColor;
            CommandButton6.BackColor = targetColor;
            CommandButton7.BackColor = targetColor;
            Description.BackColor = targetColor;
            button7_Click(CommandButton1, null);
        }


        private void button17_Click(object sender, EventArgs e)
        {
            if (MixAttribute7.Text == "聖/水")
            {
                CommandButton1.Text = Database.SKY_SHIELD;
                CommandButton2.Text = Database.SACRED_HEAL;
                CommandButton3.Text = Database.EVER_DROPLET;
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
            }
            else if (MixAttribute7.Text == "動/剛")
            {
                CommandButton1.Text = Database.CIRCLE_SLASH;
                CommandButton2.Text = Database.RISING_AURA;
                CommandButton3.Text = "";
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
            }
            CommandButton1.BackColor = Color.Yellow;
            CommandButton2.BackColor = Color.Yellow;
            CommandButton3.BackColor = Color.Yellow;
            CommandButton4.BackColor = Color.Yellow;
            CommandButton5.BackColor = Color.Yellow;
            CommandButton6.BackColor = Color.Yellow;
            CommandButton7.BackColor = Color.Yellow;
            Description.BackColor = Color.Yellow;
            button7_Click(CommandButton1, null);
        }
        private void button18_Click(object sender, EventArgs e)
        {
            if (MixAttribute8.Text == "聖/空")
            {
                CommandButton1.Text = Database.STAR_LIGHTNING;
                CommandButton2.Text = Database.ANGEL_BREATH;
                CommandButton3.Text = Database.ENDLESS_ANTHEM;
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
            }
            else if (MixAttribute8.Text == "動/無心")
            {
                CommandButton1.Text = Database.SMOOTHING_MOVE;
                CommandButton2.Text = Database.ASCENSION_AURA;
                CommandButton3.Text = "";
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
            }
            CommandButton1.BackColor = Color.Yellow;
            CommandButton2.BackColor = Color.Yellow;
            CommandButton3.BackColor = Color.Yellow;
            CommandButton4.BackColor = Color.Yellow;
            CommandButton5.BackColor = Color.Yellow;
            CommandButton6.BackColor = Color.Yellow;
            CommandButton7.BackColor = Color.Yellow;
            Description.BackColor = Color.Yellow;
            button7_Click(CommandButton1, null);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (MixAttribute9.Text == "火/空")
            {
                CommandButton1.Text = Database.IMMOLATE;
                CommandButton2.Text = Database.PHANTASMAL_WIND;
                CommandButton3.Text = Database.RED_DRAGON_WILL;
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
            }
            else if (MixAttribute9.Text == "柔/無心")
            {
                CommandButton1.Text = Database.RECOVER;
                CommandButton2.Text = Database.IMPULSE_HIT;
                CommandButton3.Text = "";
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
            }
            CommandButton1.BackColor = Color.Yellow;
            CommandButton2.BackColor = Color.Yellow;
            CommandButton3.BackColor = Color.Yellow;
            CommandButton4.BackColor = Color.Yellow;
            CommandButton5.BackColor = Color.Yellow;
            CommandButton6.BackColor = Color.Yellow;
            CommandButton7.BackColor = Color.Yellow;
            Description.BackColor = Color.Yellow;
            button7_Click(CommandButton1, null);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (MixAttribute10.Text == "闇/火")
            {
                CommandButton1.Text = Database.BLACK_FIRE;
                CommandButton2.Text = Database.BLAZING_FIELD;
                CommandButton3.Text = Database.DEMONIC_IGNITE;
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
            }
            else if (MixAttribute10.Text == "静/柔")
            {
                CommandButton1.Text = Database.FUTURE_VISION;
                CommandButton2.Text = Database.UNKNOWN_SHOCK;
                CommandButton3.Text = "";
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
            }
            CommandButton1.BackColor = Color.Yellow;
            CommandButton2.BackColor = Color.Yellow;
            CommandButton3.BackColor = Color.Yellow;
            CommandButton4.BackColor = Color.Yellow;
            CommandButton5.BackColor = Color.Yellow;
            CommandButton6.BackColor = Color.Yellow;
            CommandButton7.BackColor = Color.Yellow;
            Description.BackColor = Color.Yellow;
            button7_Click(CommandButton1, null);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (MixAttribute11.Text == "闇/理")
            {
                CommandButton1.Text = Database.WORD_OF_MALICE;
                CommandButton2.Text = Database.ABYSS_EYE;
                CommandButton3.Text = Database.SIN_FORTUNE;
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
            }
            else if (MixAttribute11.Text == "静/心眼")
            {
                CommandButton1.Text = Database.SHARP_GLARE;
                CommandButton2.Text = Database.CONCUSSIVE_HIT;
                CommandButton3.Text = "";
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
            }
            CommandButton1.BackColor = Color.Yellow;
            CommandButton2.BackColor = Color.Yellow;
            CommandButton3.BackColor = Color.Yellow;
            CommandButton4.BackColor = Color.Yellow;
            CommandButton5.BackColor = Color.Yellow;
            CommandButton6.BackColor = Color.Yellow;
            CommandButton7.BackColor = Color.Yellow;
            Description.BackColor = Color.Yellow;
            button7_Click(CommandButton1, null);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (MixAttribute12.Text == "水/理")
            {
                CommandButton1.Text = Database.WORD_OF_ATTITUDE;
                CommandButton2.Text = Database.STATIC_BARRIER;
                CommandButton3.Text = Database.AUSTERITY_MATRIX;
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
            }
            else if (MixAttribute12.Text == "剛/心眼")
            {
                CommandButton1.Text = Database.VIOLENT_SLASH;
                CommandButton2.Text = Database.ONE_AUTHORITY;
                CommandButton3.Text = "";
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
            }
            CommandButton1.BackColor = Color.Yellow;
            CommandButton2.BackColor = Color.Yellow;
            CommandButton3.BackColor = Color.Yellow;
            CommandButton4.BackColor = Color.Yellow;
            CommandButton5.BackColor = Color.Yellow;
            CommandButton6.BackColor = Color.Yellow;
            CommandButton7.BackColor = Color.Yellow;
            Description.BackColor = Color.Yellow;
            button7_Click(CommandButton1, null);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (MixAttribute13.Text == "聖/闇")
            {
                CommandButton1.Text = Database.PSYCHIC_TRANCE;
                CommandButton2.Text = Database.BLIND_JUSTICE;
                CommandButton3.Text = Database.TRANSCENDENT_WISH;
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
            }
            else if (MixAttribute13.Text == "動/静")
            {
                CommandButton1.Text = Database.NEUTRAL_SMASH;
                CommandButton2.Text = Database.STANCE_OF_DOUBLE;
                CommandButton3.Text = "";
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
            }
            CommandButton1.BackColor = Color.Magenta;
            CommandButton2.BackColor = Color.Magenta;
            CommandButton3.BackColor = Color.Magenta;
            CommandButton4.BackColor = Color.Magenta;
            CommandButton5.BackColor = Color.Magenta;
            CommandButton6.BackColor = Color.Magenta;
            CommandButton7.BackColor = Color.Magenta;
            Description.BackColor = Color.Magenta;
            button7_Click(CommandButton1, null);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (MixAttribute14.Text == "火/水")
            {
                CommandButton1.Text = Database.FROZEN_AURA;
                CommandButton2.Text = Database.CHILL_BURN;
                CommandButton3.Text = Database.ZETA_EXPLOSION;
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
            }
            else if (MixAttribute14.Text == "柔/剛")
            {
                CommandButton1.Text = Database.SURPRISE_ATTACK;
                CommandButton2.Text = Database.STANCE_OF_MYSTIC;
                CommandButton3.Text = "";
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
            }
            CommandButton1.BackColor = Color.Magenta;
            CommandButton2.BackColor = Color.Magenta;
            CommandButton3.BackColor = Color.Magenta;
            CommandButton4.BackColor = Color.Magenta;
            CommandButton5.BackColor = Color.Magenta;
            CommandButton6.BackColor = Color.Magenta;
            CommandButton7.BackColor = Color.Magenta;
            Description.BackColor = Color.Magenta;
            button7_Click(CommandButton1, null);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (MixAttribute15.Text == "理/空")
            {
                CommandButton1.Text = Database.SEVENTH_MAGIC;
                CommandButton2.Text = Database.PARADOX_IMAGE;
                CommandButton3.Text = Database.WARP_GATE;
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
            }
            else if (MixAttribute15.Text == "心眼/無心")
            {
                CommandButton1.Text = Database.STANCE_OF_SUDDENNESS;
                CommandButton2.Text = Database.SOUL_EXECUTION;
                CommandButton3.Text = "";
                CommandButton4.Text = "";
                CommandButton5.Text = "";
                CommandButton6.Text = "";
                CommandButton7.Text = "";
            }
            CommandButton1.BackColor = Color.Magenta;
            CommandButton2.BackColor = Color.Magenta;
            CommandButton3.BackColor = Color.Magenta;
            CommandButton4.BackColor = Color.Magenta;
            CommandButton5.BackColor = Color.Magenta;
            CommandButton6.BackColor = Color.Magenta;
            CommandButton7.BackColor = Color.Magenta;
            Description.BackColor = Color.Magenta;
            button7_Click(CommandButton1, null);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (((Button)sender).Text == "？？？") return;

            string ext = ".bmp";
            string command = TruthActionCommand.ConvertToEnglish(((Button)sender).Text);
            pictureBox1.Image = Image.FromFile(Database.BaseResourceFolder + command + ext);

            switch (TruthActionCommand.GetTargetType(command))
            {
                case TruthActionCommand.TargetType.AllMember:
                    CommandTarget.Text = "敵味方全体";
                    break;
                case TruthActionCommand.TargetType.Ally:
                    CommandTarget.Text = "味方単体";
                    break;
                case TruthActionCommand.TargetType.AllyGroup:
                    CommandTarget.Text = "味方全体";
                    break;
                case TruthActionCommand.TargetType.AllyOrEnemy:
                    CommandTarget.Text = "敵単体 / 味方単体";
                    break;
                case TruthActionCommand.TargetType.Enemy:
                    CommandTarget.Text = "敵単体";
                    break;
                case TruthActionCommand.TargetType.EnemyGroup:
                    CommandTarget.Text = "敵全体";
                    break;
                case TruthActionCommand.TargetType.InstantTarget:
                    CommandTarget.Text = "インスタント対象";
                    break;
                case TruthActionCommand.TargetType.NoTarget:
                    CommandTarget.Text = "なし";
                    break;
                case TruthActionCommand.TargetType.Own:
                    CommandTarget.Text = "自分";
                    break;
            }

            CommandLabel_JP.Text = TruthActionCommand.ConvertToJapanese(command);
            CommandLabel_EN.Text = command;
            Description.Text = TruthActionCommand.GetDescription(command);
            CommandCost.Text = TruthActionCommand.GetCost(command).ToString();
            if (TruthActionCommand.GetTimingType(command) == TruthActionCommand.TimingType.Instant)
            {
                CommandTiming.Text = "インスタント";
            }
            else if (TruthActionCommand.GetTimingType(command) == TruthActionCommand.TimingType.Sorcery)
            {
                CommandTiming.Text = "ソーサリー";
            }
            //if (label5.Width > 255)
            //{
            //    label5.Font = new Font(label5.Font.FontFamily, label5.Font.Size - 3, FontStyle.Bold);
            //}
            //else
            //{
            //    label5.Font = defaultFont;
            //    if (label5.Width > 255)
            //    {
            //        label5.Font = new Font(label5.Font.FontFamily, label5.Font.Size - 3, FontStyle.Bold);
            //    }
            //}

            //string factor = TruthActionCommand.GetPowerFactor(command);
            //double powerValue = TruthActionCommand.GetPowerValue(command);
            //int plus = TruthActionCommand.GetPowerPlus(command);
            //int count = TruthActionCommand.GetPowerCount(command);
            
            //if (factor == "" && powerValue == 0.0f && plus == 0 && count == 1)
            //{
            //    CommandPower.Text = "----";
            //}
            //else
            //{
            //    CommandPower.Text = factor + " x " + powerValue.ToString("0.0") + " x " + count.ToString() +"回";
            //}
        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }      
    }
}
