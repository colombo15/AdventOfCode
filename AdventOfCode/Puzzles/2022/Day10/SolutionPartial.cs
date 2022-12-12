using AdventOfCode.Common;

namespace AdventOfCode.Puzzles._2022.Day10
{
    internal sealed partial class Solution : ISolution
    {
        public void Print()
        {
            Console.WriteLine(_result);
        }

        public bool IsPartOneCorrect()
        {
            return _result == 13920;
        }

        public bool IsPartTwoCorrect()
        {
            return true;
        }
    }
}
