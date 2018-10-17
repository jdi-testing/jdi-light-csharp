using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using JDI.Light.Elements.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace JDI.Light.Elements.Base
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class UIElement : IWebElement, IBaseElement
    {
        private readonly IWebElement _webElement;
        private readonly IWebDriver _webDriver;
        private readonly Js _js;

        public UIElement(IWebElement element, IWebDriver webDriver)
        {
            _webElement = element;
            _webDriver = webDriver;
            _js = new Js(_webDriver, _webElement);
        }

        public string TagName => _webElement.TagName;
        public string Text => _webElement.Text;
        public bool Enabled => _webElement.Enabled;
        public bool Selected => _webElement.Selected;
        public Point Location => _webElement.Location;
        public Size Size => _webElement.Size;
        public bool Displayed => _webElement.Displayed;

        #region IBaseElement methods

        public string GetValue()
        {
            var text = _webElement.Text;
            var value = string.IsNullOrWhiteSpace(text) ? _webElement.GetAttribute("value") : text;
            return value;
        }

        public void Hover()
        {
            _js.ScrollIntoView();
        }

        public Point GetLocation()
        {
            return _webElement.Location;
        }

        public Size GetSize()
        {
            return _webElement.Size;
        }

        public Rectangle GetRect()
        {
            return new Rectangle(_webElement.Location, _webElement.Size);
        }

        public Screenshot GetScreenshot()
        {
            Show();
            var screenshot = _webDriver.TakeScreenshot();
            return screenshot;
        }

        public void SetAttribute(string name, string value)
        {
            _js.SetAttribute(name, value);
        }

        public void Highlight(string color)
        {
            _js.AddBorder(color);
        }

        public void Highlight()
        {
            _js.AddBorder();
        }

        public void Show()
        {
            _js.ScrollIntoView();
        }

        public SelectElement Select()
        {
            return new SelectElement(_webElement);
        }

        #endregion
        
        #region IWebElement methods
        
        public IWebElement FindElement(By by)
        {
            return _webElement.FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return _webElement.FindElements(by);
        }

        public void Clear()
        {
            _webElement.Clear();
        }

        public void SendKeys(string text)
        {
            _webElement.SendKeys(text);
        }

        public void Submit()
        {
            _webElement.Submit();
        }

        public void Click()
        {
            _webElement.Click();
        }

        public string GetAttribute(string name)
        {
            return _webElement.GetAttribute(name);
        }

        public string GetProperty(string propertyName)
        {
            return _webElement.GetProperty(propertyName);
        }

        public string GetCssValue(string propertyName)
        {
            return _webElement.GetCssValue(propertyName);
        }

        #endregion

    }
}