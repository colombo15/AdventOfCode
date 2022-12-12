using AdventOfCode.Common;

namespace AdventOfCode.Puzzles._2022.Day12
{
    internal sealed partial class Solution : ISolution
    {
        public void Print()
        {
            Console.WriteLine(_result);
        }

        public bool IsPartOneCorrect()
        {
            return _result == 490;
        }

        public bool IsPartTwoCorrect()
        {
            return false;
        }
    }
}
