using AdventOfCode.Common;
using System.Diagnostics;

namespace AdventOfCode.Puzzles._2022.Day02
{
    internal sealed class Solution : ISolution
    {
        private int _result;

        public void PartOne(string[] input)
        {
            foreach (var item in input)
            {
                var split = item.Split(' ');
                var oppMove = split[0][0];
                var myMove = (char)(split[1][0] - 23);
                _result += GetScore(oppMove, myMove);
            }
        }

        public void PartTwo(string[] input)
        {
            foreach (var item in input)
            {
                var split = item.Split(' ');
                var oppMove = split[0][0];
                var myMove = GetMyMove(oppMove, split[1][0]);
                _result += GetScore(oppMove, myMove);
            }
        }

        private static int GetScore(char oppMove, char myMove)
        {
            var win = myMove - oppMove == 1 || myMove - oppMove == -2;
            var draw = myMove - oppMove == 0;
            return (myMove - 64) + (win ? 6 : 0) + (draw ? 3 : 0);
        }

        private static char GetMyMove(char oppMove, char myMove)
        {
            return myMove switch
            {
                'X' => oppMove == 'A' ? 'C' : (char)(oppMove - 1),
                'Y' => oppMove,
                'Z' => oppMove == 'C' ? 'A' : (char)(oppMove + 1),
                _ => throw new UnreachableException(),
            };
        }

        public void Print()
        {
            Console.WriteLine(_result);
        }

        public bool IsPartOneCorrect()
        {
            return _result == 10994;
        }

        public bool IsPartTwoCorrect()
        {
            return _result == 12526;
        }
    }
}
