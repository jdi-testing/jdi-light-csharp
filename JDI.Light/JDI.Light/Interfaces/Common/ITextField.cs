using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface ITextField : ISetValue<string>, ITextElement
    {
        void Input(string text);
        void SetText(string text);
        void Focus();
        string Placeholder { get; }
    }
}