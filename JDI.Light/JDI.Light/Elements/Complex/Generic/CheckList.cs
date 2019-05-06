using System;
using System.Collections.Generic;
using System.Linq;
using JDI.Light.Asserts;
using JDI.Light.Elements.Base;
using JDI.Light.Exceptions;
using JDI.Light.Factories;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Common;
using JDI.Light.Interfaces.Complex.Generic;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Complex.Generic
{
    public class CheckList<TCheckBox> : UIElement, ICheckList<TCheckBox> where TCheckBox : ICheckBox
    {
        public By CheckListLocator { get; set; }

        public By LabelLocator { get; set; }

        private List<ILabel> Labels => FindElements(LabelLocator)
            .Select(element => UIElementFactory.CreateInstance<ILabel>(LabelLocator, this, element)).ToList();

        public List<TCheckBox> CheckBoxes => FindElements(CheckListLocator)
            .Select(element => UIElementFactory.CreateInstance<TCheckBox>(CheckListLocator, this, element)).ToList();

        public CheckList(By byLocator) : base(byLocator)
        {
            CheckListLocator = By.CssSelector(".html-left > input");
            LabelLocator = By.CssSelector(".html-left > label");
        }

        public void Check(bool checkEnabled = true, params string[] names)
        {
            var indexes = names.Select(name => GetIndexOf(name) + 1).ToArray();
            Check(checkEnabled, indexes);
        }

        public void Check(bool checkEnabled = true, params int[] indexes)
        {
            for (int i = 1; i <= Values().Count; i++)
            {
                var value = CheckBoxes[i - 1];
                if (value.IsChecked ^ indexes.Contains(i))
                {
                    CheckEnabled(value, checkEnabled);
                    value.Click();
                }
            }
        }

        public void Uncheck(bool checkEnabled = true, params string[] names)
        {
            var indexes = names.Select(name => GetIndexOf(name) + 1).ToArray();
            Uncheck(checkEnabled, indexes);
        }

        public void Uncheck(bool checkEnabled = false, params int[] indexes)
        {
            for (int i = 1; i <= Values().Count; i++)
            {
                var value = CheckBoxes[i - 1];
                if (value.IsChecked == indexes.Contains(i))
                {
                    CheckEnabled(value, checkEnabled);
                    value.Click();
                }
            }
        }

        public void Select(bool checkEnabled = true, params string[] values)
        {
            var indexes = values.Select(GetIndexOf);
            foreach (var index in indexes)
            {
                CheckEnabled(CheckBoxes[index], checkEnabled);
                CheckBoxes[index].Click();
            }
        }

        public void Select(bool checkEnabled = true, params int[] indexes)
        {
            foreach (var index in indexes)
            {
                CheckEnabled(CheckBoxes[index - 1], checkEnabled);
                CheckBoxes[index - 1].Click();
            }
        }

        public void UncheckAll(bool checkEnabled = false)
        {
            foreach (var element in GetCheckedUIElements())
            {
                CheckEnabled(element, checkEnabled);
                element.Click();
            }
        }

        public void CheckAll(bool checkEnabled = true)
        {
            foreach (var checkBox in CheckBoxes)
            {
                if (!checkBox.IsChecked && checkBox.Enabled)
                {
                    checkBox.Click();
                }
            }
        }

        public List<string> Checked() => GetRequiredOption(GetCheckedUIElements);

        public bool IsChecked(string value)
        {
            return CheckBoxes[GetIndexOf(value)].IsChecked;
        }

        public bool IsChecked(int index)
        {
            return CheckBoxes[index - 1].IsChecked;
        }

        public bool IsDisabled(string value)
        {
            return CheckBoxes[GetIndexOf(value)].Enabled != true;
        }

        public bool IsDisabled(int index)
        {
            return CheckBoxes[index - 1].Enabled != true;
        }

        public List<string> Values() => Labels.Select(label => label.Text.Trim()).ToList();

        public List<string> ListEnabled() => GetRequiredOption(GetEnabledUIElements);

        public List<string> ListDisabled() => GetRequiredOption(GetDisabledUIElements);

        public bool Selected(string option)
        {
            return Checked().Contains(option);
        }

        public List<string> Value => Checked();

        public List<IBaseUIElement> AllUI() => CheckBoxes.Cast<IBaseUIElement>().ToList();

        public int GetSize() => CheckBoxes.Count;

        public bool HasAny() => CheckBoxes.Any();

        public bool IsEmpty() => !HasAny();

        public new SelectAssert Is => new SelectAssert(this);

        public new SelectAssert AssertThat => Is;

        public new SelectAssert Has => Is;

        private IEnumerable<TCheckBox> GetCheckedUIElements() => CheckBoxes.Where(element => element.IsChecked);

        private IEnumerable<TCheckBox> GetEnabledUIElements() => CheckBoxes.Where(element => element.Enabled);

        private IEnumerable<TCheckBox> GetDisabledUIElements() => CheckBoxes.Where(element => !element.Enabled);

        private List<string> GetRequiredOption(Func<IEnumerable<TCheckBox>> option)
        {
            var requiredIds = option().Select(checkbox => checkbox.GetAttribute("id")).ToList();

            return Labels.Where(label => requiredIds.Contains(label.GetAttribute("for"))).Select(label => label.Text)
                .ToList();
        }

        private int GetIndexOf(string name)
        {
            var index = Labels.FindIndex(label => label.Text == name);
            if (index == -1)
            {
                throw new ElementNotFoundException($"cant find label: {name}");
            }
            return index;
        }

        private void CheckEnabled(TCheckBox checkbox, bool checkEnabled = true)
        {
            if (checkEnabled)
            {
                if (!checkbox.Enabled)
                {
                    throw new ElementDisabledException(this);
                }
            }
        }
    }
}