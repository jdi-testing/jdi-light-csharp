using JDI.Light.Elements.Base;
using OpenQA.Selenium;
using JDI.Light.Elements.Common;
using JDI.Light.Interfaces.Composite;

namespace JDI.Light.Elements.Composite
{
    public class MultiDropdownElement : UIElement, IMultiDropdownElement
    {
        public By LabelLocator { get; set; }
        public By CheckboxLocator { get; set; }

        public Label Label => Get<Label>(LabelLocator);
        public CheckBox CheckBox => Get<CheckBox>(CheckboxLocator);
        public bool IsSelected => GetAttribute("class") == "active";
        public bool OptionIsEnabled => GetAttribute("class") != "disabled";        

        public new string Text => Label.Text;

        public void Select()
        {
            if (!IsSelected)
            {
                CheckEnabled(true);
                JsExecutor.ExecuteScript("arguments[0].scrollIntoView();", Label.WebElement);
                Label.Click();
            }
        }

        public MultiDropdownElement(By locator) : base(locator)
        {
        }
    }
}
