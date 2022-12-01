using AdventOfCode.Common;
using Microsoft.Diagnostics.Runtime.Utilities;

namespace AdventOfCode.Puzzles._2021.Day06
{
    internal sealed class Solution : ISolution
    {
        private long _result;

        public void PartOne(string[] input)
        {
            _result = CalculateLanternfish(input, 80);
        }

        public void PartTwo(string[] input)
        {
            _result = CalculateLanternfish(input, 256);
        }

        private static long CalculateLanternfish(string[] input, int days)
        {
            var fish = new long[9];

            foreach (var item in input[0].Split(','))
            {
                fish[int.Parse(item)]++;
            }

            for (var iter = 0; iter < days; iter++)
            {
                var zero = fish[0];

                for (var i = 0; i < fish.Length - 1; i++)
                {
                    fish[i] = fish[i + 1];
                }

                fish[8] = zero;
                fish[6] += zero;
            }

            return fish.Sum();
        }

        public void Print()
        {
            Console.WriteLine(_result);
        }

        public bool IsPartOneCorrect()
        {
            return _result == 391671;
        }

        public bool IsPartTwoCorrect()
        {
            return _result == 1754000560399;
        }
    }
}
