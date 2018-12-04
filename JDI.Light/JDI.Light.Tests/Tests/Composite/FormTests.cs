using System.Collections.Generic;
using JDI.Light.Tests.Entities;
using JDI.Light.Tests.Enums;
using JDI.Light.Tests.Tests.Complex;
using JDI.Light.Tests.UIObjects;
using JDI.Light.Tests.UIObjects.Sections;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Composite
{
    public class FormTests
    {
        private readonly ContactForm _contactForm = TestSite.ContactFormPage.ContactForm;

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
        public void FillFormTest()
        {
            _contactForm.Fill(Contact.DEFAULT_CONTACT);
            IList<string> filledFields = _contactForm.GetFormValue();
            JDI.Assert.CollectionEquals(filledFields, Contact.DEFAULT_CONTACT.ToList());
        }

        [Test]
        public void SubmitTest()
        {
            _contactForm.Submit(Contact.DEFAULT_CONTACT);
            CommonActionsData.CheckResult(Contact.DEFAULT_CONTACT.ToString());
        }

        [Test]
        public void SubmitSpecButtonStringTest()
        {
            _contactForm.Submit(Contact.DEFAULT_CONTACT, "submit");
            CommonActionsData.CheckResult(Contact.DEFAULT_CONTACT.ToString());
        }

        [Test]
        public void SubmitSpecButtonEnumTest()
        {
            _contactForm.Submit(Contact.DEFAULT_CONTACT, Buttons.SUBMIT);
            CommonActionsData.CheckResult(Contact.DEFAULT_CONTACT.ToString());
        }

        [Test]
        public void SubmitStringTest()
        {
            _contactForm.Submit(Contact.DEFAULT_CONTACT.FirstName);
            var s = string.Format("Summary: 3\r\nName: {0}",
                Contact.DEFAULT_CONTACT.FirstName);
            CommonActionsData.CheckResult(s);
        }

        [Test]
        public void VerifyTest()
        {
            _contactForm.Fill(Contact.DEFAULT_CONTACT);
            JDI.Assert.IsTrue(_contactForm.Verify(Contact.DEFAULT_CONTACT).Count == 0);
        }

        [Test]
        public void CheckTest()
        {
            _contactForm.Fill(Contact.DEFAULT_CONTACT);
            _contactForm.Check(Contact.DEFAULT_CONTACT);
        }
    }
}