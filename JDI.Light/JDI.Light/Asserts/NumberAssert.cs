using System;
using JDI.Light.Asserts.Generic;
using JDI.Light.Elements.Common;
using JDI.Light.Matchers;

using static JDI.Light.Jdi;

namespace JDI.Light.Asserts
{
    public class NumberAssert : IsAssert<NumberAssert>
    {
        protected NumberSelector NumberSelector { get; }

        public NumberAssert(NumberSelector number) : base(number)
        {
            NumberSelector = number;
        }

        public NumberAssert MinValue(Matcher<double> condition)
        {
            Assert.IsTrue(condition.IsMatch(Convert.ToDouble(NumberSelector.Min)), $"The min value {NumberSelector.Min} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }

        public NumberAssert MaxValue(Matcher<double> condition)
        {
            Assert.IsTrue(condition.IsMatch(Convert.ToDouble(NumberSelector.Max)), $"The max value {NumberSelector.Max} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }

        public NumberAssert StepValue(Matcher<double> condition)
        {
            Assert.IsTrue(condition.IsMatch(Convert.ToDouble(NumberSelector.Step)), $"The step value {NumberSelector.Step} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }

        public NumberAssert Placeholder(Matcher<string> condition)
        {
            Assert.IsTrue(condition.IsMatch(NumberSelector.Placeholder), $"The placeholder {NumberSelector.Placeholder} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }

        public NumberAssert Number(Matcher<double> condition)
        {
            Assert.IsTrue(condition.IsMatch(Convert.ToDouble(NumberSelector.Value)), $"The value {NumberSelector.Value} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }
    }
}