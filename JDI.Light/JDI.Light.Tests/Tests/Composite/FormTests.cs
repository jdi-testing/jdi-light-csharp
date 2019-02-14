using System.Collections.Generic;
using JDI.Light.Tests.UIObjects.Forms;
using NUnit.Framework;
using OpenQA.Selenium;
using static JDI.Light.Tests.Entities.Contact;

namespace JDI.Light.Tests.Tests.Composite
{
    [TestFixture]
    public class FormTests : TestBase
    {
        private ContactForm ContactForm => TestSite.ContactFormPage.ContactForm;

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
        public void FillFormTest()
        {
            ContactForm.Fill(DefaultContact);
            var filledFields = ContactForm.GetFormValue();
            Jdi.Assert.CollectionEquals(filledFields, DefaultContact.ToList());
        }

        [Test]
        public void SubmitTest()
        {
            ContactForm.Submit(DefaultContact);
            Jdi.Assert.Contains(TestSite.ContactFormPage.Result.Value, DefaultContact.ToString());
            var filledFields = ContactForm.GetFormValue();
            Jdi.Assert.CollectionEquals(filledFields, DefaultContact.ToList());
        }

        [Test]
        public void SubmitSpecButtonTextTest()
        {
            ContactForm.Submit(DefaultContact, "Submit");
            Jdi.Assert.Contains(TestSite.ContactFormPage.Result.Value, DefaultContact.ToString());
            var filledFields = ContactForm.GetFormValue();
            Jdi.Assert.CollectionEquals(filledFields, DefaultContact.ToList());
        }

        [Test]
        public void SubmitSpecButtonLocatorTest()
        {
            ContactForm.Submit(DefaultContact, By.XPath("//button[@type='submit']"));
            Jdi.Assert.Contains(TestSite.ContactFormPage.Result.Value, DefaultContact.ToString());
            var filledFields = ContactForm.GetFormValue();
            Jdi.Assert.CollectionEquals(filledFields, DefaultContact.ToList());
        }
        
        [Test]
        public void VerifyTest()
        {
            ContactForm.Fill(DefaultContact);
            Jdi.Assert.IsTrue(ContactForm.Verify(DefaultContact).Count == 0);
        }

        [Test]
        public void CheckTest()
        {
            ContactForm.Fill(DefaultContact);
            ContactForm.Check(DefaultContact);
        }
    }
}