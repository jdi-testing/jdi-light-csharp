using JDI.Light.Attributes;
using JDI.Light.Interfaces.Common;
using JDI.Light.Selenium.Elements.Common;
using JDI.Light.Selenium.Elements.Composite;
using JDI.Light.Tests.UIObjects.Sections;

namespace JDI.Light.Tests.UIObjects.Pages
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