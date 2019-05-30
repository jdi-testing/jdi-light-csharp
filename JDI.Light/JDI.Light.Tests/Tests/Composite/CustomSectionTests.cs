using JDI.Light.Elements.Base;
using JDI.Light.Tests.UIObjects.Sections;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Composite
{
    public class CustomSectionTests : TestBase
    {
        public static Contact ContactSection => TestSite.ContactFormPage.ContactSection;

        [SetUp]
        public void SetUp()
        {
            Jdi.Logger.Info("Navigating to Contact page.");
            TestSite.ContactFormPage.Open();
            TestSite.ContactFormPage.CheckTitle();
            Jdi.Logger.Info("Setup method finished");
            Jdi.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [TestCaseSource(nameof(_contactSectionCases))]
        public void CustomContactSectionTest(UIElement htmlElementToCheck, string expectedLocator, Contact expectedParent, string expectedName)
        {
            ContactSection.CheckInitializedElement(htmlElementToCheck, expectedLocator, expectedParent, expectedName);
        }

        [Test]
        public void CustomSectionFirstRollerTest()
        {
            ContactSection.CheckInitializedElement(ContactSection.FirstRoller, "By.XPath: .//a[@class='ui-slider-handle ui-state-default ui-corner-all' and position()=1]", ContactSection, "FirstRoller");
        }

        [Test]
        public void CustomSectionLastNameTest()
        {
            ContactSection.CheckInitializedElement(ContactSection.LastNameField, "By.CssSelector: input#last-name", ContactSection, "Last Name");
        }

        [Test]
        public void CustomSectionNameFieldTest()
        {
            ContactSection.CheckInitializedElement(ContactSection.NameField, "By.CssSelector: input#name", ContactSection, "First Name");
        }

        [Test]
        public void CustomSectionSecondRollerTest()
        {
            ContactSection.CheckInitializedElement(ContactSection.SecondRoller, "By.XPath: .//a[@class='ui-slider-handle ui-state-default ui-corner-all' and position()=2]", ContactSection, "SecondRoller");
        }

        [Test]
        public void CustomSectionSubmitButtonTest()
        {
            ContactSection.CheckInitializedElement(ContactSection.SubmitButton, "By.XPath: //button[@type='submit' and contains(., 'Submit')]", ContactSection, "SubmitButton");
        }

        static object[] _contactSectionCases =
        {
            new object[] { ContactSection.DescriptionField, "By.CssSelector: textarea#description", ContactSection, "Description" },
            new object[] { ContactSection.FirstRoller, "By.XPath: .//a[@class='ui-slider-handle ui-state-default ui-corner-all' and position()=1]", ContactSection, "FirstRoller" },
            new object[] { ContactSection.LastNameField, "By.CssSelector: input#last-name", ContactSection, "Last Name" },
            new object[] { ContactSection.NameField, "By.CssSelector: input#name", ContactSection, "First Name" },
            new object[] { ContactSection.SecondRoller, "By.XPath: .//a[@class='ui-slider-handle ui-state-default ui-corner-all' and position()=2]", ContactSection, "SecondRoller" },
            new object[] { ContactSection.SubmitButton, "By.XPath: //button[@type='submit' and contains(., 'Submit')]", ContactSection, "SubmitButton" }
        };
    }
}
