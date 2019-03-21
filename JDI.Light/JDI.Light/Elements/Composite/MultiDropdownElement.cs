using JDI.Light.Elements.Base;
using OpenQA.Selenium;
using JDI.Light.Attributes;

namespace JDI.Light.Elements.Composite
{
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

        public bool OptionIsEnabled => GetAttribute("class") != "disabled";        

        public new string Text => _label.Text;

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
