using JDI.Light.Exceptions;
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
            TestSite.Html5Page.WeatherCheckList.CheckListLocator = By.CssSelector(".html-left > label");
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
            var checkedValues = TestSite.Html5Page.WeatherCheckList.GetChecked(arr);
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