using System.Collections.Generic;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Interfaces.Composite
{
    public interface IForm<T> : ISetValue<T>, IBaseElement
    {
        void Fill(T entity);
        void Fill(Dictionary<string, string> map);
        void Check(T entity);
        void Check(Dictionary<string, string> map);
        IList<string> Verify(Dictionary<string, string> objStrings);
        IList<string> Verify(T entity);
        void Submit(T entity, By buttonLocator);
        void Submit(T entity, string buttonText);
        void Submit(T entity);
        void Submit(Dictionary<string, string> objStrings);
    }
}