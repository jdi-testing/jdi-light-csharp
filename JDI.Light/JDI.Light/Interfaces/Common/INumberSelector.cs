using JDI.Light.Asserts;
using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface INumberSelector:  IBaseUIElement, IGetValue<double>
    {
        string Placeholder { get; }
        double Min { get; }
        double Max { get; }
        double Step { get; }
        void SetNumber(double number, bool checkEnabled = true);
        NumberAssert Is();
        NumberAssert AssertThat();
    }
}