using System.Linq;
using JDI.Light.Interfaces.Asserts;
using JDI.Light.Interfaces.Base;
using JDI.Light.Matchers;

namespace JDI.Light.Asserts.Generic
{
    public class IsAssert<T> : ICommonAssert<T> where T : IsAssert<T>
    {
        public IBaseUIElement Element { get; }

        public IsAssert(IBaseUIElement element)
        {
            Element = element;
        }
        
        public T Text(Matcher<string> condition)
        {
            Jdi.Assert.IsTrue(condition.IsMatch(Element.Text), $"text {condition.FailedMessage()}");
            return (T)this;
        }

        public T Attr(string attrName, Matcher<string> condition)
        {
            Jdi.Assert.IsTrue(condition.IsMatch(Element.GetAttribute(attrName)), $"attribute {condition.FailedMessage()}");
            return (T)this;
        }

        public T Css(string propertyName, Matcher<string> condition)
        {
            Jdi.Assert.IsTrue(condition.IsMatch(Element.GetCssValue(propertyName)), $"css {condition.FailedMessage()}");
            return (T)this;
        }

        public T Tag(Matcher<string> condition)
        {
            Jdi.Assert.IsTrue(condition.IsMatch(Element.TagName), $"tag {condition.FailedMessage()}");
            return (T)this;
        }

        public T HasClass(string className)
        {
            var classes = Element.GetAttribute("class").Split(' ');
            Jdi.Assert.IsTrue(classes.Contains(className), $"{Element.Name} doesn't contain class {className}");
            return (T)this;
        }

        public T CssClass(Matcher<string> condition)
        {
            Jdi.Assert.IsTrue(condition.IsMatch(Element.GetAttribute("class")), $"class {condition.FailedMessage()}");
            return (T)this;
        }

        public T Displayed()
        {
            Jdi.Assert.IsTrue(Element.Displayed, "element not displayed");
            return (T)this;
        }

        public T Disappear()
        {
            Jdi.Assert.IsFalse(Element.Displayed, "element not disappeared");
            return (T)this;
        }

        public T Selected()
        {
            Jdi.Assert.IsTrue(Element.Selected, "element not selected");
            return (T)this;
        }

        public T Deselected()
        {
            Jdi.Assert.IsFalse(Element.Selected, "element selected");
            return (T)this;
        }

        public T Enabled()
        {
            Jdi.Assert.IsTrue(Element.Enabled, "element disabled");
            return (T)this;
        }

        public T Disabled()
        {
            Jdi.Assert.IsFalse(Element.Enabled, "element enabled");
            return (T)this;
        }
    }
}