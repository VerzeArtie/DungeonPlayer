using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class SelectValue : MotherForm // ��ҕҏW
    {
        private int maxValue = Database.MAX_ITEM_STACK_SIZE; // �܂��̓o�b�N�p�b�N�ړ��̍ő吔��Ώۂɂ������̂Ƃ���B
        public int MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; }
        }

        private int currentValue = 1; // �Ώۂ̕����Œ����݂��邱�Ƃ𖾎��I�Ɏ������߁A�P�Ƃ���B
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