using System;
using System.Collections.Generic;
using System.Linq;
using JDI.Light.Elements.Base;
using JDI.Light.Extensions;
using JDI.Light.Factories;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class CheckList : Selector, ICheckList
    {
        public By CheckListLocator { get; set; }

        public By LabelLocator { get; set; }

        private IEnumerable<UIElement> Labels => this.FindElements(LabelLocator)
            .Select(e => UIElementFactory.CreateInstance<UIElement>(LabelLocator, this, e)).ToList();

        private IEnumerable<UIElement> checkBoxes => this.FindElements(CheckListLocator)
            .Select(e => UIElementFactory.CreateInstance<UIElement>(CheckListLocator, this, e)).ToList();

        public CheckList(By byLocator) : base(byLocator)
        {
            //CheckListLocator = By.CssSelector(".html-left > input");
            //LabelLocator = By.CssSelector(".html-left > label");
            CheckListLocator = By.CssSelector(".html-left > input");
            LabelLocator = By.CssSelector(".html-left > label");
            ItemLocator = LabelLocator;
        }

        public void Check(string[] values)
        {
            
            Select(values, this);
        }

        public void Check(string value)
        {
            Select(new []{value}, this);
        }

        public void Check(int[] indexes)
        {
            Select(indexes, this);
        }

        public void Check(int index)
        {
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

        public void UncheckAll()
        {
            foreach (var value in GetChecked())
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

        public string[] GetChecked()
        {
            var checkedIds = checkBoxes.Where(checkbox => checkbox.GetAttribute("checked") != null)
                .Select(checkbox => checkbox.GetAttribute("id")).ToList();

            return Labels.Where(label => checkedIds.Contains(label.GetAttribute("for"))).Select(label => label.Text)
                .ToArray();
        }
    }
}