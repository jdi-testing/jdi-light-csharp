using JDI.Light.Exceptions;
using JDI.Light.Interfaces.Common;
using NUnit.Framework;
using OpenQA.Selenium;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    public class CheckListTests : TestBase
    {
        private string text = "Hot option";

        private ICheckList weather;

        [SetUp]
        public void SetUp()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
            //TestSite.Html5Page.WeatherCheckList.Check(text);
            weather = TestSite.Html5Page.WeatherCheckList;
        }

        [Test]
        public void GetValueTest()
        {
            Jdi.Assert.CollectionEquals(new[] { text }, weather.GetChecked());
        }

        [Test]
        public void CheckCheckList()
        {
            TestSite.Html5Page.WeatherCheckList.Check(new[] { "Cold", "Sunny" });
            TestSite.Html5Page.WeatherCheckList.Uncheck("Cold");
        }

        [Test]
        public void CheckByIndexes()
        {
            TestSite.Html5Page.WeatherCheckList.Check(new[] { 1, 2, 3 });
            TestSite.Html5Page.WeatherCheckList.Uncheck(2);
        }

        [Test]
        public void GetCheckedTest()
        {
            TestSite.Html5Page.WeatherCheckList.Check(new[] { "Cold", "Sunny" });

            TestSite.Html5Page.WeatherCheckList.CheckListLocator = By.CssSelector(".html-left > input");
            string[] arr = { "hot", "cold", "rainy", "sunny-day" };
            var checkedValues = TestSite.Html5Page.WeatherCheckList.GetChecked();
            Assert.AreEqual(checkedValues, new[] { "sunny-day", "cold" });
        }

        [Test]
        public void NegativeCheckListTest()
        {
            var toCheck = new[] { 1000 };
            Assert.Throws<ElementNotFoundException>(() => TestSite.Html5Page.WeatherCheckList.Check(toCheck));
        }
    }
}