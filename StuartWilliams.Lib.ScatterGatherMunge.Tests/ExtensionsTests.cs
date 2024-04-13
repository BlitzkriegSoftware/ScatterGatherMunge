using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuartWilliams.Lib.ScatterGatherMunge.Tests
{
    [TestClass]
    public class ExtensionsTests
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
        public void PropertyHelper_1()
        {
            var p = Extensions.PropertyHelper<Models.TestMessage>.GetProperty(p => p.RetryCount);
            Assert.IsNotNull(p);
            var isd = Attribute.IsDefined(p, typeof(Attributes.MessageEnrichmentAttribute));
            Assert.IsTrue(isd);
            _testcontext?.WriteLine(p.ToString());
        }
    }
}
