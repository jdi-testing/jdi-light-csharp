using JDI.Light.Tests.Enums;
using NUnit.Framework;
using OpenQA.Selenium;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    public class DataListTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            TestSite.MetalsColorsPage.Open();
            TestSite.MetalsColorsPage.CheckOpened();
            TestSite.MetalsColorsPage.MetalsDataList.CaretLocator = By.CssSelector("#metals span.caret");
        }

        [Test]
        public void SelectDataList()
        {
            TestSite.MetalsColorsPage.MetalsDataList.Expand();
            TestSite.MetalsColorsPage.MetalsDataList.ItemLocator = By.XPath("//li/a");
            TestSite.MetalsColorsPage.MetalsDataList.Select(Metals.Bronze.ToString());
            Jdi.Assert.Contains(TestSite.ActionsLog.Texts[0], "Metals: value changed to Bronze");
        }

        [Test]
        public void SelectByIndex()
        {
            TestSite.MetalsColorsPage.MetalsDataList.Expand();
            TestSite.MetalsColorsPage.MetalsDataList.ItemLocator = By.CssSelector(".dropdown-menu > li > a");
            TestSite.MetalsColorsPage.MetalsDataList.Select(2);
            Jdi.Assert.Contains(TestSite.ActionsLog.Texts[0], "Metals: value changed to Silver");
        }

        [Test]
        public void FillDataList()
        {
            TestSite.MetalsColorsPage.MetalsInput.Input("My Metal");
            TestSite.MetalsColorsPage.SubmitButton.Click();
            Jdi.Assert.Contains(TestSite.ActionsLog.Texts[1], "Metals: value changed to My Metal");
        }
    }
}