using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuartWilliams.Lib.ScatterGatherMunge.Attributes
{
    /// <summary>
    /// Attribute: Indicates that this <c>Property</c> is expected in the message metadata
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class MessageMetadataAttribute : System.Attribute
    {
    }
}
