using System;
using JDI.Core.Interfaces.Base;
using JDI.Core.Interfaces.Common;

namespace JDI.Core.Interfaces.Complex
{
    public interface IDropList : IDropList<IConvertible> { }
    public interface IDropList<in TEnum> : IMultiSelector<TEnum>, IText
        where TEnum : IConvertible
    {
    }
}
