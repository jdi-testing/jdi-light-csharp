using System;
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

            Throws<Exception>(() => TestSite.Html5Page.DisabledPicker.SetColor("#432376"));
            AreEqual(TestSite.Html5Page.DisabledPicker.Color(), _color);
            AreEqual(TestSite.Html5Page.ColorPicker.Color(), "#432376");
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