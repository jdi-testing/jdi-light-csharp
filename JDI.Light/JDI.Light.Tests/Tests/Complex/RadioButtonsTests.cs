using System.Collections.Generic;
using JDI.Light.Selenium.Elements.Complex;
using JDI.Light.Tests.Enums;
using JDI.Light.Tests.UIObjects;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Complex
{
    public class RadioButtonsTests
    {
        private static readonly IList<string> OddOptions = new List<string> {"1", "3", "5", "7"};
        private RadioButtons<Odds> OddNumbersControl => TestSite.MetalsColorsPage.SummaryBlock.OddNumbers;

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
            OddNumbersControl.Select("7");
            CommonActionsData.CheckAction("Summary (Odd): value changed to 7");
        }

        [Test]
        public void SelectIndexTest()
        {
            OddNumbersControl.Select(4);
            CommonActionsData.CheckAction("Summary (Odd): value changed to 7");
        }

        [Test]
        [Ignore("C# enum does not support strings")]
        public void SelectEnumTest()
        {
            // Select method is waiting for string "7" not "Seven"
            OddNumbersControl.Select(Odds.Seven);
            CommonActionsData.CheckAction("Summary (Odd): value changed to 7");
        }

        [Test]
        public void GetOptionsTest()
        {
            JDI.Assert.CollectionEquals(OddNumbersControl.Options, OddOptions);
        }

        [Test]
        public void GetNamesTest()
        {
            JDI.Assert.CollectionEquals(OddNumbersControl.Names, OddOptions);
        }

        [Test]
        public void GetValuesTest()
        {
            JDI.Assert.CollectionEquals(OddNumbersControl.Values, OddOptions);
        }

        [Test]
        public void GetOptionsAsTextTest()
        {
            JDI.Assert.AreEquals(OddNumbersControl.OptionsAsText, "1, 3, 5, 7");
        }

        [Test]
        public void SetValueTest()
        {
            OddNumbersControl.Value = "7";
            CommonActionsData.CheckAction("Summary (Odd): value changed to 7");
        }

        [Test]
        public void GetNameTest()
        {
            JDI.Assert.AreEquals(OddNumbersControl.Name, "Odd Numbers");
        }

        [Test]
        public void GetSelectedTest()
        {
            CommonActionsData.CheckActionThrowError(() => OddNumbersControl.Selected(),
                CommonActionsData.NoElementsMessage);
        }

        [Test]
        public void GetSelectedIndexTest()
        {
            CommonActionsData.CheckActionThrowError(() => OddNumbersControl.SelectedIndex(),
                CommonActionsData.NoElementsMessage);
        }

        [Test]
        public void IsSelectedTest()
        {
            CommonActionsData.CheckActionThrowError(() => OddNumbersControl.Selected("7"),
                CommonActionsData.NoElementsMessage);
        }

        [Test]
        [Ignore("C# enum does not support strings")]
        public void IsSelectedEnumTest()
        {
            // Select method is waiting for string "7" not "Seven"
            CommonActionsData.CheckActionThrowError(() => OddNumbersControl.Selected(Odds.Seven),
                CommonActionsData.NoElementsMessage);
        }

        [Test]
        [Ignore("Timer.cs wait method hide exceptions unlike Java")]
        public void WaitSelectedTest()
        {
            CommonActionsData.CheckActionThrowError(() => OddNumbersControl.WaitSelected("7"),
                CommonActionsData.NoElementsMessage);
        }

        [Test]
        [Ignore("Timer.cs wait method hide exceptions unlike Java. C# enum does not support strings")]
        public void WaitSelectedEnumTest()
        {
            CommonActionsData.CheckActionThrowError(() => OddNumbersControl.WaitSelected(Odds.Seven),
                CommonActionsData.NoElementsMessage);
        }

        [Test]
        public void GetValueTest()
        {
            CommonActionsData.CheckActionThrowError(() => OddNumbersControl.GetValue(),
                CommonActionsData.NoElementsMessage);
        }
    }
}