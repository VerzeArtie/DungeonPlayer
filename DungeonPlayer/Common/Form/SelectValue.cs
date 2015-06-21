using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class SelectValue : MotherForm // 後編編集
    {
        private int maxValue = Database.MAX_ITEM_STACK_SIZE; // まずはバックパック移動の最大数を対象にしたものとする。
        public int MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; }
        }

        private int currentValue = 1; // 対象の物が最低一つ存在することを明示的に示すため、１とする。
        public int CurrentValue
        {
            get { return currentValue; }
            set { currentValue = value; }
        }

        public SelectValue()
        {
            InitializeComponent();
        }

        private void SelectValue_Load(object sender, EventArgs e)
        {
            button1.Image = Image.FromFile(Database.BaseResourceFolder + "Enter.bmp");
            textBox1.Text = currentValue.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (UpdateCurrentValue())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private bool UpdateCurrentValue()
        {
            try
            {
                this.currentValue = Convert.ToInt32(textBox1.Text);
                if (currentValue > maxValue)
                {
                    currentValue = maxValue;
                    this.textBox1.Text = currentValue.ToString();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private void textBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (UpdateCurrentValue())
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void SelectValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
            }

        }
    }
}