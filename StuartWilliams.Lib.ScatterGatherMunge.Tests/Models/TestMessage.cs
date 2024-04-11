using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuartWilliams.Lib.ScatterGatherMunge.Tests.Models
{
    [ExcludeFromCodeCoverage]
    public class TestMessage: StuartWilliams.Lib.ScatterGatherMunge.Models.MessageBase
    {
        public string Misc { get; set; } = string.Empty;
    }
}
