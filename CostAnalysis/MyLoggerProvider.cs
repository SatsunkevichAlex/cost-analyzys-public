using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace CostAnalysis
{
    public class MyLoggerProvider : ILoggerProvider
    {
        public void Dispose()
        {
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new MyLogger();
        }

        private class MyLogger : ILogger
        {
            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
                Func<TState, Exception, string> formatter)
            {
                File.AppendAllText("log.log", formatter(state, exception)); //Insert to log file
                Console.WriteLine(formatter(state, exception)); //Console output
            }

            public bool IsEnabled(LogLevel logLevel) //Logger always enable
            {
                return true;
            }

            public IDisposable BeginScope<TState>(TState state) //No need in this method
            {
                return null;
            }
        }
    }
}