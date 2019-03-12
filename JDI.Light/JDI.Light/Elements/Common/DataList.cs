using System;
using System.Linq;
using JDI.Light.Elements.Base;
using JDI.Light.Exceptions;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class DataList : Selector, IDataList
    {
        public DataList(By byLocator) : base(byLocator)
        {
        }

        public bool Value { get; set; }
        
        public void Select(string value)
        {
            SelectItem(value, this);
        }

        public void Select(int index)
        {
            SelectItem(index, this);
        }
        
        public string GetSelected()
        {
            return "";
        }

        public void Input(string text)
        {
            Clear();
            Invoker.DoActionWithWait($"Input text '{text}' in text field", () => WebElement.SendKeys(text));
        }
        
    }
}