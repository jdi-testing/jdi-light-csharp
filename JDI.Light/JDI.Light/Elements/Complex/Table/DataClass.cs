using System;

namespace JDI.Light.Elements.Complex.Table
{
    public class DataClass<T>
    {
        public DataClass<T> Set(T value, Action<T> executeValue)
        {
            executeValue.Invoke(value);
            return this;
        }
    }
   
}
