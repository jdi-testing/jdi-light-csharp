using JDI.Light.Tests.Enums;
using NUnit.Framework;
using System;
using JDI.Light.Exceptions;

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
            Assert.AreEqual(TestSite.Html5Page.DressCode.GetSelected(), "Casual");
        }

        [Test]
        public void SelectTest()
        {
            TestSite.Html5Page.DressCode.Select("Pirate");
            Assert.AreEqual(TestSite.Html5Page.DressCode.GetSelected(), "Pirate");
        }

        [Test]
        public void SelectEnumTest()
        {
            TestSite.Html5Page.DressCode.Select(DressCode.Fancy);
            Assert.AreEqual(TestSite.Html5Page.DressCode.GetSelected(), "Fancy");
        }

        [Test]
        public void SelectNumTest()
        {
            TestSite.Html5Page.DressCode.Select(1);
            Assert.AreEqual(TestSite.Html5Page.DressCode.GetSelected(), "Fancy");
        }

        [Test]
        public void BigDropDownTest()
        {
            TestSite.PerformancePage.Open();
            TestSite.PerformancePage.UserNames.Select("Charles Byers");

            var selected = TestSite.PerformancePage.UserNames.GetSelected();
            Assert.AreEqual(selected, "Charles Byers");
        }

        [Test]
        public void DisabledTest()
        {
            Assert.Throws<ElementDisabledException>(() => TestSite.Html5Page.DisabledDropdown.Select("Pirate", true));
            Assert.AreEqual(TestSite.Html5Page.DisabledDropdown.GetSelected(), "Disabled");

            Assert.DoesNotThrow(() => TestSite.Html5Page.DisabledDropdown.Select("Disabled", false));
        }
    }
}