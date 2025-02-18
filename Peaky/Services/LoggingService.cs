using System;
using Serilog;

namespace Peaky.Services
{
    /// <summary>
    /// Implements logging functionality using Serilog.
    /// </summary>
    public class LoggingService : ILoggingService
    {
        private readonly ILogger _logger;

        public LoggingService()
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs/peaky.log", rollingInterval: RollingInterval.Day)
                .WriteTo.Debug()
                .CreateLogger();
        }

        public void LogError(string message, Exception? exception = null)
        {
            if (exception != null)
            {
                _logger.Error(exception, message);
            }
            else
            {
                _logger.Error(message);
            }
        }

        public void LogInfo(string message)
        {
            _logger.Information(message);
        }

        public void LogWarning(string message)
        {
            _logger.Warning(message);
        }

        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }

        public void Dispose()
        {
            (_logger as IDisposable)?.Dispose();
        }
    }
}