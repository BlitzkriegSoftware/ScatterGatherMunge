using System;

namespace StuartWilliams.Lib.ScatterGatherMunge.Attributes
{

    /// <summary>
    /// Attribute: Indicates that this <c>Property</c> is expected in the message metadata
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class MessageMetadataAttribute : System.Attribute { }

}
