using System;
using JDI.Light.Interfaces.Common;
using JDI.Light.Selenium.Elements.Base;
using OpenQA.Selenium;

namespace JDI.Light.Selenium.Elements.Common
{
    public class FileInput : TextField, IFileInput
    {
        protected new Action<UIElement, string> SetValueAction = (el, val) => ((FileInput) el).Input(val);

        public FileInput() : this(null)
        {
        }

        public FileInput(By byLocator = null)
            : base(byLocator)
        {
        }
    }
}