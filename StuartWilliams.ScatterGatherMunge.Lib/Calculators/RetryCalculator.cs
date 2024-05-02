using System;

namespace StuartWilliams.ScatterGatherMunge.Lib.Calculators
{
    /// <summary>
    /// Calculator: Retries
    /// <para>Uses a crptograpic random generator</para>
    /// </summary>
    public static class RetryCalculator
    {

        /// <summary>
        /// Base Seconds
        /// </summary>
        public const int BaseSeconds = 10;

        /// <summary>
        /// Calculate the minimum number of seconds to wait before trying to reprocess
        /// </summary>
        /// <param name="retries">(sic)</param>
        /// <param name="baseSeconds">Base Seconds</param>
        /// <returns>Minimum Seconds To Wait</returns>
        public static int JitteredBackoffSeconds(int retries, int baseSeconds = BaseSeconds)
        {
            var delay = 2^retries * baseSeconds;
            int jitterMax = (int)(delay * 0.10);
            int jitter = Calculators.SecureRandomCalculator.SecureRandom.Next(0, jitterMax);
            delay += jitter;
            return delay;
        }

        /// <summary>
        /// Compute Earliest Possible Date/Time
        /// </summary>
        /// <param name="baseDate">Base Date</param>
        /// <param name="retries">(sic)</param>
        /// <param name="baseSeconds">Base Seconds</param>
        /// <returns>Minumum DateTime Message Can Be Reprocessed</returns>
        public static DateTime EarliestDequeueDate(DateTime? baseDate, int retries, int baseSeconds = BaseSeconds)
        {
            baseDate ??= DateTime.UtcNow;
            var backoffSeconds = JitteredBackoffSeconds((int)retries, baseSeconds);
            return baseDate.Value.AddSeconds(backoffSeconds);
        }

    }
}
