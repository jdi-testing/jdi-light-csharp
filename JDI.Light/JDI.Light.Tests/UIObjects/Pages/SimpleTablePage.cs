using JDI.Light.Attributes;
using JDI.Light.Elements.Composite;

namespace JDI.Light.Tests.UIObjects.Pages
{
    public class SimpleTablePage : WebPage
    {
        [FindBy(Id = "simple-table")]
        public Table Table;
    }
}