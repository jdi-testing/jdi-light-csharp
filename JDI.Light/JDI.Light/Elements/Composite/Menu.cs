using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JDI.Light.Asserts;
using JDI.Light.Elements.Base;
using JDI.Light.Exceptions;
using JDI.Light.Extensions;
using JDI.Light.Interfaces.Complex;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace JDI.Light.Elements.Composite
{
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    public class Menu : UIElement, IMenuSelector
    {
        public By MenuItemLocator { get; set; } = By.XPath(".//li/a");
        public ReadOnlyCollection<IWebElement> MenuList => FindElements(By.XPath(".//li"));

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

        public List<string> Values()
        {
            return MenuList.Select(i => i.Text).ToList();
        }

        public new int Size() => Values().Count;

        public void Select(params Enum[] items)
        {
            var itemTexts = items.Select(i => i.GetDescription()).ToArray();
            Select(itemTexts);
        }

        public void Select(params int[] items)
        {
            foreach (var item in items)
            {
                var itemText = MenuList.ElementAt(item);
                Select(itemText.Text);
            }
        }

        public void Select(params string[] itemTexts)
        {
            Invoker.DoAction($"Select menu item '{string.Join(" -> ", itemTexts)}'", () => _selectElementAction.Invoke(this, itemTexts));
        }

        public bool Selected(string option)
        {
            return MenuList.FirstOrDefault(e => e.Text.Equals(option)).GetAttribute("class").Contains("active");
        }

        public string Selected()
        {
            return MenuList.FirstOrDefault(e => e.GetAttribute("class").Contains("active")).Text;
        }

        public void HoverAndClick(params string[] values)
        {
            foreach (var value in values)
            {
                foreach (var item in MenuList)
                {
                    if (item.Text == value)
                    {
                        var action = new Actions(WebDriver);
                        action.MoveToElement(item).Click().Build().Perform();
                        break;
                    }
                }
            }
        }

        public new MenuSelectAssert Is => new MenuSelectAssert(this);

        public new MenuSelectAssert AssertThat => Is;
    }
}