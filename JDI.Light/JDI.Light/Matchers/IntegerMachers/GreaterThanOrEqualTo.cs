using System;

namespace JDI.Light.Matchers.IntegerMachers
{
    public class GreaterThanOrEqualTo : Matcher<int>
    {
        public GreaterThanOrEqualTo(int rightNumber) : base(rightNumber)
        {
        }

        public override string ActionName => "greater than or equal to";

        protected override Func<int, int, bool> Condition => (left, right) => left >= right;
    }
}
