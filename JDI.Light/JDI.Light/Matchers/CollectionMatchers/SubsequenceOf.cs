using System;
using System.Collections.Generic;
using System.Linq;

namespace JDI.Light.Matchers.CollectionMatchers
{
    public class SubsequenceOf<T> : Matcher<IEnumerable<T>>
    {
        public SubsequenceOf(IEnumerable<T> rightValue) : base(rightValue)
        {
        }

        public override string ActionName => "subsequence of";

        protected override Func<IEnumerable<T>, IEnumerable<T>, bool> Condition =>
            (leftSequence, rightSequence) => leftSequence.All(rightSequence.Contains);
    }
}
