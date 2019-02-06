using System;
using System.Threading;
using System.Threading.Tasks;
using JDI.Light.Enums;
using JDI.Light.Interfaces;
using JDI.Light.Utils;

namespace JDI.Light.Elements.WebActions
{
    public class ActionInvoker
    {
        private readonly ILogger _logger;
        private readonly int _timeoutInMSec;
        private readonly int _retryTimeoutInMSec;

        public ActionInvoker(ILogger logger, int timeoutInMSec, int retryTimeoutInMSec)
        {
            _logger = logger;
            _timeoutInMSec = timeoutInMSec;
            _retryTimeoutInMSec = retryTimeoutInMSec;
        }
        
        public TResult DoActionWithResult<TResult>(string actionName, Func<TResult> func,
            Func<TResult, string> logResult = null, LogLevel level = LogLevel.Info, Func<TResult, bool> checkResultFunc = null)
        {
            checkResultFunc = checkResultFunc ?? (r => r != null);
            _logger.Log($"Perform action with result '{actionName}'", level);
            return GetResultByCondition(() =>
            {
                var result = func.Invoke();
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
            Wait(() =>
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

        public bool Wait(Func<bool> waitFunc)
        {
            return GetResultByCondition(waitFunc, b => true);
        }

        public T GetResultByCondition<T>(Func<T> getFunc, Func<T, bool> conditionFunc)
        {
            Exception lastException = null;
            using (var tokenSource = new CancellationTokenSource())
            {
                var token = tokenSource.Token;
                using (var sta = new StaTaskScheduler(1))
                {
                    var f = new TaskFactory<T>(token, TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously, sta);
                    var task = f.StartNew(() =>
                    {
                        var cancelFlag = false;
                        while (!cancelFlag)
                        {
                            try
                            {
                                if (token.IsCancellationRequested)
                                {
                                    token.ThrowIfCancellationRequested();
                                }

                                var result = getFunc.Invoke();
                                if (result != null && conditionFunc.Invoke(result))
                                    return result;
                            }
                            catch (OperationCanceledException e)
                            {
                                lastException = e;
                                cancelFlag = true;
                            }
                            catch (Exception e)
                            {
                                _logger.Debug($"Exception: {e.Message}.{Environment.NewLine}{e.StackTrace}");
                                lastException = e;
                            }

                            Thread.Sleep(_retryTimeoutInMSec);
                        }

                        throw lastException ?? new OperationCanceledException("Operation was canceled!");
                    }, token, TaskCreationOptions.AttachedToParent, sta);
                    if (!task.Wait(_timeoutInMSec))
                    {
                        tokenSource.Cancel();
                    }
                    else if (task.IsCompleted)
                    {
                        return task.Result;
                    }
                }
            }
            throw lastException ?? new TimeoutException("The operation has timed-out");
        }
    }
}