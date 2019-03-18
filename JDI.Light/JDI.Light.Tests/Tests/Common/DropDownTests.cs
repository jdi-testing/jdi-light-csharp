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
            TestSite.MetalsColorsPage.CheckOpened();
        }

        [Test]
        public void SelectDropDown()
        {
            TestSite.MetalsColorsPage.ColorsDropDown.ItemLocator = By.XPath("//li/a");
            TestSite.MetalsColorsPage.ColorsDropDown.Select(Colors.Blue.ToString());

            Jdi.Assert.Contains(TestSite.ActionsLog.Texts[0], "Colors: value changed to Blue");
        }

        [Test]
        public void SelectByIndex()
        {
            const int indexToSelect = 2;

            TestSite.MetalsColorsPage.ColorsDropDown.ItemLocator = By.CssSelector(string.Format($"li:nth-child({indexToSelect.ToString()})"));
            TestSite.MetalsColorsPage.ColorsDropDown.Select(indexToSelect);

            Jdi.Assert.Contains(TestSite.ActionsLog.Texts[0], "Colors: value changed to Red");
        }

        [Test]
        public void GetSelectedTest()
        {
            TestSite.MetalsColorsPage.ColorsDropDown.ItemLocator = By.XPath("//li/a");
            TestSite.MetalsColorsPage.ColorsDropDown.Select(Colors.Blue.ToString());

            var selected = TestSite.MetalsColorsPage.ColorsDropDownText.GetSelected();
            Assert.AreEqual(selected, "Blue");
        }

        [Test]
        public void NegativeDropDownTest()
        {
            TestSite.MetalsColorsPage.ColorsDropDown.ItemLocator = By.XPath("//li/a");
            Assert.Throws<ElementNotFoundException>(() => TestSite.MetalsColorsPage.ColorsDropDown.Select("xxx"));
        }
    }
}