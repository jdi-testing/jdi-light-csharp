using System;

namespace JDI.Light.Matchers.DoubleMatchers
{
    public class EqualToMatcher : Matcher<double>
    {
        private EqualToMatcher(double rightNumber) : base(rightNumber)
        {
        }

        public static EqualToMatcher EqualTo(double rightNumber) => new EqualToMatcher(rightNumber);

        public override string ActionName => "equal to";

        protected override Func<double, double, bool> Condition => (left, right) => left == right;
    }
}