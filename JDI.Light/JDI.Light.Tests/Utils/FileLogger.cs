using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using JDI.Light.Enums;
using JDI.Light.Interfaces;

namespace JDI.Light.Tests.Utils
{
    public class FileLogger : ILogger
    {
        public const string LogFilePath = @"C:\temp\jdi.txt";

        public LogLevel LogLevel { get; set; } = LogLevel.Info;

        private readonly LogLevel[] _warningLevels = { LogLevel.Fatal, LogLevel.Error, LogLevel.Warning };
        private readonly LogLevel[] _infoLevels = { LogLevel.Fatal, LogLevel.Error, LogLevel.Warning, LogLevel.Info };
        private readonly LogLevel[] _debugLevels = { LogLevel.Fatal, LogLevel.Error, LogLevel.Warning, LogLevel.Info, LogLevel.Debug };
        private readonly LogLevel[] _traceLevels = { LogLevel.Fatal, LogLevel.Error, LogLevel.Warning, LogLevel.Info, LogLevel.Debug, LogLevel.Trace };

        public void Log(string message, LogLevel logLevel)
        {
            var doLog = true;
            switch (LogLevel)
            {
                case LogLevel.Off:
                    return;
                case LogLevel.Fatal:
                    doLog = logLevel == LogLevel.Fatal;
                    break;
                case LogLevel.Error:
                    doLog = logLevel == LogLevel.Fatal || logLevel == LogLevel.Error;
                    break;
                case LogLevel.Warning:
                    doLog = _warningLevels.Contains(logLevel);
                    break;
                case LogLevel.Info:
                    doLog = _infoLevels.Contains(logLevel);
                    break;
                case LogLevel.Debug:
                    doLog = _debugLevels.Contains(logLevel);
                    break;
                case LogLevel.Trace:
                    doLog = _traceLevels.Contains(logLevel);
                    break;
                case LogLevel.All:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (doLog)
            {
                var msg = $"{DateTime.Now:dd.MM.yyyy HH:mm:ss.fff} {logLevel} [{Thread.CurrentThread.ManagedThreadId}]: {message}";
                File.AppendAllLines(LogFilePath, new List<string>{msg});
            }
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