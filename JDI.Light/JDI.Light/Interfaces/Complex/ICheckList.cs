using System;
using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Complex
{
    public interface ICheckList : ICheckList<IConvertible>
    {
    }

    public interface ICheckList<in TEnum> : IMultiSelector<TEnum>
        where TEnum : IConvertible
    {
    }
}