﻿using JDI.Core.Settings;
using JDI.Light.Tests.Tests.Complex;
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
            JDISettings.Logger.Info("Navigating to Metals and Colors page.");
            TestSite.MetalsColorsPage.CheckTitle();
            JDISettings.Logger.Info("Setup method finished");
        }

        [Test]
        public void CheckCalculate()
        {
            TestSite.MetalsColorsPage.CalculateButton.Click();
            CommonActionsData.CheckCalculate("Summary: 3");
        }
    }
}