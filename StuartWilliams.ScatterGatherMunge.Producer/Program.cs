using CommandLine;
using StuartWilliams.ScatterGatherMunge.Lib.Models;
using StuartWilliams.ScatterGatherMunge.Lib.Extensions;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace StuartWilliams.ScatterGatherMunge.Producer
{
    internal class Program
    {
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

                       foreach(var key in data.Keys )
                       {
                           RabbitMqEngineConfiguration.SetProperty(key, data[key].ToString());
                           RabbitMqInstanceConfiguration.SetProperty(key, data[key].ToString());
                       }

                       if(!RabbitMqEngineConfiguration.IsValid)
                       {
                           Console.Error.WriteLine($"Invalid Configuration RabbitMq Engine");
                           Environment.Exit(3);
                       }

                       if (!RabbitMqInstanceConfiguration.IsValid)
                       {
                           Console.Error.WriteLine($"Invalid Configuration RabbitMq Instance");
                           Environment.Exit(4);
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

        static StuartWilliams.ScatterGatherMunge.Lib.Models.RabbitMqEngineConfiguration RabbitMqEngineConfiguration { get; } = new();
        static StuartWilliams.ScatterGatherMunge.Lib.Models.RabbitMqInstanceConfiguration RabbitMqInstanceConfiguration { get; } = new();

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
