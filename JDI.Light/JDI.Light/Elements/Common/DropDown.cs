using System;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JDI.Light.Elements.Common
{
    public class DropDown : Selector, IDropDown
    {
        public DropDown(By byLocator) : base(byLocator)
        {
        }

        public void Select(string value)
        {
            new SelectElement(this).SelectByText(value);
        }

        public void Select(Enum value)
        {
            new SelectElement(this).SelectByText(value.ToString());
        }

        public void Select(int index)
        {
            new SelectElement(this).SelectByIndex(index);
        }

        public string GetSelected()
        {
            return new SelectElement(this).SelectedOption.Text;
        }
    }
}