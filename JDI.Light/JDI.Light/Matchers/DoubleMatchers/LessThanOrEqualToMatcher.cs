using System;

namespace JDI.Light.Matchers.DoubleMatchers
{
    public class LessThanOrEqualToMatcher : Matcher<double>
    {
        private LessThanOrEqualToMatcher(double rightNumber) : base(rightNumber)
        {
        }

        public static LessThanOrEqualToMatcher LessThanOrEqualTo(double rightNumber) => new LessThanOrEqualToMatcher(rightNumber);

        public override string ActionName => "less than or equal to";

        protected override Func<double, double, bool> Condition => (left, right) => left <= right;
    }
}