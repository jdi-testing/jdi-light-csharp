using System;
using JDI.Core.Settings;
using JDI.Matchers.NUnit;
using JDI.UIWebTests.Entities;
using JDI.UIWebTests.UIObjects;
using NUnit.Framework;

namespace JDI.UIWebTests.Tests.Common
{
    public class TextFieldsTests
    {
        private readonly string DEFAULT_TEXT = User.DefaultUser.Login;
        private const string CONTAINS = "ame";
        private const string REGEX = ".am.";
        private const string TO_ADD_TEXT = "text123!@#$%^&*()";
        private const string EXPECTED_TEXT = "text123!@#$%^&*()";
        private const string ELEMENT_TYPE = "TextField";

        [SetUp]
        public void SetUp()
        {
            JDISettings.Logger.Info("Navigating to Metals and Colors page.");
            TestSite.ContactFormPage.Open();
            TestSite.ContactFormPage.CheckTitle();
            TestSite.ContactFormPage.IsOpened();
            TestSite.ContactFormPage.FillAndSubmitForm(DEFAULT_TEXT, DEFAULT_TEXT + (new Random()).Next(), DEFAULT_TEXT + (new Random()).Next());
            JDISettings.Logger.Info("Setup method finished");
            JDISettings.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }


        [Test]
        public void InputTest()
        {
            TestSite.ContactFormPage.NameField.Input(TO_ADD_TEXT);
            new Check().AreEquals(TestSite.ContactFormPage.NameField.GetText, DEFAULT_TEXT + TO_ADD_TEXT);            
        }


        [Test]
        public void SendKeyTest()
        {
            TestSite.ContactFormPage.NameField.SendKeys(TO_ADD_TEXT);
            new Check().AreEquals(TestSite.ContactFormPage.NameField.GetText, DEFAULT_TEXT + TO_ADD_TEXT);            
        }

        [Test]
        public void NewInputTest()
        {
            TestSite.ContactFormPage.NameField.NewInput(TO_ADD_TEXT);
            new Check().AreEquals(TestSite.ContactFormPage.NameField.GetText, TO_ADD_TEXT);            
        }

        [Test]
        public void ClearTest()
        {
            TestSite.ContactFormPage.NameField.Clear();
            new Check().AreEquals(TestSite.ContactFormPage.NameField.GetText, "");            
        }

        [Test]
        public void MultiKeyTest()
        {
            foreach (char letter in TO_ADD_TEXT)
            {
                TestSite.ContactFormPage.NameField.SendKeys(letter.ToString());
            }
            new Check().AreEquals(TestSite.ContactFormPage.NameField.GetText, DEFAULT_TEXT + TO_ADD_TEXT);            
        }

        //TO_DO
        /*
        [Test]
        public void FocusTest()
        {

        }
        */
    }
}
