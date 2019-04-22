using System;

namespace JDI.Light.Matchers.LongMatchers
{
    public class GreaterThanMatcher : Matcher<long>
    {
        private GreaterThanMatcher(long rightNumber) : base(rightNumber)
        {
        }

        public static GreaterThanMatcher GreaterThan(long rightNumber) => new GreaterThanMatcher(rightNumber);

        public override string ActionName => "greater than";

        protected override Func<long, long, bool> Condition => (left, right) => left > right;
    }
}
