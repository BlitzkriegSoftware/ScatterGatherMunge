using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuartWilliams.ScatterGatherMunge.Lib.Enums
{
    /// <summary>
    /// Kind: Message
    /// <para>
    /// Note in general this should be a <c><![CDATA[[Flags]]]></c> enumeration
    /// </para>
    /// </summary>
    [Flags]
    public enum MessageKind
    {
        /// <summary>
        /// Unknown (noop)
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Metric(s)
        /// </summary>
        Metric = 1,
        /// <summary>
        /// Error Report
        /// </summary>
        ErrorReport = 2,
        /// <summary>
        /// Diagnostics with or without an error report
        /// </summary>
        Diagnostics = 4,
        /// <summary>
        /// Client Configuration 
        /// </summary>
        ClientConfiguration = 8,
    }
}
