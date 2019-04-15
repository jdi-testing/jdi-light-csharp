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
        public By CheckListLocator => By.CssSelector(".html-left > input");

        public By LabelLocator => By.CssSelector(".html-left > label");

        private List<UIElement> Labels => FindElements(LabelLocator)
            .Select(e => UIElementFactory.CreateInstance<UIElement>(LabelLocator, this, e)).ToList();

        private List<UIElement> CheckBoxes => FindElements(CheckListLocator)
            .Select(e => UIElementFactory.CreateInstance<UIElement>(CheckListLocator, this, e)).ToList();

        public CheckList(By byLocator) : base(byLocator)
        {
        }

        public void Check(params string[] names)
        {
            foreach (var name in Values())
            {
                var value = CheckBoxes[GetIndexOf(name)];
                if (IsDisabled(value))
                {
                    continue;
                }
                if (IsSelected(value) && !names.Contains(name)
                    || !IsSelected(value) && names.Contains(name))
                {
                    value.Click();
                }
            }
        }

        public void Check(int[] indexes)
        {
            for (int i = 1; i <= Values().Count; i++)
            {
                var value = CheckBoxes[i - 1];
                if (IsDisabled(value))
                {
                    continue;
                }
                if (IsSelected(value) && !indexes.Contains(i)
                    || !IsSelected(value) && indexes.Contains(i))
                {
                    value.Click();
                }
            }
        }

        public void Uncheck(params string[] names)
        {
            foreach (var name in Values())
            {
                var value = CheckBoxes[GetIndexOf(name)];
                if (IsDisabled(value))
                {
                    continue;
                }
                if (IsSelected(value) && names.Contains(name)
                    || !IsSelected(value) && !names.Contains(name))
                {
                    value.Click();
                }
            }
        }

        public void Uncheck(params int[] indexes)
        {
            for (int i = 1; i <= Values().Count; i++)
            {
                var value = CheckBoxes[i - 1];
                if (IsDisabled(value))
                {
                    continue;
                }
                if (IsSelected(value) && indexes.Contains(i)
                    || !IsSelected(value) && !indexes.Contains(i)) 
                {
                    value.Click();
                }
            }
        }

        public void Select(params string[] values)
        {
            var indexes = values.Select(GetIndexOf);
            foreach (var index in indexes)
            {
                if (IsEnabled(CheckBoxes[index]))
                {
                    CheckBoxes[index].Click();
                }
            }
        }

        public void Select(params int[] indexes)
        {
            foreach (var index in indexes)
            {
                if (IsEnabled(CheckBoxes[index-1]))
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

        public string[] Checked()
        {
            var checkedIds = GetCheckedUIElements().Select(checkbox => checkbox.GetAttribute("id")).ToList();

            return Labels.Where(label => checkedIds.Contains(label.GetAttribute("for"))).Select(label => label.Text)
                .ToArray();
        }

        public List<string> Values() => Labels.Select(label => label.Text.Trim()).ToList();

        private IEnumerable<UIElement> GetCheckedUIElements() => CheckBoxes.Where(IsChecked);

        private int GetIndexOf(string name) => Labels.FindIndex(label => label.Text == name);

        private bool IsChecked(UIElement checkbox) => checkbox.GetAttribute("checked") != null;

        private bool IsSelected(UIElement checkbox) => checkbox.Selected;

        // todo remove after inmplementation JDIBase class with is disabled method
        private bool IsEnabled(UIElement checkbox) => checkbox.WebElement.Enabled;

        private bool IsDisabled(UIElement checkbox) => !IsEnabled(checkbox);
    }
}