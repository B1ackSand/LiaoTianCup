using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace LiaoTian_Cup.Helper
{
    public partial class MyImage : System.Windows.Controls.Image
    {
        public MyImage()
        {
        }
        public bool IsSelect;//是否选中
        /// <summary>
        /// 重绘
        /// </summary>
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            if (IsSelect)
            {
                try
                {
                    //绘制边框
                    Rect rect = new Rect(0, 0, this.ActualWidth, this.ActualHeight);
                    Pen p = new Pen(new SolidColorBrush(Colors.Red), 2);
                    p.DashStyle = DashStyles.Dot;
                    p.StartLineCap = PenLineCap.Triangle;
                    dc.DrawRectangle(Brushes.Transparent, p, rect);
                }
                catch { }
            }
        }
    }
}
