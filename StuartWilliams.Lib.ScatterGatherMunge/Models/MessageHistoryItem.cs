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
        /// When
        /// </summary>
        public DateTime Stamp { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// What
        /// </summary>
        public MessageFiniteStateKind Kind { get; set; } = MessageFiniteStateKind.New;

        /// <summary>
        /// How
        /// </summary>
        public string Text { get; set; } = string.Empty;
    }
}
