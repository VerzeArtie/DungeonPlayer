using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class BattleItemRequest : MotherForm
    {
        private System.Windows.Forms.Button[] button = new Button[10];

        private ItemBackPack[] ibr;
        public ItemBackPack[] BackPackData
        {
            get { return ibr; }
            set { ibr = value; }
        }

        private int tryUseNum;
        public int TryUseNum
        {
            get { return tryUseNum; }
            set { tryUseNum = value; }
        }

        private int targetNum;
        public int TargetNum
        {
            get { return targetNum; }
            set { targetNum = value; }
        }

        private WorldEnvironment we;
        public WorldEnvironment WE
        {
            get { return we; }
            set { we = value; }
        }

        public BattleItemRequest()
        {
            InitializeComponent();
            for (int ii = 0; ii < 10; ii++)
            {
                button[ii] = new Button();
                button[ii].BackColor = System.Drawing.Color.White;
                button[ii].FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                button[ii].Location = new System.Drawing.Point(18, 12 + 41*ii);
                button[ii].Name = "button" + ii.ToString();
                button[ii].Size = new System.Drawing.Size(227, 23);
                button[ii].TabIndex = 0;
                button[ii].UseVisualStyleBackColor = false;
                button[ii].Click += new System.EventHandler(this.button_Click);
                this.Controls.Add(button[ii]);
            }
        }

        private void BattleItemRequest_Load(object sender, EventArgs e)
        {
            for (int ii = 0; ii < ibr.Length; ii++)
            {
                if (ibr[ii] != null)
                {
                    button[ii].Text = ibr[ii].Name;
                }
                else
                {
                    button[ii].Visible = false;
                }
            }

            button2.Focus();
        }

        private void button_Click(object sender, EventArgs e)
        {
            for (int ii = 0; ii < 10; ii++)
            {
                if (((Button)sender).Name == "button" + ii.ToString())
                {
                    this.tryUseNum = ii;

                    if (((Button)sender).Text == "リヴァイヴポーション")
                    {
                        using (SelectTarget st = new SelectTarget())
                        {
                            st.StartPosition = FormStartPosition.CenterParent;
                            if (we.AvailableFirstCharacter && we.AvailableSecondCharacter && we.AvailableThirdCharacter)
                            {
                                st.FirstName = "アイン";
                                st.SecondName = "ラナ";
                                st.ThirdName = "ヴェルゼ";
                                st.FourthName = "敵";
                                st.MaxSelectable = 4;
                            }
                            else if (we.AvailableFirstCharacter && we.AvailableSecondCharacter)
                            {
                                st.FirstName = "アイン";
                                st.SecondName = "ラナ";
                                st.ThirdName = "敵";
                                st.MaxSelectable = 3;
                            }
                            else if (we.AvailableFirstCharacter)
                            {
                                st.FirstName = "アイン";
                                st.SecondName = "敵";
                                st.MaxSelectable = 2;
                            }
                            st.ShowDialog();
                            if (we.AvailableFirstCharacter && we.AvailableSecondCharacter && we.AvailableThirdCharacter)
                            {
                                if (st.TargetNum == 0) DialogResult = DialogResult.None;
                                else
                                {
                                    this.targetNum = st.TargetNum; // 1:アイン、2:ラナ、3：ヴェルゼ、4：敵
                                    DialogResult = DialogResult.OK;
                                }
                            }
                            else if (we.AvailableFirstCharacter && we.AvailableSecondCharacter)
                            {
                                if (st.TargetNum == 0) DialogResult = DialogResult.None;
                                else
                                {
                                    if (st.TargetNum == 3)
                                    {
                                        this.targetNum = 4; // 敵
                                    }
                                    else
                                    {
                                        this.targetNum = st.TargetNum; // 1:アイン、2:ラナ
                                    }
                                    DialogResult = DialogResult.OK;
                                }
                            }
                            else if (we.AvailableFirstCharacter)
                            {
                                if (st.TargetNum == 0) DialogResult = DialogResult.None;
                                else
                                {
                                    if (st.TargetNum == 2)
                                    {
                                        this.targetNum = 4; // 敵
                                    }
                                    else
                                    {
                                        this.targetNum = st.TargetNum; // 1:アイン
                                    }
                                    DialogResult = DialogResult.OK;
                                }
                            }
                            else
                            {
                                DialogResult = DialogResult.OK;
                            }
                        }
                    }
                    else
                    {
                        DialogResult = DialogResult.OK;
                    }
                    break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.tryUseNum = -1;
            this.Close();
        }
    }
}