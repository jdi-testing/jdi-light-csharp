using JDI.Light.Elements.Base;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JDI.Light.Attributes;
using JDI.Light.Elements.Common;

namespace JDI.Light.Elements.Composite
{
    public class MultiDropdown : UIElement
    {
        [FindBy(Tag = "button")]
        private Button _header;

        
        public List<CheckBox> TextElements
        {
            get
            {
                var elems = this.FindElements(By.XPath("//input[@type='checkbox']"));
                var res = new List<CheckBox>();
                return res;
            }
        }

        public MultiDropdown(By locator): base (locator)
        {
        }

        public void Expand()
        {
            _header.Click();
        }

    }
}
