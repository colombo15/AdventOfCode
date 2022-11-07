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
    }
}
