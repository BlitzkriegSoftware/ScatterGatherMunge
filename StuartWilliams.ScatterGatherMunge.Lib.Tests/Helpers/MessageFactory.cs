﻿using StuartWilliams.ScatterGatherMunge.Lib.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuartWilliams.ScatterGatherMunge.Lib.Tests.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class MessageFactory
    {
        public static Models.TestMessage Make(Guid? id, List<StuartWilliams.ScatterGatherMunge.Lib.Models.MessageHistoryItem>? history)
        {
            id ??= Guid.NewGuid();
            history ??= [];
            MessageKind kind = MessageKind.Unknown;
            foreach(Enums.MessageKind e in Enum.GetValues(typeof(Enums.MessageKind)))
            {
                if(Calculators.SecureRandomCalculator.SecureRandom.Next(0,9) > 5)
                {
                    kind |= e;
                }
            }
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
                MessageKind = kind,
                History = history
            };

        }
    }
}