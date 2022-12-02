using AdventOfCode.Common;

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
                var myMove = split[1][0];
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
            var win = (oppMove == 'A' && myMove == 'Y') || (oppMove == 'B' && myMove == 'Z') || (oppMove == 'C' && myMove == 'X');
            var draw = (oppMove == 'A' && myMove == 'X') || (oppMove == 'B' && myMove == 'Y') || (oppMove == 'C' && myMove == 'Z');
            return (myMove - 87) + (win ? 6 : 0) + (draw ? 3 : 0);
        }

        private static char GetMyMove(char oppMove, char myMove)
        {
            switch (myMove)
            {
                case 'X':
                    return oppMove == 'A' ? 'Z' : oppMove == 'B' ? 'X' : 'Y';
                case 'Y':
                    return oppMove == 'A' ? 'X' : oppMove == 'B' ? 'Y' : 'Z';
                case 'Z':
                    return oppMove == 'A' ? 'Y' : oppMove == 'B' ? 'Z' : 'X';
                default:
                    throw new NotImplementedException();
            }
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
