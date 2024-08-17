using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Lifelike.Animations;
using Lifelike.Timing;
using Lifelike.Controls;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Lifelike
{
    public class MainForm : Form
    {        
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        readonly Control circle;
        readonly Button button;
        readonly ComboBox comboEasingFunction;
        readonly ComboBox comboAnimationType;
        readonly NumericUpDown numericDuration;

        private Point GetStartPosition(string animationType)
        {
            switch (animationType)
            {
                case "Path":
                    return new Point(Width / 2 + 100, (Height - circle.Height) / 2);
                case "Sequence":
                    return new Point(Width / 2 - 100, (Height - circle.Height) / 2);
                case "Action":
                    return new Point((Width - circle.Width) / 2, (Height - circle.Height) / 2);
                default:
                    return new Point((Width - circle.Width) / 2, (Height - circle.Height) / 2);
            }
        }

        public MainForm()
        {
            DoubleBuffered = true;
            Text = "Lifelike";
            Width = 800;
            Height = 600;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;

            button = new Button
            {
                Text = "Click me!",
                AutoSize = true,
                Parent = this,
                Name = "button",
            };
            button.Location = new Point(50, Height - (button.Height + 50));

            circle = new MyCircle
            {
                Width = 32,
                Height = 32,
                ForeColor = Color.SteelBlue,
                Parent = this,
                Name = "circle",
            };
            circle.Location = new Point((Width - circle.Width) / 2, (Height - circle.Height) / 2);

            button.Click += ButtonClick;

            comboEasingFunction = new ComboBox
            {
                Parent = this,
                Name = "comboEasingFunction",
                DropDownStyle = ComboBoxStyle.DropDownList,
            };
            comboEasingFunction.Items.AddRange(new object[]
            {
                "Linear",
                "EaseIn",
                "EaseOut",
                "EaseInOut",
                "Bounce",
            });
            comboEasingFunction.SelectedIndex = 3;
            comboEasingFunction.Location = new Point(button.Right + 10, button.Top);

            comboAnimationType = new ComboBox
            {
                Parent = this,
                Name = "comboAnimationType",
                DropDownStyle = ComboBoxStyle.DropDownList,
            };
            comboAnimationType.Items.AddRange(new object[]
            {
                "Path",
                "Sequence",
                "Action",
            });
            comboAnimationType.SelectedIndex = 0;
            comboAnimationType.Location = new Point(comboEasingFunction.Right + 10, button.Top);
            comboAnimationType.SelectedIndexChanged += AnimationTypeChanged;

            numericDuration = new NumericUpDown
            {
                Parent = this,
                Name = "numericDuration",
                Minimum = 100,
                Maximum = 10000,
                Value = 1000,
                Location = new Point(comboAnimationType.Right + 10, button.Top)
            };

            var anotherButton = new Button
            {
                Text = "Another button",
                AutoSize = true,
                Parent = this,
                Name = "anotherButton",
            };
            anotherButton.Location = new Point(50, button.Top - anotherButton.Height - 10);
            anotherButton.Click += (sender, e) =>
            {
                anotherButton.AnimateMove(new Point(_random.Next(Width - anotherButton.Width), _random.Next(Height - anotherButton.Height)));
            };
        }

        private readonly Random _random = new Random();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AllocConsole();
        }

        void AnimationTypeChanged(object sender, EventArgs e)
        {
            // move the circle to the start position
            var position = GetStartPosition(comboAnimationType.SelectedItem?.ToString());
            var circleAnimation = new MoveAnimation(circle, TimeSpan.FromSeconds(1), EasingFunctions.EaseOut, position);
            circleAnimation.Start();
            // make the button bounce a little to draw attention
            var buttonLocation = button.Location;
            button.Location = new Point(buttonLocation.X, buttonLocation.Y - 10);
            var buttonAnimation = new MoveAnimation(button, TimeSpan.FromSeconds(0.5), EasingFunctions.Bounce, buttonLocation);
            buttonAnimation.Start();
        }

        void ButtonClick(object sender, EventArgs e)
        {
            var duration = TimeSpan.FromMilliseconds((double)numericDuration.Value);
            Easing easingFunction = EasingFunctions.EaseInOut;

            switch (comboEasingFunction.SelectedItem)
            {
                case "Linear":
                    easingFunction = EasingFunctions.Linear;
                    break;
                case "EaseIn":
                    easingFunction = EasingFunctions.EaseIn;
                    break;
                case "EaseOut":
                    easingFunction = EasingFunctions.EaseOut;
                    break;
                case "EaseInOut":
                    easingFunction = EasingFunctions.EaseInOut;
                    break;
                case "Bounce":
                    easingFunction = EasingFunctions.Bounce;
                    break;
            }

            IAnimation animation = null;

            switch (comboAnimationType.SelectedItem)
            {
                default:
                case "Path":
                {
                    var path = new GraphicsPath();
                    path.AddEllipse(Width / 2 - 100, Height / 2 - 100, 200, 200);
                    animation = new PathAnimation(circle, duration, easingFunction, path);
                }
                    break;
                case "Sequence":
                    var animations = new List<IAnimation>
                    {
                        new MoveAnimation(circle, duration, easingFunction, new Point(Width / 2 - 100, (Height - circle.Height) / 2)),
                        new PathAnimation(circle, duration, easingFunction, CreateHeartPath(new Point(Width / 2 - 100, (Height - circle.Height) / 2))),
                        new MoveAnimation(circle, duration, easingFunction, new Point(Width / 2, (Height - circle.Height) / 2)),
                    };
                    animation = new SequenceAnimation(animations);
                    break;
                case "Action":
                    animation = new ActionAnimation(circle, duration, easingFunction, (control, progress) =>
                    {
                        // cycle the color of the circle
                        var c = (int)(progress * 255);
                        control.ForeColor = Color.FromArgb(c, 255 - c, 255 - c);
                    })
                    {
                        Repetition = AnimationRepetition.Reverse,
                    };
                    break;
            }

            animation.Start();
        }

        private GraphicsPath CreateHeartPath(Point startAndEnd)
        {
            var path = new GraphicsPath();
            path.StartFigure();
            path.AddBezier(startAndEnd.X, startAndEnd.Y + 10, startAndEnd.X + 10, startAndEnd.Y - 10, startAndEnd.X + 30, startAndEnd.Y + 10, startAndEnd.X, startAndEnd.Y + 30);
            path.AddBezier(startAndEnd.X, startAndEnd.Y + 30, startAndEnd.X - 30, startAndEnd.Y + 10, startAndEnd.X - 10, startAndEnd.Y - 10, startAndEnd.X, startAndEnd.Y + 10);
            path.CloseFigure();
            return path;
        }
    }
}