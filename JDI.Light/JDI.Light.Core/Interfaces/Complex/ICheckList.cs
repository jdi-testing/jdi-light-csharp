using System;
using JDI.Core.Interfaces.Base;

namespace JDI.Core.Interfaces.Complex
{
    public interface ICheckList : ICheckList<IConvertible> { }
    public interface ICheckList<in TEnum> : IMultiSelector<TEnum>
        where TEnum : IConvertible
    {
    }
}
