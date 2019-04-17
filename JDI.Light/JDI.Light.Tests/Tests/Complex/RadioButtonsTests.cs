using JDI.Light.Interfaces.Complex;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Complex
{
    [TestFixture]
    public class RadioButtonTests : TestBase
    {
        private readonly string text = "Green";

        private IRadioButtons colors;

        [SetUp]
        public void SetUp()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
            colors = TestSite.Html5Page.ColorsRadioButton;
        }

        //[Test]
        public void GetValueTest()
        {
            // todo add test after HasValue interface implementation
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
            Jdi.Assert.CollectionEquals(new[] { "Red", "Green", "Blue", "Yellow" }, colors.Values());
        }
    }
}