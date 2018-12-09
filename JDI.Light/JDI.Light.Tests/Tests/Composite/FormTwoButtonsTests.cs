using JDI.Light.Tests.Entities;
using JDI.Light.Tests.UIObjects;
using JDI.Light.Tests.UIObjects.Sections;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Composite
{
    public class FormTwoButtonsTests
    {
        private readonly ContactFormTwoButtons _contactForm = TestSite.ContactFormPage.ContactFormTwoButtons;

        [SetUp]
        public void SetUp()
        {
            JDI.Logger.Info("Navigating to Contact page.");
            TestSite.ContactFormPage.Open();
            TestSite.ContactFormPage.CheckTitle();
            TestSite.ContactFormPage.IsOpened();
            JDI.Logger.Info("Setup method finished");
            JDI.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void SubmitSpecButtonStringTest()
        {
            _contactForm.Submit(Contact.DEFAULT_CONTACT, "calculate");
            JDI.Assert.Contains(TestSite.ContactFormPage.Result.Value, "Summary: 3");
        }
    }
}