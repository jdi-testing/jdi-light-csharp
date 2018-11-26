using JDI.Light.Attributes;
using JDI.Light.Selenium.Elements.Complex.Table.Interfaces;
using JDI.Light.Selenium.Elements.Composite;

namespace JDI.Light.Tests.UIObjects.Pages
{
    public class SupportPage : WebPage
    {
        [FindBy(Css = ".uui-table")] public ITable SupportTable;
    }
}