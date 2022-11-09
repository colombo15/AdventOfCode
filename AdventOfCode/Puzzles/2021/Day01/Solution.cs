using AdventOfCode.Common;

namespace AdventOfCode.Puzzles._2021.Day01
{
    internal sealed class Solution : ISolution
    {
        private int count = 0;

        public void PartOne(string[] input)
        {
            GenerelizedSolution(input, 1);
        }

        public void PartTwo(string[] input)
        {
            GenerelizedSolution(input, 3);
        }

        private void GenerelizedSolution(string[] input, int gap)
        {
            var intput_int = input.Select(x => int.Parse(x)).ToArray();

            for (var i = gap; i < intput_int.Length; i++)
            {
                count += intput_int[i - gap] < intput_int[i] ? 1 : 0;
            }
        }

        public void Print()
        {
            Console.WriteLine(count);
        }

        public bool IsPartOneCorrect()
        {
            return count == 1448;
        }

        public bool IsPartTwoCorrect()
        {
            return count == 1471;
        }
    }
}
