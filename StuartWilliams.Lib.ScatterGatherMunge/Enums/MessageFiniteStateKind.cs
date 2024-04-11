using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuartWilliams.Lib.ScatterGatherMunge.Enums
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
        /// Completed
        /// </summary>
        Completed = 2,
        /// <summary>
        /// Bad Format
        /// </summary>
        BadFormat = 16,
        /// <summary>
        /// Rejected (dead letter)
        /// </summary>
        Rejected = 32,
    }
}
