using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Common
{
    internal static class HelperMethods
    {
        /// <summary>
        ///     Generates sequence of int between 2 numbers (inclusive)
        ///     Doesn't matter if n1 > n2
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <returns></returns>
        public static IEnumerable<int> Sequence(int n1, int n2)
        {
            var descending = n1 > n2;

            if (descending)
            {
                while (n1 >= n2)
                {
                    yield return n1--;
                }
            }
            else
            {
                while (n1 <= n2)
                {
                    yield return n1++;
                }
            }
        }
    }
}
