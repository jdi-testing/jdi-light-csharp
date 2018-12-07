using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface ICheckBox : IClickable, ISetValue<bool>
    {
        void Check();
        void Uncheck();
        bool IsChecked { get; }
    }
}