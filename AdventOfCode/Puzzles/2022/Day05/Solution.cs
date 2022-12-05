using AdventOfCode.Common;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Puzzles._2022.Day05
{
    internal sealed partial class Solution : ISolution
    {
        public string? _result;

        public void PartOne(string[] input)
        {
            _result = GeneralizedSolution(input, CrateMoverModel.CM_9000);
        }

        public void PartTwo(string[] input)
        {
            _result = GeneralizedSolution(input, CrateMoverModel.CM_9001);
        }

        private static string GeneralizedSolution(string[] input, CrateMoverModel model)
        {
            var stacks = new List<Stack<char>>();
            var index = 0;

            for (; index < input.Length; index++)
            {
                if (input[index].StartsWith(" 1") || input[index] == " ") continue;
                if (input[index].StartsWith("move")) break;

                var match = StackRegex().Matches(input[index]);

                for (var i = 0; i < match.Count; i++)
                {
                    if (stacks.Count <= i) stacks.Add(new Stack<char>());
                    if (match[i].Groups[1].Value == " ") continue;
                    stacks[i].Push(match[i].Groups[1].Value[0]);
                }
            }

            stacks = stacks.Select(stack => new Stack<char>(stack)).ToList(); // Reverse Stack

            var tempStack = new Stack<char>();
            for (; index < input.Length; index++)
            {
                var match = MoveRegex().Match(input[index]);
                var count = int.Parse(match.Groups[1].Value);
                var i1 = int.Parse(match.Groups[2].Value) - 1;
                var i2 = int.Parse(match.Groups[3].Value) - 1;

                if (model == CrateMoverModel.CM_9000)
                {
                    while (count-- > 0) stacks[i2].Push(stacks[i1].Pop());
                }
                else if (model == CrateMoverModel.CM_9001)
                {
                    while (count-- > 0) tempStack.Push(stacks[i1].Pop());
                    while (tempStack.Count > 0) stacks[i2].Push(tempStack.Pop());
                }
            }

            var sb = new StringBuilder(stacks.Count);
            foreach (var stack in stacks)
            {
                sb.Append(stack.Peek());
            }

            return sb.ToString();
        }

        public void Print()
        {
            Console.WriteLine(_result);
        }

        public bool IsPartOneCorrect()
        {
            return _result == "BSDMQFLSP";
        }

        public bool IsPartTwoCorrect()
        {
            return _result == "PGSQBFLDP";
        }

        [GeneratedRegex("(?:(?:\\[|\\s)(.)(?:\\]|\\s)\\s?)")]
        private static partial Regex StackRegex();

        [GeneratedRegex("move (\\d+) from (\\d+) to (\\d+)")]
        private static partial Regex MoveRegex();

        private enum CrateMoverModel
        {
            CM_9000,
            CM_9001,
        }
    }
}