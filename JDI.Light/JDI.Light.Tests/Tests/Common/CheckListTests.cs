using System;
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
            TestSite.Html5Page.WeatherCheckList.UncheckAll();
        }

        [Test]
        public void CheckCheckList()
        {
            TestSite.Html5Page.WeatherCheckList.Check(new[] { "cold", "sunny-day" });
            TestSite.Html5Page.WeatherCheckList.Uncheck("cold");
            Assert.IsFalse(TestSite.Html5Page.WeatherCheckList.IsChecked("cold"));
        }

        [Test]
        public void CheckByIndexes()
        {
            TestSite.Html5Page.WeatherCheckList.Check(new[] { 1, 2, 3 });
            TestSite.Html5Page.WeatherCheckList.Uncheck(2);
            Assert.IsFalse(TestSite.Html5Page.WeatherCheckList.IsChecked(2));
        }

        [Test]
        public void CheckAll()
        {
            TestSite.Html5Page.WeatherCheckList.CheckAll();
            string[] arr = { "hot", "cold", "rainy", "sunny-day" };
            foreach (var elem in arr)
            {
                Assert.IsTrue(TestSite.Html5Page.WeatherCheckList.IsChecked(elem));
            }
        }

        [Test]
        public void GetCheckedTest()
        {
            TestSite.Html5Page.WeatherCheckList.Check(new[] { "cold", "sunny-day" });
            TestSite.Html5Page.WeatherCheckList.CheckListLocator = By.CssSelector(".html-left > input");
            string[] arr = { "hot", "cold", "rainy", "sunny-day" };
            var checkedValues = TestSite.Html5Page.WeatherCheckList.GetChecked(arr);
            Assert.AreEqual(checkedValues, new[] { "sunny-day", "cold" });
        }

        [Test]
        public void NegativeCheckListTest()
        {
            var toCheck = new[] { "1000" };
            Assert.Throws<NullReferenceException>(() => TestSite.Html5Page.WeatherCheckList.Check(toCheck));
        }

        [Test]
        public void IsDisabledTests()
        {
            Assert.IsTrue(TestSite.Html5Page.WeatherCheckList.IsDisabled(4));
            Assert.IsTrue(TestSite.Html5Page.WeatherCheckList.IsDisabled("disabled-ch"));
        }
    }
}