# Lifelike

Animation library for .NET Framework and Windows Forms.

## Code Samples

Add a button to the current Control that moves to
a random location each time it is clicked.

Assumes there is an instance of `System.Random` called `_random`.

``` C#
var movingButton = new Button
            {
                Text = "Don't click me, I'm shy",
                AutoSize = true,
                Parent = this,
            };
            movingButton.Location = new Point(50, 50);
            movingButton.Click += (sender, e) =>
            {
                movingButton.AnimateMove(
                    new Point(
                        _random.Next(Width - movingButton.Width), 
                        _random.Next(Height - movingButton.Height)));
            };
```