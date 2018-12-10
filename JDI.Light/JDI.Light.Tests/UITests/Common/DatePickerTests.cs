using JDI.Light.Interfaces.Common;
using JDI.Light.Tests.UIObjects;
using NUnit.Framework;

namespace JDI.Light.Tests.UITests.Common
{
    [TestFixture]
    public class DatePickerTests : TestBase
    {
        private const string DefaultDate = "09/09/1955";
        private const string CheckDate = "09/05/1955";
        private readonly ITextField _datePicker = TestSite.Dates.Datepicker;

        [SetUp]
        public void SetUp()
        {
            JDI.Logger.Info("Navigating to Metals and Colors page.");
            TestSite.Dates.Open();
            TestSite.Dates.CheckTitle();
            TestSite.Dates.IsOpened();
            TestSite.Dates.Datepicker.Clear();
            JDI.Logger.Info("Setup method finished");
            JDI.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void InputDatePickerTest()
        {
            _datePicker.Input(DefaultDate);
            JDI.Assert.AreEquals(_datePicker.Value, DefaultDate);
        }

        [Test]
        public void SendKeysDatePickerTest()
        {
            _datePicker.SendKeys(DefaultDate);
            JDI.Assert.AreEquals(_datePicker.Value, DefaultDate);
        }

        [Test]
        public void NewInputDatePickerTest()
        {
            _datePicker.SendKeys(CheckDate);
            _datePicker.NewInput(DefaultDate);
            JDI.Assert.AreEquals(_datePicker.Value, DefaultDate);
        }

        [Test]
        public void ClearTest()
        {
            _datePicker.SendKeys(DefaultDate);
            _datePicker.Clear();
            JDI.Assert.AreEquals(_datePicker.Value, "");
        }

        [Test]
        public void MultiKeyTest()
        {
            foreach (var ch in DefaultDate) _datePicker.SendKeys(ch.ToString());
            JDI.Assert.AreEquals(_datePicker.Value, DefaultDate);
        }
    }
}