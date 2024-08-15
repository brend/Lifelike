using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace Lifelike.Animations
{
    public class PathAnimation : AnimationBase
    {
        protected readonly GraphicsPath _path;

        public PathAnimation(Control control, Timing.Timing timing, GraphicsPath path)
            : base(control, timing)
        {
            _path = path;
        }

        protected override void Update()
        {
            var point = PathHelper.GetPointAtPercentage(_path, (float)Progress);
            File.AppendAllText("log.txt", $"{point.X}, {point.Y}\n");
            Control.Location = new Point((int)point.X, (int)point.Y);
        }
    }

    public class PathHelper
    {
        public static PointF GetPointAtPercentage(GraphicsPath path, float percentage)
        {
            if (percentage < 0 || percentage > 1)
                throw new ArgumentOutOfRangeException("percentage");

            float totalLength = 0;
            float[] segmentLengths = new float[path.PointCount - 1];

            // Calculate total length of the path and lengths of each segment
            for (int i = 0; i < path.PointCount - 1; i++)
            {
                float segmentLength = Distance(path.PathPoints[i], path.PathPoints[i + 1]);
                segmentLengths[i] = segmentLength;
                totalLength += segmentLength;
            }

            // Find the target length corresponding to the percentage
            float targetLength = percentage * totalLength;

            // Iterate through segments to find the point at the target length
            float cumulativeLength = 0;
            for (int i = 0; i < segmentLengths.Length; i++)
            {
                cumulativeLength += segmentLengths[i];
                if (cumulativeLength >= targetLength)
                {
                    // Calculate how far we are within this segment
                    float segmentStartLength = cumulativeLength - segmentLengths[i];
                    float remainingLength = targetLength - segmentStartLength;

                    float t = remainingLength / segmentLengths[i];

                    // Interpolate the point on the segment
                    return Lerp(path.PathPoints[i], path.PathPoints[i + 1], t);
                }
            }

            // Return the last point if percentage is exactly 100%
            return path.PathPoints[path.PointCount - 1];
        }

        private static float Distance(PointF p1, PointF p2)
        {
            return (float)Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }

        private static PointF Lerp(PointF p1, PointF p2, float t)
        {
            return new PointF(
                p1.X + (p2.X - p1.X) * t,
                p1.Y + (p2.Y - p1.Y) * t
            );
        }
    }
}