using System;
using JDI.Light.Enums;
using JDI.Light.Interfaces;

namespace JDI.Light.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message, LogLevel logLevel)
        {
            Console.WriteLine($"{DateTime.Now:dd.MM.yyyy_HH:mm:ss.fff} {logLevel} {message}");
        }

        public void Exception(Exception ex)
        {
            Log($"Exception: {ex.Message}", LogLevel.Error);
        }

        public void Trace(string message)
        {
            Log($"{message}", LogLevel.Trace);
        }

        public void Debug(string message)
        {
            Log($"{message}", LogLevel.Debug);
        }

        public void Info(string message)
        {
            Log($"{message}", LogLevel.Info);
        }

        public void Error(string message)
        {
            Log($"{message}", LogLevel.Error);
        }

        public void Step(string message)
        {
            Log($"{message}", LogLevel.Info);
        }

        public void TestDescription(string message)
        {
            Log($"{message}", LogLevel.Info);
        }

        public void TestSuit(string message)
        {
            Log($"{message}", LogLevel.Info);
        }
    }
}