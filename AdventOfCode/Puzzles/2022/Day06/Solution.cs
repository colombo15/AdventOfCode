using AdventOfCode.Common;
using System.Diagnostics;

namespace AdventOfCode.Puzzles._2022.Day06
{
    internal sealed class Solution : ISolution
    {
        private int _result;

        public void PartOne(string[] input)
        {
            _result = GeneralizedSolution(input[0], 4);
        }

        public void PartTwo(string[] input)
        {
            _result = GeneralizedSolution(input[0], 14);
        }

        private static int GeneralizedSolution(string input, int msgLength)
        {
            var hs = new HashSet<char>();

            for (var i = 0; i < input.Length; i++)
            {
                var span = input.AsSpan(i, msgLength);
                foreach (var c in span)
                {
                    hs.Add(c);
                }

                if (hs.Count == msgLength)
                {
                    return i + msgLength;
                }
                hs.Clear();
            }
            throw new UnreachableException();
        }

        public void Print()
        {
            Console.WriteLine(_result);
        }

        public bool IsPartOneCorrect()
        {
            return _result == 1848;
        }

        public bool IsPartTwoCorrect()
        {
            return _result == 2308;
        }
    }
}
