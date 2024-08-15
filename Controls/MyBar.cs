using System.Windows.Forms;

namespace Lifelike
{
    public class MyBar : Control
    {
        public int Minimum { get; set; }
        public int Maximum { get; set; }
        private int _value;
        public int Value { get => _value; set { _value = value; Invalidate(); } }

        public MyBar()
        {
            DoubleBuffered = true;
            Minimum = 0;
            Maximum = 100;
            Value = 0;
            Width = 200;
            Height = 20;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.FillRectangle(System.Drawing.Brushes.Blue, 0, 0, (int)(Width * Value / 100.0), Height);
            e.Graphics.DrawRectangle(System.Drawing.Pens.Black, 0, 0, Width - 1, Height - 1);
        }
    }
}