using JDI.Light.Asserts;
using JDI.Light.Interfaces.Asserts;
using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface ITextElement : IGetValue<string>, IBaseUIElement, IHasIsAssert
    {
        string GetText();
        string GetValue();

        new TextAssert Is { get; }
        new TextAssert AssertThat { get; }
    }
}