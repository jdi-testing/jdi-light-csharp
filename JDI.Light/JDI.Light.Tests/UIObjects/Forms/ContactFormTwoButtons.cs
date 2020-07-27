using JDI.Light.Attributes;
using JDI.Light.Elements.Common;
using JDI.Light.Elements.Composite;
using JDI.Light.Interfaces.Common;
using JDI.Light.Tests.Entities;

namespace JDI.Light.Tests.UIObjects.Forms
{
    public class ContactFormTwoButtons : Form<Contact>
    {
        [FindBy(Id = "description")]
        public TextArea Description;

        [FindBy(XPath = ".//a[@class='ui-slider-handle ui-state-default ui-corner-all' and position()=1]")]
        private Link FirstRoller;

        [FindBy(Id = "last-name")]
        public TextField LastName;

        [FindBy(Id = "name")]
        private new TextField Name;

        [FindBy(XPath = ".//a[@class='ui-slider-handle ui-state-default ui-corner-all' and position()=2]")]
        private Link SecondRoller;

        [FindBy(XPath = "//*[text()='Submit']")]
        private new IButton Submit;
    }
}
