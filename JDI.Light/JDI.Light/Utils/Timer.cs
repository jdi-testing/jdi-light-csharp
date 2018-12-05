using System;
using System.Diagnostics;
using System.Threading;

namespace JDI.Light.Utils
{
    public class Timer
    {
        private static readonly double DefaultTimeout = JDI.Timeouts.CurrentTimeoutMSec;
        private static readonly int DefaultRetryTimeout = JDI.Timeouts.RetryMSec;

        private readonly int _retryTimeoutInMSec;
        private readonly double _timeoutInMSec;

        private readonly Stopwatch _watch;

        public Timer()
        {
            _watch = Stopwatch.StartNew();
            _timeoutInMSec = DefaultTimeout;
            _retryTimeoutInMSec = DefaultRetryTimeout;
        }

        public Timer(double timeoutInMSec)
        {
            _watch = Stopwatch.StartNew();
            _timeoutInMSec = timeoutInMSec;
            _retryTimeoutInMSec = DefaultRetryTimeout;
        }

        public TimeSpan TimePassed => _watch.Elapsed;
        public bool TimeoutPassed => _watch.Elapsed > TimeSpan.FromMilliseconds(_timeoutInMSec);

        public bool Wait(Func<bool> waitFunc)
        {
            while (!TimeoutPassed)
            {
                if (waitFunc.AvoidExceptions())
                    return true;
                Thread.Sleep(_retryTimeoutInMSec);
            }

            return false;
        }

        public T GetResult<T>(Func<T> func)
        {
            return GetResultByCondition(func, result => true);
        }

        public T GetResultByCondition<T>(Func<T> getFunc, Func<T, bool> conditionFunc)
        {
            Exception exception = null;
            do
            {
                try
                {
                    var result = getFunc.Invoke();
                    if (result != null && conditionFunc.Invoke(result))
                        return result;
                }
                catch (Exception ex)
                {
                    exception = ex;
                }

                Thread.Sleep(_retryTimeoutInMSec);
            } while (!TimeoutPassed);

            if (exception != null) throw exception;
            throw new TimeoutException("The operation has timed-out");
        }
    }
}