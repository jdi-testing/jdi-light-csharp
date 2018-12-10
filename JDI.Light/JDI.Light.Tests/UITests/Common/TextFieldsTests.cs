using System;
using JDI.Light.Tests.Entities;
using JDI.Light.Tests.UIObjects;
using NUnit.Framework;

namespace JDI.Light.Tests.UITests.Common
{
    [TestFixture]
    public class TextFieldsTests : TestBase
    {
        private const string ToAddText = "text123!@#$%^&*()";
        private readonly string _defaultText = User.DefaultUser.Login;

        [SetUp]
        public void SetUp()
        {
            JDI.Logger.Info("Navigating to Metals and Colors page.");
            TestSite.ContactFormPage.Open();
            TestSite.ContactFormPage.CheckTitle();
            TestSite.ContactFormPage.IsOpened();
            TestSite.ContactFormPage.FillAndSubmitForm(_defaultText, _defaultText + new Random().Next(),
                _defaultText + new Random().Next());
            JDI.Logger.Info("Setup method finished");
            JDI.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }
        
        [Test]
        public void InputTest()
        {
            TestSite.ContactFormPage.NameField.Input(ToAddText);
            JDI.Assert.AreEquals(TestSite.ContactFormPage.NameField.Value, _defaultText + ToAddText);
        }
        
        [Test]
        public void SendKeyTest()
        {
            TestSite.ContactFormPage.NameField.SendKeys(ToAddText);
            JDI.Assert.AreEquals(TestSite.ContactFormPage.NameField.Value, _defaultText + ToAddText);
        }

        [Test]
        public void NewInputTest()
        {
            TestSite.ContactFormPage.NameField.NewInput(ToAddText);
            JDI.Assert.AreEquals(TestSite.ContactFormPage.NameField.Value, ToAddText);
        }

        [Test]
        public void ClearTest()
        {
            TestSite.ContactFormPage.NameField.Clear();
            JDI.Assert.AreEquals(TestSite.ContactFormPage.NameField.Value, "");
        }

        [Test]
        public void MultiKeyTest()
        {
            foreach (var letter in ToAddText)
            {
                TestSite.ContactFormPage.NameField.SendKeys(letter.ToString());
            }
            JDI.Assert.AreEquals(TestSite.ContactFormPage.NameField.Value, _defaultText + ToAddText);
        }
    }
}