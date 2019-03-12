using System;
using System.Linq;
using JDI.Light.Elements.Base;
using JDI.Light.Exceptions;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;
using static JDI.Light.Elements.Common.Selector;
namespace JDI.Light.Elements.Common
{
    public class DropDown : Selector, IDropDown
    {
        public DropDown(By byLocator) : base(byLocator)
        {
        }
        
        public bool Value { get; set; }
        
        private Func<DropDown, string> _getSelected = (dropDown) => dropDown.Text;
        
        public void Select(string value)
        {
            Click();
            SelectItem(value, this);
        }

        public void Select(int index)
        {
            Click();
            SelectItem(index, this);
        }

        public string GetSelected()
        {
            return Invoker.DoActionWithResult("Get DropDown value", () => _getSelected.Invoke(this));
        }
    }
}