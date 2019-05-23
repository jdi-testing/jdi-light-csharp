using JDI.Light.Tests.Enums;
using NUnit.Framework;
using JDI.Light.Exceptions;
using static JDI.Light.Elements.Base.BaseValidation;
using static JDI.Light.Matchers.CollectionMatchers.ContainsInAnyOrderMatcher<string>;
using static JDI.Light.Matchers.CollectionMatchers.HasItemsMatcher<string>;
using static JDI.Light.Matchers.StringMatchers.ContainsStringMatcher;
using static NUnit.Framework.Assert;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    public class DropDownTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
            TestSite.Html5Page.DressCode.Select("Casual");
        }

        [Test]
        public void GetValueTest()
        {
            AreEqual(TestSite.Html5Page.DressCode.GetSelected(), "Casual");
        }

        [Test]
        public void SelectTest()
        {
            TestSite.Html5Page.DressCode.Select("Pirate");
            AreEqual(TestSite.Html5Page.DressCode.GetSelected(), "Pirate");
        }

        [Test]
        public void SelectEnumTest()
        {
            TestSite.Html5Page.DressCode.Select(DressCode.Fancy);
            AreEqual(TestSite.Html5Page.DressCode.GetSelected(), "Fancy");
        }

        [Test]
        public void SelectNumTest()
        {
            TestSite.Html5Page.DressCode.Select(1);
            AreEqual(TestSite.Html5Page.DressCode.GetSelected(), "Fancy");
        }

        [Test]
        public void BigDropDownTest()
        {
            TestSite.PerformancePage.Open();
            TestSite.PerformancePage.UserNames.Select("Charles Byers");

            var selected = TestSite.PerformancePage.UserNames.GetSelected();
            AreEqual(selected, "Charles Byers");
        }

        [Test]
        public void DisabledTest()
        {
            Throws<ElementDisabledException>(() => TestSite.Html5Page.DisabledDropdown.Select("Pirate", true));
            AreEqual(TestSite.Html5Page.DisabledDropdown.GetSelected(), "Disabled");

            DoesNotThrow(() => TestSite.Html5Page.DisabledDropdown.Select("Disabled", false));
        }

        [Test]
        public void LabelTest()
        {
            AreEqual(TestSite.Html5Page.DressCode.Label().GetText(), "Dress code:");
            TestSite.Html5Page.DressCode.Label().Is.Text(ContainsString("Dress"));
        }

        [Test]
        public void IsValidationTest()
        {
            TestSite.Html5Page.DressCode.Is.Selected("Casual");
            TestSite.Html5Page.DressCode.Is.Selected(DressCode.Casual);
            TestSite.Html5Page.DressCode.Is.Values(HasItems(new[] { "Pirate" }));
            TestSite.Html5Page.DressCode.Is.Disabled(HasItems(new[] { "Disabled" }));
            //TestSite.Html5Page.DressCode.Is.Disabled(Not(HasItems(new[] { "Disabled" }))); TODO: implement Matcher<T> Not(Matcher<T> matcher)
            TestSite.Html5Page.DressCode.Is.Enabled(HasItems(new[] { "Pirate", "Fancy" }));
        }

        [Test]
        public void AssertValidationTest()
        {
            TestSite.Html5Page.DressCode.AssertThat.Values(
                ContainsInAnyOrder(new[] {"Fancy", "Casual", "Disabled", "Pirate"}));
        }

        [Test]
        public void BaseValidationTest()
        {
            BaseElementValidation(TestSite.Html5Page.DressCode);
        }
    }
}