using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuartWilliams.ScatterGatherMunge.Lib.Enums
{

    /// <summary>
    /// Enum: Message Finite State
    /// </summary>
    [Flags]
    public enum MessageFiniteStateKind
    {
        /// <summary>
        /// Default
        /// </summary>
        New = 0,
        /// <summary>
        /// Requeued
        /// </summary>
        Requeued = 1,
        /// <summary>
        /// Cloned
        /// </summary>
        Cloned = 8,
        /// <summary>
        /// Completed, success
        /// </summary>
        Completed = 64,
        /// <summary>
        /// Rejected (dead letter), bad format or bad data
        /// </summary>
        Rejected = 128,
    }
}
