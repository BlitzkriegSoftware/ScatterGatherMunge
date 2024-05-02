using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuartWilliams.ScatterGatherMunge.Lib.Attributes
{
    /// <summary>
    /// This value comes from the Queueing System
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MessageInjectedPropertyAttribute : Attribute { }
}
