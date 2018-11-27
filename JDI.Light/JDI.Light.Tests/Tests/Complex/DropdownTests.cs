using System.Collections.Generic;
using JDI.Light.Interfaces.Complex;
using JDI.Light.Settings;
using JDI.Light.Tests.Asserts;
using JDI.Light.Tests.Enums;
using JDI.Light.Tests.UIObjects;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Complex
{
    public class DropdownTests
    {
        private static readonly List<string> OddOptions = new List<string> {"Colors", "Red", "Green", "Blue", "Yellow"};
        private IDropDown<Colors> ColorsControl => TestSite.MetalsColorsPage.Colors;

        [SetUp]
        public void Setup()
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
            ColorsControl.Select("Blue");
            CommonActionsData.CheckAction("Colors: value changed to Blue");
        }

        [Test]
        public void SelectIndexTest()
        {
            ColorsControl.Select(4);
            CommonActionsData.CheckAction("Colors: value changed to Blue");
        }

        [Test]
        public void SelectEnumTest()
        {
            ColorsControl.Select(Colors.Blue);
            CommonActionsData.CheckAction("Colors: value changed to Blue");
        }

        [Test]
        public void GetOptionsTest()
        {
            JDISettings.Assert.CollectionEquals(ColorsControl.Options, OddOptions);
        }

        [Test]
        public void GetNamesTest()
        {
            JDISettings.Assert.CollectionEquals(ColorsControl.Names, OddOptions);
        }

        [Test]
        public void GetValuesTest()
        {
            JDISettings.Assert.CollectionEquals(ColorsControl.Values, OddOptions);
        }

        [Test]
        public void GetOptionsAsTextTest()
        {
            JDISettings.Assert.AreEquals(ColorsControl.OptionsAsText, "Colors, Red, Green, Blue, Yellow");
        }

        [Test]
        public void SetValueTest()
        {
            ColorsControl.Value = "Blue";
            CommonActionsData.CheckAction("Colors: value changed to Blue");
        }

        [Test]
        public void GetNameTest()
        {
            JDISettings.Assert.AreEquals(ColorsControl.Name, "Colors");
        }

        [Test]
        public void GetSelectedTest()
        {
            JDISettings.Assert.AreEquals(ColorsControl.Selected(), "Colors");
        }

        [Test]
        public void GetSelectedIndexTest()
        {
            CommonActionsData.CheckActionThrowError(() => ColorsControl.SelectedIndex(),
                CommonActionsData.NoElementsMessage);
        }

        [Test]
        public void IsSelectedTest()
        {
            JDISettings.Assert.AreEquals(ColorsControl.Selected("Colors"), true);
        }

        [Test]
        public void IsSelectedEnumTest()
        {
            JDISettings.Assert.AreEquals(ColorsControl.Selected(Colors.Colors), true);
        }

        [Test]
        public void WaitSelectedTest()
        {
            new NUnitAsserter("WaitSelected").HasNoException(() => ColorsControl.WaitSelected("Colors"));
        }

        [Test]
        public void WaitSelectedEnumTest()
        {
            new NUnitAsserter("WaitSelected").HasNoException(() => ColorsControl.WaitSelected(Colors.Colors));
        }

        [Test]
        public void GetValueTest()
        {
            JDISettings.Assert.AreEquals(ColorsControl.Value, "Colors");
        }
    }
}