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
        private ContactForm _contactForm => TestSite.ContactFormPage.ContactForm;

        [SetUp]
        public void SetUp()
        {
            Jdi.Logger.Info("Navigating to Contact page.");
            TestSite.ContactFormPage.Open();
            TestSite.ContactFormPage.CheckTitle();
            TestSite.ContactFormPage.IsOpened();
            Jdi.Logger.Info("Setup method finished");
            Jdi.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void FillFormTest()
        {
            _contactForm.Fill(Contact.DEFAULT_CONTACT);
            IList<string> filledFields = _contactForm.GetFormValue();
            Jdi.Assert.CollectionEquals(filledFields, Contact.DEFAULT_CONTACT.ToList());
        }

        [Test]
        public void SubmitTest()
        {
            _contactForm.Submit(Contact.DEFAULT_CONTACT);
            Jdi.Assert.Contains(TestSite.ContactFormPage.Result.Value, Contact.DEFAULT_CONTACT.ToString());
            IList<string> filledFields = _contactForm.GetFormValue();
            Jdi.Assert.CollectionEquals(filledFields, Contact.DEFAULT_CONTACT.ToList());
        }

        [Test]
        public void SubmitSpecButtonStringTest()
        {
            _contactForm.Submit(Contact.DEFAULT_CONTACT, "submit");
            Jdi.Assert.Contains(TestSite.ContactFormPage.Result.Value, Contact.DEFAULT_CONTACT.ToString());
            IList<string> filledFields = _contactForm.GetFormValue();
            Jdi.Assert.CollectionEquals(filledFields, Contact.DEFAULT_CONTACT.ToList());
        }

        [Test]
        public void SubmitSpecButtonEnumTest()
        {
            _contactForm.Submit(Contact.DEFAULT_CONTACT, Buttons.SUBMIT);
            Jdi.Assert.Contains(TestSite.ContactFormPage.Result.Value, Contact.DEFAULT_CONTACT.ToString());
            IList<string> filledFields = _contactForm.GetFormValue();
            Jdi.Assert.CollectionEquals(filledFields, Contact.DEFAULT_CONTACT.ToList());
        }
        
        [Test]
        public void VerifyTest()
        {
            _contactForm.Fill(Contact.DEFAULT_CONTACT);
            Jdi.Assert.IsTrue(_contactForm.Verify(Contact.DEFAULT_CONTACT).Count == 0);
        }

        [Test]
        public void CheckTest()
        {
            _contactForm.Fill(Contact.DEFAULT_CONTACT);
            _contactForm.Check(Contact.DEFAULT_CONTACT);
        }
    }
}