using NUnit.Framework;

namespace JDI.Light.Tests.UITests.Common
{
    [TestFixture]
    public class LabelsTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            TestSite.MetalsColorsPage.Open();
            Jdi.Logger.Info("Navigating to Metals and Colors page.");
            TestSite.MetalsColorsPage.CheckTitle();
            Jdi.Logger.Info("Setup method finished");
        }

        [Test]
        public void CheckCalculate()
        {
            TestSite.MetalsColorsPage.CalculateButton.Click();
            Jdi.Assert.Contains(TestSite.MetalsColorsPage.CalculateText.Value, "Summary: 3");
        }
    }
}