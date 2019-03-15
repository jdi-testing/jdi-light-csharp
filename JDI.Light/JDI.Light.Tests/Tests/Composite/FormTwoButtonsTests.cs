using JDI.Light.Tests.Entities;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Composite
{
    [TestFixture]
    public class FormTwoButtonsTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            Jdi.Logger.Info("Navigating to Contact page.");
            TestSite.ContactFormPage.Open();
            TestSite.ContactFormPage.CheckTitle();
            Jdi.Logger.Info("Setup method finished");
            Jdi.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void SubmitSpecButtonTextTest()
        {
            TestSite.ContactFormPage.ContactFormTwoButtons.Submit(Contact.DefaultContact, "Calculate");
            Jdi.Assert.Contains(TestSite.ContactFormPage.Result.Value, "Summary: 3");
        }
    }
}