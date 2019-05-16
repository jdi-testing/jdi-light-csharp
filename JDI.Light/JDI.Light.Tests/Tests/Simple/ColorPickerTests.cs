using JDI.Light.Exceptions;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace JDI.Light.Tests.Tests.Simple
{
    [TestFixture]
    public class ColorPickerTests : TestBase
    {
        private readonly string _color = "#ffd7a6";

        [SetUp]
        public void SetUp()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
        }

        [Test]
        public void GetLabelTextTest()
        {
            AreEqual(TestSite.Html5Page.ColorPicker.LabelText(), "Select a color");
        }

        [Test]
        public void GetColorTest()
        {
            AreEqual(TestSite.Html5Page.DisabledPicker.Color(), _color);
        }

        [Test]
        public void SetColorTest()
        {
            TestSite.Html5Page.ColorPicker.SetColor("#432376");
            AreEqual(TestSite.Html5Page.ColorPicker.Color(), "#432376");
        }

        [Test]
        public void IsValidationTest()
        {
            TestSite.Html5Page.DisabledPicker.Is().ExpectedColor(_color)
                .Disabled();
            TestSite.Html5Page.ColorPicker.Is().Enabled();
        }

        [Test]
        public void AssertValidationTest()
        {
            TestSite.Html5Page.DisabledPicker.AssertThat().ExpectedColor(_color);
        }

        [Test]
        public void CheckEnabledTest()
        {
            Throws<ElementDisabledException>(() => TestSite.Html5Page.DisabledPicker.SetColor("#432376", true));
            AreEqual(TestSite.Html5Page.DisabledPicker.Color(), _color);

            DoesNotThrow(() => TestSite.Html5Page.DisabledPicker.SetColor("#432376", false));
            AreEqual(TestSite.Html5Page.DisabledPicker.Color(), "#432376");
        }
    }
}