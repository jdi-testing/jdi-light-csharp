using JDI.Light.Interfaces.Common;
using JDI.Light.Tests.UIObjects;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Common
{
    public class DatePickerTests
    {
        private const string DEFAULT_DATE = "09/09/1945";
        private const string CHECK_DATE = "09/05/1945";
        private const string PARTIAL_TEXT_FIRST = "09/";
        private const string PARTIAL_TEXT_LAST = "09/1945";
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
            _datePicker.Input(DEFAULT_DATE);
            JDI.Assert.AreEquals(_datePicker.GetText, DEFAULT_DATE);
        }

        [Test]
        public void SendKeysDatePickerTest()
        {
            _datePicker.SendKeys(DEFAULT_DATE);
            JDI.Assert.AreEquals(_datePicker.GetText, DEFAULT_DATE);
        }

        [Test]
        public void NewInputDatePickerTest()
        {
            _datePicker.SendKeys(CHECK_DATE);
            _datePicker.NewInput(DEFAULT_DATE);
            JDI.Assert.AreEquals(_datePicker.GetText, DEFAULT_DATE);
        }

        [Test]
        public void ClearTest()
        {
            _datePicker.SendKeys(DEFAULT_DATE);
            _datePicker.Clear();
            JDI.Assert.AreEquals(_datePicker.GetText, "");
        }

        [Test]
        public void MultiKeyTest()
        {
            foreach (var ch in DEFAULT_DATE) _datePicker.SendKeys(ch.ToString());
            JDI.Assert.AreEquals(_datePicker.GetText, DEFAULT_DATE);
        }

        //TO_DO
        /*
        [Test]
        public void ElementFocusTest()
        {

        }
        */
    }
}