using OpenQA.Selenium;
using System;

namespace JDI.Light.Settings
{
    public static class WebSettings
    {
        public static Func<IWebElement, bool> VisibleElement { get; set; } = el => el.Displayed;
        public static Func<IWebElement, bool> SearchCondition { get; set; } = VisibleElement;
        public static Func<IWebElement, bool> AnyElement { get; set; } = el => el != null;
        public static Func<IWebElement, bool> EnabledElement { get; set; } = el => el != null && el.Displayed && el.Enabled;
    }
}
