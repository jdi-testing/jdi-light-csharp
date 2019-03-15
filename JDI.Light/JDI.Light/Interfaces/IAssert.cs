using System;
using System.Collections.Generic;

namespace JDI.Light.Interfaces
{
    public interface IAssert
    {
        ILogger Logger { get; set; }
        void ThrowFail(string message);
        void ThrowFail(string message, Exception ex);
        Exception Exception(string message);
        void Contains(string actual, string expected);
        void IsTrue(bool condition, string message = "");
        void IsFalse(bool condition, string message = "");
        void CollectionEquals<T>(IEnumerable<T> actual, IEnumerable<T> expected);
        void AreEquals<T>(T actual, T expected);
    }
}