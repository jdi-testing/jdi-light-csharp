using JDI.Light.Interfaces.Common;
using JDI.Light.Tests.UIObjects;

namespace JDI.Light.Tests.Tests.Common
{
    public class LinkTests
    {
        private readonly ILink _link = TestSite.Footer.About;

        [SetUp]
        public void SetUp()
        {
            JDI.Logger.Info("Navigating to Metals and Colors page.");
            TestSite.HomePage.Open();
            TestSite.HomePage.CheckTitle();
            TestSite.HomePage.IsOpened();
            JDI.Logger.Info("Setup method finished");
            JDI.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
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
            JDI.Assert.AreEquals(_link.GetReference(), TestSite.SupportPage.Url);
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