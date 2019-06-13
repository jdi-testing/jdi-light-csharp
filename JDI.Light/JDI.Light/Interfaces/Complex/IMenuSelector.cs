using JDI.Light.Interfaces.Asserts;
using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Complex
{
    public interface IMenuSelector : IBaseUIElement, IHasSelectMenuAssert
    {
        bool Selected(string option);
        int Size();
    }
}
