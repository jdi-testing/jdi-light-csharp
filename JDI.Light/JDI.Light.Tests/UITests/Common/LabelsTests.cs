using JDI.Light.Tests.UIObjects;
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
            JDI.Logger.Info("Navigating to Metals and Colors page.");
            TestSite.MetalsColorsPage.CheckTitle();
            JDI.Logger.Info("Setup method finished");
        }

        [Test]
        public void CheckCalculate()
        {
            TestSite.MetalsColorsPage.CalculateButton.Click();
            JDI.Assert.Contains(TestSite.MetalsColorsPage.CalculateText.Value, "Summary: 3");
        }
    }
}