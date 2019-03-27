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
            TestSite.Html5Page.ColorsRadioButton.RadioLocator = By.XPath("//label");
        }

        [Test]
        public void SelectRadioButton()
        {
            var toSelect = Colors.Blue.ToString();
            TestSite.Html5Page.ColorsRadioButton.Select(toSelect);

            TestSite.Html5Page.ColorsRadioButton.RadioLocator = By.CssSelector("[type='radio']");
            var selectedItems = TestSite.Html5Page.ColorsRadioButton.GetSelected();

            Assert.AreEqual(selectedItems, toSelect.ToLower());
        }

        [Test]
        public void SelectRadioByIndex()
        {
            TestSite.Html5Page.ColorsRadioButton.Select(2);

            TestSite.Html5Page.ColorsRadioButton.RadioLocator = By.CssSelector("[type='radio']");
            var selectedItems = TestSite.Html5Page.ColorsRadioButton.GetSelected();

            Assert.AreEqual(selectedItems, "green");
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