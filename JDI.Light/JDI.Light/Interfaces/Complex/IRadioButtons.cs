using System.Collections.Generic;
using JDI.Light.Interfaces.Base;
using OpenQA.Selenium;

namespace JDI.Light.Interfaces.Complex
{
    public interface IRadioButtons : IBaseUIElement, IGetValue<string>
    {
        By RadioButtonLocator { get; set; }
        By LabelLocator { get; set; }
        void Select(string value);
        void Select(int index);
        new string Selected();
        List<string> Values();
    }
}