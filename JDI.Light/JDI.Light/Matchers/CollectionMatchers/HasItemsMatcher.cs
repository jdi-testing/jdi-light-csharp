using System;
using System.Collections.Generic;
using System.Linq;

namespace JDI.Light.Matchers.CollectionMatchers
{
    public class HasItemsMatcher<T> : Matcher<IEnumerable<T>>
    {
        private HasItemsMatcher(IEnumerable<T> rightValue) : base(rightValue)
        {
        }

        public static HasItemsMatcher<T> HasItems(IEnumerable<T> rightValue) => new HasItemsMatcher<T>(rightValue);

        public override string ActionName => "has item";

        protected override Func<IEnumerable<T>, IEnumerable<T>, bool> Condition =>
            (leftSequence, rightSequence) => rightSequence.All(leftSequence.Contains);
    }
}
