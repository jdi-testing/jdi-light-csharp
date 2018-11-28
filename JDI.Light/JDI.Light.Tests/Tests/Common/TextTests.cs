using JDI.Light.Interfaces.Common;
using JDI.Light.Tests.Tests.Complex;
using JDI.Light.Tests.UIObjects;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    public class TextTests
    {
        [SetUp]
        public void SetUp()
        {
            JDI.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
            TestSite.HomePage.IsOpened();
            JDI.Logger.Info("Setup method finished");
        }

        private readonly IText _textItem = TestSite.HomePage.Text;

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
            JDI.Assert.AreEquals(_textItem.GetText, _expectedText);
        }

        [Test]
        public void GetValueTest()
        {
            JDI.Assert.AreEquals(_textItem.Value, _expectedText);
        }

        [Test]
        public void SetAttributeTest()
        {
            var attributeName = "testAttr";
            var value = "testValue";
            _textItem.SetAttribute(attributeName, value);
            CommonActionsData.CheckText(() => _textItem.GetAttribute(attributeName), value);
        }

        [Test]
        public void WaitMatchTest()
        {
            JDI.Assert.AreEquals(_textItem.WaitMatchText(_regEx), _expectedText);
        }

        [Test]
        public void WaitMatchTextParallelTest()
        {
            TestSite.SupportPage.IsOpened();
            CommonActionsData.RunParallel(() => TestSite.HomePage.IsOpened());
            JDI.Assert.AreEquals(_textItem.WaitMatchText(_regEx), _expectedText);
        }

        [Test]
        public void WaitText()
        {
            JDI.Assert.AreEquals(_textItem.WaitText(_contains), _expectedText);
        }

        [Test]
        public void WaitTextParallelTest()
        {
            TestSite.SupportPage.IsOpened();
            CommonActionsData.RunParallel(() => TestSite.HomePage.IsOpened());
            JDI.Assert.AreEquals(_textItem.WaitText(_contains), _expectedText);
        }
    }
}