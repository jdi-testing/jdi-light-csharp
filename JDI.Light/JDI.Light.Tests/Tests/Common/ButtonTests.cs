using JDI.Light.Interfaces.Common;
using JDI.Light.Settings;
using JDI.Light.Tests.Tests.Complex;
using JDI.Light.Tests.UIObjects;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Common
{
    public class ButtonTests
    {
        private readonly IButton _button = TestSite.MetalsColorsPage.CalculateButton;

        [SetUp]
        public void SetUp()
        {
            JDI.Logger.Info("Navigating to Metals and Colors page.");
            TestSite.MetalsColorsPage.Open();
            TestSite.MetalsColorsPage.CheckTitle();
            TestSite.MetalsColorsPage.IsOpened();
            JDI.Logger.Info("Setup method finished");
            JDI.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ClickTest()
        {
            _button.Click();
            CommonActionsData.CheckCalculate("Summary: 3");
        }
    }
}