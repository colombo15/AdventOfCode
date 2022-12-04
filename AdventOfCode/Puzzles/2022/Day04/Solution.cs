using AdventOfCode.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode.Puzzles._2022.Day04
{
    internal sealed class Solution : ISolution
    {
        private int _result;

        public void PartOne(string[] input)
        {
            static bool lambda(int x1, int x2, int y1, int y2)
            {
                return (y1 >= x1 && y1 <= x2 && y2 >= x1 && y2 <= x2) || (x1 >= y1 && x1 <= y2 && x2 >= y1 && x2 <= y2);
            }

            _result = GeneralizedSolution(input, lambda);
        }

        public void PartTwo(string[] input)
        {
            static bool lambda(int x1, int x2, int y1, int y2)
            {
                return (y1 >= x1 && y1 <= x2) || (y2 >= x1 && y2 <= x2) || (x1 >= y1 && x1 <= y2) || (x2 >= y1 && x2 <= y2);
            }

            _result = GeneralizedSolution(input, lambda);
        }

        private static int GeneralizedSolution(string[] input, Func<int, int, int, int, bool> comparer)
        {
            var regex = @"(\d+)-(\d+),(\d+)-(\d+)";
            var result = 0;

            foreach (var item in input)
            {
                var match = Regex.Match(item, regex);
                var x1 = int.Parse(match.Groups[1].Value);
                var x2 = int.Parse(match.Groups[2].Value);
                var y1 = int.Parse(match.Groups[3].Value);
                var y2 = int.Parse(match.Groups[4].Value);

                if (comparer(x1, x2, y1, y2))
                    result++;
            }

            return result;
        }

        public void Print()
        {
            Console.WriteLine(_result);
        }

        public bool IsPartOneCorrect()
        {
            return _result == 562;
        }

        public bool IsPartTwoCorrect()
        {
            return _result == 924;
        }
    }
}
