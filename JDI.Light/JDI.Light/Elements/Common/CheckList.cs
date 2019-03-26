using System;
using System.Collections.Generic;
using System.Linq;
using JDI.Light.Extensions;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class CheckList : Selector, ICheckList
    {
        public By CheckListLocator { get; set; }

        public List<By> CheckListLocators { get; set; } = new List<By>();

        public CheckList(By byLocator) : base(byLocator)
        {
        }

        public void Check(string[] values)
        {
            Select(values, this);
        }

        public void Check(int[] indexes)
        {
            for (var i = 0; i < indexes.Length; i++)
            {
                ItemLocator = CheckListLocators[i];
                Select(indexes[i], this);
            }
        }

        public void Uncheck(string[] values)
        {
            Select(values, this);
        }

        public string[] GetChecked(Array values)
        {       
            return _getSelected(values);
        }
        
        public bool Value { get; set; }

        private string[] _getSelected(Array values)
        {
            var selectedItems = new List<string>();
            foreach (var value in values.ToStringArray())
            {
                var els = WebElement.FindElements(CheckListLocator);
                var itemsList = els.FirstOrDefault(e => e.Text.Equals(value));
                if (itemsList?.GetAttribute("::after") != null)
                {
                    selectedItems.Add(value);
                }
            }

            selectedItems.Reverse();
            return selectedItems.ToArray();
        }
    }
}