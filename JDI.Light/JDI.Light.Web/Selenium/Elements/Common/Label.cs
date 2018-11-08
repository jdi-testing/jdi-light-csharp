using JDI.Core.Interfaces.Common;
using JDI.Web.Selenium.Base;
using JDI.Web.Selenium.Elements.Base;
using OpenQA.Selenium;

namespace JDI.Web.Selenium.Elements.Common
{
    public class Label: ClickableText, ILabel
    {
        public Label() : this(null) { }
        public Label(By byLocator = null, IWebElement webElement = null, WebBaseElement element = null)
            : base(byLocator, webElement, element:element) { }
    }
}
