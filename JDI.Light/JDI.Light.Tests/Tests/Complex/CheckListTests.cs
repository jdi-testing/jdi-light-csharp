using System.Collections.Generic;
using JDI.Light.Exceptions;
using JDI.Light.Interfaces.Complex;
using NUnit.Framework;
using static JDI.Light.Matchers.CollectionMatchers.HasItemsMatcher<string>;
using Is = JDI.Light.Matchers.Is;

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
            Assert.DoesNotThrow(() => TestSite.Html5Page.WeatherCheckList.Check(true, text));
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
            _weather.Check(true, "Cold", "Hot option");
            Jdi.Assert.CollectionEquals(new[] { "Cold", "Hot option" }, _weather.Checked());
        }

        [Test]
        public void SelectNumTest()
        {
            _weather.Check(true, 1, 4);
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
            Assert.Throws<ElementDisabledException>(() => _weather.Select(true, "Disabled"));
            Jdi.Assert.CollectionEquals(new[] { text }, _weather.Checked());
        }

        [Test]
        public void UncheckTest()
        {
            _weather.Uncheck(false, text);
            Jdi.Assert.CollectionEquals(new[] { "Cold", "Rainy day", "Sunny" }, _weather.Checked());
        }

        [Test]
        public void UncheckNumTest()
        {
            _weather.Uncheck(false, 1, 3);
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
            Assert.Throws<ElementNotFoundException>(() => _weather.Check(true, "wrong"));
        }

        [Test]
        public void IsDisabledTests()
        {
            Assert.IsTrue(_weather.IsDisabled(5));
            Assert.IsTrue(_weather.IsDisabled("Disabled"));
        }

        [Test]
        public void IsValidationTests()
        {
            _weather.Is
                .Selected("Hot option")
                .Selected(Is.SubsequenceOf(new[] {"Hot option", "Cold"}));
            _weather.AssertThat
                .Values(HasItems(new[] {"Sunny"}))
                .Disabled(HasItems(new[] {"Disabled"}))
                .Enabled(HasItems(new[] {"Sunny"}))
                .Enabled(HasItems(new[] {"Cold", "Sunny"}))
                .Size(Is.LessThan(6))
                .AllDisplayed()
                .AllTags(Is.SubsequenceOf(new[] {"input"}))
                .AllCss("color", Is.SubsequenceOf(new[] { "rgba(102, 102, 102, 1)" }));
            _weather.Has
                .Size(5)
                .NotEmpty()
                .Attrs(Is.SubsequenceOf(new[] { "class" }))
                .CssClasses(Is.SubsequenceOf(new[] { "html-left" }))
                .HasCssClasses("html-left");
            _weather.CheckBoxes[0].Has.Selected();
            _weather.CheckBoxes[4].ShouldBe.Deselected();
        }

        [Test]
        public void AssertValidationTests()
        {
            _weather.AssertThat.Values(HasItems(new[] {"Hot option", "Cold", "Rainy day", "Sunny"}));
            _weather.AssertThat.Selected("Hot option");
        }
    }
}