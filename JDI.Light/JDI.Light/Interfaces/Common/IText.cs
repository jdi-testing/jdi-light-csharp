using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface IText : IGetValue<string>, IBaseUIElement
    {
        string GetText();
        string WaitText(string text);
        string WaitMatchText(string regEx);
    }
}