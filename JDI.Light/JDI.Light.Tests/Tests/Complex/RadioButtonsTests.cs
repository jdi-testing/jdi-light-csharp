using JDI.Light.Exceptions;
using JDI.Light.Interfaces.Complex;
using NUnit.Framework;
using Is = JDI.Light.Matchers.Is;

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
            _colors.AssertThat().Selected(Is.EqualTo(text));
        }

        [Test]
        public void SelectTest()
        {
            _colors.Select("Blue");
            _colors.AssertThat().Selected(Is.EqualTo("Blue"));
        }

        [Test]
        public void SelectNumTest()
        {
            _colors.Select(1);
            _colors.AssertThat().Selected(Is.EqualTo("Red"));
        }

        [Test]
        public void SelectedTest()
        {
            _colors.AssertThat().Selected(Is.EqualTo(text));            
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
            _colors.Is().Selected(Is.EqualTo(text));            
        }
    }
}