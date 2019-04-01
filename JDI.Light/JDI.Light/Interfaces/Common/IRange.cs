using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface IRange : IBaseUIElement
    {
        void SetRange(string value);
        void SetRange(int value);
        string GetValue();
        int GetRange();
        int Min();
        int Max();
    }
}