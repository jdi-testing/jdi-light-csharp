using JDI.Light.Elements.Base;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class MyCheckBox : UIElement, ICheckBox
    {
        protected MyCheckBox(By byLocator) : base(byLocator)
        {
        }

        bool IGetValue<bool>.Value => true;

        bool ISetValue<bool>.Value { get; set; }
 

        public void Check()
        {
            throw new System.NotImplementedException();
        }

        public void Uncheck()
        {
            throw new System.NotImplementedException();
        }

        public bool IsChecked => false;
    }
}
