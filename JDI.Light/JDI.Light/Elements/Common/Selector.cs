using System;
using System.Linq;
using JDI.Light.Elements.Base;
using JDI.Light.Exceptions;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class Selector : UIElement
    {
        public Selector(By byLocator) : base(byLocator)
        {
        }
        
        public By ItemLocator;
        private Action<Selector, string> _selectElementAction = (selector, item) =>
        {
            var els = selector.WebElement.FindElements(selector.ItemLocator);

            var itemsList = els.FirstOrDefault(e => e.Text.Equals(item));
            if (itemsList != null)
            {
                itemsList.Click();
            }
            else
            {
                throw new ElementNotFoundException($"Can't find dataList element '{item}' to select. ");
            }
        };

        private Action<Selector> _selectByIndex = (selector) =>
        {
            var els = selector.WebElement.FindElements(selector.ItemLocator);
            els.FirstOrDefault()?.Click();
        };

        public void SelectItem(string value, Selector elem)
        {
            ItemLocator = By.XPath("//li/a");
            Invoker.DoAction($"Select item '{string.Join(" -> ", value)}'",
                () => _selectElementAction.Invoke(elem, value));
        }

        public void SelectItem(int index, Selector elem)
        {
            ItemLocator = By.CssSelector(string.Format($"li:nth-child({index.ToString()})"));
            Invoker.DoAction($"Select item with index - '{string.Join(" -> ", index)}'",
                () => _selectByIndex.Invoke(elem));
        }

    }
}