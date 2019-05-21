using System.Collections.Generic;
using System.Linq;
using JDI.Light.Exceptions;
using JDI.Light.Tests.Enums;
using NUnit.Framework;
using OpenQA.Selenium;
using static JDI.Light.Elements.Base.BaseValidation;
using static JDI.Light.Matchers.StringMatchers.ContainsStringMatcher;
using static JDI.Light.Matchers.StringMatchers.EqualToMatcher;
using static NUnit.Framework.Assert;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    public class DataListTests : TestBase
    {
        private const string Text = "Coconut";
        private readonly List<string> _options = new List<string>{ "Chocolate", "Coconut", "Mint", "Strawberry", "Vanilla" };

        [SetUp]
        public void SetUp()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
            DoesNotThrow(() => TestSite.Html5Page.IceCream.Select("Coconut"));
        }

        [Test]
        public void GetValueTest()
        {
            AreEqual(TestSite.Html5Page.IceCream.Selected(), "Coconut");
        }

        [Test]
        public void SelectTest()
        {
            TestSite.Html5Page.IceCream.Select("Chocolate");
            AreEqual(TestSite.Html5Page.IceCream.Selected(), "Chocolate");
        }

        [Test]
        public void SelectEnumTest()
        {
            TestSite.Html5Page.IceCream.Select(IceCream.Strawberry);
            AreEqual(TestSite.Html5Page.IceCream.Selected(), "Strawberry");
        }

        [Test]
        public void SelectNumTest()
        {
            TestSite.Html5Page.IceCream.Select(5);
            AreEqual(TestSite.Html5Page.IceCream.Selected(), "Vanilla");
        }

        [Test]
        public void LabelTest()
        {
            AreEqual(TestSite.Html5Page.IceCream.Label().GetText(), "Choose your lovely icecream");
            TestSite.Html5Page.IceCream.Label().Is.Text(ContainsString("lovely icecream"));
        }

        [Test]
        public void IsValidationTest()
        {
            TestSite.Html5Page.IceCream.Is.Enabled();
            TestSite.Html5Page.IceCream.Is.Attr("value" ,EqualTo(Text));
            TestSite.Html5Page.IceCream.Select("Vanilla");
            TestSite.Html5Page.IceCream.Is.Attr("value", ContainsString("Van"));
        }

        [Test]
        public void AssertValidationTest()
        {
            TestSite.Html5Page.IceCream.AssertThat.Attr("value", ContainsString(Text));
        }

        [Test]
        public void AssertOptionsValuesTest()
        {
            IsTrue(TestSite.Html5Page.IceCream.Values().SequenceEqual(_options));
        }

        [Test]
        public void NegativeSelectTest()
        {
            Throws<ElementNotSelectableException>(() => TestSite.Html5Page.DisabledDropdownAsDataList.Select("Fancy", false));
        }

        [Test]
        public void NegativeSelectEnumTest()
        {
            Throws<ElementNotSelectableException>(() => TestSite.Html5Page.DisabledDropdownAsDataList.Select(DressCode.Fancy, false));
        }

        [Test]
        public void NegativeSelectNumTest()
        {
            Throws<ElementNotFoundException>(() => TestSite.Html5Page.IceCream.Select(7, false));
        }

        [Test]
        public void BaseValidationTest()
        {
            BaseElementValidation(TestSite.Html5Page.IceCream);
        }
    }
}