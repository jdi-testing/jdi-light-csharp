using JDI.Light.Asserts;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class Icon : Image, IIcon
    {
        protected Icon(By byLocator) : base(byLocator)
        {
        }

        public new string Src => GetAttributeWithInvoker("src");

        public new string Alt => GetAttributeWithInvoker("alt");

        public new string Height => GetAttributeWithInvoker("height");

        public new string Width => GetAttributeWithInvoker("width");

        private string GetAttributeWithInvoker(string attribute)
        {
            return Invoker.DoActionWithResult($"Get icon {attribute} for Element " + this,
                () => FindImmediately(() => WebElement.GetAttribute(attribute), ""));
        }

        public new IconAssert AssertThat() => Is();        

        public new IconAssert Is() => new IconAssert(this);
    }
}
