using JDI.Light.Asserts;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class ComboBox : DataList
    {
        public ComboBox(By byLocator) : base(byLocator)
        {            
        }

        public new ComboBoxAssert Is() => new ComboBoxAssert(this);
        public new ComboBoxAssert AssertThat() => Is();
    }
}