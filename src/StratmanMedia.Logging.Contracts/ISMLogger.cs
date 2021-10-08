using System;

namespace StratmanMedia.Logging.Contracts
{
    public interface ISMLogger
    {
        void LogDebug(string message);
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogError(Exception ex, string message);
        void LogFatal(string message);
        void LogFatal(Exception ex, string message);
    }
}