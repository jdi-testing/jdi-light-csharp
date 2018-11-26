using JDI.Core.Settings;
using JDI.UIWebTests.Entities;
using JDI.UIWebTests.Tests.Complex;
using JDI.UIWebTests.UIObjects;
using JDI.UIWebTests.UIObjects.Sections;
using NUnit.Framework;

namespace JDI.UIWebTests.Tests.Composite
{
    public class FormTwoButtonsTests
    {
        private readonly ContactFormTwoButtons _contactForm = TestSite.ContactFormPage.ContactFormTwoButtons;

        [SetUp]
        public void SetUp()
        {
            JDISettings.Logger.Info("Navigating to Contact page.");
            TestSite.ContactFormPage.Open();
            TestSite.ContactFormPage.CheckTitle();
            TestSite.ContactFormPage.IsOpened();
            JDISettings.Logger.Info("Setup method finished");
            JDISettings.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void SubmitSpecButtonStringTest()
        {
            _contactForm.Submit(Contact.DEFAULT_CONTACT, "calculate");
            CommonActionsData.CheckResult("Summary: 3");
        }
    }
}