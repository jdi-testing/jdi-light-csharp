using JDI.Light.Asserts;
using JDI.Light.Interfaces.Common;

namespace JDI.Light.Elements.Common
{
    public class Title : TextElement, ITitle
    {
        public new TitleAssert Is => new TitleAssert(this);

        public new TitleAssert AssertThat => Is;
    }
}