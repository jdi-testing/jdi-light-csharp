using JDI.Core.Settings;
using JDI.UIWebTests.Tests.Complex;
using JDI.UIWebTests.UIObjects;
using NUnit.Framework;

namespace JDI.UIWebTests.Tests.Common
{
    [TestFixture]
    public class LabelsTests
    {
               

        [SetUp]
        public void SetUp() {                    
            TestSite.MetalsColorsPage.Open();
            JDISettings.Logger.Info("Navigating to Metals and Colors page.");
            TestSite.MetalsColorsPage.CheckTitle();
            JDISettings.Logger.Info("Setup method finished");

        }

        [Test]
        public void CheckCalculate() {
            TestSite.MetalsColorsPage.CalculateButton.Click();
            CommonActionsData.CheckCalculate("Summary: 3");
        }
    }
}
