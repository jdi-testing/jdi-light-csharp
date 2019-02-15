using NUnit.Framework;
using static JDI.Light.Jdi;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    public class ButtonTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            Logger.Info("Navigating to Metals and Colors page.");
            TestSite.MetalsColorsPage.Open();
            TestSite.MetalsColorsPage.CheckTitle();
            Logger.Info("Setup method finished");
            Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ClickTest()
        {
            TestSite.MetalsColorsPage.CalculateButton.Click();
            var calcText = TestSite.MetalsColorsPage.CalculateText.Value;
            Jdi.Assert.Contains(calcText, "Summary: 3");
        }
    }
}