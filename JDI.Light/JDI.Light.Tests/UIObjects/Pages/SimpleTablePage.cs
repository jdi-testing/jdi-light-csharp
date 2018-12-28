using JDI.Light.Attributes;
using JDI.Light.Elements.Composite;
using JDI.Light.Tests.UIObjects.Sections;

namespace JDI.Light.Tests.UIObjects.Pages
{
    public class SimpleTablePage : WebPage
    {
        public JdiPaginator Paginator;

        [FindBy(Id = "table")]
        public Table Table;
    }
}