using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class TruthDecision2 : MotherForm // 後編編集
    {
        public bool SelectPermutation { get; set; } // 順番通り選択する場合はTrue
        private bool Permutation1 = false;
        private bool Permutation2 = false;
        private bool Permutation3 = false;
        private bool Permutation4 = false;

        public string Description { get; set; }
        public string AnswerTop { get; set; }
        public string AnswerLeft { get; set; }
        public string AnswerRight { get; set; }
        public string AnswerBottom { get; set; }

        public enum AnswerType
        {
            Top,
            Left,
            Right,
            Bottom
        }

        public AnswerType Answer { get; set; } // SelectPermutationがFalseの時のみ判断するフラグ

        public TruthDecision2()
        {
            InitializeComponent();
        }

        private void TruthDecision2_Load(object sender, EventArgs e)
        {
            mainMessage.Text = Description;
            button1.Text = AnswerTop;
            button2.Text = AnswerLeft;
            button3.Text = AnswerRight;
            button4.Text = AnswerBottom;

            if (SelectPermutation)
            {
                button1.Location = new Point(364, 275);
                button2.Location = new Point(216, 275);
                button3.Location = new Point(68, 275);
                button4.Location = new Point(512, 275);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.SelectPermutation)
            {
                if (button1.BackColor != Color.Red)
                {
                    button1.BackColor = Color.Red;
                    this.Permutation1 = true;
                    CheckPermutationEnd();
                }
            }
            else
            {
                this.Answer = AnswerType.Top;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.SelectPermutation)
            {
                if (button2.BackColor != Color.Red)
                {
                    button2.BackColor = Color.Red;
                    if (Permutation1)
                    {
                        Permutation2 = true;
                    }
                    CheckPermutationEnd();
                }
            }
            else
            {
                this.Answer = AnswerType.Left;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.SelectPermutation)
            {
                if (button3.BackColor != Color.Red)
                {
                    button3.BackColor = Color.Red;
                    if (Permutation2)
                    {
                        Permutation3 = true;
                    }
                    CheckPermutationEnd();
                }
            }
            else
            {
                this.Answer = AnswerType.Right;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.SelectPermutation)
            {
                if (button4.BackColor != Color.Red)
                {
                    button4.BackColor = Color.Red;
                    if (Permutation3)
                    {
                        Permutation4 = true;
                    }
                    CheckPermutationEnd();
                }
            }
            else
            {
                this.Answer = AnswerType.Bottom;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void CheckPermutationEnd()
        {
            if (button1.BackColor == Color.Red &&
                button2.BackColor == Color.Red &&
                button3.BackColor == Color.Red &&
                button4.BackColor == Color.Red)
            {
                if (Permutation1 && Permutation2 && Permutation3 && Permutation4)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
            }
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            FocusBackColorChange(sender);
        }
        private void button1_Enter(object sender, EventArgs e)
        {
            FocusBackColorChange(sender);
        }
        private void FocusBackColorChange(Object sender)
        {
            if (this.SelectPermutation)
            {
                if (((Button)sender).BackColor == Color.Red)
                {
                    return;
                }
            }

            foreach (Object btn in this.Controls)
            {
                if (btn.GetType() == typeof(Button))
                {
                    if (((Button)btn).Equals(sender))
                    {
                        ((Button)sender).BackColor = Color.NavajoWhite;
                    }
                    else
                    {
                        if (((Button)btn).BackColor != Color.Red)
                        {
                            ((Button)btn).BackColor = Color.AliceBlue;
                        }
                    }
                }
            }
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            FocurLeaveBackColorChange(sender);
        }
        private void button1_Leave(object sender, EventArgs e)
        {
            FocurLeaveBackColorChange(sender);
        }
        private void FocurLeaveBackColorChange(Object sender)
        {
            if (this.SelectPermutation)
            {
                return;
            }

            ((Button)sender).BackColor = Color.AliceBlue;
        }

    }
}
