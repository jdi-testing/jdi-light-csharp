using System;
using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Complex
{
    public interface IGroup<out TType> : IGroup<IConvertible, TType>
        where TType : IBaseElement
    {
    }

    public interface IGroup<in TEnum, out TType> : IBaseElement
        where TEnum : IConvertible
        where TType : IBaseElement
    {
        TType Get(TEnum name);

        TType Get(string name);
    }
}