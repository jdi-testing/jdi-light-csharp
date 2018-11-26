using JDI.Core.Attributes;
using JDI.Core.Interfaces.Common;
using JDI.Core.Selenium.Elements.Common;
using JDI.Core.Selenium.Elements.Composite;
using JDI.UIWebTests.UIObjects.Sections;

namespace JDI.UIWebTests.UIObjects.Pages
{
    public class DatesPage : WebPage
    {
        [FindBy(Css = "main form")] public ContactForm ContactForm;

        [FindBy(Css = "#datepicker input")] public DatePicker Datepicker;

        [FindBy(Css = "[data-provides=fileinput]")]
        public IFileInput ImageInput;

        [FindBy(Css = "")] public ILabel UploadedFileName;
    }
}