using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface IRange : IBaseUIElement
    {
        double GetRange();
        string GetValue();
        double Min();
        double Max();
        double Step();
        void SetRange(string value);
        void SetRange(double value);
    }
}