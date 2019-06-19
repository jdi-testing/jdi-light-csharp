using System;
using System.Threading;

namespace JDI.Light.Tools
{
    public class CacheValue<T>
    {
        private static readonly ThreadLocal<long> globalCache = new ThreadLocal<long>();

        private static long GetGlobalCache()
        {
            if (globalCache.Value == 0)
            {
                globalCache.Value = 0L;
            }
            return globalCache.Value;
        }

        public static void Reset()
        {
            globalCache.Value = DateTime.Now.Millisecond;
        }

        private long _elementCache;
        private T _value;
        private bool _isFinal = false;
        private Func<T> _getRule = null;

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
            if (!_isFinal)
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
            return _isFinal || (IsUseCache() && !Equals(_value, default(T)) && _elementCache == GetGlobalCache());
        }

        public T Get(Func<T> defaultResult)
        {
            if (_isFinal)
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
            if (_isFinal)
                return value;
            _elementCache = GetGlobalCache();
            _value = value;
            return value;
        }

        public T SetFinal(T value)
        {
            _value = value;
            _isFinal = true;
            return value;
        }

        public T Set(T value)
        {
            return _isFinal || !IsUseCache()
                ? value
                : SetForce(value);
        }
    }
}
