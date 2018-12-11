using System;
using JDI.Light.Enums;

namespace JDI.Light.Interfaces
{
    public interface ILogger
    {
        LogLevel LogLevel { get; set; }

        void Log(string message, LogLevel level);
        void Exception(Exception ex);
        void Trace(string message);
        void Debug(string message);
        void Info(string message);
        void Error(string message);
        void Step(string message);
        void TestDescription(string message);
        void TestSuit(string message);
    }
}