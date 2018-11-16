using System;
using JDI.Core.Extensions;
using JDI.Core.Logging;
using JDI.Core.Selenium.Base;
using JDI.Core.Selenium.Elements.Base;
using JDI.Core.Settings;
using JDI.Core.Utils;

namespace JDI.Core.Selenium.Elements.WebActions
{
    public class ActionInvoker
    {
        private static ActionScenarios _actionScenarios;
        private readonly WebBaseElement _element;

        public ActionInvoker(WebBaseElement element)
        {
            JDISettings.NewTest();
            _element = element;
            _actionScenarios = new ActionScenarios(element);
        }

        public TResult DoJActionResult<TResult>(string actionName, Func<WebBaseElement, TResult> action,
            Func<TResult, string> logResult = null, LogLevels level = LogLevels.Info)
        {
            return ExceptionUtils.ActionWithException(() =>
            {
                ProcessDemoMode();
                return _actionScenarios.ResultScenario(actionName, action, logResult, level);
            }, ex => $"Failed to do '{actionName}' action. Reason: {ex}");
        }

        public void DoJAction(string actionName, Action<WebBaseElement> action, LogLevels level = LogLevels.Info)
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
            if (_element is WebBaseElement)
                ((WebBaseElement) _element).Highlight(JDISettings.HighlightSettings);
        }
    }
}