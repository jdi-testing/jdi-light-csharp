using JDI.Light.Tests.DataProviders;
using NUnit.Framework;

namespace JDI.Light.Tests.UITests.Common
{
    [TestFixture]
    public class CheckBoxTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            Jdi.Logger.Info("Navigating to Metals and Colors page.");
            TestSite.MetalsColorsPage.Open();
            TestSite.MetalsColorsPage.CheckUrl();
            TestSite.MetalsColorsPage.CheckTitle();
            Jdi.Logger.Info("Setup method finished");
            Jdi.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void CheckSingleTest()
        {
            TestSite.MetalsColorsPage.CbWater.Check();
            CommonActionsData.CheckAction(TestSite,"Water: condition changed to true");
        }

        [Test]
        public void UncheckSingleTest()
        {
            TestSite.MetalsColorsPage.CbWater.Click();
            TestSite.MetalsColorsPage.CbWater.Uncheck();
            CommonActionsData.CheckAction(TestSite, "Water: condition changed to false");
        }

        [Test]
        public void IsCheckTest()
        {
            Assert.IsFalse(TestSite.MetalsColorsPage.CbWater.IsChecked);
            TestSite.MetalsColorsPage.CbWater.Click();
            Assert.IsTrue(TestSite.MetalsColorsPage.CbWater.IsChecked);
        }

        [Test]
        public void MultipleUncheckTest()
        {
            TestSite.MetalsColorsPage.CbWater.Click();
            TestSite.MetalsColorsPage.CbWater.Uncheck();
            TestSite.MetalsColorsPage.CbWater.Uncheck();
            CommonActionsData.CheckAction(TestSite, "Water: condition changed to false");
        }

        [Test]
        public void ClickTest()
        {
            TestSite.MetalsColorsPage.CbWater.Click();
            CommonActionsData.CheckAction(TestSite, "Water: condition changed to true");
            TestSite.MetalsColorsPage.CbWater.Click();
            CommonActionsData.CheckAction(TestSite, "Water: condition changed to false");
        }

        [Test]
        [TestCaseSource(typeof(CheckBoxProvider), nameof(CheckBoxProvider.InputData))]
        public void SetValueTest(bool value, bool expected)
        {
            if (!expected) TestSite.MetalsColorsPage.CbWater.Click();
            TestSite.MetalsColorsPage.CbWater.Value = value;
            var resultMsg = "Water: condition changed to " + expected.ToString().ToLower();
            CommonActionsData.CheckAction(TestSite, resultMsg);
        }
    }
}