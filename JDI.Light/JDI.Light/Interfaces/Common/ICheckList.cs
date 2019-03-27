using System;
using JDI.Light.Interfaces.Base;
using OpenQA.Selenium;

namespace JDI.Light.Interfaces.Common
{
    public interface ICheckList : IBaseUIElement
    {
        void Check(string[] values);
        void Check(string value);
        void Check(int[] indexes);
        void Check(int index);
        void Uncheck(string[] values);
        void Uncheck(string value);
        void Uncheck(int[] indexes);
        void Uncheck(int index);
        string[] GetChecked(Array values);
    }
}