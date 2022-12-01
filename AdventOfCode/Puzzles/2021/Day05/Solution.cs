using AdventOfCode.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode.Puzzles._2021.Day05
{
    internal sealed class Solution : ISolution
    {
        private int _result;

        public void PartOne(string[] input)
        {
            _result = GetOverlapCount(input);
        }

        public void PartTwo(string[] input)
        {
            _result = GetOverlapCount(input, false);
        }

        private static int GetOverlapCount(string[] input, bool ignoreDisagonal = true)
        {
            var dic = new Dictionary<string, int>();
            var regex = @"(\d+),(\d+) -> (\d+),(\d+)";

            foreach (var item in input)
            {
                var match = Regex.Match(item, regex);
                var x1 = int.Parse(match.Groups[1].Value);
                var y1 = int.Parse(match.Groups[2].Value);
                var x2 = int.Parse(match.Groups[3].Value);
                var y2 = int.Parse(match.Groups[4].Value);

                if (ignoreDisagonal && !(x1 == x2 || y1 == y2)) continue;

                var seqX = HelperMethods.Sequence(x1, x2).ToArray();
                var seqY = HelperMethods.Sequence(y1, y2).ToArray();

                var i = 0;
                var j = 0;

                while (true)
                {
                    var key = seqX[i] + "," + seqY[j];
                    dic.TryAdd(key, 0);
                    dic[key]++;
                    if (i == seqX.Length - 1 && j == seqY.Length - 1) break;
                    if (i < seqX.Length - 1) i++;
                    if (j < seqY.Length - 1) j++;
                }
            }

            return dic.AsEnumerable().Where((kvp) => kvp.Value > 1).Count();
        }

        public void Print()
        {
            Console.WriteLine(_result);
        }

        public bool IsPartOneCorrect()
        {
            return _result == 7297;
        }

        public bool IsPartTwoCorrect()
        {
            return _result == 21038;
        }
    }
}
