using StuartWilliams.Lib.ScatterGatherMunge.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuartWilliams.Lib.ScatterGatherMunge.Tests.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class MessageFactory
    {
        public static Models.TestMessage Make(Guid? id, List<StuartWilliams.Lib.ScatterGatherMunge.Models.MessageHistoryItem>? history)
        {
            id ??= Guid.NewGuid();
            history ??= [];
            return new Models.TestMessage()
            {
                Misc = "blah",
                MessageData = new Dictionary<string, object>
                {
                    { "Moo", "Cows" },
                    { "Foo", 99 }
                },
                MessageId = id.ToString(),
                MessageDateStamp = DateTime.UtcNow,
                Version = MessageVersionKind.V1,
                History = history
            };

        }
    }
}
