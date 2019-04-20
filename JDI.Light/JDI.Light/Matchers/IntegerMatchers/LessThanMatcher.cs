using System;

namespace JDI.Light.Matchers.IntegerMatchers
{
    public class LessThanMatcher : Matcher<int>
    {
        private LessThanMatcher(int rightNumber) : base(rightNumber)
        {
        }

        public static LessThanMatcher LessThan(int rightNumber) => new LessThanMatcher(rightNumber);

        public override string ActionName => "less than";

        protected override Func<int, int, bool> Condition => (left, right) => left < right;
    }
}