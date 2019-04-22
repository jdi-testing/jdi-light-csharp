using System;
using JDI.Light.Tests.Entities;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    public class InputTests : TestBase
    {
        private const string ToAddText = "text123!@#$%^&*()";
        private readonly string _defaultText = User.DefaultUser.Login;

        [SetUp]
        public void SetUp()
        {
            Jdi.Logger.Info("Navigating to Metals and Colors page.");
            TestSite.ContactFormPage.Open();
            TestSite.ContactFormPage.CheckTitle();
            TestSite.ContactFormPage.FillAndSubmitForm(_defaultText, _defaultText + new Random().Next(),
                _defaultText + new Random().Next());
            Jdi.Logger.Info("Setup method finished");
            Jdi.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void InputTest()
        {
            TestSite.ContactFormPage.NameInput.Input(ToAddText);
            Jdi.Assert.AreEquals(TestSite.ContactFormPage.NameInput.Value, ToAddText);
        }

        [Test]
        public void SendKeyTest()
        {
            TestSite.ContactFormPage.NameInput.SendKeys(ToAddText);
            Jdi.Assert.AreEquals(TestSite.ContactFormPage.NameInput.Value, _defaultText + ToAddText);
        }

        [Test]
        public void ClearTest()
        {
            TestSite.ContactFormPage.NameInput.Clear();
            Jdi.Assert.AreEquals(TestSite.ContactFormPage.NameInput.Value, "");
        }

        [Test]
        public void MultiKeyTest()
        {
            foreach (var letter in ToAddText)
            {
                TestSite.ContactFormPage.NameInput.SendKeys(letter.ToString());
            }
            Jdi.Assert.AreEquals(TestSite.ContactFormPage.NameInput.Value, _defaultText + ToAddText);
        }
    }
}