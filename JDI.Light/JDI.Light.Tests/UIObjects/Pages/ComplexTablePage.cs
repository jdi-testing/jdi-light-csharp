using JDI.Light.Attributes;
using JDI.Light.Elements.Composite;

namespace JDI.Light.Tests.UIObjects.Pages
{
    public class ComplexTablePage : WebPage
    {
        [FindBy(XPath = "//table[@id='complex-table']/tbody/tr/td/div/table")]
        public Table Table;
    }
}