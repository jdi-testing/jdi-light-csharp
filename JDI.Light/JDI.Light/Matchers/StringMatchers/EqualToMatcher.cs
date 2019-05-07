using System;

namespace JDI.Light.Matchers.StringMatchers
{
    public class EqualToMatcher: Matcher<string>
    {
        private EqualToMatcher(string rightNumber) : base(rightNumber)
        {
        }
        public static EqualToMatcher EqualTo(string rightNumber) => new EqualToMatcher(rightNumber);

        public override string ActionName => "equal to";

        protected override Func<string, string, bool> Condition => (left, right) => left == right;
    }
}