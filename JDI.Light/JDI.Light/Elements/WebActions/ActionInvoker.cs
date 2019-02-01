using System;
using JDI.Light.Enums;
using JDI.Light.Interfaces;
using JDI.Light.Utils;

namespace JDI.Light.Elements.WebActions
{
    public class ActionInvoker
    {
        private readonly ILogger _logger;
        private readonly Timer _timer;

        public ActionInvoker(ILogger logger, Timer timer)
        {
            _logger = logger;
            _timer = timer;
        }
        
        public TResult DoActionWithResult<TResult>(string actionName, Func<TResult> action,
            Func<TResult, string> logResult = null, LogLevel level = LogLevel.Info, Func<TResult, bool> checkResultFunc = null)
        {
            checkResultFunc = checkResultFunc ?? (r => r != null);
            _logger.Log($"Perform action with result '{actionName}'", level);
            return _timer.GetResultByCondition(() =>
            {
                var result = action.Invoke();
                if (result == null)
                    throw Jdi.Assert.Exception($"Do action {actionName} failed. Can't get result.");
                var stringResult = logResult == null
                    ? result.ToString()
                    : logResult.Invoke(result);
                _logger.Log($"Get result '{stringResult}'", level);
                return result;
            }, checkResultFunc);
        }

        public void DoActionWithWait(string actionName, Action action, LogLevel level = LogLevel.Info)
        {
            _logger.Log($"Perform action '{actionName}'", level);
            _timer.Wait(() =>
            {
                action();
                return true;
            });
            _logger.Info($"Action '{actionName}' done");
        }

        public void DoAction(string actionName, Action action, LogLevel level = LogLevel.Info)
        {
            _logger.Log($"Perform action '{actionName}'", level);
            action();
            _logger.Info($"Action '{actionName}' done");
        }
    }
}