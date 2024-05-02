using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuartWilliams.ScatterGatherMunge.Lib.Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CalculatorTests
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
        public void RetryCalculator_1()
        {
            var retries = 3;
            var seconds = 5;
            var expected = 4;
            var actual = Calculators.RetryCalculator.JitteredBackoffSeconds(
                retries: retries, 
                baseSeconds: seconds
            );
            _testcontext?.WriteLine($"r: {retries}; s: {seconds}; {expected} < {actual}");
            Assert.IsTrue(actual > expected);
        }

        [TestMethod]
        public void RetryCalculator_2()
        {
            var retries = 3;
            var seconds = 5;
            var baseDate = DateTime.UtcNow;
            var actual = Calculators.RetryCalculator.EarliestDequeueDate(
                baseDate: baseDate,
                retries: retries,
                baseSeconds: seconds
            );
            _testcontext?.WriteLine($"r: {retries}; s: {seconds}; b: {baseDate}; a: {actual}");
            Assert.IsTrue(actual > baseDate);
        }

    }
}
