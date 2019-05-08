using System.Collections.Generic;
using JDI.Light.Interfaces.Base;
using OpenQA.Selenium;

namespace JDI.Light.Interfaces.Complex
{
    public interface IRadioButtons : IBaseUIElement, IGetValue<string>
    {
        By RadioButtonLocator { get; set; }
        By LabelLocator { get; set; }
        void Select(string value, bool checkEnabled = true);
        void Select(int index, bool checkEnabled = true);
        new string Selected();
        List<string> Values();
    }
}