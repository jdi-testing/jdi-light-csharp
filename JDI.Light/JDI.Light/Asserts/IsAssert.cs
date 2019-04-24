using JDI.Light.Elements.Base;
using JDI.Light.Interfaces.Asserts;
using JDI.Light.Matchers;
using static JDI.Light.Jdi;

namespace JDI.Light.Asserts
{
    public class IsAssert<T> : BaseAssert, ICommonAssert<T> where T : IsAssert<T>
    {
        public IsAssert(UIElement element) : base(element)
        {
        }

        //todo: fix assert describe after merge selectAssert PR to master
        public T Text(Matcher<string> condition)
        {
            Assert.IsTrue(condition.IsMatch(Element.Text), $"text {condition}");
            return (T)this;
        }

        public T Attr(string attrName, Matcher<string> condition)
        {
            Assert.IsTrue(condition.IsMatch(Element.GetAttribute(attrName)), $"attribute {condition}");
            return (T)this;
        }

        public T Css(string css, Matcher<string> condition)
        {
            Assert.IsTrue(condition.IsMatch(Element.GetCssValue(css)), $"css {condition}");
            return (T)this;
        }

        public T Tag(Matcher<string> condition)
        {
            Assert.IsTrue(condition.IsMatch(Element.TagName), $"tag {condition}");
            return (T)this;
        }

        public T HasClass(string className)
        {
            throw new System.NotImplementedException("implement after selectorassert merge to master");
        }

        public T CssClass(Matcher<string> condition)
        {
            Assert.IsTrue(condition.IsMatch(Element.GetAttribute("class")), $"class {condition}");
            return (T)this;
        }

        public T Displayed()
        {
            Assert.IsTrue(Element.Displayed, "element not displayed");
            return (T)this;
        }

        public T Disappear()
        {
            Assert.IsTrue(Element.Displayed, "element not disappeared");
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