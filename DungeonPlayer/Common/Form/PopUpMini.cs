using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class PopUpMini : MotherForm
    {
        private string currentInfo = String.Empty;
        public string CurrentInfo
        {
            get { return currentInfo; }
            set { currentInfo = value; }
        }

        private Color popupColor = Color.White;
        public Color PopupColor
        {
            get { return popupColor; }
            set { popupColor = value; }
        }

        private SolidBrush popupTextColor = new SolidBrush(Color.White); // 後編編集、ただし後編完成後、前編操作で不具合があれば、再度修正。
        public SolidBrush PopupTextColor
        {
            get { return popupTextColor; }
            set { popupTextColor = value; }
        }

//        private Font fontData = new Font("Arial", 14.0F, FontStyle.Regular, GraphicsUnit.Pixel, 128, true);
        private Font fontData = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
        public Font FontFamilyName
        {
            get { return fontData; }
            set { fontData = value; }
        }
	
        Graphics g ;

        public PopUpMini()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            g = CreateGraphics();
        }

        private void PopUpMini_Paint(object sender, PaintEventArgs e)
        {
            g.DrawString(currentInfo, fontData, popupTextColor, 0, 0); //  new RectangleF(0, 0, (float)this.Width, (float)this.Height));
        }

        private void PopUpMini_Load(object sender, EventArgs e)
        {
            if (currentInfo != null && currentInfo.Length > 0)
            {
                Size size = g.MeasureString(currentInfo, fontData).ToSize();
                this.Width = size.Width;
                this.Height = size.Height;
            }

            this.BackColor = this.popupColor;
        }
    }
}