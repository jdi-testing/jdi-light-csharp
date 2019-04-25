using System.Linq;
using JDI.Light.Interfaces.Asserts;
using JDI.Light.Interfaces.Base;
using JDI.Light.Matchers;
using static JDI.Light.Jdi;

namespace JDI.Light.Asserts
{
    public class IsAssert<T> : BaseAssert, ICommonAssert<T> where T : IsAssert<T>
    {
        public IsAssert(IBaseUIElement element) : base(element)
        {
        }
        
        public T Text(Matcher<string> condition)
        {
            Assert.IsTrue(condition.IsMatch(Element.Text), $"text {condition.FailedMessage()}");
            return (T)this;
        }

        public T Attr(string attrName, Matcher<string> condition)
        {
            Assert.IsTrue(condition.IsMatch(Element.GetAttribute(attrName)), $"attribute {condition.FailedMessage()}");
            return (T)this;
        }

        public T Css(string propertyName, Matcher<string> condition)
        {
            Assert.IsTrue(condition.IsMatch(Element.GetCssValue(propertyName)), $"css {condition.FailedMessage()}");
            return (T)this;
        }

        public T Tag(Matcher<string> condition)
        {
            Assert.IsTrue(condition.IsMatch(Element.TagName), $"tag {condition.FailedMessage()}");
            return (T)this;
        }

        public T HasClass(string className)
        {
            var classes = Element.GetAttribute("class").Split(' ');
            Assert.IsTrue(classes.Contains(className), $"{Name} doesn't contain class {className}");
            return (T)this;
        }

        public T CssClass(Matcher<string> condition)
        {
            Assert.IsTrue(condition.IsMatch(Element.GetAttribute("class")), $"class {condition.FailedMessage()}");
            return (T)this;
        }

        public T Displayed()
        {
            Assert.IsTrue(Element.Displayed, "element not displayed");
            return (T)this;
        }

        public T Disappear()
        {
            Assert.IsFalse(Element.Displayed, "element not disappeared");
            return (T)this;
        }

        public T Selected()
        {
            Assert.IsTrue(Element.Selected, "element not selected");
            return (T)this;
        }

        public T Deselected()
        {
            Assert.IsFalse(Element.Selected, "element selected");
            return (T)this;
        }

        public T Enabled()
        {
            Assert.IsTrue(Element.Enabled, "element disabled");
            return (T)this;
        }

        public T Disabled()
        {
            Assert.IsFalse(Element.Enabled, "element enabled");
            return (T)this;
        }
    }
}