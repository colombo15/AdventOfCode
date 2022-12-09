using AdventOfCode.Common;
using System.Diagnostics;

namespace AdventOfCode.Puzzles._2022.Day09
{
    internal sealed partial class Solution : ISolution
    {
        public void Print()
        {
            Console.WriteLine(_result);
        }

        public bool IsPartOneCorrect()
        {
            return _result == 6376;
        }

        public bool IsPartTwoCorrect()
        {
            return _result == 2607;
        }
    }
}
