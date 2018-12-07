using System;
using System.Collections.Generic;
using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Complex
{
    public interface IForm<T> : ISetValue<T>, IBaseElement
    {
        void Fill(T entity);
        void Fill(Dictionary<string, string> map);
        void Check(T entity);
        void Check(Dictionary<string, string> map);
        IList<string> Verify(Dictionary<string, string> objStrings);
        IList<string> Verify(T entity);
        void Submit(T entity, string buttonName);
        void Submit(T entity, Enum buttonName);
        void Submit(T entity);
        void Submit(Dictionary<string, string> objStrings);
    }
}