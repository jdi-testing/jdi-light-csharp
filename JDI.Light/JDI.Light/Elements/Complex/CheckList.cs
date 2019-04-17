using System.Collections.Generic;
using System.Linq;
using JDI.Light.Elements.Base;
using JDI.Light.Exceptions;
using JDI.Light.Factories;
using JDI.Light.Interfaces.Complex;
using OpenQA.Selenium;
using CheckBox = JDI.Light.Elements.Common.CheckBox;
using Label = JDI.Light.Elements.Common.Label;

namespace JDI.Light.Elements.Complex
{
    public class CheckList : UIElement, ICheckList
    {
        public By CheckListLocator { get; set; }

        public By LabelLocator { get; set; }

        private List<Label> Labels => FindElements(LabelLocator)
            .Select(element => UIElementFactory.CreateInstance<Label>(LabelLocator, this, element)).ToList();

        private List<CheckBox> CheckBoxes => FindElements(CheckListLocator)
            .Select(element => UIElementFactory.CreateInstance<CheckBox>(CheckListLocator, this, element)).ToList();

        public CheckList(By byLocator) : base(byLocator)
        {
            CheckListLocator = By.CssSelector(".html-left > input");
            LabelLocator = By.CssSelector(".html-left > label");
        }

        public void Check(params string[] names)
        {
            var indexes = names.Select(name => GetIndexOf(name) + 1).ToArray();
            Check(indexes);
        }

        public void Check(int[] indexes)
        {
            for (int i = 1; i <= Value.Count; i++)
            {
                var value = CheckBoxes[i - 1];
                if (IsDisabled(value))
                {
                    continue;
                }
                if (value.Selected ^ indexes.Contains(i))

                {
                    value.Click();
                }
            }
        }

        public void Uncheck(params string[] names)
        {
            var indexes = names.Select(name => GetIndexOf(name) + 1).ToArray();
            Uncheck(indexes);
        }

        public void Uncheck(params int[] indexes)
        {
            for (int i = 1; i <= Value.Count; i++)
            {
                var value = CheckBoxes[i - 1];
                if (IsDisabled(value))
                {
                    continue;
                }
                if (value.Selected == indexes.Contains(i))
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

        public List<string> Value => Labels.Select(label => label.Text.Trim()).ToList();

        private IEnumerable<UIElement> GetCheckedUIElements() => CheckBoxes.Where(element => element.IsChecked);

        private int GetIndexOf(string name)
        {
            var index = Labels.FindIndex(label => label.Text == name);
            if (index == -1)
            {
                throw new ElementNotFoundException($"cant find label: {name}");
            }
            return index;
        }

        // todo remove after inmplementation JDIBase class with is disabled method
        private bool IsEnabled(UIElement checkbox) => checkbox.WebElement.Enabled;

        private bool IsDisabled(UIElement checkbox) => !IsEnabled(checkbox);
    }
}