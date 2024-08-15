using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Lifelike.Timing;

namespace Lifelike
{
    public class MainForm : Form
    {
        public MainForm()
        {
            DoubleBuffered = true;
            Text = "Lifelike";
            Width = 800;
            Height = 600;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = System.Drawing.Color.White;

            var progressBar = new MyBar
            {
                Minimum = 0,
                Maximum = 100,
                Value = 0,
                Width = 200,
                Height = 20,
                Location = new System.Drawing.Point(Width / 2 - 100, Height / 2 + 100),
                Parent = this
            };

            var button = new Button
            {
                Text = "Click me!",
                AutoSize = true,
                Location = new System.Drawing.Point(Width / 2, Height / 2 + 50),
                Parent = this,
                Name = "button",
            };

            var circle = new MyCircle
            {
                Location = new System.Drawing.Point(Width / 2 - 50, Height / 2 - 50),
                Width = 32,
                Height = 32,
                Parent = this,
                Name = "circle",
            };
        }

        Animations.AnimationBase animation;

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            var circle = Controls["circle"];
            var position = PointToClient(MousePosition);
            var path = new GraphicsPath();
            var radius = 100;
            
            // var transformation = new Matrix();
            // transformation.RotateAt(45, position);
            // path.AddRectangle(new System.Drawing.Rectangle(position.X - radius, position.Y - radius, 2 * radius, 2 * radius));
            // path.Transform(transformation);

            path.AddEllipse(position.X - radius, position.Y - radius, 2 * radius, 2 * radius);

            animation = new Animations.PathAnimation(circle, TimingFunctions.Bounce(TimeSpan.FromSeconds(3)), path);
            animation.Start();
        }
    }
}