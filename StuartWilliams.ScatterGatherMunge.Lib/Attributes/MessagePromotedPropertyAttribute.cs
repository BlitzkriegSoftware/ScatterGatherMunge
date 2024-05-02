using System;

namespace StuartWilliams.ScatterGatherMunge.Lib.Attributes
{

    /// <summary>
    /// Attribute: Indicates that this <c>Property</c> is expected in the message metadata
    /// <para>And is in the header e.g, <c>promoted</c> for subscription rules</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class MessagePromotedPropertyAttribute : System.Attribute { }

}
