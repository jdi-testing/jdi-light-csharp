using System;
using JDI.Core.Interfaces.Common;

namespace JDI.Core.Interfaces.Complex
{
    public interface IComboBox : ISelector<IConvertible>
    {
    }

    public interface IComboBox<in TEnum> : IDropDown<TEnum>, ITextField
        where TEnum : IConvertible
    {
    }
}