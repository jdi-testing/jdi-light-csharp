using JDI.Light.Exceptions;
using JDI.Light.Tests.Enums;
using NUnit.Framework;
using OpenQA.Selenium;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    public class DropDownTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            TestSite.MetalsColorsPage.Open();
            TestSite.MetalsColorsPage.CheckUrl();
            TestSite.MetalsColorsPage.CheckTitle();
        }

        [Test]
        public void SelectDropDown()
        {
            TestSite.MetalsColorsPage.ColorsDropDown.Select(Colors.Blue.ToString());
            Jdi.Assert.Contains(TestSite.ActionsLog.Texts[0], "Colors: value changed to Blue");
        }

        [Test]
        public void SelectByIndex()
        {
            TestSite.MetalsColorsPage.ColorsDropDown.Select(2);
            Jdi.Assert.Contains(TestSite.ActionsLog.Texts[0], "Colors: value changed to Red");
        }

        [Test]
        public void GetSelectedTest()
        {
            TestSite.MetalsColorsPage.ColorsDropDown.Select(Colors.Blue.ToString());
            var selected = TestSite.MetalsColorsPage.ColorsDropDownText.GetSelected();
            Assert.AreEqual(selected, "Blue");
        }

        [Test]
        public void NegativeDropDownTest()
        {
            Assert.Throws<ElementNotFoundException>(() => TestSite.MetalsColorsPage.ColorsDropDown.Select("xxx"));
        }
    }
}