using JDI.Light.Attributes;
using JDI.Light.Elements.Base;
using JDI.Light.Elements.Common;
using JDI.Light.Elements.Composite;

namespace JDI.Light.Tests.UIObjects.Sections
{
    public class Contact : Section
    {
        [FindBy(Css = "textarea#description")]
        [Name("Description")]
        public TextArea DescriptionField;

        [FindBy(XPath = ".//a[@class='ui-slider-handle ui-state-default ui-corner-all' and position()=1]")]
        public Link FirstRoller;

        [FindBy(Css = "input#last-name")]
        [Name("LastName")]
        public TextField LastNameField;

        [FindBy(Css = "input#name")]
        [Name("FirstName")]
        public TextField NameField;

        [FindBy(XPath = ".//a[@class='ui-slider-handle ui-state-default ui-corner-all' and position()=2]")]
        public Link SecondRoller;

        [FindBy(XPath = "//button[@type='submit' and contains(., 'Submit')]")]
        public UIElement SubmitButton;
    }
}
