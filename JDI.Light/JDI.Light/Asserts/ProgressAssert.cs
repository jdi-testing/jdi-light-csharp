using System;
using JDI.Light.Asserts.Generic;
using JDI.Light.Elements.Common;
using JDI.Light.Matchers;
using static JDI.Light.Jdi;

namespace JDI.Light.Asserts
{
    public class ProgressAssert : IsAssert<ProgressAssert>
    {
        protected ProgressBar Progress { get; }

        public ProgressAssert(ProgressBar progress) : base(progress)
        {
            Progress = progress;
        }

        public ProgressAssert MaxValue(Matcher<int> condition)
        {
            Assert.IsTrue(condition.IsMatch(Convert.ToInt32(Progress.Max())),
                $"The max value {Progress.Max()} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }

        public ProgressAssert Value(Matcher<int> condition)
        {
            Assert.IsTrue(condition.IsMatch(Convert.ToInt32(Progress.Value())),
                $"The value {Progress.Value()} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }
    }
}