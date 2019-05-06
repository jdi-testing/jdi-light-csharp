using JDI.Light.Interfaces.Asserts;
using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface ICheckBox : IBaseUIElement, ISetValue<bool>, IHasIsAssert
    {
        void Check(bool checkEnabled = true);
        void Uncheck(bool checkEnabled = true);
        bool IsChecked { get; }
    }
}