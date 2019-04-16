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
            foreach (var value in values)
            {
                if (!IsChecked(value))
                {
                    GetCheckBox(value).Click();
                }
            }
        }

        public void Check(string value)
        {
            ItemLocator = CheckListLocator;
            if (!IsChecked(value))
            {
                GetCheckBox(value).Click();
            }
        }

        public void Check(int[] indexes)
        {
            ItemLocator = CheckListLocator;
            foreach (var index in indexes)
            {
                if (!IsChecked(index))
                {
                    GetCheckBox(index).Click();
                }
            }
        }

        public void Check(int index)
        {
            ItemLocator = CheckListLocator;
            if (!IsChecked(index))
            {
                GetCheckBox(index).Click();
            }
        }

        public void Uncheck(string[] values)
        {
            ItemLocator = CheckListLocator;
            foreach (var value in values)
            {
                if (IsChecked(value))
                {
                    GetCheckBox(value).Click();
                }
            }
        }

        public void Uncheck(string value)
        {
            ItemLocator = CheckListLocator;
            if (IsChecked(value))
            {
                GetCheckBox(value).Click();
            }
        }

        public void Uncheck(int[] indexes)
        {
            ItemLocator = CheckListLocator;
            foreach (var index in indexes)
            {
                if (IsChecked(index))
                {
                    GetCheckBox(index).Click();
                }
            }
        }

        public void Uncheck(int index)
        {
            ItemLocator = CheckListLocator;
            if (IsChecked(index))
            {
                GetCheckBox(index).Click();
            }
        }

        public void UncheckAll()
        {
            var els = WebElement.FindElements(CheckListLocator);
            foreach (var checkBox in els)
            {
                Invoker.DoAction($"Unselect item '{string.Join(" -> ", checkBox.GetAttribute("id"))}'",
                    () =>
                    {
                        if (checkBox?.GetAttribute("checked") != null)
                        {
                            checkBox.Click();
                        }
                    });
            }
        }

        public void CheckAll()
        {
            var els = WebElement.FindElements(CheckListLocator);
            foreach (var checkBox in els)
            {
                Invoker.DoAction($"Unselect item '{string.Join(" -> ", checkBox.GetAttribute("id"))}'",
                    () =>
                    {
                        if (checkBox?.GetAttribute("checked") == null)
                        {
                            checkBox.Click();
                        }
                    });
            }
        }

        public string[] GetChecked(Array values)
        {
            var selectedItems = new List<string>();
            foreach (var value in values.ToStringArray())
            {
                if (IsChecked(value))
                {
                    selectedItems.Add(value);
                }
            }
            selectedItems.Reverse();
            return selectedItems.ToArray();
        }

        public bool IsChecked(string value)
        {
            return GetCheckBox(value)?.GetAttribute("checked") != null;
        }
        public bool IsChecked(int index)
        {
            return GetCheckBox(index)?.GetAttribute("checked") != null;
        }

        public bool IsDisabled(string value)
        {
            return GetCheckBox(value)?.GetAttribute("disabled") != null;
        }

        public bool IsDisabled(int index)
        {
            return GetCheckBox(index)?.GetAttribute("disabled") != null;
        }

        private IWebElement GetCheckBox(string value)
        {
            var els = WebElement.FindElements(CheckListLocator);
            return els.FirstOrDefault(e => e.GetAttribute("id").Equals(value));
        }

        private IWebElement GetCheckBox(int index)
        {
            var els = WebElement.FindElements(CheckListLocator);
            return els[index];
        }
    }
}