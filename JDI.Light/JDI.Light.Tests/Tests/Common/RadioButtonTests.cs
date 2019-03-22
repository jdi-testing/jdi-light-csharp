using JDI.Light.Tests.Enums;
using NUnit.Framework;
using OpenQA.Selenium;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    public class RadioButtonTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
        }

        [Test]
        public void SelectRadioButton()
        {
            var toSelect = Colors.Blue.ToString();

            TestSite.Html5Page.ColorsRadioButton.RadioLocator = By.XPath("//label");
            TestSite.Html5Page.ColorsRadioButton.Select(toSelect);

            TestSite.Html5Page.ColorsRadioButton.RadioLocator = By.CssSelector("[type='radio']");
            var selectedItems = TestSite.Html5Page.ColorsRadioButton.GetSelected();

            Assert.AreEqual(selectedItems, toSelect.ToLower());
        }

        [Test]
        public void SelectRadioByIndex()
        {
            const int toSelect = 1;

            TestSite.Html5Page.ColorsRadioButton.RadioLocator = By.CssSelector($"[type='radio']:nth-child({toSelect})");
            TestSite.Html5Page.ColorsRadioButton.Select(toSelect);

            TestSite.Html5Page.ColorsRadioButton.RadioLocator = By.CssSelector("[type='radio']");
            var selectedItems = TestSite.Html5Page.ColorsRadioButton.GetSelected();

            Assert.AreEqual(selectedItems, "red");
        }

        [Test]
        public void GetSelectedRadio()
        {
            TestSite.Html5Page.ColorsRadioButton.RadioLocator = By.CssSelector("[type='radio']");
            var selectedItems = TestSite.Html5Page.ColorsRadioButton.GetSelected();

            Assert.AreEqual(selectedItems, Colors.Green.ToString().ToLower());
        }
    }
}