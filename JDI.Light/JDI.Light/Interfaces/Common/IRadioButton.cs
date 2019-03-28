using JDI.Light.Interfaces.Base;
using OpenQA.Selenium;

namespace JDI.Light.Interfaces.Common
{
    public interface IRadioButton : IBaseUIElement
    {
        By RadioLocator { get; set; }
        void Select(string value);
        void Select(int index);
        string GetSelected();
    }
}