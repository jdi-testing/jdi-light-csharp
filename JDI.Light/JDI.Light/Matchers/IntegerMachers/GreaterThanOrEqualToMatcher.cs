using System;

namespace JDI.Light.Matchers.IntegerMachers
{
    public class GreaterThanOrEqualToMatcher : Matcher<int>
    {
        private GreaterThanOrEqualToMatcher(int rightNumber) : base(rightNumber)
        {
        }

        public static GreaterThanOrEqualToMatcher GreaterThanOrEqualTo(int rightNumber) => new GreaterThanOrEqualToMatcher(rightNumber);

        public override string ActionName => "greater than or equal to";

        protected override Func<int, int, bool> Condition => (left, right) => left >= right;
    }
}
