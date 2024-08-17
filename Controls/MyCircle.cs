using System.Drawing;
using System.Windows.Forms;

namespace Lifelike.Controls
{
    public class MyCircle : Control
    {
        public MyCircle()
        {
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var brush = new SolidBrush(ForeColor))
                e.Graphics.FillEllipse(brush, 0, 0, Width, Height);
        }
    }
}