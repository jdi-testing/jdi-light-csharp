using JDI.Light.Attributes;
using JDI.Light.Elements.Common;
using JDI.Light.Elements.Composite;
using JDI.Light.Tests.UIObjects.Forms;

namespace JDI.Light.Tests.UIObjects
{
    public class Html5Page : WebPage
    {
        [FindBy(Css = "#avatar")]
        public FileInput FileInput;

        [FindBy(XPath = "//a[@href='/JDI/images/jdi-logo.jpg']")]
        public Link FileDownload;

        [FindBy(Css = ".btn-group")]
        public MultiDropdown MultiDropdown;
    }
}