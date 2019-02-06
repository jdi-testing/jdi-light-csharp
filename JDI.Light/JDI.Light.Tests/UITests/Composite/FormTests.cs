using System.Collections.Generic;
using JDI.Light.Tests.Entities;
using JDI.Light.Tests.Enums;
using JDI.Light.Tests.UIObjects.Forms;
using NUnit.Framework;

namespace JDI.Light.Tests.UITests.Composite
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
            ContactForm.Fill(Contact.DEFAULT_CONTACT);
            IList<string> filledFields = ContactForm.GetFormValue();
            Jdi.Assert.CollectionEquals(filledFields, Contact.DEFAULT_CONTACT.ToList());
        }

        [Test]
        public void SubmitTest()
        {
            ContactForm.Submit(Contact.DEFAULT_CONTACT);
            Jdi.Assert.Contains(TestSite.ContactFormPage.Result.Value, Contact.DEFAULT_CONTACT.ToString());
            IList<string> filledFields = ContactForm.GetFormValue();
            Jdi.Assert.CollectionEquals(filledFields, Contact.DEFAULT_CONTACT.ToList());
        }

        [Test]
        public void SubmitSpecButtonStringTest()
        {
            ContactForm.Submit(Contact.DEFAULT_CONTACT, "submit");
            Jdi.Assert.Contains(TestSite.ContactFormPage.Result.Value, Contact.DEFAULT_CONTACT.ToString());
            IList<string> filledFields = ContactForm.GetFormValue();
            Jdi.Assert.CollectionEquals(filledFields, Contact.DEFAULT_CONTACT.ToList());
        }

        [Test]
        public void SubmitSpecButtonEnumTest()
        {
            ContactForm.Submit(Contact.DEFAULT_CONTACT, Buttons.SUBMIT);
            Jdi.Assert.Contains(TestSite.ContactFormPage.Result.Value, Contact.DEFAULT_CONTACT.ToString());
            IList<string> filledFields = ContactForm.GetFormValue();
            Jdi.Assert.CollectionEquals(filledFields, Contact.DEFAULT_CONTACT.ToList());
        }
        
        [Test]
        public void VerifyTest()
        {
            ContactForm.Fill(Contact.DEFAULT_CONTACT);
            Jdi.Assert.IsTrue(ContactForm.Verify(Contact.DEFAULT_CONTACT).Count == 0);
        }

        [Test]
        public void CheckTest()
        {
            ContactForm.Fill(Contact.DEFAULT_CONTACT);
            ContactForm.Check(Contact.DEFAULT_CONTACT);
        }
    }
}