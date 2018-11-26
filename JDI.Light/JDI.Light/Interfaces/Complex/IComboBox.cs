using System;
using JDI.Light.Interfaces.Common;

namespace JDI.Light.Interfaces.Complex
{
    public interface IComboBox : ISelector<IConvertible>
    {
    }

    public interface IComboBox<in TEnum> : IDropDown<TEnum>, ITextField
        where TEnum : IConvertible
    {
    }
}