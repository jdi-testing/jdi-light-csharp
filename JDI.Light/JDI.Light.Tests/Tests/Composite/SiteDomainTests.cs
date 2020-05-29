using System;
using JDI.Light.Tests.UIObjects;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Composite
{
    [TestFixture]
    public class SiteDomainTests : TestBase
    {
        public TestSiteCustomDomain TestSiteCustomDomain { get; set; }

        [SetUp]
        public override void SetUpTest()
        {
            TestSiteCustomDomain = Jdi.InitSite<TestSiteCustomDomain>();
            Jdi.Logger.Info("Run test...");
        }

        [Test]
        public void TestDomain()
        {
            TestSiteCustomDomain.HomePage.Open();
            Jdi.Logger.Info("Custom domain worked!");
        }

        [Test]
        public void TestBrokenDomain()
        {
            Assert.Throws<MissingMethodException>(() =>
            {
                Jdi.InitSite<TestSiteBrokenDomain>();
            });
        }

        [TearDown]
        public override void TestTearDown()
        {
            Jdi.Logger.Info("Run test tear down done.");
        }
    }
}