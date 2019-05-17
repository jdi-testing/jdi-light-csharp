using JDI.Light.Tests.Enums;
using NUnit.Framework;
using static JDI.Light.Elements.Base.BaseValidation;
using static JDI.Light.Matchers.StringMatchers.ContainsStringMatcher;
using static JDI.Light.Matchers.StringMatchers.EqualToMatcher;
using static NUnit.Framework.Assert;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    public class DataListTests : TestBase
    {
        private readonly string _text = "Coconut";

        [SetUp]
        public void SetUp()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
            DoesNotThrow(() => TestSite.Html5Page.IceCream.Select("Coconut", true));
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
            TestSite.Html5Page.IceCream.Is.Attr("value" ,EqualTo(_text));
            TestSite.Html5Page.IceCream.Select("Vanilla");
            TestSite.Html5Page.IceCream.Is.Attr("value", ContainsString("Van"));
        }

        [Test]
        public void AssertValidationTest()
        {
            TestSite.Html5Page.IceCream.AssertThat.Attr("value", ContainsString(_text));
        }

        [Test]
        public void BaseValidationTest()
        {
            BaseElementValidation(TestSite.Html5Page.IceCream);
        }
    }
}