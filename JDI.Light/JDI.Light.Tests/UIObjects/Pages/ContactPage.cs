using JDI.Core.Attributes;
using JDI.Core.Interfaces.Common;
using JDI.Core.Selenium.Elements.Common;
using JDI.Core.Selenium.Elements.Composite;
using JDI.UIWebTests.UIObjects.Sections;

namespace JDI.UIWebTests.UIObjects.Pages
{
    public class ContactPage : WebPage
    {
        [FindBy(Css = "main form")] public ContactForm ContactForm;

        [FindBy(Css = "main form")] public ContactFormTwoButtons ContactFormTwoButtons;

        [FindBy(XPath = "//*[text()='Submit']")]
        public IButton ContactSubmit;

        [FindBy(Id = "Description")] public TextArea DescriptionField;

        [FindBy(Id = "LastName")] public ITextField LastNameField;

        [FindBy(Css = ".epam-logo img")] public IImage LogoImage;

        [FindBy(Id = "Name")] public TextField NameField;

        [FindBy(Css = ".results")] public IText Result;

        public void FillFormWithoutSubmitting(string firstName, string secondName, string description)
        {
            FillForm(firstName, secondName, description);
        }

        public void FillAndSubmitForm(string firstName, string secondName, string description)
        {
            FillForm(firstName, secondName, description);
            ContactSubmit.Click();
        }
        
        private void FillForm(string firstName, string secondName, string description)
        {
            NameField.NewInput(firstName);
            LastNameField.NewInput(secondName);
            DescriptionField.NewInput(description);
        }
    }
}