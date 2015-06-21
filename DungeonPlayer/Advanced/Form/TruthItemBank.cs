using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class TruthItemBank : MotherForm
    {
        protected MainCharacter mc;
        public MainCharacter MC
        {
            get { return mc; }
            set { mc = value; }
        }
        protected MainCharacter sc;
        public MainCharacter SC
        {
            get { return sc; }
            set { sc = value; }
        }
        protected MainCharacter tc;
        public MainCharacter TC
        {
            get { return tc; }
            set { tc = value; }
        }

        protected WorldEnvironment we;
        public WorldEnvironment WE
        {
            get { return we; }
            set { we = value; }
        }

        const int MAX_VIEW_PAGE = 10;
        const int MAX_PLAYER_LIST_PAGE = 2;

        Button[] baseListPage = new Button[MAX_VIEW_PAGE];
        Button[] p_baseListPage = new Button[MAX_PLAYER_LIST_PAGE];

        Button[] b_items = new Button[MAX_VIEW_PAGE];
        Button[] p_items = new Button[MAX_VIEW_PAGE];
        Label[] b_stacks = new Label[MAX_VIEW_PAGE];
        Label[] p_stacks = new Label[MAX_VIEW_PAGE];

        string[] items = new string[Database.MAX_ITEM_BANK];
        int[] stacks = new int[Database.MAX_ITEM_BANK];

        int currentListPage = 1; // 初めページ番号が１、それをデフォルトとする。
        int p_currentListPage = 1;
        MainCharacter currentPlayer = null;
        Button currentPlayerItem = null;
        Button currentBankItem = null;

        int shadowPage1 = 1;
        int shadowPage2 = 1;
        int shadowPage3 = 1;

        public TruthItemBank()
        {
            InitializeComponent();
            baseListPage[0] = PageNumber1;
            baseListPage[1] = PageNumber2;
            baseListPage[2] = PageNumber3;
            baseListPage[3] = PageNumber4;
            baseListPage[4] = PageNumber5;
            baseListPage[5] = PageNumber6;
            baseListPage[6] = PageNumber7;
            baseListPage[7] = PageNumber8;
            baseListPage[8] = PageNumber9;
            baseListPage[9] = PageNumber10;

            p_baseListPage[0] = p_PageNumber1;
            p_baseListPage[1] = p_PageNumber2;

            b_items[0] = target1;
            b_items[1] = target2;
            b_items[2] = target3;
            b_items[3] = target4;
            b_items[4] = target5;
            b_items[5] = target6;
            b_items[6] = target7;
            b_items[7] = target8;
            b_items[8] = target9;
            b_items[9] = target10;

            p_items[0] = p_item1;
            p_items[1] = p_item2;
            p_items[2] = p_item3;
            p_items[3] = p_item4;
            p_items[4] = p_item5;
            p_items[5] = p_item6;
            p_items[6] = p_item7;
            p_items[7] = p_item8;
            p_items[8] = p_item9;
            p_items[9] = p_item10;

            b_stacks[0] = b_stack1;
            b_stacks[1] = b_stack2;
            b_stacks[2] = b_stack3;
            b_stacks[3] = b_stack4;
            b_stacks[4] = b_stack5;
            b_stacks[5] = b_stack6;
            b_stacks[6] = b_stack7;
            b_stacks[7] = b_stack8;
            b_stacks[8] = b_stack9;
            b_stacks[9] = b_stack10;

            p_stacks[0] = p_stack1;
            p_stacks[1] = p_stack2;
            p_stacks[2] = p_stack3;
            p_stacks[3] = p_stack4;
            p_stacks[4] = p_stack5;
            p_stacks[5] = p_stack6;
            p_stacks[6] = p_stack7;
            p_stacks[7] = p_stack8;
            p_stacks[8] = p_stack9;
            p_stacks[9] = p_stack10;
        }

        private void TruthItemBank_Load(object sender, EventArgs e)
        {
            if (!we.AvailableSecondCharacter) player2Button.Visible = false;
            if (!we.AvailableThirdCharacter) player3Button.Visible = false;

            we.LoadItemBankData(ref items, ref stacks);

            PageNumber1_Click(PageNumber1, null);

            player1Button_Click(player1Button, null);
            p_PageNumber1_Click(p_PageNumber1, null);
        }


        private void TruthItemBank_FormClosing(object sender, FormClosingEventArgs e)
        {
            we.UpdateItemBankData(items, stacks);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void PageNumber1_Click(object sender, EventArgs e)
        {
            this.currentListPage = Convert.ToInt32(((Button)sender).Text);

            for (int ii = 0; ii < MAX_VIEW_PAGE; ii++)
            {
                if (baseListPage[ii].Equals((Button)sender))
                {
                    baseListPage[ii].BackColor = Color.Yellow;
                }
                else
                {
                    baseListPage[ii].BackColor = Color.LightYellow;
                }
            }
            if (this.currentBankItem != null)
            {
                this.currentBankItem.BackColor = Color.GhostWhite;
                this.currentBankItem = null;
            }

            for (int ii = 0; ii < MAX_VIEW_PAGE; ii++)
            {
                b_items[ii].Text = items[(this.currentListPage - 1) * MAX_VIEW_PAGE + ii];
                if (b_items[ii].Text == null || b_items[ii].Text == string.Empty || b_items[ii].Text == "")
                {
                    b_stacks[ii].Text = "";
                }
                else
                {
                    b_stacks[ii].Text = "x" + stacks[(this.currentListPage - 1) * MAX_VIEW_PAGE + ii].ToString();
                }
            }
        }

        private void p_PageNumber1_Click(object sender, EventArgs e)
        {
            this.p_currentListPage = Convert.ToInt32(((Button)sender).Text);

            for (int ii = 0; ii < MAX_PLAYER_LIST_PAGE; ii++)
            {
                if (p_baseListPage[ii].Equals((Button)sender))
                {
                    p_baseListPage[ii].BackColor = Color.Yellow;
                }
                else
                {
                    p_baseListPage[ii].BackColor = Color.LightYellow;
                }
            }

            if (this.currentPlayerItem != null)
            {
                if (this.currentPlayer == mc)
                {
                    this.currentPlayerItem.BackColor = Color.LightSkyBlue;
                }
                else if (this.currentPlayer == sc)
                {
                    this.currentPlayerItem.BackColor = Color.Pink;
                }
                else if (this.currentPlayer == tc)
                {
                    this.currentPlayerItem.BackColor = Color.Gold;
                }
                this.currentPlayerItem = null;
            }

            if (this.currentPlayer == mc)
            {
                shadowPage1 = this.p_currentListPage;
            }
            else if (this.currentPlayer == sc)
            {
                shadowPage2 = this.p_currentListPage;
            }
            else if (this.currentPlayer == tc)
            {
                shadowPage3 = this.p_currentListPage;
            }

            if (this.currentPlayer != null)
            {
                ItemBackPack[] info = this.currentPlayer.GetBackPackInfo();
                string[] items = new string[Database.MAX_BACKPACK_SIZE];
                int[] stacks = new int[Database.MAX_BACKPACK_SIZE];
                for(int ii = 0; ii < info.Length; ii++)
                {
                    if (info[ii] != null)
                    {
                        items[ii] = info[ii].Name;
                        stacks[ii] = info[ii].StackValue;
                    }
                }
                for (int ii = 0; ii < MAX_VIEW_PAGE; ii++)
                {
                    p_items[ii].Text = items[(this.p_currentListPage - 1) * MAX_VIEW_PAGE + ii];
                    if (p_items[ii].Text == null || p_items[ii].Text == string.Empty || p_items[ii].Text == "")
                    {
                        p_stacks[ii].Text = "";
                    }
                    else
                    {
                        p_stacks[ii].Text = "x" + stacks[(this.p_currentListPage - 1) * MAX_VIEW_PAGE + ii].ToString();
                    }
                }
            }
        }

        private void player1Button_Click(object sender, EventArgs e)
        {
            if (((Button)sender).BackColor == Color.Blue)
            {
                this.currentPlayer = mc;
                p_item1.BackColor = Color.LightSkyBlue;
                p_item2.BackColor = Color.LightSkyBlue;
                p_item3.BackColor = Color.LightSkyBlue;
                p_item4.BackColor = Color.LightSkyBlue;
                p_item5.BackColor = Color.LightSkyBlue;
                p_item6.BackColor = Color.LightSkyBlue;
                p_item7.BackColor = Color.LightSkyBlue;
                p_item8.BackColor = Color.LightSkyBlue;
                p_item9.BackColor = Color.LightSkyBlue;
                p_item10.BackColor = Color.LightSkyBlue;
                this.p_currentListPage = shadowPage1;
            }
            else if (((Button)sender).BackColor == Color.Red)
            {
                this.currentPlayer = sc;
                p_item1.BackColor = Color.Pink;
                p_item2.BackColor = Color.Pink;
                p_item3.BackColor = Color.Pink;
                p_item4.BackColor = Color.Pink;
                p_item5.BackColor = Color.Pink;
                p_item6.BackColor = Color.Pink;
                p_item7.BackColor = Color.Pink;
                p_item8.BackColor = Color.Pink;
                p_item9.BackColor = Color.Pink;
                p_item10.BackColor = Color.Pink;
                this.p_currentListPage = shadowPage2;
            }
            else if (((Button)sender).BackColor == Color.Yellow)
            {
                this.currentPlayer = tc;
                p_item1.BackColor = Color.Gold;
                p_item2.BackColor = Color.Gold;
                p_item3.BackColor = Color.Gold;
                p_item4.BackColor = Color.Gold;
                p_item5.BackColor = Color.Gold;
                p_item6.BackColor = Color.Gold;
                p_item7.BackColor = Color.Gold;
                p_item8.BackColor = Color.Gold;
                p_item9.BackColor = Color.Gold;
                p_item10.BackColor = Color.Gold;
                this.p_currentListPage = shadowPage3;
            }

            if (this.p_currentListPage == 1) p_PageNumber1_Click(p_PageNumber1, null);
            else if (this.p_currentListPage == 2) p_PageNumber1_Click(p_PageNumber2, null);
        }

        private void FromBackpackToPlayer_Click(object sender, EventArgs e)
        {
            if (this.currentBankItem == null) return; // 空選択は何もしない。
            if (this.currentBankItem.Text == String.Empty || this.currentBankItem.Text == "" || this.currentBankItem.Text == null) return;

            int stackValue = 0;
            int targetDeleteNum = 0;
            for (int ii = 0; ii < MAX_VIEW_PAGE; ii++)
            {
                if (this.currentBankItem.Equals(b_items[ii]))
                {
                    targetDeleteNum = (this.currentListPage - 1) * 10 + ii;
                    stackValue = stacks[targetDeleteNum];
                    break;
                }
            }

            int addedNumber = 0;
            bool result = this.currentPlayer.AddBackPack(new ItemBackPack(this.currentBankItem.Text), stackValue, ref addedNumber);
            if (result == false)
            {
                mainMessage.Text = currentPlayer.GetCharacterSentence(2034);
                return;
            }

            if (this.p_currentListPage == 1) p_PageNumber1_Click(p_PageNumber1, null);
            else if (this.p_currentListPage == 2) p_PageNumber1_Click(p_PageNumber2, null);

            this.currentBankItem.Text = string.Empty;
            for (int ii = 0; ii < MAX_VIEW_PAGE; ii++)
            {
                if (this.currentBankItem.Equals(b_items[ii]))
                {
                    b_stacks[ii].Text = "";
                }
            }
            items[targetDeleteNum] = string.Empty;
            stacks[targetDeleteNum] = 0;

            this.p_currentListPage = (addedNumber / MAX_VIEW_PAGE) + 1;
            p_PageNumber1_Click(p_baseListPage[this.p_currentListPage - 1], null);
            p_item1_Click(p_items[addedNumber % MAX_VIEW_PAGE], null);
        }

        private void FromPlayerToBackpack_Click(object sender, EventArgs e)
        {
            if (this.currentPlayerItem == null) return; // 空選択は何もしない。
            if (this.currentPlayerItem.Text == String.Empty || this.currentPlayerItem.Text == "" || this.currentPlayerItem.Text == null) return;
            if (TruthItemAttribute.CheckImportantItem(this.currentPlayerItem.Text) != TruthItemAttribute.Transfer.Any)
            {
                mainMessage.Text = this.currentPlayer.GetCharacterSentence(2033);
                return;
            }

            int stackValue = 0;
            for (int ii = 0; ii < MAX_VIEW_PAGE; ii++)
            {
                if (this.currentPlayerItem.Equals(p_items[ii]))
                {
                    stackValue = this.currentPlayer.CheckBackPackExist(new ItemBackPack(this.currentPlayerItem.Text), (this.p_currentListPage - 1) * 10 + ii);
                    break;
                }
            }

            this.currentPlayer.DeleteBackPack(new ItemBackPack(((Button)this.currentPlayerItem).Text));

            for (int ii = 0; ii < Database.MAX_ITEM_BANK; ii++)
            {
                if ((items[ii] == null) || (items[ii] == String.Empty) || (items[ii] == ""))
                {
                    items[ii] = ((Button)this.currentPlayerItem).Text;
                    stacks[ii] = stackValue;
                    this.currentPlayerItem.Text = string.Empty;
                    for (int jj = 0; jj < MAX_VIEW_PAGE; jj++)
                    {
                        if (this.currentPlayerItem.Equals(p_items[jj]))
                        {
                            p_stacks[jj].Text = "";
                        }
                    }

                    currentListPage = (ii / MAX_VIEW_PAGE) + 1;
                    PageNumber1_Click(baseListPage[this.currentListPage-1], null);
                    target1_Click(b_items[ii % MAX_VIEW_PAGE], null);
                    return; // 余計なFor回しはせず終了する。
                }
            }
        }

        private void p_item1_Click(object sender, EventArgs e)
        {
            this.currentPlayerItem = (Button)sender;
            for (int ii = 0; ii < MAX_VIEW_PAGE; ii++)
            {
                if (sender.Equals(p_items[ii]))
                {
                    ((Button)sender).BackColor = Color.Yellow;
                }
                else
                {
                    if (this.currentPlayer == mc)
                    {
                        p_items[ii].BackColor = Color.LightSkyBlue;
                    }
                    else if (this.currentPlayer == sc)
                    {
                        p_items[ii].BackColor = Color.Pink;
                    }
                    else if (this.currentPlayer == tc)
                    {
                        p_items[ii].BackColor = Color.Gold;
                    }
                }
            }
        }

        private void target1_Click(object sender, EventArgs e)
        {
            this.currentBankItem = (Button)sender;
            for (int ii = 0; ii < MAX_VIEW_PAGE; ii++)
            {
                if (sender.Equals(b_items[ii]))
                {
                    ((Button)sender).BackColor = Color.Yellow;
                }
                else
                {
                    b_items[ii].BackColor = Color.GhostWhite;
                }
            }
        }

    }
}
