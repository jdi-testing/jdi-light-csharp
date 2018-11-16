using System;
using JDI.Core.Extensions;
using JDI.Core.Logging;
using JDI.Core.Selenium.Base;
using JDI.Core.Settings;
using JDI.Core.Utils;

namespace JDI.Core.Selenium.Elements.WebActions
{
    public class ActionInvoker<T>
    {
        private static ActionScenarios<T> _actionScenarios;
        private readonly T _element;

        public ActionInvoker(T element)
        {
            JDISettings.NewTest();
            _element = element;
            _actionScenarios = new ActionScenarios<T>(element);
        }

        public TResult DoJActionResult<TResult>(string actionName, Func<T, TResult> action,
            Func<TResult, string> logResult = null, LogLevels level = LogLevels.Info)
        {
            return ExceptionUtils.ActionWithException(() =>
            {
                ProcessDemoMode();
                return _actionScenarios.ResultScenario(actionName, action, logResult, level);
            }, ex => $"Failed to do '{actionName}' action. Reason: {ex}");
        }

        public void DoJAction(string actionName, Action<T> action, LogLevels level = LogLevels.Info)
        {
            TimerExtensions.ForceDone(() =>
            {
                ProcessDemoMode();
                _actionScenarios.ActionScenario(actionName, action, level);
            });
        }

        public void ProcessDemoMode()
        {
            if (!JDISettings.IsDemoMode) return;
            (_element as WebBaseElement)?.Highlight(JDISettings.HighlightSettings);
        }
    }
}