using AdventOfCode.Common;

namespace AdventOfCode.Puzzles._2022.Day11
{
    internal sealed partial class Solution : ISolution
    {
        public void Print()
        {
            Console.WriteLine(_result);
        }

        public bool IsPartOneCorrect()
        {
            return _result == 151312;
        }

        public bool IsPartTwoCorrect()
        {
            return _result == 51382025916;
        }
    }
}
