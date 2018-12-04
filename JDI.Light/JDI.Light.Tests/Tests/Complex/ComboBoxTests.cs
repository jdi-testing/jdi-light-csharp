using System;
using System.Collections.Generic;
using JDI.Light.Interfaces.Complex;
using JDI.Light.Tests.Asserts;
using JDI.Light.Tests.Enums;
using JDI.Light.Tests.UIObjects;
using NUnit.Framework;
using OpenQA.Selenium;

namespace JDI.Light.Tests.Tests.Complex
{
    public class ComboBoxTests
    {
        private static readonly IList<string> OddOptions = new List<string>
            {"Col", "Gold", "Silver", "Bronze", "Selen"};

        private IComboBox<Metals> MetalsControl => TestSite.MetalsColorsPage.ComboBox;

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
        public void SelectStringTest()
        {
            MetalsControl.Select("Gold");
            CommonActionsData.CheckAction("Metals: value changed to Gold");
        }

        [Test]
        public void SelectIndexTest()
        {
            MetalsControl.Select(3);
            CommonActionsData.CheckAction("Metals: value changed to Silver");
        }

        [Test]
        public void SelectEnumTest()
        {
            MetalsControl.Select(Metals.Gold);
            CommonActionsData.CheckAction("Metals: value changed to Gold");
        }

        [Test]
        public void GetOptionsTest()
        {
            JDI.Assert.CollectionEquals(MetalsControl.Options, OddOptions);
        }

        [Test]
        public void GetNamesTest()
        {
            JDI.Assert.CollectionEquals(MetalsControl.Names, OddOptions);
        }

        [Test]
        public void GetValuesTest()
        {
            JDI.Assert.CollectionEquals(MetalsControl.Values, OddOptions);
        }

        [Test]
        public void GetOptionsAsTextTest()
        {
            JDI.Assert.AreEquals(MetalsControl.OptionsAsText, "Col, Gold, Silver, Bronze, Selen");
        }

        [Test]
        public void SetValueTest()
        {
            MetalsControl.Value = "Blue";
            JDI.DriverFactory.GetDriver().FindElement(By.ClassName("footer-content")).Click();
            CommonActionsData.CheckAction("Metals: value changed to Blue");
        }

        [Test]
        public void GetSelectedTest()
        {
            MetalsControl.Select("Gold");
            JDI.Assert.AreEquals(MetalsControl.Selected(), "Gold");
        }

        [Test]
        public void GetSelectedIndexTest()
        {
            CommonActionsData.CheckActionThrowError(() => MetalsControl.SelectedIndex(),
                CommonActionsData.NoElementsMessage);
        }

        [Test]
        public void IsSelectedTest()
        {
            JDI.Assert.AreEquals(MetalsControl.Selected("Col"), true);
        }

        [Test]
        public void IsSelectedEnumTest()
        {
            JDI.Assert.AreEquals(MetalsControl.Selected(Metals.Col), true);
        }

        [Test]
        public void WaitSelectedTest()
        {
            try
            {
                MetalsControl.WaitSelected("Col");
            }
            catch (Exception)
            {
                throw JDI.Assert.Exception("WaitSelected throws exception");
            }
        }

        [Test]
        public void WaitSelectedEnumTest()
        {
            new NUnitAsserter("WaitSelected").HasNoException(() => MetalsControl.GetValue());
        }

        [Test]
        public void GetValueTest()
        {
            JDI.Assert.AreEquals(MetalsControl.Value, "Col");
        }
    }
}