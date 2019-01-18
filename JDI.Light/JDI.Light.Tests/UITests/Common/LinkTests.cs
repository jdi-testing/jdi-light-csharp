using JDI.Light.Interfaces.Common;
using JDI.Light.Tests.UIObjects;
using NUnit.Framework;

namespace JDI.Light.Tests.UITests.Common
{
    [TestFixture]
    public class LinkTests : TestBase
    {
        private readonly ILink _link = TestSite.Footer.About;

        [SetUp]
        public void SetUp()
        {
            Jdi.Logger.Info("Navigating to Metals and Colors page.");
            TestSite.HomePage.Open();
            TestSite.HomePage.CheckTitle();
            TestSite.HomePage.IsOpened();
            Jdi.Logger.Info("Setup method finished");
            Jdi.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ClickTest()
        {
            _link.Click();
            TestSite.SupportPage.IsOpened();
        }

        [Test]
        public void GetReferenceTest()
        {
            Jdi.Assert.AreEquals(_link.GetReference(), TestSite.SupportPage.Url);
        }

        /*
        //TO_DO
        [Test]
        public void GetURLTest() 
        {
            
        }

        //TO_DO
        [Test]
        public void WaitReferenceTest()
        {

        }

        //TO_DO
        [Test]
        public void WaitMatchReferenceTest() 
        {

        }
        */
    }
}