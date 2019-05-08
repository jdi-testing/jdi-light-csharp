using JDI.Light.Exceptions;
using JDI.Light.Interfaces.Complex;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Complex
{
    [TestFixture]
    public class RadioButtonTests : TestBase
    {
        private readonly string text = "Green";

        private IRadioButtons _colors;

        [SetUp]
        public void SetUp()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
            _colors = TestSite.Html5Page.ColorsRadioButton;
        }

        [Test]
        public void GetValueTest()
        {
            Jdi.Assert.AreEquals(text, _colors.Value);
        }

        [Test]
        public void SelectTest()
        {
            _colors.Select("Blue");
            Jdi.Assert.AreEquals("Blue", _colors.Selected());
        }

        [Test]
        public void SelectNumTest()
        {
            _colors.Select(1);
            Jdi.Assert.AreEquals("Red", _colors.Selected());
        }

        [Test]
        public void SelectedTest()
        {
            Jdi.Assert.AreEquals(text, _colors.Selected());
        }

        [Test]
        public void ValueTest()
        {
            Jdi.Assert.CollectionEquals(new[] { "Red", "Green", "Blue", "Yellow" }, _colors.Values());
        }

        [Test]
        public void WrongNameTest()
        {
            Assert.Throws<ElementNotFoundException>(() => _colors.Select("wrong"));
        }

        [Test]
        public void CheckEnabledTest()
        {
            Assert.Throws<ElementDisabledException>(() => _colors.Select("Yellow"));
            Jdi.Assert.AreEquals(text, _colors.Selected());
        }
    }
}