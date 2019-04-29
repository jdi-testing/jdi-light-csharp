using System.Collections.Generic;
using System.Linq;
using JDI.Light.Asserts.Generic;
using JDI.Light.Elements.Base;
using JDI.Light.Exceptions;
using JDI.Light.Extensions;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Complex;
using JDI.Light.Matchers;
using static JDI.Light.Jdi;
using static JDI.Light.Matchers.CollectionMatchers.HasItemsMatcher<string>;

namespace JDI.Light.Asserts
{
    public class SelectAssert : IsAssert<SelectAssert>
    {
        private readonly ISelector _selector;

        public SelectAssert(ISelector selector) : base(selector)
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

        public SelectAssert Attrs( Matcher<IEnumerable<string>> condition)
        {
            var attributes = (_selector as UIElement).GetAllAttributes().Keys;
            Assert.IsTrue(condition.IsMatch(attributes),
                $"attributes {condition.FailedMessage()}");
            return this;
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
            return CssClasses(HasItems(classNames));
        }

        public SelectAssert CssClasses(Matcher<IEnumerable<string>> condition)
        {
            Assert.IsTrue(condition.IsMatch(_selector.GetClasses()),
                $"css classes {condition.FailedMessage()}");
            return this;
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
