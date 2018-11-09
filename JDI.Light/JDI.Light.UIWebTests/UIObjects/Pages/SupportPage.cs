using JDI.Web.Attributes;
using JDI.Web.Selenium.Elements.Complex.Table.Interfaces;
using JDI.Web.Selenium.Elements.Composite;

namespace JDI.UIWebTests.UIObjects.Pages
{
    public class SupportPage : WebPage
    {
        [FindBy(Css = ".uui-table")] public ITable SupportTable;
    }
}