using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using JDI.Light.Interfaces;

namespace JDI.Light.Utils
{
    public class Timer
    {
        private static readonly SynchronizationContext SynchronizationContext = new SynchronizationContext();
        private readonly int _retryTimeoutInMSec;
        private readonly int _timeoutInMSec;
        private readonly ILogger _logger;

        public Timer(int timeoutInMSec, int retryTimeout, ILogger logger)
        {
            _logger = logger;
            _timeoutInMSec = timeoutInMSec;
            _retryTimeoutInMSec = retryTimeout;
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
                if (SynchronizationContext.Current == null)
                {
                    SynchronizationContext.SetSynchronizationContext(SynchronizationContext);
                }

                var token = tokenSource.Token;
                var task = Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        try
                        {
                            var result = getFunc.Invoke();
                            if (result != null && conditionFunc.Invoke(result))
                                return result;
                        }
                        catch (Exception e)
                        {
                            _logger.Debug($"Exception: {e.Message}.{Environment.NewLine}{e.StackTrace}");
                            lastException = e;
                        }
                        Thread.Sleep(_retryTimeoutInMSec);
                    }
                }, token, TaskCreationOptions.DenyChildAttach, TaskScheduler.FromCurrentSynchronizationContext());

                if (!task.Wait(_timeoutInMSec))
                {
                    tokenSource.Cancel();
                    throw lastException ?? new TimeoutException("The operation has timed-out");
                }

                if (!task.IsCompleted)
                {
                    throw lastException ?? new TimeoutException("The operation has timed-out");
                }

                return task.Result;
            }
        }
    }
}