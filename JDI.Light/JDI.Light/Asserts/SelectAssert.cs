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
        private ISelector _selector;

        public SelectAssert(ISelector selector)
        {

        }

        public SelectAssert Selected(string option)
        {
            Assert.IsTrue(_selector.Selected(option), $"no {option} in {string.Join(",", _selector.Checked())}");
            return this;
        }

        public SelectAssert Selected(Matcher<IEnumerable<string>> condition)
        {
            Assert.IsTrue(condition.IsMatch(_selector.Checked()),
                $"checked values {string.Join(",", _selector.Checked())} are not {condition.ActionName} {string.Join(",", condition.RightValue)}");
            return this;
        }

        public SelectAssert Values(Matcher<IEnumerable<string>> condition)
        {
            Assert.IsTrue(condition.IsMatch(_selector.Values()),
                $"available values {string.Join(",", _selector.Values())} are not {condition.ActionName} {string.Join(",", condition.RightValue)}");
            return this;
        }

        public SelectAssert Enabled(Matcher<IEnumerable<string>> condition)
        {
            Assert.IsTrue(condition.IsMatch(_selector.ListEnabled()),
                $"enabled values {string.Join(",", _selector.ListEnabled())} are not {condition.ActionName} {string.Join(",", condition.RightValue)}");
            return this;
        }

        public SelectAssert Disabled(Matcher<IEnumerable<string>> condition)
        {
            Assert.IsTrue(condition.IsMatch(_selector.ListDisabled()),
                $"disabled values {string.Join(",", _selector.ListEnabled())} are not {condition.ActionName} {string.Join(",", condition.RightValue)}");
            return this;
        }

        public SelectAssert Texts(Matcher<IEnumerable<string>> condition)
        {
            return Values(condition);
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
            Assert.IsFalse(GetWebList().All(element => element.Selected), "not all elements are selected");
            return this;
        }

        public SelectAssert AllEnabled()
        {
            Assert.IsFalse(GetWebList().All(element => element.Selected), "not all elements are enabled");
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
            Assert.IsTrue(condition.IsMatch(_selector.Size),
                $"elements count {_selector.Size} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }

        public SelectAssert Size(int count)
        {
            Assert.AreEquals(count, _selector.Size);
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
