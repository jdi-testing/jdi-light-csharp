using System;

namespace JDI.Light.Matchers.IntegerMatchers
{
    public class HasSizeMatcher : Matcher<int>
    {
        private HasSizeMatcher(int rightNumber) : base(rightNumber)
        {
        }

        public static HasSizeMatcher HasSize(int rightNumber) => new HasSizeMatcher(rightNumber);

        public override string ActionName => "Has size";

        protected override Func<int, int, bool> Condition => (left, right) => left == right;
    }
}
