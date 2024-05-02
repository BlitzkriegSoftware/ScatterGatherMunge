using CommandLine;
using System.Collections.Generic;

namespace StuartWilliams.ScatterGatherMunge.Producer
{
    /// <summary>
    /// Command Line Options
    /// </summary>
    public class CommandOptions
    {
        /// <summary>
        /// Verbose Output
        /// </summary>
        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; } = false;

        /// <summary>
        /// Default Message Count To Create
        /// </summary>
        public const int MessageCount_Default = 50;

        /// <summary>
        /// Message Count
        /// </summary>
        [Option('m', "Message Count", Required = false, HelpText = "How many messages to generate", Default = MessageCount_Default)]
        public int MessageCount { get; set; } = MessageCount_Default;

        /// <summary>
        /// Path to Configuration JSON file
        /// </summary>
        [Option('c', "JSON Config File", Required = true, HelpText = "Path to JSON Configuration File")]
        public string ConfigurationFilename {  get; set; }

        /// <summary>
        /// Configuration Dictionary
        /// </summary>
        public Dictionary<string, object> Configuration { get; set; } = new();

    }

}

