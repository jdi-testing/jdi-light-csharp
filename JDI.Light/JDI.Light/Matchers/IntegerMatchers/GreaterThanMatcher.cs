using System;

namespace JDI.Light.Matchers.IntegerMatchers
{
    public class GreaterThanMatcher : Matcher<int>
    {
        private GreaterThanMatcher(int rightNumber) : base(rightNumber)
        {
        }

        public static GreaterThanMatcher GreaterThan(int rightNumber) => new GreaterThanMatcher(rightNumber);

        public override string ActionName => "greater than";

        protected override Func<int, int, bool> Condition => (left, right) => left > right;
    }
}