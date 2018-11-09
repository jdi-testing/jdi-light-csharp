using System.Collections.Generic;
using JDI.Core.Settings;
using JDI.Matchers.NUnit;
using JDI.UIWebTests.Entities;
using JDI.UIWebTests.Enums;
using JDI.UIWebTests.Tests.Complex;
using JDI.UIWebTests.UIObjects;
using JDI.UIWebTests.UIObjects.Sections;
using NUnit.Framework;

namespace JDI.UIWebTests.Tests.Composite
{
    public class FormTests
    {
        private readonly ContactForm _contactForm = TestSite.ContactFormPage.ContactForm;

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
        public void FillFormTest()
        {
            _contactForm.Fill(Contact.DEFAULT_CONTACT);
            IList<string> filledFilds = _contactForm.GetFormValue();
            new Check().CollectionEquals(filledFilds, Contact.DEFAULT_CONTACT.ToList());
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
            new Check().IsTrue(_contactForm.Verify(Contact.DEFAULT_CONTACT).Count == 0);
        }


        [Test]
        public void checkTest()
        {
            _contactForm.Fill(Contact.DEFAULT_CONTACT);
            new Check().HasNoException(() => _contactForm.Check(Contact.DEFAULT_CONTACT));
        }
    }
}