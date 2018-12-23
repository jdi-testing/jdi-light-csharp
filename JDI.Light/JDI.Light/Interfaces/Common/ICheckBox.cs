using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface ICheckBox : IBaseUIElement, ISetValue<bool>
    {
        void Check();
        void Uncheck();
        bool IsChecked { get; }
    }
}