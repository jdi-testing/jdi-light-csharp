using System;
using System.Collections.Generic;
using System.Linq;
using JDI.Light.Elements.Common;
using JDI.Light.Elements.Composite;
using JDI.Light.Matchers;
using static JDI.Light.Jdi;

namespace JDI.Light.Asserts
{
    public class MultiDropdownAssert
    {
        protected MultiDropdown MultiDropdown { get; }

        public MultiDropdownAssert(MultiDropdown multiDropdown)
        {
            MultiDropdown = multiDropdown;
        }

        public MultiDropdownAssert Selected(string option)
        {
            Assert.IsTrue(MultiDropdown.OptionIsSelected(option));
            return this;
        }

        public MultiDropdownAssert Selected(Enum option)
        {
            Assert.IsTrue(MultiDropdown.OptionIsSelected(option.ToString()));
            return this;
        }

        public MultiDropdownAssert Values(Matcher<IEnumerable<string>> condition)
        {
            Assert.IsTrue(condition.IsMatch(MultiDropdown.Options.Select(o => o.Text)), 
                $"available values {condition.FailedMessage()}");
            return this;
        }

        public MultiDropdownAssert Disabled(Matcher<IEnumerable<string>> condition)
        {
            Assert.IsTrue(condition.IsMatch(MultiDropdown.Options.Where(o => !o.OptionIsEnabled).Select(o => o.Text)),
                $"disabled values {condition.FailedMessage()}");
            return this;
        }

        public MultiDropdownAssert Enabled(Matcher<IEnumerable<string>> condition)
        {
            Assert.IsTrue(condition.IsMatch(MultiDropdown.Options.Where(o => o.OptionIsEnabled).Select(o => o.Text)),
                $"disabled values {condition.FailedMessage()}");
            return this;
        }
    }
}
