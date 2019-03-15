using JDI.Light.Attributes;
using JDI.Light.Elements.Common;
using JDI.Light.Elements.Complex.Table;
using JDI.Light.Elements.Composite;

namespace JDI.Light.Tests.UIObjects.Pages
{
    public class PerformancePage : WebPage
    {
        [FindBy(Css = "#users-table")]
        public Table UsersTable;

        //[FindBy(Css = "#user-names")]
        //public Dropdown UserNames;

        [FindBy(Css = "#textarea-performance")]
        public TextArea TextareaPerformance;
    }
}
