using System;
using System.Collections.Generic;
using JDI.Light.Exceptions;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class DataList : Selector, IDataList
    {
        public DataList(By byLocator) : base(byLocator)
        {
            LocalElementSearchCriteria = element => element != null;
        }
        
        public void Select(string text)
        {
            SetText(text);
        }

        public void Select(Enum value)
        {
            SetText(value.ToString());
        }

        public void Select(int index)
        {
        //   var v = GetAttribute("value");
           var els = FindElements(Locator);
           _selectByIndex(this, index);
        }

        private readonly Action<DataList, int> _selectByIndex = (selector, index) =>
        {
            var els = selector.WebElement.FindElements(selector.Locator);
            try
            {
                var el = els[index];
                el.Click();
            }
            catch (Exception e)
            {
                throw new ElementNotFoundException($"Can't find element with index - '{index}' to select. " + e.Message);
            }
        };

        public string Selected()
        {
            return WebElement.GetAttribute("value");
        }

        public List<string> Values()
        {
            // TODO: Return list of values
           throw new NotImplementedException();

          
        }
        
        private void SetText(string text)
        {
            var str = "value='" + text + "'";
            JsExecutor.ExecuteScript("arguments[0]." + str + ";", WebElement);
        }
    }
}