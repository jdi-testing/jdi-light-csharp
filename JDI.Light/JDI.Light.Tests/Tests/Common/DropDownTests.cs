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
            TestSite.MetalsColorsPage.MetalsDropDown.DropDownItemLocator = By.XPath("//li/a");
            TestSite.MetalsColorsPage.MetalsDropDown.Select(Metals.Bronze.ToString());
            Jdi.Assert.Contains(TestSite.ActionsLog.Texts[0], "Metals: value changed to Bronze");
        }

        [Test]
        public void SelectByIndex()
        {
            TestSite.MetalsColorsPage.ColorsDropDown.Select(2);
            Jdi.Assert.Contains(TestSite.ActionsLog.Texts[0], "Colors: value changed to Red");
        }

        [Test]
        public void NegativeDropDownTest()
        {
            TestSite.MetalsColorsPage.ColorsDropDown.DropDownItemLocator = By.XPath("//li/a");
            Assert.Throws<ElementNotFoundException>(() => TestSite.MetalsColorsPage.ColorsDropDown.Select("xxx"));
        }
    }
}