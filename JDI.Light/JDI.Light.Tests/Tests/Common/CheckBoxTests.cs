using JDI.Light.Interfaces.Common;
using JDI.Light.Tests.DataProviders;
using JDI.Light.Tests.Tests.Complex;
using JDI.Light.Tests.UIObjects;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Common
{
    public class CheckBoxTests
    {
        private readonly ICheckBox _checkBoxWater = TestSite.MetalsColorsPage.CbWater;

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
        public void CheckSingleTest()
        {
            _checkBoxWater.Check();
            CommonActionsData.CheckAction("Water: condition changed to true");
        }

        [Test]
        public void UncheckSingleTest()
        {
            _checkBoxWater.Click();
            _checkBoxWater.Uncheck();
            CommonActionsData.CheckAction("Water: condition changed to false");
        }

        [Test]
        public void IsCheckTest()
        {
            Assert.IsFalse(_checkBoxWater.IsChecked);
            _checkBoxWater.Click();
            Assert.IsTrue(_checkBoxWater.IsChecked);
        }

        [Test]
        public void MultipleUncheckTest()
        {
            _checkBoxWater.Click();
            _checkBoxWater.Uncheck();
            _checkBoxWater.Uncheck();
            CommonActionsData.CheckAction("Water: condition changed to false");
        }

        [Test]
        public void ClickTest()
        {
            _checkBoxWater.Click();
            CommonActionsData.CheckAction("Water: condition changed to true");
            _checkBoxWater.Click();
            CommonActionsData.CheckAction("Water: condition changed to false");
        }

        [Test]
        [TestCaseSource(typeof(CheckBoxProvider), nameof(CheckBoxProvider.InputData))]
        public void SetValueTest(bool value, bool expected)
        {
            if (!expected) _checkBoxWater.Click();
            _checkBoxWater.Value = value;
            var resultMsg = "Water: condition changed to " + expected.ToString().ToLower();
            CommonActionsData.CheckAction(resultMsg);
        }
    }
}