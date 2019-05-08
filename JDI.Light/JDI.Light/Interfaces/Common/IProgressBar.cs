using JDI.Light.Asserts;
using JDI.Light.Interfaces.Asserts;
using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface IProgressBar : IBaseUIElement, IHasIsAssert
    {
        string Value();
        string Max();
        ProgressAssert Is();
        ProgressAssert AssertThat();
    }
}