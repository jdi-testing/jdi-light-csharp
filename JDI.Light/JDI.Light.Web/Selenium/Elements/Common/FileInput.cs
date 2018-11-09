using System;
using JDI.Core.Interfaces.Common;
using JDI.Web.Selenium.Base;
using OpenQA.Selenium;

namespace JDI.Web.Selenium.Elements.Common
{
    public class FileInput : TextField, IFileInput
    {
        protected new Action<WebBaseElement, string> SetValueAction = (el, val) => ((FileInput) el).Input(val);

        public FileInput() : this(null)
        {
        }

        public FileInput(By byLocator = null, IWebElement webElement = null, WebBaseElement element = null)
            : base(byLocator, webElement, element)
        {
        }
    }
}