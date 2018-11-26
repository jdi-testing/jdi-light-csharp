using System;
using JDI.Light.Interfaces.Complex;

namespace JDI.Light.Selenium.Elements.Complex
{
    public class RadioButtons : RadioButtons<IConvertible>, IRadioButtons
    {
    }

    public class RadioButtons<TEnum> : Selector<TEnum>, IRadioButtons<TEnum>
        where TEnum : IConvertible
    {
    }
}