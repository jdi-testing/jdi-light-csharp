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
            TestSite.MetalsColorsPage.MetalsComboBox.CaretLocator = By.CssSelector("#metals span.caret");
        }

        [Test]
        public void SelectComboBox()
        {
            TestSite.MetalsColorsPage.MetalsComboBox.Expand();
            TestSite.MetalsColorsPage.MetalsComboBox.ItemLocator = By.XPath("//li/a");
            TestSite.MetalsColorsPage.MetalsComboBox.Select(Metals.Bronze.ToString());
            Jdi.Assert.Contains(TestSite.ActionsLog.Texts[0], "Metals: value changed to Bronze");
        }

        [Test]
        public void SelectByIndex()
        {
            TestSite.MetalsColorsPage.MetalsComboBox.Expand();
            TestSite.MetalsColorsPage.MetalsComboBox.ItemLocator = By.CssSelector(".dropdown-menu > li > a");

            TestSite.MetalsColorsPage.MetalsComboBox.Select(2);
            Jdi.Assert.Contains(TestSite.ActionsLog.Texts[0], "Metals: value changed to Silver");
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