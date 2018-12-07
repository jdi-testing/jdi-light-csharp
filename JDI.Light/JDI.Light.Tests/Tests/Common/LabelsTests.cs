using JDI.Light.Tests.UIObjects;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    public class LabelsTests
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
            CommonActionsData.CheckCalculate("Summary: 3");
        }
    }
}