using JDI.Light.Asserts.Generic;
using JDI.Light.Elements.Common;
using JDI.Light.Matchers;
using System;
using static JDI.Light.Jdi;

namespace JDI.Light.Asserts
{
    public class DateTimeSelectorAssert : IsAssert<DateTimeSelectorAssert>
    {
        protected DateTimeSelector DateTimeSelector { get; }

        public DateTimeSelectorAssert(DateTimeSelector dateTimeSelector) : base(dateTimeSelector)
        {
            DateTimeSelector = dateTimeSelector;
        }

        public DateTimeSelectorAssert SelectedTime(Matcher<long> condition)
        {
            Assert.IsTrue(condition.IsMatch(DateTimeSelector.GetDateTime().ToBinary()), $"The selected value {DateTimeSelector.GetDateTime()} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }

        public DateTimeSelectorAssert SelectedTime(Matcher<string> condition)
        {
            Assert.IsTrue(condition.IsMatch(DateTimeSelector.Value()), $"The selected value {DateTimeSelector.Value()} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }

        public DateTimeSelectorAssert HasMaxTime(Matcher<string> condition)
        {
            Assert.IsTrue(condition.IsMatch(DateTimeSelector.Max()), $"The selected value {DateTimeSelector.Max()} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }

        public DateTimeSelectorAssert HasMinTime(Matcher<string> condition)
        {
            Assert.IsTrue(condition.IsMatch(DateTimeSelector.Min()), $"The selected value {DateTimeSelector.Min()} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }
    }
}
