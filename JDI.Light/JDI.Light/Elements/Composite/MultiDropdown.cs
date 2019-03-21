using JDI.Light.Elements.Base;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
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
                var elementList = new List<MultiDropdownElement>();
                for (int i = 0; i < elems.Count; i++)
                {
                    elementList.Add(new MultiDropdownElement(By.XPath(".//li"),labels[i],chBoxes[i], elems[i]));
                }                
                return elementList;
            }
        }

        public bool OptionIsEnabled(string name)
        {            
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
            return (Options.Where(x=>x.IsSelected).All(x => options.Contains(x.Text)) && options.All(x => Options.FirstOrDefault(y => y.Text == x).IsSelected));
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

        public MultiDropdown(By locator, Button header) : base(locator)
        {
            _header = header;
        }

        public void Expand()
        {
            if (!GetAttribute("class").Contains("open"))
            {
                _header.Click();
            }
        }

        public void Close()
        {
            Expand();
        }        
    }    
}
