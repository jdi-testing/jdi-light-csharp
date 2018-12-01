using JDI.Light.Attributes;
using JDI.Light.Elements.Complex.Table.Interfaces;
using JDI.Light.Elements.Composite;

namespace JDI.Light.Tests.UIObjects.Pages
{
    public class SupportPage : WebPage
    {
        [FindBy(Css = ".uui-table")] public ITable SupportTable;
    }
}