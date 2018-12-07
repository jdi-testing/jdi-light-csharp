using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface IText : IGetValue<string>, IBaseUIElement
    {
        string GetText { get; }
        string WaitText(string text);
        string WaitMatchText(string regEx);
    }
}