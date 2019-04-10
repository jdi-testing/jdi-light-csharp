using System;

namespace JDI.Light.Matchers.IntegerMachers
{
    public class GreaterThan : Matcher<int>
    {
        public GreaterThan(int rightNumber) : base(rightNumber)
        {
        }

        public override string ActionName => "greater than";

        protected override Func<int, int, bool> Condition => (left, right) => left > right;
    }
}
