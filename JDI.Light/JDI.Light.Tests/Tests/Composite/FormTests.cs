using System.Collections.Generic;
using JDI.Light.Tests.Entities;
using JDI.Light.Tests.UIObjects.Forms;
using NUnit.Framework;
using OpenQA.Selenium;

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
            ContactForm.Fill(Contact.DefaultContact);
            IList<string> filledFields = ContactForm.GetFormValue();
            Jdi.Assert.CollectionEquals(filledFields, Contact.DefaultContact.ToList());
        }

        [Test]
        public void SubmitTest()
        {
            ContactForm.Submit(Contact.DefaultContact);
            Jdi.Assert.Contains(TestSite.ContactFormPage.Result.Value, Contact.DefaultContact.ToString());
            IList<string> filledFields = ContactForm.GetFormValue();
            Jdi.Assert.CollectionEquals(filledFields, Contact.DefaultContact.ToList());
        }

        [Test]
        public void SubmitSpecButtonTextTest()
        {
            ContactForm.Submit(Contact.DefaultContact, "Submit");
            Jdi.Assert.Contains(TestSite.ContactFormPage.Result.Value, Contact.DefaultContact.ToString());
            IList<string> filledFields = ContactForm.GetFormValue();
            Jdi.Assert.CollectionEquals(filledFields, Contact.DefaultContact.ToList());
        }

        [Test]
        public void SubmitSpecButtonLocatorTest()
        {
            ContactForm.Submit(Contact.DefaultContact, By.XPath("//button[@type='submit']"));
            Jdi.Assert.Contains(TestSite.ContactFormPage.Result.Value, Contact.DefaultContact.ToString());
            IList<string> filledFields = ContactForm.GetFormValue();
            Jdi.Assert.CollectionEquals(filledFields, Contact.DefaultContact.ToList());
        }
        
        [Test]
        public void VerifyTest()
        {
            ContactForm.Fill(Contact.DefaultContact);
            Jdi.Assert.IsTrue(ContactForm.Verify(Contact.DefaultContact).Count == 0);
        }

        [Test]
        public void CheckTest()
        {
            ContactForm.Fill(Contact.DefaultContact);
            ContactForm.Check(Contact.DefaultContact);
        }
    }
}