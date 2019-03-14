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
        
        public List<MultiDropdownElement> Options
        {
            get
            {
                var elems = FindElements(By.XPath(".//li")).ToList();
                var labels = FindElements(By.XPath(".//li//label")).ToList();
                var chBoxes = FindElements(By.XPath(".//li//input")).ToList();
                List<MultiDropdownElement> elementList = new List<MultiDropdownElement>();
                for (int i = 0; i < elems.Count; i++)
                {
                    elementList.Add(new MultiDropdownElement(By.XPath(".//li"),labels[i],chBoxes[i], elems[i]));
                }                
                return elementList;
            }
        }

        public bool OptionIsEnabled(string name)
        {
            var aa = Options;
            return Options.Any(x => x.OptionIsEnabled && x.Text == name);
        }

        public void SelectOptionByname(string name)
        {
            Expand();
            Options.FirstOrDefault(x => x.Text == name).Select();
            Close();
        }

        public List<string> GetSelectedOptions()
        {            
            return Options.Where(x => x.Selected).Select(x=>x.Text).ToList();
        }

        public bool OptionsAreSelected(List<string> options)
        {
            var a = Options.Where(x => x.IsSelected).All(x => options.Contains(x.Text));
            var b = options.All(x => Options.FirstOrDefault(y => y.Text == x) != null);
            var c = a && b;
            return (Options.Where(x=>x.IsSelected).All(x => options.Contains(x.Text)) && options.All(x => Options.Any(y => y.Text == x)));
        }

        public void SelectOptions(List<string> options)
        {
            Expand();
            foreach(var option in options)
            {
                Options.FirstOrDefault(x => x.Text == option).Select();
            }
            JsExecutor.ExecuteScript("arguments[0].scrollIntoView();", WebElement);
            Close();
        }

        public bool OptionExists(string option)
        {
            return Options.Any(x => x.Text == option);
        }

        public MultiDropdown(By locator) : base(locator)
        {
        }

        public void Expand()
        {
            if(!GetAttribute("class").Contains("open"))
                _header.Click();
        }

        public void Close()
        {
            Expand();
        }
    }

    public class MultiDropdownElement : UIElement
    {
        [FindBy(Tag = "label")]
        private IWebElement _label;

        [FindBy(Tag = "input")]
        private IWebElement _checkBox;

        public bool IsSelected
        {
            get
            {
                return (GetAttribute("class") == "active");
            }
        }

        public bool OptionIsEnabled
        {
            get
            {
                return (GetAttribute("class") != "disabled");
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
                JsExecutor.ExecuteScript("arguments[0].scrollIntoView();", _label);
                _label.Click();
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
