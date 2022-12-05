using AdventOfCode.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode.Puzzles._2022.Day05
{
    internal sealed class Solution : ISolution
    {
        public string? _result;

        public void PartOne(string[] input)
        {
            var stacks = new List<Stack<char>>();
            var regex = @"(?:(?:\[|\s)(.)(?:\]|\s)\s?)";
            int index;

            for (index = 0; index < input.Length; index++)
            {
                if (input[index].StartsWith(" 1") || input[index] == " ") continue;
                if (input[index].StartsWith("move")) break;

                var match = Regex.Matches(input[index], regex);

                for (var i = 0; i < match.Count; i++)
                {
                    if (stacks.Count <= i) stacks.Add(new Stack<char>());
                    if (match[i].Groups[1].Value == " ") continue;
                    stacks[i].Push(match[i].Groups[1].Value[0]);
                }
            }

            stacks = stacks.Select(stack => new Stack<char>(stack)).ToList(); // Reverse stacks
            regex = @"move (\d+) from (\d+) to (\d+)";

            for (;index < input.Length; index++)
            {
                var match = Regex.Match(input[index], regex);
                var count = int.Parse(match.Groups[1].Value);
                var i1 = int.Parse(match.Groups[2].Value) - 1;
                var i2 = int.Parse(match.Groups[3].Value) - 1;

                while (count-- > 0) stacks[i2].Push(stacks[i1].Pop());
            }

            foreach(var stack in stacks)
            {
                _result += stack.Peek();
            }
        }

        public void PartTwo(string[] input)
        {
            var stacks = new List<Stack<char>>();
            var regex = @"(?:(?:\[|\s)(.)(?:\]|\s)\s?)";
            var index = 0;

            for (; index < input.Length; index++)
            {
                if (input[index].StartsWith(" 1") || input[index] == " ") continue;
                if (input[index].StartsWith("move")) break;

                var match = Regex.Matches(input[index], regex);

                for (var i = 0; i < match.Count; i++)
                {
                    if (stacks.Count <= i) stacks.Add(new Stack<char>());
                    if (match[i].Groups[1].Value == " ") continue;
                    stacks[i].Push(match[i].Groups[1].Value[0]);
                }
            }

            stacks = stacks.Select(stack => new Stack<char>(stack)).ToList(); // Reverse Stack
            regex = @"move (\d+) from (\d+) to (\d+)";

            var tempStack = new Stack<char>();
            for (; index < input.Length; index++)
            {
                var match = Regex.Match(input[index], regex);
                var count = int.Parse(match.Groups[1].Value);
                var i1 = int.Parse(match.Groups[2].Value) - 1;
                var i2 = int.Parse(match.Groups[3].Value) - 1;
                
                while (count-- > 0) tempStack.Push(stacks[i1].Pop());
                while (tempStack.Count > 0) stacks[i2].Push(tempStack.Pop());
            }

            foreach (var stack in stacks)
            {
                _result += stack.Peek();
            }
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
    }
}
