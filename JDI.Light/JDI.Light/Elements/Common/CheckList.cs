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

        private readonly Action<CheckList, string> _unCheckAll = (checkList, item) =>
        {
            var els = checkList.WebElement.FindElements(checkList.CheckListLocator);
            var itemsList = els.FirstOrDefault(e => e.GetAttribute("id").Equals(item));
            if (itemsList?.GetAttribute("checked") != null)
            {
                itemsList.Click();
            }
        };

        private string[] _getAllSelected(Array values)
        {
            var selectedItems = new List<string>();
            foreach (var value in values.ToStringArray())
            {
                var els = WebElement.FindElements(CheckListLocator);
                var itemsList = els.FirstOrDefault(e => e.GetAttribute("id").Equals(value));
                if (itemsList?.GetAttribute("checked") != null)
                {
                    selectedItems.Add(value);
                }
            }

            selectedItems.Reverse();
            return selectedItems.ToArray();
        }

        public CheckList(By byLocator) : base(byLocator)
        {
        }

        public void Check(string[] values)
        {
            ItemLocator = CheckListLocator;
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

        public void UncheckAll(Array allValues)
        {
            foreach (var value in allValues.ToStringArray())
            {
                Invoker.DoAction($"Unselect item '{string.Join(" -> ", value)}'",
                    () => _unCheckAll.Invoke(this, value));
            }
        }

        public string[] GetChecked(Array values)
        {       
            return _getAllSelected(values);
        }
        
        public bool Value { get; set; }
    }
}