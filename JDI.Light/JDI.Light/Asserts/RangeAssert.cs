using System;
using JDI.Light.Asserts.Generic;
using JDI.Light.Elements.Common;
using JDI.Light.Matchers;
using static JDI.Light.Jdi;

namespace JDI.Light.Asserts
{
    public class RangeAssert : IsAssert<RangeAssert>
    {
        protected Range Range { get; }

        public RangeAssert(Range range) : base(range)
        {
            Range = range;
        }

        public RangeAssert MinValue(Matcher<int> condition)
        {
            Assert.IsTrue(condition.IsMatch(Convert.ToInt32(Range.Min())), $"The min value {Range.Min()} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }

        public RangeAssert MaxValue(Matcher<int> condition)
        {
            Assert.IsTrue(condition.IsMatch(Convert.ToInt32(Range.Max())), $"The max value {Range.Max()} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }

        public RangeAssert Value(Matcher<int> condition)
        {
            Assert.IsTrue(condition.IsMatch(Convert.ToInt32(Range.Value())), $"The value {Range.Value()} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }

        public RangeAssert Step(Matcher<int> condition)
        {
            Assert.IsTrue(condition.IsMatch(Convert.ToInt32(Range.Step())), $"The step {Range.Step()} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }
    }
}