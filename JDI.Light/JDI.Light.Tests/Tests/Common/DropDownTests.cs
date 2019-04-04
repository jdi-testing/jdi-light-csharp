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
            TestSite.MetalsColorsPage.Colors.ItemLocator = By.XPath("//li/a");
            TestSite.MetalsColorsPage.Colors.Select(Colors.Blue.ToString());

            Jdi.Assert.Contains(TestSite.ActionsLog.Texts[0], "Colors: value changed to Blue");
        }

        [Test]
        public void SelectByIndex()
        {
            TestSite.MetalsColorsPage.Colors.ItemLocator = By.CssSelector(".dropdown-menu > li > a");
            TestSite.MetalsColorsPage.Colors.Select(2);

            Jdi.Assert.Contains(TestSite.ActionsLog.Texts[0], "Colors: value changed to Green");
        }

        [Test]
        public void GetSelectedTest()
        {
            TestSite.MetalsColorsPage.Colors.ItemLocator = By.XPath("//li/a");
            TestSite.MetalsColorsPage.Colors.Select(Colors.Blue.ToString());

            var selected = TestSite.MetalsColorsPage.ColorsDropDownText.GetSelected();
            Assert.AreEqual(selected, "Blue");
        }

        [Test]
        public void NegativeDropDownTest()
        {
            TestSite.MetalsColorsPage.Colors.ItemLocator = By.XPath("//li/a");
            Assert.Throws<ElementNotFoundException>(() => TestSite.MetalsColorsPage.Colors.Select("xxx"));
        }
    }
}