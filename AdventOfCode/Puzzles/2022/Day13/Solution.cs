using AdventOfCode.Common;
using System.Text.RegularExpressions;
using static BenchmarkDotNet.Engines.Engine;

namespace AdventOfCode.Puzzles._2022.Day13
{
    internal sealed partial class Solution : ISolution
    {
        private int _result;

        public void PartOne(string[] input)
        {
            var signals = new List<Signal>();

            for (var i = 0; i < input.Length; i += 3)
            {
                signals.Add(new Signal(input[i], input[i + 1]));
            }

            for (var i = 0; i < signals.Count; i++)
            {
                if (signals[i].IsRightOrder())
                    _result += i + 1;
            }

        }

        public void PartTwo(string[] input)
        {
            throw new NotImplementedException();
        }

        private partial class Signal
        {
            public List<int> Signal1 { get; private set; }
            public List<int> Signal2 { get; private set; }

            public Signal(string s1, string s2) 
            {
                Signal1 = new List<int>();
                Signal2 = new List<int>();

                var match = MyRegex().Matches(s1);
                Signal1 = new List<int>(match.Select(x => int.Parse(x.Value)));

                match = MyRegex().Matches(s2);
                Signal2 = new List<int>(match.Select(x => int.Parse(x.Value)));
            }

            public bool IsRightOrder()
            {
                for (var i = 0; i < Signal1.Count; i++)
                {
                    if (Signal2.Count <= i)
                    {
                        return false;
                    }

                    if (Signal1[i] > Signal2[i])
                    {
                        return false;

                    }
                }

                return true;
            }

            [GeneratedRegex("\\d+")]
            private static partial Regex MyRegex();
        }
    }
}
