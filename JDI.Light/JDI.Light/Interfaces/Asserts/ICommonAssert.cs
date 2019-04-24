using JDI.Light.Matchers;

namespace JDI.Light.Interfaces.Asserts
{
    public interface ICommonAssert<out T>
    {
        T Attr(string attrName, Matcher<string> condition);
        T Css(string propertyName, Matcher<string> condition);
        T Tag(Matcher<string> condition);
        T HasClass(string className);
        T CssClass(Matcher<string> condition);
        T Displayed();
        T Disappear();
        T Selected();
        T Deselected();
        T Enabled();
        T Disabled();
    }
}
