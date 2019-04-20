using System;

namespace JDI.Light.Matchers.LongMatchers
{
    public class IsMatcher : Matcher<long>
    {
        private IsMatcher(long size) : base(size)
        {
        }

        public static IsMatcher Is(long size) => new IsMatcher(size);

        public override string ActionName => "equals to";

        protected override Func<long, long, bool> Condition => (left, right) => left.Equals(right);
    }
}
