using System;
using System.Linq;
using JDI.Light.Elements.Base;
using JDI.Light.Exceptions;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class Selector : UIElement
    {
        public By ItemLocator { get; set; }

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
                throw new ElementNotFoundException($"Can't find element '{item}' to select. ");
            }
        };

        private Action<Selector> _selectByIndex = (selector) =>
        {
            var els = selector.WebElement.FindElements(selector.ItemLocator);
            els.FirstOrDefault()?.Click();
        };

        private Func<Selector, string> _getSelected = (selector) => selector.Text;
       
        public Selector(By byLocator) : base(byLocator)
        {
        }

        public void Select(string value, Selector elem)
        {
            Invoker.DoAction($"Select item '{string.Join(" -> ", value)}'",
                () => _selectElementAction.Invoke(elem, value));
        }

        public void Select(int index, Selector elem)
        {
            Invoker.DoAction($"Select item with index - '{string.Join(" -> ", index)}'",
                () => _selectByIndex.Invoke(elem));
        }

        public void Select(string[] values, Selector elem)
        {
            foreach (var value in values)
            {
                Invoker.DoAction($"Select item '{string.Join(" -> ", value)}'",
                    () => _selectElementAction.Invoke(elem, value));
            }
        }
        
        public string GetSelected(Selector elem)
        {
            return Invoker.DoActionWithResult("Get value", () => _getSelected.Invoke(elem));
        }
    }
}