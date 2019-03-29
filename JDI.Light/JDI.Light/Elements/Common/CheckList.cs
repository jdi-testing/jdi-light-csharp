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
        
        public CheckList(By byLocator) : base(byLocator)
        {
        }

        public void Check(string[] values)
        {
            ItemLocator = CheckListLocator;
            Select(values, this);
        }

        public void Check(string value)
        {
            ItemLocator = CheckListLocator;
            Select(new []{value}, this);
        }

        public void Check(int[] indexes)
        {
            ItemLocator = CheckListLocator;
            Select(indexes, this);
        }

        public void Check(int index)
        {
            ItemLocator = CheckListLocator;
            Select(new []{index}, this);
        }

        public void Uncheck(string[] values)
        {
            Select(values, this);
        }

        public void Uncheck(string value)
        {
            Select(new []{value}, this);
        }

        public void Uncheck(int[] indexes)
        {
            Select(indexes, this);
        }

        public void Uncheck(int index)
        {
            Select(new []{index}, this);
        }

        public void UncheckAll(Array allValues)
        {
            foreach (var value in allValues.ToStringArray())
            {
                Invoker.DoAction($"Unselect item '{string.Join(" -> ", value)}'",
                    () =>
                    {
                        var els = WebElement.FindElements(CheckListLocator);
                        var itemsList = els.FirstOrDefault(e => e.GetAttribute("id").Equals(value));
                        if (itemsList?.GetAttribute("checked") != null)
                        {
                            itemsList.Click();
                        }
                    });
            }
        }

        public string[] GetChecked(Array values)
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
    }
}