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

        
        public List<MultiDropdownElement> TextElements
        {
            get
            {
                var elems = FindElements(By.XPath(".//li")).ToList();
                var labels = FindElements(By.XPath(".//li//label")).ToList(); //.Select(x=>(Label)x).ToList();
                var chBoxes = FindElements(By.XPath(".//li//input")).ToList(); //.Select(x=>(CheckBox)x).ToList();
                List<MultiDropdownElement> elementList = new List<MultiDropdownElement>();
                for (int i = 0; i < elems.Count; i++)
                {
                    elementList.Add(new MultiDropdownElement(By.XPath(".//li"),labels[i],chBoxes[i], elems[i]));
                }                

                return elementList;
            }
        }

        public void SelectElementByname(string name)
        {
            TextElements.FirstOrDefault(x => x.Text == name).Select();
        }


        public MultiDropdown(By locator) : base(locator)
        {
        }

        public void Expand()
        {
            _header.Click();
        }

    }

    public class MultiDropdownElement : UIElement
    {
        [FindBy(Tag = "label")]
        private IWebElement _label;

        [FindBy(Tag = "input")]
        private IWebElement _checkBox;

        private bool IsSelected
        {
            get
            {
                return (this.GetAttribute("class") == "active");
            }
        }

        public new string Text
        {
            get
            {
                return _label.Text;
            }
        }

        public void Select()
        {
            if (!IsSelected)
            {                
                _checkBox.Click();
            }
        }

        public MultiDropdownElement(By locator, IWebElement label, IWebElement checkBox, IWebElement itself) : base(locator)
        {
            WebElement = itself;
            _label = label;
            _checkBox = checkBox;
        }

    }
}
