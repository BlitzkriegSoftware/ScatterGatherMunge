using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using BlitzkriegSoftware.MsTest;

namespace StuartWilliams.ScatterGatherMunge.Lib.Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ModelTests
    {
        #region "Boilerplate"

        private static TestContext? _testcontext;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _testcontext = context;
        }

        #endregion

        [TestMethod]
        public void TestMessage_1()
        {
            var model = Helpers.MessageFactory.Make(id: null, history: null);
            Assert.IsNotNull(model);
            TestJsonSerializationHelper.AssertJsonSerialization<Models.TestMessage>(_testcontext, model);
            Assert.IsTrue(model.IsValid(out _));
        }

        [TestMethod]
        public void TestMessage_2()
        {
            var model = Helpers.MessageFactory.Make(id: null, history: null);
            Assert.IsNotNull(model);
            model.MessageId = null;
            TestJsonSerializationHelper.AssertJsonSerialization<Models.TestMessage>(_testcontext, model);
            Assert.IsFalse(model.IsValid(out List<System.ComponentModel.DataAnnotations.ValidationResult> validationErrors));
            Assert.IsTrue(validationErrors.Count > 0);
        }

        [TestMethod]
        public void TestMessage_2a()
        {
            var model = Helpers.MessageFactory.Make(id: null, history: null);
            Assert.IsNotNull(model);
            model.History = [];
            Assert.IsTrue(model.IsValid(out _));
        }

        [TestMethod]
        public void TestMessage_2b()
        {
            var model = Helpers.MessageFactory.Make(id: null, history: null);
            Assert.IsNotNull(model);
            model.History =
            [
                new ScatterGatherMunge.Lib.Models.MessageHistoryItem()
                {
                     Text = "Test Done",
                     Kind = Enums.MessageFiniteStateKind.Completed,
                     Stamp = DateTime.UtcNow
                },
                new ScatterGatherMunge.Lib.Models.MessageHistoryItem()
                {
                     Text = "Test In",
                     Kind = Enums.MessageFiniteStateKind.New,
                     Stamp = DateTime.UtcNow.AddMinutes(-15),
                },
            ];
            Assert.IsTrue(model.History.Count != 0);

            _testcontext?.WriteLine(model.ToString());
        }

        [TestMethod]
        public void TestMessage_2c()
        {
            var id = Guid.NewGuid().ToString();
            var model = Helpers.MessageFactory.Make(id: null, history: null);
            Assert.IsNotNull(model);
            model.History =
            [
                new ScatterGatherMunge.Lib.Models.MessageHistoryItem()
                {
                     Text = "Test Done",
                     Kind = Enums.MessageFiniteStateKind.Completed,
                     Stamp = DateTime.UtcNow,
                     Id = "Nope"
                },
                new ScatterGatherMunge.Lib.Models.MessageHistoryItem()
                {
                     Text = "Test In",
                     Kind = Enums.MessageFiniteStateKind.New,
                     Stamp = DateTime.UtcNow.AddMinutes(-30),
                     Id = id
                },
                new ScatterGatherMunge.Lib.Models.MessageHistoryItem()
                {
                     Text = "Test In",
                     Kind = Enums.MessageFiniteStateKind.New,
                     Stamp = DateTime.UtcNow.AddMinutes(-60),
                },
            ];
            Assert.IsTrue(model.History.Count != 0);

            _testcontext?.WriteLine($"SubId: {id}\n{model}");

            if(!model.SubIdentityTryGet(out var subMessageId))
            {
                Assert.Fail("Should have id");
            }

            if(!subMessageId.Equals(id, StringComparison.OrdinalIgnoreCase))
            {
                Assert.Fail("Not right id");
            }
        }

        [TestMethod]
        public void TestMessage_2d()
        {
            var model = Helpers.MessageFactory.Make(id: null, history: null);
            Assert.IsNotNull(model);
            model.History =
            [
                new ScatterGatherMunge.Lib.Models.MessageHistoryItem()
                {
                     Text = "Test Done",
                     Kind = Enums.MessageFiniteStateKind.Completed,
                     Stamp = DateTime.UtcNow,
                },
                new ScatterGatherMunge.Lib.Models.MessageHistoryItem()
                {
                     Text = "Test In",
                     Kind = Enums.MessageFiniteStateKind.New,
                     Stamp = DateTime.UtcNow.AddMinutes(-30),
                },
                new ScatterGatherMunge.Lib.Models.MessageHistoryItem()
                {
                     Text = "Test In",
                     Kind = Enums.MessageFiniteStateKind.New,
                     Stamp = DateTime.UtcNow.AddMinutes(-60),
                },
            ];
            Assert.IsTrue(model.History.Count != 0);

            _testcontext?.WriteLine($"{model}");

            if (model.SubIdentityTryGet(out var _))
            {
                Assert.Fail("Should not have id");
            }
        }

        [TestMethod]
        public void TestMessage_3()
        {
            var model = Helpers.MessageFactory.Make(id: null, history: null);
            Assert.IsNotNull(model);
            model.MessageData = null;
            TestJsonSerializationHelper.AssertJsonSerialization<Models.TestMessage>(_testcontext, model);
            Assert.IsFalse(model.IsValid(out List<System.ComponentModel.DataAnnotations.ValidationResult> validationErrors));
            Assert.IsTrue(validationErrors.Count > 0);
            bool errorFound = false;
            foreach (var ve in validationErrors)
            {
                if (ve.MemberNames.Where(t => t.Contains("MessageData")).Any())
                {
                    errorFound = true;
                }
            }
            Assert.IsTrue(errorFound);
        }

        [TestMethod]
        public void TestMessage_3a()
        {
            var model = Helpers.MessageFactory.Make(id: null, history: null);
            Assert.IsNotNull(model);
            model.MessageData = [];
            TestJsonSerializationHelper.AssertJsonSerialization<Models.TestMessage>(_testcontext, model);
            Assert.IsFalse(model.IsValid(out List<System.ComponentModel.DataAnnotations.ValidationResult> validationErrors));
            Assert.IsTrue(validationErrors.Count > 0);
            bool errorFound = false;
            foreach (var ve in validationErrors)
            {
                if (ve.MemberNames.Where(t => t.Contains("MessageData")).Any())
                {
                    errorFound = true;
                }
            }
            Assert.IsTrue(errorFound);
        }

        [TestMethod]
        public void TestMessage_6()
        {
            var model = Helpers.MessageFactory.Make(id: null, history: null);
            model.History.Add(new ScatterGatherMunge.Lib.Models.MessageHistoryItem()
            {
                Kind = Enums.MessageFiniteStateKind.Requeued,
                Stamp = DateTime.UtcNow,
                Text = "Too Busy"
            });
            Assert.IsTrue(model.History.Count != 0);
        }

    }
}