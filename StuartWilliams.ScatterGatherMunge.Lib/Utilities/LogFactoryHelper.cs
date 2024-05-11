using Microsoft.Extensions.Logging;
using NetEscapades.Extensions.Logging.RollingFile;

namespace StuartWilliams.ScatterGatherMunge.Lib.Utilities
{
    /// <summary>
    /// Log Factory Helper
    /// </summary>
    public static class LogFactoryHelper
    {

        private static ILoggerFactory _factory = null;

        /// <summary>
        /// Creates logger factory
        /// </summary>
        private static ILoggerFactory ConsoleLoggerFactory
        {
            get
            {
                _factory ??= LoggerFactory.Create(builder =>
                    {
                        builder.AddFilter("Microsoft", LogLevel.Warning)
                               .AddFilter("System", LogLevel.Warning)
                               .SetMinimumLevel(LogLevel.Trace)
                               .AddConsole()
                               .AddFile(options => {
                                   // The log file prefixes
                                   options.FileName = "diagnostics-";
                                   // The directory to write the logs
                                   options.LogDirectory = ".";
                                   // The maximum log file size (20MB here)
                                   options.FileSizeLimit = 20 * 1024 * 1024;
                                   // When maximum file size is reached, create a new file, up to a limit of 200 files per periodicity
                                   options.FilesPerPeriodicityLimit = 200;
                                   // The log file extension
                                   options.Extension = "log";
                                   // Roll log files hourly instead of daily.
                                   options.Periodicity = PeriodicityOptions.Hourly;
                               });
                        ;
                    });
                return _factory;
            }
            set { _factory = value; }
        }

        /// <summary>
        /// Create Logger
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Logger</returns>
        public static ILogger CreateLogger<T>() => ConsoleLoggerFactory.CreateLogger<T>();

    }
}
