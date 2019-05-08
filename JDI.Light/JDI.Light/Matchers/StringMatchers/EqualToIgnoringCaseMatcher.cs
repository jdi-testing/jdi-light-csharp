using System;

namespace JDI.Light.Matchers.StringMatchers
{
    public class EqualToIgnoringCaseMatcher : Matcher<string>
    {
        private EqualToIgnoringCaseMatcher(string rightNumber) : base(rightNumber)
        {
        }
        public static EqualToIgnoringCaseMatcher EqualTo(string rightNumber) => new EqualToIgnoringCaseMatcher(rightNumber);

        public override string ActionName => "equal to";

        protected override Func<string, string, bool> Condition => (left, right) => left.ToLower() == right.ToLower();
    }
}