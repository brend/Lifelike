using System.Windows.Forms;

namespace Lifelike
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
            e.Graphics.FillEllipse(System.Drawing.Brushes.SteelBlue, 0, 0, Width, Height);
        }
    }
}