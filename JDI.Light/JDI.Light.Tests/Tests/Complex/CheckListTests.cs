using System.Collections.Generic;
using System.Net.Mime;
using JDI.Light.Interfaces.Common;
using JDI.Light.Interfaces.Complex;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Complex
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
            TestSite.Html5Page.WeatherCheckList.Check(text);
            weather = TestSite.Html5Page.WeatherCheckList;
        }

        // todo add test after HasValue interface implementation
        //[Test]
        public void GetValueTest()
        {
        }

        [Test]
        public void SelectTest()
        {
            weather.Check("Cold", "Hot option");
            Jdi.Assert.CollectionEquals(new[] { "Cold", "Hot option" }, weather.Checked());
        }

        [Test]
        public void SelectNumTest()
        {
            weather.Check(1, 4);
            Jdi.Assert.CollectionEquals(new[] { "Hot option", "Sunny" }, weather.Checked());
        }

        [Test]
        public void SelectedTest()
        {
            Jdi.Assert.CollectionEquals(new[] { text }, weather.Checked());
        }

        [Test]
        public void DisabledTest()
        {
            weather.Select("Disabled");
            Jdi.Assert.CollectionEquals(new[] { text }, weather.Checked());
        }

        [Test]
        public void UncheckTest()
        {
            weather.Uncheck(text);
            Jdi.Assert.CollectionEquals(new[] { "Cold", "Rainy day", "Sunny" }, weather.Checked());
        }

        [Test]
        public void UncheckNumTest()
        {
            weather.Uncheck(1, 3);
            Jdi.Assert.CollectionEquals(new[] { "Cold", "Sunny" }, weather.Checked());
        }

        [Test]
        public void UncheckAll()
        {
            weather.UncheckAll();
            Jdi.Assert.CollectionEquals(new List<string>(), weather.Checked());
        }
    }
}