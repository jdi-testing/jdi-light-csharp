using System;
using JDI.Light.Enums;
using JDI.Light.Interfaces;
using JDI.Light.Utils;

namespace JDI.Light.Selenium.Elements.WebActions
{
    public class ActionScenarios<T>
    {
        private readonly T _targetElement;
        private readonly ILogger _logger;

        public ActionScenarios(T element, ILogger logger)
        {
            _targetElement = element;
            _logger = logger;
        }

        private void LogAction(string actionName, LogLevel level)
        {
            JDI.Logger.Log(string.Format(JDI.ShortLogMessagesFormat
                ? "{0} for {1}"
                : "Perform action '{0}' with WebElement ({1})", actionName, _targetElement.ToString()), level);
        }

        public void ActionScenario(string actionName, Action<T> action, LogLevel level)
        {
            LogAction(actionName, level);
            new Timer(JDI.Timeouts.CurrentTimeoutSec).Wait(() =>
            {
                action(_targetElement);
                return true;
            });
            JDI.Logger.Info(actionName + " done");
        }

        public TResult ResultScenario<TResult>(string actionName, Func<T, TResult> action,
            Func<TResult, string> logResult, LogLevel level)
        {
            LogAction(actionName, level);
            var timer = new Timer();
            var result =
                ExceptionUtils.ActionWithException(() => new Timer(JDI.Timeouts.CurrentTimeoutSec)
                        .GetResultByCondition(() => action.Invoke(_targetElement), res => true),
                    ex => $"Do action {actionName} failed. Can't got result. Reason: {ex}");
            if (result == null)
                throw JDI.Assert.Exception($"Do action {actionName} failed. Can't got result");
            var stringResult = logResult == null
                ? result.ToString()
                : logResult.Invoke(result);
            var timePassed = timer.TimePassed.TotalMilliseconds;
            JDI.Logger.Log($"Get result '{stringResult}' in {timePassed / 1000:F} seconds", level);
            return result;
        }
    }
}