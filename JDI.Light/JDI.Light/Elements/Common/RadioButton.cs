using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class RadioButton : Selector, IRadioButton
    {
        public By RadioLocator { get; set; }

        protected RadioButton(By byLocator) : base(byLocator)
        {
        }

        public bool Value { get; set; }

        public void Select(string value)
        {
            ItemLocator = RadioLocator;
            Select(value, this);
        }

        public void Select(int index)
        {
            ItemLocator = RadioLocator;
            Select(index, this);
        }

        public string GetSelected()
        {
            var els = WebElement.FindElements(RadioLocator);
            foreach (var element in els)
            {
                if (element?.GetAttribute("checked") != null)
                {
                    return element.GetAttribute("id");
                }
            }
            return null;
        }
    }
}