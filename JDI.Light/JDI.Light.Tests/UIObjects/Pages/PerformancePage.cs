using JDI.Light.Attributes;
using JDI.Light.Elements.Complex.Table;
using JDI.Light.Elements.Composite;
using JDI.Light.Interfaces.Common;

namespace JDI.Light.Tests.UIObjects.Pages
{
    public class PerformancePage : WebPage
    {
        [FindBy(Css = "#users-table")]
        public Table UsersTable { get; set; }

        public IDropDown UserNames { get; set; }
    }
}
