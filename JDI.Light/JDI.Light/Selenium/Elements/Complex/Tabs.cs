using System;
using JDI.Light.Interfaces.Complex;

namespace JDI.Light.Selenium.Elements.Complex
{
    public class Tabs : Tabs<IConvertible>, ITabs
    {
    }

    public class Tabs<TEnum> : Selector<TEnum>, ITabs<TEnum>
        where TEnum : IConvertible
    {
    }
}