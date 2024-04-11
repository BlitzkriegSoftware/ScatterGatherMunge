using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuartWilliams.Lib.ScatterGatherMunge.Attribute
{
    /// <summary>
    /// Attribute: Indicates the system enriched fields
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class MessageEnrichmentAttribute : System.Attribute
    {
    }
}
