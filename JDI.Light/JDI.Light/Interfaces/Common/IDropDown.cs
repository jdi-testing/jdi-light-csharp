using JDI.Light.Interfaces.Base;
using OpenQA.Selenium;

namespace JDI.Light.Interfaces.Common
{
    public interface IDropDown : IBaseUIElement
    {
        By ItemLocator { get; set; }
        void Select(string value);
        void Select(int index);
        string GetSelected();
    }
}