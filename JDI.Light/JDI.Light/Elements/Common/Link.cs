using JDI.Light.Asserts;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class Link : TextElement, ILink
    {
        public Link() : this(null)
        {
        }

        public Link(By byLocator = null)
            : base(byLocator)
        {
        }

        public string Alt()
        {
            return GetAttribute("alt");
        }

        public string Ref()
        {
            return GetAttribute("href");
        }

        public string Url()
        {
            return Jdi.WebDriver.Url;
        }

        public new LinkAssert Is() => new LinkAssert(this);

        public new LinkAssert AssertThat() => Is();
    }
}