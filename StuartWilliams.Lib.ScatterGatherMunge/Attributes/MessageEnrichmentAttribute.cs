using System;

namespace StuartWilliams.Lib.ScatterGatherMunge.Attributes
{

    /// <summary>
    /// Attribute: Indicates the enriched fields e.g., fields updated during processing
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class MessageEnrichmentAttribute : System.Attribute { }
}
