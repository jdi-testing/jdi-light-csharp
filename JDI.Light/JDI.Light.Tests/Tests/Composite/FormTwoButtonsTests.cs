using JDI.Light.Tests.Entities;
using NUnit.Framework;
using static JDI.Light.Tests.UIObjects.TestSite;

namespace JDI.Light.Tests.Tests.Composite
{
    public class FormTwoButtonsTests
    {
        [SetUp]
        public void SetUp()
        {
            JDI.Logger.Info("Navigating to Contact page.");
            ContactFormPage.Open();
            ContactFormPage.CheckTitle();
            ContactFormPage.IsOpened();
            JDI.Logger.Info("Setup method finished");
            JDI.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void SubmitSpecButtonStringTest()
        {
            ContactFormPage.ContactFormTwoButtons.Submit(Contact.DEFAULT_CONTACT, "calculate");
            JDI.Assert.Contains(ContactFormPage.Result.Value, "Summary: 3");
        }
    }
}