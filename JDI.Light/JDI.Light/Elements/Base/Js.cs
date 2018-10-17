using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace JDI.Light.Elements.Base
{
    public class Js
    {
        private readonly IWebDriver _webDriver;
        private readonly IWebElement _webElement;

        public Js(IWebDriver webDriver, IWebElement webElement)
        {
            _webDriver = webDriver;
            _webElement = webElement;
        }

        public void Execute(string script, params object[] args)
        {
            _webDriver.ExecuteJavaScript(script, args);
        }

        public void Execute<T>(string script, params object[] args)
        {
            _webDriver.ExecuteJavaScript<T>(script, args);
        }

        public void ExecuteOnElement(string scriptText)
        {
            Execute($"arguments[0].{scriptText};", _webElement);
        }

        public void SetAttribute(string name, string value)
        {
            ExecuteOnElement($"setAttribute('{name}','{value}')");
        }

        public void ScrollIntoView()
        {
            ExecuteOnElement("scrollIntoView(true)");
        }

        public void AddBorder()
        {
            ExecuteOnElement("style.border='3px dashed red'");
        }

        public void AddBorder(string color)
        {
            ExecuteOnElement($"style.border='3px dashed {color}'");
        }

    }
}