using AdventOfCode.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode.Puzzles._2022.Day05
{
    internal sealed class Solution : ISolution
    {
        public string _result;

        public void PartOne(string[] input)
        {
            var regex = @"(?:(?:\[|\s)(.)(?:\]|\s)\s?)";
            var stacks = new List<Stack<char>>();
            var index = 0;

            for (index = 0; index < input.Length; index++)
            {
                if (input[index].StartsWith(" 1")) break;

                var match = Regex.Matches(input[index], regex);
                
                for (var i = 0; i < match.Count; i++)
                {
                    if (stacks.Count <= i) stacks.Add(new Stack<char>());
                    if (match[i].Groups[1].Value == " ") continue;
                    stacks[i].Push(match[i].Groups[1].Value[0]);
                }
            }

            stacks = stacks.Select(stack => new Stack<char>(stack)).ToList();
            index += 2;
            regex = @"move (\d+) from (\d+) to (\d+)";

            for (;index < input.Length; index++)
            {
                var match = Regex.Match(input[index], regex);
                var x1 = int.Parse(match.Groups[1].Value);
                var x2 = int.Parse(match.Groups[2].Value);
                var x3 = int.Parse(match.Groups[3].Value);

                while (x1 > 0)
                {
                    var p = stacks[x2 - 1].Pop();
                    stacks[x3 - 1].Push(p);
                    x1--;
                }
            }

            foreach(var stack in stacks)
            {
                _result += stack.Peek();
            }
        }

        public void PartTwo(string[] input)
        {
            var regex = @"(?:(?:\[|\s)(.)(?:\]|\s)\s?)";
            var stacks = new List<Stack<char>>();
            var index = 0;

            for (index = 0; index < input.Length; index++)
            {
                if (input[index].StartsWith(" 1")) break;

                var match = Regex.Matches(input[index], regex);

                for (var i = 0; i < match.Count; i++)
                {
                    if (stacks.Count <= i) stacks.Add(new Stack<char>());
                    if (match[i].Groups[1].Value == " ") continue;
                    stacks[i].Push(match[i].Groups[1].Value[0]);
                }
            }

            stacks = stacks.Select(stack => new Stack<char>(stack)).ToList();
            index += 2;
            regex = @"move (\d+) from (\d+) to (\d+)";

            for (; index < input.Length; index++)
            {
                var match = Regex.Match(input[index], regex);
                var x1 = int.Parse(match.Groups[1].Value);
                var x2 = int.Parse(match.Groups[2].Value);
                var x3 = int.Parse(match.Groups[3].Value);

                var s = new Stack<char>();
                while (x1 > 0)
                {
                    s.Push(stacks[x2 - 1].Pop());
                    x1--;
                }

                while (s.Count > 0)
                {
                    stacks[x3 - 1].Push(s.Pop());
                }
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
            return false;
        }
    }
}
