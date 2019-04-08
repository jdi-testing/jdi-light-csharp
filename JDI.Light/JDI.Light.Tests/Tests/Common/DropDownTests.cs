using JDI.Light.Exceptions;
using JDI.Light.Tests.Enums;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    public class DropDownTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            TestSite.MetalsColorsPage.Open();
            TestSite.MetalsColorsPage.CheckOpened();
        }

        [Test]
        public void SelectDropDown()
        {
            TestSite.MetalsColorsPage.ColorsDropDown.Expand();
            TestSite.MetalsColorsPage.ColorsDropDown.Select(Colors.Blue.ToString());
            Jdi.Assert.Contains(TestSite.ActionsLog.Texts[0], "Colors: value changed to Blue");
        }

        [Test]
        public void SelectByIndex()
        {
            TestSite.MetalsColorsPage.ColorsDropDown.Expand();
            TestSite.MetalsColorsPage.ColorsDropDown.Select(2);

            Jdi.Assert.Contains(TestSite.ActionsLog.Texts[0], "Colors: value changed to Green");
        }

        [Test]
        public void GetSelectedTest()
        {
            TestSite.MetalsColorsPage.ColorsDropDown.Expand();
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