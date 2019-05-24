using JDI.Light.Asserts;
using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface IIcon : IBaseUIElement
    {
        string Src { get; }
        string Alt { get; }
        string Height { get; }
        string Width { get; }

        IconAssert Is();
        IconAssert AssertThat();
    }
}
