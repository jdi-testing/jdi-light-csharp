using System.Collections.Generic;
using JDI.Light.Exceptions;
using JDI.Light.Interfaces.Complex;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Complex
{
    [TestFixture]
    public class CheckListTests : TestBase
    {
        private readonly string text = "Hot option";

        private ICheckList _weather;

        [SetUp]
        public void SetUp()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
            TestSite.Html5Page.WeatherCheckList.Check(text);
            _weather = TestSite.Html5Page.WeatherCheckList;
        }
        
        [Test]
        public void GetValueTest()
        {
            Jdi.Assert.CollectionEquals(new[] {"Hot option"}, _weather.Value);
        }

        [Test]
        public void SelectTest()
        {
            _weather.Check("Cold", "Hot option");
            Jdi.Assert.CollectionEquals(new[] { "Cold", "Hot option" }, _weather.Checked());
        }

        [Test]
        public void SelectNumTest()
        {
            _weather.Check(1, 4);
            Jdi.Assert.CollectionEquals(new[] { "Hot option", "Sunny" }, _weather.Checked());
        }

        [Test]
        public void SelectedTest()
        {
            Jdi.Assert.CollectionEquals(new[] { text }, _weather.Checked());
        }

        [Test]
        public void DisabledTest()
        {
            _weather.Select("Disabled");
            Jdi.Assert.CollectionEquals(new[] { text }, _weather.Checked());
        }

        [Test]
        public void UncheckTest()
        {
            _weather.Uncheck(text);
            Jdi.Assert.CollectionEquals(new[] { "Cold", "Rainy day", "Sunny" }, _weather.Checked());
        }

        [Test]
        public void UncheckNumTest()
        {
            _weather.Uncheck(1, 3);
            Jdi.Assert.IsFalse(_weather.IsChecked(1));
            Jdi.Assert.IsTrue(_weather.IsChecked(2));
            Jdi.Assert.IsTrue(_weather.IsChecked("Cold"));
            Jdi.Assert.CollectionEquals(new[] { "Cold", "Sunny" }, _weather.Checked());
        }

        [Test]
        public void UncheckAllTest()
        {
            _weather.UncheckAll();
            Jdi.Assert.CollectionEquals(new List<string>(), _weather.Checked());
        }

        [Test]
        public void CheckAllTest()
        {
            _weather.CheckAll();
            Jdi.Assert.CollectionEquals(new[] {"Hot option", "Cold", "Rainy day", "Sunny"}, _weather.Checked());
        }

        [Test]
        public void WrongName()
        {
            Assert.Throws<ElementNotFoundException>(() => _weather.Check("wrong"));
        }

        [Test]
        public void IsDisabledTests()
        {
            Assert.IsTrue(TestSite.Html5Page.WeatherCheckList.IsDisabled(5));
            Assert.IsTrue(TestSite.Html5Page.WeatherCheckList.IsDisabled("Disabled"));
        }
    }
}