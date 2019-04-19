using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface IRange : IBaseUIElement
    {
        double Volume();
        string GetValue();
        double Min();
        double Max();
        double Step();
        void SetValue(string value);
        void SetValue(double value);
    }
}