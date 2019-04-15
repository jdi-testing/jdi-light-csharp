using JDI.Light.Interfaces.Common;
using JDI.Light.Interfaces.Complex;
using JDI.Light.Tests.Enums;
using NUnit.Framework;
using OpenQA.Selenium;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    public class RadioButtonTests : TestBase
    {
        private string text = "Green";

        private IRadioButtons colors;

        [SetUp]
        public void SetUp()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
            colors = TestSite.Html5Page.ColorsRadioButton;
        }

        // todo add test after HasValue interface implementation
        //[Test]
        public void GetValueTest()
        {
        }

        [Test]
        public void SelectTest()
        {
            colors.Select("Blue");
            Jdi.Assert.AreEquals("Blue", colors.Selected());
        }

        [Test]
        public void SelectNumTest()
        {
            colors.Select(1);
            Jdi.Assert.AreEquals("Red", colors.Selected());
        }

        [Test]
        public void SelectedTest()
        {
            Jdi.Assert.AreEquals(text, colors.Selected());
        }

        [Test]
        public void ValueTest()
        {
            Jdi.Assert.CollectionEquals(new[] {"Red", "Green", "Blue", "Yellow"}, colors.Values());
        }
    }
}