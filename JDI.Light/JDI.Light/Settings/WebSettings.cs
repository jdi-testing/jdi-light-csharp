using OpenQA.Selenium;
using System;

namespace JDI.Light.Settings
{
    public class WebSettings
    {
        public static Func<IWebElement, bool> VisibleElement = el => el.Displayed;
        public static Func<IWebElement, bool> SearchCondition = VisibleElement;
        public static Func<IWebElement, bool> AnyElement = el => el != null;
        public static Func<IWebElement, bool> EnabledElement = el => el != null && el.Displayed && el.Enabled;
    }
}
