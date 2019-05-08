using JDI.Light.Exceptions;
using JDI.Light.Tests.Enums;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    public class DropListTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            TestSite.MetalsColorsPage.Open();
            TestSite.MetalsColorsPage.CheckOpened();
            TestSite.MetalsColorsPage.ColorsDropDown.Expand();
        }

        [Test]
        public void SelectTest()
        {
            Assert.DoesNotThrow(() => TestSite.MetalsColorsPage.ColorsDropDown.Select(Colors.Blue.ToString(), true));
            Jdi.Assert.Contains(TestSite.ActionsLog.Texts[0], "Colors: value changed to Blue");
        }

        [Test]
        public void SelectEnumTest()
        {
            TestSite.MetalsColorsPage.ColorsDropDown.Select(Colors.Blue);
            Jdi.Assert.Contains(TestSite.ActionsLog.Texts[0], "Colors: value changed to Blue");
        }

        [Test]
        public void SelectNumTest()
        {
            TestSite.MetalsColorsPage.ColorsDropDown.Select(2);
            Jdi.Assert.Contains(TestSite.ActionsLog.Texts[0], "Colors: value changed to Red");
        }

        [Test]
        public void SelectedTest()
        {
            TestSite.MetalsColorsPage.ColorsDropDown.Select(Colors.Blue.ToString());
            var selected = TestSite.MetalsColorsPage.ColorsDropDown.GetSelected();
            Assert.AreEqual(selected, "Blue");
        }

        [Test]
        public void NegativeDropDownTest()
        {
             Assert.Throws<ElementNotFoundException>(() => TestSite.MetalsColorsPage.ColorsDropDown.Select("xxx"));
        }
    }
}