using System;

namespace JDI.Core.Settings
{
    public interface IAssert
    {
        Exception Exception(string message, Exception ex);
        Exception Exception(string message);
        void IsTrue(bool actual);
    }
}
