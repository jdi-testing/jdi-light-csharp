using System;
using System.Collections.Generic;
using System.Linq;
using JDI.Light.Elements.Base;
using JDI.Light.Factories;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class CheckList : Selector, ICheckList
    {
        public By CheckListLocator { get; set; }

        public By LabelLocator => By.CssSelector(".html-left > label");

        private List<UIElement> Labels => this.FindElements(LabelLocator)
            .Select(e => UIElementFactory.CreateInstance<UIElement>(LabelLocator, this, e)).ToList();

        private List<UIElement> CheckBoxes => this.FindElements(CheckListLocator)
            .Select(e => UIElementFactory.CreateInstance<UIElement>(CheckListLocator, this, e)).ToList();

        public CheckList(By byLocator) : base(byLocator)
        {
            CheckListLocator = By.CssSelector(".html-left > input");
            ItemLocator = LabelLocator;
        }

        public void Check(params string[] values)
        {
            var indexes = values.Select(GetIndexOf);
            foreach (var index in indexes)
            {
                if (IsDisabled(CheckBoxes[index]))
                {
                    continue;
                }
                if (!IsChecked(CheckBoxes[index]))
                {
                    CheckBoxes[index].Click();
                }
            }
        }

        public void Check(int[] indexes)
        {
            foreach (var index in indexes)
            {
                if (IsDisabled(CheckBoxes[index - 1]))
                {
                    continue;
                }
                if (!IsChecked(CheckBoxes[index-1]))
                {
                    CheckBoxes[index-1].Click();
                }
            }
        }

        public void Uncheck(params string[] values)
        {
            var indexes = values.Select(GetIndexOf);
            foreach (var index in indexes)
            {
                if (IsChecked(CheckBoxes[index]))
                {
                    CheckBoxes[index].Click();
                }
            }
        }

        public void Uncheck(params int[] indexes)
        {
            foreach (var index in indexes)
            {
                if (IsChecked(CheckBoxes[index-1]))
                {
                    CheckBoxes[index-1].Click();
                }
            }
        }

        public void UncheckAll()
        {
            foreach (var element in GetCheckedUIElements())
            {
                element.Click();
            }
        }

        public string[] GetChecked()
        {
            var checkedIds = GetCheckedUIElements().Select(checkbox => checkbox.GetAttribute("id")).ToList();

            return Labels.Where(label => checkedIds.Contains(label.GetAttribute("for"))).Select(label => label.Text)
                .ToArray();
        }

        private IEnumerable<UIElement> GetCheckedUIElements() => CheckBoxes.Where(IsChecked);

        private int GetIndexOf(string name) => Labels.FindIndex(label => label.Text == name);

        private bool IsChecked(UIElement checkbox) => checkbox.GetAttribute("checked") != null;

        // todo remove after inmplementation JDIBase class with is disabled method
        private bool IsDisabled(UIElement checkbox) => !checkbox.WebElement.Enabled;
    }
}