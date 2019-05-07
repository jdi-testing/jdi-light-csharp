using JDI.Light.Asserts;
using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface IRange : IBaseUIElement
    {
        double Value();
        string GetValue();
        double Min();
        double Max();
        double Step();
        void SetValue(string value, bool checkEnabled = true);
        void SetValue(double value, bool checkEnabled = true);
        RangeAssert Is();
        RangeAssert AssertThat();
    }
}