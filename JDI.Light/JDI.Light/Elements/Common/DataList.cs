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

        public By Value { get; set; }
        public By List { get; set; }

        public void Setup(By root, By list)
        {
            if (root != null)
            {
                Value = root;
            }

            if (list != null)
            {
                List = list;
            }
        }

        public void Select(string text)
        {
        //    var el = FindElement(Value);
            SetText(text);
        }

        public void Select(Enum value)
        {
            SetText(value.ToString());
        }

        public By ItemLocator { get; set; }

        public void Select(int index)
        {
            ItemLocator = By.CssSelector("option");
            // var select = _getValueByIndex(this.Parent, index);
            var select ="";
            var els =WebDriver.FindElements(List);
            try
            {
                var el = els[index];
                 select = el.GetAttribute("value");
            }
            catch (Exception e)
            {
                throw new ElementNotFoundException($"Can't find element with index - '{index}' to select. " + e.Message);
            }
            SetText(select);
        }

      
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