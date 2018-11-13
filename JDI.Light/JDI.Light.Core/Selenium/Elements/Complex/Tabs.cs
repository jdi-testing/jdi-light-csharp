using System;
using JDI.Core.Interfaces.Complex;

namespace JDI.Core.Selenium.Elements.Complex
{
    public class Tabs : Tabs<IConvertible>, ITabs
    {
    }

    public class Tabs<TEnum> : Selector<TEnum>, ITabs<TEnum>
        where TEnum : IConvertible
    {
    }
}