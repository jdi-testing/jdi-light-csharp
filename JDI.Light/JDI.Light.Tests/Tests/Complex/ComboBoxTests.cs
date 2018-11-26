using System;
using System.Collections.Generic;
using JDI.Core.Interfaces.Complex;
using JDI.Core.Settings;
using JDI.Light.Tests.Asserts;
using JDI.Light.Tests.Enums;
using JDI.Light.Tests.UIObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using Assert = JDI.Light.Tests.Asserts.Assert;

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
            JDISettings.Logger.Info("Navigating to Metals and Colors page.");
            TestSite.MetalsColorsPage.Open();
            TestSite.MetalsColorsPage.CheckTitle();
            TestSite.MetalsColorsPage.IsOpened();
            JDISettings.Logger.Info("Setup method finished");
            JDISettings.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
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
            new Check().CollectionEquals(MetalsControl.Options, OddOptions);
        }

        [Test]
        public void GetNamesTest()
        {
            new Check().CollectionEquals(MetalsControl.Names, OddOptions);
        }

        [Test]
        public void GetValuesTest()
        {
            new Check().CollectionEquals(MetalsControl.Values, OddOptions);
        }

        [Test]
        public void GetOptionsAsTextTest()
        {
            new Check().AreEquals(MetalsControl.OptionsAsText, "Col, Gold, Silver, Bronze, Selen");
        }

        [Test]
        public void SetValueTest()
        {
            MetalsControl.Value = "Blue";
            WebSettings.WebDriver.FindElement(By.ClassName("footer-content")).Click();
            CommonActionsData.CheckAction("Metals: value changed to Blue");
        }

        [Test]
        public void GetSelectedTest()
        {
            MetalsControl.Select("Gold");
            new Check().AreEquals(MetalsControl.Selected(), "Gold");
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
            Assert.AreEquals(MetalsControl.Selected("Col"), true);
        }

        [Test]
        public void IsSelectedEnumTest()
        {
            Assert.AreEquals(MetalsControl.Selected(Metals.Col), true);
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
                throw JDISettings.Exception("WaitSelected throws exception");
            }
        }

        [Test]
        public void WaitSelectedEnumTest()
        {
            new Check("WaitSelected").HasNoException(() => MetalsControl.GetValue());
        }

        [Test]
        public void GetValueTest()
        {
            Assert.AreEquals(MetalsControl.Value, "Col");
        }
    }
}