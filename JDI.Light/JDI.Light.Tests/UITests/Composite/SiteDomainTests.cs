using JDI.Light.Tests.UIObjects;
using NUnit.Framework;

namespace JDI.Light.Tests.UITests.Composite
{
    [TestFixture]
    public class SiteDomainTests : TestBase
    {
        [SetUp]
        public override void SetUpTest()
        {
            //TODO:
            //Jdi.InitSite(typeof(TestSiteCustomDomain));
            Jdi.Logger.Info("Run test...");
        }

        [Test]
        public void TestDomain()
        {
            TestSiteCustomDomain.HomePage.Open();
            Jdi.Logger.Info("Custom domain worked!");
        }
    }
}