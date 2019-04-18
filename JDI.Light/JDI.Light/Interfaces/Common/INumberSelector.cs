using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface INumberSelector:  IBaseUIElement
    {
        string Placeholder();
        double Min();
        double Max();
        double Value();
        double Step();
        void SetNumber(double number);
    }
}