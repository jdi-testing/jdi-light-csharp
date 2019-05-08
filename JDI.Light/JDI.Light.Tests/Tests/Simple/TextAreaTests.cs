using JDI.Light.Exceptions;
using JDI.Light.Matchers.StringMatchers;
using NUnit.Framework;
using Is = JDI.Light.Matchers.Is;

namespace JDI.Light.Tests.Tests.Simple
{
    [TestFixture]
    public class TextAreaTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            Jdi.Logger.Info("Navigating to Html5 page.");
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
            Jdi.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }
        private const string Text = "TextArea";

        [Test]
        public void GetTextTest()
        {
            TestSite.Html5Page.TextArea.SetText(Text);
            Assert.AreEqual(TestSite.Html5Page.TextArea.GetText(), Text);
        }

        [Test]
        public void GetValueTest()
        {
            TestSite.Html5Page.TextArea.SetText(Text);
            Assert.AreEqual(TestSite.Html5Page.TextArea.GetValue(), Text);
        }

        [Test]
        public void SendKeysTest()
        {
            TestSite.Html5Page.TextArea.SetText(Text);
            TestSite.Html5Page.TextArea.SendKeys("New text");
            Assert.AreEqual(TestSite.Html5Page.TextArea.GetText(), Text + "New text");
        }

        [Test]
        public void InputTest()
        {
            TestSite.Html5Page.TextArea.SetText(Text);
            TestSite.Html5Page.TextArea.Input("New text");
            Assert.AreEqual(TestSite.Html5Page.TextArea.GetText(), "New text");
        }

        [Test]
        public void GetLinesTest()
        {
            TestSite.Html5Page.TextArea.Clear();
            TestSite.Html5Page.TextArea.SetLines(true, "test 1", "test 2", "test 3");
            Jdi.Assert.CollectionEquals(TestSite.Html5Page.TextArea.GetLines(), new[] { "test 1", "test 2", "test 3" });
        }

        [Test]
        public void ClearTest()
        {
            TestSite.Html5Page.TextArea.SetText(Text);
            TestSite.Html5Page.TextArea.Clear();
            Assert.AreEqual(TestSite.Html5Page.TextArea.GetText(), "");
        }

        [Test]
        public void PlaceHolderTest()
        {
            Assert.AreEqual(TestSite.Html5Page.TextArea.Placeholder, "Input huge text");
        }

        [Test]
        public void DisabledTest()
        {
            Assert.Throws<ElementDisabledException>(() => TestSite.Html5Page.DisabledTextArea.SetLines(true, Text));
            Jdi.Assert.CollectionEquals(TestSite.Html5Page.DisabledTextArea.GetLines(), new[] { "" });
        }

        [Test]
        public void AddNewLineTest()
        {
            TestSite.Html5Page.TextArea.Clear();
            TestSite.Html5Page.TextArea.SetLines(true, "line1", "line2");
            TestSite.Html5Page.TextArea.AddNewLine("line3");
            Jdi.Assert.CollectionEquals(TestSite.Html5Page.TextArea.GetLines(), new[] { "line1", "line2", "line3" });
        }

        [Test]
        public void FocusTest()
        {
            TestSite.Html5Page.TextArea.Focus();
        }

        [Test]
        public void IsValidationTest()
        {
            TestSite.Html5Page.TextArea.Is().Enabled();
            TestSite.Html5Page.TextArea.SetText(Text);
            TestSite.Html5Page.TextArea.Is().Text(Is.EqualTo(Text))
                .Text(ContainsStringMatcher.ContainsString("Area"));
            TestSite.Html5Page.DisabledTextArea.Is().Displayed();
        }

        [Test]
        public void AssertValidationTest()
        {
            TestSite.Html5Page.TextArea.SetText(Text);
            TestSite.Html5Page.TextArea.AssertThat().Text(Is.EqualTo(Text));
        }
        
        [Test]
        public void RowsTest()
        {
            Assert.AreEqual(TestSite.Html5Page.TextArea.Rows(), 3);
            Assert.AreEqual(TestSite.Html5Page.TextArea.Cols(), 33);
            Assert.AreEqual(TestSite.Html5Page.TextArea.MinLength(), 10);
            Assert.AreEqual(TestSite.Html5Page.TextArea.MaxLength(), 200);

            TestSite.Html5Page.TextArea.Is().RowsCount(Is.EqualTo(3))
                .ColsCount(Is.EqualTo(33))
                .MinLength(Is.EqualTo(10))
                .MaxLength(Is.EqualTo(200));
        }
    }
}