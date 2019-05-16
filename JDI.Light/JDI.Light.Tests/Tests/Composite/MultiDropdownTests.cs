using System.Collections.Generic;
using NUnit.Framework;
using static JDI.Light.Jdi;

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
    }
}
