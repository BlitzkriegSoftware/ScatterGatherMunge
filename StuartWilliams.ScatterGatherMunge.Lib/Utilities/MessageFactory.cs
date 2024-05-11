using BlitzkriegSoftware.SecureRandomLibrary;
using StuartWilliams.ScatterGatherMunge.Lib.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace StuartWilliams.ScatterGatherMunge.Lib.Utilities
{

    /// <summary>
    /// Generate TEST messages
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class MessageFactory
    {
        /// <summary>
        /// Make Test Messages
        /// </summary>
        /// <param name="id">Unique Id</param>
        /// <param name="history">History</param>
        /// <returns>MessageBase - filled out</returns>
        public static Models.MessageBase Make(Guid? id, List<StuartWilliams.ScatterGatherMunge.Lib.Models.MessageHistoryItem> history)
        {
            id ??= Guid.NewGuid();
            history ??= new();

            MessageKind kind = MessageKind.Unknown;
            foreach (Enums.MessageKind e in Enum.GetValues(typeof(Enums.MessageKind)))
            {
                if (Calculators.SecureRandomCalculator.SecureRandom.Next(0, 9) > 5)
                {
                    kind |= e;
                }
            }

            return new Models.MessageBase()
            {
                MessageData = new Dictionary<string, object>
                {
                    { "Moo", "Cows" },
                    { "Foo", 99 }
                },
                MessageId = id.ToString(),
                MessageDateStamp = DateTime.UtcNow,
                Version = MessageVersionKind.V1,
                MessageKind = kind,
                History = history
            };
        }

        /// <summary>
        /// Make History
        /// </summary>
        /// <param name="count">(optional) count</param>
        /// <returns>List</returns>
        public static List<StuartWilliams.ScatterGatherMunge.Lib.Models.MessageHistoryItem> MakeHistory(int? count)
        {
            count ??= Calculators.SecureRandomCalculator.SecureRandom.Next(3, 9);
            var list = new List<StuartWilliams.ScatterGatherMunge.Lib.Models.MessageHistoryItem>();

            for (int i= 0; i < count; i++)
            {
                var m = new Models.MessageHistoryItem()
                {
                    Id = i.ToString(),
                    Kind = (i <= 0 ? MessageFiniteStateKind.New: MessageFiniteStateKind.Requeued),
                    Stamp = DateTime.UtcNow,
                    Text = $"Text: {i}"
                };
                list.Add(m);
            }

            return list;
        }

    }
}
