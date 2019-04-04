using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface IRange : IBaseUIElement
    {
        void SetRange(string value);
        void SetRange(double value);
        string GetValue();
        double GetRange();
        double Min();
        double Max();
    }
}