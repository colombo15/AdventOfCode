using AdventOfCode.Common;

namespace AdventOfCode.Puzzles._2022.Day01
{
    internal sealed class Solution : ISolution
    {
        private int _result;

        public void PartOne(string[] input)
        {
            var elfCalories = new List<int>() { 0 };
            foreach (var i in input)
            {
                if (i == "")
                    elfCalories.Add(0);
                else
                    elfCalories[^1] += int.Parse(i);
            }
            _result = elfCalories.OrderByDescending(x => x).First();
        }

        public void PartTwo(string[] input)
        {
            var elfCalories = new List<int>() { 0 };
            foreach (var i in input)
            {
                if (i == "")
                    elfCalories.Add(0);
                else
                    elfCalories[^1] += int.Parse(i);
            }
            _result = elfCalories.OrderByDescending(x => x).Take(3).Sum();
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
