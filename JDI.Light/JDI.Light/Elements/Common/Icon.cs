using JDI.Light.Asserts;
using JDI.Light.Elements.Base;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDI.Light.Elements.Common
{
    public class Icon : Image, IIcon
    {
        protected Icon(By byLocator) : base(byLocator)
        {
        }

        public string Src => GetAttributeWithInvoker("src");

        public string Alt => GetAttributeWithInvoker("alt");

        public string Height => GetAttributeWithInvoker("height");

        public string Width => GetAttributeWithInvoker("width");

        private string GetAttributeWithInvoker(string attribute)
        {
            return Invoker.DoActionWithResult($"Get icon {attribute} for Element " + this,
                () => FindImmediately(() => WebElement.GetAttribute(attribute), ""));
        }

        public new IconAssert AssertThat() => Is();        

        public new IconAssert Is() => new IconAssert(this);
    }
}
