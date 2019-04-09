using System;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class DropList : Selector, IDropList
    {
        public DropList(By byLocator) : base(byLocator)
        {
        }

        public string Expander { get; set; }
        public string Value { get; set; }
        public string List { get; set; }
      
        public void Setup(string value, string list, string expand)
        {
            if (value != null) Value = value;
            if (list != null) List = list;
            if (expand != null) Expander = expand;
        }

        public void Expand()
        {
            if (Expander != null)
            {
                FindElement(By.CssSelector(Expander)).Click();
            }
        }

        public void Select(string value)
        {
            if (List != null)
            {
                ItemLocator = By.CssSelector(List);
                Select(value, this);
            }
        }

        public void Select(Enum value)
        {
            if (List != null)
            {
                ItemLocator = By.CssSelector(List);
                Select(value.ToString(), this);
            }
        }

        public void Select(int index)
        {
            if (List != null)
            {
                ItemLocator = By.CssSelector(List);
                Select(index, this);
            }
        }

        public string GetSelected()
        {
            if (Value != null)
            {
                return GetSelected(FindElement(By.CssSelector(Value)));
            }
            return null;
        }
    }
}