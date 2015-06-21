using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class RequestInput : MotherForm
    {
        private string inputData = string.Empty;
        public string InputData
        {
            get { return inputData; }
            set { inputData = value; }
        }

        public RequestInput()
        {
            InitializeComponent();
        }

        private void RequestInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.inputData = textBox1.Text;
                this.Close();
            }
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void RequestInput_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = inputData;
        }
    }
}