using System;
using JDI.Light.Interfaces.Base;
using OpenQA.Selenium;

namespace JDI.Light.Interfaces.Common
{
    public interface IDropList : IBaseUIElement
    {
        By Expander { get; set; }
        By Value { get; set; }
        By List { get; set; }

        void Expand();
        void Select(string value);
        void Select(Enum value);
        void Select(int index);
        string GetSelected();
    }
}