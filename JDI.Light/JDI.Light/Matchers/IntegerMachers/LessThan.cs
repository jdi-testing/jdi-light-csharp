using System;

namespace JDI.Light.Matchers.IntegerMachers
{
    public class LessThan : Matcher<int>
    {
        public LessThan(int rightNumber) : base(rightNumber)
        {
        }

        public override string ActionName => "less than";

        protected override Func<int, int, bool> Condition => (left, right) => left < right;
    }
}
