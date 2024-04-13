using StuartWilliams.Lib.ScatterGatherMunge.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuartWilliams.Lib.ScatterGatherMunge.Models
{
    /// <summary>
    /// Model: Message History Item
    /// </summary>
    public class MessageHistoryItem
    {

        /// <summary>
        /// When: UTC DateTime Stamp
        /// </summary>
        public DateTime Stamp { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Why: State
        /// </summary>
        public MessageFiniteStateKind Kind { get; set; } = MessageFiniteStateKind.New;

        /// <summary>
        /// How: Reason Message
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// What: SubId (for clone)
        /// </summary>
        public string Id { get; set; } = string.Empty;

    }
}
