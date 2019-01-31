using System;
using System.Diagnostics;
using System.Threading;

namespace JDI.Light.Utils
{
    public class Timer
    {
        private static readonly double DefaultTimeout = Jdi.Timeouts.CurrentTimeoutMSec;
        private static readonly int DefaultRetryTimeout = Jdi.Timeouts.RetryMSec;
        private readonly int _retryTimeoutInMSec;
        private readonly double _timeoutInMSec;

        private Stopwatch _watch = Stopwatch.StartNew();

        public Timer()
        {
            _timeoutInMSec = DefaultTimeout;
            _retryTimeoutInMSec = DefaultRetryTimeout;
        }

        public Timer(double timeoutInMSec)
        {
            _timeoutInMSec = timeoutInMSec;
            _retryTimeoutInMSec = DefaultRetryTimeout;
        }

        public TimeSpan TimePassed => _watch.Elapsed;
        public bool TimeoutPassed => _watch.Elapsed > TimeSpan.FromMilliseconds(_timeoutInMSec);

        public bool Wait(Func<bool> waitFunc)
        {
            _watch = Stopwatch.StartNew();
            while (!TimeoutPassed)
            {
                bool res;
                try
                {
                    res = waitFunc();
                }
                catch (Exception e)
                {
                    Jdi.Logger.Debug($"Exception: {e.Message}.{Environment.NewLine}{e.StackTrace}");
                    res = false;
                }
                if (res) return true;
                Thread.Sleep(_retryTimeoutInMSec);
            }
            return false;
        }

        public T GetResultByCondition<T>(Func<T> getFunc, Func<T, bool> conditionFunc)
        {
            _watch = Stopwatch.StartNew();
            Exception exception = null;
            do
            {
                try
                {
                    var result = getFunc.Invoke();
                    if (result != null && conditionFunc.Invoke(result))
                        return result;
                }
                catch (Exception e)
                {
                    Jdi.Logger.Debug($"Exception: {e.Message}.{Environment.NewLine}{e.StackTrace}");
                    exception = e;
                }
                Thread.Sleep(_retryTimeoutInMSec);
            } while (!TimeoutPassed);

            throw exception ?? new TimeoutException("The operation has timed-out");
        }
    }
}