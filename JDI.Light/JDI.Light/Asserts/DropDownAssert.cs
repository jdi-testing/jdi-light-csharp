using System;
using System.Collections.Generic;
using System.Linq;
using JDI.Light.Elements.Common;
using JDI.Light.Matchers;
using OpenQA.Selenium.Support.UI;
using static JDI.Light.Jdi;

namespace JDI.Light.Asserts
{
    public class DropDownAssert
    {
        protected DropDown DropDown { get; }

        public DropDownAssert(DropDown dropDown)
        {
            DropDown = dropDown;
        }

        public DropDownAssert Selected(string option)
        {
            Assert.AreEquals(option, new SelectElement(DropDown).SelectedOption.Text);
            return this;
        }

        public DropDownAssert Selected(Enum option)
        {
            Assert.AreEquals(option.ToString(), new SelectElement(DropDown).SelectedOption.Text);
            return this;
        }

        public DropDownAssert Values(Matcher<IEnumerable<string>> condition)
        {
            Assert.IsTrue(condition.IsMatch(DropDown.SelectElement.Options.Select(o => o.Text)),
                $"available values {condition.FailedMessage()}");
            return this;
        }

        public DropDownAssert Disabled(Matcher<IEnumerable<string>> condition)
        {
            Assert.IsTrue(condition.IsMatch(DropDown.SelectElement.Options.Where(o => !o.Enabled).Select(o => o.Text)),
                $"disabled values {condition.FailedMessage()}");
            return this;
        }

        public DropDownAssert Enabled(Matcher<IEnumerable<string>> condition)
        {
            Assert.IsTrue(condition.IsMatch(DropDown.SelectElement.Options.Where(o => o.Enabled).Select(o => o.Text)),
                $"enabled values {condition.FailedMessage()}");
            return this;
        }
    }
}
