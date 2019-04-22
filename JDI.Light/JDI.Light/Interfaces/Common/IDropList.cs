using System;
using JDI.Light.Interfaces.Base;
using OpenQA.Selenium;

namespace JDI.Light.Interfaces.Common
{
    public interface IDropList : IBaseUIElement
    {
        By ExpanderLocator { get; set; }
        By ValueLocator { get; set; }
        By ListLocator { get; set; }

        void Expand();
        void Select(string value);
        void Select(Enum value);
        void Select(int index);
        string GetSelected();
    }
}