using AdventOfCode.Common;

namespace AdventOfCode.Puzzles._2022.Day01
{
    internal sealed class Solution : ISolution
    {
        private int _result;

        public void PartOne(string[] input)
        {
            _result = GetElfCalories(input).First();
        }

        public void PartTwo(string[] input)
        {
            _result = GetElfCalories(input).Take(3).Sum();
        }

        public static IEnumerable<int> GetElfCalories(string[] input)
        {
            var elfCalories = new List<int>() { 0 };
            foreach (var i in input)
            {
                if (i == "")
                    elfCalories.Add(0);
                else
                    elfCalories[^1] += int.Parse(i);
            }
            return elfCalories.OrderByDescending(x => x);
        }

        public void Print()
        {
            Console.WriteLine(_result);
        }

        public bool IsPartOneCorrect()
        {
            return _result == 68775;
        }

        public bool IsPartTwoCorrect()
        {
            return _result == 202585;
        }
    }
}
