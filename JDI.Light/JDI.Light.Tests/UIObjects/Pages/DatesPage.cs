using JDI.Light.Attributes;
using JDI.Light.Elements.Common;
using JDI.Light.Elements.Composite;
using JDI.Light.Interfaces.Common;
using JDI.Light.Tests.UIObjects.Forms;

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