using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    /// <summary>
    /// 薬の説明が本クラスの始まり。今後、武具系、宿屋新レシピなども解説可能な画面にしていく。
    /// </summary>
    public partial class TruthItemDesc : MotherForm // 後編編集
    {
        public string ItemNameTitle { get; set; }
        public string Description { get; set; }
        public string ItemNameButton { get; set; }
        public string ItemNameButtonSentence { get; set; }

        public TruthItemDesc()
        {
            InitializeComponent();
            this.ItemNameButtonSentence = "が新しく入荷しました！";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TruthItemDesc_Load(object sender, EventArgs e)
        {
            this.labelTitle.Text = ItemNameTitle;
            this.labelDescription.Text = Description;
            button2.Text = ItemNameButton + ItemNameButtonSentence;
        }
    }
}
