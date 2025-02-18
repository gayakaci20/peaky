using System;

namespace Peaky.Services
{
    /// <summary>
    /// Defines the contract for logging operations within the application.
    /// </summary>
    public interface ILoggingService
    {
        /// <summary>
        /// Logs an error message with optional exception details.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        /// <param name="exception">Optional exception that caused the error.</param>
        void LogError(string message, Exception? exception = null);

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        void LogInfo(string message);

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">The warning message to log.</param>
        void LogWarning(string message);

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="message">The debug message to log.</param>
        void LogDebug(string message);
    }
}