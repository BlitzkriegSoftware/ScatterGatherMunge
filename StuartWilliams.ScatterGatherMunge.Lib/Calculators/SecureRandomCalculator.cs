using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuartWilliams.ScatterGatherMunge.Lib.Calculators
{
    /// <summary>
    /// Calculator: Better Random Numbers
    /// </summary>
    public static class SecureRandomCalculator
    {

        /// <summary>
        /// Random Number Generator
        /// </summary>
        public static readonly BlitzkriegSoftware.SecureRandomLibrary.SecureRandom SecureRandom = new();

    }
}
