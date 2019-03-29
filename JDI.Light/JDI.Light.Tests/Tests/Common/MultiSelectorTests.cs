using System;
using JDI.Light.Tests.Enums;
using NUnit.Framework;
using OpenQA.Selenium;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    public class MultiSelectorTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
            TestSite.Html5Page.AgeSelector.MultiItemLocator = By.CssSelector("option");
            TestSite.Html5Page.AgeSelector.UnselectAll(Enum.GetValues(typeof(Ages)));
        }

        [Test]
        public void MultiSelectByValues()
        {
            var toSelect = new[] {Ages.Metalic.ToString(), Ages.Electro.ToString()};
            
            TestSite.Html5Page.AgeSelector.Select(toSelect);
            var selectedItems = TestSite.Html5Page.AgeSelector.GetSelected(Enum.GetValues(typeof(Ages)));

            Assert.AreEqual(selectedItems, toSelect);
        }

        [Test]
        public void MultiSelectByIndexes()
        {
            TestSite.Html5Page.AgeSelector.Select(new[] { 1, 4 });
            var selectedItems = TestSite.Html5Page.AgeSelector.GetSelected(Enum.GetValues(typeof(Ages)));
            Assert.AreEqual(selectedItems, new[] {"Wood", "Steam" });
        }
    }
}