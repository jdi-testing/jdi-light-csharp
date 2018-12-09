using System;
using System.Linq;
using JDI.Light.Enums;
using JDI.Light.Interfaces;

namespace JDI.Light.Logging
{
    public class ConsoleLogger : ILogger
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Info;

        private readonly LogLevel[] _warningLevels = { LogLevel.Fatal, LogLevel.Error, LogLevel.Warning };
        private readonly LogLevel[] _infoLevels = { LogLevel.Fatal, LogLevel.Error, LogLevel.Warning, LogLevel.Info };
        private readonly LogLevel[] _debugLevels = { LogLevel.Fatal, LogLevel.Error, LogLevel.Warning, LogLevel.Info, LogLevel.Debug };
        private readonly LogLevel[] _traceLevels = { LogLevel.Fatal, LogLevel.Error, LogLevel.Warning, LogLevel.Info, LogLevel.Debug, LogLevel.Trace };

        public void Log(string message, LogLevel logLevel)
        {
            switch (LogLevel)
            {
                case LogLevel.Off:
                    return;
                case LogLevel.Fatal:
                    if (logLevel != LogLevel.Fatal)
                    {
                        return;
                    }
                    break;
                case LogLevel.Error:
                    if (logLevel != LogLevel.Fatal && logLevel!= LogLevel.Error)
                    {
                        return;
                    }
                    break;
                case LogLevel.Warning:
                    if (!_warningLevels.Contains(logLevel))
                    {
                        return;
                    }
                    break;
                case LogLevel.Info:
                    if (!_infoLevels.Contains(logLevel))
                    {
                        return;
                    }
                    break;
                case LogLevel.Debug:
                    if (!_debugLevels.Contains(logLevel))
                    {
                        return;
                    }
                    break;
                case LogLevel.Trace:
                    if (_traceLevels.Contains(logLevel))
                    {
                        return;
                    }
                    break;
                case LogLevel.All:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Console.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss.fff} {logLevel}: {message}");
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