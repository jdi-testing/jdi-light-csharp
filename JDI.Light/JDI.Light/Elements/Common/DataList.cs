using System;
using System.Collections.Generic;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class DataList : Selector, IDataList
    {
        public DataList(By byLocator) : base(byLocator)
        {
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
            // TODO: Select by index
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

        public List<string> ListEnabled()
        {
            // TODO: Return list of enabled values
            throw new NotImplementedException();
        }

        public List<string> ListDisabled()
        {
            // TODO: Return list of disabled values
            throw new NotImplementedException();
        }

        private void SetText(string text)
        {
            var str = "value='" + text + "'";
            JsExecutor.ExecuteScript("arguments[0]." + str + ";", WebElement);
        }
    }
}