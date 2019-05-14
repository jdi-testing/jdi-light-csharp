using System;

namespace JDI.Light.Matchers.DoubleMatchers
{
    public class LessThanMatcher : Matcher<double>
    {
        private LessThanMatcher(double rightNumber) : base(rightNumber)
        {
        }

        public static LessThanMatcher LessThan(double rightNumber) => new LessThanMatcher(rightNumber);

        public override string ActionName => "less than";

        protected override Func<double, double, bool> Condition => (left, right) => left < right;
    }
}