using System;
using JDI.Core.Extensions;
using JDI.Core.Logging;
using JDI.Core.Settings;
using JDI.Core.Utils;
using JDI.Web.Selenium.Base;
using JDI.Web.Selenium.Elements.Base;

namespace JDI.Web.Selenium.Elements.WebActions
{
    public class ActionInvoker
    {
        public static ActionScenarios ActionScenarios = new ActionScenarios();
        private readonly WebBaseElement _element;

        public ActionInvoker(WebBaseElement element)
        {
            JDISettings.NewTest();
            _element = element;
        }

        public TResult DoJActionResult<TResult>(string actionName, Func<WebBaseElement, TResult> action,
            Func<TResult, string> logResult = null, LogLevels level = LogLevels.Info)
        {
            return ExceptionUtils.ActionWithException(() =>
            {
                ProcessDemoMode();
                return ActionScenarios.SetElement(_element).ResultScenario(actionName, action, logResult, level);
            }, ex => $"Failed to do '{actionName}' action. Reason: {ex}");
        }

        public void DoJAction(string actionName, Action<WebBaseElement> action, LogLevels level = LogLevels.Info)
        {
            TimerExtensions.ForceDone(() =>
            {
                ProcessDemoMode();
                ActionScenarios.SetElement(_element).ActionScenario(actionName, action, level);
            });
        }

        public void ProcessDemoMode()
        {
            if (!JDISettings.IsDemoMode) return;
            if (_element is WebElement)
                ((WebElement) _element).Highlight(JDISettings.HighlightSettings);
        }
    }
}