using System;
using System.IO;
using Xunit;
using Peaky.Services;

namespace Peaky.Tests.Services
{
    public class LoggingServiceTests : IDisposable
    {
        private readonly string _logDirectory = "logs";
        private readonly string _logFile;
        private readonly LoggingService _loggingService;

        public LoggingServiceTests()
        {
            _logFile = Path.Combine(_logDirectory, $"peaky_{DateTime.Now:yyyyMMdd}.log");
            _loggingService = new LoggingService();
        }

        [Fact]
        public void LogError_WithException_WritesToFile()
        {
            // Arrange
            var testMessage = "Test error message";
            var testException = new Exception("Test exception");

            // Act
            _loggingService.LogError(testMessage, testException);

            // Assert
            Assert.True(File.Exists(_logFile));
            var logContent = File.ReadAllText(_logFile);
            Assert.Contains(testMessage, logContent);
            Assert.Contains(testException.Message, logContent);
        }

        [Fact]
        public void LogInfo_WritesToFile()
        {
            // Arrange
            var testMessage = "Test info message";

            // Act
            _loggingService.LogInfo(testMessage);

            // Assert
            Assert.True(File.Exists(_logFile));
            var logContent = File.ReadAllText(_logFile);
            Assert.Contains(testMessage, logContent);
        }

        [Fact]
        public void LogWarning_WritesToFile()
        {
            // Arrange
            var testMessage = "Test warning message";

            // Act
            _loggingService.LogWarning(testMessage);

            // Assert
            Assert.True(File.Exists(_logFile));
            var logContent = File.ReadAllText(_logFile);
            Assert.Contains(testMessage, logContent);
        }

        [Fact]
        public void LogDebug_WritesToFile()
        {
            // Arrange
            var testMessage = "Test debug message";

            // Act
            _loggingService.LogDebug(testMessage);

            // Assert
            Assert.True(File.Exists(_logFile));
            var logContent = File.ReadAllText(_logFile);
            Assert.Contains(testMessage, logContent);
        }

        public void Dispose()
        {
            if (File.Exists(_logFile))
            {
                File.Delete(_logFile);
            }

            if (Directory.Exists(_logDirectory))
            {
                Directory.Delete(_logDirectory, true);
            }
        }
    }
}