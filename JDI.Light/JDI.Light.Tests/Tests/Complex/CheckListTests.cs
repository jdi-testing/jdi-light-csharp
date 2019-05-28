using JDI.Light.Exceptions;
using JDI.Light.Interfaces.Complex;
using NUnit.Framework;
using static JDI.Light.Matchers.CollectionMatchers.HasItemsMatcher<string>;
using static JDI.Light.Matchers.IntegerMatchers.HasSizeMatcher;
using static NUnit.Framework.Assert;
using Is = JDI.Light.Matchers.Is;

namespace JDI.Light.Tests.Tests.Complex
{
    [TestFixture]
    public class CheckListTests : TestBase
    {
        private const string Text = "Hot option";

        private ICheckList _weather;

        [SetUp]
        public void SetUp()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
            DoesNotThrow(() => TestSite.Html5Page.WeatherCheckList.Check(true, Text));
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
            Jdi.Assert.CollectionEquals(new[] { Text }, _weather.Checked());
        }

        [Test]
        public void DisabledTest()
        {
            Throws<ElementDisabledException>(() => _weather.Select(true, "Disabled"));
            Jdi.Assert.CollectionEquals(new[] { Text }, _weather.Checked());
        }

        [Test]
        public void UncheckTest()
        {
            _weather.Check(false, "Rainy day", "Sunny");
            _weather.Uncheck(false, "Rainy day", "Sunny");
            _weather.Is.Selected(HasSize(2));
            _weather.Is.Selected(HasItems(new[] { "Hot option", "Cold" }));
        }

        [Test]
        public void UncheckAllTest()
        {
            _weather.Uncheck(false, "Rainy day", "Sunny");
            _weather.UncheckAll();
            _weather.Is.Selected(HasSize(0));
        }

        [Test]
        public void CheckAllTest()
        {
            _weather.CheckAll();
            _weather.Is.Selected(HasSize(4));
            _weather.Is.Selected(HasItems(new[] { "Hot option", "Cold", "Rainy day", "Sunny" }));
        }

        [Test]
        public void WrongName()
        {
            Throws<ElementNotFoundException>(() => _weather.Check(true, "wrong"));
        }

        [Test]
        public void IsDisabledTest()
        {
            _weather.Select(false, "Disabled");
            _weather.Is.Selected(HasItems(new [] { "Hot option" } ));
        }

        [Test]
        public void IsValidationTest()
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
        public void AssertValidationTest()
        {
            _weather.AssertThat.Values(HasItems(new[] {"Hot option", "Cold", "Rainy day", "Sunny"}));
            _weather.AssertThat.Selected("Hot option");
        }
    }
}