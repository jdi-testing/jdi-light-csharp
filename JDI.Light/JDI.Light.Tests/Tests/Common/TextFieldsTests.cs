using JDI.Light.Exceptions;
using JDI.Light.Interfaces.Common;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    public class TextFieldsTests : TestBase
    {
        private const string ToAddText = "text123!@#$%^&*()";
        private const string Text = "TextField";
        private ITextField _name;
        private ITextField _disabledName;

        [SetUp]
        public void SetUp()
        {
            Jdi.Logger.Info($"Navigating to {TestSite.Html5Page.Name} page.");
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
            _name = TestSite.Html5Page.NameTextField;
            _disabledName = TestSite.Html5Page.SurnameTextField;
            _name.SetText(Text);
            Jdi.Logger.Info("Setup method finished");
            Jdi.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void GetTextTest()
        {
            Jdi.Assert.AreEquals(_name.GetText(), Text);
        }

        [Test]
        public void GetValueTest()
        {
            Jdi.Assert.AreEquals(_name.GetValue(), Text);
        }

        [Test]
        public void InputTest()
        {
            _name.Input("New text");
            Jdi.Assert.AreEquals(_name.GetText(), "New text");
        }
        
        [Test]
        public void SendKeyTest()
        {
            _name.SendKeys("Test");
            Jdi.Assert.AreEquals(_name.GetValue(), Text + "Test");
        }

        [Test]
        public void ClearTest()
        {
            _name.Clear();
            Jdi.Assert.AreEquals(_name.GetText(), string.Empty);
        }

        [Test]
        public void PlaceholderTest()
        {
            Jdi.Assert.AreEquals(_name.Placeholder, "Input name");
        }

        [Test]
        public void DisabledTest()
        {
            Assert.Throws<ElementDisabledException>(() => _disabledName.SendKeys(Text));
            Jdi.Assert.AreEquals(_disabledName.GetText(), string.Empty);

            Assert.Throws<ElementDisabledException>(() => _disabledName.Input(Text, true));
            Jdi.Assert.AreEquals(_disabledName.GetText(), string.Empty);
            _disabledName.Is
                .Disabled()
                .Displayed();
            _disabledName.AssertThat.Displayed();
        }

        [Test]
        public void FocusTest()
        {
            _name.Focus();
        }

        //[Test]
        public void LabelTest()
        {
            // todo: implement HasLabel interface
        }

        [Test]
        public void MultiKeyTest()
        {
            foreach (var letter in ToAddText)
            {
                _name.SendKeys(letter.ToString());
            }
            Jdi.Assert.AreEquals(_name.Value, Text + ToAddText);
        }
    }
}