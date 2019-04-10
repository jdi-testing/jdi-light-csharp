using System;
using JDI.Light.Interfaces.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JDI.Light.Interfaces.Common
{
    public interface IDropDown : IBaseUIElement
    {
        SelectElement SelectElement { get; }
        void Select(string value);
        void Select(Enum value);
        void Select(int index);
        string GetSelected();
    }
}