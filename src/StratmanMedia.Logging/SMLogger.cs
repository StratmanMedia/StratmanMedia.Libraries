using System;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using StratmanMedia.Logging.Contracts;

namespace StratmanMedia.Logging
{
    public class SMLogger : ISMLogger
    {
        public SMLogger(SMLoggerConfiguration configuration)
        {
            InitializeLogger(configuration).CreateLogger();
        }

        public void LogDebug(string message)
        {
            Log.Debug(message);
        }

        public void LogInformation(string message)
        {
            Log.Information(message);
        }

        public void LogWarning(string message)
        {
            Log.Warning(message);
        }

        public void LogError(string message)
        {
            Log.Error(message);
        }

        public void LogError(Exception ex, string message)
        {
            Log.Error(ex, message);
        }

        public void LogFatal(string message)
        {
            Log.Fatal(message);
        }

        public void LogFatal(Exception ex, string message)
        {
            Log.Fatal(ex, message);
        }

        private LoggerConfiguration InitializeLogger(SMLoggerConfiguration configuration)
        {
            var loggerConfig = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("ApplicationName", configuration.ApplicationName);
            SetMinimumLogLevel(loggerConfig, configuration);
            if (configuration.SqlServerConfiguration != null)
                SetSqlServerConfiguration(loggerConfig, configuration.SqlServerConfiguration);

            return loggerConfig;
        }

        private void SetMinimumLogLevel(
            LoggerConfiguration loggerConfiguration,
            SMLoggerConfiguration smLoggerConfiguration)
        {
            switch (smLoggerConfiguration.MinimumLogLevel)
            {
                case SMLogLevel.Debug:
                    loggerConfiguration.MinimumLevel.Debug();
                    break;
                case SMLogLevel.Information:
                    loggerConfiguration.MinimumLevel.Information();
                    break;
                case SMLogLevel.Warning:
                    loggerConfiguration.MinimumLevel.Warning();
                    break;
                case SMLogLevel.Error:
                    loggerConfiguration.MinimumLevel.Error();
                    break;
                default:
                    loggerConfiguration.MinimumLevel.Fatal();
                    break;
            }
        }

        private void SetSqlServerConfiguration(
            LoggerConfiguration loggerConfiguration,
            SMLoggerSqlServerConfiguration sqlServerConfiguration)
        {
            loggerConfiguration.WriteTo.MSSqlServer(
                connectionString: sqlServerConfiguration.ConnectionString,
                sinkOptions: new MSSqlServerSinkOptions
                {
                    TableName = sqlServerConfiguration.TableName
                });
        }
    }
}