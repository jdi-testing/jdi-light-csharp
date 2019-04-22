using JDI.Light.Attributes;
using JDI.Light.Elements.Common;
using JDI.Light.Elements.Composite;
using JDI.Light.Interfaces.Common;
using JDI.Light.Tests.UIObjects.Forms;

namespace JDI.Light.Tests.UIObjects.Pages
{
    public class ContactPage : WebPage
    {
        [FindBy(Id = "contact-form")]
        public ContactForm ContactForm;

        [FindBy(Css = "main form")]
        public ContactFormTwoButtons ContactFormTwoButtons;

        [FindBy(XPath = "//*[text()='Submit']")]
        public IButton ContactSubmit;

        [FindBy(Id = "description")]
        public TextArea DescriptionArea;

        [FindBy(Id = "last-name")]
        public ITextField LastNameField;

        [FindBy(Id = "name")]
        public TextField NameField;

        [FindBy(Id = "name")]
        public Input NameInput;

        [FindBy(Css = ".results")]
        public ITextElement Result;
        
        public void FillAndSubmitForm(string firstName, string secondName, string description)
        {
            FillForm(firstName, secondName, description);
            ContactSubmit.Click();
        }
        
        private void FillForm(string firstName, string secondName, string description)
        {
            NameField.Input(firstName);
            LastNameField.Input(secondName);
            DescriptionArea.Input(description);
        }
    }
}