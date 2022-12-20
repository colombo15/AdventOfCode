using AdventOfCode.Common;

namespace AdventOfCode.Puzzles._2022.Day19
{
    internal sealed partial class Solution : ISolution
    {
        public void Print()
        {
            Console.WriteLine(_result);
        }

        public bool IsPartOneCorrect()
        {
            return _result == 4320;
        }

        public bool IsPartTwoCorrect()
        {
            return _result == 1751;
        }
    }
}
