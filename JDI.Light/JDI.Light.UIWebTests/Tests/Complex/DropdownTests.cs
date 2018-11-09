using System.Collections.Generic;
using JDI.Core.Interfaces.Complex;
using JDI.Core.Settings;
using JDI.Matchers.NUnit;
using JDI.UIWebTests.Enums;
using JDI.UIWebTests.UIObjects;
using NUnit.Framework;
using Assert = JDI.Matchers.NUnit.Assert;

namespace JDI.UIWebTests.Tests.Complex
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
            Assert.CollectionEquals(ColorsControl.Options, OddOptions);
        }

        [Test]
        public void GetNamesTest()
        {
            Assert.CollectionEquals(ColorsControl.Names, OddOptions);
        }

        [Test]
        public void GetValuesTest()
        {
            Assert.CollectionEquals(ColorsControl.Values, OddOptions);
        }

        [Test]
        public void GetOptionsAsTextTest()
        {
            Assert.AreEquals(ColorsControl.OptionsAsText, "Colors, Red, Green, Blue, Yellow");
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
            Assert.AreEquals(ColorsControl.Name, "Colors");
        }

        [Test]
        public void GetSelectedTest()
        {
            Assert.AreEquals(ColorsControl.Selected(), "Colors");
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
            Assert.AreEquals(ColorsControl.Selected("Colors"), true);
        }

        [Test]
        public void IsSelectedEnumTest()
        {
            Assert.AreEquals(ColorsControl.Selected(Colors.Colors), true);
        }

        [Test]
        public void WaitSelectedTest()
        {
            new Check("WaitSelected").HasNoException(() => ColorsControl.WaitSelected("Colors"));
        }

        [Test]
        public void WaitSelectedEnumTest()
        {
            new Check("WaitSelected").HasNoException(() => ColorsControl.WaitSelected(Colors.Colors));
        }

        [Test]
        public void GetValueTest()
        {
            Assert.AreEquals(ColorsControl.Value, "Colors");
        }
    }
}