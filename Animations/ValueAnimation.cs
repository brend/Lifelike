using System.Linq.Expressions;
using System.Windows.Forms;

namespace Lifelike.Animations
{
    public class ValueAnimation : AnimationBase
    {
        private readonly double _startValue;
        private readonly double _endValue;
        private readonly double _delta;
        private readonly Expression<System.Func<Control, int>> _property;

        public ValueAnimation(
            Control control, 
            Expression<System.Func<Control, int>> property,
            double startValue, 
            double endValue, 
            Timing.Timing? timingFunction = null)
            : base(control, timingFunction)
        {
            _property = property;
            _startValue = startValue;
            _endValue = endValue;
            _delta = _endValue - _startValue;
        }

        protected override void Update()
        {
            var value = _startValue + _delta * Progress;
            _property.Compile().DynamicInvoke(Control, value);
        }
    }
}