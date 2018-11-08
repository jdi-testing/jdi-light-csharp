using JDI.Core.Interfaces.Common;
using JDI.Core.Settings;
using JDI.UIWebTests.Tests.Complex;
using JDI.UIWebTests.UIObjects;
using NUnit.Framework;
using Assert = JDI.Matchers.NUnit.Assert;

namespace JDI.UIWebTests.Tests.Common
{
    [TestFixture]
    public class TextTests
    {
        private IText _textItem = TestSite.HomePage.Text;
        private string _expectedText = ("Lorem ipsum dolor sit amet, consectetur adipisicing elit,"
            + " sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
            + " Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris"
            + " nisi ut aliquip ex ea commodo consequat Duis aute irure dolor in"
            + " reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.").ToUpper();
        private string _regEx = ".* IPSUM DOLOR SIT AMET.*";
        private string _contains = "ENIM AD MINIM VENIAM, QUIS NOSTRUD";

        [SetUp]
        public void SetUp()
        {
            JDISettings.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
            TestSite.HomePage.IsOpened();
            JDISettings.Logger.Info("Setup method finished");
        }

        [Test]
        public void GetTextTest() {
            Assert.AreEquals(_textItem.GetText, _expectedText);
        }

        [Test]
        public void GetValueTest()
        {
            Assert.AreEquals(_textItem.Value, _expectedText);
        }
        
        [Test]
        public void WaitMatchTest()
        {
            Assert.AreEquals(_textItem.WaitMatchText(_regEx), _expectedText);
        }

        [Test]
        public void WaitMatchTextParallelTest()
        {
            TestSite.SupportPage.IsOpened();
            CommonActionsData.RunParallel(() => TestSite.HomePage.IsOpened());
            Assert.AreEquals(_textItem.WaitMatchText(_regEx), _expectedText);
        }

        [Test]
        public void WaitText()
        {
            Assert.AreEquals(_textItem.WaitText(_contains), _expectedText);
        }

        [Test]
        public void WaitTextParallelTest()
        {
            TestSite.SupportPage.IsOpened();
            CommonActionsData.RunParallel(() => TestSite.HomePage.IsOpened());
            Assert.AreEquals(_textItem.WaitText(_contains), _expectedText);
        }

        [Test]
        public void SetAttributeTest()
        {
            string attributeName = "testAttr";
            string value = "testValue";
            _textItem.SetAttribute(attributeName, value);
            CommonActionsData.CheckText(() => _textItem.GetAttribute(attributeName), value);
        }
    }
}
