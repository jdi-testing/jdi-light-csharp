using System;
using System.Collections.Generic;
using System.Linq;

namespace JDI.Light.Matchers.CollectionMatchers
{
    public class ContainsInAnyOrderMatcher<T> : Matcher<IEnumerable<T>>
    {
        private ContainsInAnyOrderMatcher(IEnumerable<T> rightValue) : base(rightValue)
        {
        }

        public static ContainsInAnyOrderMatcher<T> ContainsInAnyOrder(IEnumerable<T> rightValue) => new ContainsInAnyOrderMatcher<T>(rightValue);

        public override string ActionName => "has item";

        protected override Func<IEnumerable<T>, IEnumerable<T>, bool> Condition =>
            (leftSequence, rightSequence) => rightSequence.All(leftSequence.Contains);
    }
}