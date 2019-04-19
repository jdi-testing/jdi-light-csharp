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

        public By ValuesLocator { get; set; }

        public void Setup(By values)
        {
            if (values != null)
            {
                ValuesLocator = values;
            }
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
            string select;
            var els = WebDriver.FindElements(ValuesLocator);
            try
            {
                var el = els[index - 1];
                select = el.GetAttribute("value");
            }
            catch (Exception e)
            {
                throw new ElementNotFoundException($"Can't find element with index - '{index - 1}' to select. " + e.Message);
            }
            SetText(select);
        }
        
        public string Selected()
        {
            return WebElement.GetAttribute("value");
        }

        public List<string> Values()
        {
            var list = new List<string>();
            var els = WebDriver.FindElements(ValuesLocator);
            foreach (var el in els)
            {
                list.Add(el.GetAttribute("value"));
            }
            return list;
        }
        
        private void SetText(string text)
        {
            var str = "value='" + text + "'";
            JsExecutor.ExecuteScript("arguments[0]." + str + ";", WebElement);
        }
    }
}