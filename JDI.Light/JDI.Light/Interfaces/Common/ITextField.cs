using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface ITextField : ISetValue<string>, ITextElement
    {
        void Input(string text);
        new void SendKeys(string text);
        void SetText(string text);
        new void Clear();
        void Focus();
        string Placeholder { get; }
    }
}