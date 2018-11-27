using System;
using System.Collections.Generic;

namespace JDI.Light.Interfaces
{
    public interface IAssert
    {
        void ThrowFail(string message);
        void ThrowFail(string message, Exception ex);
        Exception Exception(string message);
        void Contains(string actual, string expected);
        void IsTrue(bool condition);
        void IsFalse(bool condition);
        void CollectionEquals<T>(IEnumerable<T> actual, IEnumerable<T> expected);
        void AreEquals<T>(T actual, T expected, bool logOnlyFail = false);
    }
}