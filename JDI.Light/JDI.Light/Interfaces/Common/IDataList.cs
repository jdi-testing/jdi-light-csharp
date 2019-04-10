using System;
using JDI.Light.Interfaces.Base;
using OpenQA.Selenium;

namespace JDI.Light.Interfaces.Common
{
    public interface IDataList : IBaseUIElement
    {
        void Select(string value);
        void Select(Enum value);
        void Select(int index);
        string GetValue();
    }
}