using AdventOfCode.Common;
using Microsoft.Diagnostics.Runtime.Utilities;

namespace AdventOfCode.Puzzles._2022.Day16
{
    internal sealed partial class Solution : ISolution
    {
        public void Print()
        {
            Console.WriteLine(_result);
        }

        public bool IsPartOneCorrect()
        {
            return _result == 1751;
        }

        public bool IsPartTwoCorrect()
        {
            return _result == 1751;
        }
    }
}
