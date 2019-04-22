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

        public By ExpanderLocator { get; set; }
        public By ValueLocator { get; set; }
        public By ListLocator { get; set; }
      
        public void Setup(By value, By list, By expand)
        {
            if (value != null)
            {
                ValueLocator = value;
            }

            if (list != null)
            {
                ListLocator = list;
            }

            if (expand != null)
            {
                ExpanderLocator = expand;
            }
        }

        public void Expand()
        {
            if (ExpanderLocator != null)
            {
                FindElement(ExpanderLocator).Click();
            }
        }

        public void Select(string value)
        {
            if (ListLocator != null)
            {
                ItemLocator = ListLocator;
                Select(value, this);
            }
        }

        public void Select(Enum value)
        {
            if (ListLocator != null)
            {
                ItemLocator = ListLocator;
                Select(value.ToString(), this);
            }
        }

        public void Select(int index)
        {
            if (ListLocator != null)
            {
                ItemLocator = ListLocator;
                Select(index, this);
            }
        }

        public string GetSelected()
        {
            if (ValueLocator != null)
            {
                return GetSelected(FindElement(ValueLocator));
            }
            return null;
        }
    }
}