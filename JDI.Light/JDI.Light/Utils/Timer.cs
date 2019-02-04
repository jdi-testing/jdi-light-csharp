using System;
using System.Threading;
using System.Threading.Tasks;
using JDI.Light.Interfaces;

namespace JDI.Light.Utils
{
    public class Timer
    {
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
                using (var sta = new StaTaskScheduler(1))
                {
                    var f = new TaskFactory<T>(tokenSource.Token, TaskCreationOptions.DenyChildAttach, TaskContinuationOptions.ExecuteSynchronously, sta);
                    var task = f.StartNew(() =>
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
                    }, tokenSource.Token);
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
}