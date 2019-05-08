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
            Assert.AreEqual(TestSite.Html5Page.ColorPicker.Color(), "#432376");
        }

        [Test]
        public void CheckEnabledTest()
        {
            Assert.Throws<ElementDisabledException>(() => TestSite.Html5Page.DisabledPicker.SetColor("#432376", true));
            Assert.AreEqual(TestSite.Html5Page.DisabledPicker.Color(), _color);

            Assert.DoesNotThrow(() => TestSite.Html5Page.DisabledPicker.SetColor("#432376", false));
            Assert.AreEqual(TestSite.Html5Page.DisabledPicker.Color(), "#432376");
        }
    }
}