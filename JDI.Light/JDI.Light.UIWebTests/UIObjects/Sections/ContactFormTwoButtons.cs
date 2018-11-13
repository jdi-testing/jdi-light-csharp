using JDI.Core.Attributes;
using JDI.Core.Interfaces.Common;
using JDI.Core.Selenium.Elements.Common;
using JDI.Core.Selenium.Elements.Composite;
using JDI.UIWebTests.Entities;

namespace JDI.UIWebTests.UIObjects.Sections
{
    public class ContactFormTwoButtons : Form<Contact>
    {
        [FindBy(Id = "Description")] public TextArea Description;

        [FindBy(XPath = ".//a[@class='ui-slider-handle ui-state-default ui-corner-all' and position()=1]")]
        public Link FirstRoller;

        [FindBy(Id = "LastName")] public TextField LastName;

        [FindBy(Id = "Name")] public new TextField Name;

        [FindBy(XPath = ".//a[@class='ui-slider-handle ui-state-default ui-corner-all' and position()=2]")]
        public Link SecondRoller;

        [FindBy(XPath = "//*[text()='Submit']")]
        public new IButton Submit;
    }
}