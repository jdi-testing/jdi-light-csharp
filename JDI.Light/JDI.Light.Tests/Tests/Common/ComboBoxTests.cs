using JDI.Light.Tests.Enums;
using NUnit.Framework;
using Is = JDI.Light.Matchers.Is;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    public class ComboBoxTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
            TestSite.Html5Page.IceCreamComboBox.Select("Coconut");
        }

        [Test]
        public void GetValueTest()
        {
            Jdi.Assert.AreEquals(TestSite.Html5Page.IceCreamComboBox.Selected(), "Coconut");
        }

        [Test]
        public void SelectTest()
        {
            TestSite.Html5Page.IceCream.Select("Chocolate");
            Jdi.Assert.AreEquals(TestSite.Html5Page.IceCreamComboBox.Selected(), "Chocolate");
        }

        [Test]
        public void SelectEnumTest()
        {
            TestSite.Html5Page.IceCream.Select(IceCream.Strawberry);
            Jdi.Assert.AreEquals(TestSite.Html5Page.IceCreamComboBox.Selected(), "Strawberry");            
        }

        [Test]
        public void SelectNumTest()
        {
            TestSite.Html5Page.IceCream.Select(5);
            Jdi.Assert.AreEquals(TestSite.Html5Page.IceCreamComboBox.Selected(), "Vanilla");
        }
    }
}