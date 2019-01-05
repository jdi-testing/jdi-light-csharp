using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface ITextField : ISetValue<string>, IText
    {
        void Input(string text);
        new void SendKeys(string text);
        void NewInput(string text);
        new void Clear();
        void Focus();
    }
}