using System;
using System.Collections.Generic;
using System.Linq;
using JDI.Light.Exceptions;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Complex;
using JDI.Light.Matchers;
using static JDI.Light.Jdi;

namespace JDI.Light.Asserts
{
    public class SelectAssert
    {
        private readonly ISelector _selector;

        public SelectAssert(ISelector selector)
        {
            _selector = selector;
        }

        public SelectAssert Selected(string option)
        {
            Assert.IsTrue(_selector.Selected(option), $"no {option} in {string.Join(",", _selector.Checked())}");
            return this;
        }

        public SelectAssert Selected(Matcher<IEnumerable<string>> condition)
        {
            Assert.IsTrue(condition.IsMatch(_selector.Checked()),
                $"checked values {condition.FailedMessage()}");
            return this;
        }

        public SelectAssert Values(Matcher<IEnumerable<string>> condition)
        {
            Assert.IsTrue(condition.IsMatch(_selector.Values()),
                $"available values {condition.FailedMessage()}");
            return this;
        }

        public SelectAssert Enabled(Matcher<IEnumerable<string>> condition)
        {
            Assert.IsTrue(condition.IsMatch(_selector.ListEnabled()),
                $"enabled values {condition.FailedMessage()}");
            return this;
        }

        public SelectAssert Disabled(Matcher<IEnumerable<string>> condition)
        {
            Assert.IsTrue(condition.IsMatch(_selector.ListDisabled()),
                $"disabled values {condition.FailedMessage()}");
            return this;
        }

        public SelectAssert Texts(Matcher<IEnumerable<string>> condition)
        {
            return Values(condition);
        }

        public SelectAssert Attrs(string attrName, Matcher<IEnumerable<string>> condition)
        {
           throw new NotImplementedException("implement after JDIBase creation with getAllAttributes method");
        }

        public SelectAssert AllCss(string css, Matcher<IEnumerable<string>> condition)
        {
            var cssValues = GetWebList().Select(element => element.GetCssValue(css)).ToList();
            Assert.IsTrue(condition.IsMatch(cssValues),
                $"css values {condition.FailedMessage()}");
            return this;
        }

        public SelectAssert AllTags(Matcher<IEnumerable<string>> condition)
        {
            var tags = GetWebList().Select(element => element.TagName).ToList();
            Assert.IsTrue(condition.IsMatch(tags),
                $"tags {condition.FailedMessage()}");
            return this;
        }

        public SelectAssert HasCssClasses(params string[] classNames)
        {
            throw new NotImplementedException("implement after JDIBase creation with Classes method");
        }

        public SelectAssert CssClasses(Matcher<IEnumerable<string>> condition)
        {
            throw new NotImplementedException("implement after JDIBase creation with Classes method");
        }

        public SelectAssert AllDisplayed()
        {
            Assert.IsTrue(GetWebList().All(element => element.Displayed), "not all elements are displayed");
            return this;
        }

        public SelectAssert AllHidden()
        {
            Assert.IsFalse(GetWebList().All(element => element.Displayed), "not all elements are hidden");
            return this;
        }

        public SelectAssert AllSelected()
        {
            Assert.IsTrue(GetWebList().All(element => element.Selected), "not all elements are selected");
            return this;
        }

        public SelectAssert AllEnabled()
        {
            Assert.IsTrue(GetWebList().All(element => element.Enabled), "not all elements are enabled");
            return this;
        }

        public SelectAssert Empty()
        {
            Assert.IsTrue(_selector.IsEmpty(), "list of elements is not empty");
            return this;
        }

        public SelectAssert NotEmpty()
        {
            Assert.IsTrue(_selector.HasAny(), "list of elements is empty");
            return this;
        }

        public SelectAssert Size(Matcher<int> condition)
        {
            Assert.IsTrue(condition.IsMatch(_selector.GetSize()),
                $"elements count {condition.FailedMessage()}");
            return this;
        }

        public SelectAssert Size(int count)
        {
            Assert.AreEquals(count, _selector.GetSize());
            return this;
        }

        private List<IBaseUIElement> GetWebList()
        {
            var elements = _selector.AllUI();
            if (!elements.Any())
            {
                throw new ElementNotFoundException("No elements found");
            }
            return elements;
        }
    }
}
