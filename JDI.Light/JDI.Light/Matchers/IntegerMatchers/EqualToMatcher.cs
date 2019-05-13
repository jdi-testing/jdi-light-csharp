using System;

namespace JDI.Light.Matchers.IntegerMatchers
{
    public class EqualToMatcher: Matcher<int>
    {
        private EqualToMatcher(int rightNumber) : base(rightNumber)
        {
        }

        public static EqualToMatcher EqualTo(int rightNumber) => new EqualToMatcher(rightNumber);

        public override string ActionName => "equal to";

        protected override Func<int, int, bool> Condition => (left, right) => left == right;
    }
}