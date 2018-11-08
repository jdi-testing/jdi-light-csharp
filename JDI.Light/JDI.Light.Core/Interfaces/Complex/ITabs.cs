using System;

namespace JDI.Core.Interfaces.Complex
{
    public interface ITabs : ITabs<IConvertible> { }
    public interface ITabs<TEnum> : ISelector<TEnum>
        where TEnum : IConvertible
    {
    }
}
