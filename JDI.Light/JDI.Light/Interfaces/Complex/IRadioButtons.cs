using JDI.Light.Interfaces.Base;
using OpenQA.Selenium;

namespace JDI.Light.Interfaces.Complex
{
    public interface IRadioButtons : IBaseUIElement
    {
        By RadioLocator { get; set; }
        void Select(string value);
        void Select(int index);
        new string Selected();
    }
}