using NUnit.Framework;
using OpenQA.Selenium;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    public class CheckListTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
            TestSite.Html5Page.WeatherCheckList.CheckListLocator = By.CssSelector(".html-left > input");

            string[] arr = {"hot", "cold", "rainy", "sunny-day"};
            TestSite.Html5Page.WeatherCheckList.UncheckAll(arr);
        }

        [Test]
        public void CheckCheckList()
        {
            TestSite.Html5Page.WeatherCheckList.CheckListLocator = By.CssSelector(".html-left > label");
            var toCheck = new[] {"Cold", "Sunny"};
            TestSite.Html5Page.WeatherCheckList.Check(toCheck);
        }

        [Test]
        public void CheckByIndexes()
        {
            TestSite.Html5Page.WeatherCheckList.CheckListLocator = By.CssSelector(".html-left > label");
            var toCheck = new[] { 4, 10};
            foreach (var index in toCheck)
            {
                TestSite.Html5Page.WeatherCheckList.CheckListLocators.Add(
                    By.CssSelector($"[name='checks-group']:nth-child({index.ToString()})"));
            }
            TestSite.Html5Page.WeatherCheckList.Check(toCheck);
        }

        [Test]
        public void CheckUncheckTest()
        {
            TestSite.Html5Page.WeatherCheckList.CheckListLocator = By.CssSelector(".html-left > label");
            var toCheck = new[] { "Cold", "Sunny" };
            TestSite.Html5Page.WeatherCheckList.Check(toCheck);

            var toUncheck = new[] { "Cold" };
            TestSite.Html5Page.WeatherCheckList.Uncheck(toUncheck);

            string[] arr = { "hot", "cold", "rainy", "sunny-day" };
            TestSite.Html5Page.WeatherCheckList.CheckListLocator = By.CssSelector(".html-left > input");
            var checkedValues = TestSite.Html5Page.WeatherCheckList.GetChecked(arr);
            Assert.AreEqual(checkedValues, new[] { "sunny-day" });
        }
    }
}