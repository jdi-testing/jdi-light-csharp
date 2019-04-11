using System;
using System.Collections.Generic;
using System.Linq;

namespace JDI.Light.Matchers.CollectionMatchers
{
    public class SubsequenceOfMatcher<T> : Matcher<IEnumerable<T>>
    {
        private SubsequenceOfMatcher(IEnumerable<T> rightValue) : base(rightValue)
        {
        }

        public static SubsequenceOfMatcher<T> SubsequenceOf(IEnumerable<T> rightValue) => new SubsequenceOfMatcher<T>(rightValue);

        public override string ActionName => "subsequence of";

        protected override Func<IEnumerable<T>, IEnumerable<T>, bool> Condition =>
            (leftSequence, rightSequence) => leftSequence.All(rightSequence.Contains);
    }
}
