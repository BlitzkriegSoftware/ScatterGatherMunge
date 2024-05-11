using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StuartWilliams.ScatterGatherMunge.Lib.Clients;
using StuartWilliams.ScatterGatherMunge.Lib.Extensions;
using StuartWilliams.ScatterGatherMunge.Lib.Models;
using StuartWilliams.ScatterGatherMunge.Lib.Utilities;
using System;
using System.Collections.Generic;

namespace StuartWilliams.ScatterGatherMunge.Producer
{
    internal class Program
    {
        #region "Fields"
        static ILogger _logger;
        static IConfiguration _config;
        static StuartWilliams.ScatterGatherMunge.Lib.Models.RabbitMqEngineConfiguration _rabbitMqEngineConfiguration { get; set; } = new();
        static StuartWilliams.ScatterGatherMunge.Lib.Models.RabbitMqQueueConfiguration _rabbitMqQueueConfiguration { get; set; } = new();
        #endregion

        static void Main(string[] args)
        {

            Console.WriteLine($"{ProgramMetadata} {ProgramMetadata.Copyright}");

            Parser.Default.ParseArguments<CommandOptions>(args)
                   .WithParsed<CommandOptions>(o =>
                   {
                       var arguments = CommandLine.Parser.Default.FormatCommandLine<CommandOptions>(o);
                       Console.WriteLine($"{ProgramMetadata.Product} {arguments}");

                       if(!System.IO.File.Exists(  o.ConfigurationFilename))
                       {
                           Console.Error.WriteLine($"Can't find {o.ConfigurationFilename}");
                           Environment.Exit(2);
                       }

                       var json = System.IO.File.ReadAllText( o.ConfigurationFilename );
                       var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                       var configurationBuilder = new ConfigurationBuilder();
                        configurationBuilder.AddInMemoryCollection(data);
                       _config = configurationBuilder.Build();

                       _rabbitMqEngineConfiguration = RabbitMqEngineConfiguration.CreateDefault();
                       _rabbitMqQueueConfiguration = RabbitMqQueueConfiguration.CreateDefault();

                       foreach (var key in data.Keys)
                       {
                           _rabbitMqEngineConfiguration.SetProperty(key, data[key].ToString());
                           _rabbitMqQueueConfiguration.SetProperty(key, data[key].ToString());
                       }

                       if(!_rabbitMqEngineConfiguration.IsValid)
                       {
                           Console.Error.WriteLine($"Invalid Configuration RabbitMq Engine");
                           Environment.Exit(3);
                       }

                       if (!_rabbitMqQueueConfiguration.IsValid)
                       {
                           Console.Error.WriteLine($"Invalid Configuration RabbitMq Instance");
                           Environment.Exit(4);
                       }

                       _logger = LogFactoryHelper.CreateLogger<Program>();

                       var _client = new RabbitMQClient(_logger, _config);

                       if (o.PurgeExisting)
                       {
                           _logger.LogInformation("Purging existing messages");
                           _client.PurgeQueue(_rabbitMqQueueConfiguration);
                       }
                       else
                       {
                           _logger.LogInformation("Preserving existing messages");
                       }

                       for (int i = 0; i < o.MessageCount; i++)
                       {
                           var msg = StuartWilliams.ScatterGatherMunge.Lib.Utilities.MessageFactory.Make(null, StuartWilliams.ScatterGatherMunge.Lib.Utilities.MessageFactory.MakeHistory(null)).ToJson();
                           _client.Enqueue<string>(msg, _rabbitMqQueueConfiguration);
                       }

                   })
                   .WithNotParsed(errors =>
                   {
                       foreach (var e in errors)
                       {
                           Console.WriteLine($"{e.Tag}");
                       }
                       Environment.ExitCode = -1;
                   });

        }

        #region "Assembly Version"

        private static BlitzAssemblyVersionMetadata _blitzassemblyversionmetadata = null;

        /// <summary>
        /// Semantic Version, etc from Assembly Metadata
        /// </summary>
        public static BlitzAssemblyVersionMetadata ProgramMetadata
        {
            get
            {
                if (_blitzassemblyversionmetadata == null)
                {
                    _blitzassemblyversionmetadata = new BlitzAssemblyVersionMetadata();
                    var assembly = typeof(Program).Assembly;
                    foreach (var attribute in assembly.GetCustomAttributesData())
                    {
                        if (!attribute.TryParse(out string value))
                        {
                            value = string.Empty;
                        }
                        var name = attribute.AttributeType.Name;
                        System.Diagnostics.Trace.WriteLine($"{name}, {value}");
                        _blitzassemblyversionmetadata.PropertySet(name, value);
                    }
                }
                return _blitzassemblyversionmetadata;
            }
        }

        #endregion

    }
}
