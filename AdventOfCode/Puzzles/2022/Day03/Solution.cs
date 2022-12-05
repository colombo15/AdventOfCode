using AdventOfCode.Common;

namespace AdventOfCode.Puzzles._2022.Day03
{
    internal sealed class Solution : ISolution
    {
        private int _result;

        public void PartOne(string[] input)
        {
            foreach (var str in input)
            {
                var itemTypes = new HashSet<char>();
                var both = new HashSet<char>();

                var subStrLength = str.Length / 2;

                for (var i = 0; i < subStrLength; i++)
                {
                    itemTypes.Add(str[i]);
                }

                for (var i = subStrLength; i < str.Length; i++)
                {
                    if (itemTypes.Contains(str[i]) && both.Add(str[i]))
                    {
                        _result += char.IsLower(str[i]) ? str[i] - 96 : str[i] - 38;
                    }
                }
            }
        }

        public void PartTwo(string[] input)
        {
            for (var i = 0; i < input.Length; i += 3)
            {
                var common1 = new HashSet<char>();
                var common2 = new HashSet<char>();

                foreach (var c in input[i])
                {
                    common1.Add(c);
                }

                foreach (var c in input[i + 1])
                {
                    if (common1.Contains(c))
                    {
                        common2.Add(c);
                    }
                }

                foreach (var c in input[i + 2])
                {
                    if (common2.Contains(c))
                    {
                        _result += char.IsLower(c) ? c - 96 : c - 38;
                        break;
                    }
                }
            }
        }

        public void Print()
        {
            Console.WriteLine(_result);
        }

        public bool IsPartOneCorrect()
        {
            return _result == 8185;
        }

        public bool IsPartTwoCorrect()
        {
            return _result == 2817;
        }
    }
}
