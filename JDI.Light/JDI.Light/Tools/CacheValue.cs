using System;
using System.Threading;

namespace JDI.Light.Tools
{
    public class CacheValue<T> : IDisposable
    {
        private readonly ThreadLocal<long> _globalCache = new ThreadLocal<long>();

        private long GetGlobalCache()
        {
            if (_globalCache.Value == 0)
            {
                _globalCache.Value = 0L;
            }
            return _globalCache.Value;
        }

        public void Reset()
        {
            _globalCache.Value = DateTime.Now.Millisecond;
        }

        private long _elementCache;
        private T _value;
        public bool IsFinal { get; set; }
        private Func<T> _getRule;

        public CacheValue()
        {
        }

        public CacheValue(Func<T> getRule)
        {
            _getRule = getRule;
        }

        public bool IsUseCache() => _elementCache > -1;

        public void Clear()
        {
            if (!IsFinal)
            { 
                _value = default(T);
            }
        }

        public void SetRule(Func<T> getRule)
        {
            _getRule = getRule;
        }

        public bool HasValue()
        {
            return IsFinal || (IsUseCache() && !Equals(_value, default(T)) && _elementCache == GetGlobalCache());
        }

        public T Get(Func<T> defaultResult)
        {
            if (IsFinal)
            { 
                return _value;
            }
            if (!IsUseCache())
            { 
                return defaultResult.Invoke();
            }
            if (_elementCache >= GetGlobalCache() && !Equals(_value, default(T))) { return _value; }
            _value = _getRule.Invoke();
            _elementCache = GetGlobalCache();
            return _value;
        }

        public T Get()
        {
            return Get(_getRule);
        }

        public T GetForce()
        {
            Reset();
            return Get();
        }

        public void UseCache(bool value) => _elementCache = value ? 0 : -1;

        public T SetForce(T value)
        {
            if (IsFinal)
            { 
                return value;
            }
            _elementCache = GetGlobalCache();
            _value = value;
            return value;
        }

        public T SetFinal(T value)
        {
            _value = value;
            IsFinal = true;
            return value;
        }

        public T Set(T value)
        {
            return IsFinal || !IsUseCache()
                ? value
                : SetForce(value);
        }

        public void Dispose()
        {
            _globalCache?.Dispose();
        }
    }
}
