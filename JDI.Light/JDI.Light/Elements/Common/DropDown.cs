using System;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JDI.Light.Elements.Common
{
    public class DropDown : Selector, IDropDown
    {
        public SelectElement SelectElement { get; }

        public DropDown(By byLocator) : base(byLocator)
        {
            SelectElement = new SelectElement(this);
        }

        public void Select(string value)
        {
            SelectElement.SelectByText(value);
        }

        public void Select(Enum value)
        {
            SelectElement.SelectByText(value.ToString());
        }

        public void Select(int index)
        {
            SelectElement.SelectByIndex(index);
        }

        public string GetSelected()
        {
            return SelectElement.SelectedOption.Text;
        }
    }
}