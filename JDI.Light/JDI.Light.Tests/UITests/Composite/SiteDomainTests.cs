using JDI.Light.Tests.UIObjects;
using NUnit.Framework;

namespace JDI.Light.Tests.UITests.Composite
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

        [TearDown]
        public override void TestTearDown()
        {
        }
    }
}