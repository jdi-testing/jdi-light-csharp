using JDI.Core.Interfaces.Common;
using JDI.UIWebTests.UIObjects.Sections;
using JDI.Web.Attributes;
using JDI.Web.Selenium.Elements.Common;
using JDI.Web.Selenium.Elements.Composite;

namespace JDI.UIWebTests.UIObjects.Pages
{
    public class DatesPage:WebPage
    {
        [FindBy(Css = "#datepicker input")]
        public DatePicker Datepicker;

        [FindBy(Css = "[data-provides=fileinput]")]
        public IFileInput ImageInput;

        [FindBy(Css = "")]
        public ILabel UploadedFileName;

        [FindBy(Css = "main form")]
        public ContactForm ContactForm;
    }
}
