namespace StratmanMedia.Logging
{
    public class SMLoggerConfiguration
    {
        public string ApplicationName { get; set; }
        public SMLogLevel MinimumLogLevel { get; set; }
        public SMLoggerSqlServerConfiguration SqlServerConfiguration { get; set; }
    }
}