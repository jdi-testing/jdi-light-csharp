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
        
        public TResult DoActionWithResult<TResult>(string actionName, Func<TResult> action,
            Func<TResult, string> logResult = null, LogLevel level = LogLevel.Info)
        {
            _logger.Log($"Perform action with result '{actionName}' with WebElement ({_element.ToString()})", level);
            return ExceptionUtils.ActionWithException(() =>
            {
                var timer = new Timer();
                var result = action.Invoke();
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

        public void DoAction(string actionName, Action action, LogLevel level = LogLevel.Info)
        {
            _logger.Log($"Perform action '{actionName}' with WebElement ({_element.ToString()})", level);
            TimerExtensions.ForceDone(() =>
            {
                new Timer(JDI.Timeouts.CurrentTimeoutSec).Wait(() =>
                {
                    action();
                    return true;
                });
                _logger.Info(actionName + " done");
            });
        }
    }
}