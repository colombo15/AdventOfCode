using AdventOfCode.Common;
using System.Linq;

namespace AdventOfCode.Puzzles._2021.Day07
{
    internal sealed class Solution : ISolution
    {
        private int _result;

        public void PartOne(string[] input)
        {
            static int lambda(int target, int x) { return Math.Abs(target - x); }

            var subs = input[0].Split(',').Select(x => int.Parse(x)).OrderBy(x => x).ToArray();
            var median = subs[(int)Math.Floor((double)(subs.Length + 1) / 2)];
            _result = CalculateFuel(subs, median, lambda);
        }

        public void PartTwo(string[] input)
        {
            static int lambda(int target, int x) { return Enumerable.Range(0, Math.Abs(target - x) + 1).Sum(); }

            var subs = input[0].Split(',').Select(x => int.Parse(x)).OrderBy(x => x).ToArray();
            var avg = subs.Average();
            var calc1 = CalculateFuel(subs, (int)Math.Floor(avg), lambda);
            var calc2 = CalculateFuel(subs, (int)Math.Ceiling(avg), lambda);
            _result = calc1 < calc2 ? calc1: calc2;
        }

        private static int CalculateFuel(int[] subs, int target, Func<int, int, int> lambda)
        {
            return subs.Select(x => lambda(target, x)).Sum();
        }

        public void Print()
        {
            Console.WriteLine(_result);
        }

        public bool IsPartOneCorrect()
        {
            return _result == 342534;
        }

        public bool IsPartTwoCorrect()
        {
            return _result == 94004208;
        }
    }
}
