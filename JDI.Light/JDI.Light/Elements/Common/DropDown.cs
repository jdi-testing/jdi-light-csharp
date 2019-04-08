using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class DropDown : Selector, IDropDown
    {
        public string Expander { get; set; }
        public string Value { get; set; }
        public string List { get; set; }

        public void Setup(string value, string list, string expand)
        {
            if (value != null) Value = value;
            if (list != null) List = list;
            if (expand != null) Expander = expand;
        }

        public DropDown(By byLocator) : base(byLocator)
        {
        }

        public void Expand()
        {
            if (Expander != null)
            {
                FindElement(By.CssSelector(Expander)).Click();
            }
        }
        
        public void Select(string value)
        {
            if (List != null)
            {
                ItemLocator = By.CssSelector(List);
            }
            Select(value, this);
        }

        public void Select(int index)
        {
            if (List != null)
            {
                ItemLocator = By.CssSelector(List);
            }
            Select(index, this);
        }

        public string GetSelected()
        {
            return Value != null ? GetSelected(FindElement(By.CssSelector(Value))) : null;
        }
    }
}