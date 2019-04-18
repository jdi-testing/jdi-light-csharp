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
                if (!value.Enabled)
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
                if (!value.Enabled)
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
                if (CheckBoxes[index].Enabled)
                {
                    CheckBoxes[index].Click();
                }
            }
        }

        public void Select(params int[] indexes)
        {
            foreach (var index in indexes)
            {
                if (CheckBoxes[index-1].Enabled)
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

        public void CheckAll()
        {
            foreach (var checkBox in CheckBoxes)
            {
                if (!checkBox.IsChecked && checkBox.Enabled)
                {
                    checkBox.Click();
                }
            }
        }

        public string[] Checked()
        {
            var checkedIds = GetCheckedUIElements().Select(checkbox => checkbox.GetAttribute("id")).ToList();

            return Labels.Where(label => checkedIds.Contains(label.GetAttribute("for"))).Select(label => label.Text)
                .ToArray();
        }

        public bool IsChecked(string value)
        {
            return CheckBoxes[GetIndexOf(value) - 1].GetAttribute("checked") != null;
        }

        public bool IsChecked(int index)
        {
            return CheckBoxes[index - 1].GetAttribute("checked") != null;
        }

        public bool IsDisabled(string value)
        {
            return CheckBoxes[GetIndexOf(value)].Enabled != true;
        }

        public bool IsDisabled(int index)
        {
            return CheckBoxes[index - 1].Enabled != true;
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
    }
}