using CommandLine;
using StuartWilliams.ScatterGatherMunge.Lib.Models;
using StuartWilliams.ScatterGatherMunge.Lib.Extensions;
using System;

namespace StuartWilliams.ScatterGatherMunge.Consumer
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine($"{Program.ProgramMetadata} {Program.ProgramMetadata.Copyright}");

            Parser.Default.ParseArguments<CommandOptions>(args)
                   .WithParsed<CommandOptions>(o =>
                   {
                       var arguments = CommandLine.Parser.Default.FormatCommandLine<CommandOptions>(o);
                       Console.WriteLine($"{Program.ProgramMetadata.Product} {arguments}");

                       // TODO: 
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
