using JDI.Light.Interfaces.Common;
using NUnit.Framework;
using OpenQA.Selenium;

namespace JDI.Light.Tests.Tests.Common
{
    public class UIElementTests : TestBase
    {
        [Test]
        public void GenericGetTest()
        {
            const string text = "some dummy text";
            TestSite.ContactFormPage.Open();
            TestSite.ContactFormPage.CheckOpened();
            var descField = TestSite.ContactFormPage.ContactForm.Get<ITextArea>(By.CssSelector("textarea#description"));
            descField.Input(text);
            Assert.AreEqual(text, TestSite.ContactFormPage.ContactForm.DescriptionField.Text);
        }
    }
}