using System;

namespace JDI.Light.Matchers.StringMatchers
{
    public class IsMatcher : Matcher<string>
    {
        public IsMatcher(string rightValue) : base(rightValue)
        {
        }

        public static IsMatcher Is(string rightNumber) => new IsMatcher(rightNumber);

        public override string ActionName => "is equal to";
        protected override Func<string, string, bool> Condition => (left, right) => left == right;
    }
}
