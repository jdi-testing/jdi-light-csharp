using System;

namespace JDI.Light.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Exception(Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }

        public void Trace(string message)
        {
            Console.WriteLine(message);
        }

        public void Debug(string message)
        {
            Console.WriteLine(message);
        }

        public void Info(string message)
        {
            Console.WriteLine(message);
        }

        public void Error(string message)
        {
            Console.WriteLine(message);
        }

        public void Step(string message)
        {
            Console.WriteLine(message);
        }

        public void TestDescription(string message)
        {
            Console.WriteLine(message);
        }

        public void TestSuit(string message)
        {
            Console.WriteLine(message);
        }
    }
}