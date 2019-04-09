using System;
using JDI.Light.Interfaces.Base;
using OpenQA.Selenium;

namespace JDI.Light.Interfaces.Common
{
    public interface IDropList : IBaseUIElement
    {
        string Expander { get; set; }
        string Value { get; set; }
        string List { get; set; }

        void Expand();
        void Select(string value);
        void Select(Enum value);
        void Select(int index);
        string GetSelected();
    }
}