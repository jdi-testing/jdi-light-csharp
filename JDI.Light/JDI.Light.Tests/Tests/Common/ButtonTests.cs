using JDI.Core.Interfaces.Common;
using JDI.Core.Settings;
using JDI.UIWebTests.Tests.Complex;
using JDI.UIWebTests.UIObjects;
using NUnit.Framework;

namespace JDI.UIWebTests.Tests.Common
{
    public class ButtonTests
    {
        private readonly IButton _button = TestSite.MetalsColorsPage.CalculateButton;

        [SetUp]
        public void SetUp()
        {
            JDISettings.Logger.Info("Navigating to Metals and Colors page.");
            TestSite.MetalsColorsPage.Open();
            TestSite.MetalsColorsPage.CheckTitle();
            TestSite.MetalsColorsPage.IsOpened();
            JDISettings.Logger.Info("Setup method finished");
            JDISettings.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ClickTest()
        {
            _button.Click();
            CommonActionsData.CheckCalculate("Summary: 3");
        }
    }
}