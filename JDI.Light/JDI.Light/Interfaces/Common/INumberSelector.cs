using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface INumberSelector:  IBaseUIElement
    {
        string Placeholder();
        string Min();
        string Max();
        string Value();
        string Step();
        void SetNumber(string number);
    }
}
