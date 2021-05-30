using System.Collections.Generic;
using JDI.Light.Attributes;
using JDI.Light.Elements.Common;
using JDI.Light.Elements.Composite;
using JDI.Light.Interfaces.Common;
using JDI.Light.Tests.Entities;

namespace JDI.Light.Tests.UIObjects.Forms
{
    public class ContactForm : Form<Contact>
    {
        [FindBy(Css = "textarea#description")]
        [Name("Description")]
        public ITextArea DescriptionField;

        [FindBy(XPath = "//a[@class='ui-slider-handle ui-state-default ui-corner-all' and position()=1]")]
        private Link FirstRoller;

        [FindBy(Css = "input#last-name")]
        [Name("LastName")]
        public ITextField LastNameField;

        [FindBy(Css = "input#first-name")]
        [Name("FirstName")]
        public ITextField NameField;

        [FindBy(XPath = "//a[@class='ui-slider-handle ui-state-default ui-corner-all' and position()=2]")]
        private Link SecondRoller;

        [FindBy(XPath = "//button[@type='submit' and contains(., 'Submit')]")]
        public IButton SubmitButton;

        public List<string> GetFormValue()
        {
            var fields = new List<string> {NameField.Value, LastNameField.Value, DescriptionField.Value};
            return fields;
        }

        public void FillForm(Contact contact)
        {
            NameField.Input(contact.FirstName);
            LastNameField.Input(contact.LastName);
            DescriptionField.Input(contact.Description);
        }
    }
}