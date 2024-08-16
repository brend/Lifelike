using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Lifelike.Animations;
using Lifelike.Timing;

namespace Lifelike
{
    public class MainForm : Form
    {
        readonly Control circle;
        readonly Button button;
        readonly ComboBox comboEasingFunction;
        readonly ComboBox comboAnimationType;
        readonly NumericUpDown numericDuration;

        private Point GetStartPosition(string animationType)
        {
            switch (animationType)
            {
                case "Slide":
                    return new Point((Width - circle.Width) / 2, (Height - circle.Height) / 2);
                case "Path":
                    return new Point(Width / 2 + 100, (Height - circle.Height) / 2);
                case "Sequence":
                    return new Point(Width / 2 - 100, (Height - circle.Height) / 2);
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
            BackColor = System.Drawing.Color.White;

            button = new Button
            {
                Text = "Click me!",
                AutoSize = true,
                Parent = this,
                Name = "button",
            };
            button.Location = new System.Drawing.Point(50, Height - (button.Height + 50));

            circle = new MyCircle
            {
                Width = 32,
                Height = 32,
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
            comboEasingFunction.Location = new System.Drawing.Point(button.Right + 10, button.Top);

            comboAnimationType = new ComboBox
            {
                Parent = this,
                Name = "comboAnimationType",
                DropDownStyle = ComboBoxStyle.DropDownList,
            };
            comboAnimationType.Items.AddRange(new object[]
            {
                "Slide",
                "Path",
                "Sequence",
            });
            comboAnimationType.SelectedIndex = 0;
            comboAnimationType.Location = new System.Drawing.Point(comboEasingFunction.Right + 10, button.Top);
            comboAnimationType.SelectedIndexChanged += AnimationTypeChanged;

            numericDuration = new NumericUpDown
            {
                Parent = this,
                Name = "numericDuration",
                Minimum = 100,
                Maximum = 10000,
                Value = 1000,
            };
            numericDuration.Location = new System.Drawing.Point(comboAnimationType.Right + 10, button.Top);
        }

        void AnimationTypeChanged(object sender, EventArgs e)
        {
            // move the circle to the start position
            var position = GetStartPosition(comboAnimationType.SelectedItem?.ToString());
            var circleAnimation = new MoveAnimation(circle, position, EasingFunctions.EaseInOut(TimeSpan.FromMilliseconds(1000)));
            circleAnimation.Start();
            // make the button bounce a little to draw attention
            var buttonLocation = button.Location;
            button.Location = new Point(buttonLocation.X, buttonLocation.Y - 10);
            var buttonAnimation = new MoveAnimation(button, buttonLocation, EasingFunctions.Bounce(TimeSpan.FromMilliseconds(500)));
            buttonAnimation.Start();
        }

        void ButtonClick(object sender, EventArgs e)
        {
            var duration = TimeSpan.FromMilliseconds((double)numericDuration.Value);
            Easing easingFunction = EasingFunctions.EaseInOut(duration);

            switch (comboEasingFunction.SelectedItem)
            {
                case "Linear":
                    easingFunction = EasingFunctions.Linear(duration);
                    break;
                case "EaseIn":
                    easingFunction = EasingFunctions.EaseIn(duration);
                    break;
                case "EaseOut":
                    easingFunction = EasingFunctions.EaseOut(duration);
                    break;
                case "EaseInOut":
                    easingFunction = EasingFunctions.EaseInOut(duration);
                    break;
                case "Bounce":
                    easingFunction = EasingFunctions.Bounce(duration);
                    break;
            }

            IAnimation animation = null;

            switch (comboAnimationType.SelectedItem)
            {
                default:
                case "Slide":
                    animation = new SlideAnimation(circle, easingFunction);
                    break;
                case "Path":
                {
                    var path = new GraphicsPath();
                    path.AddEllipse(Width / 2 - 100, Height / 2 - 100, 200, 200);
                    animation = new PathAnimation(circle, easingFunction, path);
                }
                    break;
                case "Sequence":
                    var animations = new List<IAnimation>
                    {
                        new MoveAnimation(circle, new Point(Width / 2 - 100, (Height - circle.Height) / 2), easingFunction),
                        new PathAnimation(circle, easingFunction, CreateHeartPath(new Point(Width / 2 - 100, (Height - circle.Height) / 2))),
                        new MoveAnimation(circle, new Point(Width / 2, (Height - circle.Height) / 2), easingFunction),
                    };
                    animation = new SequenceAnimation(animations);
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