﻿using System;
using System.Linq;
using JDI.Light.Elements.Base;
using JDI.Light.Exceptions;
using JDI.Light.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace JDI.Light.Elements.Composite
{
    public class Menu : UIElement
    {
        public By MenuItemLocator { get; set; } = By.XPath(".//li/a");

        private Action<Menu, string[]> _selectElementAction = (menu, itemTexts) =>
            {
                for (var i = 0; i < itemTexts.Length - 1; i++)
                {
                    var item = itemTexts[i];
                    var menuItem = menu.WebElement.FindElements(menu.MenuItemLocator).FirstOrDefault(e => e.Text.Equals(item));
                    var action = new Actions(menu.WebDriver);
                    action.MoveToElement(menuItem).Click().Build().Perform();
                }

                var lastItemText = itemTexts.Last();
                var els = menu.WebElement.FindElements(menu.MenuItemLocator);
                var lastItem = els.FirstOrDefault(e => e.Text.Equals(lastItemText));
                if (lastItem != null)
                {
                    lastItem.Click();
                }
                else
                {
                    var elementsList = string.Join(", ",
                        els.Where(e => !string.IsNullOrEmpty(e.Text)).Select(e => e.Text));
                    throw new ElementNotFoundException($"Can't find menu element '{lastItemText}' to select. " +
                                                       $"Available options are {elementsList}");
                }
            };

        public Menu(By locator) : base(locator)
        {
        }

        public void Select(string itemText)
        {
            Invoker.DoAction($"Select menu item '{itemText}'", () => _selectElementAction.Invoke(this, new[] { itemText }));
        }

        public void Select(Enum item)
        {
            Select(item.GetDescription());
        }

        public void Select(Enum item1, Enum item2)
        {
            Select(item1.GetDescription(),item2.GetDescription());
        }

        public void Select(Enum item1, Enum item2, Enum item3)
        {
            Select(item1.GetDescription(), item2.GetDescription(), item3.GetDescription());
        }

        public void Select(string itemText1, string itemText2)
        {
            Select(new[]{itemText1, itemText2});
        }

        public void Select(string itemText1, string itemText2, string itemText3)
        {
            Select(new[] { itemText1, itemText2, itemText3 });
        }

        public void Select(string[] itemTexts)
        {
            Invoker.DoAction($"Select menu item '{string.Join(" -> ", itemTexts)}'", () => _selectElementAction.Invoke(this, itemTexts));
        }
    }
}