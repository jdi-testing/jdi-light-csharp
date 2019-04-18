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

        public By Expander { get; set; }
        public By Value { get; set; }
        public By List { get; set; }
      
        public void Setup(By value, By list, By expand)
        {
            if (value != null)
            {
                Value = value;
            }

            if (list != null)
            {
                List = list;
            }

            if (expand != null)
            {
                Expander = expand;
            }
        }

        public void Expand()
        {
            if (Expander != null)
            {
                FindElement(Expander).Click();
            }
        }

        public void Select(string value)
        {
            if (List != null)
            {
                ItemLocator = List;
                Select(value, this);
            }
        }

        public void Select(Enum value)
        {
            if (List != null)
            {
                ItemLocator = List;
                Select(value.ToString(), this);
            }
        }

        public void Select(int index)
        {
            if (List != null)
            {
                ItemLocator = List;
                Select(index, this);
            }
        }

        public string GetSelected()
        {
            if (Value != null)
            {
                return GetSelected(FindElement(Value));
            }
            return null;
        }
    }
}