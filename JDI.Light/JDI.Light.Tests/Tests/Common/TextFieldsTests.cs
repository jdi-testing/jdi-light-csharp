using JDI.Light.Exceptions;
using JDI.Light.Matchers.StringMatchers;
using NUnit.Framework;
using Is = JDI.Light.Matchers.Is;
using static JDI.Light.Matchers.StringMatchers.ContainsStringMatcher;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    public class TextFieldsTests : TestBase
    {
        private const string ToAddText = "text123!@#$%^&*()";
        private const string Text = "TextField";

        [SetUp]
        public void SetUp()
        {
            Jdi.Logger.Info($"Navigating to {TestSite.Html5Page.Name} page.");
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
            TestSite.Html5Page.NameTextField.SetText(Text);
            Jdi.Logger.Info("Setup method finished");
            Jdi.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void GetTextTest()
        {
            Jdi.Assert.AreEquals(TestSite.Html5Page.NameTextField.GetText(), Text);
        }

        [Test]
        public void GetValueTest()
        {
            Jdi.Assert.AreEquals(TestSite.Html5Page.NameTextField.GetValue(), Text);
        }

        [Test]
        public void InputTest()
        {
            TestSite.Html5Page.NameTextField.Input("New text");
            Jdi.Assert.AreEquals(TestSite.Html5Page.NameTextField.GetText(), "New text");
        }
        
        [Test]
        public void SendKeyTest()
        {
            TestSite.Html5Page.NameTextField.SendKeys("Test");
            Jdi.Assert.AreEquals(TestSite.Html5Page.NameTextField.GetValue(), Text + "Test");
        }
        
        [Test]
        public void ClearTest()
        {
            TestSite.Html5Page.NameTextField.Clear();
            Jdi.Assert.AreEquals(TestSite.Html5Page.NameTextField.GetText(), string.Empty);
        }

        [Test]
        public void PlaceholderTest()
        {
            Jdi.Assert.AreEquals(TestSite.Html5Page.NameTextField.Placeholder, "Input name");
        }

        [Test]

        public void DisabledTest()
        {
            Assert.Throws<ElementDisabledException>(() => TestSite.Html5Page.SurnameTextField.SendKeys(Text));
            Jdi.Assert.AreEquals(TestSite.Html5Page.SurnameTextField.GetText(), string.Empty);
            Assert.Throws<ElementDisabledException>(() => TestSite.Html5Page.SurnameTextField.Input(Text, true));
            Jdi.Assert.AreEquals(TestSite.Html5Page.SurnameTextField.GetText(), string.Empty);
            TestSite.Html5Page.SurnameTextField.Is
                .Disabled()
                .Displayed();
            TestSite.Html5Page.SurnameTextField.AssertThat.Displayed();
        }

        [Test]
        public void FocusTest()
        {
            TestSite.Html5Page.NameTextField.Focus();
        }

        [Test]
        public void IsValidationTest()
        {
            TestSite.Html5Page.NameTextField.Is.Enabled()
                .Text(Is.EqualTo(Text))
                .Text(ContainsStringMatcher.ContainsString("Field"));
            TestSite.Html5Page.SurnameTextField.Is.Disabled();
        }

        [Test]
        public void LabelTest()
        {
            Assert.AreEqual(TestSite.Html5Page.NameTextField.Label().GetText(), "Your name:");
            TestSite.Html5Page.NameTextField.Label().Is.Text(ContainsString("Your"));
            Assert.AreEqual(TestSite.Html5Page.SurnameTextField.Label().GetText(), "Surname:");
            TestSite.Html5Page.SurnameTextField.Label().Is.Text(ContainsString("Surname:"));
        }

        [Test]
        public void AssertValidationTest()
        {
            TestSite.Html5Page.NameTextField.AssertThat.Text(Is.EqualTo(Text));
        }
    }
}