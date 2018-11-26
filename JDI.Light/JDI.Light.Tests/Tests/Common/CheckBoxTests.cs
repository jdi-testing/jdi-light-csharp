using JDI.Light.Interfaces.Common;
using JDI.Light.Settings;
using JDI.Light.Tests.DataProviders;
using JDI.Light.Tests.Tests.Complex;
using JDI.Light.Tests.UIObjects;
using NUnit.Framework;
using Assert = JDI.Light.Tests.Asserts.Assert;

namespace JDI.Light.Tests.Tests.Common
{
    public class CheckBoxTests
    {
        private readonly ICheckBox _checkBoxWater = TestSite.MetalsColorsPage.CbWater;

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
            Assert.IsFalse(_checkBoxWater.IsChecked());
            _checkBoxWater.Click();
            Assert.IsTrue(_checkBoxWater.IsChecked());
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
        public void SetValueTest(string value, bool expected)
        {
            if (!expected) _checkBoxWater.Click();
            _checkBoxWater.Value = value;
            var resultMsg = "Water: condition changed to " + expected.ToString().ToLower();
            CommonActionsData.CheckAction(resultMsg);
        }

        [Test]
        [TestCaseSource(typeof(CheckBoxProvider), nameof(CheckBoxProvider.InputValue))]
        public void SetWrongValueTest(string value)
        {
            _checkBoxWater.Click();
            _checkBoxWater.Value = value;
            CommonActionsData.CheckAction("Water: condition changed to true");
            _checkBoxWater.Click();
            _checkBoxWater.Value = value;
            CommonActionsData.CheckAction("Water: condition changed to false");
        }
    }
}