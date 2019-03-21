using JDI.Light.Tests.Enums;
using NUnit.Framework;
using OpenQA.Selenium;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    public class ComboBoxTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            TestSite.MetalsColorsPage.Open();
            TestSite.MetalsColorsPage.CheckOpened();
        }

        [Test]
        public void SelectComboBox()
        {
            TestSite.MetalsColorsPage.MetalsComboBox.CaretLocator = By.CssSelector("#metals span.caret");
            TestSite.MetalsColorsPage.MetalsComboBox.Expand();
            TestSite.MetalsColorsPage.MetalsComboBox.ItemLocator = By.XPath("//li/a");
            TestSite.MetalsColorsPage.MetalsComboBox.Select(Metals.Bronze.ToString());

            Jdi.Assert.Contains(TestSite.ActionsLog.Texts[0], "Metals: value changed to Bronze");
        }

        [Test]
        public void SelectByIndex()
        {
            const int indexToSelect = 2;

            TestSite.MetalsColorsPage.MetalsComboBox.CaretLocator = By.CssSelector("#metals span.caret");
            TestSite.MetalsColorsPage.MetalsComboBox.Expand();
            TestSite.MetalsColorsPage.MetalsComboBox.ItemLocator = By.CssSelector(string.Format($"li:nth-child({indexToSelect.ToString()})"));
            TestSite.MetalsColorsPage.MetalsComboBox.Select(indexToSelect);

            Jdi.Assert.Contains(TestSite.ActionsLog.Texts[0], "Metals: value changed to Gold");
        }

        [Test]
        public void FillComboBox()
        {
            TestSite.MetalsColorsPage.MetalsInputComboBox.Input("My Metal");
            TestSite.MetalsColorsPage.SubmitButton.Click();

            Jdi.Assert.Contains(TestSite.ActionsLog.Texts[1], "Metals: value changed to My Metal");
        }
    }
}