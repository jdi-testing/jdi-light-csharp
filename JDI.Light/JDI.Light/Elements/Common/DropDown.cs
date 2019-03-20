using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class DropDown : Selector, IDropDown
    {
        public DropDown(By byLocator) : base(byLocator)
        {
        }
        
        public bool Value { get; set; }
        
        public void Select(string value)
        {
            Click();
            Select(value, this);
        }

        public void Select(int index)
        {
            Click();
            Select(index, this);
        }

        public string GetSelected()
        {
            return GetSelected(this);
        }
    }
}