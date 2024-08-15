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

            var label = new Label
            {
                Text = "Hello, Lifelike!",
                AutoSize = true,
                Font = new System.Drawing.Font("Arial", 24),
                ForeColor = System.Drawing.Color.Black,
                Location = new System.Drawing.Point(Width / 2, Height / 2),
                BackColor = System.Drawing.Color.Bisque,
                Parent = this
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
                Animations.SlideAnimation.SlideInFromLeft(label);
            };
        }
    }
}