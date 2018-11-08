using System;
using JDI.Core.Interfaces.Base;

namespace JDI.Core.Interfaces.Complex
{
    public interface IGroup<out TType> : IGroup<IConvertible, TType>
        where TType : IElement
    { }
    public interface IGroup<in TEnum, out TType> : IBaseElement 
        where TEnum : IConvertible
        where TType : IElement
    {
        TType Get(TEnum name);

        TType Get(string name);
    }
}
