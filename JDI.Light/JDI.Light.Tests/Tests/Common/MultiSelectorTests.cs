using System;
using JDI.Light.Exceptions;
using JDI.Light.Tests.Enums;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    public class MultiSelectorTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
            TestSite.Html5Page.Ages.Check("Steam");
        }

        [Test]
        public void GetValueTest()
        {
            Assert.AreEqual(TestSite.Html5Page.Ages.Selected(), "Steam");
        }

        [Test]
        public void SelectTest()
        {
            var toSelect = new[] { "Electro", "Metalic" };
            TestSite.Html5Page.Ages.Check(toSelect);
            Assert.AreEqual(TestSite.Html5Page.Ages.Checked(), toSelect);
        }

        [Test]
        public void SelectEnumTest()
        {
            var toSelect = new Enum[] { Ages.Wood, Ages.Steam };
            TestSite.Html5Page.Ages.Check(toSelect);
            Assert.AreEqual(TestSite.Html5Page.Ages.Checked(), new[] { "Steam", "Wood" });
        }

        [Test]
        public void SelectNumTest()
        {
            TestSite.Html5Page.Ages.Check(new[] { 1, 4 });
            Assert.AreEqual(TestSite.Html5Page.Ages.Checked(), new[] { "Steam", "Wood" });
        }

        [Test]
        public void SelectedTest()
        {
            Assert.AreEqual(TestSite.Html5Page.Ages.Checked(), new[] { "Steam" });
        }

        [Test]
        public void DisabledTest()
        {
            Assert.Throws<ElementDisabledException>(() => TestSite.Html5Page.Ages.Check("Disabled", true));
            Assert.AreEqual(TestSite.Html5Page.Ages.Checked(), "");

            Assert.DoesNotThrow(() => TestSite.Html5Page.Ages.Check("Disabled", false));
            Assert.AreEqual(TestSite.Html5Page.Ages.Checked(), "");
        }
    }
}