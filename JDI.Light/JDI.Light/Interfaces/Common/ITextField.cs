using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface ITextField : ISetValue<string>, IText
    {
        void Input(string text);
        void SendKeys(string text);
        void NewInput(string text);
        void Clear();
        void Focus();
    }
}