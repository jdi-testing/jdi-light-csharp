using System;
using JDI.Core.Logging;
using JDI.Core.Reporting;
using JDI.Core.Selenium.Base;
using JDI.Core.Settings;
using JDI.Core.Utils;

namespace JDI.Core.Selenium.Elements.WebActions
{
    public class ActionScenarios
    {
        private readonly WebBaseElement _element;

        public ActionScenarios(WebBaseElement element)
        {
            _element = element;
        }

        public void ActionScenario(string actionName, Action<WebBaseElement> action, LogLevels logSettings)
        {
            _element.LogAction(actionName, logSettings);
            var timer = new Timer();
            new Timer(JDISettings.Timeouts.CurrentTimeoutSec).Wait(() =>
            {
                action(_element);
                return true;
            });
            JDISettings.Logger.Info(actionName + " done");
            PerformanceStatistic.AddStatistic(timer.TimePassed.TotalMilliseconds);
        }

        public TResult ResultScenario<TResult>(string actionName, Func<WebBaseElement, TResult> action,
            Func<TResult, string> logResult, LogLevels level)
        {
            _element.LogAction(actionName);
            var timer = new Timer();
            var result =
                ExceptionUtils.ActionWithException(() => new Timer(JDISettings.Timeouts.CurrentTimeoutSec)
                        .GetResultByCondition(() => action.Invoke(_element), res => true),
                    ex => $"Do action {actionName} failed. Can't got result. Reason: {ex}");
            if (result == null)
                throw JDISettings.Exception($"Do action {actionName} failed. Can't got result");
            var stringResult = logResult == null
                ? result.ToString()
                : logResult.Invoke(result);
            var timePassed = timer.TimePassed.TotalMilliseconds;
            PerformanceStatistic.AddStatistic(timer.TimePassed.TotalMilliseconds);
            JDISettings.ToLog($"Get result '{stringResult}' in {timePassed / 1000:F} seconds", level);
            return result;
        }
    }
}