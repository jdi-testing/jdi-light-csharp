using System;
using JDI.Light.Enums;
using JDI.Light.Extensions;
using JDI.Light.Interfaces;
using JDI.Light.Utils;

namespace JDI.Light.Elements.WebActions
{
    public class ActionInvoker<T>
    {
        private readonly T _element;
        private readonly ILogger _logger;

        public ActionInvoker(T element, ILogger logger)
        {
            _element = element;
            _logger = logger;
        }
        
        private void LogAction(string actionName, LogLevel level)
        {
            _logger.Log($"Perform action '{actionName}' with WebElement ({_element.ToString()})", level);
        }

        public TResult DoActionWithResult<TResult>(string actionName, Func<T, TResult> action,
            Func<TResult, string> logResult = null, LogLevel level = LogLevel.Info)
        {
            return ExceptionUtils.ActionWithException(() =>
            {
                LogAction(actionName, level);
                var timer = new Timer();
                var result = action.Invoke(_element);
                if (result == null)
                    throw JDI.Assert.Exception($"Do action {actionName} failed. Can't got result");
                var stringResult = logResult == null
                    ? result.ToString()
                    : logResult.Invoke(result);
                var timePassed = timer.TimePassed.TotalMilliseconds;
                _logger.Log($"Get result '{stringResult}' in {timePassed / 1000:F} seconds", level);
                return result;
            }, ex => $"Failed to do '{actionName}' action. Reason: {ex}");
        }

        public void DoAction(string actionName, Action<T> action, LogLevel level = LogLevel.Info)
        {
            TimerExtensions.ForceDone(() =>
            {
                LogAction(actionName, level);
                new Timer(JDI.Timeouts.CurrentTimeoutSec).Wait(() =>
                {
                    action(_element);
                    return true;
                });
                _logger.Info(actionName + " done");
            });
        }
    }
}