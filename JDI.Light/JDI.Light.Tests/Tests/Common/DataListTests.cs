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
        }

        [Test]
        public void SelectDataList()
        {
            TestSite.MetalsColorsPage.OpenDataList();
            TestSite.MetalsColorsPage.MetalsDataList.ItemLocator = By.XPath("//li/a");
            TestSite.MetalsColorsPage.MetalsDataList.Select(Metals.Bronze.ToString());

            Jdi.Assert.Contains(TestSite.ActionsLog.Texts[0], "Metals: value changed to Bronze");
        }

        [Test]
        public void SelectByIndex()
        {
            const int indexToSelect = 2;

            TestSite.MetalsColorsPage.OpenDataList();
            TestSite.MetalsColorsPage.MetalsDataList.ItemLocator = By.CssSelector(string.Format($"li:nth-child({indexToSelect.ToString()})"));
            TestSite.MetalsColorsPage.MetalsDataList.Select(indexToSelect);

            Jdi.Assert.Contains(TestSite.ActionsLog.Texts[0], "Metals: value changed to Gold");
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