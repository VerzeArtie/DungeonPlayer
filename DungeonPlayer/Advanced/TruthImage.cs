using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DungeonPlayer
{
    public class TruthImage : System.Windows.Forms.PictureBox
    {
        public enum buffType
        {
            None,
            Small,
            Large,
        }
        public enum CumulativeTextAlign
        {
            TopRight,
            Center,
        }

        protected buffType buffMode = buffType.None;
        public buffType BuffMode
        {
            get { return buffMode; }
            set
            {
                buffMode = value;
                if (value == buffType.Large)
                {
                    this.fixedRect = new Rectangle(0, 0, 50, 50);
                    this.fixedPointCount = new PointF(4, 47);
                }
            }
        }
        
        protected string imageName = String.Empty;
        public string ImageName
        {
            get { return imageName; }
            set { imageName = value; }
        }

        protected int count = 0;
        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        protected int cumulative = 0;
        public int Cumulative
        {
            get { return cumulative; }
            set { if (value >= 0) { cumulative = value; } }
        }
        protected CumulativeTextAlign align = CumulativeTextAlign.TopRight;
        public CumulativeTextAlign CumulativeAlign
        {
            get { return align; }
            set { align = value; }
        }

        Rectangle fixedRect = new Rectangle(0, 0, 25, 25);
        PointF fixedPointCount = new PointF(4, 22);
        PointF fixedPointCumulative = new PointF(12, -3);
        PointF fixedPointCumulative_2 = new PointF(4, -3);
        PointF fixedPointCumulativeCenter = new PointF(6, 4);
        Font fixedFontCount = new Font("Arial", 11, FontStyle.Bold, GraphicsUnit.Point, (byte)(128));
        Font fixedFontCumulative = new Font("Times New Roman", 14, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, (byte)(128));
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs pe)
        {
            if (buffMode == buffType.None)
            {
                base.OnPaint(pe);
            }
            else
            {
                System.Drawing.Graphics e = pe.Graphics;
                if (this.Image != null)
                {
                    e.DrawImage(this.Image, this.fixedRect);
                }

                if (0 < count && count <= 99)
                {
                    e.DrawString(count.ToString(), fixedFontCount, Brushes.DarkGreen, fixedPointCount);
                }

                if (cumulative > 0)
                {
                    if (this.align == CumulativeTextAlign.TopRight)
                    {

                        if (cumulative >= 10)
                        {
                            e.DrawString(cumulative.ToString(), fixedFontCumulative, Brushes.Black, fixedPointCumulative_2);
                        }
                        else
                        {
                            e.DrawString(cumulative.ToString(), fixedFontCumulative, Brushes.Black, fixedPointCumulative);
                        }
                    }
                    else
                    {
                        e.DrawString(cumulative.ToString(), fixedFontCumulative, Brushes.Black, fixedPointCumulativeCenter);
                    }

                }
            }
        }


        public bool AbstractCountDownBuff()
        {
            if (this.Count > 0)
            {
                this.Count--;
                this.Invalidate();
                if (this.Count <= 0)
                {
                    this.Image = null;
                    this.Update();
                    return true; // BUFF効果が切れた事を通達する。
                }
            }
            return false;
        }
    }
}
