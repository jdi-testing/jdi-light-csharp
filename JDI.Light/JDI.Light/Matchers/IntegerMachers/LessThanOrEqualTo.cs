using System;

namespace JDI.Light.Matchers.IntegerMachers
{
    public class LessThanOrEqualTo : Matcher<int>
    {
        public LessThanOrEqualTo(int rightNumber) : base(rightNumber)
        {
        }

        public override string ActionName => "less than or equal to";

        protected override Func<int, int, bool> Condition => (left, right) => left <= right;
    }
}
