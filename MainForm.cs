using System.Windows.Forms;

namespace Lifelike
{
    public class MainForm : Form
    {
        public MainForm()
        {
            Text = "Lifelike";
            Width = 800;
            Height = 600;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = System.Drawing.Color.White;

            var progressBar = new ProgressBar
            {
                Minimum = 0,
                Maximum = 100,
                Value = 0,
                Width = 200,
                Height = 20,
                Location = new System.Drawing.Point(Width / 2 - 100, Height / 2 + 100),
                Parent = this
            };

            var timer = new AnimationTimer();
            timer.Tick += (sender, e) =>
            {
                this.Text = $"Progress: {timer.Progress:P0}";
                progressBar.Value = (int)(timer.Progress * 100);
                if (timer.Progress >= 1)
                {
                    end = System.DateTime.Now;
                    MessageBox.Show($"Animation took {(end - start).TotalMilliseconds} ms");
                }
            };

            var button = new Button
            {
                Text = "Click me!",
                AutoSize = true,
                Location = new System.Drawing.Point(Width / 2, Height / 2 + 50),
                Parent = this
            };

            button.Click += (sender, e) =>
            {
                start = System.DateTime.Now;
                timer.Start();
            };
        }

        System.DateTime start, end;
    }
}