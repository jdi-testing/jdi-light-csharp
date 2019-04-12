using System;

namespace JDI.Light.Matchers.IntegerMachers
{
    public class LessThanOrEqualToMatcher : Matcher<int>
    {
        private LessThanOrEqualToMatcher(int rightNumber) : base(rightNumber)
        {
        }

        public static LessThanOrEqualToMatcher LessThanOrEqualTo(int rightNumber) => new LessThanOrEqualToMatcher(rightNumber);

        public override string ActionName => "less than or equal to";

        protected override Func<int, int, bool> Condition => (left, right) => left <= right;
    }
}