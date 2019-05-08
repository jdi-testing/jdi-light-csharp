using System;

namespace JDI.Light.Matchers.DoubleMatchers
{
    public class GreaterThanMatcher : Matcher<double>
    {
        private GreaterThanMatcher(double rightNumber) : base(rightNumber)
        {
        }

        public static GreaterThanMatcher GreaterThan(double rightNumber) => new GreaterThanMatcher(rightNumber);

        public override string ActionName => "greater than";

        protected override Func<double, double, bool> Condition => (left, right) => left > right;
    }
}