using JDI.Light.Interfaces.Asserts;
using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface ITextElement : IGetValue<string>, IBaseUIElement, IHasIsAssert
    {
        string GetText();
        string WaitText(string text);
        string WaitMatchText(string regEx);
        string GetValue();
    }
}