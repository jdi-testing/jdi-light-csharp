using System;
using JDI.Light.Interfaces.Base;
using OpenQA.Selenium.Support.UI;

namespace JDI.Light.Interfaces.Common
{
    public interface IDropDown : IBaseUIElement
    {
        SelectElement SelectElement { get; }
        void Select(string value, bool checkEnables = true);
        void Select(Enum value, bool checkEnables = true);
        void Select(int index, bool checkEnables = true);
        string GetSelected();
    }
}