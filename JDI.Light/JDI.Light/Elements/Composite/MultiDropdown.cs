using JDI.Light.Elements.Base;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDI.Light.Elements.Composite
{
    public class MultiDropdown : UIElement
    {
        public MultiDropdown(By locator): base (locator)
        {
        }

        public void Expand()
        {
            this.Click();
        }

    }
}
