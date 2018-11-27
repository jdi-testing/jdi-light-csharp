using System.Collections.Generic;
using JDI.Light.Interfaces.Complex;
using JDI.Light.Settings;
using JDI.Light.Tests.Asserts;
using JDI.Light.Tests.Enums;
using JDI.Light.Tests.UIObjects;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Complex
{
    public class DropdownExpandedTests
    {
        private static readonly IList<string> ColorsOptions = new List<string>
            {"Colors", "Red", "Green", "Blue", "Yellow"};

        private IDropDown<Colors> _colors()
        {
            return TestSite.MetalsColorsPage.Colors;
        }

        [SetUp]
        public void SetUp()
        {
            JDISettings.Logger.Info("Navigating to Metals and Colors page.");
            TestSite.MetalsColorsPage.Open();
            TestSite.MetalsColorsPage.CheckTitle();
            TestSite.MetalsColorsPage.IsOpened();
            _colors().Expand();
            JDISettings.Logger.Info("Setup method finished");
            JDISettings.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void SelectStringTest()
        {
            _colors().Select("Blue");
            CommonActionsData.CheckAction("Colors: value changed to Blue");
        }

        [Test]
        public void SelectIndexTest()
        {
            _colors().Select(4);
            CommonActionsData.CheckAction("Colors: value changed to Blue");
        }

        [Test]
        public void SelectEnumTest()
        {
            _colors().Select(Colors.Blue);
            CommonActionsData.CheckAction("Colors: value changed to Blue");
        }

        [Test]
        public void GetOptionsTest()
        {
            new NUnitAsserter().CollectionEquals(_colors().Options, ColorsOptions);
        }

        [Test]
        public void GetNamesTest()
        {
            new NUnitAsserter().CollectionEquals(_colors().Names, ColorsOptions);
        }

        [Test]
        public void GetValuesTest()
        {
            new NUnitAsserter().CollectionEquals(_colors().Values, ColorsOptions);
        }

        [Test]
        public void GetOptionsAsTextTest()
        {
            new NUnitAsserter().AreEquals(_colors().OptionsAsText, "Colors, Red, Green, Blue, Yellow");
        }

        [Test]
        public void SetValueTest()
        {
            _colors().Value = "Blue";
            CommonActionsData.CheckAction("Colors: value changed to Blue");
        }

        [Test]
        public void GetNameTest()
        {
            new NUnitAsserter().AreEquals(_colors().Name, "Colors");
        }

        [Test]
        public void GetSelectedTest()
        {
            new NUnitAsserter().AreEquals(_colors().Selected(), "Colors");
        }

        [Test]
        public void GetValueTest()
        {
            new NUnitAsserter().AreEquals(_colors().Value, "Colors");
        }
    }
}