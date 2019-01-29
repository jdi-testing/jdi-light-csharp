using JDI.Light.Interfaces.Common;
using NUnit.Framework;

namespace JDI.Light.Tests.UITests.Common
{
    [TestFixture]
    public class TextTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            Jdi.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
            TestSite.HomePage.IsOpened();
            Jdi.Logger.Info("Setup method finished");
        }

        private IText TextItem => TestSite.HomePage.Text;

        private readonly string _expectedText = ("Lorem ipsum dolor sit amet, consectetur adipisicing elit,"
                                                 + " sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
                                                 + " Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris"
                                                 + " nisi ut aliquip ex ea commodo consequat Duis aute irure dolor in"
                                                 + " reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur."
            ).ToUpper();

        private readonly string _regEx = ".* IPSUM DOLOR SIT AMET.*";
        private readonly string _contains = "ENIM AD MINIM VENIAM, QUIS NOSTRUD";

        [Test]
        public void GetTextTest()
        {
            Jdi.Assert.AreEquals(TextItem.Value, _expectedText);
        }

        [Test]
        public void GetValueTest()
        {
            Jdi.Assert.AreEquals(TextItem.Value, _expectedText);
        }

        [Test]
        public void SetAttributeTest()
        {
            var attributeName = "testAttr";
            var value = "testValue";
            TextItem.SetAttribute(attributeName, value);
            CommonActionsData.CheckText(() => TextItem.GetAttribute(attributeName), value);
        }

        [Test]
        public void WaitMatchTest()
        {
            Jdi.Assert.AreEquals(TextItem.WaitMatchText(_regEx), _expectedText);
        }

        [Test]
        public void WaitMatchTextParallelTest()
        {
            TestSite.SupportPage.IsOpened();
            CommonActionsData.RunParallel(() => TestSite.HomePage.IsOpened());
            Jdi.Assert.AreEquals(TextItem.WaitMatchText(_regEx), _expectedText);
        }

        [Test]
        public void WaitText()
        {
            Jdi.Assert.AreEquals(TextItem.WaitText(_contains), _expectedText);
        }

        [Test]
        public void WaitTextParallelTest()
        {
            TestSite.SupportPage.IsOpened();
            CommonActionsData.RunParallel(() => TestSite.HomePage.IsOpened());
            Jdi.Assert.AreEquals(TextItem.WaitText(_contains), _expectedText);
        }
    }
}