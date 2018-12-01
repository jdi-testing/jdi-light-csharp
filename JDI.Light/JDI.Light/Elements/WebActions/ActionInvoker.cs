using System;
using JDI.Light.Elements.Base;
using JDI.Light.Enums;
using JDI.Light.Extensions;
using JDI.Light.Interfaces;
using JDI.Light.Utils;

namespace JDI.Light.Elements.WebActions
{
    public class ActionInvoker<T>
    {
        private static ActionScenarios<T> _actionScenarios;
        private readonly T _element;

        public ActionInvoker(T element, ILogger logger)
        {
            _element = element;
            _actionScenarios = new ActionScenarios<T>(element, logger);
        }

        public TResult DoJActionResult<TResult>(string actionName, Func<T, TResult> action,
            Func<TResult, string> logResult = null, LogLevel level = LogLevel.Info)
        {
            return ExceptionUtils.ActionWithException(() =>
            {
                ProcessDemoMode();
                return _actionScenarios.ResultScenario(actionName, action, logResult, level);
            }, ex => $"Failed to do '{actionName}' action. Reason: {ex}");
        }

        public void DoJAction(string actionName, Action<T> action, LogLevel level = LogLevel.Info)
        {
            TimerExtensions.ForceDone(() =>
            {
                ProcessDemoMode();
                _actionScenarios.ActionScenario(actionName, action, level);
            });
        }

        public void ProcessDemoMode()
        {
            if (!JDI.IsDemoMode) return;
            (_element as UIElement)?.Highlight();
        }
    }
}