using System;
using JDI.Light.Settings;
using JDI.Light.Tests.Asserts;
using JDI.Light.Tests.Entities;
using JDI.Light.Tests.UIObjects;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Common
{
    public class TextFieldsTests
    {
        private const string CONTAINS = "ame";
        private const string REGEX = ".am.";
        private const string TO_ADD_TEXT = "text123!@#$%^&*()";
        private const string EXPECTED_TEXT = "text123!@#$%^&*()";
        private const string ELEMENT_TYPE = "TextField";
        private readonly string DEFAULT_TEXT = User.DefaultUser.Login;

        [SetUp]
        public void SetUp()
        {
            JDISettings.Logger.Info("Navigating to Metals and Colors page.");
            TestSite.ContactFormPage.Open();
            TestSite.ContactFormPage.CheckTitle();
            TestSite.ContactFormPage.IsOpened();
            TestSite.ContactFormPage.FillAndSubmitForm(DEFAULT_TEXT, DEFAULT_TEXT + new Random().Next(),
                DEFAULT_TEXT + new Random().Next());
            JDISettings.Logger.Info("Setup method finished");
            JDISettings.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }
        
        [Test]
        public void InputTest()
        {
            TestSite.ContactFormPage.NameField.Input(TO_ADD_TEXT);
            new NUnitAsserter().AreEquals(TestSite.ContactFormPage.NameField.GetText, DEFAULT_TEXT + TO_ADD_TEXT);
        }
        
        [Test]
        public void SendKeyTest()
        {
            TestSite.ContactFormPage.NameField.SendKeys(TO_ADD_TEXT);
            new NUnitAsserter().AreEquals(TestSite.ContactFormPage.NameField.GetText, DEFAULT_TEXT + TO_ADD_TEXT);
        }

        [Test]
        public void NewInputTest()
        {
            TestSite.ContactFormPage.NameField.NewInput(TO_ADD_TEXT);
            new NUnitAsserter().AreEquals(TestSite.ContactFormPage.NameField.GetText, TO_ADD_TEXT);
        }

        [Test]
        public void ClearTest()
        {
            TestSite.ContactFormPage.NameField.Clear();
            new NUnitAsserter().AreEquals(TestSite.ContactFormPage.NameField.GetText, "");
        }

        [Test]
        public void MultiKeyTest()
        {
            foreach (var letter in TO_ADD_TEXT) TestSite.ContactFormPage.NameField.SendKeys(letter.ToString());
            new NUnitAsserter().AreEquals(TestSite.ContactFormPage.NameField.GetText, DEFAULT_TEXT + TO_ADD_TEXT);
        }
    }
}