using JDI.Core.Attributes;
using JDI.Core.Selenium.Elements.Complex.Table.Interfaces;
using JDI.Core.Selenium.Elements.Composite;

namespace JDI.Light.Tests.UIObjects.Pages
{
    public class SupportPage : WebPage
    {
        [FindBy(Css = ".uui-table")] public ITable SupportTable;
    }
}