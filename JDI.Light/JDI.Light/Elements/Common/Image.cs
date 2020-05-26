using JDI.Light.Asserts;
using JDI.Light.Elements.Base;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class Image : UIElement, IImage
    {
        public Image() : this(null)
        {
        }

        public Image(By byLocator)
            : base(byLocator)
        {
            
        }
        
        public string Src => GetAttributeWithInvoker("src");

        public string Alt => GetAttributeWithInvoker("alt");

        public string Height => GetAttributeWithInvoker("height");

        public string Width => GetAttributeWithInvoker("width");
        
        private string GetAttributeWithInvoker(string attribute)
        {
            return Invoker.DoActionWithResult($"Get image {attribute} for Element " + this,
                () => FindImmediately(() => WebElement.GetAttribute(attribute), ""));
        }

        public new ImageAssert Is() => new ImageAssert(this);

        public new ImageAssert AssertThat() => Is();
    }
}