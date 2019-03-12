using System;
using System.Linq;
using JDI.Light.Elements.Base;
using JDI.Light.Exceptions;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class DropDown : UIElement, IDropDown
    {
        public DropDown(By byLocator) : base(byLocator)
        {
        }

        public DropDown() : this(null)
        {
        }

        public By DropDownItemLocator;
        private Action<DropDown, string> _selectElementAction = (dropDown, item) =>
        {
            dropDown.Click();
            var els = dropDown.WebElement.FindElements(dropDown.DropDownItemLocator);
            
            var dropDownItem = els.FirstOrDefault(e => e.Text.Equals(item));
            if (dropDownItem != null)
            {
                dropDownItem.Click();
            }
            else
            {
                throw new ElementNotFoundException($"Can't find dropdown element '{item}' to select. ");
            }
        };

        private Action<DropDown> _selectByIndex = (dropDown) =>
        {
            dropDown.Click();
            var els = dropDown.WebElement.FindElements(dropDown.DropDownItemLocator);
            els.FirstOrDefault()?.Click();
        };
        
        
        public bool Value { get; set; }

        public void Select(string value)
        {
            Invoker.DoAction($"Select dropdown item '{string.Join(" -> ", value)}'", 
                () => _selectElementAction.Invoke(this, value));
        }

        public void Select(int index)
        {
            DropDownItemLocator = By.CssSelector(string.Format($"li:nth-child({index.ToString()})"));
            Invoker.DoAction($"Select dropdown item with index - '{string.Join(" -> ", index)}'",
                () => _selectByIndex.Invoke(this));
        }
    }
}