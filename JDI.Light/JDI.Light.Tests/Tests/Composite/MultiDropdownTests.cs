using System;
using JDI.Light.Exceptions;
using JDI.Light.Matchers.CollectionMatchers;
using JDI.Light.Tests.Enums;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Composite
{
    [TestFixture]
    public class MultiDropdownTests : TestBase
    {       
        [SetUp]
        public void SetUp()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
            TestSite.Html5Page.MultiDropdown.Check("Steam");
        }

        [Test]
        public void GetValueTest()
        {
            Assert.AreEqual(TestSite.Html5Page.MultiDropdown.Selected(), "Steam");
        }

        [Test]
        public void SelectTest()
        {
            var toSelect = new[] { "Electro", "Metalic" };
            TestSite.Html5Page.MultiDropdown.Check(toSelect);
            Assert.AreEqual(TestSite.Html5Page.MultiDropdown.Checked(), toSelect);
        }

        [Test]
        public void SelectEnumTest()
        {
            var toSelect = new Enum[] { Ages.Wood, Ages.Steam };
            TestSite.Html5Page.MultiDropdown.Check(toSelect);
            Assert.AreEqual(TestSite.Html5Page.MultiDropdown.Checked(), new[] { "Steam", "Wood" });
        }

        [Test]
        public void SelectNumTest()
        {
            TestSite.Html5Page.MultiDropdown.Check(new[] { 1, 4 });
            Assert.AreEqual(TestSite.Html5Page.MultiDropdown.Checked(), new[] { "Steam", "Wood" });
        }

        [Test]
        public void SelectedTest()
        {
            Assert.AreEqual(TestSite.Html5Page.MultiDropdown.Checked(), new[] { "Steam" });
        }

        [Test]
        public void DisabledTest()
        {
            Assert.Throws<ElementDisabledException>(() => TestSite.Html5Page.MultiDropdown.Check("Disabled", true));
            Assert.AreEqual(TestSite.Html5Page.MultiDropdown.Checked(), "");

            Assert.DoesNotThrow(() => TestSite.Html5Page.MultiDropdown.Check("Disabled", false));
            Assert.AreEqual(TestSite.Html5Page.MultiDropdown.Checked(), "");
        }

        [Test]
        public void IsValidationTest()
        {
            TestSite.Html5Page.MultiDropdown.Is.Selected("Steam")
                .Selected(Ages.Steam.ToString())
                .Values(HasItemsMatcher<string>.HasItems(new[] {"Wood"}))
                .Disabled(HasItemsMatcher<string>.HasItems(new[] {"Disabled"}))
                .Enabled(HasItemsMatcher<string>.HasItems(new[] {"Electro", "Metalic"}));
        }

        [Test]
        public void AssertValidationTest()
        {
            TestSite.Html5Page.MultiDropdown.AssertThat
                .Values(ContainsInAnyOrderMatcher<string>.ContainsInAnyOrder(new[] {"Disabled", "Electro", "Metalic", "Wood", "Steam"}))
                .Selected(Ages.Steam.ToString());
        }
    }
}
