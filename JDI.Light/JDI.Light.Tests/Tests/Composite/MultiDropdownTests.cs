using System.Collections.Generic;
using JDI.Light.Tests.Enums;
using NUnit.Framework;
using static JDI.Light.Elements.Base.BaseValidation;
using static JDI.Light.Jdi;
using static JDI.Light.Matchers.CollectionMatchers.ContainsInAnyOrderMatcher<string>;
using static JDI.Light.Matchers.CollectionMatchers.HasItemsMatcher<string>;
using static JDI.Light.Matchers.StringMatchers.ContainsStringMatcher;

namespace JDI.Light.Tests.Tests.Composite
{
    [TestFixture]
    public class MultiDropdownTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            Logger.Info("Navigating to HTML5 page.");
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckTitle();
            TestSite.Html5Page.CheckOpened();
            Logger.Info("Setup method finished");
            Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ExpandMultiDropdown()
        {
            TestSite.Html5Page.MultiDropdown.Expand();
        }

        [Test]
        public void SelectSingleOption()
        {
            var option = "Electro";
            TestSite.Html5Page.MultiDropdown.SelectOption(option);
            var selectedOptions = TestSite.Html5Page.MultiDropdown.GetSelectedOptions();
            Jdi.Assert.IsTrue(TestSite.Html5Page.MultiDropdown.OptionIsSelected(option));
            Jdi.Assert.IsTrue(selectedOptions.Contains(option));
        }

        [Test]
        public void SelectMultipleOptions()
        {
            var optionsList = new List<string> { "Steam", "Electro" };
            TestSite.Html5Page.MultiDropdown.SelectOptions(optionsList);
            Jdi.Assert.IsTrue(TestSite.Html5Page.MultiDropdown.OptionsAreSelected(optionsList));
        }

        [Test]
        public void CheckOptionExists()
        {
            TestSite.Html5Page.MultiDropdown.Expand();
            Jdi.Assert.IsTrue(TestSite.Html5Page.MultiDropdown.OptionExists("Steam"));
            Jdi.Assert.IsFalse(TestSite.Html5Page.MultiDropdown.OptionExists("Steam2"));
        }

        [Test]
        public void CheckOptionIsDisabled()
        {
            TestSite.Html5Page.MultiDropdown.Expand();
            Jdi.Assert.IsFalse(TestSite.Html5Page.MultiDropdown.OptionIsEnabled("Disabled"));
            Jdi.Assert.IsTrue(TestSite.Html5Page.MultiDropdown.OptionIsEnabled("Wood"));
        }

        [Test]
        public void LabelTest()
        {
            Jdi.Assert.AreEquals(TestSite.Html5Page.MultiDropdown.Label().GetText(), "Multi dropdown:");
            TestSite.Html5Page.MultiDropdown.Label().Is.Text(ContainsString("Multi"));
        }

        [Test]
        public void IsValidationTest()
        {
            TestSite.Html5Page.MultiDropdown.SelectOptions(new List<string> { "Steam" });
            TestSite.Html5Page.MultiDropdown.Is.Selected("Steam");
            TestSite.Html5Page.MultiDropdown.Is.Selected(Ages.Steam);
            TestSite.Html5Page.MultiDropdown.Is.Values(HasItems( new []{ "Wood" }));
            TestSite.Html5Page.MultiDropdown.Is.Disabled(HasItems(new[] { "Disabled" }));
            //TestSite.Html5Page.MultiDropdown.Is.Enabled(Not(HasItems(new[] { "Disabled" }))); TODO: // implement Matcher<T> Not(Matcher<T> matcher)
            TestSite.Html5Page.MultiDropdown.Is.Enabled(HasItems( new []{ "Electro", "Metalic" }));
        }

        [Test]
        public void AssertValidationTest()
        {
            TestSite.Html5Page.MultiDropdown.SelectOptions(new List<string> { "Steam" });
            TestSite.Html5Page.MultiDropdown.AssertThat.Values(ContainsInAnyOrder( new []{ "Disabled", "Electro", "Metalic", "Wood", "Steam" }));
        }

        [Test]
        public void BaseValidationTest()
        {
            BaseElementValidation(TestSite.Html5Page.MultiDropdown);
        }
    }
}
