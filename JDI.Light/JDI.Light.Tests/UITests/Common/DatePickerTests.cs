using JDI.Light.Interfaces.Common;
using NUnit.Framework;

namespace JDI.Light.Tests.UITests.Common
{
    [TestFixture]
    public class DatePickerTests : TestBase
    {
        private const string DefaultDate = "09/09/1955";
        private const string CheckDate = "09/05/1955";
        private ITextField DatePicker => TestSite.Dates.Datepicker;

        [SetUp]
        public void SetUp()
        {
            Jdi.Logger.Info("Navigating to Metals and Colors page.");
            TestSite.Dates.Open();
            TestSite.Dates.CheckTitle();
            TestSite.Dates.Datepicker.Clear();
            Jdi.Logger.Info("Setup method finished");
            Jdi.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void InputDatePickerTest()
        {
            DatePicker.Input(DefaultDate);
            Jdi.Assert.AreEquals(DatePicker.Value, DefaultDate);
        }

        [Test]
        public void SendKeysDatePickerTest()
        {
            DatePicker.SendKeys(DefaultDate);
            Jdi.Assert.AreEquals(DatePicker.Value, DefaultDate);
        }

        [Test]
        public void NewInputDatePickerTest()
        {
            DatePicker.SendKeys(CheckDate);
            DatePicker.NewInput(DefaultDate);
            Jdi.Assert.AreEquals(DatePicker.Value, DefaultDate);
        }

        [Test]
        public void ClearTest()
        {
            DatePicker.SendKeys(DefaultDate);
            DatePicker.Clear();
            Jdi.Assert.AreEquals(DatePicker.Value, "");
        }

        [Test]
        public void MultiKeyTest()
        {
            foreach (var ch in DefaultDate) DatePicker.SendKeys(ch.ToString());
            Jdi.Assert.AreEquals(DatePicker.Value, DefaultDate);
        }
    }
}