using System;
using System.Windows.Forms;
using Lifelike.Timing;

namespace Lifelike
{
    public class MainForm : Form
    {
        AnimationTimer timer;

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

            timer = new AnimationTimer(TimingFunctions.Bounce(TimeSpan.FromSeconds(1)));
            timer.Tick += (sender, e) =>
            {
                this.Text = $"Progress: {timer.Progress:P0}";
                progressBar.Value = (int)(timer.Progress * 100);
                if (timer.Progress >= 1)
                {
                    end = System.DateTime.Now;
                    Text = $"Animation took {(end - start).TotalMilliseconds} ms";
                }
            };

            var button = new Button
            {
                Text = "Click me!",
                AutoSize = true,
                Location = new System.Drawing.Point(Width / 2, Height / 2 + 50),
                Parent = this,
                Name = "button",
            };

            button.Click += (sender, e) =>
            {
                start = System.DateTime.Now;
                //timer.Start();
                progressBar.Value = 100;
                new Animations.SlideAnimation(progressBar, TimingFunctions.Bounce(TimeSpan.FromSeconds(1)));
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

        System.DateTime start, end;

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            var circle = Controls["circle"];
            var position = PointToClient(MousePosition) - new System.Drawing.Size(circle.Width / 2, circle.Height / 2);
            new Animations.MoveAnimation(circle, position, TimingFunctions.EaseOut(TimeSpan.FromSeconds(1)));
        }
    }
}