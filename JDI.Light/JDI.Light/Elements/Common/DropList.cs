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

        public void Select(string value, bool checkEnables = true)
        {
            CheckEnabled(checkEnables);
            if (ListLocator != null)
            {
                ItemLocator = ListLocator;
                Select(value, this);
                if (GetSelected() != value)
                {
                    throw new Exception($"{value} element not selected.");
                }
            }
        }

        public void Select(Enum value, bool checkEnables = true)
        {
            CheckEnabled(checkEnables);
            if (ListLocator != null)
            {
                ItemLocator = ListLocator;
                Select(value.ToString(), this);
                if (GetSelected() != value.ToString())
                {
                    throw new Exception($"{value} element not selected.");
                }
            }
        }

        public void Select(int index, bool checkEnables = true)
        {
            CheckEnabled(checkEnables);
            index--;
            if (ListLocator != null)
            {
                ItemLocator = ListLocator;
                Select(index, this);
                if (GetSelectedIndex(this) != index)
                {
                    throw new Exception($"Element with {index} index not selected.");
                }
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