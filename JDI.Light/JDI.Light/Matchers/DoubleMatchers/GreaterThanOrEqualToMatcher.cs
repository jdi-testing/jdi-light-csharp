using System;

namespace JDI.Light.Matchers.DoubleMatchers
{
    public class GreaterThanOrEqualToMatcher : Matcher<double>
    {
        private GreaterThanOrEqualToMatcher(double rightNumber) : base(rightNumber)
        {
        }

        public static GreaterThanOrEqualToMatcher GreaterThanOrEqualTo(double rightNumber) => new GreaterThanOrEqualToMatcher(rightNumber);

        public override string ActionName => "greater than or equal to";

        protected override Func<double, double, bool> Condition => (left, right) => left >= right;
    }
}