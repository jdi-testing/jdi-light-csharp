using JDI.Core.Interfaces.Common;
using JDI.Core.Settings;
using JDI.Matchers.NUnit;
using JDI.UIWebTests.UIObjects;
using NUnit.Framework;

namespace JDI.UIWebTests.Tests.Common
{
    public class LinkTests
    {
        private ILink _link = TestSite.Footer.About;

        [SetUp]
        public void SetUp()
        {
            JDISettings.Logger.Info("Navigating to Metals and Colors page.");
            TestSite.HomePage.Open();
            TestSite.HomePage.CheckTitle();
            TestSite.HomePage.IsOpened();
            JDISettings.Logger.Info("Setup method finished");
            JDISettings.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
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
            new Check().AreEquals(_link.GetReference(), TestSite.SupportPage.Url);            
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
