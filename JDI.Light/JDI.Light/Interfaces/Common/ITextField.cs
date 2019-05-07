using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface ITextField : ISetValue<string>, ITextElement
    {
        void Input(string text, bool checkEnabled = true);
        void SetText(string text, bool checkEnabled = true);
        void Focus();
        string Placeholder { get; }
    }
}