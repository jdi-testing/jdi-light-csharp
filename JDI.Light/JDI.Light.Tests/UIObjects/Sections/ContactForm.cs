using System.Collections.Generic;
using JDI.Light.Attributes;
using JDI.Light.Elements.Common;
using JDI.Light.Elements.Composite;
using JDI.Light.Interfaces.Common;
using JDI.Light.Tests.Entities;
using OpenQA.Selenium;

namespace JDI.Light.Tests.UIObjects.Sections
{
    public class ContactForm : Form<Contact>
    {
        [FindBy(Css = "textarea#Description")] [Name("Description")]
        public ITextArea DescriptionField;

        [FindBy(XPath = ".//a[@class='ui-slider-handle ui-state-default ui-corner-all' and position()=1]")]
        public Link FirstRoller;

        private IJavaScriptExecutor javaScriptExecutor;

        [FindBy(Css = "input#LastName")] [Name("LastName")]
        public ITextField LastNameField;

        [FindBy(Css = "input#Name")] [Name("FirstName")]
        public ITextField NameField;

        [FindBy(XPath = ".//a[@class='ui-slider-handle ui-state-default ui-corner-all' and position()=2]")]
        public Link SecondRoller;

        [FindBy(XPath = "//button[@type='submit' and contains(., 'Submit')]")]
        public IButton SubmitButton;

        public List<string> GetFormValue()
        {
            var fields = new List<string>();
            fields.Add(NameField.Value);
            fields.Add(LastNameField.Value);
            fields.Add(DescriptionField.Value);
            return fields;
        }

        public void FillForm(Contact contact)
        {
            NameField.NewInput(contact.FirstName);
            LastNameField.NewInput(contact.LastName);
            DescriptionField.NewInput(contact.Description);
        }
    }
}