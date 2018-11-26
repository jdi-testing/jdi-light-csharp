using System;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Common;

namespace JDI.Light.Interfaces.Complex
{
    public interface IDropList : IDropList<IConvertible>
    {
    }

    public interface IDropList<in TEnum> : IMultiSelector<TEnum>, IText
        where TEnum : IConvertible
    {
    }
}