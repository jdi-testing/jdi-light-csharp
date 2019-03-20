using JDI.Light.Attributes;
using JDI.Light.Elements.Common;
using JDI.Light.Elements.Composite;
using JDI.Light.Interfaces.Common;
using JDI.Light.Tests.UIObjects.Forms;

namespace JDI.Light.Tests.UIObjects
{
    public class Html5Page : WebPage
    {
        [FindBy(Css = "#avatar")]
        public FileInput FileInput { get; set; }

        [FindBy(XPath = "//a[@href='/JDI/images/jdi-logo.jpg']")]
        public Link FileDownload { get; set; }
		
        [FindBy(Css = ".btn-group")]
        public MultiDropdown MultiDropdown { get; set; }

        [FindBy(Css = "#ages")]
        public MultiSelector AgeSelector { get; set; }

        [FindBy(Id = "blue-button")]
        public IButton BlueButton { get; set; }

        [FindBy(Css = "h1")]
        public ILabel JdiLabel { get; set; }
    }
}