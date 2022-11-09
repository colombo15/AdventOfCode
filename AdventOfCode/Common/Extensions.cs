using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Common
{
    internal static class Extensions
    {
        public static int Sum(this ReadOnlySpan<int> numbers)
        {
            var total = 0;
            foreach (var i in numbers) total += i;
            return total;
        }

        public static uint ConvertBinaryToUInt(this string str)
        {
            var retval = (uint)0;
            for(var i = 0; i < str.Length; i++)
            {
                retval += str[^(1 + i)] == '1' ? (uint)Math.Pow(2.0, (double)(i)) : 0;
            }
            return retval;
        }
    }
}
