using NUnit.Framework;
using static JDI.Light.Tests.UIObjects.TestSite;

namespace JDI.Light.Tests.Tests.Common
{
    public class ButtonTests
    {
        [SetUp]
        public void SetUp()
        {
            JDI.Logger.Info("Navigating to Metals and Colors page.");
            MetalsColorsPage.Open();
            MetalsColorsPage.CheckTitle();
            MetalsColorsPage.IsOpened();
            JDI.Logger.Info("Setup method finished");
            JDI.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ClickTest()
        {
            MetalsColorsPage.CalculateButton.Click();
            var calcText = MetalsColorsPage.CalculateText.Value;
            JDI.Assert.Contains(calcText, "Summary: 3");
        }
    }
}